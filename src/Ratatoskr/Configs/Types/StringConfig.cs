using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ratatoskr.Configs.Types
{
    [Serializable]
    public sealed class StringConfig : IConfigData<string>, IConfigReader, IConfigWriter
    {
        public string Value { get; set; } = "";


        public StringConfig(string value)
        {
            Value = value;
        }

        public bool LoadConfigData(XmlElement xml_own)
        {
            Value = xml_own.InnerText;

            return (true);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            xml_own.InnerText = Value;

            return (true);
        }
    }
}
