using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Config;

namespace Ratatoskr.Config.Types
{
    [Serializable]
    public sealed class ColumnListConfig<TKey> : ConfigObject, IConfigDataReadOnly<List<KeyValuePair<TKey, int>>>, IConfigReader, IConfigWriter
        where TKey   : struct
    {
        private const string XML_NODE_DATA = "data";


        public List<KeyValuePair<TKey, int>> Value { get; } = new List<KeyValuePair<TKey, int>>();


        public ColumnListConfig() { }

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
				var name  = (TKey)Enum.Parse(typeof(TKey), GetAttribute(xml_node, "name", ""));
				var width = int.Parse(GetAttribute(xml_node, "width", ""));

				Value.Add(new KeyValuePair<TKey, int>(name, width));

			} catch {
			}
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            foreach (var info in Value) {
                var xml_data = xml_own.OwnerDocument.CreateElement(XML_NODE_DATA);

                /* === パラメータ書き込み === */
                /* name */
                xml_data.SetAttribute("name", info.Key.ToString());

                /* width */
                xml_data.SetAttribute("width", info.Value.ToString());

                /* === ノード追加 === */
                xml_own.AppendChild(xml_data);
            }

            return (true);
        }
    }
}
