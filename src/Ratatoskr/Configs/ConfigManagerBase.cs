﻿using System;
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
    public abstract class ConfigManagerBase<Type> : ConfigHolder
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


        public delegate void ConfigLoadedDelegate(object sender, ConfigLoadedEventHandler e);
        public event ConfigLoadedDelegate    ConfigLoaded;

        public delegate void ConfigSaveReadyDelegate(object sender, ConfigSaveReadyEventHandler e);
        public event ConfigSaveReadyDelegate ConfigSaveReady;


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
            if (!LoadXmlConfig(path))return (false);

            return (true);
        }

        private bool LoadXmlConfig(string path)
        {
            var xml_doc = new XmlDocument();

            /* XMLファイル読み込み */
            xml_doc.Load(path);

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
            var xml_doc = new XmlDocument();
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
    }
}
