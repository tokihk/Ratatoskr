using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Configs;

namespace Ratatoskr.PacketViews.Packet.Configs
{
    internal sealed class ColumnHeaderConfig
    {
        public ColumnType Type  { get; set; } = ColumnType.Alias;
        public uint       Width { get; set; } = 0;


        public ColumnHeaderConfig(ColumnType type, uint width)
        {
            Type = type;
            Width = width;
        }

        public ColumnHeaderConfig(ColumnType type)
        {
            Type = type;
            Width = GetInitWidth(type);
        }

        public ColumnHeaderConfig()
        {
        }

        public static uint GetInitWidth(ColumnType type)
        {
            switch (type) {
                case ColumnType.Class:                  return (80);
                case ColumnType.Alias:                  return (80);
                case ColumnType.Datetime_UTC:           return (150);
                case ColumnType.Datetime_Local:         return (150);
                case ColumnType.Information:            return (140);
                case ColumnType.Source:                 return (100);
                case ColumnType.Destination:            return (100);
                case ColumnType.DataLength:             return (70);
                case ColumnType.DataPreviewBinary:      return (320);
                case ColumnType.DataPreviewText:        return (180);
                case ColumnType.DataPreviewCustom:      return (320);
                default:                                return (150);
            }
        }
    }

    internal sealed class ColumnListConfig : IConfigDataReadOnly<List<ColumnHeaderConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<ColumnHeaderConfig> Value { get; } = new List<ColumnHeaderConfig>();


        public ColumnListConfig()
        {
            Value.Add(new ColumnHeaderConfig(ColumnType.Class));
            Value.Add(new ColumnHeaderConfig(ColumnType.Alias));
            Value.Add(new ColumnHeaderConfig(ColumnType.Datetime_Local));
            Value.Add(new ColumnHeaderConfig(ColumnType.Information));
            Value.Add(new ColumnHeaderConfig(ColumnType.DataLength));
            Value.Add(new ColumnHeaderConfig(ColumnType.DataPreviewBinary));
            Value.Add(new ColumnHeaderConfig(ColumnType.DataPreviewText));
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

            var newobj = new ColumnHeaderConfig();

            /* パラメータ読み込み */
            newobj.Type = (ColumnType)Enum.Parse(typeof(ColumnType), xml_node.GetAttribute("type"));
            newobj.Width = uint.Parse(xml_node.GetAttribute("width"));

            /* 設定リストへ追加 */
            Value.Add(newobj);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            foreach (var info in Value) {
                var xml_data = xml_own.OwnerDocument.CreateElement(XML_NODE_DATA);

                xml_data.SetAttribute("type", info.Type.ToString());
                xml_data.SetAttribute("width", info.Width.ToString());

                xml_own.AppendChild(xml_data);
            }

            return (true);
        }
    }
}
