using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.Gate.PacketAutoSave
{
    internal static class PacketAutoSaveManager
    {
        private static PacketAutoSaveObject module_ = null;
        private static object               module_sync_ = new object();


        public static void Update()
        {
            lock (module_sync_) {
                Stop();
                Start();
            }
        }

        private static void Start()
        {
            if (!ConfigManager.User.Option.AutoSave.Value)return;

            switch (ConfigManager.User.Option.AutoSaveTimming.Value) {
                case AutoSaveTimmingType.Interval:    module_ = new PacketAutoSaveObject_Interval();    break;
                case AutoSaveTimmingType.FileSize:    module_ = new PacketAutoSaveObject_FileSize();    break;
                case AutoSaveTimmingType.PacketCount: module_ = new PacketAutoSaveObject_PacketCount(); break;
            }
        }

        private static void Stop()
        {
            if (module_ == null)return;

            module_ = null;
        }

        public static void Output(IEnumerable<PacketObject> packets)
        {
            if (module_ == null)return;

            module_.Output(packets);
        }
    }
}
