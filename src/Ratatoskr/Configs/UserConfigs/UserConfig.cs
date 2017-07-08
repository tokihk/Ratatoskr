using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;
using Ratatoskr.Generic.Generic;

namespace Ratatoskr.Configs.UserConfigs
{
    [Serializable]
    internal sealed class UserConfig : ConfigManagerBase<UserConfig>
    {
        private const string CONFIG_FILE_NAME = "setting.xml";


        public MainWindowConfig      MainWindow      { get; } = new MainWindowConfig();

        public GateConfig            Gate            { get; } = new GateConfig();
        public PacketViewConfig      PacketView      { get; } = new PacketViewConfig();
        public PacketConverterConfig PacketConverter { get; } = new PacketConverterConfig();

        public StringConfig  CurrentDirectory { get; } = new StringConfig("");

        public SingleCommandTargetConfig   SingleCommandTarget   { get; } = new SingleCommandTargetConfig();
        public SingleCommandContentsConfig SingleCommandContents { get; } = new SingleCommandContentsConfig();
        public IntegerConfig               SingleCommandLogLimit { get; } = new IntegerConfig(20);
        public StringConfig                SingleCommandFormat   { get; } = new StringConfig("Send(\"${target}\",\"${command}\")");

        public SequentialCommandListConfig SequentialCommandList   { get; } = new SequentialCommandListConfig();
        public StringConfig                SequentialCommandTarget { get; } = new StringConfig("*");
        public IntegerConfig               SequentialCommandLimit  { get; } = new IntegerConfig(200);
        public IntegerConfig               SequentialCommandRepeat { get; } = new IntegerConfig(1);

        public GateRedirectListConfig GateRedirectList { get; } = new GateRedirectListConfig();

        public PacketListConfig PacketList       { get; } = new PacketListConfig();
        public IntegerConfig    PacketListLimit  { get; } = new IntegerConfig(2000);
        public IntegerConfig    PacketListRepeat { get; } = new IntegerConfig(1);


        private OptionConfig option_ = new OptionConfig();
        public  OptionConfig Option
        {
            get { return (option_); }
            set {
                /* === ここに設定更新時の反映処理を入れる === */

                /* ========================================== */
                option_ = value;
            }
        }


        public UserConfig() : base("user")
        {
        }

        public string GetProfilePath()
        {
            var profile_list = ConfigManager.System.Profile.ProfileList.Value;
            var profile_name = ConfigManager.System.Profile.ProfileName.Value;

            /* プロファイルリストから該当プロファイルを検索 */
            var profile = profile_list.Find(pf => pf.Name == profile_name);

            if (profile == null)return (null);

            return (Program.GetWorkspaceDirectory(profile.Path));
        }

        public string GetProfilePath(string filename, bool exist_check = false)
        {
            /* プロファイルディレクトリパスを取得 */
            var path_profile = GetProfilePath();

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

        public bool Load()
        {
            /* 設定ファイルパスを取得 */
            var path_config = GetProfilePath(CONFIG_FILE_NAME);

            if (path_config == null)return (false);

            /* プロファイルを読み込み */
            return (LoadConfig(path_config));
        }

        public bool Save()
        {
            /* 設定ファイルパスを取得 */
            var path_config = GetProfilePath(CONFIG_FILE_NAME);

            if (path_config == null)return (false);

            /* プロファイルを保存 */
            return (SaveConfig(path_config));
        }
    }
}
