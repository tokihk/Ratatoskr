using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Native;
using Ratatoskr.Generic;

namespace Ratatoskr.Drivers.SerialPort
{
    internal enum SerialPortParity
    {
        None,
        Odd,
        Even,
        Mark,
        Space,
    }

    internal enum SerialPortStopBits
    {
        None,
        One,
        OnePointFive,
        Two,
    }

    internal enum fDtrControlType
    {
        DTR_CONTROL_DISABLE,
        DTR_CONTROL_ENABLE,
        DTR_CONTROL_HANDSHAKE,
    }

    internal enum fRtsControlType
    {
        RTS_CONTROL_DISABLE,
        RTS_CONTROL_ENABLE,
        RTS_CONTROL_HANDSHAKE,
        RTS_CONTROL_TOGGLE,
    }

    [Flags]
    internal enum ModemStatus
    {
        CTS_ON      = (int)WinAPI.MS_CTS_ON,
        DSR_ON      = (int)WinAPI.MS_DSR_ON,
        RING_ON     = (int)WinAPI.MS_RING_ON,
        RLSD_ON     = (int)WinAPI.MS_RLSD_ON,
    }

    [Flags]
    internal enum ErrorStatus
    {
        BREAK    = (int)WinAPI.CE_BREAK,
        DNS      = (int)WinAPI.CE_DNS,
        FRAME    = (int)WinAPI.CE_FRAME,
        IOE      = (int)WinAPI.CE_IOE,
        MODE     = (int)WinAPI.CE_MODE,
        OOP      = (int)WinAPI.CE_OOP,
        OVERRUN  = (int)WinAPI.CE_OVERRUN,
        PTO      = (int)WinAPI.CE_PTO,
        RXOVER   = (int)WinAPI.CE_RXOVER,
        RXPARITY = (int)WinAPI.CE_RXPARITY,
        TXFULL   = (int)WinAPI.CE_TXFULL,
    }

    [Flags]
    internal enum CommStatus
    {
        CTS_HOLD    = 1 << WinAPI.COMSTAT.FlagsParamOffset.CtsHold,
        DSR_HOLD    = 1 << WinAPI.COMSTAT.FlagsParamOffset.DsrHold,
        RLSD_HOLD   = 1 << WinAPI.COMSTAT.FlagsParamOffset.RlsHold,
        XOFF_HOLD   = 1 << WinAPI.COMSTAT.FlagsParamOffset.XoffHold,
        XOFF_SENT   = 1 << WinAPI.COMSTAT.FlagsParamOffset.XoffSent,
        EOF         = 1 << WinAPI.COMSTAT.FlagsParamOffset.Eof,
        TXIM        = 1 << WinAPI.COMSTAT.FlagsParamOffset.Txim,
    }

    internal class SerialPortController : IDisposable
    {
        private const int SIMPLEX_SEND_WAIT_TIME = 200;
        private const int SIMPLEX_RECV_HOLD_TIME = 200;

        public class CommStatusUpdatedEventArgs : EventArgs
        {
            public CommStatusUpdatedEventArgs(ErrorStatus error_status, CommStatus comm_status, CommStatus comm_status_old)
            {
                ErrorStatus = error_status;

                CommStatus = comm_status;
                CommStatusMask = comm_status ^ comm_status_old;
            }

            public ErrorStatus ErrorStatus   { get; }
            public CommStatus  CommStatus     { get; }
            public CommStatus  CommStatusMask { get; }
        }


        private IntPtr handle_ = WinAPI.INVALID_HANDLE_VALUE;
        private object handle_sync_ = new object();

        private uint error_stat_ = 0;

        private WinAPI.COMSTAT comstat_ = new WinAPI.COMSTAT();
        private WinAPI.COMSTAT comstat_temp_ = new WinAPI.COMSTAT();

        private bool      simplex_mode_ = false;
        private uint      loop_data_size_ = 0;


        public delegate void CommStatusUpdatedEvent(object sender, CommStatusUpdatedEventArgs e);


        public SerialPortController()
        {
        }

