using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RtsCore.Config
{
    [Serializable]
    public abstract class ConfigObject
    {
        public static string GetAttribute(XmlElement node, string name, string value_def)
        {
            var attr = node.GetAttributeNode(name);

            if (attr != null) {
                return (attr.Value);
            } else {
                return (value_def);
            }
        }
    }
}
