﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.FileFormats
{
    public partial class FileFormatOptionEditor : UserControl
    {
        public FileFormatOptionEditor()
        {
            InitializeComponent();
        }

        public virtual void Flush() { }
    }
}
