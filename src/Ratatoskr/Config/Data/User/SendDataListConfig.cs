using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Config;

namespace Ratatoskr.Config.Data.User
{
    public enum SendDataTargetType
    {
        System,
        Common,
        Custom,
    }

    [Serializable]
    public sealed class SendDataConfig
    {
        public string             SendData         { get; set; } = "";
        public bool               PlayListInclude  { get; set; } = true;
        public SendDataTargetType SendTargetType   { get; set; } = SendDataTargetType.System;
        public string             SendTargetCustom { get; set; } = "*";
        public uint               DelayFixed       { get; set; } = 0;
        public uint               DelayRandom      { get; set; } = 0;

        public SendDataConfig(bool enable, SendDataTargetType target_type, string target, string command, uint delay_fixed, uint delay_random)
        {
            SendData = command;
            PlayListInclude = enable;
            SendTargetType = target_type;
            SendTargetCustom = target;
            DelayFixed = delay_fixed;
            DelayRandom = delay_random;
        }

        public SendDataConfig(SendDataConfig obj)
        {
            SendData = string.Copy(obj.SendData);
            PlayListInclude = obj.PlayListInclude;
            SendTargetType = obj.SendTargetType;
            SendTargetCustom = obj.SendTargetCustom;
            DelayFixed = obj.DelayFixed;
            DelayRandom = obj.DelayRandom;
        }

        public SendDataConfig() { }
    }

    [Serializable]
    public sealed class SendDataListConfig : ConfigObject, IConfigDataReadOnly<List<SendDataConfig>>, IConfigReader, IConfigWriter
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
            /* send_data */
            newobj.SendData = GetAttribute(xml_node, "send_data", "");

            /* play_list_include */
            newobj.PlayListInclude = bool.Parse(GetAttribute(xml_node, "play_list_include", "false"));

            /* send_target_type */
            newobj.SendTargetType = (SendDataTargetType)Enum.Parse(typeof(SendDataTargetType), GetAttribute(xml_node, "send_target_type", SendDataTargetType.Common.ToString()));

            /* send_target_custom */
            newobj.SendTargetCustom = GetAttribute(xml_node, "send_target_custom", "*");

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
                /* send_data */
                xml_data.SetAttribute("send_data", info.SendData);

                /* play_list_include */
                xml_data.SetAttribute("play_list_include", info.PlayListInclude.ToString());

                /* send_target_type */
                xml_data.SetAttribute("send_target_type", info.SendTargetType.ToString());

                /* send_target_custom */
                xml_data.SetAttribute("send_target_custom", info.SendTargetCustom.ToString());

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
