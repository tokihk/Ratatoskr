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
using Ratatoskr.Configs;
using Ratatoskr.Gate;
using Ratatoskr.Scripts.API;

namespace Ratatoskr.Forms.MainFrame
{
    internal partial class MainForm_SendFilePanel : MainForm_SendPanel
    {
        private API_SendFile api_obj_ = new API_SendFile();
        private Timer        api_progress_timer_ = new Timer();


        public MainForm_SendFilePanel()
        {
            InitializeComponent();
        }

        public MainForm_SendFilePanel(MainForm_SendPanelContainer panel) : base(panel)
        {
            InitializeComponent();

            api_progress_timer_.Interval = 1000;
            api_progress_timer_.Tick += OnActionProgress;
        }

        public override void LoadConfig()
        {
            LoadFileListConfig();

            Num_BlockSize.Value = ConfigManager.User.SendPanel_File_BlockSize.Value;

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

            ConfigManager.User.SendPanel_File_BlockSize.Value = Num_BlockSize.Value;
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

            if ((api_obj_ != null) && (api_obj_.IsBusy)) {
                transfer_value = (int)api_obj_.ProgressNow;
                progress_value = (int)(transfer_value / Math.Max(1, api_obj_.ProgressMax / 100));
            }

            PBar_Progress.Value = Math.Min(PBar_Progress.Maximum, progress_value);
            Label_TransSize.Text = transfer_value.ToString("#,0");
        }

        private void UpdateSendButton()
        {
            if ((api_obj_ != null) && (api_obj_.IsBusy)) {
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

            var path_file = CBox_FileList.Text;

            /* ファイルが存在しない場合は失敗 */
            if (!File.Exists(path_file)) {
                SendExecComplete(false);
                return;
            }

            /* 進捗バー更新開始 */
            api_progress_timer_.Start();

            /* 送信開始 */
            api_obj_.ExecAsync(target, path_file, (uint)Num_BlockSize.Value, OnExecCompleted);

            UpdateSendButton();
        }

        private void OnExecCompleted(object sender, EventArgs e)
        {
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

        private void Btn_Send_Click(object sender, EventArgs e)
        {
            if ((api_obj_ != null) && (api_obj_.IsBusy)) {
                api_obj_.CancelRequest();
            } else {
                SendExecRequest();
            }
        }
    }
}
