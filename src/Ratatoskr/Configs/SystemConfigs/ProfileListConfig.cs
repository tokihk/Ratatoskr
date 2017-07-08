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
        public string Name { get; set; } = "";
        public string Path { get; set; } = "";


        public ProfileObjectConfig(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public ProfileObjectConfig() { }
    }

    internal sealed class ProfileListConfig : IConfigDataReadOnly<List<ProfileObjectConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<ProfileObjectConfig> Value { get; } = new List<ProfileObjectConfig>();


        public ProfileListConfig()
        {
            Value.Add(new ProfileObjectConfig("default", "profile"));
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
            newobj.Name = xml_node.GetAttribute("name");
            newobj.Path = xml_node.GetAttribute("path");

            /* 設定リストへ追加 */
            Value.Add(newobj);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            foreach (var info in Value) {
                var xml_data = xml_own.OwnerDocument.CreateElement(XML_NODE_DATA);

                xml_data.SetAttribute("name", info.Name);
                xml_data.SetAttribute("path", info.Path);

                xml_own.AppendChild(xml_data);
            }

            return (true);
        }
    }
}
