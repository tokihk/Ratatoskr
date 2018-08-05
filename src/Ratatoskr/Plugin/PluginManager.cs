using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Forms;
using Ratatoskr.Protocol;

namespace Ratatoskr.Plugin
{
    internal static class PluginManager
    {
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

        private static string GetPluginPath()
        {
#if DEBUG
            return (System.Windows.Forms.Application.StartupPath);
#else
            return (Program.GetWorkspaceDirectory("plugins"));
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
            Debugger.DebugManager.MessageOut(string.Format("LoadAllPlugin - Start: [{0}]", path_plugin));

            /* プラグイン検索 */
            try {
                var asm_paths = Directory.EnumerateFiles(path_plugin, "*.dll", SearchOption.TopDirectoryOnly);

                foreach (var asm_path in asm_paths.Select((v, i) => (v, i))) {
                    LoadPlugin(asm_path.v);

                    Program.SetStartupProgress(Program.StartupTaskID.LoadPlugin, (byte)(asm_path.i * 100 / asm_paths.Count()));
                }
            } catch {
            }

            Program.SetStartupProgress(Program.StartupTaskID.LoadPlugin, 100);

            Debugger.DebugManager.MessageOut("LoadAllPlugin - End");
        }

        private static void LoadPlugin(string asm_path)
        {
            if (!File.Exists(asm_path))return;

            try {
                var asm_object = Assembly.LoadFile(asm_path);

                if (asm_object == null)return;

                var plugin_info = (PluginInfo)null;

                foreach (var asm_type in asm_object.GetTypes()) {
                    /* デコーダークラスを継承しているクラスのみロード */
                    if (   (!asm_type.IsClass)
                        || (!asm_type.IsPublic)
                        || (asm_type.IsAbstract)
                    ) {
                        continue;
                    }

                    plugin_info = new PluginInfo(asm_path, asm_object, asm_type);

                    /* モジュール読み込み */
                    ProtocolManager.LoadFromPlugin(plugin_info);
                }
            } catch {
            }
        }
    }
}
