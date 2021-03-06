﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config;
using Ratatoskr.Config.Data.User;
using Ratatoskr.Gate;
using Ratatoskr.Device;

namespace Ratatoskr.Forms.MainWindow
{
    internal partial class MainWindow_SendPanelContainer : UserControl
    {
        private MainWindow_SendPanel[] SDPanel_List = new MainWindow_SendPanel[Enum.GetValues(typeof(SendPanelType)).Length];
        private MainWindow_SendPanel   SDPanel_Busy;

        private ToolTip ttip_target_ = new ToolTip();

        private List<string> target_log_list_ = new List<string>();


        public MainWindow_SendPanelContainer()
        {
            InitializeComponent();

            Disposed += OnDisposed;
            GateObject.AnyStatusChanged += GateObject_AnyStatusChanged;

            SDPanel_List[(int)SendPanelType.Data] = new MainWindow_SendDataPanel(this);
            SDPanel_List[(int)SendPanelType.File] = new MainWindow_SendFilePanel(this);
            SDPanel_List[(int)SendPanelType.Log] = new MainWindow_SendLogPanel(this);
        }

        private void OnDisposed(object sender, EventArgs e)
        {
            GateObject.AnyStatusChanged -= GateObject_AnyStatusChanged;
        }

        public void LoadConfig()
        {
            LoadToolTipExplanation();
            LoadTargetListConfig();
            LoadSendPanelTypeConfig();

            /* コンテンツ設定を読込 */
            foreach (var panel in SDPanel_List) {
                panel.LoadConfig();
            }

            UpdateSendPanelContents();
        }

        private void LoadToolTipExplanation()
        {
            ttip_target_.SetToolTip(
                CBox_TargetList,
@"Specify the command target with a wildcard.
  Ex) GATE_001
  Ex) GATE_00*");
        }

        private void LoadTargetListConfig()
        {
            target_log_list_.Clear();
            target_log_list_.AddRange(ConfigManager.User.SendPanelTargetList.Value);

            UpdateTargetList();

            if (target_log_list_.Count > 0) {
                /* ログがある場合はログの先頭のアイテムを選択 */
                CBox_TargetList.Text = target_log_list_.First();
            } else if (CBox_TargetList.Items.Count > 0) {
                /* ログが存在しない場合は先頭のアイテムを選択 */
                CBox_TargetList.SelectedIndex = 0;
            }
        }

        private void LoadSendPanelTypeConfig()
        {
            switch (ConfigManager.User.SendPanelType.Value) {
                case SendPanelType.Data:    RBtn_ModeData.Checked = true;   break;
                case SendPanelType.File:    RBtn_ModeFile.Checked = true;   break;
                case SendPanelType.Log:     RBtn_ModeLog.Checked = true;    break;
                default:                                                    break;
            }
        }

        public void BackupConfig()
        {
            BackupTargetListConfig();
            BackupSendPanelTypeConfig();

            /* コンテンツ設定をバックアップ */
            foreach (var panel in SDPanel_List) {
                panel.BackupConfig();
            }
        }

        private void BackupTargetListConfig()
        {
            ConfigManager.User.SendPanelTargetList.Value.Clear();

            foreach (string target in target_log_list_) {
                ConfigManager.User.SendPanelTargetList.Value.Remove(target);
                ConfigManager.User.SendPanelTargetList.Value.Add(target);
            }
        }

        private void BackupSendPanelTypeConfig()
        {
            if (RBtn_ModeData.Checked) {
                ConfigManager.User.SendPanelType.Value = SendPanelType.Data;
            } else if (RBtn_ModeFile.Checked) {
                ConfigManager.User.SendPanelType.Value = SendPanelType.File;
            } else if (RBtn_ModeLog.Checked) {
                ConfigManager.User.SendPanelType.Value = SendPanelType.Log;
            } else {
                ConfigManager.User.SendPanelType.Value = SendPanelType.Data;
            }
        }

        public string SendTarget
        {
            get { return (CBox_TargetList.Text); }
        }

