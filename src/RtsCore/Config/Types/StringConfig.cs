using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RtsCore.Config.Types
{
    [Serializable]
    public sealed class StringConfig : ConfigObject, IConfigData<string>, IConfigReader, IConfigWriter
    {
        public string Value { get; set; } = "";


        public StringConfig(string value)
        {
            Value = value;
        }

        public bool LoadConfigData(XmlElement xml_own)
        {
            Value = xml_own.InnerText.TrimEnd('\r', '\n');

            return (true);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            xml_own.InnerText = Value.TrimEnd('\r', '\n');

            return (true);
        }
    }
}
