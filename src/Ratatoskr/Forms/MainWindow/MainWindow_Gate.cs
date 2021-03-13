using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Forms.Dialog;
using Ratatoskr.Gate;
using Ratatoskr.General;

namespace Ratatoskr.Forms.MainWindow
{
    internal partial class MainWindow_Gate : UserControl
    {
        private const int MOUSE_HOLD_TIMER = 1000;

        private readonly Padding DATA_RATE_GRAPH_MARGIN = new Padding(5, 2, 5, 4);

        private readonly Font  DATA_RATE_FONT = new Font("MS Gothic", 8);
        private readonly Brush DATA_RATE_FONT_BRUSH = Brushes.Gray;
        private StringFormat   DATA_RATE_FONT_FORMAT = new StringFormat();
        private Rectangle      DATA_RATE_GRAPH_REGION = new Rectangle();
        private Pen            DATA_RATE_GRAPH_PEN = Pens.Gray;

        private GateObject gate_ = null;
        private MainWindow_DeviceControlPanel devcp_ = new MainWindow_DeviceControlPanel();

        private MouseButtons mouse_busy_button_ = MouseButtons.None;
        private Timer        mouse_hold_timer_ = new Timer();

        private ToolTip ttip_main_ = new ToolTip();

        private ulong[] data_rate_buffer_ = null;
        private int     data_rate_in_ = 0;
        private ulong   data_rate_latest_ = 0;

        private bool details_mode_ = false;


        public MainWindow_Gate()
        {
            InitializeComponent();

            devcp_.FormClosing += Devcp_FormClosing;

            mouse_hold_timer_.Interval = MOUSE_HOLD_TIMER;
            mouse_hold_timer_.Tick += OnMouseHoldTimer;

            DATA_RATE_FONT_FORMAT.Alignment = StringAlignment.Center;
            DATA_RATE_FONT_FORMAT.LineAlignment = StringAlignment.Center;
            DATA_RATE_GRAPH_REGION.X = DATA_RATE_GRAPH_MARGIN.Left;
            DATA_RATE_GRAPH_REGION.Y = DATA_RATE_GRAPH_MARGIN.Top;
            DATA_RATE_GRAPH_REGION.Width = PBox_DataRate.ClientSize.Width - DATA_RATE_GRAPH_MARGIN.Horizontal;
            DATA_RATE_GRAPH_REGION.Height = PBox_DataRate.ClientSize.Height - DATA_RATE_GRAPH_MARGIN.Vertical;

            /* 幅に合わせてバッファを初期化 */
            data_rate_buffer_ = new ulong[DATA_RATE_GRAPH_REGION.Width];
        }

        public MainWindow_Gate(GateObject gate) : this()
        {
            Gate = gate;
        }

        public GateObject Gate
        {
            get { return (gate_); }
            set {
                if (gate_ != value) {
                    if (gate_ != null) {
                        /* 登録イベント解除 */
                        gate_.StatusChanged -= OnGateStatusChanged;
                        gate_.DataRateUpdated -= OnGateDataRateUpdated;
                    }

                    gate_ = value;

                    if (gate_ != null) {
                        /* 背景色更新 */
                        Btn_Main.BackColor = gate_.GateProperty.Color;

                        /* イベント登録 */
                        gate_.StatusChanged += OnGateStatusChanged;
                        gate_.DataRateUpdated += OnGateDataRateUpdated;
                    }
                }

                UpdateView();
                UpdateDeviceControlPanel();
            }
        }

        public bool DetailsMode
        {
            get { return (details_mode_); }
            set
            {
                details_mode_ = value;
            }
        }

        private delegate void UpdateViewHandler();
        private void UpdateView()
        {
            if (InvokeRequired) {
                BeginInvoke(new UpdateViewHandler(UpdateView));
                return;
            }

            UpdateToolTip();
            Refresh();
        }

        private delegate void UpdateDataRateViewHandler();
        private void UpdateDataRateView()
        {
            if (InvokeRequired) {
                BeginInvoke(new UpdateDataRateViewHandler(UpdateDataRateView));
                return;
            }

            PBox_DataRate.Invalidate();
        }

        private void UpdateToolTip()
        {
            var str = new StringBuilder();

            if (   (gate_ != null)
                && (gate_.IsDeviceSetup)
            ) {
                str.AppendLine(gate_.DeviceClassName);
                str.AppendLine(gate_.DeviceStatusText);
                
                str.AppendLine("Left click: Connect/Disconnect");
                str.AppendLine("Right click: Edit");
                str.Append("Right hold: Release");

            } else {
                str.AppendLine("Empty");
                str.AppendLine("Left click: Edit");
                str.Append("Right hold: Edit");
            }

            ttip_main_.SetToolTip(Btn_Main, str.ToString());
        }

