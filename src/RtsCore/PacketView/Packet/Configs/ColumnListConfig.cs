using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RtsCore.Config;
using Ratatoskr.Configs;

namespace Ratatoskr.PacketViews.Packet.Configs
{
    internal sealed class ColumnHeaderConfig
    {
        public static uint GetDefaultWidth(ColumnType type)
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
                case ColumnType.DataPreviewBinaryWithoutDivider:  return (320);
                case ColumnType.DataPreviewText:        return (180);
                case ColumnType.DataPreviewCustom:      return (320);
                default:                                return (150);
            }
        }

        private static string GetDefaultDisplayText(ColumnType type)
        {
            switch (type) {
                case ColumnType.Class:              return (ConfigManager.Language.PacketView.Packet.Column_Class.Value);
                case ColumnType.Alias:              return (ConfigManager.Language.PacketView.Packet.Column_Alias.Value);
                case ColumnType.Datetime_UTC:       return (ConfigManager.Language.PacketView.Packet.Column_Datetime_UTC.Value);
                case ColumnType.Datetime_Local:     return (ConfigManager.Language.PacketView.Packet.Column_Datetime_Local.Value);
                case ColumnType.Information:        return (ConfigManager.Language.PacketView.Packet.Column_Information.Value);
                case ColumnType.Mark:               return (ConfigManager.Language.PacketView.Packet.Column_Mark.Value);
                case ColumnType.Source:             return (ConfigManager.Language.PacketView.Packet.Column_Source.Value);
                case ColumnType.Destination:        return (ConfigManager.Language.PacketView.Packet.Column_Destination.Value);
                case ColumnType.DataLength:         return (ConfigManager.Language.PacketView.Packet.Column_DataLength.Value);
                case ColumnType.DataPreviewBinary:  return (ConfigManager.Language.PacketView.Packet.Column_DataPreviewBinary.Value);
                case ColumnType.DataPreviewBinaryWithoutDivider:  return (ConfigManager.Language.PacketView.Packet.Column_DataPreviewBinaryWithoutDivider.Value);
                case ColumnType.DataPreviewText:    return (ConfigManager.Language.PacketView.Packet.Column_DataPreviewText.Value);
                case ColumnType.DataPreviewCustom:  return (ConfigManager.Language.PacketView.Packet.Column_DataPreviewCustom.Value);
                default:                            return (type.ToString());
            }
        }


        public ColumnType Type         { get; set; } = ColumnType.Alias;
        public string     Text         { get; set; } = "";
        public uint       Width        { get; set; } = 0;
        public string     PacketFilter { get; set; } = "";


        public ColumnHeaderConfig(ColumnHeaderConfig obj)
        {
            Type = obj.Type;
            Text = obj.Text;
            Width = obj.Width;
            PacketFilter = obj.PacketFilter;
        }

        public ColumnHeaderConfig(ColumnType type, uint width)
        {
            Type = type;
            Text = GetDefaultDisplayText(type);
            Width = width;
        }

        public ColumnHeaderConfig(ColumnType type)
        {
            Type = type;
            Text = GetDefaultDisplayText(type);
            Width = GetDefaultWidth(type);
        }

        public ColumnHeaderConfig()
        {
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

            newobj.Width = (xml_node.HasAttribute("width"))
                         ? (uint.Parse(xml_node.GetAttribute("width")))
                         : (ColumnHeaderConfig.GetDefaultWidth(newobj.Type));

            newobj.Text = (xml_node.HasAttribute("text"))
                        ? (xml_node.GetAttribute("text"))
                        : (newobj.Type.ToString());

            newobj.PacketFilter = (xml_node.HasAttribute("packet-filter"))
                                ? (xml_node.GetAttribute("packet-filter"))
                                : ("");

            /* 設定リストへ追加 */
            Value.Add(newobj);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            foreach (var info in Value) {
                var xml_data = xml_own.OwnerDocument.CreateElement(XML_NODE_DATA);

                xml_data.SetAttribute("type", info.Type.ToString());
                xml_data.SetAttribute("width", info.Width.ToString());
                xml_data.SetAttribute("text", info.Text);
                xml_data.SetAttribute("packet-filter", info.PacketFilter);

                xml_own.AppendChild(xml_data);
            }

            return (true);
        }
    }
}
