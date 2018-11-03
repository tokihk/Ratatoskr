using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RtsCore.Config.Types
{
    [Serializable]
    public sealed class DateTimeConfig : ConfigObject, IConfigData<DateTime>, IConfigReader, IConfigWriter
    {
        public DateTime Value { get; set; } = DateTime.MinValue;


        public DateTimeConfig(DateTime dt)
        {
            Value = dt;
        }

        public bool LoadConfigData(XmlElement xml_own)
        {
            Value = DateTime.ParseExact(xml_own.InnerText, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);

            return (true);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            xml_own.InnerText = Value.ToString("yyyy-MM-dd HH:mm:ss.fff");

            return (true);
        }
    }
}
