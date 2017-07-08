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
using Ratatoskr.Actions;
using Ratatoskr.Configs;
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.Scripts;
using Ratatoskr.Scripts.Expression.Parser;
using Ratatoskr.Scripts.Expression;
using Ratatoskr.Generic;

namespace Ratatoskr.Forms.MainFrame
{
    internal partial class MainFrameSequentialCommandPanel : UserControl
    {
        private sealed class CommandInfo
        {
            public CommandInfo(bool enable, string command, uint delay_fixed, uint delay_random)
            {
                Enable = enable;
                Command = command;
                DelayFixed = delay_fixed;
                DelayRandom = delay_random;
            }

            public bool   Enable      { get; set; }
            public string Command     { get; set; }
            public uint   DelayFixed  { get; set; }
            public uint   DelayRandom { get; set; }
        }


        private enum ColumnId
        {
            Enable,
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

        private readonly Color COLOR_BUSY_COMMAND      = Color.LightGreen;
        private readonly Color COLOR_COMMAND_FORMAT_OK = Color.LightSkyBlue;
        private readonly Color COLOR_COMMAND_FORMAT_NG = Color.LightPink;


        private CommandRunner runner_ = new CommandRunner();

        private PlayStatus  play_state_ = PlayStatus.Reset;
        private Timer       play_timer_ = new Timer();
        private CommandInfo play_cmd_info_ = null;
        private uint        play_next_delay_ = 0;

        private int play_cmd_index_busy_ = -1;
        private int play_cmd_index_view_ = -1;

        private uint  play_repeat_count_ = 0;


        public MainFrameSequentialCommandPanel()
        {
            InitializeComponent();
            InitializeCommandList();

            play_timer_.Tick += OnPlayTimer;

            LoadConfig();

            UpdateOperationUI();
            UpdateStatusUI();
        }

