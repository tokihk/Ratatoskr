using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RtsCore;
using RtsCore.Framework.Native;

namespace Ratatoskr.Drivers.USBPcap
{
    internal class USBPcapCMD
    {
        private const int GLOBAL_BUFF_SIZE = 4096;
        private const int DEVICE_IO_BUFFER = 1024;
        private const int IOCTL_OUTPUT_BUFFER_SIZE = 1024;

        private const string DEVICE_KEY = "USBPcap";


        [StructLayout(LayoutKind.Sequential)]
        public struct USBPCAP_IOCTL_SIZE
        {
            public UInt32 size;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct USBPCAP_ADDRESS_FILTER
        {
            /* Individual device filter bit array. USB standard assigns device
             * numbers 1 to 127 (0 is reserved for initial configuration).
             *
             * If address 0 bit is set, then we will automatically capture from
             * newly connected devices.
             *
             * addresses[0] - 0 - 31
             * addresses[1] - 32 - 63
             * addresses[2] - 64 - 95
             * addresses[3] - 96 - 127
             */
            public fixed UInt32 addresses[4];

            /* Filter all devices */
            public byte         filterAll;

            public void SetAddress(uint device_no, bool watch)
            {
                var offset_byte = (int)device_no / 32;
                var offset_bit  = (int)device_no % 32;
                var value = ((watch) ? (1u) : (0u)) << offset_bit;

                fixed (uint *addresses_f = addresses) {
                    addresses_f[offset_byte] &= (~value);
                    addresses_f[offset_byte] |= value;
                }
            }
        }

        public static readonly uint IOCTL_USBPCAP_SETUP_BUFFER_1007 = WinAPI.CTL_CODE(
            WinAPI.FILE_DEVICE_UNKNOWN, 0x800, WinAPI.METHOD_BUFFERED, WinAPI.FILE_ANY_ACCESS);
        public static readonly uint IOCTL_USBPCAP_START_FILTERING_1007 = WinAPI.CTL_CODE(
            WinAPI.FILE_DEVICE_UNKNOWN, 0x801, WinAPI.METHOD_BUFFERED, WinAPI.FILE_WRITE_ACCESS);
        public static readonly uint IOCTL_USBPCAP_STOP_FILTERING_1007 = WinAPI.CTL_CODE(
            WinAPI.FILE_DEVICE_UNKNOWN, 0x802, WinAPI.METHOD_BUFFERED, WinAPI.FILE_WRITE_ACCESS);
        public static readonly uint IOCTL_USBPCAP_GET_HUB_SYMLINK_1007 = WinAPI.CTL_CODE(
            WinAPI.FILE_DEVICE_UNKNOWN, 0x803, WinAPI.METHOD_BUFFERED, WinAPI.FILE_ANY_ACCESS);
        public static readonly uint IOCTL_USBPCAP_SET_SNAPLEN_SIZE_1007 = WinAPI.CTL_CODE(
            WinAPI.FILE_DEVICE_UNKNOWN, 0x804, WinAPI.METHOD_BUFFERED, WinAPI.FILE_ANY_ACCESS);

        public static readonly uint IOCTL_USBPCAP_SETUP_BUFFER_1100 = WinAPI.CTL_CODE(
            WinAPI.FILE_DEVICE_UNKNOWN, 0x800, WinAPI.METHOD_BUFFERED, WinAPI.FILE_READ_ACCESS);
        public static readonly uint IOCTL_USBPCAP_START_FILTERING_1100 = WinAPI.CTL_CODE(
            WinAPI.FILE_DEVICE_UNKNOWN, 0x801, WinAPI.METHOD_BUFFERED, WinAPI.FILE_READ_ACCESS | WinAPI.FILE_WRITE_ACCESS);
        public static readonly uint IOCTL_USBPCAP_STOP_FILTERING_1100 = WinAPI.CTL_CODE(
            WinAPI.FILE_DEVICE_UNKNOWN, 0x802, WinAPI.METHOD_BUFFERED, WinAPI.FILE_READ_ACCESS | WinAPI.FILE_WRITE_ACCESS);
        public static readonly uint IOCTL_USBPCAP_GET_HUB_SYMLINK_1100 = WinAPI.CTL_CODE(
            WinAPI.FILE_DEVICE_UNKNOWN, 0x803, WinAPI.METHOD_BUFFERED, WinAPI.FILE_ANY_ACCESS);
        public static readonly uint IOCTL_USBPCAP_SET_SNAPLEN_SIZE_1100 = WinAPI.CTL_CODE(
            WinAPI.FILE_DEVICE_UNKNOWN, 0x804, WinAPI.METHOD_BUFFERED, WinAPI.FILE_READ_ACCESS);

        public enum EnumerationType
        {
            ENUMERATE_USBPCAPCMD,
            ENUMERATE_EXTCAP
        }

