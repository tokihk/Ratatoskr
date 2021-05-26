using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config;
using Ratatoskr.Debugger;
using Ratatoskr.FileFormat;
using Ratatoskr.Gate;
using Ratatoskr.Device;
using Ratatoskr.PacketView;

namespace Ratatoskr.Plugin
{
    internal static class PluginManager
    {
        private static List<PluginInstance> plugin_list_ = new List<PluginInstance>();
        private static readonly object      plugin_sync_ = new object();

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

		public static void LoadConfig()
		{
			lock (plugin_sync_) {
				foreach (var plgi in plugin_list_) {
					var config = ConfigManager.User.Plugins.Value.Find(item => item.PluginClassID == plgi.Class.ID);
					var prop = (PluginProperty)null;

					/* 設定ファイルからプロパティを取得 */
					if (config != null) {
						prop = config.PluginProperty;
					}

					/* 設定ファイルからプロパティを復元できない場合はデフォルト値で生成 */
					if (prop == null) {
						prop = plgi.Class.CreateProperty();
					}

					plgi.LoadProperty(prop);
				}
			}
		}

		public static void BackupConfig()
		{
			ConfigManager.User.Plugins.Value.Clear();

			lock (plugin_sync_) {
				foreach (var plgi in plugin_list_) {
					ConfigManager.User.Plugins.Value.Add(new Config.Data.User.PluginObjectConfig(plgi.Class.ID, plgi.Property));
				}
			}
		}

        public static PluginInstance[] GetPluginList()
        {
            lock (plugin_sync_) {
                return (plugin_list_.ToArray());
            }
        }

		public static PluginProperty CreatePluginPropery(Guid class_id)
		{
			lock (plugin_sync_) {
				var plgi = plugin_list_.Find(plugin => plugin.Class.ID == class_id);

				if (plgi == null)return (null);

				return (plgi.Class.CreateProperty());
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
            DebugManager.MessageOut(DebugMessageSender.Plugin, string.Format("LoadAllPlugin - Start: [{0}]", path_plugin));

            /* プラグイン検索 */
            try {
                var asm_paths = from path in Directory.EnumerateFiles(path_plugin, "*.dll", SearchOption.TopDirectoryOnly)
								where path.Contains("RtsPlugin")
								where !PluginEgnoreList.IGNORE_NAMES.Contains(Path.GetFileName(path))
								select path;

                foreach (var asm_path in asm_paths.Select((v, i) => (v, i))) {
                    DebugManager.MessageOut(string.Format("CheckDLL [{0:D4}: {1}]", asm_path.i, asm_path.v));

                    LoadPlugin(asm_path.v);

                    Program.SetStartupProgress(Program.StartupTaskID.LoadPlugin, (byte)(asm_path.i * 100 / asm_paths.Count()));
                }
            } catch {
            }

            Program.SetStartupProgress(Program.StartupTaskID.LoadPlugin, 100);

            DebugManager.MessageOut(DebugMessageSender.Plugin, "LoadAllPlugin - End");
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
                        || (!asm_type.IsSubclassOf(typeof(Ratatoskr.Plugin.PluginClass)))
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

            var plgc = info.LoadModule() as PluginClass;

            if (plgc == null)return;

			var plgi = plgc.CreateInstance();

			if (plgi == null)return;

            lock (plugin_sync_) {
                DebugManager.MessageOut(DebugMessageSender.Plugin, string.Format("LoadPlugin [{0}]", info.AssemblyPath));

                plugin_list_.Add(plgi);

				LoadPluginInterface(plgi.Interface);
            }
        }

		private static void LoadPluginInterface(PluginInterface plgif)
		{
			if (plgif.DevicePacketCaptureHandlers != null) {
				foreach (var handler in plgif.DevicePacketCaptureHandlers) {
					GatePacketManager.RawPacketEntried += handler;
				}
			}

            if (plgif.DeviceClasses != null) {
				foreach (var devc in plgif.DeviceClasses) {
					DeviceManager.Instance.AddDevice(devc);
				}
			}

            if (plgif.PacketViewClasses != null) {
				foreach (var viewc in plgif.PacketViewClasses) {
					PacketViewManager.Instance.AddView(viewc);
				}
			}

            if (plgif.PacketLogFormatClasses != null) {
				foreach (var format in plgif.PacketLogFormatClasses) {
					if (format.CanRead) {
						FileManager.FileOpen.Formats.Add(format);
						FileManager.PacketLogOpen.Formats.Add(format);
					}
					if (format.CanWrite) {
						FileManager.PacketLogSave.Formats.Add(format);
					}
				}
			}
		}
    }
}
