using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Native;

namespace Ratatoskr.Drivers.SerialPort
{
    internal sealed class SerialPortInfo : IComparable
    {
        private string devname_ = "";
        private string details_ = "";


        public SerialPortInfo(string devname)
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
            if (String.Compare(SerialPortManager.DEVICE_COM_TEXT, 0, devname_, 0, SerialPortManager.DEVICE_COM_TEXT.Length) != 0)return (0);

            return (uint.Parse(devname_.Substring(SerialPortManager.DEVICE_COM_TEXT.Length)));
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
            var target = obj as SerialPortInfo;

            if (target == null)return (1);

            return ((int)GetPortNo() - (int)target.GetPortNo());
        }
    }
}
