using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Forms;
using Ratatoskr.Gate;
using Ratatoskr.Configs;
using Ratatoskr.PacketViews;

namespace Ratatoskr.Configs.UserConfigs
{
    [Serializable]
    internal sealed class PacketViewObjectConfig
    {
        public PacketViewObjectConfig() { }
        public PacketViewObjectConfig(Guid class_id, Guid obj_id, ViewProperty viewp)
        {
            ViewClassID = class_id;
            ViewObjectID = obj_id;
            ViewProperty = viewp;
        }

        public Guid         ViewClassID  { get; set; } = Guid.Empty;
        public Guid         ViewObjectID { get; set; } = Guid.NewGuid();
        public ViewProperty ViewProperty { get; set; } = null;
    }

    [Serializable]
    internal sealed class PacketViewConfig : ConfigObject, IConfigDataReadOnly<List<PacketViewObjectConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<PacketViewObjectConfig> Value { get; } = new List<PacketViewObjectConfig>();


        public PacketViewConfig()
        {
            Value.Add(new PacketViewObjectConfig(PacketViews.Packet.ViewClassImpl.ClassID, Guid.NewGuid(), null));
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

            var newobj = new PacketViewObjectConfig();

            /* === パラメータ読み込み === */
            /* view-class-id */
            if (xml_node.HasAttribute("view-class-id")) {
                newobj.ViewClassID = Guid.Parse(xml_node.GetAttribute("view-class-id"));
            }

            /* view-object-id */
            if (xml_node.HasAttribute("view-object-id")) {
                newobj.ViewObjectID = Guid.Parse(xml_node.GetAttribute("view-object-id"));
            }

            /* view-property */
            foreach (XmlNode node in xml_node.GetElementsByTagName("view-property")) {
                newobj.ViewProperty = LoadConfigPart_Property(node as XmlElement, newobj.ViewClassID);
            }

            /* === 設定リストへ追加 === */
            Value.Add(newobj);
        }

        private ViewProperty LoadConfigPart_Property(XmlElement xml_node, Guid class_id)
        {
            if (xml_node == null)return (null);

            /* クラスIDからプロパティを取得 */
            var viewp = FormTaskManager.CreatePacketViewProperty(class_id);

            if (viewp == null)return (null);

            /* プロパティ読み込み */
            viewp.LoadConfigData(xml_node);

            return (viewp);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            foreach (var info in Value) {
                var xml_data = xml_own.OwnerDocument.CreateElement(XML_NODE_DATA);

                /* === パラメータ書き込み === */
                /* view-class-id */
                xml_data.SetAttribute("view-class-id", info.ViewClassID.ToString());

                /* view-object-id */
                xml_data.SetAttribute("view-object-id", info.ViewObjectID.ToString());

                /* view-property */
                SaveConfigPart_Property(xml_data, info);

                /* === ノード追加 === */
                xml_own.AppendChild(xml_data);
            }

            return (true);
        }

        public void SaveConfigPart_Property(XmlElement xml_own, PacketViewObjectConfig info)
        {
            if (info.ViewProperty == null)return;

            var xml_data = xml_own.OwnerDocument.CreateElement("view-property");

            info.ViewProperty.SaveConfigData(xml_data);

            xml_own.AppendChild(xml_data);
        }
    }
}
