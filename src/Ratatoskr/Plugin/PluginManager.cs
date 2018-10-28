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

            var ignore_path_list = new []
            {
                "System.Net.Security.dll",
                "System.Net.Sockets.dll",
                "System.Net.WebHeaderCollection.dll",
                "System.Net.WebSockets.Client.dll",
                "System.Net.WebSockets.dll",
                "System.ObjectModel.dll",
                "System.Reflection.dll",
                "System.Reflection.Extensions.dll",
                "System.Reflection.Metadata.dll",
                "System.Reflection.Primitives.dll",
                "System.Resources.Reader.dll",
                "System.Resources.ResourceManager.dll",
                "System.Resources.Writer.dll",
                "System.Runtime.CompilerServices.Unsafe.dll",
                "System.Runtime.CompilerServices.VisualC.dll",
                "System.Runtime.dll",
                "System.Runtime.Extensions.dll",
                "System.Runtime.Handles.dll",
                "System.Runtime.InteropServices.dll",
                "System.Runtime.InteropServices.RuntimeInformation.dll",
                "System.Runtime.Numerics.dll",
                "System.Runtime.Serialization.Formatters.dll",
                "System.Runtime.Serialization.Json.dll",
                "System.Runtime.Serialization.Primitives.dll",
                "System.Runtime.Serialization.Xml.dll",
                "System.Security.Claims.dll",
                "System.Security.Cryptography.Algorithms.dll",
                "System.Security.Cryptography.Csp.dll",
                "System.Security.Cryptography.Encoding.dll",
                "System.Security.Cryptography.Primitives.dll",
                "System.Security.Cryptography.X509Certificates.dll",
                "System.Security.Principal.dll",
                "System.Security.SecureString.dll",
                "System.Text.Encoding.CodePages.dll",
                "System.Text.Encoding.dll",
                "System.Text.Encoding.Extensions.dll",
                "System.Text.RegularExpressions.dll",
                "System.Threading.dll",
                "System.Threading.Overlapped.dll",
                "System.Threading.Tasks.dll",
                "System.Threading.Tasks.Extensions.dll",
                "System.Threading.Tasks.Parallel.dll",
                "System.Threading.Thread.dll",
                "System.Threading.ThreadPool.dll",
                "System.Threading.Timer.dll",
                "System.ValueTuple.dll",
                "System.Xml.ReaderWriter.dll",
                "System.Xml.XDocument.dll",
                "System.Xml.XmlDocument.dll",
                "System.Xml.XmlSerializer.dll",
                "System.Xml.XPath.dll",
                "System.Xml.XPath.XDocument.dll",
                "WeifenLuo.WinFormsUI.Docking.dll",
                "LibUsbDotNet.dll",
                "log4net.dll",
                "Microsoft.CodeAnalysis.CSharp.dll",
                "Microsoft.CodeAnalysis.CSharp.Scripting.dll",
                "Microsoft.CodeAnalysis.dll",
                "Microsoft.CodeAnalysis.Scripting.dll",
                "Microsoft.Win32.Primitives.dll",
                "NAudio.dll",
                "netstandard.dll",
                "NxdnDotNet.dll",
                "PacketDotNet.dll",
                "ScintillaNET.dll",
                "SharpPcap.dll",
                "System.AppContext.dll",
                "System.Collections.Concurrent.dll",
                "System.Collections.dll",
                "System.Collections.Immutable.dll",
                "System.Collections.NonGeneric.dll",
                "System.Collections.Specialized.dll",
                "System.ComponentModel.dll",
                "System.ComponentModel.EventBasedAsync.dll",
                "System.ComponentModel.Primitives.dll",
                "System.ComponentModel.TypeConverter.dll",
                "System.Console.dll",
                "System.Data.Common.dll",
                "System.Diagnostics.Contracts.dll",
                "System.Diagnostics.Debug.dll",
                "System.Diagnostics.FileVersionInfo.dll",
                "System.Diagnostics.Process.dll",
                "System.Diagnostics.StackTrace.dll",
                "System.Diagnostics.TextWriterTraceListener.dll",
                "System.Diagnostics.Tools.dll",
                "System.Diagnostics.TraceSource.dll",
                "System.Diagnostics.Tracing.dll",
                "System.Drawing.Primitives.dll",
                "System.Dynamic.Runtime.dll",
                "System.Globalization.Calendars.dll",
                "System.Globalization.dll",
                "System.Globalization.Extensions.dll",
                "System.IO.Compression.dll",
                "System.IO.Compression.ZipFile.dll",
                "System.IO.dll",
                "System.IO.FileSystem.dll",
                "System.IO.FileSystem.DriveInfo.dll",
                "System.IO.FileSystem.Primitives.dll",
                "System.IO.FileSystem.Watcher.dll",
                "System.IO.IsolatedStorage.dll",
                "System.IO.MemoryMappedFiles.dll",
                "System.IO.Pipes.dll",
                "System.IO.UnmanagedMemoryStream.dll",
                "System.Linq.dll",
                "System.Linq.Expressions.dll",
                "System.Linq.Parallel.dll",
                "System.Linq.Queryable.dll",
                "System.Net.Http.dll",
                "System.Net.NameResolution.dll",
                "System.Net.NetworkInformation.dll",
                "System.Net.Ping.dll",
                "System.Net.Primitives.dll",
                "System.Net.Requests.dll",
            };

            /* プラグイン検索 */
            try {
                var asm_paths = Directory.EnumerateFiles(
                                    path_plugin,
                                    "*.dll",
                                    SearchOption.TopDirectoryOnly).Where(
                                        path => !ignore_path_list.Contains(Path.GetFileName(path)));

                foreach (var asm_path in asm_paths.Select((v, i) => (v, i))) {
                    Debugger.DebugManager.MessageOut(string.Format("LoadPlugin [{0:D4} : {1}]", asm_path.i, asm_path.v));

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
