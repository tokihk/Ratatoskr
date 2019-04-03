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
using RtsCore.Framework.BinaryText;
using RtsCore.Utility;

namespace Ratatoskr.Forms.MainWindow
{
    internal partial class MainWindow_SendDataListPanel : UserControl
    {
        private enum ColumnId
        {
            SendButton,
            SendData,
            PlayListInclude,
            SendTargetType,
            SendTargetCustom,
            DelayFixed,
            DelayRandom,
        }

        private enum ColumnId_Simple
        {
            SendButton = ColumnId.SendButton,
            SendData = ColumnId.SendData,
        }

        private enum PlayStatus
        {
            Reset,
            Pause,
            Busy,
        }

        private readonly Color COLOR_BUSY_COMMAND      = Color.LightSkyBlue;
        private readonly Color COLOR_COMMAND_FORMAT_OK = RtsCore.Parameter.COLOR_OK;
        private readonly Color COLOR_COMMAND_FORMAT_NG = RtsCore.Parameter.COLOR_NG;


        private bool           load_config_busy_ = false;

        private PlayStatus     play_state_ = PlayStatus.Reset;
        private Timer          play_timer_ = new Timer();
        private uint           play_next_delay_ = 0;

        private int            play_data_index_busy_ = -1;
        private int            play_data_index_view_ = -1;

        private SendDataConfig play_data_ = null;

        private uint  play_repeat_count_ = 0;


        public MainWindow_SendDataListPanel()
        {
            InitializeComponent();

            GView_SendDataList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            play_timer_.Tick += OnListPlayTimer;
        }

        public void LoadConfig()
        {
            load_config_busy_ = true;

            /* ターゲット */
            TBox_CommonTarget.Text = ConfigManager.User.SendDataListTarget.Value;

            /* モード */
            switch (ConfigManager.User.SendDataListMode.Value) {
                case SendDataListMode.Simple:   RBtn_Style_Simple.Checked = true;   break;
                default:                        RBtn_Style_Details.Checked = true;  break;
            }

            /* 繰り返し回数 */
            Num_RepeatCount.Value = ConfigManager.User.SendDataListRepeat.Value;

            UpdateOperationUI();
            UpdateStatusUI();

            load_config_busy_ = false;
        }

