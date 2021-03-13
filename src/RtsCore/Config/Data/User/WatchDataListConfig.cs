using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RtsCore.Config;

namespace RtsCore.Framework.Config.Data.User
{
    public enum WatchTargetType
    {
        RawPacket,
        ViewPacket,
        DrawPacket,
    }


    [Serializable]
    public sealed class WatchDataConfig
    {
        public bool            Enable       { get; set; } = false;
        public WatchTargetType WatchTarget  { get; set; } = WatchTargetType.RawPacket;
        public string          Expression   { get; set; } = "";
        public bool            NtfEvent     { get; set; } = false;
        public bool            NtfDialog    { get; set; } = false;
        public bool            NtfMail      { get; set; } = false;


        public WatchDataConfig(bool enable, WatchTargetType target, string exp, bool ntf_event, bool ntf_dialog, bool ntf_mail)
        {
            Enable = enable;
            WatchTarget = target;
            Expression = exp;
            NtfEvent = ntf_event;
            NtfDialog = ntf_dialog;
            NtfMail = ntf_mail;
        }

        public WatchDataConfig() { }
    }

    [Serializable]
    public sealed class WatchDataListConfig : ConfigObject, IConfigDataReadOnly<List<WatchDataConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<WatchDataConfig> Value { get; } = new List<WatchDataConfig>();


        public WatchDataListConfig()
        {
            Value.Add(new WatchDataConfig(false, WatchTargetType.RawPacket, "AsciiText == /.*sample.*/", false, false, false));
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

            var newobj = new WatchDataConfig();

            /* === パラメータ読み込み === */
            /* enable */
            newobj.Enable = bool.Parse(GetAttribute(xml_node, "enable", "false"));

            /* watch-target */
            newobj.WatchTarget = (WatchTargetType)Enum.Parse(typeof(WatchTargetType), GetAttribute(xml_node, "watch-target", "0"));

            /* expression */
            newobj.Expression = GetAttribute(xml_node, "expression", "");

            /* ntf_event */
            newobj.NtfEvent = bool.Parse(GetAttribute(xml_node, "ntf_event", "false"));

            /* ntf_dialog */
            newobj.NtfDialog = bool.Parse(GetAttribute(xml_node, "ntf_dialog", "false"));

            /* ntf_mail */
            newobj.NtfMail = bool.Parse(GetAttribute(xml_node, "ntf_mail", "false"));

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

                /* watch-target */
                xml_data.SetAttribute("watch-target", info.WatchTarget.ToString());

                /* expression */
                xml_data.SetAttribute("expression", info.Expression.ToString());

                /* ntf_event */
                xml_data.SetAttribute("ntf_event", info.NtfEvent.ToString());

                /* ntf_dialog */
                xml_data.SetAttribute("ntf_dialog", info.NtfDialog.ToString());

                /* ntf_mail */
                xml_data.SetAttribute("ntf_mail", info.NtfMail.ToString());

                /* === ノード追加 === */
                xml_own.AppendChild(xml_data);
            }

            return (true);
        }
    }
}
