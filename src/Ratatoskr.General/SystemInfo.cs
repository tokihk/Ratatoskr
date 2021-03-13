using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ratatoskr.General
{
    public class SystemInfo
    {
        public static IEnumerable<SystemInfo> ParseFromXmlFile(string path)
        {
            try {
                var xml_doc = new XmlDocument();

                xml_doc.Load(path);

                return (ParseFromXml(xml_doc));

            } catch {
                return (null);
            }
        }

        public static IEnumerable<SystemInfo> ParseFromXml(string xml_text)
        {
            var xml_doc = new XmlDocument();

            xml_doc.LoadXml(xml_text);

            return (ParseFromXml(xml_doc));
        }

        private static List<SystemInfo> ParseFromXml(XmlDocument xml_doc)
        {
            try {
                var xml_root = xml_doc.DocumentElement;

                if (xml_root == null)return (null);
                if (xml_root.Name != "apps")return (null);

                var apps = new List<SystemInfo>();

                /* アプリケーションリスト読込 */
                foreach (XmlElement xml_node in xml_root.GetElementsByTagName("application")) {
                    ParseFromXml_Application(apps, xml_node);
                }

                return (apps);

            } catch {
                return (null);
            }
        }

        private static void ParseFromXml_Application(List<SystemInfo> apps, XmlElement xml_root)
        {
            var vers = new List<ModuleInfo>();

            /* バージョンリスト読み込み */
            foreach (XmlElement xml_node in xml_root.GetElementsByTagName("version")) {
                ParseFromXml_Version(vers, xml_node);
            }

            apps.Add(new SystemInfo(xml_root.GetAttribute("name"), vers.ToArray()));
        }

        private static void ParseFromXml_Version(List<ModuleInfo> vers, XmlElement xml_root)
        {
            if (xml_root == null)return;

            vers.Add(new ModuleInfo(
                xml_root.GetAttribute("download"),
                ushort.Parse(xml_root.GetAttribute("major")),
                ushort.Parse(xml_root.GetAttribute("minor")),
                ushort.Parse(xml_root.GetAttribute("bugfix")),
                xml_root.GetAttribute("model"),
                xml_root.InnerText
            ));
        }


        public SystemInfo(string name, ModuleInfo[] ver_infos)
        {
            Name = name;
            Versions = ver_infos;
        }

        public string       Name     { get; }
        public ModuleInfo[] Versions { get; }
    }
}
