using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Gate;
using RtsCore.Framework.Device;

namespace Ratatoskr.Devices
{
    internal static class DeviceManager
    {
        private static DeviceManagementClass devm_;
        private static readonly object devm_sync_ = new object();


		public static void Initialize()
		{
			/* デバイスマネージャー初期化 */
			devm_ = new DeviceManagementClass();

			/* 基本デバイスインストール */
			InstallBasicDevice();
		}

		public static void Startup()
        {
			devm_.PacketManager = GatePacketManager.BasePacketManager;
        }

        public static void Shutdown()
        {
            devm_.RemoveAllInstance();
        }

        public static void Poll()
        {
            devm_.Poll();
        }

        private static void InstallBasicDevice()
        {
            AddDevice(new Devices.Null.DeviceClassImpl());
            AddDevice(new Devices.SerialPort.DeviceClassImpl());
            AddDevice(new Devices.TcpServer.DeviceClassImpl());
            AddDevice(new Devices.TcpClient.DeviceClassImpl());
            AddDevice(new Devices.UdpClient.DeviceClassImpl());
#if DEBUG
            AddDevice(new Devices.UsbComm.DeviceClassImpl());
#endif
            AddDevice(new Devices.AudioDevice.DeviceClassImpl());
            AddDevice(new Devices.AudioFile.DeviceClassImpl());
        }

        public static DeviceClass[] GetDeviceList()
        {
            return (devm_.GetClasses().ToArray());
        }

        public static DeviceClass FindDeviceClass(Guid class_id)
        {
            return (devm_.FindClass(class_id));
        }

        public static void AddDevice(DeviceClass devc)
        {
            if (devc == null)return;

            lock (devm_) {
                devm_.AddClass(devc);
            }
        }

        public static DeviceInstance CreateDeviceObject(DeviceConfig devconf, Guid class_id, DeviceProperty devp)
        {
            return (devm_.CreateInstance(devconf, class_id, devp));
        }

        public static DeviceProperty CreateDeviceProperty(Guid class_id)
        {
            return (devm_.CreateProperty(class_id));
        }
    }
}