        public string             PortName { get; set; } = "COM1";

        public uint               BaudRate { get; set; } = 9600;
        public SerialPortParity   Parity   { get; set; } = SerialPortParity.None;
        public byte               DataBits { get; set; } = 8;
        public SerialPortStopBits StopBits { get; set; } = SerialPortStopBits.One;

        public fDtrControlType    fDtrControl { get; set; } = fDtrControlType.DTR_CONTROL_DISABLE;
        public fRtsControlType    fRtsControl { get; set; } = fRtsControlType.RTS_CONTROL_DISABLE;

        public bool fOutxCtsFlow { get; set; } = false;
        public bool fOutxDsrFlow { get; set; } = false;

        public bool fDsrSensitivity   { get; set; } = false;
        public bool fTXContinueOnXoff { get; set; } = false;
        public bool fOutX             { get; set; } = false;
        public bool fInX              { get; set; } = false;

        public ushort XonLim   { get; set; } = 2048;
        public ushort XoffLim  { get; set; } = 2048;
        public sbyte  XonChar  { get; set; } = 0x11;
        public sbyte  XoffChar { get; set; } = 0x13;

        public ushort InQueue  { get; set; } = 2048;
        public ushort OutQueue { get; set; } = 2048;

        public bool SimplexMode { get; set; } = false;

        public event CommStatusUpdatedEvent CommStatusUpdated;

        public void Dispose()
        {
            Close();
        }

        public bool Open()
        {
            Close();

#if ASYNC_MODE
            handle_ = NativeMethods.CreateFile(
                "\\\\.\\" + PortName,
                NativeMethods.GENERIC_READ | NativeMethods.GENERIC_WRITE,
                0,
                NativeMethods.Null,
                NativeMethods.OPEN_EXISTING,
                NativeMethods.FILE_FLAG_OVERLAPPED,
                NativeMethods.Null);
#else
            handle_ = WinAPI.CreateFile(
                "\\\\.\\" + PortName,
                WinAPI.GENERIC_READ | WinAPI.GENERIC_WRITE,
                0,
                WinAPI.Null,
                WinAPI.OPEN_EXISTING,
                0,
                WinAPI.Null);
#endif

            var error = WinAPI.GetLastError();

            return (handle_ != WinAPI.INVALID_HANDLE_VALUE);
        }

        public void Close()
        {
            if (handle_ == WinAPI.INVALID_HANDLE_VALUE)return;

            /* ポートクローズ */
            if (handle_ != WinAPI.INVALID_HANDLE_VALUE) {
                WinAPI.CloseHandle(handle_);
                handle_ = WinAPI.INVALID_HANDLE_VALUE;
            }

            error_stat_ = 0;
            comstat_ = new WinAPI.COMSTAT();
        }

