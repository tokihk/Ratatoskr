using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ratatoskr.Config.Types
{
    [Serializable]
    public sealed class IPAddressConfig : ConfigObject, IConfigData<IPAddress>, IConfigReader, IConfigWriter
    {
        public IPAddress Value { get; set; } = IPAddress.None;


        public IPAddressConfig(IPAddress ipaddr)
        {
            Value = ipaddr;
        }

        public bool LoadConfigData(XmlElement xml_own)
        {
            IPAddress ipaddr;

            if (!IPAddress.TryParse(xml_own.InnerText, out ipaddr)) {
                return (false);
            }

            Value = ipaddr;
                
            return (true);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            xml_own.InnerText = Value.ToString();

            return (true);
        }
    }
}
