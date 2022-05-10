﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config.Types;
using Ratatoskr.Device;
using Ratatoskr.General;
using Ratatoskr.General.Pcap;

namespace Ratatoskr.Device.Ethernet
{
	public enum FrameProtocolItemType
	{
		Ethernet,
		IEEE8021Q,
		IPv4,
		IPv6,
		TCP,
		UDP,
		Payload,
	}

    [Serializable]
    internal sealed class DevicePropertyImpl : DeviceProperty
    {
        public StringConfig                    Interface           { get; } = new StringConfig("");
        public StringConfig                    Filter              { get; } = new StringConfig("");

        public EnumConfig<PcapPacketInfoType>		 PacketInfo        { get; } = new EnumConfig<PcapPacketInfoType>(PcapPacketInfoType.TopProtocolName);
        public EnumConfig<PcapPacketSourceType>      PacketSource      { get; } = new EnumConfig<PcapPacketSourceType>(PcapPacketSourceType.IpAddress);
        public EnumConfig<PcapPacketDestinationType> PacketDestination { get; } = new EnumConfig<PcapPacketDestinationType>(PcapPacketDestinationType.IpAddress);
        public EnumConfig<PcapPacketDataType>		 PacketData        { get; } = new EnumConfig<PcapPacketDataType>(PcapPacketDataType.TopProtocolDataUnit);

        public override DeviceProperty Clone()
        {
            return (ClassUtil.Clone<DevicePropertyImpl>(this));
        }

        public override string GetStatusString()
        {
            var str = new StringBuilder();

            str.AppendLine(Interface.Value);
            str.AppendFormat("Filter: {0}\n", PacketData.Value.ToString());

            return (str.ToString());
        }

        public override DevicePropertyEditor GetPropertyEditor()
        {
            return (new DevicePropertyEditorImpl(this));
        }
    }
}
