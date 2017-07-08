using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ratatoskr.Configs
{
    public interface IConfigWriter
    {
        bool SaveConfigData(XmlElement xml_own);
    }
}
