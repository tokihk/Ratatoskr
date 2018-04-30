using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Generic;
using Ratatoskr.Configs;

namespace Ratatoskr.Configs.Types
{
    internal enum SmtpConnectModeType
    {
        Standard,
        STARTTLS,
        SSL_TLS,
    }

    [Serializable]
    internal sealed class MailConfig
    {
        public Guid     ID           { get; set; } = Guid.Empty;

        public string   SmtpHost     { get; set; } = "";
        public uint     SmtpPort     { get; set; } = 25;

        public SmtpConnectModeType SmtpConnectMode { get; set; } = SmtpConnectModeType.Standard;
        public string   SmtpUsername { get; set; } = "";
        public string   SmtpPassword { get; set; } = "";

        public uint     MinimumInterval   { get; set; } = 180;

        public string   NotifyTimmingMask { get; set; } = "";


        public MailConfig(Guid id, string smtp_host, uint smtp_port, SmtpConnectModeType mode, string smtp_username, string smtp_password)
        {
            ID = id;
            SmtpHost = smtp_host;
            SmtpPort = smtp_port;
            SmtpConnectMode = mode;
            SmtpUsername = smtp_username;
            SmtpPassword = smtp_password;
        }

        public MailConfig() { }
    }

    [Serializable]
    internal sealed class MailListConfig : ConfigObject, IConfigDataReadOnly<List<MailConfig>>, IConfigReader, IConfigWriter
    {
        private const    string XML_NODE_DATA   = "data";
        private readonly string CRYPTO_PASSCODE = "E1DD0BA3-5D35-4246-A467-1E33C65960E9";


        public List<MailConfig> Value { get; } = new List<MailConfig>();


        public MailListConfig()
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

            var newobj = new MailConfig();

            /* === パラメータ読み込み === */
            /* id */
            newobj.ID = new Guid(GetAttribute(xml_node, "id", Guid.NewGuid().ToString()));

            /* snmp-host */
            newobj.SmtpHost = GetAttribute(xml_node, "smtp-host", "");

            /* snmp-port */
            newobj.SmtpPort = uint.Parse(GetAttribute(xml_node, "smtp-port", ""));

            /* snmp-username */
            newobj.SmtpUsername = GetAttribute(xml_node, "smtp-username", "");

            /* snmp-password */
            newobj.SmtpPassword = GetAttribute(xml_node, "smtp-password", "");
            if (newobj.SmtpPassword.Length > 0) {
                newobj.SmtpPassword = CryptoUtil.DecryptText(newobj.SmtpPassword, CRYPTO_PASSCODE);
            }

            /* minimum-interval */
            newobj.MinimumInterval = uint.Parse(GetAttribute(xml_node, "minimum-interval", "180"));

            /* notify-timming-mask */
            newobj.NotifyTimmingMask = GetAttribute(xml_node, "notify-timming-mask", "");

            /* === 設定リストへ追加 === */
            Value.Add(newobj);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            foreach (var info in Value) {
                var xml_data = xml_own.OwnerDocument.CreateElement(XML_NODE_DATA);

                /* === パラメータ書き込み === */
                /* id */
                xml_data.SetAttribute("id", info.ID.ToString());

                /* snmp-host */
                xml_data.SetAttribute("smtp-host", info.SmtpHost);

                /* snmp-port */
                xml_data.SetAttribute("smtp-port", info.SmtpPort.ToString());

                /* snmp-username */
                xml_data.SetAttribute("smtp-username", info.SmtpUsername);

                /* snmp-password */
                xml_data.SetAttribute("smtp-password", CryptoUtil.EncryptText(info.SmtpPassword, CRYPTO_PASSCODE));

                /* minimum-interval */
                xml_data.SetAttribute("minimum-interval", info.MinimumInterval.ToString());

                /* notify-timming-mask */
                xml_data.SetAttribute("notify-timming-mask", info.NotifyTimmingMask);

                /* === ノード追加 === */
                xml_own.AppendChild(xml_data);
            }

            return (true);
        }
    }
}