        private static unsafe bool DeviceIoControl_IOCTL_USBPCAP_GET_HUB_SYMLINK(
            IntPtr hDevice,
            IntPtr lpInBuffer,
            uint nInBufferSize,
            IntPtr lpOutBuffer,
            uint nOutBufferSize,
            out uint lpBytesReturned,
            IntPtr lpOverlapped)
        {
            if (WinAPI.DeviceIoControl(
                hDevice,
                IOCTL_USBPCAP_GET_HUB_SYMLINK_1100,
                WinAPI.Null,
                0,
                lpOutBuffer,
                nOutBufferSize,
                out lpBytesReturned,
                WinAPI.Null)
            ) {
                return (true);
            }

            if (WinAPI.DeviceIoControl(
                hDevice,
                IOCTL_USBPCAP_GET_HUB_SYMLINK_1007,
                WinAPI.Null,
                0,
                lpOutBuffer,
                nOutBufferSize,
                out lpBytesReturned,
                WinAPI.Null)
            ) {
                return (true);
            }

            return (false);
        }

#if false
        private static void EnumerateHub(string hub,
                                 NativeMethods.USB_NODE_CONNECTION_INFORMATION connection_info,
                                 ULONG level,
                                 EnumerationType enumType)
        {
            PUSB_NODE_INFORMATION   hubInfo;
            HANDLE                  hHubDevice;
            PTSTR                   deviceName;
            size_t                  deviceNameSize;
            BOOL                    success;
            ULONG                   nBytes;

            // Initialize locals to not allocated state so the error cleanup routine
            // only tries to cleanup things that were successfully allocated.
            hubInfo     = NULL;
            hHubDevice  = INVALID_HANDLE_VALUE;

            // Allocate some space for a USB_NODE_INFORMATION structure for this Hub
            hubInfo = (PUSB_NODE_INFORMATION)GlobalAlloc(GPTR, sizeof(USB_NODE_INFORMATION));

            if (hubInfo == NULL)
            {
                OOPS();
                goto EnumerateHubError;
            }

            // Allocate a temp buffer for the full hub device name.
            deviceNameSize = _tcslen(hub) + _tcslen(_T("\\\\.\\")) + 1;
            deviceName = (PTSTR)GlobalAlloc(GPTR, deviceNameSize * sizeof(TCHAR));

            if (deviceName == NULL)
            {
                OOPS();
                goto EnumerateHubError;
            }

            if (_tcsncmp(_T("\\\?\?\\"), hub, 4) == 0)
            {
                /* Replace the \??\ with \\.\ */
                _tcscpy_s(deviceName, deviceNameSize, _T("\\\\.\\"));
                _tcscat_s(deviceName, deviceNameSize, &hub[4]);
            }
            else if (hub[0] == _T('\\'))
            {
                _tcscpy_s(deviceName, deviceNameSize, hub);
            }
            else
            {
                _tcscpy_s(deviceName, deviceNameSize, _T("\\\\.\\"));
                _tcscat_s(deviceName, deviceNameSize, hub);
            }

            // Try to hub the open device
            hHubDevice = CreateFile(deviceName,
                                    GENERIC_WRITE,
                                    FILE_SHARE_WRITE,
                                    NULL,
                                    OPEN_EXISTING,
                                    0,
                                    NULL);

            GlobalFree(deviceName);

            if (hHubDevice == INVALID_HANDLE_VALUE)
            {
                fprintf(stderr, "unable to open %s\n", hub);
                OOPS();
                goto EnumerateHubError;
            }

            // Now query USBHUB for the USB_NODE_INFORMATION structure for this hub.
            // This will tell us the number of downstream ports to enumerate, among
            // other things.
            success = DeviceIoControl(hHubDevice,
                                      IOCTL_USB_GET_NODE_INFORMATION,
                                      hubInfo,
                                      sizeof(USB_NODE_INFORMATION),
                                      hubInfo,
                                      sizeof(USB_NODE_INFORMATION),
                                      &nBytes,
                                      NULL);

            if (!success)
            {
                OOPS();
                goto EnumerateHubError;
            }

            // Now recursively enumrate the ports of this hub.
            EnumerateHubPorts(hHubDevice,
                              hubInfo->u.HubInformation.HubDescriptor.bNumberOfPorts,
                              level,
                              (connection_info == NULL) ? 0 : connection_info->DeviceAddress,
                              enumType);

        EnumerateHubError:
            // Clean up any stuff that got allocated

            if (hHubDevice != INVALID_HANDLE_VALUE)
            {
                CloseHandle(hHubDevice);
                hHubDevice = INVALID_HANDLE_VALUE;
            }

            if (hubInfo)
            {
                GlobalFree(hubInfo);
            }
        }
#endif

        public static void enumerate_attached_devices(string filter, EnumerationType enumType)
        {
            IntPtr  filter_handle;
            var     outBuf = Marshal.AllocHGlobal(IOCTL_OUTPUT_BUFFER_SIZE * 2);
            UInt32  outBufSize = IOCTL_OUTPUT_BUFFER_SIZE;
            UInt32  bytes_ret;

            filter_handle = WinAPI.CreateFileA(
                                        filter,
                                        0,
                                        0,
                                        WinAPI.Null,
                                        WinAPI.OPEN_EXISTING,
                                        0,
                                        WinAPI.Null);

            if (filter_handle == WinAPI.INVALID_HANDLE_VALUE)
            {
                Kernel.DebugMessage(string.Format("Couldn't open device - {0}", WinAPI.GetLastError()));
                return;
            }

            if (DeviceIoControl_IOCTL_USBPCAP_GET_HUB_SYMLINK(filter_handle,
                                WinAPI.Null,
                                0,
                                outBuf,
                                outBufSize,
                                out bytes_ret,
                                WinAPI.Null)
            ) {
                if (bytes_ret > 0) {
                    var str = Marshal.PtrToStringAuto(outBuf, (int)bytes_ret);

                    if (enumType == EnumerationType.ENUMERATE_USBPCAPCMD) {
                        Kernel.DebugMessage(str);
                    }

//                    EnumerateHub(str, NULL, 2, enumType);
                }
            }

            Marshal.FreeHGlobal(outBuf);

            if (filter_handle != WinAPI.INVALID_HANDLE_VALUE)
            {
                WinAPI.CloseHandle(filter_handle);
            }
        }
    }
}
