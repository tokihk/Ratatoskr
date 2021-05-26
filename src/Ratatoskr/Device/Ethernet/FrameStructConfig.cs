using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Config;

namespace Ratatoskr.Device.Ethernet
{
    [Serializable]
    internal sealed class FrameStructProtocolConfig
    {
        public FrameStructProtocolConfig(PacketDotNet.Packet packet_pdn)
        {
			ProtocolName = GetProtocolName(packet_pdn);
			ProtocolData = packet_pdn;
        }

        public FrameStructProtocolConfig() { }

		public string				ProtocolName = "";
        public PacketDotNet.Packet	ProtocolData = null;

		private string GetProtocolName(PacketDotNet.Packet packet_pdn)
		{
			return (packet_pdn.ToString());
		}
    }

    [Serializable]
    internal sealed class FrameStructConfig : ConfigObject, IConfigDataReadOnly<List<FrameStructProtocolConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<FrameStructProtocolConfig> Value { get; } = new List<FrameStructProtocolConfig>();


        public FrameStructConfig()
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

            var newobj = new FrameStructProtocolConfig();

            /* === パラメータ読み込み === */

            /* frame-protocol-property */
            foreach (XmlNode node in xml_node.GetElementsByTagName("frame-protocol-property")) {
                newobj.Packet = LoadConfigPart_FrameProtocolProperty(node as XmlElement, newobj.DeviceClassID);
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
				Debugger.DebugSystem.MessageOut(string.Format("LoadDeviceProperty Error: {0}", class_id.ToString("D")));
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