        public bool Setup()
        {
            if (handle_ == WinAPI.INVALID_HANDLE_VALUE)return (false);

            /* === COMポート設定読み込み === */
            var dcb = new WinAPI.DCB();

            WinAPI.GetCommState(handle_, out dcb);

            /* Baudrate */
            dcb.BaudRate = BaudRate;

            /* Parity */
            switch (Parity) {
                case SerialPortParity.None:   dcb.Parity = WinAPI.NOPARITY;       break;
                case SerialPortParity.Odd:    dcb.Parity = WinAPI.ODDPARITY;      break;
                case SerialPortParity.Even:   dcb.Parity = WinAPI.EVENPARITY;     break;
                case SerialPortParity.Mark:   dcb.Parity = WinAPI.MASKPARITY;     break;
                case SerialPortParity.Space:  dcb.Parity = WinAPI.SPACEPARITY;    break;
                default:                      dcb.Parity = WinAPI.NOPARITY;       break;
            }

            /* BitSize */
            dcb.ByteSize = (byte)DataBits;

            /* StopBits */
            switch (StopBits) {
                case SerialPortStopBits.One:          dcb.StopBits = WinAPI.ONESTOPBIT;   break;
                case SerialPortStopBits.OnePointFive: dcb.StopBits = WinAPI.ONE5STOPBITS; break;
                case SerialPortStopBits.Two:          dcb.StopBits = WinAPI.TWOSTOPBIT;   break;
                default:                              dcb.StopBits = WinAPI.ONESTOPBIT;   break;
            }

            /* Simplex Mode */
            simplex_mode_ = SimplexMode;
            loop_data_size_ = 0;

            /* バイナリモード */
            dcb.fBinary = 1;

            dcb.fOutxCtsFlow = (fOutxCtsFlow) ? (1U) : (0U);
            dcb.fOutxDsrFlow = (fOutxDsrFlow) ? (1U) : (0U);
            dcb.fDsrSensitivity = (fDsrSensitivity) ? (1U) : (0U);
            dcb.fTXContinueOnXoff = (fTXContinueOnXoff) ? (1U) : (0U);
            dcb.fOutX = (fOutX) ? (1U) : (0U);
            dcb.fInX = (fInX) ? (1U) : (0U);

            switch (fDtrControl) {
                case fDtrControlType.DTR_CONTROL_DISABLE:   dcb.fDtrControl = WinAPI.DTR_CONTROL_DISABLE;   break;
                case fDtrControlType.DTR_CONTROL_ENABLE:    dcb.fDtrControl = WinAPI.DTR_CONTROL_ENABLE;    break;
                case fDtrControlType.DTR_CONTROL_HANDSHAKE: dcb.fDtrControl = WinAPI.DTR_CONTROL_HANDSHAKE; break;
            }

            switch (fRtsControl) {
                case fRtsControlType.RTS_CONTROL_DISABLE:   dcb.fRtsControl = WinAPI.RTS_CONTROL_DISABLE;   break;
                case fRtsControlType.RTS_CONTROL_ENABLE:    dcb.fRtsControl = WinAPI.RTS_CONTROL_ENABLE;    break;
                case fRtsControlType.RTS_CONTROL_HANDSHAKE: dcb.fRtsControl = WinAPI.RTS_CONTROL_HANDSHAKE; break;
                case fRtsControlType.RTS_CONTROL_TOGGLE:    dcb.fRtsControl = WinAPI.RTS_CONTROL_TOGGLE;    break;
            }

            dcb.XonLim = XonLim;
            dcb.XoffLim = XoffLim;
            dcb.XonChar = XonChar;
            dcb.XoffChar = XoffChar;

            /* === COMポート設定更新 === */
            if (!WinAPI.SetCommState(handle_, ref dcb)) {
                return (false);
            }

            if (!WinAPI.SetupComm(handle_, (uint)InQueue, (uint)OutQueue)) {
                return (false);
            }

            /* === タイムアウト設定 === */
            var timeout = new WinAPI.COMMTIMEOUTS();

            timeout.ReadIntervalTimeout = 0;
            timeout.ReadTotalTimeoutMultiplier = 0;
            timeout.ReadTotalTimeoutConstant = 10;
            timeout.WriteTotalTimeoutConstant = 100;

            WinAPI.SetCommTimeouts(handle_, ref timeout);

            /* === イベント設定 === */
            if (!WinAPI.SetCommMask(
                handle_,
//                  NativeMethods.EV_BREAK
//                | NativeMethods.EV_CTS
//                | NativeMethods.EV_DSR
//                | NativeMethods.EV_ERR
//                | NativeMethods.EV_RING
//                | NativeMethods.EV_RLSD
                  WinAPI.EV_RXCHAR
//                | NativeMethods.EV_RXFLAG
//                | NativeMethods.EV_TXEMPTY
                )
            ) {
                return (false);
            }

            error_stat_ = 0;
            comstat_ = new WinAPI.COMSTAT();

            return (true);
        }

