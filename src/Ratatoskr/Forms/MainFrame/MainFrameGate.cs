using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Devices;
using Ratatoskr.Gate;
using Ratatoskr.Generic;

namespace Ratatoskr.Forms.MainFrame
{
    internal partial class MainFrameGate : UserControl
    {
        public class ButtonEx : Button
        {
            public ButtonEx()
            {
                SetStyle(ControlStyles.Selectable, false);
            }
        }


        private const int MOUSE_HOLD_TIMER = 1000;
        private const int DRAW_IMAGE_SPACE = 2;
        private const int DRAW_TEXT_ALIAS_TOP = 0;
        private const int DRAW_TEXT_TYPE_TOP  = 16;
        private const int DRAW_TEXT_TYPE_LEFT = 10;

        private Font font_alias_ = new Font("Courier New", 9);
        private Font font_type_  = new Font("Arial", 7);

        private GateObject gate_ = null;
        private MainFrameDeviceControlPanel devcp_ = new MainFrameDeviceControlPanel();

        private MouseButtons mouse_busy_button_ = MouseButtons.None;
        private Timer        mouse_hold_timer_ = new Timer();

        private ToolTip ttip_main_ = new ToolTip();


        public MainFrameGate()
        {
            InitializeComponent();

            devcp_.FormClosing += Devcp_FormClosing;

            mouse_hold_timer_.Interval = MOUSE_HOLD_TIMER;
            mouse_hold_timer_.Tick += OnMouseHoldTimer;

            UpdateView();
            UpdateDeviceControlPanel();
        }

        public GateObject Gate
        {
            get { return (gate_); }
            set {
                if (gate_ == value)return;

                if (gate_ != null) {
                    /* 登録イベント解除 */
                    gate_.StatusChanged -= OnGateStatusChanged;
                }

                gate_ = value;

                if (gate_ != null) {
                    /* イベント登録 */
                    gate_.StatusChanged += OnGateStatusChanged;
                }

                UpdateView();
            }
        }

        public Color ImageColor
        {
            get { return (Btn_Main.BackColor); }
            set { Btn_Main.BackColor = value;  }
        }

        private delegate void UpdateViewDelegate();
        private void UpdateView()
        {
            /* Invoke */
            if (InvokeRequired) {
                BeginInvoke(new UpdateViewDelegate(UpdateView));
                return;
            }

            UpdateToolTip();
            Refresh();
        }

        private void UpdateToolTip()
        {
            var str = new StringBuilder();

            if (   (gate_ != null)
                && (gate_.Device != null)
            ) {
                str.AppendLine(gate_.Device.Class.Name);
                str.AppendLine(gate_.Device.GetStatusString());
                
                str.AppendLine("マウス左クリック: 接続/切断");
                str.AppendLine("マウス右クリック: 編集");
                str.Append("マウス右ホールド: 解放");

            } else {
                str.AppendLine("Empty");
                str.AppendLine("マウス左クリック: 編集");
                str.Append("マウス右ホールド: 編集");
            }

            ttip_main_.SetToolTip(Btn_Main, str.ToString());
        }

        private void UpdateDeviceControlPanel()
        {
            var devcp = (Control)null;

            if ((gate_ != null) && (gate_.Device != null)) {
                devcp = gate_.Device.CreateControlPanel();
            }

            /* コントロールパネル作成 */
            devcp_.ContentsControl = devcp;
            devcp_.Visible = (devcp != null);
            Btn_ControlPanel.Enabled = (devcp != null);
        }

        private void GateEdit()
        {
            if (gate_ == null)return;

            /* === 編集 === */
            var edit_form = new GateEditForm.GateEditForm();

            edit_form.Alias = gate_.Alias;

            if (gate_.Device != null) {
                /* === 既存の設定で上書き === */
                edit_form.Class = gate_.Device.Class;
                edit_form.Property = gate_.Device.Property.Clone();
            }

            /* === ゲート編集ダイアログ実行 === */
            if (edit_form.ShowDialog() != DialogResult.OK)return;

            gate_.Alias = edit_form.Alias;

            /* === デバイスインスタンス準備 === */
            var devi = gate_.Device;

            if (   (gate_.Device == null)                                          /* 設定されているデバイスインスタンスが存在しない   */
                || (gate_.Device.Class != edit_form.Class)                         /* 異なるデバイスクラスが選択された                 */
                || (!ClassUtil.Compare(gate_.Device.Property, edit_form.Property)) /* プロパティ値が変化した                           */
            ) {
                /* 新規デバイスインスタンス作成 */
                devi = GateManager.CreateDeviceObject(edit_form.Class.ID, edit_form.Alias, edit_form.Property);

                /* 新規割り当てのときは接続状態から開始 */
                if (gate_.Device == null) {
                    gate_.ConnectRequest = true;
                }
            }

            /* === ゲートにデバイスインスタンスを設定 */
            gate_.Device = devi;
        }

        private void OnGateStatusChanged()
        {
            UpdateView();
            UpdateDeviceControlPanel();
        }

        private void OnMouseLeftClick()
        {
            if ((gate_ != null) && (gate_.Device != null)) {
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
            if (gate_.Device != null) {
                if (MessageBox.Show("Do you want to initialize this gate?", "Ratatoskr", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                    gate_.Device.ShutdownRequest();
                    gate_.Device = null;
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
            Btn_Main.Invalidate();
        }

        private void Btn_Main_Paint(object sender, PaintEventArgs e)
        {
            var image = Properties.Resources.connect_off;
            var image_rect = new Rectangle();

            var text_brush = Brushes.Gray;

            var text_alias = "";
            var text_alias_rect = new Rectangle();

            var text_type = "";
            var text_type_rect = new Rectangle();

            image_rect.Height = e.ClipRectangle.Height - DRAW_IMAGE_SPACE * 2;
            image_rect.Width = image_rect.Height;
            image_rect.X = DRAW_IMAGE_SPACE;
            image_rect.Y = DRAW_IMAGE_SPACE;

            text_alias_rect.X = image_rect.Right + DRAW_IMAGE_SPACE;
            text_alias_rect.Y = DRAW_TEXT_ALIAS_TOP;
            text_alias_rect.Width = e.ClipRectangle.Width - text_alias_rect.X;
            text_alias_rect.Height = e.ClipRectangle.Height;

            text_type_rect.X = image_rect.Right + DRAW_IMAGE_SPACE + DRAW_TEXT_TYPE_LEFT;
            text_type_rect.Y = DRAW_TEXT_TYPE_TOP;
            text_type_rect.Width = e.ClipRectangle.Width - text_type_rect.X;
            text_type_rect.Height = e.ClipRectangle.Height;

            if (gate_ != null) {
                text_alias = gate_.Alias;
                text_type = "(Empty)";

                if (gate_.Device != null) {
                    text_type = gate_.Device.Class.Name;
                    text_brush = Brushes.Black;

                    /* 状態アイコン設定 */
                    switch (gate_.ConnectStatus) {
                        case ConnectState.Connected:    image = Properties.Resources.connect_on;    break;
                        case ConnectState.Disconnected: image = Properties.Resources.connect_off;   break;
                        default:                        image = Properties.Resources.connect_busy;  break;
                    }
                }
            }

            /* アイコン描画 */
            e.Graphics.DrawImage(image, image_rect);

            /* エイリアス表示 */
            e.Graphics.DrawString(text_alias, font_alias_, text_brush, text_alias_rect);

            /* デバイスタイプ表示 */
            e.Graphics.DrawString(text_type, font_type_, text_brush, text_type_rect);
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
