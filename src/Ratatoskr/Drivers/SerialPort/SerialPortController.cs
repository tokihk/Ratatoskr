using System;
using System.Collections.Generic;
using System.Linq;
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

    internal class SerialPortController : IDisposable
    {
        private IntPtr handle_ = NativeMethods.INVALID_HANDLE_VALUE;


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

            return (true);
        }

        public void Purge()
        {
            if (handle_ == NativeMethods.INVALID_HANDLE_VALUE)return;

            /* 処理中の操作を全てキャンセル */
            NativeMethods.PurgeComm(
                handle_,
                  NativeMethods.PURGE_RXABORT
                | NativeMethods.PURGE_RXCLEAR
                | NativeMethods.PURGE_TXABORT
                | NativeMethods.PURGE_TXCLEAR);

            NativeMethods.EscapeCommFunction(handle_, NativeMethods.CLRDTR);
            NativeMethods.SetCommMask(handle_, 0);
        }

        public bool IsWriteBusy
        {
            get
            {
                var comm_error = (uint)0;
                var comm_stat = new NativeMethods.COMSTAT();

                if (!NativeMethods.ClearCommError(handle_, out comm_error, out comm_stat)) {
                    return (false);
                }

                return (comm_stat.cbOutQue > 0);
            }
        }

        public uint Write(byte[] data)
        {
            uint send_size = 0;

            if (!NativeMethods.WriteFile(handle_, data, (uint)data.Length, out send_size, NativeMethods.Null)) {
                return (0);
            }

            return (send_size);
        }

        public uint RecvDataSize
        {
            get
            {
                var comm_error = (uint)0;
                var comm_stat = new NativeMethods.COMSTAT();

                if (!NativeMethods.ClearCommError(handle_, out comm_error, out comm_stat)) {
                    return (0);
                }

                /* 受信していなければスキップ */
                return (comm_stat.cbInQue);
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
