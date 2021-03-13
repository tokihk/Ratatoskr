using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ratatoskr.Config
{
    public interface IConfigReader
    {
        bool LoadConfigData(XmlElement xml_own);
    }
}
