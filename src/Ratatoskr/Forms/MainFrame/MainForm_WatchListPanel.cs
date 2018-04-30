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
using Ratatoskr.Forms;
using Ratatoskr.Gate;
using Ratatoskr.Generic;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Scripts.PacketFilterExp;
using Ratatoskr.Scripts.PacketFilterExp.Parser;

namespace Ratatoskr.Forms.MainFrame
{
    public partial class MainForm_WatchListPanel : UserControl
    {
        private enum ColumnId
        {
            Enable,
            Target,
            Expression,
            NtfEvent,
            NtfDialog,
            NtfMail,
        }

        private class NtfMailItem
        {
            public Guid ConfigID { get; }

            public NtfMailItem(Guid id)
            {
                ConfigID = id;
            }

            public override string ToString()
            {
                if (ConfigID == Guid.Empty) {
                    return ("None");
                }

                var config = ConfigManager.System.MailList.Value.Find(item => item.ID == ConfigID);

                if (config == null)return ("Unknown");

                return (string.Format("{0} - {1}", config.SmtpHost, config.SmtpUsername));
            }
        }

        private class WatchObject
        {
            private ExpressionFilter filter_;
            private bool ntf_event_;
            private bool ntf_dialog_;
            private bool ntf_mail_;
            
            public WatchObject(WatchTargetType target, ExpressionFilter filter, bool ntf_event, bool ntf_dialog, bool ntf_mail)
            {
                Target = target;
                filter_ = filter;
                ntf_event_ = ntf_event;
                ntf_dialog_ = ntf_dialog;
                ntf_mail_ = ntf_mail;
            }

            public WatchTargetType Target { get; }

            public void Input(IEnumerable<PacketObject> packets)
            {
                foreach (var packet in packets) {
                    Input(packet);
                }
            }

            public void Input(PacketObject packet)
            {
                if (!filter_.Input(packet))return;

                var event_dt = DateTime.UtcNow;
                var event_text = string.Format("Event catch. [{0}]", filter_.ExpressionText);

                if (ntf_event_) {
                    NotifyEvent(event_dt, event_text);
                }

                if (ntf_dialog_) {
                    NotifyDialog(event_dt, event_text);
                }

                if (ntf_mail_) {
                    NotifyMail(event_dt, event_text);
                }
            }

            private void NotifyEvent(DateTime dt, string text)
            {
                GatePacketManager.SetWatchEvent(dt, text);
            }

            private void NotifyDialog(DateTime dt, string text)
            {
                FormUiManager.SetWatchEventText(string.Format("{0} - {1}", dt.ToLocalTime().ToString(), text));
            }

            private void NotifyMail(DateTime dt, string text)
            {
            }
        }


        private readonly Color COLOR_COMMAND_FORMAT_OK = Color.LightSkyBlue;
        private readonly Color COLOR_COMMAND_FORMAT_NG = Color.LightPink;

        
        private WatchObject[] watch_objs_raw_ = null;
        private WatchObject[] watch_objs_view_ = null;


