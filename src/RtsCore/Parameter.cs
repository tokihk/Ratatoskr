using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RtsCore
{
    public static class Parameter
    {
        public static readonly Color COLOR_OK = Color.LightGreen;
        public static readonly Color COLOR_NG = Color.LightPink;

        public static readonly Color COLOR_SUCCESS = COLOR_OK;
        public static readonly Color COLOR_WARNING = Color.FromArgb(242, 231, 0);
        public static readonly Color COLOR_ERROR   = COLOR_NG;
    }
}
