using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General;
using Ratatoskr.Config.Data.Fixed;
using Ratatoskr.Config.Data.System;
using Ratatoskr.Config.Data.User;
using Ratatoskr.Config.Data.Language;
using Ratatoskr.Debugger;
using Ratatoskr.FileFormat;
using Ratatoskr.Forms;
using Ratatoskr.Plugin;
using Ratatoskr.Native.Windows;

namespace Ratatoskr.Config
{
    internal static class ConfigManager
    {
        internal class ProfileData
        {
            public Guid       ID     { get; } = Guid.Empty;
            public UserConfig Config { get; } = null;


            private ProfileData(Guid profile_id, UserConfig config)
            {
                ID = profile_id;
                Config = config;
            }

            public static ProfileData Load(Guid profile_id)
            {
                var config = new UserConfig();

                /* 読み込めないプロファイルは無視 */
                if (!config.Load(GetProfilePath(profile_id)))return (null);

                return (new ProfileData(profile_id, config));
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


		static ConfigManager()
		{
            Fixed.ApplicationName.Value = AppInfo.Name;
            Fixed.Copyright.Value       = AppInfo.Copyright;
            Fixed.Version.Value         = AppInfo.Version;

            Fixed.ApplicationListUrl.Value.Add("");
		}

        public static FixedConfig			Fixed    { get; private set; } = new FixedConfig();
        public static SystemConfig			System   { get; private set; } = new SystemConfig();
        public static UserConfig			User     { get; private set; } = new UserConfig();
        public static LanguageConfig		Language { get; private set; } = new LanguageConfig();

		public static event EventHandler	Updated;

        public static void Startup()
        {
        }

        public static void Shutdown()
        {
        }

        public static void LoadFromFile(Guid profile_id)
        {
            /* システム設定を読み込み */
            System.LoadConfig(Program.GetWorkspaceDirectory(Fixed.SystemConfigPath.Value));

            /* 外部からプロファイルを指定している場合は差し替える */
            if (profile_id != Guid.Empty) {
                System.Profile.ProfileID.Value = profile_id;
            }

            /* システム設定に従ってプロファイルを読み込み */
            LoadCurrentProfile();

            /* 言語ファイルを読み込み */
            Language.Load();

			/* モジュールに設定を反映 */
			FormUiManager.LoadConfig();
        }

        public static void SaveToFile(bool profile_backup = true)
        {
            /* 現在の状態を設定データに反映 */
            FormUiManager.BackupConfig();

            /* システム設定を保存 */
            System.SaveConfig(Program.GetWorkspaceDirectory(ConfigManager.Fixed.SystemConfigPath.Value));

            /* 現在のプロファイルを保存 */
            if (profile_backup) {
                SaveCurrentProfile();
            }
        }

		public static void UpdateConfig(SystemConfig sys_config = null, UserConfig user_config = null, LanguageConfig lang_config = null)
		{
			if (sys_config != null) {
				System.DeepCopy(sys_config);
			}
			if (user_config != null) {
				User.DeepCopy(user_config);
			}
			if (lang_config != null) {
				Language.DeepCopy(lang_config);
			}

			Updated?.Invoke(null, EventArgs.Empty);
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
            System.CurrentDirectory.Value = (path ?? "");
        }

        public static string GetProfileRootPath()
        {
            return (Program.GetWorkspaceDirectory(System.Profile.ProfileDir.Value));
        }

        public static IEnumerable<ProfileData> GetProfileList()
        {
            var profiles = new List<ProfileData>();
            var profile = (ProfileData)null;
            var search_dir = GetProfileRootPath();

            if (Directory.Exists(search_dir)) {
                foreach (var dir in Directory.EnumerateDirectories(GetProfileRootPath())) {
                    try {
                        profile = ProfileData.Load(Guid.Parse(Path.GetFileName(dir)));

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

            DebugManager.MessageOut(DebugEventSender.Application, DebugEventType.ConfigEvent, string.Format("Load Profile :{0}", GetCurrentProfileID()));

            User.Load(GetCurrentProfilePath());

			/* 各モジュールで設定値を適用 */
			PluginManager.LoadConfig();
        }

        public static void SaveCurrentProfile(bool ignore_readonly = false)
        {
            var path_profile = GetCurrentProfilePath();

            /* 実体が存在しないプロファイルの場合は何もしない */
            if (!ProfileIsExist(GetCurrentProfileID()))return;

            /* 読込専用のときは何もしない */
            if ((!ignore_readonly) && (User.ReadOnly.Value))return;

			/* 各モジュールの設定値をバックアップ */
			PluginManager.BackupConfig();

            DebugManager.MessageOut(DebugEventSender.Application, DebugEventType.ConfigEvent, string.Format("Save Profile :{0}", GetCurrentProfileID()));

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
                if (!FormUiManager.ShowProfileEditDialog("Edit Profile", config)) {
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

            DebugManager.MessageOut(DebugEventSender.Application, DebugEventType.ConfigEvent, string.Format("Delete Profile :{0}", profile_id));

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
                Program.ChangeProfile(profile_list.ElementAt(profile_index).ID, false);
            }
        }

        public static void ImportProfile(UserConfig config, Dictionary<string, byte[]> ext_data_list)
        {
            var profile_id = CreateNewProfile(null, config);

            if (profile_id == Guid.Empty)return;

            foreach (var ext_data in ext_data_list) {
                File.WriteAllBytes(GetProfileFilePath(profile_id, ext_data.Key), ext_data.Value);
            }
        }

        public static void ExportProfile(FileControlParam file, Guid profile_id)
        {
            var writer = file.Format.CreateWriter() as UserConfigWriter;

			if (writer == null)return;

            var option = file.Option as UserConfigWriterOption;

			if (option == null)return;

            /* 出力設定 */
            option.TargetProfileID = profile_id;

            /* 出力 */
            if (writer.Open(option, file.FilePath)) {
                writer.Save();
                writer.Close();
            }
        }
    }
}
