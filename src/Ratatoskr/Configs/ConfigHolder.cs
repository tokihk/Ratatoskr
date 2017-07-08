using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ratatoskr.Configs
{
    [Serializable]
    public abstract class ConfigHolder : IConfigReader, IConfigWriter
    {
        public bool LoadConfigData(XmlElement xml_own)
        {
            try {
                foreach (XmlNode xml_node in xml_own.ChildNodes) {

                    if (xml_node.NodeType != XmlNodeType.Element)continue;

                    LoadConfigData_Child(xml_node as XmlElement);
                }

                return (true);

            } catch {
                return (false);
            }
        }

        private bool LoadConfigData_Child(XmlElement xml_child)
        {
            /* プロパティから該当要素を検索 */
            var prop = GetType().GetProperty(xml_child.Name);

            if (prop == null)return (false);

            var prop_obj = prop.GetValue(this);

            if (prop_obj == null)return (false);

            /* 該当要素がIConfigReaderを継承していなければ無視 */
            if (!(prop_obj is IConfigReader))return (false);

            /* 読込用メソッドを取得 */
            var method = prop.PropertyType.GetMethod("LoadConfigData");

            /* 読込用メソッドを呼び出し */
            method.Invoke(prop_obj, new object[]{ xml_child });

            return (true);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            foreach (var prop in GetType().GetProperties()) {
                SaveConfigData_Child(xml_own, prop);
            }

            return (true);
        }

        private bool SaveConfigData_Child(XmlElement xml_own, PropertyInfo prop)
        {
            /* プロパティの実体を取得 */
            var prop_obj = prop.GetValue(this);

            /* プロパティがIConfigDataを継承していなければ無視 */
            if (!(prop_obj is IConfigWriter))return (true);

            /* 保存用メソッドを取得 */
            var method = prop.PropertyType.GetMethod("SaveConfigData");

            /* プロパティ用要素作成 */
            var xml_elem = xml_own.AppendChild(xml_own.OwnerDocument.CreateElement(prop.Name));

            /* 保存用メソッドを呼び出し */
            method.Invoke(prop_obj, new object[]{ xml_elem });

            return (true);
        }
    }
}
