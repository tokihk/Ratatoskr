﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using Ratatoskr.Native;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;

namespace Ratatoskr.Devices.UsbMonitor
{
    internal sealed class UsbPcapManager
    {
        private const int GLOBAL_BUFF_SIZE = 4096;
        private const int DEVICE_IO_BUFFER = 1024;

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
            public byte   filterAll;
        }

        public static readonly uint IOCTL_USBPCAP_SETUP_BUFFER_1007 = NativeMethods.CTL_CODE(
            NativeMethods.FILE_DEVICE_UNKNOWN, 0x800, NativeMethods.METHOD_BUFFERED, NativeMethods.FILE_ANY_ACCESS);
        public static readonly uint IOCTL_USBPCAP_START_FILTERING_1007 = NativeMethods.CTL_CODE(
            NativeMethods.FILE_DEVICE_UNKNOWN, 0x801, NativeMethods.METHOD_BUFFERED, NativeMethods.FILE_WRITE_ACCESS);
        public static readonly uint IOCTL_USBPCAP_STOP_FILTERING_1007 = NativeMethods.CTL_CODE(
            NativeMethods.FILE_DEVICE_UNKNOWN, 0x802, NativeMethods.METHOD_BUFFERED, NativeMethods.FILE_WRITE_ACCESS);
        public static readonly uint IOCTL_USBPCAP_GET_HUB_SYMLINK_1007 = NativeMethods.CTL_CODE(
            NativeMethods.FILE_DEVICE_UNKNOWN, 0x803, NativeMethods.METHOD_BUFFERED, NativeMethods.FILE_ANY_ACCESS);
        public static readonly uint IOCTL_USBPCAP_SET_SNAPLEN_SIZE_1007 = NativeMethods.CTL_CODE(
            NativeMethods.FILE_DEVICE_UNKNOWN, 0x804, NativeMethods.METHOD_BUFFERED, NativeMethods.FILE_ANY_ACCESS);

        public static readonly uint IOCTL_USBPCAP_SETUP_BUFFER_1100 = NativeMethods.CTL_CODE(
            NativeMethods.FILE_DEVICE_UNKNOWN, 0x800, NativeMethods.METHOD_BUFFERED, NativeMethods.FILE_READ_ACCESS);
        public static readonly uint IOCTL_USBPCAP_START_FILTERING_1100 = NativeMethods.CTL_CODE(
            NativeMethods.FILE_DEVICE_UNKNOWN, 0x801, NativeMethods.METHOD_BUFFERED, NativeMethods.FILE_READ_ACCESS | NativeMethods.FILE_WRITE_ACCESS);
        public static readonly uint IOCTL_USBPCAP_STOP_FILTERING_1100 = NativeMethods.CTL_CODE(
            NativeMethods.FILE_DEVICE_UNKNOWN, 0x802, NativeMethods.METHOD_BUFFERED, NativeMethods.FILE_READ_ACCESS | NativeMethods.FILE_WRITE_ACCESS);
        public static readonly uint IOCTL_USBPCAP_GET_HUB_SYMLINK_1100 = NativeMethods.CTL_CODE(
            NativeMethods.FILE_DEVICE_UNKNOWN, 0x803, NativeMethods.METHOD_BUFFERED, NativeMethods.FILE_ANY_ACCESS);
        public static readonly uint IOCTL_USBPCAP_SET_SNAPLEN_SIZE_1100 = NativeMethods.CTL_CODE(
            NativeMethods.FILE_DEVICE_UNKNOWN, 0x804, NativeMethods.METHOD_BUFFERED, NativeMethods.FILE_READ_ACCESS);
        

        public static IEnumerable<UsbPcapDeviceObject> GetDeviceList()
        {
            var devices = new List<UsbPcapDeviceObject>();
            var handle = (SafeFileHandle)null;
            var attr = new NativeMethods.OBJECT_ATTRIBUTES("\\Device", 0);
            var result = 0;

            result = NativeMethods.NtOpenDirectoryObject(out handle, NativeMethods.DIRECTORY_QUERY, ref attr);
            if (result == 0) {
                var buff = Marshal.AllocHGlobal(GLOBAL_BUFF_SIZE);
                var read_index = (uint)0;
                var read_size = (uint)0;

                result = NativeMethods.NtQueryDirectoryObject(handle, buff, GLOBAL_BUFF_SIZE, true, true, ref read_index, out read_size);
                if (result == 0) {

                    while (NativeMethods.NtQueryDirectoryObject(handle, buff, GLOBAL_BUFF_SIZE, true, false, ref read_index, out read_size) == 0) {
                        var odi = (NativeMethods.OBJECT_DIRECTORY_INFORMATION)Marshal.PtrToStructure(buff, typeof(NativeMethods.OBJECT_DIRECTORY_INFORMATION));
                        var name = odi.Name.ToString();

                        if (name.Substring(0, Math.Min(name.Length, DEVICE_KEY.Length)) == DEVICE_KEY) {
                            var devname = "\\\\.\\" + odi.Name.ToString();
                            var hid = GetCaptureHardwareID(devname);
                            var text = (string)null;

                            /* デバイス未使用 */
                            if (hid != null) {
                                text = string.Format("{0}: {1}", odi.Name, hid);
                            } else {
                                text = string.Format("{0}: Opened device!", odi.Name);
                            }

                            devices.Add(new UsbPcapDeviceObject(devname, text));
                        }
                    }
                }

                Marshal.FreeHGlobal(buff);
            }

            return (devices);
        }