        private void UpdateTargetList()
        {
            CBox_TargetList.BeginUpdate();
            {
                var target_now = CBox_TargetList.Text;

                CBox_TargetList.ClearItemGroup();

                var group = (ComboBoxEx.ItemGroup)null;

                /* ゲート一覧のターゲットリスト */
                group = CBox_TargetList.AddItemGroup();
                group.Font = new Font(CBox_TargetList.Font.Name, CBox_TargetList.Font.Size, FontStyle.Bold);
                group.Items.Add("*");
                foreach (var gate in GateManager.GetGateList()) {
                    group.Items.Add(gate.Alias);
                }

                /* 過去設定値 */
                group = CBox_TargetList.AddItemGroup();
                foreach (var target in target_log_list_) {
                    group.Items.Add(target);
                }

                CBox_TargetList.Text = target_now;
            }
            CBox_TargetList.EndUpdate();
        }

        private void UpdateTargetView()
        {
            if (InvokeRequired) {
                Invoke((MethodInvoker)UpdateTargetView);
                return;
            }

            /* 表示更新 */
            if (CBox_TargetList.Text.Length > 0) {
                CBox_TargetList.BackColor = (GateManager.FindGateObjectFromWildcardAlias(CBox_TargetList.Text).Any(gate => gate.ConnectStatus == ConnectState.Connected))
                                          ? (Ratatoskr.Resource.AppColors.Ok)
                                          : (Ratatoskr.Resource.AppColors.Warning);
            } else {
                CBox_TargetList.BackColor = Ratatoskr.Resource.AppColors.Ng;
            }
        }

        private void UpdateSendPanelContents()
        {
            /* モード毎の制御パネルを取得 */
            SDPanel_Busy = SDPanel_List[(int)ConfigManager.User.SendPanelType.Value];

            /* パネルコンテンツを入れ替え */
            Panel_Contents.Controls.Clear();

            if (SDPanel_Busy != null) {
                SDPanel_Busy.Dock = DockStyle.Fill;
                Panel_Contents.Controls.Add(SDPanel_Busy);
            }
        }

        private void SendExecRequest()
        {
            /* コンテンツ経由で送信実行 */
            SDPanel_Busy?.SendExec();
        }

        public (string target_alias, GateObject[] target_gates) SendExecBegin()
        {
            /* モード選択ボタンを無効化 */
            RBtn_ModeData.Enabled = false;
            RBtn_ModeFile.Enabled = false;
            RBtn_ModeLog.Enabled = false;

            /* ターゲット編集ボックスを無効化 */
            CBox_TargetList.Enabled = false;

            return (CBox_TargetList.Text, GateManager.FindGateObjectFromWildcardAlias(CBox_TargetList.Text));
        }

        public void SendExecEnd(bool success)
        {
            /* 成功時のみログに残す */
            if (success) {
                AddTargetLog(CBox_TargetList.Text);
            }

            /* モード選択ボタンを有効化 */
            RBtn_ModeData.Enabled = true;
            RBtn_ModeFile.Enabled = true;
            RBtn_ModeLog.Enabled = true;

            /* ターゲット編集ボックスを有効化 */
            CBox_TargetList.Enabled = true;
        }

        private void AddTargetLog(string target)
        {
            /* 重複するコマンドを削除 */
            target_log_list_.Remove(target);

            /* ログの最大値に合わせて古いログを削除 */
            if (target_log_list_.Count >= (ConfigManager.User.SendPanelLogLimit.Value - 1)) {
                target_log_list_.RemoveAt(target_log_list_.Count - 1);
            }

            /* 先頭に追加 */
            target_log_list_.Insert(0, target);
        }

        public void OnMainFormDeactivated()
        {
            foreach (var panel in SDPanel_List) {
                panel.OnMainFormDeactivated();
            }
        }

        private void CBox_TargetList_DropDown(object sender, EventArgs e)
        {
            UpdateTargetList();
        }

        private void CBox_TargetList_KeyDown(object sender, KeyEventArgs e)
        {
            /* Beep音がなるのを防ぐ */
//            e.SuppressKeyPress = true;

            if (e.KeyCode == Keys.Enter) {
                SendExecRequest();
            }
        }

        private void CBox_TargetList_TextChanged(object sender, EventArgs e)
        {
            UpdateTargetView();
        }

        private void Mode_CheckedChanged(object sender, EventArgs e)
        {
            BackupSendPanelTypeConfig();

            UpdateSendPanelContents();
        }

        private void GateObject_AnyStatusChanged(object sender, EventArgs e)
        {
            UpdateTargetView();
        }
    }
}