        private void LoadConfig_SendDataListHeader()
        {
            GView_SendDataList.Columns.Clear();

            var column_id_list = (Array)null;

            switch (ConfigManager.User.SendDataListMode.Value) {
                case SendDataListMode.Simple:   column_id_list = Enum.GetValues(typeof(ColumnId_Simple));   break;
                default:                        column_id_list = Enum.GetValues(typeof(ColumnId));          break;
            }

            foreach (ColumnId id in column_id_list) {
                switch (id) {
                    case ColumnId.SendButton:
                    {
                        var column = new DataGridViewButtonColumn();
                        
                        column.Tag = id;
                        column.Width = 50;
                        column.Resizable = DataGridViewTriState.False;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        GView_SendDataList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.SendData:
                    {
                        var column = new DataGridViewTextBoxColumn();

                        column.Tag = id;
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderText = "Data";
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        column.Width = 200;
                        if (ConfigManager.User.SendDataListMode.Value != SendDataListMode.Simple) {
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        }
                        GView_SendDataList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.PlayListInclude:
                    {
                        var column = new DataGridViewCheckBoxColumn();

                        column.Tag = id;
                        column.HeaderText = "Play";
                        column.Width = 40;
                        column.Resizable = DataGridViewTriState.False;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        GView_SendDataList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.SendTargetType:
                    {
                        var column = new DataGridViewComboBoxColumn();

                        column.Tag = id;
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderText = "Target Type";
                        column.Width = 80;
                        column.Resizable = DataGridViewTriState.False;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                        column.ValueType = typeof(SendDataTargetType);
                        foreach (var type in Enum.GetValues(typeof(SendDataTargetType))) {
                            column.Items.Add(type);
                        }
                        GView_SendDataList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.SendTargetCustom:
                    {
                        var column = new DataGridViewTextBoxColumn();

                        column.Tag = id;
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderText = "Target Name";
                        column.Width = 120;
                        column.Resizable = DataGridViewTriState.False;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        column.DefaultCellStyle.Font = new Font("MS Gothic", 9);
                        GView_SendDataList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.DelayFixed:
                    {
                        var column = new DataGridViewTextBoxColumn();

                        column.Tag = id;
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderText = "Delay(Fixed)" + "[ms]";
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.Width = 120;
                        column.Resizable = DataGridViewTriState.False;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        GView_SendDataList.Columns.Add(column);
                    }
                        break;

                    case ColumnId.DelayRandom:
                    {
                        var column = new DataGridViewTextBoxColumn();

                        column.Tag = id;
                        column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.HeaderText = "Delay(Random)" + "[ms]";
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        column.Width = 120;
                        column.Resizable = DataGridViewTriState.False;
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        GView_SendDataList.Columns.Add(column);
                    }
                        break;

                    default:
                    {
                        var column = new DataGridViewTextBoxColumn();

                        column.Tag = id;
                        GView_SendDataList.Columns.Add(column);
                    }
                        break;
                }
            }
        }

        private void LoadConfig_SendDataListData()
        {
            GView_SendDataList.Rows.Clear();

            foreach (var config in ConfigManager.User.SendDataList.Value) {
                AddGridDataFromConfig(config);
            }
        }

        private void LoadConfig_SendDataList()
        {
            LoadConfig_SendDataListHeader();
            LoadConfig_SendDataListData();
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

            foreach (DataGridViewRow row in GView_SendDataList.Rows) {
                if (row.IsNewRow)break;

                SetConfigFromGridData(row);

                ConfigManager.User.SendDataList.Value.Add(row.Tag as SendDataConfig);
            }
        }

        private void ListPlayReset()
        {
            ListPlayPause();

            play_state_ = PlayStatus.Reset;

            /* 非選択状態 */
            play_data_index_busy_ = -1;

            /* 繰り返し回数を初期化 */
            play_repeat_count_ = 0;

            UpdateOperationUI();
            UpdateStatusUI();
        }

        private void ListPlayStart()
        {
            ListPlayPause();

            play_state_ = PlayStatus.Busy;

            /* 途中で編集されたときを考慮してコマンド番号を補正 */
            if (play_data_index_busy_ >= GView_SendDataList.RowCount) {
                play_data_index_busy_ = -1;
            }

            /* 初回タイマーイベント */
            OnListPlayTimer(play_timer_, EventArgs.Empty);

            UpdateOperationUI();
            UpdateStatusUI();
        }

        private void ListPlayPause()
        {
            play_state_ = PlayStatus.Pause;

            /* 実行中タイマーを停止 */
            if (play_timer_.Enabled) {
                play_timer_.Stop();
            }

            UpdateOperationUI();
            UpdateStatusUI();
        }

        private void ListPlayTimerSetup(uint ival_ms)
        {
            play_timer_.Stop();
            play_timer_.Interval = (int)Math.Max(1, ival_ms);
            play_timer_.Start();
        }

        private void ListPlayTimerRecall()
        {
            ListPlayTimerSetup(1);
        }

        private void OnListPlayTimer(object sender, EventArgs e)
        {
            var timer = sender as Timer;

            /* 初期化処理 */
            if (play_data_index_busy_ < 0) {
                play_data_index_busy_ = 0;
            } else {
                play_data_index_busy_++;
            }

            /* コマンド情報取得 */
            play_data_ = LoadNextPlaySendData(ref play_data_index_busy_);

            if (play_data_ != null) {
                /* --- コマンド情報取得成功 --- */
                /* 次のコマンドまでの遅延時間 */
                play_next_delay_ = play_data_.DelayFixed + (uint)((new Random()).Next((int)play_data_.DelayRandom));

                /* タイマー再起動 */
                ListPlayTimerSetup(play_next_delay_);

                /* コマンド実行 */
                SendDataExec(play_data_);

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
                    ListPlayReset();

                } else {
                    /* --- 繰り返し実行 --- */
                    /* 再チェック */
                    ListPlayTimerRecall();
                }
            }

            UpdateStatusUI();
        }

        private void SetConfigFromGridData(DataGridViewCell cell, SendDataConfig config)
        {
            switch ((ColumnId)cell.OwningColumn.Tag) {
                case ColumnId.SendData:
                    config.SendData = cell.Value as string;
                    break;

                case ColumnId.PlayListInclude:
                    config.PlayListInclude = (bool)cell.Value;
                    break;

                case ColumnId.SendTargetType:
                    config.SendTargetType = (SendDataTargetType)cell.Value;
                    break;

                case ColumnId.SendTargetCustom:
                    config.SendTargetCustom = cell.Value as string;
                    break;

                case ColumnId.DelayFixed:
                {
                    var value = (uint)0;

                    if (uint.TryParse(cell.Value as string, out value)) {
                        config.DelayFixed = value;
                    }
                }
                    break;

                case ColumnId.DelayRandom:
                {
                    var value = (uint)0;

                    if (uint.TryParse(cell.Value as string, out value)) {
                        config.DelayRandom = value;
                    }
                }
                    break;
            }
        }

        private void SetConfigFromGridData(DataGridViewRow row_obj, SendDataConfig config)
        {
            if (config == null)return;

            foreach (DataGridViewCell cell in row_obj.Cells) {
                SetConfigFromGridData(cell, config);
            }
        }

        private void SetConfigFromGridData(DataGridViewRow row_obj)
        {
            SetConfigFromGridData(row_obj, row_obj.Tag as SendDataConfig);
        }

        private void SetGridDataFromConfig(DataGridViewCell cell, SendDataConfig config)
        {
            switch ((ColumnId)cell.OwningColumn.Tag) {
                case ColumnId.SendButton:
                    cell.Value = "Send";
                    break;
                case ColumnId.SendData:
                    cell.Value = config.SendData;
                    break;

                case ColumnId.PlayListInclude:
                    cell.Value = config.PlayListInclude;
                    break;

                case ColumnId.SendTargetType:
                    cell.Value = config.SendTargetType;
                    break;

                case ColumnId.SendTargetCustom:
                    cell.Value = config.SendTargetCustom;
                    break;

                case ColumnId.DelayFixed:
                    cell.Value = config.DelayFixed.ToString();
                    break;

                case ColumnId.DelayRandom:
                    cell.Value = config.DelayRandom.ToString();
                    break;
            }
        }

        private void SetGridDataFromConfig(DataGridViewRow row_obj, SendDataConfig config)
        {
            if (config == null)return;

            foreach (DataGridViewCell cell in row_obj.Cells) {
                SetGridDataFromConfig(cell, config);
            }
        }

        private void SetGridDataFromConfig(DataGridViewRow row_obj)
        {
            SetGridDataFromConfig(row_obj, row_obj.Tag as SendDataConfig);
        }

        private void AddGridDataFromConfig(SendDataConfig config)
        {
            /* 新規行を追加 */
            var row_index = GView_SendDataList.Rows.Add();

            if (row_index < 0)return;

            var row_obj = GView_SendDataList.Rows[row_index];

            row_obj.Tag = new SendDataConfig(config);

            /* 値を書き換え */
            SetGridDataFromConfig(row_obj);

            /* エラー表示を更新 */
            UpdateEditStatus(row_obj);
        }

        private SendDataConfig GetSendDataConfig(int index)
        {
            try {
                /* 範囲外 */
                if ((index < 0) || (index >= GView_SendDataList.RowCount))return (null);

                var row_obj = GView_SendDataList.Rows[(int)index];

                /* New行 */
                if (row_obj.IsNewRow)return (null);

                SetConfigFromGridData(row_obj);

                return (row_obj.Tag as SendDataConfig);
            } catch {
                return (null);
            }
        }

        private SendDataConfig LoadNextPlaySendData(ref int index)
        {
            for (; index < GView_SendDataList.RowCount; index++) {
                var config = GetSendDataConfig(index);

                if (   (config == null)
                    || (!config.PlayListInclude)
                ) {
                    continue;
                }

                var config_play = new SendDataConfig(config);

                if (ConfigManager.User.SendDataListMode.Value == SendDataListMode.Simple) {
                    config_play.DelayFixed = (uint)Num_SendInterval_Fixed.Value;
                    config_play.DelayRandom = (uint)Num_SendInterval_Random.Value;
                }

                return (config_play);
            }

            return (null);
        }

        private void SendDataExec(SendDataConfig config)
        {
            var target = "*";

            /* 送信先補正 */
            switch (config.SendTargetType) {
                case SendDataTargetType.System:
                    target = FormUiManager.SendTarget;
                    break;
                case SendDataTargetType.Common:
                    target = TBox_CommonTarget.Text;
                    break;
                case SendDataTargetType.Custom:
                    target = config.SendTargetCustom;
                    break;
            }

            if (ConfigManager.User.SendDataListMode.Value == SendDataListMode.Simple) {
                target = FormUiManager.SendTarget;
            }

            /* 送信実行 */
            Program.API.API_SendData(target, config.SendData);
        }

        private void UpdateOperationUI()
        {
            /* Play/Pause/Stopボタン */
            Btn_Play.Image = (play_state_ == PlayStatus.Busy)
                           ? (RtsCore.Resource.Images.pause_32x32)
                           : (RtsCore.Resource.Images.play_32x32);
            Btn_Play.Text = (play_state_ == PlayStatus.Busy)
                          ? ("Pause")
                          : ("Play");

            Btn_Stop.Enabled = (play_state_ != PlayStatus.Reset);

            /* コントロールの操作可能状態を更新 */
            var enable = play_state_ != PlayStatus.Busy;

            GBox_Style.Enabled = enable;
            TBox_CommonTarget.Enabled = enable;
            Num_RepeatCount.Enabled = enable;
            GView_SendDataList.Enabled = enable;
            GBox_SendInterval.Enabled = enable;

            /* コントロールの可視状態を更新 */
            var is_details = (ConfigManager.User.SendDataListMode.Value == SendDataListMode.Details);

            GBox_CommonTarget.Visible = is_details;
            Btn_Play.Visible = is_details;
            Btn_Stop.Visible = is_details;
            GBox_NumberOfRepeat.Visible = is_details;
            GBox_SendInterval.Visible = !is_details;
        }

        private void UpdateStatusUI()
        {
            /* GridViewを更新 */
            if (play_data_index_view_ != play_data_index_busy_) {
                /*  */

                /* 変更前に選択状態のコマンドリストを非選択状態にする */
                if (play_data_index_view_ >= 0) {
                    GView_SendDataList.Rows[play_data_index_view_].DefaultCellStyle.BackColor = Color.White;
                }

                play_data_index_view_ = play_data_index_busy_;

                /* 変更後に選択状態のコマンドリストを選択状態にする */
                if (play_data_index_view_ >= 0) {
                    GView_SendDataList.Rows[play_data_index_view_].DefaultCellStyle.BackColor = COLOR_BUSY_COMMAND;
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
            switch ((ColumnId)cell.OwningColumn.Tag) {
                case ColumnId.SendData:
                {
                    if (cell.Value is string value_str) {
                        if (value_str.Length > 0) {
                            if (HexTextEncoder.ToByteArray(value_str) != null) {
                                cell.Style.BackColor = COLOR_COMMAND_FORMAT_OK;
                            } else {
                                cell.Style.BackColor = COLOR_COMMAND_FORMAT_NG;
                                error_text = "Command incorrect";
                            }
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
                    GView_SendDataList.RowCount,
                    "Next command",
                    play_next_delay_,
                    "Delay waiting");

            /* 一時停止中 */
            } else if (play_state_ == PlayStatus.Pause) {
                str.AppendFormat(
                    "{0} {1,3}/{2,-3}",
                    "Command list pause",
                    play_data_index_busy_ + 1,
                    GView_SendDataList.RowCount);
            }

            FormUiManager.SetStatusText(StatusTextID.SequentialCommandStatus, str.ToString());
        }

        private void ImportCsv(string path)
        {
            using (var reader = new StreamReader(new FileStream(path, FileMode.Open)))
            {
                /* ヘッダー情報を読み込み */
                var columns = ImportCsvHeader(reader);

                /* データ情報を読み込み */
                var configs = ImportCsvData(reader, columns);

                /* 適用 */
                GView_SendDataList.Rows.Clear();
                foreach (var config in configs) {
                    AddGridDataFromConfig(config);
                }
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
                        case ColumnId.PlayListInclude:
                            config.PlayListInclude = bool.Parse(item.v);
                            break;
                        case ColumnId.SendTargetType:
                            config.SendTargetType = (SendDataTargetType)Enum.Parse(typeof(SendDataTargetType), item.v);
                            break;
                        case ColumnId.SendTargetCustom:
                            config.SendTargetCustom = item.v;
                            break;
                        case ColumnId.SendData:
                            config.SendData = item.v;
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

            using (var writer = new StreamWriter(new FileStream(path, FileMode.Create)))
            {
                /* ヘッダー情報を書き込み */
                ExportCsvHeader(writer);

                /* データ情報を書き込み */
                ExportCsvData(writer, ConfigManager.User.SendDataList.Value);
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
                        case ColumnId.PlayListInclude:       items.Enqueue(config.PlayListInclude.ToString());        break;
                        case ColumnId.SendTargetType:   items.Enqueue(config.SendTargetType.ToString());    break;
                        case ColumnId.SendTargetCustom: items.Enqueue(config.SendTargetCustom);             break;
                        case ColumnId.SendData:      items.Enqueue(config.SendData);                  break;
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
                ListPlayPause();

            /* 停止 or 一時停止 ⇒ 再生 */
            } else {
                ListPlayStart();
            }
        }

        private void Btn_Stop_Click(object sender, EventArgs e)
        {
            ListPlayReset();
        }

        private void GView_SendDataList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (sender is DataGridView gview) {
                var row_obj = gview.Rows[e.RowIndex];
                var cell_obj = row_obj.Cells[e.ColumnIndex];

                switch ((ColumnId)cell_obj.OwningColumn.Tag) {
                    case ColumnId.SendData:
                    {
                        /* 編集開始時に背景色をクリア */
                        cell_obj.Style.BackColor = Color.White;
                    }
                        break;
                }
            }
        }

        private void GView_SendDataList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView gview) {
                var row_obj = gview.Rows[e.RowIndex];

                /* 編集データを反映 */
                SetConfigFromGridData(row_obj);
                SetGridDataFromConfig(row_obj);

                /* エラー表示を更新 */
                UpdateEditStatus(row_obj);
            }
        }

        private void GView_SendDataList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            /* 編集中動作 */
            if (sender is DataGridView control) {
                var row_obj = control.CurrentRow;
                var cell_obj = control.CurrentCell;

                e.Control.Tag = cell_obj;

                if (e.Control is DataGridViewTextBoxEditingControl control_sub) {
                    /* 初回イベント */
                    GView_SendDataList_EditingControlShowing_TextChanged(control_sub, EventArgs.Empty);

                    /* これ以降はイベントで処理 */
                    control_sub.TextChanged -= GView_SendDataList_EditingControlShowing_TextChanged;
                    control_sub.TextChanged += GView_SendDataList_EditingControlShowing_TextChanged;
                }
            }
        }

        private void GView_SendDataList_EditingControlShowing_TextChanged(object sender, EventArgs e)
        {
            if (sender is DataGridViewTextBoxEditingControl control) {
                var cell_obj = control.Tag as DataGridViewCell;
                var value = control.Text;

                switch ((ColumnId)cell_obj.OwningColumn.Tag) {
                    case ColumnId.SendData:
                    {
                        if ((value != null) && (value.Length > 0)) {
                            control.BackColor = (BinaryTextCompiler.Build(value) != null)
                                              ? (COLOR_COMMAND_FORMAT_OK)
                                              : (COLOR_COMMAND_FORMAT_NG);
                        } else {
                            control.BackColor = Color.White;
                        }
                    }
                        break;
                }
            }
        }

        private void GView_SendDataList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var control = sender as DataGridView;
            var row_obj = control.Rows[e.RowIndex];
            var cell_obj = row_obj.Cells[e.ColumnIndex];

            if (row_obj.IsNewRow)return;

            var config = row_obj.Tag as SendDataConfig;

            switch ((ColumnId)cell_obj.OwningColumn.Tag) {
                case ColumnId.SendButton:
                    SendDataExec(config);
                    break;
            }
        }

        private void GView_SendDataList_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            /* 初期値設定 */
            e.Row.Tag = new SendDataConfig();

            SetGridDataFromConfig(e.Row);
        }

        private void GView_SendDataList_RowContextMenuStripNeeded(object sender, DataGridViewRowContextMenuStripNeededEventArgs e)
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
            GView_SendDataList.BeginEdit(true);
        }

        private void RBtn_Style_Simple_CheckedChanged(object sender, EventArgs e)
        {
            ConfigManager.User.SendDataListMode.Value = SendDataListMode.Simple;

            if (!load_config_busy_) {
                BackupConfig_SendDataList();
            }
            LoadConfig_SendDataList();
            UpdateOperationUI();
        }

        private void RBtn_Style_Details_CheckedChanged(object sender, EventArgs e)
        {
            ConfigManager.User.SendDataListMode.Value = SendDataListMode.Details;

            if (!load_config_busy_) {
                BackupConfig_SendDataList();
            }
            LoadConfig_SendDataList();
            UpdateOperationUI();
        }

        private void GView_SendDataList_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
