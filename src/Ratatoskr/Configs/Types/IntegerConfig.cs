using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ratatoskr.Configs.Types
{
    [Serializable]
    public sealed class IntegerConfig : IConfigData<decimal>, IConfigReader, IConfigWriter
    {
        public decimal Value { get; set; } = 0;


        public IntegerConfig(decimal value)
        {
            Value = value;
        }

        public bool LoadConfigData(XmlElement xml_own)
        {
            Value = decimal.Parse(xml_own.InnerText);

            return (true);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            xml_own.InnerText = Value.ToString();

            return (true);
        }
    }
}
