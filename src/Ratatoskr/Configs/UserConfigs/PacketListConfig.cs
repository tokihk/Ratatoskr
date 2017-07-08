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
    internal sealed class PacketObjectConfig
    {
        public string ProtocolName { get; set; } = "";
        public byte[] BitData      { get; set; } = null;
        public uint   BitSize      { get; set; } = 0;

        public PacketObjectConfig(string protocol_name, byte[] bitdata, uint bitsize)
        {
            ProtocolName = protocol_name;
            BitData = bitdata;
            BitSize = bitsize;
        }

        public PacketObjectConfig() { }
    }

    [Serializable]
    internal sealed class PacketListConfig : IConfigDataReadOnly<List<PacketObjectConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<PacketObjectConfig> Value { get; } = new List<PacketObjectConfig>();


        public PacketListConfig()
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

            var newobj = new PacketObjectConfig();

            /* === パラメータ読み込み === */
            /* protocol-name */
            newobj.ProtocolName = XmlUtil.GetAttribute(xml_node, "protocol-name", "");

            /* bit-data */
            newobj.BitData = HexTextEncoder.ToByteArray(XmlUtil.GetAttribute(xml_node, "bit-data", ""));

            /* bit-size */
            newobj.BitSize = uint.Parse(XmlUtil.GetAttribute(xml_node, "bit-size", ""));

            /* === 設定リストへ追加 === */
            Value.Add(newobj);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            foreach (var info in Value) {
                var xml_data = xml_own.OwnerDocument.CreateElement(XML_NODE_DATA);

                /* === パラメータ書き込み === */
                /* protocol-name */
                xml_data.SetAttribute("protocol-name", info.ProtocolName);

                /* bit-data */
                xml_data.SetAttribute("bit-data", HexTextEncoder.ToHexText(info.BitData));

                /* bit-size */
                xml_data.SetAttribute("bit-size", info.BitSize.ToString());

                /* === ノード追加 === */
                xml_own.AppendChild(xml_data);
            }

            return (true);
        }
    }
}
