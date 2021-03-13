using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Config.Types;
using RtsCore.Framework.Device;
using RtsCore.Generic;

namespace Ratatoskr.Devices.UdpClient
{
	internal enum AddressFamilyType
	{
		IPv4,
		IPv6,
	}

    internal enum BindModeType
    {
		NotBind,
		INADDR_ANY,
		SelectAddress,
    }

	internal enum AddressType
	{
		Unicast,
		Broadcast,
		Multicast,
	}

    [Serializable]
    internal sealed class DevicePropertyImpl : DeviceProperty
    {
        public EnumConfig<AddressFamilyType> AddressFamily { get; } = new EnumConfig<AddressFamilyType>(AddressFamilyType.IPv4);

        public EnumConfig<BindModeType> LocalBindMode { get; } = new EnumConfig<BindModeType>(BindModeType.NotBind);

        public IPAddressConfig          LocalIpAddress { get; } = new IPAddressConfig(IPAddress.None);
        public IntegerConfig            LocalPortNo  { get; } = new IntegerConfig(1024);

		public EnumConfig<AddressType>   RemoteAddressType { get; } = new EnumConfig<AddressType>(AddressType.Unicast);
		public StringConfig				 RemoteAddress     { get; } = new StringConfig("localhost");
		public IPAddressConfig			 RemoteIpAddress   { get; } = new IPAddressConfig(IPAddress.None);
		public IntegerConfig			 RemotePortNo      { get; } = new IntegerConfig(1024);

		public BoolConfig			Unicast_TTL       { get; } = new BoolConfig(false);
		public IntegerConfig		Unicast_TTL_Value { get; } = new IntegerConfig(128);

		public BoolConfig			Multicast_TTL		{ get; } = new BoolConfig(false);
		public IntegerConfig		Multicast_TTL_Value { get; } = new IntegerConfig(1);

		public BoolConfig			Multicast_Loopback { get; } = new BoolConfig(false);

		public BoolConfig			Multicast_GroupAddress     { get; } = new BoolConfig(false);
		public StringListConfig		Multicast_GroupAddressList { get; } = new StringListConfig();

		public BoolConfig			Multicast_Interface       { get; } = new BoolConfig(false);
		public StringConfig			Multicast_Interface_Value { get; } = new StringConfig("");


		public override DeviceProperty Clone()
        {
            return (ClassUtil.Clone<DevicePropertyImpl>(this));
        }

        public override string GetStatusString()
        {
            return (String.Format(
                "Local {0:G}:{1:G}\nRemote {2:G}:{3:G}",
                LocalIpAddress.Value,
                LocalPortNo.Value,
                RemoteIpAddress.Value,
                RemotePortNo.Value));
        }

        public override DevicePropertyEditor GetPropertyEditor()
        {
            return (new DevicePropertyEditorImpl(this));
        }
    }
}
