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
using Ratatoskr.Configs;
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.Generic;
using Ratatoskr.Resources;
using Ratatoskr.Utility;

namespace Ratatoskr.Forms.MainWindow
{
    internal partial class MainWindow_SendDataListPanel : UserControl
    {
        internal sealed class SendDataInfo
        {
            public string Target      { get; set; } = "";
            public string Command     { get; set; } = "";
            public uint   DelayFixed  { get; set; } = 0;
            public uint   DelayRandom { get; set; } = 0;

            public SendDataInfo(string target, string command, uint delay_fixed, uint delay_random)
            {
                Target = target;
                Command = command;
                DelayFixed = delay_fixed;
                DelayRandom = delay_random;
            }
        }

        private enum ColumnId
        {
            Enable,
            TargetType,
            CustomTarget,
            Command,
            DelayFixed,
            DelayRandom,
        }

        private enum PlayStatus
        {
            Reset,
            Pause,
            Busy,
        }

        private readonly Color COLOR_BUSY_COMMAND      = Color.LightSkyBlue;
        private readonly Color COLOR_COMMAND_FORMAT_OK = AppColors.PATTERN_OK;
        private readonly Color COLOR_COMMAND_FORMAT_NG = AppColors.PATTERN_NG;


        private PlayStatus     play_state_ = PlayStatus.Reset;
        private Timer          play_timer_ = new Timer();
        private uint           play_next_delay_ = 0;

        private int            play_data_index_busy_ = -1;
        private int            play_data_index_view_ = -1;

        private SendDataInfo   play_data_info_ = null;

        private uint  play_repeat_count_ = 0;


        public MainWindow_SendDataListPanel()
        {
            InitializeComponent();

            GView_CmdList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            play_timer_.Tick += OnPlayTimer;
        }