        public MainForm_WatchListPanel()
        {
            InitializeComponent();

            GatePacketManager.RawPacketEntried += OnRawPacketEntried;
            FormTaskManager.DrawPacketEntried += OnDrawPacketEntried;

            DGView_WatchList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public void LoadConfig()
        {
            /* 監視式リスト */
            LoadWatchListHeaderConfig();
            LoadWatchListDataConfig();
        }

        private void LoadWatchListHeaderConfig()
        {
            DGView_WatchList.Columns.Clear();

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
                        DGView_WatchList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.Target:
                    {
                        var column = new DataGridViewComboBoxColumn();

                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderText = ConfigManager.Language.MainUI.WLPanel_Column_Target.Value;
                        column.Width = 90;
                        column.Resizable = DataGridViewTriState.False;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        column.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                        column.ValueType = typeof(WatchTargetType);
                        foreach (var value in Enum.GetValues(typeof(WatchTargetType))) {
                            column.Items.Add(value);
                        }
                        DGView_WatchList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.Expression:
                    {
                        var column = new DataGridViewTextBoxColumn();

                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderText = ConfigManager.Language.MainUI.WLPanel_Column_Expression.Value;
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        DGView_WatchList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.NtfEvent:
                    {
                        var column = new DataGridViewCheckBoxColumn();

                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderText = ConfigManager.Language.MainUI.WLPanel_Column_NtfPlan_Event.Value;
                        column.Width = 60;
                        column.Resizable = DataGridViewTriState.False;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        DGView_WatchList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.NtfDialog:
                    {
                        var column = new DataGridViewCheckBoxColumn();

                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderText = ConfigManager.Language.MainUI.WLPanel_Column_NtfPlan_Dialog.Value;
                        column.Width = 60;
                        column.Resizable = DataGridViewTriState.False;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        DGView_WatchList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.NtfMail:
                    {
                        var column = new DataGridViewCheckBoxColumn();

                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderText = ConfigManager.Language.MainUI.WLPanel_Column_NtfPlan_Mail.Value;
                        column.Visible = false;
                        column.Width = 60;
                        column.Resizable = DataGridViewTriState.False;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        DGView_WatchList.Columns.Add(column);
                    }
                        break;

                    default:
                    {
                        var column = new DataGridViewTextBoxColumn();

                        DGView_WatchList.Columns.Add(column);
                    }
                        break;
                }
            }
        }

        private void LoadWatchListDataConfig()
        {
            DGView_WatchList.Rows.Clear();
            foreach (var config in ConfigManager.User.WatchDataList.Value) {
                AddWatchDataConfig(config);
            }
        }

        public void BackupConfig()
        {
            /* コマンドリスト */
            BackupConfig_WatchList();
        }

        private void BackupConfig_WatchList()
        {
            ConfigManager.User.WatchDataList.Value.Clear();

            foreach (DataGridViewRow row in DGView_WatchList.Rows) {
                if (row.IsNewRow)break;

                try {
                    ConfigManager.User.WatchDataList.Value.Add(
                        new WatchDataConfig(
                            (bool)row.Cells[(int)ColumnId.Enable].Value,
                            (WatchTargetType)row.Cells[(int)ColumnId.Target].Value,
                            row.Cells[(int)ColumnId.Expression].Value as string,
                            (bool)row.Cells[(int)ColumnId.NtfEvent].Value,
                            (bool)row.Cells[(int)ColumnId.NtfDialog].Value,
                            (bool)row.Cells[(int)ColumnId.NtfMail].Value));
                } catch {
                    ConfigManager.User.SendDataList.Value.Add(
                        new SendDataConfig(false, SendDataTargetType.Common, "*", "", 0, 0));
                }
            }
        }

        private void UpdateEditStatus(DataGridViewRow row_obj)
        {
            var error = false;

            /* === セル毎のエラーを設定 === */
            foreach (DataGridViewCell cell in row_obj.Cells) {
                UpdateEditStatus(cell);

                /* セルのエラー状態を収集 */
                if (cell.ErrorText.Length > 0) {
                    error = true;
                }
            }

            /* === 行エラーを設定 === */
            if (error) {
                row_obj.ErrorText = "There is an item of input value error";
            } else {
                row_obj.ErrorText = "";
            }
        }

        private void UpdateEditStatus(DataGridViewCell cell)
        {
            var error_text = "";

            /* エラー判定 */
            switch ((ColumnId)cell.OwningColumn.Index) {
                case ColumnId.Expression:
                {
                    var value_str = cell.Value as string;

                    if (   (value_str != null)
                        && (value_str.Length > 0)
                    ) {
                        if (ExpressionFilter.Build(value_str) != null) {
                            cell.Style.BackColor = COLOR_COMMAND_FORMAT_OK;
                        } else {
                            cell.Style.BackColor = COLOR_COMMAND_FORMAT_NG;
                        }
                    }
                }
                    break;

                default:
                    break;
            }

            /* エラーテキストを設定 */
            cell.ErrorText = error_text;
        }

        private void SetWatchDataConfig(DataGridViewRow row_obj, WatchDataConfig config)
        {
            if (row_obj.Cells.Count > (int)ColumnId.Enable) {
                row_obj.Cells[(int)ColumnId.Enable].Value = config.Enable;
            }

            if (row_obj.Cells.Count > (int)ColumnId.Target) {
                row_obj.Cells[(int)ColumnId.Target].Value = config.WatchTarget;
            }

            if (row_obj.Cells.Count > (int)ColumnId.Expression) {
                row_obj.Cells[(int)ColumnId.Expression].Value = config.Expression;
            }

            if (row_obj.Cells.Count > (int)ColumnId.NtfEvent) {
                row_obj.Cells[(int)ColumnId.NtfEvent].Value = config.NtfEvent;
            }

            if (row_obj.Cells.Count > (int)ColumnId.NtfDialog) {
                row_obj.Cells[(int)ColumnId.NtfDialog].Value = config.NtfDialog;
            }

            if (row_obj.Cells.Count > (int)ColumnId.NtfMail) {
                row_obj.Cells[(int)ColumnId.NtfMail].Value = config.NtfMail;
            }
        }

        private void AddWatchDataConfig(WatchDataConfig config)
        {
            var row_index = DGView_WatchList.Rows.Add();

            var row_obj = DGView_WatchList.Rows[row_index];

            SetWatchDataConfig(row_obj, config);

            /* エラー表示を更新 */
            UpdateEditStatus(row_obj);
        }

        private WatchDataConfig LoadWatchDataConfig(DataGridViewRow row_obj)
        {
            /* 全てのパラメータが設定されていない場合は無視 */
            foreach (DataGridViewCell cell_obj in row_obj.Cells) {
                if (cell_obj.Value == null)return (null);
            }

            return (
                new WatchDataConfig(
                    (bool)row_obj.Cells[(int)ColumnId.Enable].Value,
                    (WatchTargetType)row_obj.Cells[(int)ColumnId.Target].Value,
                    row_obj.Cells[(int)ColumnId.Expression].Value as string,
                    (bool)row_obj.Cells[(int)ColumnId.NtfEvent].Value,
                    (bool)row_obj.Cells[(int)ColumnId.NtfDialog].Value,
                    (bool)row_obj.Cells[(int)ColumnId.NtfMail].Value
                ));
        }

        private WatchObject LoadWatchObject(DataGridViewRow row_obj)
        {
            if (   (row_obj == null)
                || (row_obj.IsNewRow)
            ) {
                return (null);
            }

            var config = LoadWatchDataConfig(row_obj);

            if (   (config == null)
                || (!config.Enable)
            ) {
                return (null);
            }

            /* 通知先がない場合は無視 */
            if (   (!config.NtfEvent)
                && (!config.NtfDialog)
                && (!config.NtfMail)
            ) {
                return (null);
            }

            var filter = ExpressionFilter.Build(config.Expression);

            if (filter == null)return (null);

            return (new WatchObject(config.WatchTarget, filter, config.NtfEvent, config.NtfDialog, config.NtfMail));
        }

        private void UpdateWatchRule(DataGridViewRow row_obj)
        {
            if (row_obj.IsNewRow)return;

            /* 生成した監視ルールをタグにバックアップ */
            row_obj.Tag = LoadWatchObject(row_obj);
        }

        private void UpdateWatchRule(int row_index)
        {
            UpdateWatchRule(DGView_WatchList.Rows[row_index]);
        }

        private void UpdateWatchRule()
        {
            /* 全監視ルールを更新 */
            foreach (DataGridViewRow row_obj in DGView_WatchList.Rows) {
                UpdateWatchRule(row_obj);
            }
        }

        private void BuildWatchRule()
        {
            var objs_raw = new List<WatchObject>();
            var objs_view = new List<WatchObject>();
            var obj = (WatchObject)null;

            foreach (DataGridViewRow row_obj in DGView_WatchList.Rows) {
                obj = row_obj.Tag as WatchObject;
                
                if (obj == null)continue;

                switch (obj.Target) {
                    case WatchTargetType.RawPacket:  objs_raw.Add(obj);  break;
                    case WatchTargetType.ViewPacket: objs_raw.Add(obj);  break;
                }
            }

            watch_objs_raw_ = objs_raw.ToArray();
            watch_objs_view_ = objs_view.ToArray();
        }

        private void OnRawPacketEntried(IEnumerable<Generic.Packet.PacketObject> packets)
        {
            if (watch_objs_raw_ != null) {
                foreach (var obj in watch_objs_raw_) {
                    obj.Input(packets);
                }
            }
        }

        private void OnDrawPacketEntried(IEnumerable<Generic.Packet.PacketObject> packets)
        {
            if (watch_objs_view_ != null) {
                foreach (var obj in watch_objs_view_) {
                    obj.Input(packets);
                }
            }
        }

        private void DGView_WatchList_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            SetWatchDataConfig(e.Row, new WatchDataConfig());
        }

        private void DGView_WatchList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdateWatchRule(e.RowIndex);
            BuildWatchRule();
        }

        private void DGView_WatchList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var gview = sender as DataGridView;

            if (gview == null)return;

            var row_obj = gview.Rows[gview.CurrentCell.RowIndex];
            var cell_obj = row_obj.Cells[gview.CurrentCell.ColumnIndex];

            switch ((ColumnId)gview.CurrentCell.ColumnIndex) {
                case ColumnId.Expression:
                    DGView_CmdList_EditCell_Command(gview.EditingControl as DataGridViewTextBoxEditingControl, row_obj, cell_obj);
                    break;
            }

        }

