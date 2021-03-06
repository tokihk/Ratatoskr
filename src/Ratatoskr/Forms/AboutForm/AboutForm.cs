﻿using System;
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
using Ratatoskr.Config;
using Ratatoskr.General;
using Ratatoskr.Plugin;

namespace Ratatoskr.Forms.AboutForm
{
    public partial class AboutForm : Form
    {
        private readonly Font FONT_LICENSE_NAME     = new Font("Arial", 14, FontStyle.Bold);
        private readonly Font FONT_LICENSE_HOMEPAGE = new Font("Arial", 10, FontStyle.Italic);
        private readonly Font FONT_LICENSE_DEFAULT  = new Font("Arial", 8, FontStyle.Italic);


        public AboutForm()
        {
            InitializeComponent();

            Disposed += (sender, e) => {
                FONT_LICENSE_DEFAULT.Dispose();
                FONT_LICENSE_HOMEPAGE.Dispose();
                FONT_LICENSE_DEFAULT.Dispose();
            };

            PictBox_Icon.Image = Ratatoskr.Resource.Images.app_icon_128x128;
            PictBox_Logo.Image = Ratatoskr.Resource.Images.app_logo_300x60;

            Text = ConfigManager.Language.MainUI.AboutForm_Title.Value;
            LLabel_HomePage.Text = ConfigManager.Fixed.HomePage.Value;
            Label_Version.Text = Program.Version.ToString();
            Label_Copyright.Text = ConfigManager.Fixed.Copyright.Value;

            UpdateLicenseList();
            UpdatePluginList();
        }

        private void UpdateLicenseList()
        {
            try {
                /* ライセンスファイル読み込み */
                var infos = LicenseFileParser.ParseFromXml(
                                Assembly.GetExecutingAssembly().GetManifestResourceStream(
                                    "Ratatoskr.Forms.AboutForm.LicenseList.xml"));

                /* ライセンスをテキストに変換 */
                foreach (var info in infos) {
                    /* Name */
                    RTBox_LicenseList.SelectionFont = FONT_LICENSE_NAME;
                    RTBox_LicenseList.AppendText(info.Name + Environment.NewLine);

                    /* Homepage */
                    if (info.Homepage.Length > 0) {
                        RTBox_LicenseList.SelectionFont = FONT_LICENSE_HOMEPAGE;
                        RTBox_LicenseList.AppendText("  " + info.Homepage + Environment.NewLine);
                    }

                    RTBox_LicenseList.SelectionFont = FONT_LICENSE_DEFAULT;
                    RTBox_LicenseList.AppendText(Environment.NewLine);
                }
            } catch { }
        }

        private void UpdatePluginList()
        {
            TView_PluginList.BeginUpdate();
            {
                TView_PluginList.Nodes.Clear();
                foreach (var plugin in PluginManager.GetPluginList()) {
                    TView_PluginList.Nodes.Add(CreateNodeFromPlugin(plugin.Class));
                }
            }
            TView_PluginList.EndUpdate();
        }

        private TreeNode CreateNodeFromPlugin(PluginClass plugin)
        {
            var node = new TreeNode()
            {
                Text = string.Format("{0} {1}", plugin.Name, plugin.Version.ToString()),
            };

            node.Nodes.Add(plugin.Details);
            node.Nodes.Add(string.Format("Copyright©{0}", plugin.Copyright));

			if (plugin.ThirdPartyLicenses is LicenseInfo[] infos) {
				foreach (var info in infos) {
					var node_l = new TreeNode(info.Name);

					if ((info.Homepage != null) && (info.Homepage.Length > 0)) {
						node_l.Nodes.Add(info.Homepage);
					}
					if ((info.LicenseName != null) && (info.LicenseName.Length > 0)) {
						node_l.Nodes.Add(info.LicenseName);
					}

					node.Nodes.Add(node_l);
				}
			}

            return (node);
        }

        private void LLabel_HomePage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start((sender as LinkLabel).Text);
        }
    }
}
