using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using Ratatoskr.Actions;
using Ratatoskr.Configs;

namespace Ratatoskr.Configs.UserConfigs
{
    [Serializable]
    internal sealed class KeyPatternConfig
    {
        public bool IsControl { get; set; } = false;
        public bool IsShift   { get; set; } = false;
        public bool IsAlt     { get; set; } = false;

        public Keys  KeyCode  { get; set; } = Keys.None;


        public KeyPatternConfig(bool control, bool shift, bool alt, Keys key)
        {
            IsControl = control;
            IsShift = shift;
            IsAlt = alt;
            KeyCode = key;
        }

        public override string ToString()
        {
            var str = new StringBuilder();

            if (IsControl) {
                str.Append("Ctrl+");
            }

            if (IsShift) {
                str.Append("Shift+");
            }

            if (IsAlt) {
                str.Append("Alt+");
            }

            str.Append(KeyCode.ToString());

            return (str.ToString());
        }
    }

    [Serializable]
    internal sealed class ShortcutDataConfig
    {
        public KeyPatternConfig KeyPattern { get; }

        public ActionShortcutId ActionID { get; }


        public ShortcutDataConfig(bool control, bool shift, bool alt, Keys key, ActionShortcutId id)
        {
            KeyPattern = new KeyPatternConfig(control, shift, alt, key);
            ActionID = id;
        }
    }

    [Serializable]
    internal sealed class ShortcutKeyConfig : IConfigDataReadOnly<List<ShortcutDataConfig>>, IConfigReader, IConfigWriter
    {
        private const string XML_NODE_DATA = "data";


        public List<ShortcutDataConfig> Value { get; } = new List<ShortcutDataConfig>();


        public ShortcutKeyConfig()
        {
            /* ここにショートカットキーの初期設定を追加 */
            var default_map = new [] {
                new { id = ActionShortcutId.ApplicationExit,      control = false, shift = false, alt = true,  key = Keys.F4  },
                new { id = ActionShortcutId.FileOpen,             control = true,  shift = false, alt = false, key = Keys.O   },
                new { id = ActionShortcutId.PacketSaveRuleOff,    control = true,  shift = false, alt = false, key = Keys.S   },
                new { id = ActionShortcutId.PacketSaveAsRuleOff,  control = true,  shift = true,  alt = false, key = Keys.S   },
                new { id = ActionShortcutId.TimeStampRun,         control = false, shift = false, alt = false, key = Keys.F6  },
                new { id = ActionShortcutId.EventPacketClear,     control = false, shift = false, alt = false, key = Keys.F4  },
                new { id = ActionShortcutId.EventPacketRedraw,    control = false, shift = false, alt = false, key = Keys.F5  },
                new { id = ActionShortcutId.ShowAppDocument,      control = false, shift = false, alt = false, key = Keys.F1  },
            };

            /* デフォルトキーマップを適用 */
            foreach (var config in default_map) {
                /* 重複キー情報を削除 */
                Value.RemoveAll(
                    value => (
                           (value.KeyPattern.IsControl == config.control)
                        && (value.KeyPattern.IsShift == config.shift)
                        && (value.KeyPattern.IsAlt == config.alt)
                        && (value.KeyPattern.KeyCode == config.key)));

                /* キー情報を追加 */
                Value.Add(new ShortcutDataConfig(config.control, config.shift, config.alt, config.key, config.id));
            }
        }

        public bool LoadConfigData(XmlElement xml_own)
        {
            /* 現在の情報をクリア */
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

            /* === パラメータ読み込み === */
            /* key */
            var key_pattern = DecodeKeyText(xml_node.GetAttribute("key"));

            /* id */
            var id = (ActionShortcutId)Enum.Parse(typeof(ActionShortcutId), xml_node.GetAttribute("id"));

            if (key_pattern == null)return;

            /* 重複キー情報を削除 */
            Value.RemoveAll(
                value => (
                       (value.KeyPattern.IsControl == key_pattern.IsControl)
                    && (value.KeyPattern.IsShift == key_pattern.IsShift)
                    && (value.KeyPattern.IsAlt == key_pattern.IsAlt)
                    && (value.KeyPattern.KeyCode == key_pattern.KeyCode)));

            /* === 設定リストへ追加 === */
            Value.Add(
                new ShortcutDataConfig(
                    key_pattern.IsControl,
                    key_pattern.IsShift,
                    key_pattern.IsAlt,
                    key_pattern.KeyCode,
                    id));
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            foreach (var info in Value) {
                var xml_data = xml_own.OwnerDocument.CreateElement(XML_NODE_DATA);

                /* === パラメータ書き込み === */
                /* key */
                xml_data.SetAttribute("key", EncodeKeyText(info.KeyPattern));

                /* id */
                xml_data.SetAttribute("id", info.ActionID.ToString());

                /* === ノード追加 === */
                xml_own.AppendChild(xml_data);
            }

            return (true);
        }

        private string EncodeKeyText(KeyPatternConfig config)
        {
            var str = new StringBuilder();

            if (config.IsControl) {
                str.Append("Ctrl+");
            }

            if (config.IsShift) {
                str.Append("Shift+");
            }

            if (config.IsAlt) {
                str.Append("Alt+");
            }

            str.Append(config.KeyCode.ToString());

            return (str.ToString());
        }

        private KeyPatternConfig DecodeKeyText(string text)
        {
            try {
                var shift = false;
                var alt = false;
                var control = false;
                var key = Keys.None;

                foreach (var block in text.Split(new char[] { '+' })) {
                    var value = block.Trim();

                    switch (value) {
                        case "Ctrl":
                            control = true;
                            break;
                        case "Shift":
                            shift = true;
                            break;
                        case "Alt":
                            alt = true;
                            break;
                        default:
                            key = (Keys)Enum.Parse(typeof(Keys), value);
                            break;
                    }
                }

                return (new KeyPatternConfig(control, shift, alt, key));

            } catch {
                return (null);
            }
        }
    }
}
