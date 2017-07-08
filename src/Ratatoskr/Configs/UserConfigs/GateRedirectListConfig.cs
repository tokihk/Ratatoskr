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
    internal sealed class GateRedirectConfig
    {
        public bool   Enable { get; set; } = false;
        public string Input  { get; set; } = "";
        public string Output { get; set; } = "";

        public GateRedirectConfig(bool enable, string input, string output)
        {
            Enable = enable;
            Input = input;
            Output = output;
        }

        public GateRedirectConfig() { }
    }

    [Serializable]
    internal sealed class GateRedirectListConfig : IConfigDataReadOnly<List<GateRedirectConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<GateRedirectConfig> Value { get; } = new List<GateRedirectConfig>();


        public GateRedirectListConfig()
        {
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

            var newobj = new GateRedirectConfig();

            /* === パラメータ読み込み === */
            /* enable */
            newobj.Enable = bool.Parse(XmlUtil.GetAttribute(xml_node, "enable", "false"));

            /* input */
            newobj.Input = XmlUtil.GetAttribute(xml_node, "input", "");

            /* output */
            newobj.Output = XmlUtil.GetAttribute(xml_node, "output", "");

            /* === 設定リストへ追加 === */
            Value.Add(newobj);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            foreach (var info in Value) {
                var xml_data = xml_own.OwnerDocument.CreateElement(XML_NODE_DATA);

                /* === パラメータ書き込み === */
                /* enable */
                xml_data.SetAttribute("enable", info.Enable.ToString());

                /* input */
                xml_data.SetAttribute("input", info.Input);

                /* output */
                xml_data.SetAttribute("output", info.Output);

                /* === ノード追加 === */
                xml_own.AppendChild(xml_data);
            }

            return (true);
        }
    }
}
