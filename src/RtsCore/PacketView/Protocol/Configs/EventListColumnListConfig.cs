using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RtsCore.Config;

namespace Ratatoskr.PacketViews.Protocol.Configs
{
    internal enum EventListColumnType
    {
		Alias,
		Channel,
        BlockTime_UTC,
        BlockTime_Local,
        EventTime_UTC,
        EventTime_Local,
		EventType,
        Information,
    }

    internal sealed class EventListColumnHeaderConfig
    {
        public EventListColumnType Type  { get; set; }
        public uint                Width { get; set; }


        public EventListColumnHeaderConfig(EventListColumnType type, uint width)
        {
            Type = type;
            Width = width;
        }

        public EventListColumnHeaderConfig(EventListColumnType type)
        {
            Type = type;
            Width = GetInitWidth(type);
        }

        public EventListColumnHeaderConfig()
        {
        }

        public static uint GetInitWidth(EventListColumnType type)
        {
            switch (type) {
				case EventListColumnType.Alias:           return (80);
				case EventListColumnType.Channel:         return (80);
                case EventListColumnType.BlockTime_UTC:   return (150);
                case EventListColumnType.BlockTime_Local: return (150);
                case EventListColumnType.EventTime_UTC:   return (150);
                case EventListColumnType.EventTime_Local: return (150);
                case EventListColumnType.EventType:       return (100);
                case EventListColumnType.Information:     return (320);
                default:                                  return (150);
            }
        }
    }

    internal sealed class EventListColumnListConfig : IConfigDataReadOnly<List<EventListColumnHeaderConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<EventListColumnHeaderConfig> Value { get; } = new List<EventListColumnHeaderConfig>();


        public EventListColumnListConfig()
        {
			Value.Add(new EventListColumnHeaderConfig(EventListColumnType.Alias));
			Value.Add(new EventListColumnHeaderConfig(EventListColumnType.Channel));
            Value.Add(new EventListColumnHeaderConfig(EventListColumnType.BlockTime_Local));
			Value.Add(new EventListColumnHeaderConfig(EventListColumnType.EventTime_Local));
			Value.Add(new EventListColumnHeaderConfig(EventListColumnType.EventType));
            Value.Add(new EventListColumnHeaderConfig(EventListColumnType.Information));
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

			try {
				var newobj = new EventListColumnHeaderConfig();

				/* パラメータ読み込み */
				newobj.Type = (EventListColumnType)Enum.Parse(typeof(EventListColumnType), xml_node.GetAttribute("type"));
				newobj.Width = uint.Parse(xml_node.GetAttribute("width"));

				/* 設定リストへ追加 */
				Value.Add(newobj);

			} catch {
			}
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
