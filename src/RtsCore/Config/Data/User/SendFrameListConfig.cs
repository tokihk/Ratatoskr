using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RtsCore.Config;
using RtsCore.Utility;

namespace RtsCore.Framework.Config.Data.User
{
    [Serializable]
    public sealed class SendFrameConfig
    {
		public Guid               ProtocolDecoderID { get; set; } = Guid.Empty;
		public UInt16             FrameType         { get; set; } = 0;
        public byte[]             FrameBitData      { get; set; } = null;
		public uint               FrameBitLength    { get; set; } = 0;

        public SendFrameConfig(Guid prdc_id, UInt16 type, byte[] bitdata, uint bitdata_length)
        {
			ProtocolDecoderID = prdc_id;
			FrameType = type;
			FrameBitData = bitdata;
			FrameBitLength = bitdata_length;
        }

        public SendFrameConfig(SendFrameConfig obj)
        {
			ProtocolDecoderID = obj.ProtocolDecoderID;
			FrameType = obj.FrameType;
			FrameBitData = (byte[])obj.FrameBitData.Clone();
			FrameBitLength = obj.FrameBitLength;
        }

        public SendFrameConfig() { }
    }

    [Serializable]
    public sealed class SendFrameListConfig : ConfigObject, IConfigDataReadOnly<List<SendFrameConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<SendFrameConfig> Value { get; } = new List<SendFrameConfig>();


        public SendFrameListConfig()
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

            var newobj = new SendFrameConfig();

            /* === パラメータ読み込み === */
            /* protocol_decoder_id */
            newobj.ProtocolDecoderID = Guid.Parse(GetAttribute(xml_node, "protocol_decoder_id", ""));

            /* frame_type */
            newobj.FrameType = UInt16.Parse(GetAttribute(xml_node, "frame_type", "0"));

            /* frame_bitdata */
            newobj.FrameBitData = HexTextEncoder.ToBinary(GetAttribute(xml_node, "frame_bitdata", ""));

			/* frame_bitlength */
			newobj.FrameBitLength = uint.Parse(GetAttribute(xml_node, "frame_bitlength", "0"));

            /* === 設定リストへ追加 === */
            Value.Add(newobj);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            foreach (var info in Value) {
                var xml_data = xml_own.OwnerDocument.CreateElement(XML_NODE_DATA);

				/* === パラメータ書き込み === */
				/* protocol_decoder_id */
				xml_data.SetAttribute("protocol_decoder_id", info.ProtocolDecoderID.ToString("D"));

				/* frame_type */
				xml_data.SetAttribute("frame_type", info.FrameType.ToString());

				/* frame_bitdata */
				xml_data.SetAttribute("frame_bitdata", HexTextEncoder.ToHexText(info.FrameBitData));

				/* frame_bitlength */
				xml_data.SetAttribute("frame_bitlength", info.FrameBitLength.ToString());

                /* === ノード追加 === */
                xml_own.AppendChild(xml_data);
            }

            return (true);
        }
    }
}