        private void InitializeCommandList()
        {
            GView_CmdList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

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

                    case ColumnId.Command:
                    {
                        var column = new DataGridViewTextBoxColumn();

                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderText = "コマンド";
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        GView_CmdList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.DelayFixed:
                    {
                        var column = new DataGridViewTextBoxColumn();

                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderText = "基本遅延[ms]";
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.Width = 150;
                        column.Resizable = DataGridViewTriState.False;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        GView_CmdList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.DelayRandom:
                    {
                        var column = new DataGridViewTextBoxColumn();

                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderText = "乱数遅延(0～XXX)[ms]";
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.Width = 150;
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

        public void LoadConfig()
        {
            /* ターゲット */
            TBox_Target.Text = ConfigManager.User.SequentialCommandTarget.Value;

            /* コマンドリスト */
            GView_CmdList.Rows.Clear();
            foreach (var cmd in ConfigManager.User.SequentialCommandList.Value) {
                AddCommand(cmd.Enable, cmd.Command, cmd.DelayFixed, cmd.DelayRandom);
            }

            /* 繰り返し回数 */
            Num_RepeatCount.Value = ConfigManager.User.SequentialCommandRepeat.Value;
        }

        public void BackupConfig()
        {
            /* ターゲット */
            ConfigManager.User.SequentialCommandTarget.Value = TBox_Target.Text;

            /* コマンドリスト */
            ConfigManager.User.SequentialCommandList.Value.Clear();
            foreach (DataGridViewRow row in GView_CmdList.Rows) {
                if (row.IsNewRow)break;

                try {
                    ConfigManager.User.SequentialCommandList.Value.Add(
                        new SequentialCommandConfig(
                            (bool)row.Cells[(int)ColumnId.Enable].Value,
                            row.Cells[(int)ColumnId.Command].Value as string,
                            uint.Parse(row.Cells[(int)ColumnId.DelayFixed].Value as string),
                            uint.Parse(row.Cells[(int)ColumnId.DelayRandom].Value as string)));
                } catch {
                    ConfigManager.User.SequentialCommandList.Value.Add(
                        new SequentialCommandConfig(false, "", 0, 0));
                }
            }

            /* 繰り返し回数 */
            ConfigManager.User.SequentialCommandRepeat.Value = Num_RepeatCount.Value;
        }

        private void PlayReset()
        {
            PlayPause();

            play_state_ = PlayStatus.Reset;

            /* 非選択状態 */
            play_cmd_index_busy_ = -1;

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
            if (play_cmd_index_busy_ >= GView_CmdList.RowCount) {
                play_cmd_index_busy_ = -1;
            }

            /* 初回タイマーイベント */
            OnPlayTimer(play_timer_, EventArgs.Empty);

            UpdateOperationUI();
            UpdateStatusUI();
        }

        private void PlayPause()
        {
            play_state_ = PlayStatus.Pause;

            /* 実行中コマンドを停止 */
            runner_.Cancel();

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

            /* コマンド実行中は再チェック */
            if (runner_.IsRunning) {
                PlayTimerRecall();
                return;
            }

            /* 初期化処理 */
            if (play_cmd_index_busy_ < 0) {
                play_cmd_index_busy_ = 0;
                runner_.Reset();
            } else {
                play_cmd_index_busy_++;
            }

            /* コマンド実行 */
            play_cmd_info_ = SetupNextCommand(ref play_cmd_index_busy_);

            if (play_cmd_info_ != null) {
                /* --- コマンド実行成功 --- */
                /* 次のコマンドまでの遅延時間 */
                play_next_delay_ = play_cmd_info_.DelayFixed + (uint)((new Random()).Next((int)play_cmd_info_.DelayRandom));

                /* タイマー再起動 */
                PlayTimerSetup(play_next_delay_);

            } else {
                /* --- コマンド終端に到着 --- */
                /* 実行中コマンド番号を初期化 */
                play_cmd_index_busy_ = -1;

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

        private bool CommandCheck(string command)
        {
            return (CommandRunner.FormatCheck(TBox_Target.Text, command));
        }

        private void AddCommand(bool enable, string command, uint delay_fixed, uint delay_random)
        {
            /* 新規行を追加 */
            var row_index = GView_CmdList.Rows.Add();

            if (row_index < 0)return;

            var row_obj = GView_CmdList.Rows[row_index];

            /* 値を書き換え */
            row_obj.Cells[(int)ColumnId.Enable].Value = enable;
            row_obj.Cells[(int)ColumnId.Command].Value = command;
            row_obj.Cells[(int)ColumnId.DelayFixed].Value = delay_fixed.ToString();
            row_obj.Cells[(int)ColumnId.DelayRandom].Value = delay_random.ToString();

            /* エラー表示を更新 */
            UpdateEditStatus(row_obj);
        }

        private CommandInfo GetCommand(int index)
        {
            try {
                if ((index < 0) || (index >= GView_CmdList.RowCount))return (null);

                var row_obj = GView_CmdList.Rows[(int)index];

                if (row_obj.IsNewRow)return (null);

                return (new CommandInfo(
                                (bool)row_obj.Cells[(int)ColumnId.Enable].Value,
                                row_obj.Cells[(int)ColumnId.Command].Value as string,
                                uint.Parse(row_obj.Cells[(int)ColumnId.DelayFixed].Value as string),
                                uint.Parse(row_obj.Cells[(int)ColumnId.DelayRandom].Value as string)));
            } catch {
                return (null);
            }
        }

        private CommandInfo SetupNextCommand(ref int index)
        {
            for (; index < GView_CmdList.RowCount; index++) {
                var cmd_info = GetCommand(index);

                /* コマンド内容が不正 */
                if (cmd_info == null)continue;

                /* コマンドが無効 */
                if (!cmd_info.Enable)continue;

                /* コマンド構文が無効 */
                if (!runner_.Exec(TBox_Target.Text, cmd_info.Command, PlayCommandComplete))continue;

                return (cmd_info);
            }

            return (null);
        }

        private delegate void PlayCommandCompleteDelegate(object sender, EventArgs e);
        private void PlayCommandComplete(object sender, EventArgs e)
        {
            if (InvokeRequired) {
                Invoke(new PlayCommandCompleteDelegate(PlayCommandComplete), sender, e);
                return;
            }

            UpdateStatusUI();
        }

        private void UpdateOperationUI()
        {
            /* Play/Pause/Stopボタン */
            Btn_Play.Image = (play_state_ == PlayStatus.Busy)
                           ? (global::Ratatoskr.Properties.Resources.pause_32x32)
                           : (global::Ratatoskr.Properties.Resources.play_32x32);
            Btn_Play.Text = (play_state_ == PlayStatus.Busy)
                          ? ("一時停止")
                          : ("再生");

            Btn_Stop.Enabled = (play_state_ != PlayStatus.Reset);

            /* コントロールの操作可能状態を更新 */
            TBox_Target.Enabled = (play_state_ != PlayStatus.Busy);
            Num_RepeatCount.Enabled = (play_state_ != PlayStatus.Busy);
            GView_CmdList.Enabled = (play_state_ != PlayStatus.Busy);
        }

        private void UpdateStatusUI()
        {
            /* GridViewを更新 */
            if (play_cmd_index_view_ != play_cmd_index_busy_) {
                /*  */

                /* 変更前に選択状態のコマンドリストを非選択状態にする */
                if (play_cmd_index_view_ >= 0) {
                    GView_CmdList.Rows[play_cmd_index_view_].DefaultCellStyle.BackColor = Color.White;
                }

                play_cmd_index_view_ = play_cmd_index_busy_;

                /* 変更後に選択状態のコマンドリストを選択状態にする */
                if (play_cmd_index_view_ >= 0) {
                    GView_CmdList.Rows[play_cmd_index_view_].DefaultCellStyle.BackColor = COLOR_BUSY_COMMAND;
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
                row_obj.ErrorText = "入力値エラーの項目があります";
            } else {
                row_obj.ErrorText = "";
            }
        }

        private void UpdateEditStatus(DataGridViewCell cell)
        {
            var error_text = "";
            var cmd_target = TBox_Target.Text;

            /* エラー判定 */
            switch ((ColumnId)cell.OwningColumn.Index) {
                case ColumnId.Command:
                {
                    var value_str = cell.Value as string;

                    if (   (value_str != null)
                        && (value_str.Length > 0)
                    ) {
                        if (CommandRunner.FormatCheck(cmd_target, value_str)) {
                            cell.Style.BackColor = COLOR_COMMAND_FORMAT_OK;
                        } else {
                            cell.Style.BackColor = COLOR_COMMAND_FORMAT_NG;
                            error_text = "コマンド式が間違っています";
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
                    "コマンドリスト実行中 {0,3}/{1,-3} | 次のコマンドまでの遅延 {2,6}[ms] | {3}",
                    play_cmd_index_busy_ + 1,
                    GView_CmdList.RowCount,
                    play_next_delay_,
                    (runner_.IsRunning) ? ("コマンド処理中") : ("ウェイト"));

            /* 一時停止中 */
            } else if (play_state_ == PlayStatus.Pause) {
                str.AppendFormat(
                    "コマンドリスト一時停止中 {0,3}/{1,-3}",
                    play_cmd_index_busy_ + 1,
                    GView_CmdList.RowCount);
            }

            FormUiManager.SetStatusText(StatusTextId.SequentialCommandStatus, str.ToString());
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
                            AddCommand(true, config.Command, config.DelayFixed, config.DelayRandom);
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

        private SequentialCommandConfig[] ImportCsvData(StreamReader reader, ColumnId[] columns)
        {
            var configs = new Queue<SequentialCommandConfig>();

            while (!reader.EndOfStream) {
                var config = new SequentialCommandConfig();
                var items = TextUtil.ReadCsvLine(reader);

                foreach (var item in items.Select((v, i) => new {v, i})) {
                    switch (columns[item.i]) {
                        case ColumnId.Command:     config.Command = item.v;                 break;
                        case ColumnId.DelayFixed:  config.DelayFixed = uint.Parse(item.v);  break;
                        case ColumnId.DelayRandom: config.DelayRandom = uint.Parse(item.v); break;
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
                        ExportCsvData(writer, ConfigManager.User.SequentialCommandList.Value);
                    }
                }
            } catch {
            }
        }

        private void ExportCsvHeader(StreamWriter writer)
        {
            writer.WriteLine(TextUtil.WriteCsvLine(Enum.GetNames(typeof(ColumnId))));
        }

        private void ExportCsvData(StreamWriter writer, IEnumerable<SequentialCommandConfig> configs)
        {
            foreach (var config in configs) {
                var items = new Queue<string>();

                foreach (ColumnId column in Enum.GetValues(typeof(ColumnId))) {
                    switch (column) {
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
                control.BackColor = (CommandCheck(value)) ? (COLOR_COMMAND_FORMAT_OK) : (COLOR_COMMAND_FORMAT_NG);
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

            row_obj.Cells[(int)ColumnId.Enable].Value = true;
            row_obj.Cells[(int)ColumnId.Command].Value = "";
            row_obj.Cells[(int)ColumnId.DelayFixed].Value = "0";
            row_obj.Cells[(int)ColumnId.DelayRandom].Value = "0";
        }

        private void GView_CmdList_RowContextMenuStripNeeded(object sender, DataGridViewRowContextMenuStripNeededEventArgs e)
        {
        }

        private void Btn_Import_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.Filter = "CSVファイル(*.csv)|*.csv";

            if (dialog.ShowDialog() != DialogResult.OK)return;

            ImportCsv(dialog.FileName);
        }

        private void Btn_Export_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();

            dialog.Filter = "CSVファイル(*.csv)|*.csv";

            if (dialog.ShowDialog() != DialogResult.OK)return;

            ExportCsv(dialog.FileName);
        }

        private void GView_CmdList_KeyDown(object sender, KeyEventArgs e)
        {
            GView_CmdList.BeginEdit(true);
        }
    }
}
