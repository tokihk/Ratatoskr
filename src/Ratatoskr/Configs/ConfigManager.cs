using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic.Container;
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

        public static void LoadAllConfig(string profile_name = null)
        {
            System.Load();

            if (profile_name != null) {
                System.Profile.ProfileName.Value = profile_name;
            }

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

        public static string GetProfileRootPath()
        {
            return (Program.GetWorkspaceDirectory(System.Profile.ProfilePath.Value));
        }

        public static string GetProfilePath(string profile_name)
        {
            return (Path.Combine(
                        Program.GetWorkspaceDirectory(System.Profile.ProfilePath.Value),
                        profile_name));
        }

        public static bool ProfileIsExist(string profile_name)
        {
            return (Directory.Exists(GetProfilePath(profile_name)));
        }

        public static string[] GetProfileList()
        {
            var profiles = new List<string>();

            profiles.Add(GetSelectProfileName());
            if (Directory.Exists(GetProfileRootPath())) {
                profiles.AddRange(Directory.EnumerateDirectories(GetProfileRootPath()).Select(path => Path.GetFileNameWithoutExtension(path)));
            }

            return (profiles.Distinct().ToArray());
        }

        public static string GetDefaultProfileName()
        {
            var name_base = string.Format("{1}_{2}", Environment.MachineName, Environment.UserName, DateTime.Now.ToString("yyyyMMddHHmmss"));
            var name_make = name_base;
            var count = 1;

            /* 同名のディレクトリが存在する場合は末尾にカウントを付けて再確認 */
            while (Directory.Exists(GetProfilePath(name_make))) {
                name_make = string.Format("{0}({1})", name_base, count);
                count++;
            }

            return (name_make);
        }

        public static string GetSelectProfileName()
        {
            return (System.Profile.ProfileName.Value);
        }

        public static string GetSelectProfilePath()
        {
            return (GetProfilePath(GetSelectProfileName()));
        }

        public static string GetSelectProfileFilePath(string filename, bool exist_check = false)
        {
            /* プロファイルディレクトリパスを取得 */
            var path_profile = GetSelectProfilePath();

            if (path_profile == null)return (null);

            /* 設定ファイルパスを取得 */
            var path_config = Path.Combine(path_profile, filename);

            /* 存在確認 */
            if (exist_check) {
                if ((path_config == null) || (!File.Exists(path_config))) {
                    return (null);
                }
            }

            return (path_config);
        }
    }
}