        public static string GetCaptureHardwareID_1007(string devname)
        {
            var hid = (string)null;
            var handle = NativeMethods.CreateFile(devname, 0, 0, IntPtr.Zero, NativeMethods.OPEN_EXISTING, 0, IntPtr.Zero);

            if (handle != NativeMethods.INVALID_HANDLE_VALUE) {
                var read_buff = Marshal.AllocHGlobal(DEVICE_IO_BUFFER);
                var read_size = (uint)0;

                if (NativeMethods.DeviceIoControl(
                            handle,
                            UsbPcapManager.IOCTL_USBPCAP_GET_HUB_SYMLINK_1007,
                            IntPtr.Zero,
                            0,
                            read_buff,
                            DEVICE_IO_BUFFER,
                            out read_size,
                            IntPtr.Zero)
                ) {
                    if (read_size > 0) {
                        hid = Marshal.PtrToStringUni(read_buff);
                    }
                }

                Marshal.FreeHGlobal(read_buff);

                NativeMethods.CloseHandle(handle);
            }

            return (hid);
        }

        public static string GetCaptureHardwareID_1100(string devname)
        {
            var hid = (string)null;
            var handle = NativeMethods.CreateFile(devname, 0, 0, IntPtr.Zero, NativeMethods.OPEN_EXISTING, 0, IntPtr.Zero);

            if (handle != NativeMethods.INVALID_HANDLE_VALUE) {
                var read_buff = Marshal.AllocHGlobal(DEVICE_IO_BUFFER);
                var read_size = (uint)0;

                if (NativeMethods.DeviceIoControl(
                            handle,
                            UsbPcapManager.IOCTL_USBPCAP_GET_HUB_SYMLINK_1100,
                            IntPtr.Zero,
                            0,
                            read_buff,
                            DEVICE_IO_BUFFER,
                            out read_size,
                            IntPtr.Zero)
                ) {
                    if (read_size > 0) {
                        hid = Marshal.PtrToStringUni(read_buff);
                    }
                }

                Marshal.FreeHGlobal(read_buff);

                NativeMethods.CloseHandle(handle);
            }

            return (hid);
        }

        public static string GetCaptureHardwareID(string devname)
        {
            var hid = (string)null;

            if (hid == null) {
                hid = GetCaptureHardwareID_1100(devname);
            }

            if (hid == null) {
                hid = GetCaptureHardwareID_1007(devname);
            }

            return (hid);
        }

        public static IntPtr OpenDevice(string devname)
        {
            var handle = NativeMethods.CreateFile(
                                    devname,
                                    NativeMethods.GENERIC_READ | NativeMethods.GENERIC_WRITE,
                                    0,
                                    IntPtr.Zero,
                                    NativeMethods.OPEN_EXISTING,
                                    NativeMethods.FILE_FLAG_OVERLAPPED,
                                    IntPtr.Zero);

            if (handle != NativeMethods.INVALID_HANDLE_VALUE) {
                SetDeviceSnapLength(handle, 65535);
                SetDeviceBufferSize(handle, 4096);
                StartDeviceCapture(handle);
            }

            return (handle);
        }

        public static void CloseDevice(IntPtr handle)
        {
            StopDeviceCapture(handle);
            NativeMethods.CloseHandle(handle);
        }

        public static bool SetDeviceSnapLength(IntPtr handle, uint size)
        {
            var set_ok = false;
            var ioctl_size = new USBPCAP_IOCTL_SIZE();

            ioctl_size.size = size;

            if (!set_ok) {
                set_ok = NativeMethods.DeviceIoControl(handle, IOCTL_USBPCAP_SET_SNAPLEN_SIZE_1100, ioctl_size, null, true);
            }

            if (!set_ok) {
                set_ok = NativeMethods.DeviceIoControl(handle, IOCTL_USBPCAP_SET_SNAPLEN_SIZE_1007, ioctl_size, null, true);
            }

            return (set_ok);
        }

        public static bool SetDeviceBufferSize(IntPtr handle, uint size)
        {
            var set_ok = false;
            var ioctl_size = new USBPCAP_IOCTL_SIZE();

            ioctl_size.size = size;

            if (!set_ok) {
                set_ok = NativeMethods.DeviceIoControl(handle, IOCTL_USBPCAP_SETUP_BUFFER_1100, ioctl_size, null, true);
            }

            if (!set_ok) {
                set_ok = NativeMethods.DeviceIoControl(handle, IOCTL_USBPCAP_SETUP_BUFFER_1007, ioctl_size, null, true);
            }

            return (set_ok);
        }

        public static bool StartDeviceCapture(IntPtr handle)
        {
            var start_ok = false;
            var ioctl_data = new USBPCAP_ADDRESS_FILTER();

            ioctl_data.filterAll = 1;

            if (!start_ok) {
                start_ok = NativeMethods.DeviceIoControl(handle, IOCTL_USBPCAP_START_FILTERING_1100, ioctl_data, null, true);
            }

            if (!start_ok) {
                start_ok = NativeMethods.DeviceIoControl(handle, IOCTL_USBPCAP_START_FILTERING_1007, ioctl_data, null, true);
            }

            return (start_ok);
        }

        public static void StopDeviceCapture(IntPtr handle)
        {
            var stop_ok = false;

            if (!stop_ok) {
                stop_ok = NativeMethods.DeviceIoControl(handle, IOCTL_USBPCAP_STOP_FILTERING_1100, null, null, true);
            }

            if (!stop_ok) {
                stop_ok = NativeMethods.DeviceIoControl(handle, IOCTL_USBPCAP_STOP_FILTERING_1007, null, null, true);
            }
        }
    }
}