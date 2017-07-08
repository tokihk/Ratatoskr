using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using Ratatoskr.Configs;

namespace Ratatoskr.Forms.Information
{
    public partial class InformationFrame : Form
    {
        private readonly Font font_license_name_ = new Font("Arial", 14, FontStyle.Bold);
        private readonly Font font_license_homepage_ = new Font("Arial", 10, FontStyle.Italic);


        public InformationFrame()
        {
            InitializeComponent();

            PictBox_Icon.Image = Properties.Resources.app_icon_128x128;
            PictBox_Logo.Image = Properties.Resources.app_logo_300x60;

            UpdateAppVersion();
            UpdateAppCopyright();
            UpdateLicenseList();

            Text = ConfigManager.Language.MainUI.AboutForm_Title.Value;
        }

        private void UpdateAppVersion()
        {
            Label_Version.Text = Program.Version.ToString();
        }

        private void UpdateAppCopyright()
        {
            Label_Copyright.Text = 
                ((System.Reflection.AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(
                    System.Reflection.Assembly.GetExecutingAssembly(), 
                    typeof(System.Reflection.AssemblyCopyrightAttribute))
                ).Copyright;
        }

        private void UpdateLicenseList()
        {
            try {
                var asm = Assembly.GetExecutingAssembly();

                using (var stream = asm.GetManifestResourceStream("Ratatoskr.Forms.Information.LicenseList.xml")) {
                    var xml_doc = new XmlDocument();

                    /* ライセンスリストを読み込み */
                    xml_doc.Load(stream);

                    /* ファイルチェック */
                    var xml_root = xml_doc.DocumentElement;

                    if (xml_root.Name != "license") return;

                    /* ライセンス表示追加 */
                    foreach (XmlElement xml_node in xml_root.GetElementsByTagName("item")) {
                        var name = "";
                        var homepage = "";
                        var license_name = "";

                        name = xml_node.GetAttribute("name");
                        homepage = xml_node.GetAttribute("homepage");
                        license_name = xml_node.GetAttribute("license_name");

                        RTBox_LicenseList.Font = font_license_name_;
                        RTBox_LicenseList.AppendText(name);

                        RTBox_LicenseList.Font = font_license_homepage_;
                        RTBox_LicenseList.AppendText(String.Format("    ({0})", homepage));

                        RTBox_LicenseList.AppendText(Environment.NewLine);
                    }
                }
            } catch { }
        }
    }
}
