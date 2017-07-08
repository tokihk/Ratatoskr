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

        private static List<GateObject> gates_ = new List<GateObject>();

        
        public static void Startup()
        {
            /* デバイスマネージャー初期化 */
            devm_ = new DeviceManager(GatePacketManager.BasePacketManager);

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
            devm_.AddClass(new Devices.SerialPort.DeviceClassImpl());
            devm_.AddClass(new Devices.TcpServer.DeviceClassImpl());
            devm_.AddClass(new Devices.TcpClient.DeviceClassImpl());
            devm_.AddClass(new Devices.UdpClient.DeviceClassImpl());
            devm_.AddClass(new Devices.Ethernet.DeviceClassImpl());
            devm_.AddClass(new Devices.UsbMonitor.DeviceClassImpl());
            devm_.AddClass(new Devices.MicInput.DeviceClassImpl());
            devm_.AddClass(new Devices.AudioFile.DeviceClassImpl());
        }

        public static DeviceClass[] GetDeviceList()
        {
            return (devm_.GetClasses().ToArray());
        }

        public static DeviceClass FindDeviceClass(Guid class_id)
        {
            return (devm_.FindClass(class_id));
        }

        public static DeviceInstance FindDeviceObject(Guid obj_id)
        {
            return (devm_.FindInstance(obj_id));
        }

        public static GateObject FindGateObject(Guid obj_id)
        {
            lock (gates_) {
                return (gates_.Find(gate => gate.ID == obj_id));
            }
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
                return (gates_.FindAll(gate => regex.IsMatch(gate.Alias)).ToArray());
            }
        }

        public static GateObject[] FindGateObjectFromAlias(string alias)
        {
            lock (gates_) {
                return (gates_.FindAll(gate => gate.Alias == alias).ToArray());
            }
        }

        public static DeviceInstance CreateDeviceObject(Guid class_id, string name, DeviceProperty devp)
        {
            return (devm_.CreateInstance(class_id, Guid.NewGuid(), name, devp));
        }

        public static DeviceProperty CreateDeviceProperty(Guid class_id)
        {
            return (devm_.CreateProperty(class_id));
        }

        public static GateObject CreateGateObject(string alias, bool connect, DeviceInstance devi)
        {
            var gate = new GateObject(Guid.NewGuid());

            gate.Alias = alias;
            gate.ConnectRequest = connect;
            gate.Device = devi;

            lock (gates_) {
                gates_.Add(gate);
            }

            return (gate);
        }

        public static string CreateNewAlias()
        {
            var alias_new = "GATE_000";

            lock (gates_) {
                for (var num = 0; num < ConfigManager.Fixed.GateControllerLimit.Value; num++) {
                    alias_new = String.Format("GATE_{0:D3}", num);

                    /* ゲートのいずれのエイリアスにも一致しない場合はスキャン終了 */
                    if (gates_.TrueForAll(gate => gate.Alias != alias_new))break;
                }
            }

            return (alias_new);
        }
    }
}
