using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Gate;
using RtsCore.Framework.Device;

namespace Ratatoskr.Forms.MainWindow
{
    internal class MainWindow_GateButton : Button
    {
        private readonly Padding IMAGE_PADDING = new Padding(2);
        private readonly Padding GATE_ALIAS_PADDING = new Padding(0, 0, 0, 0);
        private readonly Padding GATE_TYPE_PADDING  = new Padding(10, 16, 0, 0);

        private readonly Padding CORNER_SIZE = new Padding(10);

        private readonly Font GATE_ALIAS_FONT = new Font("Terminal", 9);
        private readonly Font GATE_TYPE_FONT  = new Font("Arial", 7);

        private GraphicsPath    gpath_surface_;

        private Color           back_color_;
        private Brush           back_color_brush_ = null;
        private Brush           back_color_s_brush_ = null;

//        private Brush shadow_b


        public MainWindow_GateButton()
        {
            UpdateDrawBrush();
        }

        public bool Selectable
        {
            get
            {
                return (GetStyle(ControlStyles.Selectable));
            }
            set
            {
                SetStyle(ControlStyles.Selectable, false);
            }
        }

        public bool IsMouseEnter { get; private set; } = false;

        private GateObject Gate
        {
            get
            {
                if (Parent == null)return (null);

                return ((Parent as MainWindow_Gate).Gate);
            }
        }

        private GraphicsPath GetDrawPath_Surface()
        {
            var gpath = new GraphicsPath();
            var csize = new Size(Width, Height);

            gpath.AddArc(0,                               0,                                 CORNER_SIZE.Left, CORNER_SIZE.Top,    180, 90);
            gpath.AddArc(csize.Width - CORNER_SIZE.Right, 0,                                 CORNER_SIZE.Left, CORNER_SIZE.Top,    270, 90);
            gpath.AddArc(csize.Width - CORNER_SIZE.Right, csize.Height - CORNER_SIZE.Bottom, CORNER_SIZE.Left, CORNER_SIZE.Bottom, 0,   90);
            gpath.AddArc(0,                               csize.Height - CORNER_SIZE.Bottom, CORNER_SIZE.Left, CORNER_SIZE.Bottom, 90,  90);
            gpath.CloseFigure();

            return (gpath);
        }

        private Image GetGateStatusImage(GateObject gate)
        {
            var image = RtsCore.Resource.Images.connect_off;

            if (gate != null) {
                switch (gate.ConnectStatus) {
                    case ConnectState.Connected:    image = RtsCore.Resource.Images.connect_on;    break;
                    case ConnectState.Disconnected: image = RtsCore.Resource.Images.connect_off;   break;
                    default:                        image = RtsCore.Resource.Images.connect_busy;  break;
                }
            }

            return (image);
        }

        private void UpdateDrawPath()
        {
            gpath_surface_ = GetDrawPath_Surface();
        }

        private void UpdateDrawBrush(bool force = false)
        {
            if ((!force) && (back_color_ == BackColor))return;

            back_color_ = BackColor;

            back_color_brush_?.Dispose();
            back_color_brush_ = new SolidBrush(back_color_);

            back_color_s_brush_?.Dispose();
            back_color_s_brush_ = new SolidBrush(
                Color.FromArgb(
                    Math.Min(back_color_.A + 0, 255),
                    Math.Min(back_color_.R + 15, 255),
                    Math.Min(back_color_.G + 15, 255),
                    Math.Min(back_color_.B + 15, 255)));
        }

		protected override void OnCreateControl()
		{
			base.OnCreateControl();

			UpdateDrawBrush(true);
		}

		protected override void OnMouseEnter(EventArgs e)
        {
            IsMouseEnter = true;

            base.OnMouseEnter(e);

            Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            IsMouseEnter = false;

            base.OnMouseLeave(e);

            Refresh();
        }

        protected override void OnResize(EventArgs e)
        {
            UpdateDrawPath();

            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            if (DesignMode) {
                base.OnPaint(pevent);
                return;
            }

            UpdateDrawBrush();

            var g = pevent.Graphics;

            var gate = Gate;

            var region_rect = ClientRectangle;

            var image_rect = new Rectangle();
            var image = GetGateStatusImage(gate);

            var text_brush = Brushes.Gray;

            var text_alias = "";
            var text_alias_rect = new Rectangle();

            var text_type = "";
            var text_type_rect = new Rectangle();

            image_rect.Height = region_rect.Height - IMAGE_PADDING.Vertical;
            image_rect.Width = image_rect.Height;
            image_rect.X = IMAGE_PADDING.Left;
            image_rect.Y = IMAGE_PADDING.Top;

            text_alias_rect.X = image_rect.Right + IMAGE_PADDING.Right + GATE_ALIAS_PADDING.Left;
            text_alias_rect.Y = GATE_ALIAS_PADDING.Top;
            text_alias_rect.Width = region_rect.Width - text_alias_rect.X;
            text_alias_rect.Height = region_rect.Height - GATE_ALIAS_PADDING.Vertical;

            text_type_rect.X = image_rect.Right + IMAGE_PADDING.Right + GATE_TYPE_PADDING.Left;
            text_type_rect.Y = GATE_TYPE_PADDING.Top;
            text_type_rect.Width = region_rect.Width - text_type_rect.X;
            text_type_rect.Height = region_rect.Height;

            if (gate != null) {
                text_alias = gate.Alias;
                text_type = "(Empty)";

                if (gate.IsDeviceSetup) {
                    text_type = gate.DeviceClassName;
                    text_brush = Brushes.Black;
                }
            }

            /* 描画品質設定 */
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            /* 背景初期化 */
            g.FillRectangle(SystemBrushes.Control, DisplayRectangle);

            /* 背景色で塗りつぶし */
            g.FillPath(IsMouseEnter ? back_color_s_brush_ : back_color_brush_, gpath_surface_);

            /* アイコン描画 */
            g.DrawImage(image, image_rect);

            /* エイリアス表示 */
            g.DrawString(text_alias, GATE_ALIAS_FONT, text_brush, text_alias_rect);

            /* デバイスタイプ表示 */
            g.DrawString(text_type, GATE_TYPE_FONT, text_brush, text_type_rect);

            /* 枠を描画 */
            g.DrawPath(SystemPens.Highlight, gpath_surface_);

//            base.OnPaint(pevent);
        }
    }
}
