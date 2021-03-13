using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RtsCore;
using RtsCore.Config;
using RtsCore.Framework.Plugin;
using Ratatoskr.Plugin;

namespace RtsCore.Framework.Config.Data.User
{
    [Serializable]
    public sealed class PluginObjectConfig
    {
        public PluginObjectConfig(Guid plgid, PluginProperty plgp)
        {
            PluginClassID  = plgid;
            PluginProperty = plgp;
        }

        public PluginObjectConfig() { }

        public Guid           PluginClassID  { get; set; } = Guid.Empty;
        public PluginProperty PluginProperty { get; set; } = null;
    }

    [Serializable]
    public sealed class PluginListConfig : ConfigObject, IConfigDataReadOnly<List<PluginObjectConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<PluginObjectConfig> Value { get; } = new List<PluginObjectConfig>();


        public PluginListConfig()
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

            var newobj = new PluginObjectConfig();

            /* === パラメータ読み込み === */
            /* plugin-class-id */
            newobj.PluginClassID = Guid.Parse(GetAttribute(xml_node, "plugin-class-id", Guid.Empty.ToString()));

            /* plugin-property */
            foreach (XmlNode node in xml_node.GetElementsByTagName("plugin-property")) {
                newobj.PluginProperty = LoadConfigPart_PluginProperty(node as XmlElement, newobj.PluginClassID);
            }

            /* 設定リストへ追加 */
            Value.Add(newobj);
        }

        private PluginProperty LoadConfigPart_PluginProperty(XmlElement xml_node, Guid class_id)
        {
            if (xml_node == null)return (null);

            /* クラスIDからプロパティを取得 */
            var plgp = PluginManager.CreatePluginPropery(class_id);

            if (plgp == null) {
				/* 該当IDのプラグインがインストールされていない場合は無視 */
				Kernel.DebugMessage(string.Format("LoadPluginProperty Error: {0}", class_id.ToString("D")));
				return (null);
			}

            /* プロパティ読み込み */
            plgp.LoadConfigData(xml_node);

            return (plgp);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            foreach (var config in Value) {
                var xml_data = xml_own.OwnerDocument.CreateElement(XML_NODE_DATA);

                /* plugin-class-id */
                xml_data.SetAttribute("plugin-class-id", config.PluginClassID.ToString());

                /* plugin-property */
                SaveConfigPart_PluginProperty(xml_data, config);

                xml_own.AppendChild(xml_data);
            }

            return (true);
        }

        private void SaveConfigPart_PluginProperty(XmlElement xml_own, PluginObjectConfig config)
        {
            if (config.PluginProperty == null)return;

            var xml_data = xml_own.OwnerDocument.CreateElement("plugin-property");

            config.PluginProperty.SaveConfigData(xml_data);

            xml_own.AppendChild(xml_data);
        }
    }
}
