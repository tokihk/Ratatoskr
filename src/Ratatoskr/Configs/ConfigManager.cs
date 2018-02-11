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
        public class ProfileInfo
        {
            public string     ID     { get; } = "";
            public UserConfig Config { get; } = null;


            private ProfileInfo(string profile_id, UserConfig config)
            {
                ID = profile_id;
                Config = config;
            }

            public static ProfileInfo Load(string profile_id)
            {
                var config = new UserConfig();

                /* 読み込めないプロファイルは無視 */
                if (!config.Load(GetProfilePath(profile_id)))return (null);

                return (new ProfileInfo(profile_id, config));
            }
        }


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

        public static void LoadAllConfig(string profile_id = null)
        {
            System.Load();

            /* 存在するプロファイルを指定している場合は指定したプロファイルに切り替える */
            if (ProfileIsExist(profile_id)) {
                System.Profile.ProfileID.Value = profile_id;
            }

            LoadCurrentProfile();

            Language.Load();

            /* プロファイルの実体が存在しない場合は実体を作るために保存する */
            if (!ProfileIsExist(System.Profile.ProfileID.Value)) {
                SaveAllConfig();
            }
        }

        public static void SaveAllConfig()
        {
            System.Save();

            SaveCurrentProfile();
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
            return (Program.GetWorkspaceDirectory(System.Profile.ProfileDir.Value));
        }

        public static string GetProfilePath(string profile_id)
        {
            if ((profile_id == null) || (profile_id.Length == 0))return (null);

            return (Path.Combine(
                        Program.GetWorkspaceDirectory(System.Profile.ProfileDir.Value),
                        profile_id));
        }

        public static string GetProfileFilePath(string profile_id, string file_name, bool exist_check = false)
        {
            /* プロファイルディレクトリパスを取得 */
            var path_profile = GetProfilePath(profile_id);

            if (path_profile == null)return (null);

            /* 設定ファイルパスを取得 */
            var path_config = Path.Combine(path_profile, file_name);

            /* 存在確認 */
            if (exist_check) {
                if ((path_config == null) || (!File.Exists(path_config))) {
                    return (null);
                }
            }

            return (path_config);
        }

        public static bool ProfileIsExist(string profile_id)
        {
            return (Directory.Exists(GetProfilePath(profile_id)));
        }

        public static IEnumerable<ProfileInfo> GetProfileList()
        {
            var profiles = new List<ProfileInfo>();
            var profile = (ProfileInfo)null;

            foreach (var dir in Directory.EnumerateDirectories(GetProfileRootPath())) {
                profile = ProfileInfo.Load(Path.GetFileName(dir));

                if (profile == null)continue;

                profiles.Add(profile);
            }

            return (profiles);
        }

        public static string GetDefaultProfileID()
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

        public static string GetCurrentProfileID()
        {
            return (System.Profile.ProfileID.Value);
        }

        public static string GetCurrentProfilePath()
        {
            return (GetProfilePath(GetCurrentProfileID()));
        }

        public static string GetCurrentProfileFilePath(string file_name, bool exist_check = false)
        {
            return (GetProfileFilePath(GetCurrentProfileID(), file_name, exist_check));
        }

        private static bool LoadCurrentProfile()
        {
            return (User.Load(GetCurrentProfilePath()));
        }

        public static bool SaveCurrentProfile(bool read_only_check = true)
        {
            return (User.Save(GetCurrentProfilePath(), read_only_check));
        }

        public static string CreateNewProfile(UserConfig config)
        {
            var profile_id = Guid.NewGuid().ToString("B");

            config.Save(GetProfilePath(profile_id), false);

            return (profile_id);
        }
    }
}
