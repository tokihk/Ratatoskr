using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Debugger;
using Ratatoskr.Device;
using Ratatoskr.Gate;

namespace Ratatoskr.Config.Data.User
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
    internal sealed class GateListConfig : ConfigObject, IConfigDataReadOnly<List<GateObjectConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<GateObjectConfig> Value { get; } = new List<GateObjectConfig>();


        public GateListConfig()
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

            var newobj = new GateObjectConfig();

            /* === パラメータ読み込み === */
            /* alias */
            newobj.GateProperty.Alias = GetAttribute(xml_node, "alias", "");

            /* back-color */
            newobj.GateProperty.Color = ColorTranslator.FromHtml(GetAttribute(xml_node, "back-color", "#FFFFFF"));

            /* connect */
            newobj.GateProperty.ConnectRequest = bool.Parse(GetAttribute(xml_node, "connect", "false"));

            /* data-send-completed-notify */
            newobj.DeviceConfig.DataSendCompletedNotify = bool.Parse(GetAttribute(xml_node, "data-send-completed-notify", "true"));

            /* data-recv-completed-notify */
            newobj.DeviceConfig.DataRecvCompletedNotify = bool.Parse(GetAttribute(xml_node, "data-recv-completed-notify", "true"));

            /* device-connect-notify */
            newobj.DeviceConfig.DeviceConnectNotify = bool.Parse(GetAttribute(xml_node, "device-connect-notify", "true"));

            /* data-send-enable */
            newobj.DeviceConfig.DataSendEnable = bool.Parse(GetAttribute(xml_node, "data-send-enable", "true"));

            /* data-send-queue-limit */
            newobj.DeviceConfig.DataSendQueueLimit = uint.Parse(GetAttribute(xml_node, "data-send-queue-limit", "0"));

            /* data-redirect-enable */
            newobj.DeviceConfig.DataRedirectEnable = bool.Parse(GetAttribute(xml_node, "data-redirect-enable", "true"));

            /* data-redirect-queue-limit */
            newobj.DeviceConfig.DataRedirectQueueLimit = uint.Parse(GetAttribute(xml_node, "data-redirect-queue-limit", "0"));

            /* send-data-redirect-alias */
            newobj.GateProperty.SendRedirectAlias = GetAttribute(xml_node, "send-data-redirect-alias", "");

            /* recv-data-redirect-alias */
            newobj.GateProperty.RecvRedirectAlias = GetAttribute(xml_node, "recv-data-redirect-alias", "");

            /* connect-command */
            newobj.GateProperty.ConnectCommand = GetAttribute(xml_node, "connect-command", "");

            /* device-class-id */
            newobj.DeviceClassID = Guid.Parse(GetAttribute(xml_node, "device-class-id", Guid.Empty.ToString()));

            /* device-property */
            foreach (XmlNode node in xml_node.GetElementsByTagName("device-property")) {
                newobj.DeviceProperty = LoadConfigPart_DeviceProperty(node as XmlElement, newobj.DeviceClassID);
            }

            /* 設定リストへ追加 */
            Value.Add(newobj);
        }

        private string[] LoadConfigPart_ConnectCommand(XmlElement xml_node)
        {
            if (xml_node == null)return (null);

            var values = new List<string>();

            foreach (XmlElement data in xml_node.GetElementsByTagName("data")) {
                values.Add(GetAttribute(data, "value", ""));
            }

            return (values.ToArray());
        }

        private DeviceProperty LoadConfigPart_DeviceProperty(XmlElement xml_node, Guid class_id)
        {
            if (xml_node == null)return (null);

            /* クラスIDからプロパティを取得 */
            var devp = DeviceManager.Instance.CreateDeviceProperty(class_id);

            if (devp == null) {
				/* 該当デバイスが存在しないかプロパティが生成できない */
				DebugManager.MessageOut(DebugEventSender.Application, DebugEventType.ConfigEvent, string.Format("LoadDeviceProperty Error: {0}", class_id.ToString("D")));
				return (null);
			}

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

                /* data-send-completed-notify */
                xml_data.SetAttribute("data-send-completed-notify", config.DeviceConfig.DataSendCompletedNotify.ToString());

                /* data-recv-completed-notify */
                xml_data.SetAttribute("data-recv-completed-notify", config.DeviceConfig.DataRecvCompletedNotify.ToString());

                /* device-connect-notify */
                xml_data.SetAttribute("device-connect-notify", config.DeviceConfig.DeviceConnectNotify.ToString());

                /* data-send-enable */
                xml_data.SetAttribute("data-send-enable", config.DeviceConfig.DataSendEnable.ToString());

                /* data-send-queue-limit */
                xml_data.SetAttribute("data-send-queue-limit", config.DeviceConfig.DataSendQueueLimit.ToString());

                /* data-redirect-enable */
                xml_data.SetAttribute("data-redirect-enable", config.DeviceConfig.DataRedirectEnable.ToString());
                
                /* data-redirect-queue-limit */
                xml_data.SetAttribute("data-redirect-queue-limit", config.DeviceConfig.DataRedirectQueueLimit.ToString());

                /* send-data-redirect-alias */
                xml_data.SetAttribute("send-data-redirect-alias", config.GateProperty.SendRedirectAlias);

                /* recv-data-redirect-alias */
                xml_data.SetAttribute("recv-data-redirect-alias", config.GateProperty.RecvRedirectAlias);

                /* connect-command */
                xml_data.SetAttribute("connect-command", config.GateProperty.ConnectCommand);

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
