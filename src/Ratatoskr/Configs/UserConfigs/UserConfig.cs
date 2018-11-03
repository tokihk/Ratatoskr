using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Config;
using RtsCore.Config.Types;
using RtsCore.Packet;


namespace Ratatoskr.Configs.UserConfigs
{
    internal enum SendPanelType
    {
        Data,
        File,
        Log,
    }

    internal enum SendLogDataType
    {
        RecvOnly,
        SendOnly,
        RecvAndSend,
    }

    [Serializable]
    internal sealed class UserConfig : ConfigManagerBase<UserConfig>
    {
        public const string CONFIG_FILE_NAME = "UserConfig.xml";

        public DateTimeConfig CreateDateTime { get; } = new DateTimeConfig(DateTime.UtcNow);

        public StringConfig   ProfileName    { get; } = new StringConfig(Environment.UserName + " " + DateTime.Now.ToString("yyyyMMddHHmmss"));
        public StringConfig   ProfileComment { get; } = new StringConfig("");

        public BoolConfig ReadOnlyLock { get; } = new BoolConfig(false);
        public BoolConfig ReadOnly     { get; } = new BoolConfig(false);

        public IntegerConfig VersionMajor  { get; } = new IntegerConfig(0);
        public IntegerConfig VersionMinor  { get; } = new IntegerConfig(0);
        public IntegerConfig VersionBugfix { get; } = new IntegerConfig(0);
        public StringConfig  VersionModel  { get; } = new StringConfig("");

        public GateListConfig        GateList        { get; } = new GateListConfig();
        public PacketViewConfig      PacketView      { get; } = new PacketViewConfig();
        public PacketConverterConfig PacketConverter { get; } = new PacketConverterConfig();

        public EnumConfig<PacketDataRateTarget>  DataRateTarget { get; } = new EnumConfig<PacketDataRateTarget>(PacketDataRateTarget.RecvData);

        public IntegerConfig             SendPanelLogLimit   { get; } = new IntegerConfig(20);
        public EnumConfig<SendPanelType> SendPanelType       { get; } = new EnumConfig<SendPanelType>(UserConfigs.SendPanelType.Data);
        public StringListConfig          SendPanelTargetList { get; } = new StringListConfig();

        public StringListConfig SendPanel_Data_List    { get; } = new StringListConfig();
        public BoolConfig       SendPanel_Data_Preview { get; } = new BoolConfig(true);

        public StringListConfig SendPanel_File_List      { get; } = new StringListConfig();
        public IntegerConfig    SendPanel_File_BlockSize { get; } = new IntegerConfig(100);
        public IntegerConfig    SendPanel_File_SendDelay { get; } = new IntegerConfig(0);

        public StringListConfig            SendPanel_Log_List         { get; } = new StringListConfig();
        public EnumConfig<SendLogDataType> SendPanel_Log_PlayDataType { get; } = new EnumConfig<SendLogDataType>(SendLogDataType.RecvOnly);

        public SendDataListConfig SendDataList       { get; } = new SendDataListConfig();
        public StringConfig       SendDataListTarget { get; } = new StringConfig("*");
        public IntegerConfig      SendDataListLimit  { get; } = new IntegerConfig(200);
        public IntegerConfig      SendDataListRepeat { get; } = new IntegerConfig(1);

        public WatchDataListConfig WatchDataList { get; } = new WatchDataListConfig();

        public PacketListConfig PacketList       { get; } = new PacketListConfig();
        public IntegerConfig    PacketListLimit  { get; } = new IntegerConfig(2000);
        public IntegerConfig    PacketListRepeat { get; } = new IntegerConfig(1);

        public StringListConfig Script_OpenFileList { get; } = new StringListConfig();


        public UserConfig() : base("user")
        {
            SendPanelTargetList.Value.Add("*");
            SendPanelTargetList.Value.Add("GATE_001");
            SendPanelTargetList.Value.Add("GATE_002");
            SendPanelTargetList.Value.Add("GATE_003");
            SendPanelTargetList.Value.Add("GATE_004");
            SendPanelTargetList.Value.Add("GATE_005");
        }

        public override string ToString()
        {
            return (ProfileName.Value);
        }

        public bool Load(string path)
        {
            /* プロファイルを読み込み */
            return (LoadConfig(Path.Combine(path, CONFIG_FILE_NAME)));
        }

        public bool Save(string path)
        {
            if (path == null)return (false);

            /* バージョン情報を更新 */
            VersionMajor.Value = Program.Version.VersionMajor;
            VersionMinor.Value = Program.Version.VersionMinor;
            VersionBugfix.Value = Program.Version.VersionBugfix;
            VersionModel.Value = Program.Version.VersionModel;

            /* プロファイルを保存 */
            return (SaveConfig(Path.Combine(path, CONFIG_FILE_NAME)));
        }
    }
}
