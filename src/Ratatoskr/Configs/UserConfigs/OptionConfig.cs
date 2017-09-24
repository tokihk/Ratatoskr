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
    [Flags]
    internal enum AutoTimeStampTriggerType
    {
        LastRecvPeriod = 1 << 0,
    }

    internal enum AutoSaveFormatType
    {
        Ratatoskr,
        CSV,
        Binary,
    }

    internal enum AutoSaveTimmingType
    {
        NoSave,         // 保存無し
        Interval,       // 時間間隔[分単位] (例: 10分毎)
        FileSize,       // ファイルサイズ[kbyte単位]
        PacketCount,    // パケット数
    }

    [Serializable]
    internal sealed class OptionConfig : ConfigHolder
    {
        public ShortcutKeyConfig ShortcutKey { get; } = new ShortcutKeyConfig();

        public BoolConfig NewVersionAutoUpdate { get; } = new BoolConfig(false);

        public BoolConfig AutoScroll { get; } = new BoolConfig(true);

        public BoolConfig                           AutoTimeStamp                     { get; } = new BoolConfig(false);
        public EnumConfig<AutoTimeStampTriggerType> AutoTimeStampTrigger              { get; } = new EnumConfig<AutoTimeStampTriggerType>(AutoTimeStampTriggerType.LastRecvPeriod);
        public IntegerConfig                        AutoTimeStampValue_LastRecvPeriod { get; } = new IntegerConfig(1000);

        public StringConfig                    AutoSaveDirectory         { get; } = new StringConfig("");
        public StringConfig                    AutoSavePrefix            { get; } = new StringConfig("autosave");
        public EnumConfig<AutoSaveFormatType>  AutoSaveFormat            { get; } = new EnumConfig<AutoSaveFormatType>(AutoSaveFormatType.Ratatoskr);
        public EnumConfig<AutoSaveTimmingType> AutoSaveTimming           { get; } = new EnumConfig<AutoSaveTimmingType>(AutoSaveTimmingType.NoSave);
        public IntegerConfig                   AutoSaveValue_Interval    { get; } = new IntegerConfig(10);
        public IntegerConfig                   AutoSaveValue_FileSize    { get; } = new IntegerConfig(500);
        public IntegerConfig                   AutoSaveValue_PacketCount { get; } = new IntegerConfig(2000);

        private StringListConfig CustomConvertAlgorithm { get; } = new StringListConfig();

//        public StringConfig ExtPath_USBPcapCMD { get; } = new StringConfig("C:\\Program Files\\USBPcap\\USBPcapCMD.exe");
    }
}
