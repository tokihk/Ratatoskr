using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RtsCore.Framework.Native;

namespace RtsCore.Framework.Controls
{
    public class RoundTextBox : TextBox
    {
        private readonly Padding CORNER_SIZE = new Padding(7);

        private GraphicsPath gpath_surface_;
        private GraphicsPath gpath_surface_frame_;


        public RoundTextBox() : base()
        {
        }

        private GraphicsPath GetDrawPath_Surface()
        {
            var gpath = new GraphicsPath();
            var csize = new Size(Width, Height);

            gpath.AddArc(0,                                   0,                                 CORNER_SIZE.Left,  CORNER_SIZE.Top,    180, 90);
            gpath.AddArc(csize.Width - CORNER_SIZE.Right - 1, 0,                                 CORNER_SIZE.Right + 1, CORNER_SIZE.Top,    270, 90);
            gpath.AddArc(csize.Width - CORNER_SIZE.Right - 1, csize.Height - CORNER_SIZE.Bottom - 1, CORNER_SIZE.Right + 1, CORNER_SIZE.Bottom + 1, 0,   90);
            gpath.AddArc(0,                                   csize.Height - CORNER_SIZE.Bottom - 1, CORNER_SIZE.Left,  CORNER_SIZE.Bottom + 1, 90,  90);
            gpath.CloseFigure();

            return (gpath);
        }

        private GraphicsPath GetDrawPath_SurfaceFrame()
        {
            var gpath = new GraphicsPath();
            var csize = new Size(Width, Height);

            gpath.AddArc(0,                                   0,                                 CORNER_SIZE.Left, CORNER_SIZE.Top,    180, 90);
            gpath.AddArc(csize.Width - CORNER_SIZE.Right - 1, 0,                                 CORNER_SIZE.Right + 1, CORNER_SIZE.Top,    270, 90);
            gpath.AddArc(csize.Width - CORNER_SIZE.Right, csize.Height - CORNER_SIZE.Bottom - 1, CORNER_SIZE.Right, CORNER_SIZE.Bottom + 1, 0,   90);
            gpath.AddArc(0,                                   csize.Height - CORNER_SIZE.Bottom - 1, CORNER_SIZE.Left, CORNER_SIZE.Bottom + 1, 90,  90);
            gpath.CloseFigure();

            return (gpath);
        }

        private void UpdateDrawPath()
        {
            gpath_surface_ = GetDrawPath_Surface();
            gpath_surface_frame_ = GetDrawPath_SurfaceFrame();
        }

        protected override void OnResize(EventArgs e)
        {
            UpdateDrawPath();

            Region = new Region(gpath_surface_);

            base.OnResize(e);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == (int)WinAPI.WM_PAINT) {
                var graphics = Graphics.FromHwnd(m.HWnd);

                OnPaintFrame(graphics);

                graphics.Dispose();
            }
        }

        private void OnPaintFrame(Graphics g)
        {
            var pen = new Pen(SystemColors.Highlight, 2);

            /* 枠を描画 */
//            g.DrawPath(Pens.Black, gpath_surface_frame_);
            g.DrawPath(pen, gpath_surface_frame_);

            pen.Dispose();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

//            Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

//            Refresh();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (SelectionLength > 0) {
//                Refresh();
            }
        }
    }
}
