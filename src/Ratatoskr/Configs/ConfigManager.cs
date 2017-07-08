using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic.Generic;
using Ratatoskr.Configs;
using Ratatoskr.Configs.FixedConfigs;
using Ratatoskr.Configs.LanguageConfigs;
using Ratatoskr.Configs.SystemConfigs;
using Ratatoskr.Configs.UserConfigs;

namespace Ratatoskr.Configs
{
    internal static class ConfigManager
    {
        public static FixedConfig    Fixed    { get; } = new FixedConfig();
        public static SystemConfig   System   { get; } = new SystemConfig();
        public static UserConfig     User     { get; } = new UserConfig();
        public static LanguageConfig Language { get; } = new LanguageConfig();


        public static void Startup()
        {
        }

        public static void Shutdown()
        {
        }

        public static void LoadAllConfig()
        {
            System.Load();
            User.Load();
            Language.Load();
        }

        public static void SaveAllConfig()
        {
            System.Save();
            User.Save();
        }

        public static string GetCurrentDirectory()
        {
            /* ディレクトリが存在しない場合はマイドキュメント */
            if (!Directory.Exists(User.CurrentDirectory.Value)) {
                User.CurrentDirectory.Value = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }

            /* ディレクトリが存在しない場合はデスクトップ */
            if (!Directory.Exists(User.CurrentDirectory.Value)) {
                User.CurrentDirectory.Value = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            }

            return (User.CurrentDirectory.Value);
        }

        public static void SetCurrentDirectory(string path)
        {
            User.CurrentDirectory.Value = path;
        }
    }
}
