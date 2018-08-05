using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        CTS_ON      = (int)NativeMethods.MS_CTS_ON,
        DSR_ON      = (int)NativeMethods.MS_DSR_ON,
        RING_ON     = (int)NativeMethods.MS_RING_ON,
        RLSD_ON     = (int)NativeMethods.MS_RLSD_ON,
    }

    [Flags]
    internal enum ErrorStatus
    {
        BREAK    = (int)NativeMethods.CE_BREAK,
        DNS      = (int)NativeMethods.CE_DNS,
        FRAME    = (int)NativeMethods.CE_FRAME,
        IOE      = (int)NativeMethods.CE_IOE,
        MODE     = (int)NativeMethods.CE_MODE,
        OOP      = (int)NativeMethods.CE_OOP,
        OVERRUN  = (int)NativeMethods.CE_OVERRUN,
        PTO      = (int)NativeMethods.CE_PTO,
        RXOVER   = (int)NativeMethods.CE_RXOVER,
        RXPARITY = (int)NativeMethods.CE_RXPARITY,
        TXFULL   = (int)NativeMethods.CE_TXFULL,
    }

    [Flags]
    internal enum CommStatus
    {
        CTS_HOLD    = 1 << NativeMethods.COMSTAT.FlagsParamOffset.CtsHold,
        DSR_HOLD    = 1 << NativeMethods.COMSTAT.FlagsParamOffset.DsrHold,
        RLSD_HOLD   = 1 << NativeMethods.COMSTAT.FlagsParamOffset.RlsHold,
        XOFF_HOLD   = 1 << NativeMethods.COMSTAT.FlagsParamOffset.XoffHold,
        XOFF_SENT   = 1 << NativeMethods.COMSTAT.FlagsParamOffset.XoffSent,
        EOF         = 1 << NativeMethods.COMSTAT.FlagsParamOffset.Eof,
        TXIM        = 1 << NativeMethods.COMSTAT.FlagsParamOffset.Txim,
    }

    internal class SerialPortController : IDisposable
    {
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


        private IntPtr handle_ = NativeMethods.INVALID_HANDLE_VALUE;
        private object handle_sync_ = new object();

        private uint error_stat_ = 0;

        private NativeMethods.COMSTAT comstat_ = new NativeMethods.COMSTAT();
        private NativeMethods.COMSTAT comstat_temp_ = new NativeMethods.COMSTAT();


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
            handle_ = NativeMethods.CreateFile(
                "\\\\.\\" + PortName,
                NativeMethods.GENERIC_READ | NativeMethods.GENERIC_WRITE,
                0,
                NativeMethods.Null,
                NativeMethods.OPEN_EXISTING,
                0,
                NativeMethods.Null);
#endif

            var error = NativeMethods.GetLastError();

            return (handle_ != NativeMethods.INVALID_HANDLE_VALUE);
        }

        public void Close()
        {
            if (handle_ == NativeMethods.INVALID_HANDLE_VALUE)return;

            /* ポートクローズ */
            if (handle_ != NativeMethods.INVALID_HANDLE_VALUE) {
                NativeMethods.CloseHandle(handle_);
                handle_ = NativeMethods.INVALID_HANDLE_VALUE;
            }

            error_stat_ = 0;
            comstat_ = new NativeMethods.COMSTAT();
        }

        public bool Setup()
        {
            if (handle_ == NativeMethods.INVALID_HANDLE_VALUE)return (false);

            /* === COMポート設定読み込み === */
            var dcb = new NativeMethods.DCB();

            NativeMethods.GetCommState(handle_, out dcb);

            /* Baudrate */
            dcb.BaudRate = BaudRate;

            /* Parity */
            switch (Parity) {
                case SerialPortParity.None:   dcb.Parity = NativeMethods.NOPARITY;       break;
                case SerialPortParity.Odd:    dcb.Parity = NativeMethods.ODDPARITY;      break;
                case SerialPortParity.Even:   dcb.Parity = NativeMethods.EVENPARITY;     break;
                case SerialPortParity.Mark:   dcb.Parity = NativeMethods.MASKPARITY;     break;
                case SerialPortParity.Space:  dcb.Parity = NativeMethods.SPACEPARITY;    break;
                default:                      dcb.Parity = NativeMethods.NOPARITY;       break;
            }

            /* BitSize */
            dcb.ByteSize = (byte)DataBits;

            /* StopBits */
            switch (StopBits) {
                case SerialPortStopBits.One:          dcb.StopBits = NativeMethods.ONESTOPBIT;   break;
                case SerialPortStopBits.OnePointFive: dcb.StopBits = NativeMethods.ONE5STOPBITS; break;
                case SerialPortStopBits.Two:          dcb.StopBits = NativeMethods.TWOSTOPBIT;   break;
                default:                              dcb.StopBits = NativeMethods.ONESTOPBIT;   break;
            }

            /* バイナリモード */
            dcb.fBinary = 1;

            dcb.fOutxCtsFlow = (fOutxCtsFlow) ? (1U) : (0U);
            dcb.fOutxDsrFlow = (fOutxDsrFlow) ? (1U) : (0U);
            dcb.fDsrSensitivity = (fDsrSensitivity) ? (1U) : (0U);
            dcb.fTXContinueOnXoff = (fTXContinueOnXoff) ? (1U) : (0U);
            dcb.fOutX = (fOutX) ? (1U) : (0U);
            dcb.fInX = (fInX) ? (1U) : (0U);

            switch (fDtrControl) {
                case fDtrControlType.DTR_CONTROL_DISABLE:   dcb.fDtrControl = NativeMethods.DTR_CONTROL_DISABLE;   break;
                case fDtrControlType.DTR_CONTROL_ENABLE:    dcb.fDtrControl = NativeMethods.DTR_CONTROL_ENABLE;    break;
                case fDtrControlType.DTR_CONTROL_HANDSHAKE: dcb.fDtrControl = NativeMethods.DTR_CONTROL_HANDSHAKE; break;
            }

            switch (fRtsControl) {
                case fRtsControlType.RTS_CONTROL_DISABLE:   dcb.fRtsControl = NativeMethods.RTS_CONTROL_DISABLE;   break;
                case fRtsControlType.RTS_CONTROL_ENABLE:    dcb.fRtsControl = NativeMethods.RTS_CONTROL_ENABLE;    break;
                case fRtsControlType.RTS_CONTROL_HANDSHAKE: dcb.fRtsControl = NativeMethods.RTS_CONTROL_HANDSHAKE; break;
                case fRtsControlType.RTS_CONTROL_TOGGLE:    dcb.fRtsControl = NativeMethods.RTS_CONTROL_TOGGLE;    break;
            }

            dcb.XonLim = XonLim;
            dcb.XoffLim = XoffLim;
            dcb.XonChar = XonChar;
            dcb.XoffChar = XoffChar;

            /* === COMポート設定更新 === */
            if (!NativeMethods.SetCommState(handle_, ref dcb)) {
                return (false);
            }

            if (!NativeMethods.SetupComm(handle_, (uint)InQueue, (uint)OutQueue)) {
                return (false);
            }

            /* === タイムアウト設定 === */
            var timeout = new NativeMethods.COMMTIMEOUTS();

            timeout.ReadIntervalTimeout = 0;
            timeout.ReadTotalTimeoutMultiplier = 0;
            timeout.ReadTotalTimeoutConstant = 10;
            timeout.WriteTotalTimeoutConstant = 100;

            NativeMethods.SetCommTimeouts(handle_, ref timeout);

            /* === イベント設定 === */
            if (!NativeMethods.SetCommMask(
                handle_,
//                  NativeMethods.EV_BREAK
//                | NativeMethods.EV_CTS
//                | NativeMethods.EV_DSR
//                | NativeMethods.EV_ERR
//                | NativeMethods.EV_RING
//                | NativeMethods.EV_RLSD
                  NativeMethods.EV_RXCHAR
//                | NativeMethods.EV_RXFLAG
//                | NativeMethods.EV_TXEMPTY
                )
            ) {
                return (false);
            }

            error_stat_ = 0;
            comstat_ = new NativeMethods.COMSTAT();

            return (true);
        }

        public void Purge()
        {
            if (handle_ == NativeMethods.INVALID_HANDLE_VALUE)return;

            /* 処理中の操作を全てキャンセル */
            if (!NativeMethods.PurgeComm(
                    handle_,
                      NativeMethods.PURGE_RXABORT
                    | NativeMethods.PURGE_RXCLEAR
                    | NativeMethods.PURGE_TXABORT
                    | NativeMethods.PURGE_TXCLEAR)
            ) {
//                throw new Win32Exception();
            }

            if (!NativeMethods.EscapeCommFunction(handle_, NativeMethods.CLRDTR)) {
//                throw new Win32Exception();
            }

            if (!NativeMethods.SetCommMask(handle_, 0)) {
//                throw new Win32Exception();
            }
        }

        public void UpdateCommStatus()
        {
            lock (handle_sync_) {
                if (!NativeMethods.ClearCommError(handle_, out error_stat_, out comstat_temp_)) {
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
            get { return (handle_ != NativeMethods.INVALID_HANDLE_VALUE); }
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

            if (!NativeMethods.GetCommModemStatus(handle_, out status)) {
                status = 0;
            }

            return ((ModemStatus)status);
        }

        public bool GetDeviceDetachStatus()
        {
            if (!IsOpened)return (false);

            bool error_state = false;

            var handle_temp = NativeMethods.CreateFile(
                "\\\\.\\" + PortName,
                NativeMethods.GENERIC_READ | NativeMethods.GENERIC_WRITE,
                0,
                NativeMethods.Null,
                NativeMethods.OPEN_EXISTING,
                0,
                NativeMethods.Null);

            if (handle_temp == NativeMethods.INVALID_HANDLE_VALUE) {
                switch ((WinErrorCode)Marshal.GetLastWin32Error()) {
                    case WinErrorCode.ERROR_FILE_NOT_FOUND:
                        error_state = true;
                        break;
                }
            } else {
                NativeMethods.CloseHandle(handle_temp);
            }

            return (error_state);
        }

        public uint Write(byte[] data)
        {
            uint send_size_result = 0;
            uint send_size = Math.Min((uint)data.Length, OutQueue);

            if (!NativeMethods.WriteFile(handle_, data, send_size, out send_size_result, NativeMethods.Null)) {
                var error_code = Marshal.GetLastWin32Error();

                return (0);
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
                if (!NativeMethods.ReadFile(handle_, buff, (uint)buffer.Length, out read_size, NativeMethods.Null)) {
                    return (0);
                }
            }

            return (read_size);
        }
    }
}
