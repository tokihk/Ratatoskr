using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RtsCore.Config;

namespace Ratatoskr.Configs.UserConfigs
{
    internal enum SendDataTargetType
    {
        Common,
        Custom,
    }

    [Serializable]
    internal sealed class SendDataConfig
    {
        public bool               Enable       { get; set; } = true;
        public SendDataTargetType TargetType   { get; set; } = SendDataTargetType.Common;
        public string             CustomTarget { get; set; } = "*";
        public string             Command      { get; set; } = "";
        public uint               DelayFixed   { get; set; } = 0;
        public uint               DelayRandom  { get; set; } = 0;

        public SendDataConfig(bool enable, SendDataTargetType target_type, string target, string command, uint delay_fixed, uint delay_random)
        {
            Enable = enable;
            TargetType = target_type;
            CustomTarget = target;
            Command = command;
            DelayFixed = delay_fixed;
            DelayRandom = delay_random;
        }

        public SendDataConfig() { }
    }

    [Serializable]
    internal sealed class SendDataListConfig : ConfigObject, IConfigDataReadOnly<List<SendDataConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<SendDataConfig> Value { get; } = new List<SendDataConfig>();


        public SendDataListConfig()
        {
            Value.Add(new SendDataConfig(true, SendDataTargetType.Common, "*", "02'Test Command 1'03", 2000, 0));
            Value.Add(new SendDataConfig(true, SendDataTargetType.Common, "*", "02'Test Command 2'03", 1000, 0));
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

            var newobj = new SendDataConfig();

            /* === パラメータ読み込み === */
            /* enable */
            newobj.Enable = bool.Parse(GetAttribute(xml_node, "enable", "false"));

            /* target_type */
            newobj.TargetType = (SendDataTargetType)Enum.Parse(typeof(SendDataTargetType), GetAttribute(xml_node, "target_type", SendDataTargetType.Common.ToString()));

            /* custom_target */
            newobj.CustomTarget = GetAttribute(xml_node, "custom_target", "*");

            /* command */
            newobj.Command = GetAttribute(xml_node, "command", "");

            /* delay_fixed */
            newobj.DelayFixed = uint.Parse(GetAttribute(xml_node, "delay_fixed", "0"));

            /* delay_random */
            newobj.DelayRandom = uint.Parse(GetAttribute(xml_node, "delay_random", "0"));

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

                /* target_type */
                xml_data.SetAttribute("target_type", info.TargetType.ToString());

                /* custom_target */
                xml_data.SetAttribute("custom_target", info.CustomTarget.ToString());

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
