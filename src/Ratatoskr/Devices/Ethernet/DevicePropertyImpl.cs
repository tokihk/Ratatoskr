using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.Devices.Ethernet
{
    [Serializable]
    internal sealed class DevicePropertyImpl : DeviceProperty
    {
        public StringConfig                    Interface           { get; } = new StringConfig("");
        public BoolConfig                      SendEnable          { get; } = new BoolConfig(false);
        public StringConfig                    Filter              { get; } = new StringConfig("");
        public EnumConfig<SourceInfoType>      ViewSourceType      { get; } = new EnumConfig<SourceInfoType>(SourceInfoType.IpAddress);
        public EnumConfig<DestinationInfoType> ViewDestinationType { get; } = new EnumConfig<DestinationInfoType>(DestinationInfoType.IpAddress);
        public EnumConfig<DataContentsType>    ViewDataType        { get; } = new EnumConfig<DataContentsType>(DataContentsType.Raw);


        public override DeviceProperty Clone()
        {
            return (ClassUtil.Clone<DevicePropertyImpl>(this));
        }

        public override string GetStatusString()
        {
            var str = new StringBuilder();

            str.AppendLine(Interface.Value);
            str.AppendFormat("Filter: {0}\n", ViewDataType.Value.ToString());

            return (str.ToString());
        }

        public override DevicePropertyEditor GetPropertyEditor()
        {
            return (new DevicePropertyEditorImpl(this));
        }
    }
}
