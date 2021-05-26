using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Config;
using Ratatoskr.Config.Types;

namespace Ratatoskr.Config.Data.System
{
    public sealed class ApplicationCoreConfig : ConfigHolder
    {
        public IntegerConfig AppTimerInterval { get; } = new IntegerConfig(10);

        public BoolConfig NewVersionAutoUpdate { get; } = new BoolConfig(false);

		public IntegerConfig GateNum { get; } = new IntegerConfig(5);

        public IntegerConfig RawPacketCountLimit         { get; } = new IntegerConfig(999999);

        public IntegerConfig Packet_ViewPacketCountLimit     { get; } = new IntegerConfig(999999);

        public BoolConfig    Sequential_WinApiMode               { get; } = new BoolConfig(true);
        public BoolConfig    Sequential_ViewCharCountLimitEnable { get; } = new BoolConfig(true);
        public IntegerConfig Sequential_ViewCharCountLimit       { get; } = new IntegerConfig(9999999);
        public BoolConfig    Sequential_LineNoVisible            { get; } = new BoolConfig(true);

        public IntegerConfig AutoHighSpeedDrawLimit { get; } = new IntegerConfig(999999);

        public IntegerConfig PacketDrawIntervalMin  { get; } = new IntegerConfig(10);
        public IntegerConfig PacketDrawIntervalMax  { get; } = new IntegerConfig(600);
        public IntegerConfig PacketDrawIntervalStep { get; } = new IntegerConfig(50);
    }
}
