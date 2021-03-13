using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.General;
using Ratatoskr.Config;

namespace Ratatoskr.Config.Types
{
    [Serializable]
    public sealed class StringListConfig : ConfigObject, IConfigDataReadOnly<List<string>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public StringListConfig() { }

        public StringListConfig(IEnumerable<string> values)
        {
            Value.AddRange(values);
        }

        public List<string> Value { get; } = new List<string>();

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

            /* === 設定リストへ追加 === */
            Value.Add(GetAttribute(xml_node, "value", ""));
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            foreach (var info in Value) {
                var xml_data = xml_own.OwnerDocument.CreateElement(XML_NODE_DATA);

                /* === パラメータ書き込み === */
                /* command */
                xml_data.SetAttribute("value", info);

                /* === ノード追加 === */
                xml_own.AppendChild(xml_data);
            }

            return (true);
        }
    }
}
