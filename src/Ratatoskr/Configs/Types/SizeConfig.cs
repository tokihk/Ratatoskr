using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ratatoskr.Configs.Types
{
    [Serializable]
    public sealed class SizeConfig : IConfigData<Size>, IConfigReader, IConfigWriter
    {
        public Size Value { get; set; } = Size.Empty;


        public SizeConfig(int width, int height)
        {
            Value = new Size(width, height);
        }

        public bool LoadConfigData(XmlElement xml_own)
        {
            var size = Value;

            size.Width = int.Parse(xml_own.GetAttribute("width"));
            size.Height = int.Parse(xml_own.GetAttribute("height"));

            Value = size;

            return (true);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            xml_own.SetAttribute("width", Value.Width.ToString());
            xml_own.SetAttribute("height", Value.Height.ToString());

            return (true);
        }
    }
}
