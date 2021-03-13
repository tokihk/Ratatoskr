using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ratatoskr.Config.Types
{
    [Serializable]
    public sealed class GuidConfig : ConfigObject, IConfigData<Guid>, IConfigReader, IConfigWriter
    {
        public Guid Value { get; set; } = Guid.Empty;


        public GuidConfig(Guid value)
        {
            Value = value;
        }

        public bool LoadConfigData(XmlElement xml_own)
        {
			Guid value;

			if (Guid.TryParse(xml_own.InnerText, out value)) {
				Value = value;
			}

            return (true);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            xml_own.InnerText = Value.ToString();

            return (true);
        }
    }
}