        public void Purge()
        {
            if (handle_ == WinAPI.INVALID_HANDLE_VALUE)return;

            /* 処理中の操作を全てキャンセル */
            if (!WinAPI.PurgeComm(
                    handle_,
                      WinAPI.PURGE_RXABORT
                    | WinAPI.PURGE_RXCLEAR
                    | WinAPI.PURGE_TXABORT
                    | WinAPI.PURGE_TXCLEAR)
            ) {
//                throw new Win32Exception();
            }

            if (!WinAPI.EscapeCommFunction(handle_, WinAPI.CLRDTR)) {
//                throw new Win32Exception();
            }

            if (!WinAPI.SetCommMask(handle_, 0)) {
//                throw new Win32Exception();
            }
        }

        public void UpdateCommStatus()
        {
            lock (handle_sync_) {
                if (!WinAPI.ClearCommError(handle_, out error_stat_, out comstat_temp_)) {
                    error_stat_ = 0;
                    comstat_temp_.Flags = 0;
                    comstat_temp_.cbInQue = 0;
                    comstat_temp_.cbOutQue = 0;
                }

                if (   (error_stat_ != 0)
                    || (comstat_.Flags != comstat_temp_.Flags)
                ) {
                    CommStatusUpdated?.Invoke(
                        this,
                        new CommStatusUpdatedEventArgs(
                            (ErrorStatus)error_stat_,
                            (CommStatus)comstat_temp_.Flags,
                            (CommStatus)comstat_.Flags));
                }

                comstat_.Flags = comstat_temp_.Flags;
                comstat_.cbInQue = comstat_temp_.cbInQue;
                comstat_.cbOutQue = comstat_temp_.cbOutQue;
            }
        }

        public bool IsOpened
        {
            get { return (handle_ != WinAPI.INVALID_HANDLE_VALUE); }
        }

        public bool IsWriteBusy
        {
            get
            {
                UpdateCommStatus();

                return (comstat_.cbOutQue > 0);
            }
        }

        public ModemStatus GetModemStatus()
        {
            var status = (uint)0;

            if (!WinAPI.GetCommModemStatus(handle_, out status)) {
                status = 0;
            }

            return ((ModemStatus)status);
        }

        public bool GetDeviceDetachStatus()
        {
            if (!IsOpened)return (false);

            bool error_state = false;

            var handle_temp = WinAPI.CreateFile(
                "\\\\.\\" + PortName,
                WinAPI.GENERIC_READ | WinAPI.GENERIC_WRITE,
                0,
                WinAPI.Null,
                WinAPI.OPEN_EXISTING,
                0,
                WinAPI.Null);

            if (handle_temp == WinAPI.INVALID_HANDLE_VALUE) {
                switch ((WinErrorCode)Marshal.GetLastWin32Error()) {
                    case WinErrorCode.ERROR_FILE_NOT_FOUND:
                        error_state = true;
                        break;
                }
            } else {
                WinAPI.CloseHandle(handle_temp);
            }

            return (error_state);
        }

        public uint Write(byte[] data)
        {
            uint send_size_result = 0;
            uint send_size = Math.Min((uint)data.Length, OutQueue);

            if (!WinAPI.WriteFile(handle_, data, send_size, out send_size_result, WinAPI.Null)) {
                var error_code = Marshal.GetLastWin32Error();

                return (0);
            }

            if (simplex_mode_) {
                loop_data_size_ += send_size_result;
            }

            return (send_size_result);
        }

        public uint RecvDataSize
        {
            get
            {
                UpdateCommStatus();

                return (comstat_.cbInQue);
            }
        }

        public unsafe uint Recv(byte[] buffer)
        {
            var read_size = (uint)0;

            fixed (byte *buff = buffer)
            {
                if (!WinAPI.ReadFile(handle_, buff, (uint)buffer.Length, out read_size, WinAPI.Null)) {
                    return (0);
                }
            }

            if (read_size == 0)return (0);

            if (simplex_mode_) {
                if (loop_data_size_ > 0) {
                    var skip_size = Math.Min(loop_data_size_, read_size);

                    loop_data_size_ -= skip_size;

                    if (loop_data_size_ > 0)return (0);

                    Array.Copy(buffer, skip_size, buffer, 0, read_size - skip_size);

                    read_size -= skip_size;
                }
            }

            return (read_size);
        }
    }
}
