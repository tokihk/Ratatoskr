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
        public GateObjectConfig(GateProperty gatep, DeviceConfig devconf, Guid devc_id, DeviceProperty devp)
        {
            GateProperty = gatep;
            DeviceConfig = devconf;
            DeviceClassID = devc_id;
            DeviceProperty = devp;
        }

        public GateObjectConfig(string alias, Color color)
            : this(new GateProperty(alias, color), new DeviceConfig(), Guid.Empty, null)
        {
        }

        public GateObjectConfig() { }

        public GateProperty   GateProperty   { get; set; } = new GateProperty("", Color.White);
        public DeviceConfig   DeviceConfig   { get; set; } = new DeviceConfig();
        public Guid           DeviceClassID  { get; set; } = Guid.Empty;
        public DeviceProperty DeviceProperty { get; set; } = null;
    }

    [Serializable]
    internal sealed class GateListConfig : IConfigDataReadOnly<List<GateObjectConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<GateObjectConfig> Value { get; } = new List<GateObjectConfig>();


        public GateListConfig()
        {
            Value.Add(new GateObjectConfig("GATE_001", Color.LightGoldenrodYellow));
            Value.Add(new GateObjectConfig("GATE_002", Color.LightBlue));
            Value.Add(new GateObjectConfig("GATE_003", Color.LightPink));
            Value.Add(new GateObjectConfig("GATE_004", Color.LightGreen));
            Value.Add(new GateObjectConfig("GATE_005", Color.LightSalmon));
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
            /* alias */
            newobj.GateProperty.Alias = XmlUtil.GetAttribute(xml_node, "alias", "");

            /* back-color */
            newobj.GateProperty.Color = ColorTranslator.FromHtml(XmlUtil.GetAttribute(xml_node, "back-color", "#FFFFFF"));

            /* connect */
            newobj.GateProperty.ConnectRequest = bool.Parse(XmlUtil.GetAttribute(xml_node, "connect", "false"));

            /* send-enable */
            newobj.DeviceConfig.SendEnable = bool.Parse(XmlUtil.GetAttribute(xml_node, "send-enable", "true"));

            /* recv-enable */
            newobj.DeviceConfig.RecvEnable = bool.Parse(XmlUtil.GetAttribute(xml_node, "recv-enable", "true"));

            /* redirect-enable */
            newobj.DeviceConfig.RedirectEnable = bool.Parse(XmlUtil.GetAttribute(xml_node, "redirect-enable", "true"));

            /* send-data-queue-limit */
            newobj.DeviceConfig.SendDataQueueLimit = uint.Parse(XmlUtil.GetAttribute(xml_node, "send-data-queue-limit", "0"));

            /* redirect-data-queue-limit */
            newobj.DeviceConfig.RedirectDataQueueLimit = uint.Parse(XmlUtil.GetAttribute(xml_node, "redirect-data-queue-limit", "0"));

            /* redirect-alias */
            newobj.GateProperty.RedirectAlias = XmlUtil.GetAttribute(xml_node, "redirect-alias", "");

            /* data-rate-target */
            newobj.GateProperty.DataRateTarget = (DeviceDataRateTarget)Enum.Parse(typeof(DeviceDataRateTarget), XmlUtil.GetAttribute(xml_node, "data-rate-target", "0"));

            /* data-rate-graph-limit */
            newobj.GateProperty.DataRateGraphLimit = ulong.Parse(XmlUtil.GetAttribute(xml_node, "data-rate-graph-limit", "1000000"));

            /* device-class-id */
            newobj.DeviceClassID = Guid.Parse(XmlUtil.GetAttribute(xml_node, "device-class-id", Guid.Empty.ToString()));

            /* device-property */
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

                /* alias */
                xml_data.SetAttribute("alias", config.GateProperty.Alias);

                /* back-color */
                xml_data.SetAttribute("back-color", ColorTranslator.ToHtml(config.GateProperty.Color));

                /* connect */
                xml_data.SetAttribute("connect", config.GateProperty.ConnectRequest.ToString());

                /* send-enable */
                xml_data.SetAttribute("send-enable", config.DeviceConfig.SendEnable.ToString());

                /* recv-enable */
                xml_data.SetAttribute("recv-enable", config.DeviceConfig.RecvEnable.ToString());

                /* redirect-enable */
                xml_data.SetAttribute("redirect-enable", config.DeviceConfig.RedirectEnable.ToString());
                
                /* send-data-queue-limit */
                xml_data.SetAttribute("send-data-queue-limit", config.DeviceConfig.SendDataQueueLimit.ToString());

                /* redirect-data-queue-limit */
                xml_data.SetAttribute("redirect-data-queue-limit", config.DeviceConfig.RedirectDataQueueLimit.ToString());

                /* redirect-alias */
                xml_data.SetAttribute("redirect-alias", config.GateProperty.RedirectAlias);

                /* data-rate-target */
                xml_data.SetAttribute("data-rate-target", config.GateProperty.DataRateTarget.ToString());

                /* data-rate-graph-limit */
                xml_data.SetAttribute("data-rate-graph-limit", config.GateProperty.DataRateGraphLimit.ToString());

                /* device-class-id */
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
