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
    internal sealed class SequentialCommandConfig
    {
        public bool   Enable      { get; set; } = true;
        public string Command     { get; set; } = "";
        public uint   DelayFixed  { get; set; } = 0;
        public uint   DelayRandom { get; set; } = 0;

        public SequentialCommandConfig(bool enable, string command, uint delay_fixed, uint delay_random)
        {
            Enable = enable;
            Command = command;
            DelayFixed = delay_fixed;
            DelayRandom = delay_random;
        }

        public SequentialCommandConfig() { }
    }

    [Serializable]
    internal sealed class SequentialCommandListConfig : IConfigDataReadOnly<List<SequentialCommandConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<SequentialCommandConfig> Value { get; } = new List<SequentialCommandConfig>();


        public SequentialCommandListConfig()
        {
            Value.Add(new SequentialCommandConfig(true, "02'Test Command 1'03", 2000, 0));
            Value.Add(new SequentialCommandConfig(true, "02'Test Command 2'03", 1000, 0));
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

            var newobj = new SequentialCommandConfig();

            /* === パラメータ読み込み === */
            /* command */
            newobj.Command = XmlUtil.GetAttribute(xml_node, "command", "");

            /* delay_fixed */
            newobj.DelayFixed = uint.Parse(XmlUtil.GetAttribute(xml_node, "delay_fixed", "0"));

            /* delay_random */
            newobj.DelayRandom = uint.Parse(XmlUtil.GetAttribute(xml_node, "delay_random", "0"));

            /* === 設定リストへ追加 === */
            Value.Add(newobj);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            foreach (var info in Value) {
                var xml_data = xml_own.OwnerDocument.CreateElement(XML_NODE_DATA);

                /* === パラメータ書き込み === */
                /* command */
                xml_data.SetAttribute("command", info.Command);

                /* delay_fixed */
                xml_data.SetAttribute("delay_fixed", info.DelayFixed.ToString());

                /* delay_random */
                xml_data.SetAttribute("delay_random", info.DelayRandom.ToString());

                /* === ノード追加 === */
                xml_own.AppendChild(xml_data);
            }

            return (true);
        }
    }
}
