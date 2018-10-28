#define BINARY_CODE_PARSER

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Forms.Controls;
using Ratatoskr.Resources;
using Ratatoskr.Scripts.BinaryCodeBuilder;
using Ratatoskr.Utility;

namespace Ratatoskr.Forms.MainWindow
{
    internal partial class MainWindow_SendDataPanel : MainWindow_SendPanel
    {
        private readonly Size  PREVIEW_SIZE = new Size(600, 230);
        private readonly Point PREVIEW_DISP_MARGIN = new Point(10, -10);


        private PreviewLabel preview_label_ = new PreviewLabel();
        private ToolTip ttip_command_ = new ToolTip();

        private string send_data_exp_ = null;
        private byte[] send_data_bin_ = null;


        public MainWindow_SendDataPanel() : base(null)
        {
            InitializeComponent();
        }

        public MainWindow_SendDataPanel(MainWindow_SendPanelContainer panel) : base(panel)
        {
            InitializeComponent();
            InitializePreviewWindow();
        }

        private void InitializePreviewWindow()
        {
            preview_label_.Hide();
            preview_label_.Size = PREVIEW_SIZE;
            preview_label_.Opacity = 0.7;
            preview_label_.Label_Font = new Font("MS Gothic", 9);
            preview_label_.BackColor = Color.LightCyan;
        }

        public override void LoadConfig()
        {
            LoadExpListConfig();

            ChkBox_Preview.Checked = ConfigManager.User.SendPanel_Data_Preview.Value;

            UpdateTooltipExplanation();
            UpdateExpListView();
            UpdatePreviewWindow();
        }

        private void LoadExpListConfig()
        {
            CBox_ExpList.BeginUpdate();
            {
                CBox_ExpList.Items.Clear();
                foreach (var exp in ConfigManager.User.SendPanel_Data_List.Value) {
                    CBox_ExpList.Items.Add(exp);
                }

                /* 先頭のアイテムを選択 */
                if (CBox_ExpList.Items.Count > 0) {
                    CBox_ExpList.SelectedIndex = 0;
                }
            }
            CBox_ExpList.EndUpdate();
        }

        public override void BackupConfig()
        {
            BackupExpListConfig();

            ConfigManager.User.SendPanel_Data_Preview.Value = ChkBox_Preview.Checked;
        }

        private void BackupExpListConfig()
        {
            ConfigManager.User.SendPanel_Data_List.Value.Clear();

            foreach (string exp in CBox_ExpList.Items) {
                ConfigManager.User.SendPanel_Data_List.Value.Add(exp);
            }
        }

        public override void OnMainFormDeactivated()
        {
            preview_label_.Hide();
        }

        private void UpdateTooltipExplanation()
        {
            ttip_command_.SetToolTip(
                CBox_ExpList,
@"Enter the transmission data in hexadecimal.
  Enclose the string in single quotation marks.
  Enclose it with <> to specify the character code of the character string.
  Ex) 001122334455
  Ex) 02'test'03 => 027465737403
  Ex) 02<utf-8>'あいうえお'03 => 02E38182E38184E38186E38188E3818A03
");
        }

        private void UpdateExpListView()
        {
            send_data_exp_ = CBox_ExpList.Text;

            if (send_data_exp_.Length > 0) {
#if BINARY_CODE_PARSER
                send_data_bin_ = BinaryCodeCompiler.Run(send_data_exp_);
#else
                send_data_bin_ = HexTextEncoder.ToByteArray(send_data_exp_);
#endif

                CBox_ExpList.BackColor = (send_data_bin_ != null)
                                       ? (AppColors.PATTERN_OK)
                                       : (AppColors.PATTERN_NG);
            } else {
                send_data_bin_ = null;

                CBox_ExpList.BackColor = Color.White;
            }
        }

        protected override void OnSendExecBegin(string target)
        {
            /* コンテンツを無効化 */
//            CBox_ExpList.Enabled = false;

            /* データを生成できない場合は失敗 */
            if (send_data_bin_ == null) {
                SendExecComplete(false);
                return;
            }

            /* 送信実行 */
            Program.API.API_SendData(target, send_data_bin_);

            SendExecComplete(true);
        }

        protected override void OnSendExecEnd(bool success)
        {
            AddLog(CBox_ExpList.Text);

            /* コンテンツを有効化 */
//            CBox_ExpList.Enabled = true;

            /* フォーカスを戻す */
//            CBox_ExpList.Focus();
        }

        private void AddLog(string text)
        {
            CBox_ExpList.BeginUpdate();
            {
                var text_now = CBox_ExpList.Text;

                /* 重複するコマンドを削除 */
                CBox_ExpList.Items.Remove(text);

                /* ログの最大値に合わせて古いログを削除 */
                if (CBox_ExpList.Items.Count >= (ConfigManager.User.SendPanelLogLimit.Value - 1)) {
                    CBox_ExpList.Items.RemoveAt(CBox_ExpList.Items.Count - 1);
                }

                /* 先頭に追加 */
                CBox_ExpList.Items.Insert(0, text);

                /* コマンドを復元 */
                CBox_ExpList.Text = text_now;
            }
            CBox_ExpList.EndUpdate();
        }

        private void UpdatePreviewWindow()
        {
            var visible = (ChkBox_Preview.Checked && CBox_ExpList.Focused);
#if true
            if (preview_label_.Visible != visible) {
                preview_label_.Visible = visible;
            }
            
            if (visible) {
                var view_pos = CBox_ExpList.PointToScreen(Point.Empty);

                view_pos.X += PREVIEW_DISP_MARGIN.X;
                view_pos.Y += PREVIEW_DISP_MARGIN.Y - preview_label_.Height;

                preview_label_.Location = view_pos;

                var view_str = new StringBuilder();
                var view_data = (send_data_bin_ != null) ? (HexTextEncoder.ToHexText(send_data_bin_, " ")) : ("");

                view_str.AppendLine(string.Format("<Data Preview> size = {0} bytes", (send_data_bin_ != null) ? (send_data_bin_.Length) : (0)));
                view_str.AppendLine((send_data_bin_ != null) ? (HexTextEncoder.ToHexText(send_data_bin_, " ")) : ("Format error."));

                preview_label_.Label_Text = view_str.ToString();
            }
#endif
#if false

            if (visible) {
                ttip_preview_.SetToolTip(CBox_ExpList, HexTextEncoder.ToHexText(send_data_bin_));
                ttip_preview_.Active = true;
            } else {
                ttip_preview_.Active = false;
            }
#endif
        }

        private void CBox_Exp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                SendExec();
            }
        }

        private void CBox_ExpList_TextChanged(object sender, EventArgs e)
        {
            UpdateExpListView();
            UpdatePreviewWindow();
        }

        private void ChkBox_Preview_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePreviewWindow();
        }

        private void CBox_ExpList_Enter(object sender, EventArgs e)
        {
            UpdatePreviewWindow();
        }

        private void CBox_ExpList_Leave(object sender, EventArgs e)
        {
            UpdatePreviewWindow();
        }

        private void Btn_Send_Click(object sender, EventArgs e)
        {
            SendExec();
        }
    }
}
