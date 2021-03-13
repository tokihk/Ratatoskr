using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Ratatoskr.Device.AudioDevice
{
    internal sealed class MicDeviceInfo
    {
        public static IEnumerable<WaveInCapabilities> GetDeviceList()
        {
            var dev_list = new List<WaveInCapabilities>();
            var dev_count = WaveIn.DeviceCount;

            for (var dev_no = 0; dev_no < dev_count; dev_no++) {
                dev_list.Add(WaveIn.GetCapabilities(dev_no));
            }

            return (dev_list);
        }

        public static WaveInCapabilities FindDevice(Guid guid)
        {
            try {
                return (GetDeviceList().First(dev => dev.NameGuid == guid));
            } catch {
                return (new WaveInCapabilities());
            }
        }

        public static int GetDeviceNo(Guid guid)
        {
            foreach (var dev in GetDeviceList().Select((v, i) => new {v, i})) {
                if (dev.v.NameGuid == guid)return (dev.i);
            }

            return (-1);
        }

        public static int GetDeviceNo(string guid)
        {
            try {
                return (GetDeviceNo(Guid.Parse(guid)));
            } catch {
                return (-1);
            }
        }

        public static string GetDeviceText(Guid guid)
        {
            return (FindDevice(guid).ProductName);
        }

        public static string GetDeviceText(string guid)
        {
            try {
                return (GetDeviceText(Guid.Parse(guid)));
            } catch {
                return ("Unknown");
            }
        }
    }
}
