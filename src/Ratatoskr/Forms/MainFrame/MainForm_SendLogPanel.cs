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
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.Gate;
using Ratatoskr.Scripts.API;

namespace Ratatoskr.Forms.MainFrame
{
    internal partial class MainForm_SendLogPanel : MainForm_SendPanel
    {
        private API_PlayRecord api_obj_ = new API_PlayRecord();
        private Timer          api_progress_timer_ = new Timer();


        public MainForm_SendLogPanel()
        {
            InitializeComponent();
        }

        public MainForm_SendLogPanel(MainForm_SendPanelContainer panel) : base(panel)
        {
            InitializeComponent();

            api_progress_timer_.Interval = 1000;
            api_progress_timer_.Tick += OnActionProgress;
        }

        public override void LoadConfig()
        {
            LoadDataTypeListConfig();
            LoadLogListConfig();

            CBox_PlayDataType.SelectedItem = ConfigManager.User.SendPanel_Log_PlayDataType.Value;

            UpdateLogListView();
        }

        private void LoadDataTypeListConfig()
        {
            CBox_PlayDataType.BeginUpdate();
            {
                CBox_PlayDataType.Items.Clear();
                foreach (SendLogDataType type in Enum.GetValues(typeof(SendLogDataType))) {
                    CBox_PlayDataType.Items.Add(type);
                }
            }
            CBox_PlayDataType.EndUpdate();
        }

        private void LoadLogListConfig()
        {
            CBox_LogList.BeginUpdate();
            {
                CBox_LogList.Items.Clear();
                foreach (var exp in ConfigManager.User.SendPanel_Log_List.Value) {
                    CBox_LogList.Items.Add(exp);
                }

                /* 先頭のアイテムを選択 */
                if (CBox_LogList.Items.Count > 0) {
                    CBox_LogList.SelectedIndex = 0;
                }
            }
            CBox_LogList.EndUpdate();
        }

        public override void BackupConfig()
        {
            BackupLogListConfig();

            ConfigManager.User.SendPanel_Log_PlayDataType.Value = (SendLogDataType)CBox_PlayDataType.SelectedItem;
        }

        private void BackupLogListConfig()
        {
            ConfigManager.User.SendPanel_Log_List.Value.Clear();

            foreach (string exp in CBox_LogList.Items) {
                ConfigManager.User.SendPanel_Log_List.Value.Add(exp);
            }
        }

        private void UpdateLogListView()
        {
            var file_path = CBox_LogList.Text;

            if (file_path.Length > 0) {
                CBox_LogList.BackColor = (File.Exists(file_path))
                                        ? (Color.LightSkyBlue)
                                        : (Color.LightPink);
            } else {
                CBox_LogList.BackColor = Color.White;
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
            CBox_LogList.Enabled = false;
            Btn_FileSelect.Enabled = false;
            CBox_PlayDataType.Enabled = false;

            var path_file = CBox_LogList.Text;

            /* ファイルが存在しない場合は失敗 */
            if (!File.Exists(path_file)) {
                SendExecComplete(false);
                return;
            }

            /* ターゲットフィルタ */
            var filter = "";

            switch ((SendLogDataType)CBox_PlayDataType.SelectedItem) {
                case SendLogDataType.RecvOnly: filter = "IsRecv";   break;
                case SendLogDataType.SendOnly: filter = "IsSend";   break;
                default:                       filter = "";         break;
            }

            /* 進捗バー更新開始 */
            api_progress_timer_.Start();

            /* 開始 */
            api_obj_.ExecAsync(target, path_file, filter, OnApiCompleted);

            UpdateSendButton();
            UpdateProgressView();
        }

        private void OnApiCompleted(object sender, EventArgs e)
        {
            SendExecComplete(api_obj_.Success);
        }

        private void OnActionProgress(object sender, EventArgs e)
        {
            UpdateProgressView();
        }

        protected override void OnSendExecEnd(bool success)
        {
            AddLog(CBox_LogList.Text);

            /* 進捗バー更新停止 */
            UpdateProgressView();
            api_progress_timer_.Stop();

            /* コンテンツを有効化 */
            CBox_LogList.Enabled = true;
            Btn_FileSelect.Enabled = true;
            CBox_PlayDataType.Enabled = true;

            /* キャンセルボタン非表示 */
            UpdateSendButton();
        }

        private void AddLog(string text)
        {
            CBox_LogList.BeginUpdate();
            {
                var text_now = CBox_LogList.Text;

                /* 重複するコマンドを削除 */
                CBox_LogList.Items.Remove(text);

                /* ログの最大値に合わせて古いログを削除 */
                if (CBox_LogList.Items.Count >= (ConfigManager.User.SendPanelLogLimit.Value - 1)) {
                    CBox_LogList.Items.RemoveAt(CBox_LogList.Items.Count - 1);
                }

                /* 先頭に追加 */
                CBox_LogList.Items.Insert(0, text);

                /* コマンドを復元 */
                CBox_LogList.Text = text_now;
            }
            CBox_LogList.EndUpdate();
        }

        private void CBox_LogList_TextChanged(object sender, EventArgs e)
        {
            UpdateLogListView();
        }

        private void CBox_LogList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                SendExecRequest();
            }
        }

        private void CBox_LogList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void CBox_LogList_DragDrop(object sender, DragEventArgs e)
        {
            var files = e.Data.GetData(DataFormats.FileDrop) as string[];

            if (files == null)return;
            if (files.Length == 0)return;

            /* ファイル転送コマンドに置き換え */
            CBox_LogList.Text = files.First();
        }

        private void Btn_FileSelect_Click(object sender, EventArgs e)
        {
            var init_path = CBox_LogList.Text;

            if (File.Exists(init_path)) {
                init_path = Path.GetDirectoryName(init_path);
            }

            var file_path = FormUiManager.AnyFileOpen(init_path);

            if (file_path == null)return;

            CBox_LogList.Text = file_path;

            UpdateLogListView();
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
