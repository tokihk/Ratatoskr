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
using Ratatoskr.Forms;

namespace Ratatoskr.Configs
{
    internal static class ConfigManager
    {
        public class ProfileInfo
        {
            public Guid       ID     { get; } = Guid.Empty;
            public UserConfig Config { get; } = null;


            private ProfileInfo(Guid profile_id, UserConfig config)
            {
                ID = profile_id;
                Config = config;
            }

            public static ProfileInfo Load(Guid profile_id)
            {
                var config = new UserConfig();

                /* 読み込めないプロファイルは無視 */
                if (!config.Load(GetProfilePath(profile_id)))return (null);

                return (new ProfileInfo(profile_id, config));
            }

            public override string ToString()
            {
                return (Config.ProfileName.Value);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj is Guid) {
                    return (((Guid)obj) == ID);
                }

                return (base.Equals(obj));
            }
        }


        public static FixedConfig    Fixed    { get; private set; }
        public static SystemConfig   System   { get; private set; }
        public static UserConfig     User     { get; private set; }
        public static LanguageConfig Language { get; private set; }

        private static ConfigHolder[] override_configs_ = null;


        public static void Startup()
        {
            Fixed = new FixedConfig();
            System = new SystemConfig();
            User = new UserConfig();
            Language = new LanguageConfig();
        }

        public static void Shutdown()
        {
        }

        public static void LoadConfig(Guid profile_id)
        {
            System.Load();

            if (profile_id != Guid.Empty) {
                System.Profile.ProfileID.Value = profile_id;
            }

            LoadCurrentProfile();

            Language.Load();

            /* --- 設定上書き --- */
            LoadOverrideConfig();
        }

        private static void LoadOverrideConfig()
        {
            if (override_configs_ == null)return;

            foreach (var config in override_configs_) {
                if (config is SystemConfig) {
                    System = config as SystemConfig;
                } else if (config is UserConfig) {
                    User = config as UserConfig;
                } else if (config is LanguageConfig) {
                    Language = config as LanguageConfig;
                }
            }

            override_configs_ = null;
        }

        public static void SaveConfig()
        {
            /* 現在の状態を設定データに反映 */
            FormUiManager.BackupConfig();

            System.Save();

            SaveCurrentProfile();
        }

        public static string GetCurrentDirectory()
        {
            /* ディレクトリが存在しない場合はマイドキュメント */
            if (!Directory.Exists(System.CurrentDirectory.Value)) {
                System.CurrentDirectory.Value = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }

            /* ディレクトリが存在しない場合はデスクトップ */
            if (!Directory.Exists(System.CurrentDirectory.Value)) {
                System.CurrentDirectory.Value = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            }

            return (System.CurrentDirectory.Value);
        }

        public static void SetCurrentDirectory(string path)
        {
            System.CurrentDirectory.Value = path;
        }

        public static string GetProfileRootPath()
        {
            return (Program.GetWorkspaceDirectory(System.Profile.ProfileDir.Value));
        }

        public static string GetProfilePath(Guid profile_id)
        {
            if (profile_id == Guid.Empty)return (null);

            return (Path.Combine(
                        Program.GetWorkspaceDirectory(System.Profile.ProfileDir.Value),
                        profile_id.ToString("D")));
        }

        public static string GetProfileFilePath(Guid profile_id, string file_name, bool exist_check = false)
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

        public static bool ProfileIsExist(Guid profile_id)
        {
            return (Directory.Exists(GetProfilePath(profile_id)));
        }

        public static IEnumerable<ProfileInfo> GetProfileList()
        {
            var profiles = new List<ProfileInfo>();
            var profile = (ProfileInfo)null;
            var search_dir = GetProfileRootPath();

            if (Directory.Exists(search_dir)) {
                foreach (var dir in Directory.EnumerateDirectories(GetProfileRootPath())) {
                    try {
                        profile = ProfileInfo.Load(Guid.Parse(Path.GetFileName(dir)));

                        if (profile == null)continue;

                        profiles.Add(profile);
                    } catch {
                    }
                }
            }

            return (profiles);
        }

        public static string GetDefaultProfileName()
        {
            return (string.Format("{1}_{2}", Environment.MachineName, Environment.UserName, DateTime.Now.ToString("yyyyMMddHHmmss")));
        }

        public static Guid GetCurrentProfileID()
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

        public static Guid CreateNewProfile(UserConfig config)
        {
            var profile_id = Guid.NewGuid();

            config.Save(GetProfilePath(profile_id), false);

            return (profile_id);
        }

        public static void OverrideConfig(params UserConfig[] configs)
        {
            override_configs_ = configs;

            Program.RestartRequest();
        }
    }
}
