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

namespace Ratatoskr.Devices.TcpServer
{
	internal enum AddressFamilyType
	{
		IPv4,
		IPv6,
	}

    internal enum BindModeType
    {
		INADDR_ANY,
		SelectAddress,
    }

    [Serializable]
    internal sealed class DevicePropertyImpl : DeviceProperty
    {
        public EnumConfig<AddressFamilyType> AddressFamily { get; } = new EnumConfig<AddressFamilyType>(AddressFamilyType.IPv4);

        public EnumConfig<BindModeType> LocalBindMode { get; } = new EnumConfig<BindModeType>(BindModeType.INADDR_ANY);
		public IPAddressConfig			LocalIpAddress { get; } = new IPAddressConfig(IPAddress.None);
		public IntegerConfig			LocalPortNo    { get; } = new IntegerConfig(50000);

        public IntegerConfig        Capacity     { get; } = new IntegerConfig(10);

		public IntegerConfig		SendBufferSize { get; } = new IntegerConfig(8192);
		public IntegerConfig		RecvBufferSize { get; } = new IntegerConfig(8192);

		public BoolConfig           KeepAliveOnOff      { get; } = new BoolConfig(false);

		public BoolConfig			KeepAliveTime       { get; } = new BoolConfig(true);
		public IntegerConfig		KeepAliveTime_Value { get; } = new IntegerConfig(7200000);

		public BoolConfig			KeepAliveInterval       { get; } = new BoolConfig(true);
		public IntegerConfig        KeepAliveInterval_Value { get; } = new IntegerConfig(0);

		public BoolConfig			KeepAliveRetryCount       { get; } = new BoolConfig(true);
		public IntegerConfig		KeepAliveRetryCount_Value { get; } = new IntegerConfig(10);

		public BoolConfig			TTL_Unicast       { get; } = new BoolConfig(false);
		public IntegerConfig		TTL_Unicast_Value { get; } = new IntegerConfig(0);

		public BoolConfig			ReuseAddress { get; } = new BoolConfig(false);


        public override DeviceProperty Clone()
        {
            return (ClassUtil.Clone<DevicePropertyImpl>(this));
        }

        public override DevicePropertyEditor GetPropertyEditor()
        {
            return (new DevicePropertyEditorImpl(this));
        }

        public override string GetStatusString()
        {
            return (String.Format(
                "{0:G}",
                LocalPortNo.Value));
        }
    }
}
