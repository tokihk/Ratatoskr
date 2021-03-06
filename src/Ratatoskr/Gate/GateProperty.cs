﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Device;

namespace Ratatoskr.Gate
{
    [Serializable]
    internal class GateProperty
    {
        public GateProperty(string alias, Color color)
        {
            Alias = alias;
            Color = color;
        }

        public string               Alias              { get; set; } = "";
        public Color                Color              { get; set; } = Color.White;
        public bool                 ConnectRequest     { get; set; } = true;
        public string               SendRedirectAlias  { get; set; } = "";
        public string               RecvRedirectAlias  { get; set; } = "";
        public string               ConnectCommand     { get; set; } = null;
    }
}
