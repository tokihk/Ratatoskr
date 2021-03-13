using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ratatoskr.Config.Types
{
    [Serializable]
    public class EnumConfig<T> : ConfigObject, IConfigData<T>, IConfigReader, IConfigWriter
        where T : struct
    {
        public T Value { get; set; }


        public EnumConfig(T value)
        {
            Value = value;
        }

        public bool LoadConfigData(XmlElement xml_own)
        {
            if (!Enum.TryParse<T>(xml_own.InnerText, out T value))return (false);

            Value = value;

            return (true);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            xml_own.InnerText = Enum.GetName(typeof(T), Value);

            return (true);
        }
    }
}
