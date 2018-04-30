using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ratatoskr.Configs
{
    [Serializable]
    internal abstract class ConfigManagerBase<Type> : ConfigHolder
        where Type : class, new()
    {
        public class ConfigLoadedEventHandler : EventArgs
        {
            public ConfigLoadedEventHandler(Type config_new, Type config_old) : base()
            {
                NewConfig = config_new;
                OldConfig = config_old;
            }

            public Type NewConfig { get; }
            public Type OldConfig { get; }
        }

        public class ConfigSaveReadyEventHandler : EventArgs
        {
            public ConfigSaveReadyEventHandler(Type config) : base()
            {
                Config = config;
            }

            public Type Config { get; }
        }


        private const string XML_TAG_DATA    = "data";

        private const string DATA_ATTR_NAME  = "name";
        private const string DATA_ATTR_VALUE = "value";


        private string config_name_ = null;


        public ConfigManagerBase(string name)
        {
            config_name_ = name;
        }

        public bool LoadConfig(string path)
        {
            if (path == null)return (false);
            if (path.Length == 0)return (false);
            if (!File.Exists(path))return (false);

            /* 設定値を読み込む */
            if (!LoadXml(path))return (false);

            return (true);
        }

        public bool LoadXml(Stream stream)
        {
            try {
                var xml_doc = CreateXmlDocument(true);

                xml_doc.Load(stream);

                return (LoadXml(xml_doc));
            } catch {
                return (false);
            }
        }

        private bool LoadXml(string path)
        {
            try {
                var xml_doc = CreateXmlDocument(true);

                /* XMLファイル読み込み */
                xml_doc.Load(path);

                return (LoadXml(xml_doc));
            } catch {
                return (false);
            }
        }

        private bool LoadXml(XmlDocument xml_doc)
        {
            if (xml_doc == null)return (false);

            var xml_root = xml_doc.DocumentElement;

            /* ルート要素名が異なる場合は無視 */
            if (xml_root == null)return (false);
            if (xml_root.Name != config_name_)return (false);

            LoadConfigData(xml_root);

            return (true);
        }

        public bool SaveConfig(string path)
        {
            if (path == null)return (false);
            if (path.Length == 0)return (false);

            return (SaveXmlConfig(path));
        }

        private bool SaveXmlConfig(string path)
        {
            var xml_doc = CreateXmlDocument(false);
            var xml_dec = xml_doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            var xml_root = xml_doc.CreateElement(config_name_);

            xml_doc.AppendChild(xml_dec);
            xml_doc.AppendChild(xml_root);

            /* 要素出力 */
            SaveConfigData(xml_root);

            /* ディレクトリ作成 */
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            /* ファイル保存 */
            xml_doc.Save(path);

            return (true);
        }

        private XmlDocument CreateXmlDocument(bool is_read)
        {
            var xml_doc = new XmlDocument();

            xml_doc.PreserveWhitespace = (is_read) ? (true) : (false);

            return (xml_doc);
        }
    }
}
