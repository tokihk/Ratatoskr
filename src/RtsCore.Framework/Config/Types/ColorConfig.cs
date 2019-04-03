using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RtsCore.Config;

namespace RtsCore.Framework.Config.Types
{
    [Serializable]
    public sealed class ColorConfig : ConfigObject, IConfigData<Color>, IConfigReader, IConfigWriter
    {
        public Color Value { get; set; } = Color.Black;


        public ColorConfig(Color color)
        {
            Value = color;
        }

        public bool LoadConfigData(XmlElement xml_own)
        {
            Value = ColorTranslator.FromHtml(xml_own.GetAttribute("argb"));

            return (true);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            xml_own.SetAttribute("argb", ColorTranslator.ToHtml(Value));

            return (true);
        }
    }
}
