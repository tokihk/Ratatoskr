using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Actions;
using Ratatoskr.Configs;
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.Scripts;
using Ratatoskr.Scripts.Expression.Parser;
using Ratatoskr.Scripts.Expression;

namespace Ratatoskr.Forms.MainFrame
{
    internal partial class MainFrameSingleCommandPanel : UserControl
    {
//        private ExpressionCommand runner_ = null;
//        private ExpressionCallStack cs_ = null;

        private CommandRunner runner_ = new CommandRunner();

        private ToolTip ttip_target_ = new ToolTip();
        private ToolTip ttip_command_ = new ToolTip();


        public MainFrameSingleCommandPanel()
        {
            InitializeComponent();

            InitializeTooltip();
        }

        private void InitializeTooltip()
        {
            ttip_target_.SetToolTip(
                CBox_TargetList,
@"コマンドのターゲットとなるゲートをワイルドカードで指定してください。
  Ex1: GATE_001
  Ex2: GATE_00*");

            ttip_command_.SetToolTip(
                CBox_CommandList,
@"送信データを16進数で入力してください。シングルクォテーションで囲むと文字列を直接入力できます。
  <>で囲むと文字列の文字コードを指定できます。
  Ex1: 001122334455
  Ex2: 02'test'03
  Ex3: 02<utf8>'あいうえお'03
");
        }

        public void LoadConfig()
        {
            LoadTargetListConfig();
            LoadCommandListConfig();
        }

        private void LoadTargetListConfig()
        {
            CBox_TargetList.BeginUpdate();
            {
                CBox_TargetList.Items.Clear();
                foreach (var target in ConfigManager.User.SingleCommandTarget.Value) {
                    CBox_TargetList.Items.Add(target.Alias);
                }

                /* 先頭のアイテムを選択 */
                if (CBox_TargetList.Items.Count > 0) {
                    CBox_TargetList.SelectedIndex = 0;
                }
            }
            CBox_TargetList.EndUpdate();
        }

        private void LoadCommandListConfig()
        {
            CBox_CommandList.BeginUpdate();
            {
                CBox_CommandList.Items.Clear();
                foreach (var cmd in ConfigManager.User.SingleCommandContents.Value) {
                    CBox_CommandList.Items.Add(cmd.Command);
                }

                /* 先頭のアイテムを選択 */
                if (CBox_CommandList.Items.Count > 0) {
                    CBox_CommandList.SelectedIndex = 0;
                }
            }
            CBox_CommandList.EndUpdate();
        }

        public void BackupConfig()
        {
            BackupTargetListConfig();
            BackupCommandListConfig();
        }

        private void BackupTargetListConfig()
        {
            ConfigManager.User.SingleCommandTarget.Value.Clear();

            foreach (string target in CBox_TargetList.Items) {
                ConfigManager.User.SingleCommandTarget.Value.Add(new SingleCommandTargetObjectConfig(target));
            }
        }

        private void BackupCommandListConfig()
        {
            ConfigManager.User.SingleCommandContents.Value.Clear();

            foreach (string cmd in CBox_CommandList.Items) {
                ConfigManager.User.SingleCommandContents.Value.Add(new SingleCommandObjectConfig(cmd));
            }
        }

        private void UpdateTargetBox()
        {
            /* 表示更新 */
            if (CBox_TargetList.Text.Length > 0) {
                CBox_TargetList.BackColor = Color.LightSkyBlue;
            } else {
                CBox_TargetList.BackColor = Color.LightPink;
            }
        }

        private void UpdateCommandBox()
        {
            var cmd_target = CBox_TargetList.Text;
            var cmd_text = CBox_CommandList.Text;

            if (cmd_text.Length > 0) {
                CBox_CommandList.BackColor = (CommandRunner.FormatCheck(cmd_target, cmd_text))
                                           ? (Color.LightSkyBlue)
                                           : (Color.LightPink);
            } else {
                CBox_CommandList.BackColor = Color.White;
            }
        }

        private void CommandExec()
        {
            var cmd_target = CBox_TargetList.Text;
            var cmd_text = CBox_CommandList.Text;

            runner_.Reset();

            if (!runner_.Exec(cmd_target, cmd_text, CommandExecComplete))return;

            /* コマンド入力を禁止 */
            Btn_CmdCancel.Visible = true;
            CBox_TargetList.Enabled = false;
            CBox_CommandList.Enabled = false;

            /* コマンドリストへ追加 */
            AddCommandLog(cmd_target, cmd_text);
        }

        private void CommandExecComplete(object sender, EventArgs e)
        {
            CommandExecCompleteUi();
        }

        private delegate void CommandExecCompleteUiDelegate();
        private void CommandExecCompleteUi()
        {
            if (InvokeRequired) {
                Invoke(new CommandExecCompleteUiDelegate(CommandExecCompleteUi));
                return;
            }

            /* コマンド入力を許可 */
            Btn_CmdCancel.Visible = false;
            CBox_TargetList.Enabled = true;
            CBox_CommandList.Enabled = true;
            CBox_CommandList.Focus();
        }

        private void AddCommandLog(string target, string text)
        {
            CBox_TargetList.BeginUpdate();
            CBox_CommandList.BeginUpdate();
            {
                var target_now = CBox_TargetList.Text;
                var text_now = CBox_CommandList.Text;

                /* 重複するコマンドを削除 */
                CBox_TargetList.Items.Remove(target);
                CBox_CommandList.Items.Remove(text);

                /* ログの最大値に合わせて古いログを削除 */
                if (CBox_TargetList.Items.Count >= (ConfigManager.User.SingleCommandLogLimit.Value - 1)) {
                    CBox_TargetList.Items.RemoveAt(CBox_TargetList.Items.Count - 1);
                }
                if (CBox_CommandList.Items.Count >= (ConfigManager.User.SingleCommandLogLimit.Value - 1)) {
                    CBox_CommandList.Items.RemoveAt(CBox_CommandList.Items.Count - 1);
                }

                /* 先頭に追加 */
                CBox_TargetList.Items.Insert(0, target);
                CBox_CommandList.Items.Insert(0, text);

                /* コマンドを復元 */
                CBox_TargetList.Text = target_now;
                CBox_CommandList.Text = text_now;
            }
            CBox_CommandList.EndUpdate();
            CBox_TargetList.EndUpdate();
        }

        private void CBox_TargetList_KeyDown(object sender, KeyEventArgs e)
        {
            /* Beep音がなるのを防ぐ */
//            e.SuppressKeyPress = true;

            UpdateCommandBox();

            if ((runner_ != null) && (e.KeyCode == Keys.Enter)) {
                CommandExec();
            }
        }

        private void CBox_CommandList_KeyDown(object sender, KeyEventArgs e)
        {
            /* Beep音がなるのを防ぐ */
//            e.SuppressKeyPress = true;

            UpdateCommandBox();

            if ((runner_ != null) && (e.KeyCode == Keys.Enter)) {
                CommandExec();
            }
        }

        private void Btn_CmdCancel_Click(object sender, EventArgs e)
        {
            runner_.Cancel();
        }

        private void CBox_CommandList_OnTextChanged(object sender, EventArgs e)
        {
            UpdateCommandBox();
        }

        private void CBox_TargetList_TextChanged(object sender, EventArgs e)
        {
            UpdateTargetBox();
        }

        private void CBox_CommandList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void CBox_CommandList_DragDrop(object sender, DragEventArgs e)
        {
            var files = e.Data.GetData(DataFormats.FileDrop) as string[];

            if (files == null)return;
            if (files.Length == 0)return;

            /* ファイル転送コマンドに置き換え */
            CBox_CommandList.Text = String.Format("#SendFile(\"${{target}}\",\"{0:G}\",1024)", files.First());
        }

        private void Btn_FileFunction_Click(object sender, EventArgs e)
        {
            CMenu_FileFunction.Show(
                PointToScreen(
                    new Point(
                        GBox_CommandList.Location.X + Btn_FileFunction.Location.X + Btn_FileFunction.Size.Width - CMenu_FileFunction.Width,
                        GBox_CommandList.Location.Y + Btn_FileFunction.Location.Y + Btn_FileFunction.Size.Height)));
        }

        private void Menu_FileFunc_Transfer_Click(object sender, EventArgs e)
        {
            var file = FormUiManager.AnyFileOpen();

            if (file == null)return;

            /* ファイル転送コマンドに置き換え */
            CBox_CommandList.Text = String.Format("#SendFile(\"${{target}}\",\"{0:G}\",1024)", file);
        }
    }
}
