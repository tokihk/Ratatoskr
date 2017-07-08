using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Devices;
using Ratatoskr.Gate;
using Ratatoskr.Generic;
using Ratatoskr.Configs;

namespace Ratatoskr.Configs.UserConfigs
{
    [Serializable]
    internal sealed class GateObjectConfig
    {
        public string Alias          { get; set; } = "";
        public Color  Color          { get; set; } = Color.White;
        public bool   ConnectRequest { get; set; } = false;

        public Guid           DeviceClassID  { get; set; } = Guid.Empty;
        public DeviceProperty DeviceProperty { get; set; } = null;


        public GateObjectConfig(string alias, Color color, bool connect_req, Guid class_id, DeviceProperty devp)
        {
            Alias = alias;
            Color = color;
            ConnectRequest = connect_req;

            DeviceClassID = class_id;
            DeviceProperty = devp;
        }

        public GateObjectConfig() { }
    }

    [Serializable]
    internal sealed class GateConfig : IConfigDataReadOnly<List<GateObjectConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<GateObjectConfig> Value { get; } = new List<GateObjectConfig>();


        public GateConfig()
        {
            Value.Add(new GateObjectConfig("GATE_001", Color.LightGoldenrodYellow, false, Guid.Empty, null));
            Value.Add(new GateObjectConfig("GATE_002", Color.LightBlue,            false, Guid.Empty, null));
            Value.Add(new GateObjectConfig("GATE_003", Color.LightPink,            false, Guid.Empty, null));
            Value.Add(new GateObjectConfig("GATE_004", Color.LightGreen,           false, Guid.Empty, null));
            Value.Add(new GateObjectConfig("GATE_005", Color.LightSalmon,          false, Guid.Empty, null));
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

            var newobj = new GateObjectConfig();

            /* === パラメータ読み込み === */
            newobj.Alias = XmlUtil.GetAttribute(xml_node, "alias", "");
            newobj.Color = ColorTranslator.FromHtml(XmlUtil.GetAttribute(xml_node, "back-color", "#FFFFFF"));
            newobj.ConnectRequest = bool.Parse(XmlUtil.GetAttribute(xml_node, "connect", "false"));

            newobj.DeviceClassID = Guid.Parse(XmlUtil.GetAttribute(xml_node, "device-class-id", Guid.Empty.ToString()));

            /* property */
            foreach (XmlNode node in xml_node.GetElementsByTagName("device-property")) {
                newobj.DeviceProperty = LoadConfigPart_DeviceProperty(node as XmlElement, newobj.DeviceClassID);
            }

            /* 設定リストへ追加 */
            Value.Add(newobj);
        }

        private DeviceProperty LoadConfigPart_DeviceProperty(XmlElement xml_node, Guid class_id)
        {
            if (xml_node == null)return (null);

            /* クラスIDからプロパティを取得 */
            var devp = GateManager.CreateDeviceProperty(class_id);

            if (devp == null)return (null);

            /* プロパティ読み込み */
            devp.LoadConfigData(xml_node);

            return (devp);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            foreach (var config in Value) {
                var xml_data = xml_own.OwnerDocument.CreateElement(XML_NODE_DATA);

                xml_data.SetAttribute("alias", config.Alias);
                xml_data.SetAttribute("back-color", ColorTranslator.ToHtml(config.Color));
                xml_data.SetAttribute("connect", config.ConnectRequest.ToString());

                xml_data.SetAttribute("device-class-id", config.DeviceClassID.ToString());

                /* device-property */
                SaveConfigPart_DeviceProperty(xml_data, config);

                xml_own.AppendChild(xml_data);
            }

            return (true);
        }

        private void SaveConfigPart_DeviceProperty(XmlElement xml_own, GateObjectConfig config)
        {
            if (config.DeviceProperty == null)return;

            var xml_data = xml_own.OwnerDocument.CreateElement("device-property");

            config.DeviceProperty.SaveConfigData(xml_data);

            xml_own.AppendChild(xml_data);
        }
    }
}
