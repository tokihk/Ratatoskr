using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Native.Windows
{
    public static class WinUtility
    {
        public static Bitmap CaptureWindowScreen(IntPtr hwnd, Rectangle capture_rect)
        {
            WinAPI.RECT win_rect;

            WinAPI.GetWindowRect(hwnd, out win_rect);

            var capture_left   = Math.Max(capture_rect.Left, win_rect.Left);
            var capture_top    = Math.Max(capture_rect.Top, win_rect.Top);
            var capture_right  = Math.Max(capture_rect.Right, win_rect.Right);
            var capture_bottom = Math.Max(capture_rect.Bottom, win_rect.Bottom);

            var win_bmp = new Bitmap(capture_right - capture_left, capture_bottom - capture_top);

            var graphics = Graphics.FromImage(win_bmp);

            var graphics_dc = graphics.GetHdc();

            var win_dc = WinAPI.GetWindowDC(hwnd);

            WinAPI.BitBlt(graphics_dc, 0, 0, win_bmp.Width, win_bmp.Height, win_dc, 0, 0, WinAPI.SRCCOPY | WinAPI.CAPTUREBLT);

            graphics.ReleaseHdc(graphics_dc);
            graphics.Dispose();

            WinAPI.ReleaseDC(WinAPI.Null, win_dc);

            return (win_bmp);
        }

        public static Bitmap CaptureWindowScreen(IntPtr hwnd)
        {
            WinAPI.RECT win_rect;

            WinAPI.GetWindowRect(hwnd, out win_rect);

            return (CaptureWindowScreen(hwnd, new Rectangle(win_rect.Left, win_rect.Top, win_rect.Right - win_rect.Left, win_rect.Bottom - win_rect.Top)));
        }
    }
}
