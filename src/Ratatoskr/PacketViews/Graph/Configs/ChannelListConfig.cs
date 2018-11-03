using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RtsCore.Config;

namespace Ratatoskr.PacketViews.Graph.Configs
{
    internal sealed class ChannelConfig
    {
        public Color   ForeColor     { get; set; } = Color.Black;
        public decimal Magnification { get; set; } = 1;
        public decimal Offset        { get; set; } = 0;


        public ChannelConfig(Color fore_color, decimal mag, decimal offset)
        {
            ForeColor = fore_color;
            Magnification = mag;
            Offset = offset;
        }

        public ChannelConfig()
        {
        }
    }

    internal sealed class ChannelListConfig : IConfigDataReadOnly<List<ChannelConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<ChannelConfig> Value { get; } = new List<ChannelConfig>();


        public ChannelListConfig()
        {
            Value.Add(new ChannelConfig(Color.LightGoldenrodYellow, 1, 0));
            Value.Add(new ChannelConfig(Color.LightBlue, 1, 0));
            Value.Add(new ChannelConfig(Color.LightPink, 1, 0));
            Value.Add(new ChannelConfig(Color.LightGreen, 1, 0));
            Value.Add(new ChannelConfig(Color.LightSalmon, 1, 0));
        }

        public bool LoadConfigData(XmlElement xml_own)
        {
            /* 現在のリストをクリア */
            Value.Clear();

            /* 新しい設定を読み込む */
            foreach (XmlNode xml_node in xml_own.ChildNodes) {
                if (xml_node.NodeType != XmlNodeType.Element)continue;
                if (xml_node.Name != XML_NODE_DATA)continue;

                LoadConfigPart_Data(xml_node as XmlElement);
            }

            return (true);
        }

        private void LoadConfigPart_Data(XmlElement xml_node)
        {
            if (xml_node == null)return;

            var newobj = new ChannelConfig();

            /* パラメータ読み込み */

            /* fore-color */
            newobj.ForeColor = ColorTranslator.FromHtml(xml_node.GetAttribute("fore-color"));

            /* mag */
            newobj.Magnification = decimal.Parse(xml_node.GetAttribute("mag"));

            /* offset */
            newobj.Offset = decimal.Parse(xml_node.GetAttribute("offset"));

            /* 設定リストへ追加 */
            Value.Add(newobj);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            foreach (var info in Value) {
                var xml_data = xml_own.OwnerDocument.CreateElement(XML_NODE_DATA);

                /* fore-color */
                xml_data.SetAttribute("fore-color", ColorTranslator.ToHtml(info.ForeColor));

                /* mag */
                xml_data.SetAttribute("mag", info.Magnification.ToString());

                /* offset */
                xml_data.SetAttribute("offset", info.Offset.ToString());

                xml_own.AppendChild(xml_data);
            }

            return (true);
        }
    }
}
