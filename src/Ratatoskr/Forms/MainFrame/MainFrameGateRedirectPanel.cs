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
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.Gate;
using Ratatoskr.Generic;

namespace Ratatoskr.Forms.MainFrame
{
    internal partial class MainFrameGateRedirectPanel : UserControl
    {
        private enum ColumnId { Enable, Input, Output }


        private bool system_busy_ = false;


        public MainFrameGateRedirectPanel()
        {
            InitializeComponent();
            InitializeRedirectList();
        }

        private void InitializeRedirectList()
        {
            GView_RedirectList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (ColumnId id in Enum.GetValues(typeof(ColumnId))) {
                switch (id) {
                    case ColumnId.Enable:
                    {
                        var column = new DataGridViewCheckBoxColumn();

                        column.HeaderText = "有効/無効";
                        column.Width = 100;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        column.Resizable = DataGridViewTriState.False;
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        GView_RedirectList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.Input:
                    {
                        var column = new DataGridViewTextBoxColumn();

                        column.HeaderText = "監視ゲート";
                        column.Width = 150;
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        GView_RedirectList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.Output:
                    {
                        var column = new DataGridViewTextBoxColumn();

                        column.HeaderText = "転送先ゲート";
                        column.Width = 150;
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        GView_RedirectList.Columns.Add(column);
                    }
                        break;

                    default:
                    {
                        var column = new DataGridViewTextBoxColumn();

                        GView_RedirectList.Columns.Add(column);
                    }
                        break;
                }
            }
        }

        public void LoadConfig()
        {
            system_busy_ = true;

            /* コマンドリスト */
            GView_RedirectList.Rows.Clear();
            foreach (var config in ConfigManager.User.GateRedirectList.Value) {
                AddRule(config.Enable, config.Input, config.Output);
            }

            /* 設定を適用 */
            GateRedirectManager.LoadConfig();

            system_busy_ = false;
        }

        public void BackupConfig()
        {
            /* コマンドリスト */
            ConfigManager.User.GateRedirectList.Value.Clear();

            foreach (DataGridViewRow row in GView_RedirectList.Rows) {
                /* 新規行は無視 */
                if (row.IsNewRow)break;

                try {
                    ConfigManager.User.GateRedirectList.Value.Add(
                        new GateRedirectConfig(
                            (bool)row.Cells[(int)ColumnId.Enable].Value,
                            row.Cells[(int)ColumnId.Input].Value as string,
                            row.Cells[(int)ColumnId.Output].Value as string));
                } catch {
                    ConfigManager.User.GateRedirectList.Value.Add(
                        new GateRedirectConfig(false, "", ""));
                }
            }
        }

        private void AddRule(bool enable, string input, string output)
        {
            var row_index = GView_RedirectList.Rows.Add();

            if (row_index < 0)return;

            var row_obj = GView_RedirectList.Rows[row_index];

            row_obj.Cells[(int)ColumnId.Enable].Value = enable;
            row_obj.Cells[(int)ColumnId.Input].Value = input;
            row_obj.Cells[(int)ColumnId.Output].Value = output;
        }

        private void GView_RedirectList_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[(int)ColumnId.Enable].Value = false;
            e.Row.Cells[(int)ColumnId.Input].Value = "";
            e.Row.Cells[(int)ColumnId.Output].Value = "";
        }

        private void GView_RedirectList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (system_busy_)return;

            BackupConfig();

            GateRedirectManager.LoadConfig();
        }
    }
}
