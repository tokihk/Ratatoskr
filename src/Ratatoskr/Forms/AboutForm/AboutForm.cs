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
using Ratatoskr.Configs;
using RtsCore.Utility;

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

            PictBox_Icon.Image = Properties.Resources.app_icon_128x128;
            PictBox_Logo.Image = Properties.Resources.app_logo_300x60;

            Text = ConfigManager.Language.MainUI.AboutForm_Title.Value;
            LLabel_HomePage.Text = ConfigManager.Fixed.HomePage.Value;
            Label_Version.Text = Program.Version.ToString();
            Label_Copyright.Text = ConfigManager.Fixed.Copyright.Value;

            UpdateLicenseList();
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

        private void LLabel_HomePage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start((sender as LinkLabel).Text);
        }
    }
}
