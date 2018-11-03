using System;
using System.Drawing;

namespace RtsCore.Framework.Utility
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
