using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RtsCore.Config.Types
{
    [Serializable]
    public sealed class BoolConfig : ConfigObject, IConfigData<bool>, IConfigReader, IConfigWriter
    {
        public bool Value { get; set; } = false;


        public BoolConfig(bool value)
        {
            Value = value;
        }

        public bool LoadConfigData(XmlElement xml_own)
        {
            try {
                Value = bool.Parse(xml_own.InnerText);

                return (true);

            } catch {
                return (false);
            }
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            xml_own.InnerText = Value.ToString();

            return (true);
        }
    }
}
