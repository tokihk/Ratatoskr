using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Ratatoskr.Resource
{
    public static class AppColors
    {
        public static readonly Color Ok = Color.LightGreen;
        public static readonly Color Ng = Color.LightPink;

        public static readonly Color Success = Ok;
        public static readonly Color Warning = Color.FromArgb(242, 231, 0);
        public static readonly Color Error   = Ng;
    }
}
