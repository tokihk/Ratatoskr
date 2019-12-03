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
    internal partial class MainWindow_SendFrameListPanel : UserControl
    {
        private enum ColumnId
        {
            ProtocolID,
			FrameBitLength,
			FrameBitData,
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


        public MainWindow_SendFrameListPanel()
        {
            InitializeComponent();

            play_timer_.Tick += OnListPlayTimer;
        }

		private void InitializeSendFrameList()
		{
			LView_SendFrameList.BeginUpdate();
			{
				/* 先にデータをすべて削除してからヘッダーを削除する */
				LView_SendFrameList.ItemClear();
				LView_SendFrameList.Columns.Clear();

				/* メイン要素 */
				LView_SendFrameList.Columns.Add(
					new ColumnHeader() {
						Text = "No.",
						Width = 80
					}
				);

				/* サブ要素 */
				foreach (ColumnId id in Enum.GetValues(typeof(ColumnId))) {
					var column = new ColumnHeader();

					switch (id) {
						case ColumnId.ProtocolID:
							column.Text = "Protocol";
							column.Width = 120;
							break;
						case ColumnId.FrameBitLength:
							column.Text = "Bit Length";
							column.Width = 120;
							break;
						case ColumnId.FrameBitData:
							column.Text = "Bit Data";
							column.Width = -1;
							break;
					}
				}
			}
			LView_SendFrameList.EndUpdate();
		}

		public void LoadConfig()
        {
            load_config_busy_ = true;

            /* ターゲット */
            TBox_SendTarget.Text = ConfigManager.User.SendFrameListTarget.Value;

            /* 繰り返し回数 */
            Num_RepeatCount.Value = ConfigManager.User.SendFrameListRepeat.Value;

            UpdateOperationUI();

            load_config_busy_ = false;
        }

        private void LoadConfig_SendFrameList()
        {
            foreach (var config in ConfigManager.User.SendFrameList.Value) {
            }
        }

        public void BackupConfig()
        {
            /* ターゲット */
            ConfigManager.User.SendFrameListTarget.Value = TBox_SendTarget.Text;

            /* フレームリスト */
            BackupConfig_SendFrameList();

            /* 繰り返し回数 */
            ConfigManager.User.SendFrameListRepeat.Value = Num_RepeatCount.Value;
        }

        private void BackupConfig_SendFrameList()
        {
            ConfigManager.User.SendFrameList.Value.Clear();
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
        }

        private void ListPlayStart()
        {
            ListPlayPause();

            play_state_ = PlayStatus.Busy;

            UpdateOperationUI();
        }

        private void ListPlayPause()
        {
            play_state_ = PlayStatus.Pause;

            /* 実行中タイマーを停止 */
            if (play_timer_.Enabled) {
                play_timer_.Stop();
            }

            UpdateOperationUI();
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

            TBox_SendTarget.Enabled = enable;
            Num_RepeatCount.Enabled = enable;
            LView_SendFrameList.Enabled = enable;
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
    }
}