        private void UpdateDeviceControlPanel()
        {
            var devcp = (Control)null;

            if (   (gate_ != null)
                && (gate_.IsDeviceSetup)
            ) {
                devcp = gate_.CreateDeviceControlPanel();
            }

            /* コントロールパネル作成 */
            devcp_.ContentsControl = devcp;
            devcp_.Visible = (devcp != null);
        }

        private void GateEdit()
        {
            if (gate_ == null)return;

            var gatep   = gate_.GateProperty;
            var devconf = gate_.DeviceConfig;
            var devc_id = gate_.DeviceClassID;
            var devp    = gate_.DeviceProperty;

            /* 編集 */
            var edit_form = new GateEditDialog(gatep, devconf, devc_id, devp);

            if (edit_form.ShowDialog() != DialogResult.OK)return;

            /* 適用 */
            gate_.ChangeDevice(edit_form.GateProperty, edit_form.DeviceConfig, edit_form.DeviceClassID, edit_form.DeviceProperty);
        }

        private void DataRateValueInput(ulong value)
        {
            /* 入力データを記憶 */
            data_rate_latest_ = value;

            /* グラフ表示用に入力データを補正 */
            value = Math.Min(value, gate_.GateProperty.DataRateGraphLimit) * (ulong)DATA_RATE_GRAPH_REGION.Height;
            if (value > 0) {
                value = value / gate_.GateProperty.DataRateGraphLimit;
            }

            /* グラフ表示用に入力データを補正して記憶 */
            data_rate_buffer_[data_rate_in_++] = value;

            /* 入力ポインタを移動 */
            data_rate_in_ %= data_rate_buffer_.Length;

            PBox_DataRate.Invalidate();
        }

        private void OnGateStatusChanged(object sender, EventArgs e)
        {
            UpdateView();
            UpdateDeviceControlPanel();
        }

        private void OnGateDataRateUpdated(object sender, ulong data_rate)
        {
            DataRateValueInput(data_rate);
        }

        private void OnMouseLeftClick()
        {
            if ((gate_ != null) && (gate_.IsDeviceSetup)) {
                gate_.ConnectRequest = !gate_.ConnectRequest;
            } else {
                GateEdit();
            }
        }

        private void OnMouseRightClick()
        {
            GateEdit();
        }

        private void OnMouseRightHold()
        {
            if (gate_.IsDeviceSetup) {
                if (MessageBox.Show("Do you want to initialize this gate?", "Ratatoskr", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    gate_.ReleaseDevice();
                }
            }
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (mouse_busy_button_ != MouseButtons.None)return;

            /* マウス監視開始 */
            mouse_busy_button_ = e.Button;
            mouse_hold_timer_.Start();
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (mouse_busy_button_ != e.Button)return;

            /* タイマー停止 */
            mouse_hold_timer_.Stop();

            /* マウス操作(クリック)イベント */
            switch (mouse_busy_button_) {
                case MouseButtons.Left:     OnMouseLeftClick();     break;
                case MouseButtons.Right:    OnMouseRightClick();    break;
                default:                                            break;
            }

            /* マウス監視終了 */
            mouse_busy_button_ = MouseButtons.None;
        }

        private void OnMouseHoldTimer(object sender, EventArgs e)
        {
            if (mouse_busy_button_ == MouseButtons.None)return;

            /* タイマー停止 */
            mouse_hold_timer_.Stop();

            /* マウス操作(ホールド)イベント */
            switch (mouse_busy_button_) {
                case MouseButtons.Right: OnMouseRightHold();    break;
                default:                                        break;
            }
            
            /* マウス監視終了 */
            mouse_busy_button_ = MouseButtons.None;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Btn_Main.Refresh();
        }

        private void PBox_DataRate_Paint(object sender, PaintEventArgs e)
        {
            /* === グラフ === */
            var points = new Point[data_rate_buffer_.Length];
            var data_offset = data_rate_in_;

            for (var data_count = 0; data_count < data_rate_buffer_.Length; data_count++) {
                points[data_count].X = DATA_RATE_GRAPH_REGION.Left + data_count;
                points[data_count].Y = DATA_RATE_GRAPH_REGION.Bottom - (int)data_rate_buffer_[data_offset];

                data_offset++;
                data_offset %= data_rate_buffer_.Length;
            }
            e.Graphics.DrawLines(DATA_RATE_GRAPH_PEN, points);

            /* テキスト */
            e.Graphics.DrawString(
                String.Format(
                    "Rate: {0,7}B/s",
                    TextUtil.DecToText(data_rate_latest_)),
                    DATA_RATE_FONT,
                    DATA_RATE_FONT_BRUSH,
                    (sender as Control).ClientRectangle,
                    DATA_RATE_FONT_FORMAT);
        }

        private void Btn_ControlPanel_Click(object sender, EventArgs e)
        {
            devcp_.Visible = !devcp_.Visible;
        }

        private void Devcp_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            devcp_.Visible = false;
        }
    }
}
