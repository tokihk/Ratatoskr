using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.Forms.Controls
{
    internal class ButtonEx : Button
    {
        private Padding         shadow_size_ = new Padding();
        private Padding         corner_size_ = new Padding();

        private GraphicsPath    gpath_surface_;
        private GraphicsPath    gpath_surface_h_;
        private GraphicsPath    gpath_shadow_;

//        private Brush shadow_b


        public ButtonEx()
        {
//            UpdateDrawPath();
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

        public Padding ShadowRect
        {
            get { return (shadow_size_); }
            set
            {
                shadow_size_ = value;
                UpdateDrawPath();
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
            var csize = new Size(Width - corner_size_.Horizontal, Height - corner_size_.Vertical);

            gpath.AddArc(shadow_size_.Left,                shadow_size_.Top,                   corner_size_.Left,  corner_size_.Top,    180, 90);
            gpath.AddArc(csize.Width - shadow_size_.Right, shadow_size_.Top,                   corner_size_.Right, corner_size_.Top,    270, 90);
            gpath.AddArc(csize.Width - shadow_size_.Right, csize.Height - shadow_size_.Bottom, corner_size_.Right, corner_size_.Bottom, 0,   90);
            gpath.AddArc(shadow_size_.Left,                csize.Height - shadow_size_.Bottom, corner_size_.Left,  corner_size_.Bottom, 90,  90);
            gpath.CloseFigure();

            return (gpath);
        }

        private GraphicsPath GetDrawPath_SurfaceHighlignt()
        {
            var gpath = new GraphicsPath();
            var wsize = Size;
            var csize = new Size(wsize.Width - corner_size_.Horizontal, wsize.Height - corner_size_.Vertical);
            var shadow_rect = new Padding(shadow_size_.Left - 1, shadow_size_.Top - 1, shadow_size_.Right - 1, shadow_size_.Bottom - 1);

            gpath.AddArc(shadow_size_.Left,                shadow_size_.Top,                   corner_size_.Left,  corner_size_.Top,    180, 90);
            gpath.AddArc(csize.Width - shadow_size_.Right, shadow_size_.Top,                   corner_size_.Right, corner_size_.Top,    270, 90);
//            gpath.AddLine(wsize.Width - shadow_rect.Left, )

            gpath.CloseFigure();

            return (gpath);
        }

        private GraphicsPath GetDrawPath_Shadow()
        {
            var gpath = new GraphicsPath();
            var csize = new Size(Width - corner_size_.Horizontal, Height - corner_size_.Vertical);

            gpath.AddArc(0,           0,            corner_size_.Left,  corner_size_.Top,    180, 90);
            gpath.AddArc(csize.Width, 0,            corner_size_.Right, corner_size_.Top,    270, 90);
            gpath.AddArc(csize.Width, csize.Height, corner_size_.Right, corner_size_.Bottom, 0,   90);
            gpath.AddArc(0,           csize.Height, corner_size_.Left,  corner_size_.Bottom, 90,  90);
            gpath.CloseFigure();

            return (gpath);
        }

        private void UpdateDrawPath()
        {
            gpath_surface_ = GetDrawPath_Surface();
            gpath_surface_h_ = GetDrawPath_SurfaceHighlignt();
            gpath_shadow_ = GetDrawPath_Shadow();
        }

        protected override void OnResize(EventArgs e)
        {
//            UpdateDrawPath();

            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;

            /* 描画品質設定 */
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;


            base.OnPaint(pevent);
        }
    }
}
