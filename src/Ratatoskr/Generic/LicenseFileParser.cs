using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ratatoskr.Generic
{
    internal static class LicenseFileParser
    {
        public static IEnumerable<LicenseInfo> ParseFromXmlFile(string path)
        {
            try {
                var xml_doc = new XmlDocument();

                xml_doc.Load(path);

                return (ParseFromXml(xml_doc));

            } catch {
                return (null);
            }
        }

        public static IEnumerable<LicenseInfo> ParseFromXml(string xml_text)
        {
            var xml_doc = new XmlDocument();

            xml_doc.LoadXml(xml_text);

            return (ParseFromXml(xml_doc));
        }

        public static IEnumerable<LicenseInfo> ParseFromXml(Stream stream)
        {
            var xml_doc = new XmlDocument();

            xml_doc.Load(stream);

            return (ParseFromXml(xml_doc));
        }

        private static List<LicenseInfo> ParseFromXml(XmlDocument xml_doc)
        {
            try {
                var xml_root = xml_doc.DocumentElement;

                if (xml_root == null)return (null);
                if (xml_root.Name != "license")return (null);

                var infos = new List<LicenseInfo>();

                /* ライセンスリスト読込 */
                ParseFromXml_Root(infos, xml_root);

                return (infos);

            } catch {
                return (null);
            }
        }

        private static void ParseFromXml_Root(List<LicenseInfo> infos, XmlElement xml_root)
        {
            foreach (XmlElement xml_node in xml_root.GetElementsByTagName("item")) {
                ParseFromXml_Item(infos, xml_node);
            }
        }

        private static void ParseFromXml_Item(List<LicenseInfo> infos, XmlElement xml_root)
        {
            if (xml_root == null)return;

            infos.Add(new LicenseInfo(
                xml_root.GetAttribute("name"),
                xml_root.GetAttribute("homepage"),
                xml_root.GetAttribute("license_name"),
                xml_root.InnerText
            ));
        }
    }
}