        public void LoadConfig()
        {
            /* ターゲット */
            TBox_CommonTarget.Text = ConfigManager.User.SendDataListTarget.Value;

            /* コマンドリスト */
            LoadCommandListHeaderConfig();
            LoadCommandListDataConfig();

            /* 繰り返し回数 */
            Num_RepeatCount.Value = ConfigManager.User.SendDataListRepeat.Value;

            UpdateOperationUI();
            UpdateStatusUI();
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
            /* ターゲット */
            ConfigManager.User.SendDataListTarget.Value = TBox_CommonTarget.Text;

            /* コマンドリスト */
            BackupConfig_SendDataList();

            /* 繰り返し回数 */
            ConfigManager.User.SendDataListRepeat.Value = Num_RepeatCount.Value;
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

        private void PlayReset()
        {
            PlayPause();

            play_state_ = PlayStatus.Reset;

            /* 非選択状態 */
            play_data_index_busy_ = -1;

            /* 繰り返し回数を初期化 */
            play_repeat_count_ = 0;

            UpdateOperationUI();
            UpdateStatusUI();
        }

        private void PlayStart()
        {
            PlayPause();

            play_state_ = PlayStatus.Busy;

            /* 途中で編集されたときを考慮してコマンド番号を補正 */
            if (play_data_index_busy_ >= GView_CmdList.RowCount) {
                play_data_index_busy_ = -1;
            }

            /* 初回タイマーイベント */
            OnPlayTimer(play_timer_, EventArgs.Empty);

            UpdateOperationUI();
            UpdateStatusUI();
        }

        private void PlayPause()
        {
            play_state_ = PlayStatus.Pause;

            /* 実行中タイマーを停止 */
            if (play_timer_.Enabled) {
                play_timer_.Stop();
            }

            UpdateOperationUI();
            UpdateStatusUI();
        }

        private void PlayTimerSetup(uint ival_ms)
        {
            play_timer_.Stop();
            play_timer_.Interval = (int)Math.Max(1, ival_ms);
            play_timer_.Start();
        }

        private void PlayTimerRecall()
        {
            PlayTimerSetup(1);
        }

        private void OnPlayTimer(object sender, EventArgs e)
        {
            var timer = sender as Timer;

            /* 初期化処理 */
            if (play_data_index_busy_ < 0) {
                play_data_index_busy_ = 0;
            } else {
                play_data_index_busy_++;
            }

            /* コマンド情報取得 */
            play_data_info_ = LoadNextDataInfo(ref play_data_index_busy_);

            if (play_data_info_ != null) {
                /* --- コマンド情報取得成功 --- */
                /* 次のコマンドまでの遅延時間 */
                play_next_delay_ = play_data_info_.DelayFixed + (uint)((new Random()).Next((int)play_data_info_.DelayRandom));

                /* タイマー再起動 */
                PlayTimerSetup(play_next_delay_);

                /* コマンド実行 */
                SendExec(play_data_info_);

            } else {
                /* --- コマンド終端に到着 --- */
                /* 実行中コマンド番号を初期化 */
                play_data_index_busy_ = -1;

                if (play_repeat_count_ < uint.MaxValue) {
                    play_repeat_count_++;
                }

                if (   (Num_RepeatCount.Value > 0)
                    && (play_repeat_count_ >= Num_RepeatCount.Value)
                ) {
                    /* --- 繰り返し回数上限に達した --- */
                    PlayReset();

                } else {
                    /* --- 繰り返し実行 --- */
                    /* 再チェック */
                    PlayTimerRecall();
                }
            }

            UpdateStatusUI();
        }

        private void AddSendDataConfig(SendDataConfig config)
        {
            /* 新規行を追加 */
            var row_index = GView_CmdList.Rows.Add();

            if (row_index < 0)return;

            var row_obj = GView_CmdList.Rows[row_index];

            /* 値を書き換え */
            row_obj.Cells[(int)ColumnId.Enable].Value = config.Enable;
            row_obj.Cells[(int)ColumnId.TargetType].Value = config.TargetType;
            row_obj.Cells[(int)ColumnId.CustomTarget].Value = config.CustomTarget;
            row_obj.Cells[(int)ColumnId.Command].Value = config.Command;
            row_obj.Cells[(int)ColumnId.DelayFixed].Value = config.DelayFixed.ToString();
            row_obj.Cells[(int)ColumnId.DelayRandom].Value = config.DelayRandom.ToString();

            /* エラー表示を更新 */
            UpdateEditStatus(row_obj);
        }

        private SendDataConfig GetSendDataConfig(int index)
        {
            try {
                /* 範囲外 */
                if ((index < 0) || (index >= GView_CmdList.RowCount))return (null);

                var row_obj = GView_CmdList.Rows[(int)index];

                /* New行 */
                if (row_obj.IsNewRow)return (null);

                return (new SendDataConfig(
                                    (bool)row_obj.Cells[(int)ColumnId.Enable].Value,
                                    (SendDataTargetType)row_obj.Cells[(int)ColumnId.TargetType].Value,
                                    row_obj.Cells[(int)ColumnId.CustomTarget].Value as string,
                                    row_obj.Cells[(int)ColumnId.Command].Value as string,
                                    uint.Parse(row_obj.Cells[(int)ColumnId.DelayFixed].Value as string),
                                    uint.Parse(row_obj.Cells[(int)ColumnId.DelayRandom].Value as string)));
            } catch {
                return (null);
            }
        }

        private SendDataInfo GetSendDataInfo(int index)
        {
            var config = GetSendDataConfig(index);

            if (   (config == null)
                || (!config.Enable)
            ) {
                return (null);
            }

            return (new SendDataInfo(
                            (config.TargetType == SendDataTargetType.Common) ? (TBox_CommonTarget.Text) : (config.CustomTarget),
                            config.Command,
                            config.DelayFixed,
                            config.DelayRandom));
        }

        private SendDataInfo LoadNextDataInfo(ref int index)
        {
            for (; index < GView_CmdList.RowCount; index++) {
                var info = GetSendDataInfo(index);

                if (info == null)continue;

                return (info);
            }

            return (null);
        }

        private void SendExec(SendDataInfo info)
        {
            /* 送信実行 */
            Program.API.API_SendData(info.Target, info.Command);
        }

        private void UpdateOperationUI()
        {
            /* Play/Pause/Stopボタン */
            Btn_Play.Image = (play_state_ == PlayStatus.Busy)
                           ? (global::Ratatoskr.Properties.Resources.pause_32x32)
                           : (global::Ratatoskr.Properties.Resources.play_32x32);
            Btn_Play.Text = (play_state_ == PlayStatus.Busy)
                          ? ("Pause")
                          : ("Play");

            Btn_Stop.Enabled = (play_state_ != PlayStatus.Reset);

            /* コントロールの操作可能状態を更新 */
            TBox_CommonTarget.Enabled = (play_state_ != PlayStatus.Busy);
            Num_RepeatCount.Enabled = (play_state_ != PlayStatus.Busy);
            GView_CmdList.Enabled = (play_state_ != PlayStatus.Busy);
        }

        private void UpdateStatusUI()
        {
            /* GridViewを更新 */
            if (play_data_index_view_ != play_data_index_busy_) {
                /*  */

                /* 変更前に選択状態のコマンドリストを非選択状態にする */
                if (play_data_index_view_ >= 0) {
                    GView_CmdList.Rows[play_data_index_view_].DefaultCellStyle.BackColor = Color.White;
                }

                play_data_index_view_ = play_data_index_busy_;

                /* 変更後に選択状態のコマンドリストを選択状態にする */
                if (play_data_index_view_ >= 0) {
                    GView_CmdList.Rows[play_data_index_view_].DefaultCellStyle.BackColor = COLOR_BUSY_COMMAND;
                }
            }

            /* 繰り返し回数 */
            if (play_state_ == PlayStatus.Reset) {
                Label_RepeatCount.Text = "";
            } else {
                Label_RepeatCount.Text = (play_repeat_count_ + 1).ToString();
            }

            UpdateStatusText();
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
            var cmd_target = TBox_CommonTarget.Text;

            /* エラー判定 */
            switch ((ColumnId)cell.OwningColumn.Index) {
                case ColumnId.Command:
                {
                    var value_str = cell.Value as string;

                    if (   (value_str != null)
                        && (value_str.Length > 0)
                    ) {
                        if (HexTextEncoder.ToByteArray(value_str) != null) {
                            cell.Style.BackColor = COLOR_COMMAND_FORMAT_OK;
                        } else {
                            cell.Style.BackColor = COLOR_COMMAND_FORMAT_NG;
                            error_text = "Command incorrect";
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

        private delegate void UpdateStatusTextDelegate();
        private void UpdateStatusText()
        {
            if (InvokeRequired) {
                Invoke(new UpdateStatusTextDelegate(UpdateStatusText));
                return;
            }

            var str = new StringBuilder();

            /* 再生中 */
            if (play_state_ == PlayStatus.Busy) {
                str.AppendFormat(
                    "{0} {1,3}/{2,-3} | {3} {4,6}[ms] | {5}",
                    "Command list running",
                    play_data_index_busy_ + 1,
                    GView_CmdList.RowCount,
                    "Next command",
                    play_next_delay_,
                    "Delay waiting");

            /* 一時停止中 */
            } else if (play_state_ == PlayStatus.Pause) {
                str.AppendFormat(
                    "{0} {1,3}/{2,-3}",
                    "Command list pause",
                    play_data_index_busy_ + 1,
                    GView_CmdList.RowCount);
            }

            FormUiManager.SetStatusText(StatusTextID.SequentialCommandStatus, str.ToString());
        }

        private void ImportCsv(string path)
        {
            try {
                using (var file = new FileStream(path, FileMode.Open)) {
                    using (var reader = new StreamReader(file)) {
                        /* ヘッダー情報を読み込み */
                        var columns = ImportCsvHeader(reader);

                        /* データ情報を読み込み */
                        var configs = ImportCsvData(reader, columns);

                        /* 適用 */
                        GView_CmdList.Rows.Clear();
                        foreach (var config in configs) {
                            AddSendDataConfig(config);
                        }
                    }
                }
            } catch {
            }
        }

        private ColumnId[] ImportCsvHeader(StreamReader reader)
        {
            return ((from block in TextUtil.ReadCsvLine(reader)
                     select (ColumnId)Enum.Parse(typeof(ColumnId), block))
                     .ToArray());
        }

        private SendDataConfig[] ImportCsvData(StreamReader reader, ColumnId[] columns)
        {
            var configs = new Queue<SendDataConfig>();

            while (!reader.EndOfStream) {
                var config = new SendDataConfig();
                var items = TextUtil.ReadCsvLine(reader);

                foreach (var item in items.Select((v, i) => new {v, i})) {
                    switch (columns[item.i]) {
                        case ColumnId.Enable:
                            config.Enable = bool.Parse(item.v);
                            break;
                        case ColumnId.TargetType:
                            config.TargetType = (SendDataTargetType)Enum.Parse(typeof(SendDataTargetType), item.v);
                            break;
                        case ColumnId.CustomTarget:
                            config.CustomTarget = item.v;
                            break;
                        case ColumnId.Command:
                            config.Command = item.v;
                            break;
                        case ColumnId.DelayFixed:
                            config.DelayFixed = uint.Parse(item.v);
                            break;
                        case ColumnId.DelayRandom:
                            config.DelayRandom = uint.Parse(item.v);
                            break;
                    }
                }

                configs.Enqueue(config);
            }

            return (configs.ToArray());
        }

        private void ExportCsv(string path)
        {
            BackupConfig();

            try {
                using (var file = new FileStream(path, FileMode.Create)) {
                    using (var writer = new StreamWriter(file)) {
                        /* ヘッダー情報を書き込み */
                        ExportCsvHeader(writer);

                        /* データ情報を書き込み */
                        ExportCsvData(writer, ConfigManager.User.SendDataList.Value);
                    }
                }
            } catch {
            }
        }

        private void ExportCsvHeader(StreamWriter writer)
        {
            writer.WriteLine(TextUtil.WriteCsvLine(Enum.GetNames(typeof(ColumnId))));
        }

        private void ExportCsvData(StreamWriter writer, IEnumerable<SendDataConfig> configs)
        {
            foreach (var config in configs) {
                var items = new Queue<string>();

                foreach (ColumnId column in Enum.GetValues(typeof(ColumnId))) {
                    switch (column) {
                        case ColumnId.Enable:       items.Enqueue(config.Enable.ToString());        break;
                        case ColumnId.TargetType:   items.Enqueue(config.TargetType.ToString());    break;
                        case ColumnId.CustomTarget: items.Enqueue(config.CustomTarget);             break;
                        case ColumnId.Command:      items.Enqueue(config.Command);                  break;
                        case ColumnId.DelayFixed:   items.Enqueue(config.DelayFixed.ToString());    break;
                        case ColumnId.DelayRandom:  items.Enqueue(config.DelayRandom.ToString());   break;
                    }
                }

                writer.WriteLine(TextUtil.WriteCsvLine(items));
            }
        }

        private void Btn_Play_Click(object sender, EventArgs e)
        {
            /* 再生中 ⇒ 一時停止 */
            if (play_state_ == PlayStatus.Busy) {
                PlayPause();

            /* 停止 or 一時停止 ⇒ 再生 */
            } else {
                PlayStart();
            }
        }

        private void Btn_Stop_Click(object sender, EventArgs e)
        {
            PlayReset();
        }

        private void GView_CmdList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var gview = sender as DataGridView;

            if (gview == null)return;

            var row_obj = gview.Rows[gview.CurrentCell.RowIndex];
            var cell_obj = row_obj.Cells[gview.CurrentCell.ColumnIndex];

            switch ((ColumnId)gview.CurrentCell.ColumnIndex) {
                case ColumnId.Command:
                    GView_CmdList_EditCell_Command(gview.EditingControl as DataGridViewTextBoxEditingControl, row_obj, cell_obj);
                    break;
            }
        }

        private void GView_CmdList_EditCell_Command(DataGridViewTextBoxEditingControl control, DataGridViewRow row_obj, DataGridViewCell cell_obj)
        {
            if (control == null)return;

            /* 初回イベント */
            GView_CmdList_EditCell_Command_TextChanged(control, new EventArgs());

            /* イベントを登録 */
            control.TextChanged -= GView_CmdList_EditCell_Command_TextChanged;
            control.TextChanged += GView_CmdList_EditCell_Command_TextChanged;
        }

        private void GView_CmdList_EditCell_Command_TextChanged(object sender, EventArgs e)
        {
            var control = sender as DataGridViewTextBoxEditingControl;

            if (control == null)return;

            var value = control.Text;

            /* 表示更新 */
            if ((value != null) && (value.Length > 0)) {
                control.BackColor = (HexTextEncoder.ToByteArray(value) != null) ? (COLOR_COMMAND_FORMAT_OK) : (COLOR_COMMAND_FORMAT_NG);
            } else {
                control.BackColor = Color.White;
            }
        }

        private void GView_CmdList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var gview = sender as DataGridView;

            if (gview == null)return;

            var row_obj = gview.Rows[e.RowIndex];
            var cell_obj = row_obj.Cells[e.ColumnIndex];

            switch ((ColumnId)e.ColumnIndex) {
                case ColumnId.Command:
                {
                    /* 編集開始時に背景色をクリア */
                    cell_obj.Style.BackColor = Color.White;
                }
                    break;
            }
        }

        private void GView_CmdList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var gview = sender as DataGridView;

            if (gview == null)return;

            var row_obj = gview.Rows[e.RowIndex];

            /* エラー表示を更新 */
            UpdateEditStatus(row_obj);
        }

        private void GView_CmdList_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            var row_obj = e.Row;
            var config = new SendDataConfig();

            row_obj.Cells[(int)ColumnId.Enable].Value = config.Enable;
            row_obj.Cells[(int)ColumnId.TargetType].Value = config.TargetType;
            row_obj.Cells[(int)ColumnId.CustomTarget].Value = config.CustomTarget;
            row_obj.Cells[(int)ColumnId.Command].Value = config.Command;
            row_obj.Cells[(int)ColumnId.DelayFixed].Value = config.DelayFixed.ToString();
            row_obj.Cells[(int)ColumnId.DelayRandom].Value = config.DelayRandom.ToString();
        }

        private void GView_CmdList_RowContextMenuStripNeeded(object sender, DataGridViewRowContextMenuStripNeededEventArgs e)
        {
        }

        private void Btn_Import_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.Filter = "CSV file(*.csv)|*.csv";

            if (dialog.ShowDialog() != DialogResult.OK)return;

            ImportCsv(dialog.FileName);
        }

        private void Btn_Export_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();

            dialog.Filter = "CSV file(*.csv)|*.csv";

            if (dialog.ShowDialog() != DialogResult.OK)return;

            ExportCsv(dialog.FileName);
        }

        private void GView_CmdList_KeyDown(object sender, KeyEventArgs e)
        {
            GView_CmdList.BeginEdit(true);
        }
    }
}
