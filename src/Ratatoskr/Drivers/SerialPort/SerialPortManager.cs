using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Runtime.InteropServices;
using Ratatoskr.Native;

namespace Ratatoskr.Drivers.SerialPort
{
    internal static class SerialPortManager
    {
        public const string DEVICE_COM_TEXT = "COM";


        public static IEnumerable<SerialPortInfo> GetSerialPortList()
        {
            var ports = new List<SerialPortInfo>();

            SetupPortListFromRegistory(ports);
            SetupPortListFromWMI(ports);
            SetupPortListFromDeviceList(ports);

            ports.Sort();

            return (ports);
        }

        private static SerialPortInfo LoadSerialPortInfo(List<SerialPortInfo> ports, string devname)
        {
            if (ports == null)return (null);
            if (devname == null)return (null);
            if (devname.Length == 0)return (null);

            var port = ports.Find(obj => obj.DeviceName == devname);

            if (port == null) {
                port = new SerialPortInfo(devname);
                ports.Add(port);
            }

            return (port);
        }

        private static void SetupPortListFromRegistory(List<SerialPortInfo> ports)
        {
            if (ports == null)return;

            var buffer = new byte[0xFFFF];
            var result = WinAPI.QueryDosDevice(null, buffer, (UInt32)buffer.Length);

            if (result == 0)return;

            var devnames = Encoding.UTF8.GetString(buffer, 0, (int)result);
            var devlist = devnames.ToString().Split(new char[] { '\0' });

            foreach (var dev in devlist) {
                if (String.Compare(DEVICE_COM_TEXT, 0, dev, 0, DEVICE_COM_TEXT.Length) == 0) {
                    LoadSerialPortInfo(ports, dev);
                }
            }
        }

        private static void SetupPortListFromWMI(List<SerialPortInfo> ports)
        {
            if (ports == null)return;

            var manager = new ManagementClass("Win32_SerialPort");

            foreach (var obj in manager.GetInstances()) {
                var port = LoadSerialPortInfo(ports, obj.GetPropertyValue("DeviceID") as string);

                if (port != null) {
                    port.Details = obj.GetPropertyValue("Caption") as string;
                }
            }
        }

        private static void SetupPortListFromDeviceList(List<SerialPortInfo> ports)
        {
            var guids = new Guid[1];
            var size = (UInt32)0;

            if (WinAPI.SetupDiClassGuidsFromName("PORTS", ref guids[0], (UInt32)guids.Length, out size)) {
                var devinfo = WinAPI.SetupDiGetClassDevs(ref guids[0], null, WinAPI.Null, WinAPI.DIGCF_PRESENT | WinAPI.DIGCF_PROFILE);

                if (devinfo != WinAPI.Null) {
                    var devdata = new WinAPI.SP_DEVINFO_DATA();
                    var frndname = new byte[WinAPI.MAX_PATH];
                    var portname = new byte[WinAPI.MAX_PATH];
                    var frndname_size = (UInt32)0;
                    var portname_size = (UInt32)0;
                    var type = (UInt32)0;
                    var index = (UInt32)0;

                    devdata.cbSize = (uint)Marshal.SizeOf(devdata.GetType());

                    while (WinAPI.SetupDiEnumDeviceInfo(
                                devinfo,
                                index++,
                                out devdata)
                    ){
                        frndname[0] = (byte)char.MinValue;
                        portname[0] = (byte)char.MinValue;

                        WinAPI.SetupDiGetDeviceRegistryProperty(
                            devinfo,
                            ref devdata,
                            WinAPI.SPDRP_FRIENDLYNAME,
                            out type,
                            frndname,
                            (UInt32)frndname.Length,
                            out frndname_size);

                        var hkey = WinAPI.SetupDiOpenDevRegKey(
                                        devinfo,
                                        ref devdata,
                                        WinAPI.DICS_FLAG_GLOBAL,
                                        0,
                                        WinAPI.DIREG_DEV,
                                        WinAPI.KEY_READ);

                        if (hkey != WinAPI.Null) {
                            portname_size = (UInt32)portname.Length;

                            WinAPI.RegQueryValueEx(hkey, "PortName", WinAPI.Null, out type, portname, ref portname_size);
                            WinAPI.RegCloseKey(hkey);
                        }

                        var devname = Encoding.Unicode.GetString(portname, 0, (int)portname_size);
                        var details = Encoding.GetEncoding(932).GetString(frndname, 0, (int)frndname_size);

                        devname = devname.Remove(devname.Length - 1);
                        details = details.Remove(details.Length - 1);

                        if (String.Compare(DEVICE_COM_TEXT, 0, devname, 0, DEVICE_COM_TEXT.Length) == 0) {
                            var port = LoadSerialPortInfo(ports, devname);

                            if ((port != null) && (port.Details.Length == 0)) {
                                port.Details = details;
                            }
                        }
                    }
                }
                WinAPI.SetupDiDestroyDeviceInfoList(devinfo);
            }
        }
    }
}
