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
    public sealed class PointConfig : IConfigData<Point>, IConfigReader, IConfigWriter
    {
        public Point Value { get; set; } = Point.Empty;


        public PointConfig(int x, int y)
        {
            Value = new Point(x, y);
        }

        public bool LoadConfigData(XmlElement xml_own)
        {
            var pos = Value;

            pos.X = int.Parse(xml_own.GetAttribute("x"));
            pos.Y = int.Parse(xml_own.GetAttribute("y"));

            Value = pos;

            return (true);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            xml_own.SetAttribute("x", Value.X.ToString());
            xml_own.SetAttribute("y", Value.Y.ToString());

            return (true);
        }
    }
}
