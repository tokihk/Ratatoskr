using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WeifenLuo.WinFormsUI.Docking;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;
using Ratatoskr.Forms;

namespace Ratatoskr.Configs.SystemConfigs
{
    internal enum AutoPacketSaveFormatType
    {
        Ratatoskr,
        CSV,
        Binary,
    }

    internal enum AutoPacketSaveTargetType
    {
        DevicePacket,
        ViewPacket,
    }

    internal enum AutoPacketSaveTimmingType
    {
        NoSave,         // 保存無し
        Interval,       // 時間間隔[分単位] (例: 10分毎)
        FileSize,       // ファイルサイズ[kbyte単位]
        PacketCount,    // パケット数
    }

    [Serializable]
    internal sealed class AutoPacketSaveConfig : ConfigHolder
    {
        public StringConfig                          SaveDirectory         { get; } = new StringConfig("");
        public StringConfig                          SavePrefix            { get; } = new StringConfig("autosave");
        public EnumConfig<AutoPacketSaveFormatType>  SaveFormat            { get; } = new EnumConfig<AutoPacketSaveFormatType>(AutoPacketSaveFormatType.Ratatoskr);
        public EnumConfig<AutoPacketSaveTargetType>  SaveTarget            { get; } = new EnumConfig<AutoPacketSaveTargetType>(AutoPacketSaveTargetType.DevicePacket);
        public EnumConfig<AutoPacketSaveTimmingType> SaveTimming           { get; } = new EnumConfig<AutoPacketSaveTimmingType>(AutoPacketSaveTimmingType.NoSave);
        public IntegerConfig                         SaveValue_Interval    { get; } = new IntegerConfig(10);
        public IntegerConfig                         SaveValue_FileSize    { get; } = new IntegerConfig(500);
        public IntegerConfig                         SaveValue_PacketCount { get; } = new IntegerConfig(2000);
    }
}
