using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.Forms
{
    public class ButtonEx : Button
    {
        private Padding         corner_size_ = new Padding();

        private GraphicsPath    gpath_surface_;

        private Color           back_color_;
        private Brush           back_color_brush_;
        private Brush           back_color_s_brush_;

//        private Brush shadow_b


        public ButtonEx()
        {
            back_color_ = BackColor;
            back_color_brush_ = new SolidBrush(back_color_);

            Corner = new Padding(10);
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

        public Padding Corner
        {
            get { return (corner_size_); }
            set
            {
                corner_size_ = value;
                UpdateDrawPath();
            }
        }

        private GraphicsPath GetDrawPath_Surface()
        {
            var gpath = new GraphicsPath();
            var csize = new Size(Width, Height);

            if (corner_size_.Left == 0) { corner_size_.Left = 1; }
            if (corner_size_.Top == 0) { corner_size_.Top = 1; }
            if (corner_size_.Right == 0) { corner_size_.Right = 1; }
            if (corner_size_.Bottom == 0) { corner_size_.Bottom = 1; }

            gpath.AddArc(0,           0,            corner_size_.Left,  corner_size_.Top,    180, 90);
            gpath.AddArc(csize.Width - corner_size_.Right, 0,            corner_size_.Left, corner_size_.Top,    270, 90);
            gpath.AddArc(csize.Width - corner_size_.Right, csize.Height - corner_size_.Bottom, corner_size_.Left, corner_size_.Bottom, 0,   90);
            gpath.AddArc(0,           csize.Height - corner_size_.Bottom, corner_size_.Left,  corner_size_.Bottom, 90,  90);
            gpath.CloseFigure();

            return (gpath);
        }

        private void UpdateDrawPath()
        {
            gpath_surface_ = GetDrawPath_Surface();
        }

        private void UpdateDrawBrush()
        {
            if (back_color_ == BackColor)return;

            back_color_ = BackColor;

            back_color_brush_?.Dispose();
            back_color_brush_ = new SolidBrush(back_color_);

            back_color_s_brush_?.Dispose();
            back_color_s_brush_ = new SolidBrush(
                Color.FromArgb(
                    Math.Min(back_color_.A + 10, 255),
                    Math.Min(back_color_.R + 10, 255),
                    Math.Min(back_color_.G + 10, 255),
                    Math.Min(back_color_.B + 10, 255)));
        }

        protected override void OnResize(EventArgs e)
        {
            UpdateDrawPath();

            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            UpdateDrawBrush();

            var g = pevent.Graphics;
            var rect_image = Rectangle.Empty;
            var rect_text = Rectangle.Empty;

            /* 描画品質設定 */
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            /* 背景初期化 */
            g.FillRectangle(SystemBrushes.Control, DisplayRectangle);

            /* 背景色で塗りつぶし */
            g.FillPath(back_color_brush_, gpath_surface_);

            /* 枠を描画 */
            g.DrawPath(SystemPens.Highlight, gpath_surface_);

//            base.OnPaint(pevent);
        }
    }
}
