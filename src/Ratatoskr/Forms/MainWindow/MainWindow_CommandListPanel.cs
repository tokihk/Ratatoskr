using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config;
using Ratatoskr.Config.Data.User;

namespace Ratatoskr.Forms.MainWindow
{
    internal partial class MainWindow_CommandListPanel : UserControl
    {
        private enum ColumnId
        {
            Enable,
            TargetType,
            CustomTarget,
            Command,
            DelayFixed,
            DelayRandom,
        }


        public MainWindow_CommandListPanel()
        {
            InitializeComponent();

            GView_CmdList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void LoadConfig()
        {
            /* コマンドリスト */
            LoadCommandListHeaderConfig();
            LoadCommandListDataConfig();
        }

        private void LoadCommandListHeaderConfig()
        {
            GView_CmdList.Columns.Clear();

            foreach (ColumnId id in Enum.GetValues(typeof(ColumnId))) {
                switch (id) {
                    case ColumnId.Enable:
                    {
                        var column = new DataGridViewCheckBoxColumn();

                        column.HeaderText = "";
                        column.Width = 40;
                        column.Resizable = DataGridViewTriState.False;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        GView_CmdList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.TargetType:
                    {
                        var column = new DataGridViewComboBoxColumn();

                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderText = "Target type";
                        column.Width = 100;
                        column.Resizable = DataGridViewTriState.False;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        column.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                        column.ValueType = typeof(SendDataTargetType);
                        foreach (var type in Enum.GetValues(typeof(SendDataTargetType))) {
                            column.Items.Add(type);
                        }
                        GView_CmdList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.CustomTarget:
                    {
                        var column = new DataGridViewTextBoxColumn();

                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderText = "Custom target";
                        column.Width = 120;
                        column.Resizable = DataGridViewTriState.False;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        GView_CmdList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.Command:
                    {
                        var column = new DataGridViewTextBoxColumn();

                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderText = "Command";
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        GView_CmdList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.DelayFixed:
                    {
                        var column = new DataGridViewTextBoxColumn();

                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderText = "Fixed delay" + " [msec]";
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.Width = 140;
                        column.Resizable = DataGridViewTriState.False;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        GView_CmdList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.DelayRandom:
                    {
                        var column = new DataGridViewTextBoxColumn();

                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderText = "Random delay" + " [msec]";
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.Width = 140;
                        column.Resizable = DataGridViewTriState.False;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        GView_CmdList.Columns.Add(column);
                    }
                        break;

                    default:
                    {
                        var column = new DataGridViewTextBoxColumn();

                        GView_CmdList.Columns.Add(column);
                    }
                        break;
                }
            }
        }

        private void LoadCommandListDataConfig()
        {
            GView_CmdList.Rows.Clear();
            foreach (var config in ConfigManager.User.SendDataList.Value) {
                AddSendDataConfig(config);
            }
        }

        public void BackupConfig()
        {
            /* コマンドリスト */
            BackupConfig_SendDataList();
        }

        private void BackupConfig_SendDataList()
        {
            ConfigManager.User.SendDataList.Value.Clear();

            foreach (DataGridViewRow row in GView_CmdList.Rows) {
                if (row.IsNewRow)break;

                try {
                    ConfigManager.User.SendDataList.Value.Add(
                        new SendDataConfig(
                            (bool)row.Cells[(int)ColumnId.Enable].Value,
                            (SendDataTargetType)row.Cells[(int)ColumnId.TargetType].Value,
                            row.Cells[(int)ColumnId.CustomTarget].Value as string,
                            row.Cells[(int)ColumnId.Command].Value as string,
                            uint.Parse(row.Cells[(int)ColumnId.DelayFixed].Value as string),
                            uint.Parse(row.Cells[(int)ColumnId.DelayRandom].Value as string)));
                } catch {
                    ConfigManager.User.SendDataList.Value.Add(
                        new SendDataConfig(false, SendDataTargetType.Common, "*", "", 0, 0));
                }
            }
        }

        private void AddSendDataConfig(SendDataConfig config)
        {
            /* 新規行を追加 */
            var row_index = GView_CmdList.Rows.Add();

            if (row_index < 0)return;

            var row_obj = GView_CmdList.Rows[row_index];

            /* 値を書き換え */
            row_obj.Cells[(int)ColumnId.Enable].Value = config.PlayListInclude;
            row_obj.Cells[(int)ColumnId.TargetType].Value = config.SendTargetType;
            row_obj.Cells[(int)ColumnId.CustomTarget].Value = config.SendTargetCustom;
            row_obj.Cells[(int)ColumnId.Command].Value = config.SendData;
            row_obj.Cells[(int)ColumnId.DelayFixed].Value = config.DelayFixed.ToString();
            row_obj.Cells[(int)ColumnId.DelayRandom].Value = config.DelayRandom.ToString();
        }
    }
}
