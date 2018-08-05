using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Devices;

namespace Ratatoskr.Gate
{
    internal static class GateManager
    {
        private static DeviceManager devm_;

        private static List<GateObject> gates_;

        
        public static void Startup()
        {
            /* デバイスマネージャー初期化 */
            devm_ = new DeviceManager(GatePacketManager.BasePacketManager);

            /* ゲートリスト初期化 */
            gates_ = new List<GateObject>();

            /* 基本デバイスインストール */
            InstallDevice();
        }

        public static void Shutdown()
        {
            devm_.RemoveAllInstance();
        }

        public static void Poll()
        {
            devm_.Poll();
        }

        private static void InstallDevice()
        {
            devm_.AddClass(new Devices.Null.DeviceClassImpl());
            devm_.AddClass(new Devices.SerialPort.DeviceClassImpl());
            devm_.AddClass(new Devices.TcpServer.DeviceClassImpl());
            devm_.AddClass(new Devices.TcpClient.DeviceClassImpl());
            devm_.AddClass(new Devices.UdpClient.DeviceClassImpl());
            devm_.AddClass(new Devices.Ethernet.DeviceClassImpl());
            devm_.AddClass(new Devices.UsbMonitor.DeviceClassImpl());
            devm_.AddClass(new Devices.AudioDevice.DeviceClassImpl());
            devm_.AddClass(new Devices.AudioFile.DeviceClassImpl());

#if DEBUG
            devm_.AddClass(new Devices.UsbComm.DeviceClassImpl());
            devm_.AddClass(new Devices.Ncom.DeviceClassImpl());
#endif
        }

        public static DeviceClass[] GetDeviceList()
        {
            return (devm_.GetClasses().ToArray());
        }

        public static GateObject[] GetGateList()
        {
            lock (gates_) {
                return (gates_.ToArray());
            }
        }

        public static DeviceClass FindDeviceClass(Guid class_id)
        {
            return (devm_.FindClass(class_id));
        }

        public static Regex GetWildcardAliasModule(string alias)
        {
            /* aliasをワイルドカードとして扱うために正規表現に変換する */
            alias = alias.Replace("?", ".");
            alias = alias.Replace("*", ".*?");

            /* 正規表現オブジェクトを生成 */
            return (new Regex("^" + alias + "$"));
        }

        public static GateObject[] FindGateObjectFromWildcardAlias(string alias)
        {
            /* 正規表現オブジェクトを生成 */
            var regex = GetWildcardAliasModule(alias);

            /* スキャン実行 */
            lock (gates_) {
                return (gates_.FindAll(gate => (gate.DeviceClassID != Guid.Empty) && (regex.IsMatch(gate.Alias))).ToArray());
            }
        }

        public static GateObject[] FindGateObjectFromAlias(string alias)
        {
            lock (gates_) {
                return (gates_.FindAll(gate => (gate.DeviceClassID != Guid.Empty) && (gate.Alias == alias)).ToArray());
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

        public static GateObject CreateGateObject(GateProperty gatep, DeviceConfig devconf, Guid devc_id, DeviceProperty devp)
        {
            var gate = new GateObject(gatep, devconf, devc_id, devp);

            lock (gates_) {
                gates_.Add(gate);
            }

            return (gate);
        }
    }
}
