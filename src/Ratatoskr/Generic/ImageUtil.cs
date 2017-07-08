using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.Generic
{
    internal static class ImageUtil
    {
        public static Image Resize(Icon icon, Size size)
        {
            if (icon == null)return (null);

            var bitmap = new Bitmap(icon.ToBitmap());

            return (bitmap.GetThumbnailImage(size.Width, size.Height, null, IntPtr.Zero));
        }
    }
}
