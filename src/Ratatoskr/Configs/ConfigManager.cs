using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Configs;
using Ratatoskr.Configs.FixedConfigs;
using Ratatoskr.Configs.LanguageConfigs;
using Ratatoskr.Configs.SystemConfigs;
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.Forms;
using Ratatoskr.Native;

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
            /* システム設定を読み込み */
            System.Load();

            /* 外部からプロファイルを指定している場合は差し替える */
            if (profile_id != Guid.Empty) {
                System.Profile.ProfileID.Value = profile_id;
            }

            /* システム設定に従ってプロファイルを読み込み */
            LoadCurrentProfile();

            /* 言語ファイルを読み込み */
            Language.Load();
        }

        public static void SaveConfig()
        {
            /* 現在の状態を設定データに反映 */
            FormUiManager.BackupConfig();

            /* システム設定を保存 */
            System.Save();

            /* 現在のプロファイルを保存 */
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
            var path_config = Path.Combine(path_profile, file_name.Trim());

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

        public static bool ProfileIsExist(string profile_name)
        {
            foreach (var profile in GetProfileList()) {
                if (profile.Config.ProfileName.Value == profile_name) {
                    return (true);
                }
            }

            return (false);
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

        private static void LoadCurrentProfile()
        {
            var profile_list = GetProfileList();

            /* プロファイルが１つも存在しないときはデフォルトプロファイルを生成 */
            if (profile_list.Count() == 0) {
                CreateNewProfile("Default Profile", null);

                /* 直前でプロファイル一覧が変化しているので再読み込み */
                profile_list = GetProfileList();
            }

            /* プロファイルが存在しないときは何もしない */
            if (profile_list.Count() == 0)return;

            /* 選択中のプロファイルが存在しないときは一番先頭のプロファイルを選択 */
            if (profile_list.FirstOrDefault(profile => profile.ID == System.Profile.ProfileID.Value) == null) {
                System.Profile.ProfileID.Value = profile_list.First().ID;
            }

            Debugger.DebugManager.MessageOut(string.Format("Load Profile :{0}", GetCurrentProfileID()));

            User.Load(GetCurrentProfilePath());
        }

        public static void SaveCurrentProfile(bool ignore_readonly = false)
        {
            var path_profile = GetCurrentProfilePath();

            /* 実体が存在しないプロファイルの場合は何もしない */
            if (!ProfileIsExist(GetCurrentProfileID()))return;

            /* 読込専用のときは何もしない */
            if ((!ignore_readonly) && (User.ReadOnly.Value))return;

            Debugger.DebugManager.MessageOut(string.Format("Save Profile :{0}", GetCurrentProfileID()));

            User.Save(GetCurrentProfilePath());
        }

        public static Guid CreateNewProfile(string profile_name, UserConfig config, bool force_edit_req = false)
        {
            var profile_id = Guid.NewGuid();

            /* 重複しないIDになるまで繰り返し */
            while (ProfileIsExist(profile_id)) { }

            if (config == null) {
                config = new UserConfig();
            }

            if (profile_name != null) {
                config.ProfileName.Value = profile_name;
            }

            /* 重複する名前のプロファイルが存在する場合は編集 */
            if ((ProfileIsExist(config.ProfileName.Value)) || (force_edit_req)) {
                if (!FormUiManager.ShowProfileEditDialog("Edit profile", config)) {
                    return (Guid.Empty);
                }
            }

            config.Save(GetProfilePath(profile_id));

            return (profile_id);
        }

        public static void DeleteProfile(Guid profile_id)
        {
            var profile_list = GetProfileList();

            /* プロファイルが2つ以上存在しないときは無効 */
            if (profile_list.Count() < 2)return;

            /* 指定のプロファイルが存在しないときは無効 */
            if (profile_list.FirstOrDefault(profile => profile.ID == profile_id) == null)return;

            /* 指定のプロファイルを削除 */
            Shell.rm(GetProfilePath(profile_id));

            Debugger.DebugManager.MessageOut(string.Format("Delete Profile :{0}", profile_id));

            /* 選択中のプロファイルを削除したときは違うプロファイルを選択 */
            if (System.Profile.ProfileID.Value == profile_id) {
                var profile_index = 0;

                /* 選択していたプロファイルのインデックスを取得 */
                foreach (var profile in profile_list) {
                    if (profile.ID == profile_id)break;
                    profile_index++;
                }

                /* 基本的には選択中だったプロファイルの次のプロファイルを選択する */
                if ((profile_index + 1) < profile_list.Count()) {
                    profile_index++;
                } else {
                    /* プロファイル数を超える場合は前のプロファイルを選択する */
                    profile_index--;
                }

                /* 再起動 */
                Program.ChangeProfile(profile_list.ElementAt(profile_index).ID);
            }
        }

        public static void ImportProfile(UserConfig config)
        {
            CreateNewProfile(null, config);
        }

        public static void ExportProfile(Guid profile_id)
        {
            var writer_info = FormUiManager.CreateUserConfigWriter();

            if (writer_info.writer == null)return;

            /* 出力設定 */
            writer_info.option.TargetProfileID = profile_id;

            /* 出力 */
            if (writer_info.writer.Open(writer_info.option, writer_info.path)) {
                writer_info.writer.Save();
                writer_info.writer.Close();
            }
        }
    }
}
