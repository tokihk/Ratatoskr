using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.FileFormats;
using Ratatoskr.Protocol;
using Ratatoskr.Devices;
using RtsCore;
using RtsCore.Framework.Device;
using RtsCore.Framework.FileFormat;
using RtsCore.Framework.Plugin;
using RtsCore.Protocol;

namespace Ratatoskr.Plugin
{
    internal static class PluginManager
    {
        private static List<PluginClass> plugin_list_ = new List<PluginClass>();
        private static readonly object   plugin_sync_ = new object();

        private static IAsyncResult ar_load_plugin_ = null;


        public static void Startup()
        {
            Program.SetStartupProgress(Program.StartupTaskID.LoadPlugin, 0);

            if (   (Program.IsSafeMode)
                || (!LoadAllPlugin())
            ) {
                Program.SetStartupProgress(Program.StartupTaskID.LoadPlugin, 100);
            }
        }

        public static void Shutdown()
        {
        }

        public static bool IsLoadBusy
        {
            get
            {
                return ((ar_load_plugin_ != null) && (!ar_load_plugin_.IsCompleted));
            }
        }

        public static IEnumerable<PluginClass> GetPluginList()
        {
            lock (plugin_sync_) {
                return (plugin_list_.ToArray());
            }
        }

        private static string GetPluginPath()
        {
#if DEBUG
            return (System.Windows.Forms.Application.StartupPath);
#else
            return (System.Windows.Forms.Application.StartupPath);
//            return (Program.GetWorkspaceDirectory("plugins"));
#endif
        }

        public static bool LoadAllPlugin()
        {
            var path_plugin = GetPluginPath();

            /* プラグインディレクトリが存在しない場合は何もしない */
            if (!Directory.Exists(path_plugin))return (false);

            /* 読込開始 */
            ar_load_plugin_ = (new LoadAllPluginTaskDelegate(LoadAllPluginTask)).BeginInvoke(path_plugin, null, null);

            return (IsLoadBusy);
        }

        private delegate void LoadAllPluginTaskDelegate(string path_plugin);
        private static void LoadAllPluginTask(string path_plugin)
        {
            Kernel.DebugMessage(string.Format("LoadAllPlugin - Start: [{0}]", path_plugin));

            /* プラグイン検索 */
            try {
                var asm_paths = from path in Directory.EnumerateFiles(path_plugin, "*.dll", SearchOption.TopDirectoryOnly)
								where path.Contains("RtsPlugin")
								where !PluginEgnoreList.IGNORE_NAMES.Contains(Path.GetFileName(path))
								select path;

                foreach (var asm_path in asm_paths.Select((v, i) => (v, i))) {
                    Kernel.DebugMessage(string.Format("CheckDLL [{0:D4}: {1}]", asm_path.i, asm_path.v));

                    LoadPlugin(asm_path.v);

                    Program.SetStartupProgress(Program.StartupTaskID.LoadPlugin, (byte)(asm_path.i * 100 / asm_paths.Count()));
                }
            } catch {
            }

            Program.SetStartupProgress(Program.StartupTaskID.LoadPlugin, 100);

            Kernel.DebugMessage("LoadAllPlugin - End");
        }

        private static void LoadPlugin(string asm_path)
        {
            if (!File.Exists(asm_path))return;

            try {
                var asm_object = Assembly.LoadFile(asm_path);

                if (asm_object == null)return;

                foreach (var asm_type in asm_object.GetTypes()) {
                    /* プラグインクラスを継承しているクラスのみロード */
                    if (   (!asm_type.IsClass)
                        || (!asm_type.IsPublic)
                        || (asm_type.IsAbstract)
                        || (!asm_type.IsSubclassOf(typeof(RtsCore.Framework.Plugin.PluginClass)))
                    ) {
                        continue;
                    }

                    LoadPlugin(new PluginInfo(asm_path, asm_object, asm_type));
                }
            } catch {
            }
        }

        private static void LoadPlugin(PluginInfo info)
        {
            if (info == null)return;

            var plugin = info.LoadModule() as PluginClass;

            if (plugin == null)return;

            lock (plugin_sync_) {
                Kernel.DebugMessage(string.Format("LoadPlugin [{0}]", info.AssemblyPath));

                plugin_list_.Add(plugin);

                LoadPlugin_Device(plugin.LoadDeviceClasses());
                LoadPlugin_PacketLogFormat(plugin.LoadPacketLogFormatClasses());
                LoadPlugin_ProtocolDecoder(plugin.LoadProtocolDecoderClasses());
            }
        }

        private static void LoadPlugin_Device(IEnumerable<DeviceClass> devs)
        {
            if (devs == null)return;

            foreach (var dev in devs) {
                DeviceManager.AddDevice(dev);
            }
        }

        private static void LoadPlugin_PacketLogFormat(IEnumerable<FileFormatClass> formats)
        {
            if (formats == null)return;

            foreach (var format in formats) {
                if (format.CanRead) {
                    FileManager.FileOpen.Formats.Add(format);
                    FileManager.PacketLogOpen.Formats.Add(format);
                }
                if (format.CanWrite) {
                    FileManager.PacketLogSave.Formats.Add(format);
                }
            }
        }

        private static void LoadPlugin_ProtocolDecoder(IEnumerable<ProtocolDecoderClass> prdcs)
        {
            if (prdcs == null)return;

            foreach (var prdc in prdcs) {
                ProtocolManager.AddDecoder(prdc);
            }
        }
    }
}
