using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Configs;

namespace Ratatoskr.PacketViews.Protocol.Configs
{
    internal enum FrameListColumnType
    {
        Alias,
        Channel,
        BlockTime_UTC,
        BlockTime_Local,
        BlockIndex,
        DateTime_UTC,
        DateTime_Local,
        Outline,
        Outline_Custom,
    }

    internal sealed class FrameListColumnHeaderConfig
    {
        public FrameListColumnType Type  { get; set; } = FrameListColumnType.Alias;
        public uint                Width { get; set; } = 0;


        public FrameListColumnHeaderConfig(FrameListColumnType type, uint width)
        {
            Type = type;
            Width = width;
        }

        public FrameListColumnHeaderConfig(FrameListColumnType type)
        {
            Type = type;
            Width = GetInitWidth(type);
        }

        public FrameListColumnHeaderConfig()
        {
        }

        public static uint GetInitWidth(FrameListColumnType type)
        {
            switch (type) {
                case FrameListColumnType.Alias:           return (80);
                case FrameListColumnType.Channel:         return (80);
                case FrameListColumnType.BlockTime_UTC:   return (150);
                case FrameListColumnType.BlockTime_Local: return (150);
                case FrameListColumnType.BlockIndex:      return (80);
                case FrameListColumnType.DateTime_UTC:    return (150);
                case FrameListColumnType.DateTime_Local:  return (150);
                case FrameListColumnType.Outline:         return (180);
                case FrameListColumnType.Outline_Custom:  return (320);
                default:                                  return (150);
            }
        }
    }

    internal sealed class FrameListColumnListConfig : IConfigDataReadOnly<List<FrameListColumnHeaderConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<FrameListColumnHeaderConfig> Value { get; } = new List<FrameListColumnHeaderConfig>();


        public FrameListColumnListConfig()
        {
            Value.Add(new FrameListColumnHeaderConfig(FrameListColumnType.Alias));
            Value.Add(new FrameListColumnHeaderConfig(FrameListColumnType.Channel));
            Value.Add(new FrameListColumnHeaderConfig(FrameListColumnType.BlockTime_Local));
            Value.Add(new FrameListColumnHeaderConfig(FrameListColumnType.BlockIndex));
            Value.Add(new FrameListColumnHeaderConfig(FrameListColumnType.Outline));
            Value.Add(new FrameListColumnHeaderConfig(FrameListColumnType.Outline_Custom));
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

            var newobj = new FrameListColumnHeaderConfig();

            /* パラメータ読み込み */
            newobj.Type = (FrameListColumnType)Enum.Parse(typeof(FrameListColumnType), xml_node.GetAttribute("type"));
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
