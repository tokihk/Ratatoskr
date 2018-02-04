using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Actions;
using Ratatoskr.Actions.ActionModules;
using Ratatoskr.Configs;
using Ratatoskr.Gate;

namespace Ratatoskr.Forms.MainFrame
{
    internal partial class MainFrameSendFilePanel : MainFrameSendPanel
    {
        private ActionObject action_busy_ = null;

        private Timer timer_progress_ = new Timer();


        public MainFrameSendFilePanel() : base(null)
        {
            InitializeComponent();
        }

        public MainFrameSendFilePanel(MainFrameSendPanelContainer panel) : base(panel)
        {
            InitializeComponent();

            timer_progress_.Interval = 1000;
            timer_progress_.Tick += OnActionProgress;

            UpdateFileListView();
        }

        public override void LoadConfig()
        {
            LoadFileListConfig();

            UpdateFileListView();
        }

        private void LoadFileListConfig()
        {
            CBox_FileList.BeginUpdate();
            {
                CBox_FileList.Items.Clear();
                foreach (var exp in ConfigManager.User.SendPanel_FileList.Value) {
                    CBox_FileList.Items.Add(exp);
                }

                /* 先頭のアイテムを選択 */
                if (CBox_FileList.Items.Count > 0) {
                    CBox_FileList.SelectedIndex = 0;
                }
            }
            CBox_FileList.EndUpdate();
        }

        public override void BackupConfig()
        {
            BackupFileListConfig();
        }

        private void BackupFileListConfig()
        {
            ConfigManager.User.SendPanel_FileList.Value.Clear();

            foreach (string exp in CBox_FileList.Items) {
                ConfigManager.User.SendPanel_FileList.Value.Add(exp);
            }
        }

        private void UpdateFileListView()
        {
            var file_path = CBox_FileList.Text;

            if (file_path.Length > 0) {
                CBox_FileList.BackColor = (File.Exists(file_path))
                                        ? (Color.LightSkyBlue)
                                        : (Color.LightPink);
            } else {
                CBox_FileList.BackColor = Color.White;
            }

            var file_size_text = "-";

            if (File.Exists(file_path)) {
                file_size_text = (new FileInfo(file_path)).Length.ToString("#,0");
            }

            Label_FileSize.Text = file_size_text;

            UpdateProgressView();
        }

        private void UpdateProgressView()
        {
            var transfer_value = 0;
            var progress_value = 0;

            if ((action_busy_ != null) && (!action_busy_.IsComplete)) {
                transfer_value = (int)action_busy_.ProgressNow;
                progress_value = (int)(transfer_value / (action_busy_.ProgressMax / 100));
            }

            PBar_Progress.Value = progress_value;
            Label_TransSize.Text = transfer_value.ToString("#,0");
        }

        protected override void OnSendExecBegin(Tuple<string, GateObject[]> target)
        {
            /* コンテンツを無効化 */
            CBox_FileList.Enabled = false;
            Btn_FileSelect.Enabled = false;

            var path_file = CBox_FileList.Text;

            /* ファイルが存在しない場合は失敗 */
            if (!File.Exists(path_file)) {
                SendExecComplete(false);
                return;
            }

            /* アクションオブジェクトを生成 */
            action_busy_ = new Action_SendFile(target.Item1, path_file, (int)Num_BlockSize.Value);

            /* 完了イベントを登録 */
            action_busy_.ActionCompleted += OnActionCompleted;

            /* 進捗バー更新開始 */
            timer_progress_.Start();

            /* アクション要求 */
            ActionManager.AddNormalAction(action_busy_);

            /* キャンセルボタン表示 */
            Btn_Cancel.Show();
        }

        private void OnActionCompleted(object sender, ActionObject.ActionResultType result, ActionParam[] result_values)
        {
            SendExecComplete(result == ActionObject.ActionResultType.Success);
        }

        private void OnActionProgress(object sender, EventArgs e)
        {
            UpdateProgressView();
        }

        protected override void OnSendExecEnd(bool success)
        {
            AddLog(CBox_FileList.Text);

            /* 進捗バー更新停止 */
            UpdateProgressView();
            timer_progress_.Stop();

            /* コンテンツを有効化 */
            CBox_FileList.Enabled = true;
            Btn_FileSelect.Enabled = true;

            /* キャンセルボタン非表示 */
            Btn_Cancel.Hide();
        }

        private void AddLog(string text)
        {
            CBox_FileList.BeginUpdate();
            {
                var text_now = CBox_FileList.Text;

                /* 重複するコマンドを削除 */
                CBox_FileList.Items.Remove(text);

                /* ログの最大値に合わせて古いログを削除 */
                if (CBox_FileList.Items.Count >= (ConfigManager.User.SendPanelLogLimit.Value - 1)) {
                    CBox_FileList.Items.RemoveAt(CBox_FileList.Items.Count - 1);
                }

                /* 先頭に追加 */
                CBox_FileList.Items.Insert(0, text);

                /* コマンドを復元 */
                CBox_FileList.Text = text_now;
            }
            CBox_FileList.EndUpdate();
        }

        private void CBox_FileList_TextChanged(object sender, EventArgs e)
        {
            UpdateFileListView();
        }

        private void CBox_FileList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                SendExecRequest();
            }
        }

        private void CBox_FileList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void CBox_FileList_DragDrop(object sender, DragEventArgs e)
        {
            var files = e.Data.GetData(DataFormats.FileDrop) as string[];

            if (files == null)return;
            if (files.Length == 0)return;

            /* ファイル転送コマンドに置き換え */
            CBox_FileList.Text = files.First();
        }

        private void Btn_FileSelect_Click(object sender, EventArgs e)
        {
            var init_path = CBox_FileList.Text;

            if (File.Exists(init_path)) {
                init_path = Path.GetDirectoryName(init_path);
            }

            var file_path = FormUiManager.AnyFileOpen(init_path);

            if (file_path == null)return;

            CBox_FileList.Text = file_path;

            UpdateFileListView();
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            action_busy_?.Cancel();
        }
    }
}
