using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;
using Ratatoskr.Gate.PacketAutoSave;
using Ratatoskr.Generic.Packet;


namespace Ratatoskr.Configs.UserConfigs
{
    internal enum SendPanelType
    {
        Data,
        File,
    }

    [Serializable]
    internal sealed class UserConfig : ConfigManagerBase<UserConfig>
    {
        private const string CONFIG_FILE_NAME = "setting.xml";

        public BoolConfig ReadOnlyLock => new BoolConfig(false);
        public BoolConfig ReadOnly     => new BoolConfig(false);

        public StringConfig  CreaterInfo    => new StringConfig(Environment.UserName);
        public StringConfig  CreaterComment => new StringConfig("");

        public IntegerConfig VersionMajor  => new IntegerConfig(0);
        public IntegerConfig VersionMinor  => new IntegerConfig(0);
        public IntegerConfig VersionBugfix => new IntegerConfig(0);
        public StringConfig  VersionModel  => new StringConfig("");

        public GateListConfig        GateList        => new GateListConfig();
        public PacketViewConfig      PacketView      => new PacketViewConfig();
        public PacketConverterConfig PacketConverter => new PacketConverterConfig();

        public StringConfig CurrentDirectory => new StringConfig("");

        public EnumConfig<PacketDataRateTarget>  DataRateTarget => new EnumConfig<PacketDataRateTarget>(PacketDataRateTarget.RecvData);

        public IntegerConfig             SendPanelLogLimit   => new IntegerConfig(20);
        public EnumConfig<SendPanelType> SendPanelType       => new EnumConfig<SendPanelType>(UserConfigs.SendPanelType.Data);
        public StringListConfig          SendPanelTargetList => new StringListConfig();
        public StringListConfig          SendPanel_ExpList   => new StringListConfig();
        public StringListConfig          SendPanel_FileList  => new StringListConfig();

        public BoolConfig                SendPanel_ExpList_Preview => new BoolConfig(true);

        public SendDataListConfig SendDataList       => new SendDataListConfig();
        public StringConfig       SendDataListTarget => new StringConfig("*");
        public IntegerConfig      SendDataListLimit  => new IntegerConfig(200);
        public IntegerConfig      SendDataListRepeat => new IntegerConfig(1);

        public WatchDataListConfig WatchDataList => new WatchDataListConfig();

        public PacketListConfig PacketList       => new PacketListConfig();
        public IntegerConfig    PacketListLimit  => new IntegerConfig(2000);
        public IntegerConfig    PacketListRepeat => new IntegerConfig(1);


        private OptionConfig option_ = new OptionConfig();
        public  OptionConfig Option
        {
            get { return (option_); }
            set {
                var value_old = option_;

                option_ = value;

                /* === ここに設定更新時の反映処理を入れる === */
                if (IsUpdated_AutoSaveSetting(value, value_old)) {
                    PacketAutoSaveManager.Setup();
                }

                /* ========================================== */
            }
        }


        public UserConfig() : base("user")
        {
            SendPanelTargetList.Value.Add("*");
            SendPanelTargetList.Value.Add("GATE_001");
            SendPanelTargetList.Value.Add("GATE_002");
            SendPanelTargetList.Value.Add("GATE_003");
            SendPanelTargetList.Value.Add("GATE_004");
            SendPanelTargetList.Value.Add("GATE_005");
        }

        public bool Load()
        {
            /* 設定ファイルパスを取得 */
            var path_config = ConfigManager.GetSelectProfileFilePath(CONFIG_FILE_NAME);

            if (path_config == null)return (false);

            /* プロファイルを読み込み */
            return (LoadConfig(path_config));
        }

        public bool Save()
        {
            /* 読込専用のときは何もしない */
            if (ReadOnly.Value)return (true);

            /* 設定ファイルパスを取得 */
            var path_config = ConfigManager.GetSelectProfileFilePath(CONFIG_FILE_NAME);

            if (path_config == null)return (false);

            /* バージョン情報を更新 */
            VersionMajor.Value = Program.Version.VersionMajor;
            VersionMinor.Value = Program.Version.VersionMinor;
            VersionBugfix.Value = Program.Version.VersionBugfix;
            VersionModel.Value = Program.Version.VersionModel;

            /* プロファイルを保存 */
            return (SaveConfig(path_config));
        }

        private bool IsUpdated_AutoSaveSetting(OptionConfig cfg_new, OptionConfig cfg_old)
        {
            return (   (cfg_new.AutoSaveDirectory.Value != cfg_old.AutoSaveDirectory.Value)
                    || (cfg_new.AutoSaveFormat.Value != cfg_old.AutoSaveFormat.Value)
                    || (cfg_new.AutoSavePrefix.Value != cfg_old.AutoSavePrefix.Value)
                    || (cfg_new.AutoSaveTimming.Value != cfg_old.AutoSaveTimming.Value)
                    || (cfg_new.AutoSaveValue_FileSize.Value != cfg_old.AutoSaveValue_FileSize.Value)
                    || (cfg_new.AutoSaveValue_Interval.Value != cfg_old.AutoSaveValue_Interval.Value)
                    || (cfg_new.AutoSaveValue_PacketCount.Value != cfg_old.AutoSaveValue_PacketCount.Value)
                );
        }
    }
}
