using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Native;

namespace Ratatoskr.Devices.SerialPort
{
    internal sealed class ComPortInfo : IComparable
    {
        private const string STR_COM = "COM";


        public static IEnumerable<ComPortInfo> GetSerialPortList()
        {
            var ports = new List<ComPortInfo>();

            SetupPortListFromRegistory(ports);
            SetupPortListFromWMI(ports);
            SetupPortListFromDeviceList(ports);

            ports.Sort();

            return (ports);
        }

        private static ComPortInfo LoadSerialPortInfo(List<ComPortInfo> ports, string devname)
        {
            if (ports == null)return (null);
            if (devname == null)return (null);
            if (devname.Length == 0)return (null);

            var port = ports.Find(obj => obj.DeviceName == devname);

            if (port == null) {
                port = new ComPortInfo(devname);
                ports.Add(port);
            }

            return (port);
        }

        private static void SetupPortListFromRegistory(List<ComPortInfo> ports)
        {
            if (ports == null)return;

            var buffer = new byte[0xFFFF];
            var result = NativeMethods.QueryDosDevice(null, buffer, (UInt32)buffer.Length);

            if (result == 0)return;

            var devnames = Encoding.UTF8.GetString(buffer, 0, (int)result);
            var devlist = devnames.ToString().Split(new char[] { '\0' });

            foreach (var dev in devlist) {
                if (String.Compare(STR_COM, 0, dev, 0, STR_COM.Length) == 0) {
                    LoadSerialPortInfo(ports, dev);
                }
            }
        }

        private static void SetupPortListFromWMI(List<ComPortInfo> ports)
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

        private static void SetupPortListFromDeviceList(List<ComPortInfo> ports)
        {
            var guids = new Guid[1];
            var size = (UInt32)0;

            if (NativeMethods.SetupDiClassGuidsFromName("PORTS", ref guids[0], (UInt32)guids.Length, out size)) {
                var devinfo = NativeMethods.SetupDiGetClassDevs(ref guids[0], null, NativeMethods.Null, NativeMethods.DIGCF_PRESENT | NativeMethods.DIGCF_PROFILE);

                if (devinfo != NativeMethods.Null) {
                    var devdata = new NativeMethods.SP_DEVINFO_DATA();
                    var frndname = new byte[NativeMethods.MAX_PATH];
                    var portname = new byte[NativeMethods.MAX_PATH];
                    var frndname_size = (UInt32)0;
                    var portname_size = (UInt32)0;
                    var type = (UInt32)0;
                    var index = (UInt32)0;

                    devdata.cbSize = (uint)Marshal.SizeOf(devdata.GetType());

                    while (NativeMethods.SetupDiEnumDeviceInfo(
                                devinfo,
                                index++,
                                out devdata)
                    ){
                        frndname[0] = (byte)char.MinValue;
                        portname[0] = (byte)char.MinValue;

                        NativeMethods.SetupDiGetDeviceRegistryProperty(
                            devinfo,
                            ref devdata,
                            NativeMethods.SPDRP_FRIENDLYNAME,
                            out type,
                            frndname,
                            (UInt32)frndname.Length,
                            out frndname_size);

                        var hkey = NativeMethods.SetupDiOpenDevRegKey(
                                        devinfo,
                                        ref devdata,
                                        NativeMethods.DICS_FLAG_GLOBAL,
                                        0,
                                        NativeMethods.DIREG_DEV,
                                        NativeMethods.KEY_READ);

                        if (hkey != NativeMethods.Null) {
                            portname_size = (UInt32)portname.Length;

                            NativeMethods.RegQueryValueEx(hkey, "PortName", NativeMethods.Null, out type, portname, ref portname_size);
                            NativeMethods.RegCloseKey(hkey);
                        }

                        var devname = Encoding.Unicode.GetString(portname, 0, (int)portname_size);
                        var details = Encoding.GetEncoding(932).GetString(frndname, 0, (int)frndname_size);

                        devname = devname.Remove(devname.Length - 1);
                        details = details.Remove(details.Length - 1);

                        if (String.Compare(STR_COM, 0, devname, 0, STR_COM.Length) == 0) {
                            var port = LoadSerialPortInfo(ports, devname);

                            if ((port != null) && (port.Details.Length == 0)) {
                                port.Details = details;
                            }
                        }
                    }
                }
                NativeMethods.SetupDiDestroyDeviceInfoList(devinfo);
            }
        }


        private string devname_ = "";
        private string details_ = "";


        public ComPortInfo(string devname)
        {
            devname_ = devname;
        }

        public string DeviceName
        {
            get { return (devname_); }
        }

        public string Details
        {
            get { return (details_); }
            set { details_ = value;  }
        }

        public uint GetPortNo()
        {
            if (devname_ == null)return (0);
            if (String.Compare(STR_COM, 0, devname_, 0, STR_COM.Length) != 0)return (0);

            return (uint.Parse(devname_.Substring(STR_COM.Length)));
        }

        public override string ToString()
        {
            return (String.Format("{0:G}: {1:G}", DeviceName, Details));
        }

        public override bool Equals(object obj)
        {
            if (obj is string) {
                return ((obj as string) == DeviceName);
            } else {
                return (base.Equals(obj));
            }
        }

        public override int GetHashCode()
        {
            return (base.GetHashCode());
        }

        public int CompareTo(object obj)
        {
            var target = obj as ComPortInfo;

            if (target == null)return (1);

            return ((int)GetPortNo() - (int)target.GetPortNo());
        }
    }
}
