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
using Ratatoskr.Api;
using Ratatoskr.Config;

namespace Ratatoskr.Forms.MainWindow
{
    internal partial class MainWindow_SendFilePanel : MainWindow_SendPanel
    {
        private ApiTask_SendFile api_obj_ = null;
        private Timer        api_progress_timer_ = new Timer();


        public MainWindow_SendFilePanel()
        {
            InitializeComponent();
        }

        public MainWindow_SendFilePanel(MainWindow_SendPanelContainer panel) : base(panel)
        {
            InitializeComponent();

            api_progress_timer_.Interval = 1000;
            api_progress_timer_.Tick += OnActionProgress;
        }

        public override void LoadConfig()
        {
            LoadFileListConfig();

            Num_SendBlockSize.Value = ConfigManager.User.SendPanel_File_BlockSize.Value;
            Num_SendDelay.Value = ConfigManager.User.SendPanel_File_SendDelay.Value;

            UpdateFileListView();
        }

        private void LoadFileListConfig()
        {
            CBox_FileList.BeginUpdate();
            {
                CBox_FileList.Items.Clear();
                foreach (var exp in ConfigManager.User.SendPanel_File_List.Value) {
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

            ConfigManager.User.SendPanel_File_BlockSize.Value = Num_SendBlockSize.Value;
            ConfigManager.User.SendPanel_File_SendDelay.Value = Num_SendDelay.Value;
        }

        private void BackupFileListConfig()
        {
            ConfigManager.User.SendPanel_File_List.Value.Clear();

            foreach (string exp in CBox_FileList.Items) {
                ConfigManager.User.SendPanel_File_List.Value.Add(exp);
            }
        }

        private void UpdateFileListView()
        {
            var file_path = CBox_FileList.Text;

            if (file_path.Length > 0) {
                CBox_FileList.BackColor = (File.Exists(file_path))
                                        ? (Ratatoskr.Resource.AppColors.Ok)
                                        : (Ratatoskr.Resource.AppColors.Ng);
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

            if ((api_obj_ != null) && (api_obj_.IsRunning)) {
                transfer_value = (int)api_obj_.ProgressNow;
                progress_value = (int)(transfer_value / Math.Max(1, api_obj_.ProgressMax / 100));
            }

            PBar_Progress.Value = Math.Min(PBar_Progress.Maximum, progress_value);
            Label_TransSize.Text = transfer_value.ToString("#,0");
        }

        private void UpdateSendButton()
        {
            if (IsSendBusy) {
                Btn_Send.Text = "Cancel";
            } else {
                Btn_Send.Text = "Send";
            }            
        }

        protected override void OnSendExecBegin(string target)
        {
            /* コンテンツを無効化 */
            CBox_FileList.Enabled = false;
            Btn_FileSelect.Enabled = false;

            var file_path = CBox_FileList.Text;

            /* ファイルが存在しない場合は失敗 */
            if (!File.Exists(file_path)) {
                SendExecComplete(false);
                return;
            }

            /* 送信開始 */
            api_obj_ = Program.API.API_SendFileAsync(target, file_path, (uint)Num_SendBlockSize.Value, (uint)Num_SendDelay.Value);
            if (api_obj_ == null) {
                SendExecComplete(false);
                return;
            }

            /* 監視タスク実行 */
            (new MethodInvoker(OnSendWatchTask)).BeginInvoke(null, null);

            /* UI更新 */
            UpdateSendButton();

            /* 進捗バー更新開始 */
            api_progress_timer_.Start();
        }

        private void OnSendWatchTask()
        {
            api_obj_.Join();

            SendExecComplete(api_obj_.Success);
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
            api_progress_timer_.Stop();

            /* コンテンツを有効化 */
            CBox_FileList.Enabled = true;
            Btn_FileSelect.Enabled = true;

            UpdateSendButton();
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
                SendExec();
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
            if (e.Data.GetData(DataFormats.FileDrop) is string[] file_paths) {
                if (file_paths.Length > 0) {
                    /* ファイル転送コマンドに置き換え */
                    CBox_FileList.Text = file_paths.First();
                }
            }
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

        private void Btn_Send_Click(object sender, EventArgs e)
        {
            if ((api_obj_ != null) && (api_obj_.IsRunning)) {
                api_obj_.Stop();
            } else {
                SendExec();
            }
        }
    }
}
