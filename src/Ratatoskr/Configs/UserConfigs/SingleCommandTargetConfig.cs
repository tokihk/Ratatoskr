using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Generic;
using Ratatoskr.Configs;

namespace Ratatoskr.Configs.UserConfigs
{
    [Serializable]
    internal sealed class SingleCommandTargetObjectConfig
    {
        public string Alias { get; set; } = "";


        public SingleCommandTargetObjectConfig(string alias)
        {
            Alias = alias;
        }

        public SingleCommandTargetObjectConfig() { }
    }

    [Serializable]
    internal sealed class SingleCommandTargetConfig : IConfigDataReadOnly<List<SingleCommandTargetObjectConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<SingleCommandTargetObjectConfig> Value { get; } = new List<SingleCommandTargetObjectConfig>();


        public SingleCommandTargetConfig()
        {
            Value.Add(new SingleCommandTargetObjectConfig("*"));
            Value.Add(new SingleCommandTargetObjectConfig("GATE_001"));
            Value.Add(new SingleCommandTargetObjectConfig("GATE_002"));
            Value.Add(new SingleCommandTargetObjectConfig("GATE_003"));
            Value.Add(new SingleCommandTargetObjectConfig("GATE_004"));
            Value.Add(new SingleCommandTargetObjectConfig("GATE_005"));
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

            var newobj = new SingleCommandTargetObjectConfig();

            /* === パラメータ読み込み === */
            /* command */
            newobj.Alias = XmlUtil.GetAttribute(xml_node, "alias", "");

            /* === 設定リストへ追加 === */
            Value.Add(newobj);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            foreach (var info in Value) {
                var xml_data = xml_own.OwnerDocument.CreateElement(XML_NODE_DATA);

                /* === パラメータ書き込み === */
                /* command */
                xml_data.SetAttribute("alias", info.Alias);

                /* === ノード追加 === */
                xml_own.AppendChild(xml_data);
            }

            return (true);
        }
    }
}