        private void DGView_CmdList_EditCell_Command(DataGridViewTextBoxEditingControl control, DataGridViewRow row_obj, DataGridViewCell cell_obj)
        {
            if (control == null)return;

            /* 初回イベント */
            DGView_WatchList_EditCell_Command_TextChanged(control, new EventArgs());

            /* イベントを登録 */
            control.TextChanged -= DGView_WatchList_EditCell_Command_TextChanged;
            control.TextChanged += DGView_WatchList_EditCell_Command_TextChanged;
        }

        private void DGView_WatchList_EditCell_Command_TextChanged(object sender, EventArgs e)
        {
            var control = sender as DataGridViewTextBoxEditingControl;

            if (control == null)return;

            var value = control.Text;

            /* 表示更新 */
            if ((value != null) && (value.Length > 0)) {
                control.BackColor = (ExpressionFilter.Build(value) != null) ? (COLOR_COMMAND_FORMAT_OK) : (COLOR_COMMAND_FORMAT_NG);
            } else {
                control.BackColor = Color.White;
            }
        }

        private void DGView_WatchList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var gview = sender as DataGridView;

            if (gview == null)return;

            var row_obj = gview.Rows[e.RowIndex];
            var cell_obj = row_obj.Cells[e.ColumnIndex];

            switch ((ColumnId)e.ColumnIndex) {
                case ColumnId.Expression:
                {
                    /* 編集開始時に背景色をクリア */
                    cell_obj.Style.BackColor = Color.White;
                }
                    break;
            }
        }

        private void DGView_WatchList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var gview = sender as DataGridView;

            if (gview == null)return;

            var row_obj = gview.Rows[e.RowIndex];

            /* エラー表示を更新 */
            UpdateEditStatus(row_obj);
        }

        private void DGView_WatchList_KeyDown(object sender, KeyEventArgs e)
        {
            (sender as DataGridView).BeginEdit(true);
        }

        private void DGView_WatchList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            var gview = sender as DataGridView;

            if (   (gview.IsCurrentCellDirty)
                && (gview.CurrentCell is DataGridViewCheckBoxCell)
            ) {
                gview.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
