using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Scripts;
using Ratatoskr.Scripts.ScriptEngines;

namespace Ratatoskr.Forms.ScriptWindow
{
    internal class ScriptWindow_ScriptList : DataGridView
    {
        private const int UPDATE_DELAY = 100;

        private enum ColumnId
        {
            Mode,
            Status,
            ScriptInfo,
        }


        private bool  update_forbid_ = false;
        private bool  update_busy_ = false;
        private Timer update_timer_ = new Timer();


        public ScriptWindow_ScriptList()
        {
            Disposed += OnDisposed;

            update_timer_.Interval = UPDATE_DELAY;
            update_timer_.Tick += OnUpdateScriptList;

            MultiSelect = false;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            AllowUserToResizeColumns = false;
            AllowUserToResizeRows = false;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToOrderColumns = false;

            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            ScriptManager.ControllerListUpdated += OnScriptControllerListUpdated;
            ScriptManager.ControllerStatusUpdated += OnScriptControllerStatusUpdated;
        }

        private void OnDisposed(object sender, EventArgs e)
        {
            ScriptManager.ControllerListUpdated -= OnScriptControllerListUpdated;
            ScriptManager.ControllerStatusUpdated -= OnScriptControllerStatusUpdated;
        }

        public void LoadConfig()
        {
            LoadScriptListHeaderConfig();
        }

        public void BackupConfig()
        {
        }

        private void LoadScriptListHeaderConfig()
        {
            Columns.Clear();

            foreach (ColumnId id in Enum.GetValues(typeof(ColumnId))) {
                switch (id) {
                    case ColumnId.Mode:
                    {
                        var column = new DataGridViewComboBoxColumn();

                        column.HeaderText = "Mode";
                        column.Width = 100;
                        column.Resizable = DataGridViewTriState.False;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.ValueType = typeof(ScriptRunMode);
                        column.DataSource = Enum.GetValues(typeof(ScriptRunMode));
                        Columns.Add(column);
                    }
                        break;

                    case ColumnId.Status:
                    {
                        var column = new DataGridViewComboBoxColumn();

                        column.HeaderText = "Status";
                        column.Width = 100;
                        column.Resizable = DataGridViewTriState.False;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.ValueType = typeof(ScriptRunStatus);
                        column.DataSource = Enum.GetValues(typeof(ScriptRunStatus));
                        Columns.Add(column);
                    }
                        break;

                    case ColumnId.ScriptInfo:
                    {
                        var column = new DataGridViewTextBoxColumn();
                        
                        column.ReadOnly = true;
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderText = "Script Information";
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        Columns.Add(column);
                    }
                        break;

                    default:
                    {
                        Columns.Add(new DataGridViewTextBoxColumn());
                    }
                        break;
                }
            }
        }

        private void UpdateScriptControllerStatus(DataGridViewRow row_obj, ScriptController controller = null)
        {
            /* nullの場合はROWオブジェクトから復元 */
            if (controller == null) {
                controller = row_obj.Tag as ScriptController;
                if (controller == null)return;
            }

            foreach (DataGridViewCell cell in row_obj.Cells) {
                switch ((ColumnId)cell.ColumnIndex) {
                    case ColumnId.Mode:
                        cell.Value = controller.Mode;
                        break;
                    case ColumnId.Status:
                        cell.Value = controller.Status;
                        break;
                    case ColumnId.ScriptInfo:
                        cell.Value = controller.ToString();
                        break;
                }
            }

            switch (controller.Status) {
                case ScriptRunStatus.Running: row_obj.DefaultCellStyle.BackColor = Color.LightSkyBlue;    break;
                case ScriptRunStatus.Pause:   row_obj.DefaultCellStyle.BackColor = Color.LightYellow;     break;
                default:                      row_obj.DefaultCellStyle.BackColor = Color.White;           break;
            }

            row_obj.DefaultCellStyle.ForeColor = Color.Black;
            row_obj.DefaultCellStyle.SelectionForeColor = row_obj.DefaultCellStyle.ForeColor;
            row_obj.DefaultCellStyle.SelectionBackColor = row_obj.DefaultCellStyle.BackColor;
        }

        private void UpdateScriptControllerStatus(ScriptController controller)
        {
            foreach (DataGridViewRow row_obj in Rows) {
                if (row_obj.Tag.Equals(controller)) {
                    UpdateScriptControllerStatus(row_obj, controller);
                    return;
                }
            }
        }

        private void UpdateScriptControllerList()
        {
            if (update_forbid_)return;
            if (update_busy_)return;

            update_busy_ = true;

            Rows.Clear();

            foreach (var script_obj in ScriptManager.ScriptList) {
                var row_obj = Rows[Rows.Add()];

                row_obj.Tag = script_obj;

                UpdateScriptControllerStatus(row_obj, script_obj);
            }

            if (update_timer_.Enabled) {
                update_timer_.Stop();
            }

            update_busy_ = false;
        }

        private void UpdateScriptListRequest()
        {
            if (!update_timer_.Enabled) {
                update_timer_.Start();
            }
        }

        private void ContentAction(DataGridViewRow row_obj, ColumnId column_id, ScriptController controller)
        {
            var cell_obj = row_obj.Cells[(int)column_id];

            switch (column_id) {
                case ColumnId.Mode:
                {
                    controller.Mode = (ScriptRunMode)cell_obj.Value;
                }
                    break;

                case ColumnId.Status:
                {
                    controller.Status = (ScriptRunStatus)cell_obj.Value;
                }
                    break;
            }
        }

        protected override void OnCellContentClick(DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0))return;

            var row_obj = Rows[e.RowIndex];
            var controller = row_obj.Tag as ScriptController;

            if (controller == null)return;

            ContentAction(row_obj, (ColumnId)e.ColumnIndex, controller);
        }

        protected override void OnCellValueChanged(DataGridViewCellEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnCellValueChanged");

            OnCellContentClick(e);
        }

        protected override void OnCurrentCellDirtyStateChanged(EventArgs e)
        {
            CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        protected override void OnCellEnter(DataGridViewCellEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnCellEnter");
            if ((e.RowIndex < 0) || (e.ColumnIndex < 0))return;

            if (Columns[e.ColumnIndex] is DataGridViewComboBoxColumn) {
                SendKeys.Send(" ");
            }
        }

        protected override void OnCellLeave(DataGridViewCellEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnCellLeave");
        }

        protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnCellBeginEdit");
            update_forbid_ = true;
        }

        protected override void OnCellEndEdit(DataGridViewCellEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnCellEndEdit");
            update_forbid_ = false;

            UpdateScriptListRequest();
        }

        private void OnUpdateScriptList(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnUpdateScriptList");

            UpdateScriptControllerList();
        }

        private void OnScriptControllerListUpdated()
        {
            if (InvokeRequired) {
                Invoke((ScriptControllerListUpdatedHandler)OnScriptControllerListUpdated);
                return;
            }

            System.Diagnostics.Debug.WriteLine("OnScriptStatusUpdated");

            UpdateScriptControllerList();
        }

        private void OnScriptControllerStatusUpdated(ScriptController controller)
        {
            if (InvokeRequired) {
                Invoke((ScriptControllerStatusUpdatedHandler)OnScriptControllerStatusUpdated, controller);
                return;
            }

            UpdateScriptControllerStatus(controller);
        }
    }
}
