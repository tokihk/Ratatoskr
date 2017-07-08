using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ratatoskr.Configs.Types
{
    [Serializable]
    public sealed class BoolConfig : IConfigData<bool>, IConfigReader, IConfigWriter
    {
        public bool Value { get; set; } = false;


        public BoolConfig(bool value)
        {
            Value = value;
        }

        public bool LoadConfigData(XmlElement xml_own)
        {
            Value = bool.Parse(xml_own.InnerText);

            return (true);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            xml_own.InnerText = Value.ToString();

            return (true);
        }
    }
}
