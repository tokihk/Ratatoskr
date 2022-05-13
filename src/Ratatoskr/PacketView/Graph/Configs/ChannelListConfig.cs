﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Config;

namespace Ratatoskr.PacketView.Graph.Configs
{
	internal enum VertRangeType
	{
		Preset_100,
		Preset_200,
		Preset_500,
		Preset_10000,
		Custom,
	}

    internal sealed class ChannelConfig
    {
		public bool				Visible					{ get; set; } = false;
		public Color			ForeColor				{ get; set; } = Color.Black;
		public uint				ValueBitSize			{ get; set; } = 8;
		public bool				ReverseByteEndian		{ get; set; } = false;
		public bool				ReverseBitEndian		{ get; set; } = false;
		public bool				SignedValue				{ get; set; } = false;
		public int				OscilloVertOffset		{ get; set; } = 0;
		public VertRangeType	OscilloVertRange		{ get; set; } = VertRangeType.Preset_100;
		public uint				OscilloVertRangeCustom	{ get; set; } = 1000;


        public ChannelConfig()
        {
        }
    }

    internal sealed class ChannelListConfig : IConfigDataReadOnly<List<ChannelConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<ChannelConfig> Value { get; } = new List<ChannelConfig>();


        public ChannelListConfig()
        {
            Value.Add(new ChannelConfig()
			{
				Visible = true,
				ForeColor = Color.LightGoldenrodYellow,
			});

            Value.Add(new ChannelConfig()
			{
				Visible = true,
				ForeColor = Color.LightBlue,
			});

            Value.Add(new ChannelConfig()
			{
				Visible = true,
				ForeColor = Color.LightPink,
			});

            Value.Add(new ChannelConfig()
			{
				Visible = true,
				ForeColor = Color.LightGreen,
			});

            Value.Add(new ChannelConfig()
			{
				Visible = true,
				ForeColor = Color.LightSalmon,
			});
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

            var newobj = new ChannelConfig();

            /* パラメータ読み込み */

			/* visible */
			try {
				newobj.Visible = bool.Parse(xml_node.GetAttribute("visible"));
			} catch {}

			/* fore-color */
			try {
				newobj.ForeColor = ColorTranslator.FromHtml(xml_node.GetAttribute("fore-color"));
			} catch {}

			/* value-bit-size */
			try {
				newobj.ValueBitSize = uint.Parse(xml_node.GetAttribute("value-bit-size"));
			} catch {}

			/* reverse-byte-endian */
			try {
				newobj.ReverseByteEndian = bool.Parse(xml_node.GetAttribute("reverse-byte-endian"));
			} catch {}

			/* reverse-bit-endian */
			try {
				newobj.ReverseBitEndian = bool.Parse(xml_node.GetAttribute("reverse-bit-endian"));
			} catch {}

			/* signed-value */
			try {
				newobj.SignedValue = bool.Parse(xml_node.GetAttribute("signed-value"));
			} catch {}

			/* oscillo-vert-offset */
			try {
				newobj.OscilloVertOffset = int.Parse(xml_node.GetAttribute("oscillo-vert-offset"));
			} catch {}

			/* oscillo-vert-range */
			try {
				newobj.OscilloVertRange = (VertRangeType)Enum.Parse(typeof(VertRangeType), xml_node.GetAttribute("oscillo-vert-range"));
			} catch {}

			/* oscillo-vert-range-custom */
			try {
				newobj.OscilloVertRangeCustom = uint.Parse(xml_node.GetAttribute("oscillo-vert-range-custom"));
			} catch {}

            /* 設定リストへ追加 */
            Value.Add(newobj);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            foreach (var info in Value) {
                var xml_data = xml_own.OwnerDocument.CreateElement(XML_NODE_DATA);

                /* fore-color */
                xml_data.SetAttribute("fore-color", ColorTranslator.ToHtml(info.ForeColor));

				/* value-bit-size */
                xml_data.SetAttribute("value-bit-size", info.ValueBitSize.ToString());

				/* reverse-byte-endian */
                xml_data.SetAttribute("reverse-byte-endian", info.ReverseByteEndian.ToString());

				/* reverse-bit-endian */
                xml_data.SetAttribute("reverse-bit-endian", info.ReverseBitEndian.ToString());

				/* signed-value */
                xml_data.SetAttribute("signed-value", info.SignedValue.ToString());

                /* oscillo-vert-offset */
                xml_data.SetAttribute("oscillo-vert-offset", info.OscilloVertOffset.ToString());

                /* oscillo-vert-range */
                xml_data.SetAttribute("oscillo-vert-range", info.OscilloVertRange.ToString());

                /* oscillo-vert-range-custom */
                xml_data.SetAttribute("oscillo-vert-range-custom", info.OscilloVertRangeCustom.ToString());

                xml_own.AppendChild(xml_data);
            }

            return (true);
        }
    }
}
