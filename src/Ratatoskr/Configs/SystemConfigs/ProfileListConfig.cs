using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Configs;

namespace Ratatoskr.Configs.SystemConfigs
{
    internal sealed class ProfileObjectConfig
    {
        public string ID   { get; set; } = "";
        public string Name { get; set; } = "";


        public ProfileObjectConfig(string id, string name)
        {
            ID = id;
            Name = name;
        }

        public ProfileObjectConfig() { }

        public override bool Equals(object obj)
        {
            if (obj is ProfileObjectConfig) {
                return ((obj as ProfileObjectConfig).ID == ID);
            } else if (obj is string) {
                return ((obj as string) == ID);
            }

            return (base.Equals(obj));
        }

        public override string ToString()
        {
            return (Name);
        }
    }

    internal sealed class ProfileListConfig : IConfigDataReadOnly<List<ProfileObjectConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<ProfileObjectConfig> Value { get; } = new List<ProfileObjectConfig>();


        public ProfileListConfig()
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

            var newobj = new ProfileObjectConfig();

            /* パラメータ読み込み */
            newobj.ID = xml_node.GetAttribute("id");
            newobj.Name = xml_node.GetAttribute("name");

            /* 設定リストへ追加 */
            Value.Add(newobj);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            foreach (var info in Value) {
                var xml_data = xml_own.OwnerDocument.CreateElement(XML_NODE_DATA);

                xml_data.SetAttribute("id", info.ID);
                xml_data.SetAttribute("name", info.Name);

                xml_own.AppendChild(xml_data);
            }

            return (true);
        }
    }
}
