﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Scripts.PacketFilterExp.Parser;
using Ratatoskr.Scripts.PacketFilterExp;
using Ratatoskr.Packet;

namespace Ratatoskr.Forms.Controls
{
    public partial class PacketFilterBox : UserControl
    {
        private uint filter_log_limit_ = 0;

        private string           filter_exp_busy_ = "";
        private ExpressionFilter filter_obj_busy_ = null;

        private string           filter_exp_new_ = "";
        private ExpressionFilter filter_obj_new_ = null;


        public PacketFilterBox()
        {
            InitializeComponent();
        }

        public uint FilterLogLimit
        {
            get { return (filter_log_limit_); }
            set
            {
                filter_log_limit_ = value;
            }
        }

        public ComboBox.ObjectCollection Items
        {
            get { return (CBox_Filter.Items); }
        }
    }
}
