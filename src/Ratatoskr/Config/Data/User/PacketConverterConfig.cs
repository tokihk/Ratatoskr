using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Config;
using Ratatoskr.Forms;
using Ratatoskr.PacketConverter;

namespace Ratatoskr.Config.Data.User
{
    [Serializable]
    public sealed class PacketConverterObjectConfig
    {
        public Guid                    ClassID  { get; set; } = Guid.Empty;
        public PacketConverterProperty Property { get; set; } = null;


        public PacketConverterObjectConfig(Guid class_id, PacketConverterProperty devp)
        {
            ClassID = class_id;
            Property = devp;
        }

        public PacketConverterObjectConfig() { }
    }

    [Serializable]
    public sealed class PacketConverterConfig : ConfigObject, IConfigDataReadOnly<List<PacketConverterObjectConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<PacketConverterObjectConfig> Value { get; } = new List<PacketConverterObjectConfig>();


        public PacketConverterConfig()
        {
            Value.Add(new PacketConverterObjectConfig(PacketConverter.Filter.PacketConverterClassImpl.ClassID, null));
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

            var newobj = new PacketConverterObjectConfig();

            /* === パラメータ読み込み === */
            /* converter-class-id */
            newobj.ClassID = Guid.Parse(xml_node.GetAttribute("converter-class-id"));

            /* converter-property */
            foreach (XmlNode node in xml_node.GetElementsByTagName("converter-property")) {
                newobj.Property = LoadConfigPart_Property(node as XmlElement, newobj.ClassID);
            }

            /* === 設定リストへ追加 === */
            Value.Add(newobj);
        }

        private PacketConverterProperty LoadConfigPart_Property(XmlElement xml_node, Guid class_id)
        {
            if (xml_node == null)return (null);

            /* クラスIDからプロパティを取得 */
            var viewp = FormTaskManager.CreatePacketConverterProperty(class_id);

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
                /* converter-class-id */
                xml_data.SetAttribute("converter-class-id", info.ClassID.ToString());

                /* converter-property */
                SaveConfigPart_Property(xml_data, info);

                /* === ノード追加 === */
                xml_own.AppendChild(xml_data);
            }

            return (true);
        }

        public void SaveConfigPart_Property(XmlElement xml_own, PacketConverterObjectConfig info)
        {
            var xml_data = xml_own.OwnerDocument.CreateElement("converter-property");

            if (info.Property != null) {
                info.Property.SaveConfigData(xml_data);
            }

            xml_own.AppendChild(xml_data);
        }
    }
}
