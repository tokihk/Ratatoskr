using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RtsCore.Config
{
    [Serializable]
    public abstract class ConfigRoot<Type> : ConfigHolder, IConfigRoot
        where Type : class, IConfigRoot, new()
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


        private readonly string config_name_ = null;


        private XmlDocument CreateXmlDocument(bool is_read)
        {
            return (new XmlDocument() {
                PreserveWhitespace = (is_read) ? (true) : (false)
            });
        }

        public ConfigRoot(string name)
        {
            config_name_ = name;
        }

        private bool LoadFromXmlDocument(XmlDocument xml_doc)
        {
            if (xml_doc == null)return (false);

            var xml_root = xml_doc.DocumentElement;

            /* ルート要素名が異なる場合は無視 */
            if (xml_root == null)return (false);
            if (xml_root.Name != config_name_)return (false);

            LoadConfigData(xml_root);

            return (true);
        }

        private bool LoadFromXmlFile(string path)
        {
            try {
                var xml_doc = CreateXmlDocument(true);

                /* XMLファイル読み込み */
                xml_doc.Load(path);

                return (LoadFromXmlDocument(xml_doc));
            } catch {
                return (false);
            }
        }

        public bool LoadFromStream(Stream stream)
        {
            try {
                var xml_doc = CreateXmlDocument(true);

                xml_doc.Load(stream);

                return (LoadFromXmlDocument(xml_doc));
            } catch {
                return (false);
            }
        }

        public bool LoadConfig(string path)
        {
            if (path == null)return (false);
            if (path.Length == 0)return (false);
            if (!File.Exists(path))return (false);

            /* 設定値を読み込む */
            if (!LoadFromXmlFile(path))return (false);

            return (true);
        }

        private bool SaveToXmlDocument(XmlDocument xml_doc)
        {
            var xml_dec = xml_doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            var xml_root = xml_doc.CreateElement(config_name_);

            xml_doc.AppendChild(xml_dec);
            xml_doc.AppendChild(xml_root);

            /* 要素出力 */
            SaveConfigData(xml_root);

            return (true);
        }

        private bool SaveToXmlFile(string path)
        {
            var xml_doc = CreateXmlDocument(false);

            /* XML化 */
            if (!SaveToXmlDocument(xml_doc)) {
                return (false);
            }

            /* ディレクトリ作成 */
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            /* ファイル保存 */
            xml_doc.Save(path);

            return (true);
        }

        public bool SaveToStream(Stream stream)
        {
            var xml_doc = CreateXmlDocument(false);

            /* XML化 */
            if (!SaveToXmlDocument(xml_doc)) {
                return (false);
            }

            /* メモリに保存 */
            xml_doc.Save(stream);

            return (true);
        }

        public bool SaveConfig(string path)
        {
            if (path == null)return (false);
            if (path.Length == 0)return (false);

            return (SaveToXmlFile(path));
        }

        public Type DeepClone()
        {
            var obj_new = new Type();

            using (var stream = new MemoryStream()) {
                /* 現在の値をメモリに保存 */
                SaveToStream(stream);

                stream.Flush();
                stream.Position = 0;

                /* メモリに保存した値を新しいオブジェクトに保存 */
                obj_new.LoadFromStream(stream);
            }

            return (obj_new);
        }

        public void DeepCopy(Type obj)
        {
            using (var stream = new MemoryStream()) {
                /* コピー元の値をメモリに保存 */
                obj.SaveToStream(stream);

                stream.Flush();
                stream.Position = 0;

                /* メモリに保存した値を自身に復元 */
                LoadFromStream(stream);
            }
        }
    }
}
