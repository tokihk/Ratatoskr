using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Forms;
using Ratatoskr.Forms.Controls;
using Ratatoskr.PacketViews.Packet.Configs;
using RtsCore.Packet;
using RtsCore.Framework.PacketFilter;
using RtsCore.Framework.PacketView;
using RtsCore.Utility;

namespace Ratatoskr.PacketViews.Packet
{
    internal sealed class PacketViewInstanceImpl : PacketViewInstance
    {
        private const ulong ITEM_NO_MIN = 1;
        private const ulong ITEM_NO_MAX = ulong.MaxValue;

        private class PacketListColumnData
        {
            public PacketFilterController filter_obj_ = null;
            public PacketFilterCallStack  filter_cs_  = null;


            public PacketListColumnData(ColumnHeaderConfig config)
            {
                Config = config;

                filter_obj_ = PacketFilterController.Build(config.PacketFilter);

                Reset();
            }

            public ColumnHeaderConfig   Config { get; }

            public void Reset()
            {
                if (filter_obj_ != null) {
                    filter_cs_ = new PacketFilterCallStack();
                    filter_obj_.CallStack = filter_cs_;
                }
            }

            public bool Input(PacketObject packet)
            {
                if (filter_obj_ == null)return (true);

                return (filter_obj_.Input(packet));
            }
        }

        private class PacketListViewItem
        {
            public PacketListViewItem(ulong no, PacketObject packet)
            {
                No = no;
                Packet = packet;
            }

            public ulong No { get; }
            public PacketObject Packet { get; }
        }

        private enum PacketSelectStatus
        {
            NotSelect,
            SingleSelect,
            MultiSelect,
        }

        private enum MenuActionId
        {
            Copy_Packet_AllInfo_Csv,
            Copy_Packet_Class,
            Copy_Packet_Alias,
            Copy_Packet_DateTime_UTC_ISO8601,
            Copy_Packet_DateTime_UTC_Display,
            Copy_Packet_DateTime_Local_ISO8601,
            Copy_Packet_DateTime_Local_Display,
            Copy_Packet_Information,
            Copy_Packet_Source,
            Copy_Packet_Destination,
            Copy_Packet_Message,
            Copy_Packet_Data_BitString,
            Copy_Packet_Data_HexString,
            Copy_Packet_Data_AsciiText,
            Copy_Packet_Data_Utf8Text,
            Copy_Packet_Data_Utf16BeText,
            Copy_Packet_Data_Utf16LeText,
            Copy_Packet_Data_ShiftJisText,
            Copy_Packet_Data_EucJpText,
            Copy_Packet_Data_Custom,
            Copy_Packet_Data_LF_BitString,
            Copy_Packet_Data_LF_HexString,
            Copy_Packet_Data_LF_AsciiText,
            Copy_Packet_Data_LF_Utf8Text,
            Copy_Packet_Data_LF_Utf16BeText,
            Copy_Packet_Data_LF_Utf16LeText,
            Copy_Packet_Data_LF_ShiftJisText,
            Copy_Packet_Data_LF_EucJpText,
            Copy_Packet_Data_LF_Custom,
            Export_Packet_Data,
        }

        private PacketViewPropertyImpl prop_;
        private Encoding encoder_;

        private readonly Regex format_regex_ = new Regex(@"\$\{(?<value>[^\}]*)\}", RegexOptions.Compiled);
        private PacketObject   format_packet_ = null;

        private PacketListColumnData[] column_data_;

        private ulong next_item_no_ = 0;

        private List<PacketListViewItem> list_items_temp_;

        private int preview_data_size_bin_ = 0;
        private int preview_data_size_bin_wd_ = 0;
        private int preview_data_size_text_ = 0;

        private ListViewItem ExtViewItem_SelectPacketCount = null;
        private ListViewItem ExtViewItem_SelectTotalSize = null;
        private ListViewItem ExtViewItem_FirstPacketInfo = null;
        private ListViewItem ExtViewItem_LastPacketInfo = null;
        private ListViewItem ExtViewItem_SelectDelta = null;
        private ListViewItem ExtViewItem_SelectRate = null;

        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Panel Panel_ToolBar;
        private System.Windows.Forms.GroupBox GBox_CharCode;
        private System.Windows.Forms.ComboBox CBox_CharCode;
        private System.Windows.Forms.GroupBox GBox_PreviewByteNum;
        private System.Windows.Forms.SplitContainer Split_Main;
        private ListViewEx LView_Main;

        private SplitContainer Split_Sub;

        private BinEditBox BBox_Main;

        private GroupBox GBox_CustomFormat;
        private NumericUpDown Num_PreviewDataSize;
        private ToolStrip toolStrip1;
        private ToolStripDropDownButton Menu_ExtView;
        private ToolStripMenuItem Menu_ExtView_SelectPacketCount;
        private ToolStripMenuItem Menu_ExtView_SelectTotalSize;
        private ToolStripMenuItem Menu_ExtView_FirstPacketInfo;
        private ToolStripMenuItem Menu_ExtView_LastPacketInfo;
        private ToolStripMenuItem Menu_ExtView_SelectDelta;
        private ListView LView_ExtInfo;
        private ColumnHeader LView_ExtInfoColumn_Name;
        private ColumnHeader LView_ExtInfoColumn_Value;
        private Label label1;
        private ToolStripMenuItem Menu_ExtView_SelectRate;

        private ContextMenuStrip CMenu_Packet;
        private ToolStripMenuItem CMenu_Packet_Copy;
        private ToolStripMenuItem CMenu_Packet_Copy_AllInfo_Csv;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem CMenu_Packet_Copy_Alias;
        private ToolStripMenuItem CMenu_Packet_Copy_Information;
        private ToolStripMenuItem CMenu_Packet_Copy_Source;
        private ToolStripMenuItem CMenu_Packet_Copy_Destination;
        private ToolStripMenuItem CMenu_Packet_Copy_Message;
        private ToolStripMenuItem CMenu_Packet_Copy_DateTime;
        private ToolStripMenuItem CMenu_Packet_Copy_DateTime_UTC_ISO8601;
        private ToolStripMenuItem CMenu_Packet_Copy_DateTime_Local_ISO8601;
        private ToolStripMenuItem CMenu_Packet_Copy_DateTime_Local_Display;
        private ToolStripMenuItem CMenu_Packet_Copy_DateTime_UTC_Display;
        private ToolStripMenuItem CMenu_Packet_Copy_Data;
        private ToolStripMenuItem CMenu_Packet_Copy_Data_BitString;
        private ToolStripMenuItem CMenu_Packet_Copy_Data_HexString;
        private ToolStripMenuItem CMenu_Packet_Copy_Data_Utf8Text;
        private ToolStripMenuItem CMenu_Packet_Copy_Data_Utf16BeText;
        private ToolStripMenuItem CMenu_Packet_Copy_Data_Utf16LeText;
        private ToolStripMenuItem CMenu_Packet_Copy_Data_AsciiText;
        private ToolStripMenuItem CMenu_Packet_Copy_Data_ShiftJisText;
        private ToolStripMenuItem CMenu_Packet_Copy_Data_EucJpText;
        private ToolStripMenuItem CMenu_Packet_Copy_Data_Custom;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem CMenu_Packet_Copy_DataLF;
        private ToolStripMenuItem CMenu_Packet_Copy_DataLF_BitString;
        private ToolStripMenuItem CMenu_Packet_Copy_DataLF_HexString;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem CMenu_Packet_Copy_DataLF_AsciiText;
        private ToolStripMenuItem CMenu_Packet_Copy_DataLF_Utf8Text;
        private ToolStripMenuItem CMenu_Packet_Copy_DataLF_Utf16BeText;
        private ToolStripMenuItem CMenu_Packet_Copy_DataLF_Utf16LeText;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem CMenu_Packet_Copy_DataLF_ShiftJisText;
        private ToolStripMenuItem CMenu_Packet_Copy_DataLF_EucJpText;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripMenuItem CMenu_Packet_Copy_DataLF_Custom;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem CMenu_Packet_Copy_Class;
        private ToolStripMenuItem CMenu_Packet_Export;
        private ToolStripMenuItem CMenu_Packet_Export_Data;
        private GroupBox groupBox1;
        private RadioButton RBtn_Layout_2;
        private RadioButton RBtn_Layout_1;
        private RadioButton RBtn_Layout_0;
        private TextBox TBox_CustomFormat;


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PacketViewInstanceImpl));
            this.Panel_ToolBar = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RBtn_Layout_0 = new System.Windows.Forms.RadioButton();
            this.RBtn_Layout_2 = new System.Windows.Forms.RadioButton();
            this.RBtn_Layout_1 = new System.Windows.Forms.RadioButton();
            this.GBox_CustomFormat = new System.Windows.Forms.GroupBox();
            this.TBox_CustomFormat = new System.Windows.Forms.TextBox();
            this.GBox_CharCode = new System.Windows.Forms.GroupBox();
            this.CBox_CharCode = new System.Windows.Forms.ComboBox();
            this.GBox_PreviewByteNum = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Num_PreviewDataSize = new System.Windows.Forms.NumericUpDown();
            this.Split_Main = new System.Windows.Forms.SplitContainer();
            this.Split_Sub = new System.Windows.Forms.SplitContainer();
            this.LView_ExtInfo = new System.Windows.Forms.ListView();
            this.LView_ExtInfoColumn_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LView_ExtInfoColumn_Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Menu_ExtView = new System.Windows.Forms.ToolStripDropDownButton();
            this.Menu_ExtView_SelectPacketCount = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_ExtView_SelectTotalSize = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_ExtView_FirstPacketInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_ExtView_LastPacketInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_ExtView_SelectDelta = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_ExtView_SelectRate = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CMenu_Packet_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_AllInfo_Csv = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CMenu_Packet_Copy_Class = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_Alias = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_DateTime = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_DateTime_UTC_ISO8601 = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_DateTime_UTC_Display = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.CMenu_Packet_Copy_DateTime_Local_ISO8601 = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_DateTime_Local_Display = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_Information = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_Source = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_Destination = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_Message = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_DataLF = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_DataLF_BitString = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_DataLF_HexString = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.CMenu_Packet_Copy_DataLF_AsciiText = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_DataLF_Utf8Text = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_DataLF_Utf16BeText = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_DataLF_Utf16LeText = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.CMenu_Packet_Copy_DataLF_ShiftJisText = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_DataLF_EucJpText = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.CMenu_Packet_Copy_DataLF_Custom = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_Data = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_Data_BitString = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_Data_HexString = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CMenu_Packet_Copy_Data_AsciiText = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_Data_Utf8Text = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_Data_Utf16BeText = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_Data_Utf16LeText = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.CMenu_Packet_Copy_Data_ShiftJisText = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Copy_Data_EucJpText = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.CMenu_Packet_Copy_Data_Custom = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_Export_Data = new System.Windows.Forms.ToolStripMenuItem();
            this.LView_Main = new Ratatoskr.Forms.Controls.ListViewEx();
            this.BBox_Main = new Ratatoskr.Forms.Controls.BinEditBox();
            this.Panel_ToolBar.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.GBox_CustomFormat.SuspendLayout();
            this.GBox_CharCode.SuspendLayout();
            this.GBox_PreviewByteNum.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_PreviewDataSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Split_Main)).BeginInit();
            this.Split_Main.Panel1.SuspendLayout();
            this.Split_Main.Panel2.SuspendLayout();
            this.Split_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Split_Sub)).BeginInit();
            this.Split_Sub.Panel1.SuspendLayout();
            this.Split_Sub.Panel2.SuspendLayout();
            this.Split_Sub.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.CMenu_Packet.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_ToolBar
            // 
            this.Panel_ToolBar.Controls.Add(this.groupBox1);
            this.Panel_ToolBar.Controls.Add(this.GBox_CustomFormat);
            this.Panel_ToolBar.Controls.Add(this.GBox_CharCode);
            this.Panel_ToolBar.Controls.Add(this.GBox_PreviewByteNum);
            this.Panel_ToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_ToolBar.Location = new System.Drawing.Point(0, 0);
            this.Panel_ToolBar.Name = "Panel_ToolBar";
            this.Panel_ToolBar.Size = new System.Drawing.Size(957, 49);
            this.Panel_ToolBar.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RBtn_Layout_0);
            this.groupBox1.Controls.Add(this.RBtn_Layout_2);
            this.groupBox1.Controls.Add(this.RBtn_Layout_1);
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(100, 40);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Layout";
            // 
            // RBtn_Layout_0
            // 
            this.RBtn_Layout_0.Appearance = System.Windows.Forms.Appearance.Button;
            this.RBtn_Layout_0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.RBtn_Layout_0.Cursor = System.Windows.Forms.Cursors.Default;
            this.RBtn_Layout_0.FlatAppearance.BorderSize = 0;
            this.RBtn_Layout_0.Image = ((System.Drawing.Image)(resources.GetObject("RBtn_Layout_0.Image")));
            this.RBtn_Layout_0.Location = new System.Drawing.Point(6, 13);
            this.RBtn_Layout_0.Name = "RBtn_Layout_0";
            this.RBtn_Layout_0.Size = new System.Drawing.Size(24, 24);
            this.RBtn_Layout_0.TabIndex = 3;
            this.RBtn_Layout_0.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.RBtn_Layout_0.UseVisualStyleBackColor = true;
            this.RBtn_Layout_0.CheckedChanged += new System.EventHandler(this.RBtn_Layout_0_CheckedChanged);
            // 
            // RBtn_Layout_2
            // 
            this.RBtn_Layout_2.Appearance = System.Windows.Forms.Appearance.Button;
            this.RBtn_Layout_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.RBtn_Layout_2.Cursor = System.Windows.Forms.Cursors.Default;
            this.RBtn_Layout_2.FlatAppearance.BorderSize = 0;
            this.RBtn_Layout_2.Image = ((System.Drawing.Image)(resources.GetObject("RBtn_Layout_2.Image")));
            this.RBtn_Layout_2.Location = new System.Drawing.Point(66, 13);
            this.RBtn_Layout_2.Name = "RBtn_Layout_2";
            this.RBtn_Layout_2.Size = new System.Drawing.Size(24, 24);
            this.RBtn_Layout_2.TabIndex = 2;
            this.RBtn_Layout_2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.RBtn_Layout_2.UseVisualStyleBackColor = true;
            this.RBtn_Layout_2.CheckedChanged += new System.EventHandler(this.RBtn_Layout_2_CheckedChanged);
            // 
            // RBtn_Layout_1
            // 
            this.RBtn_Layout_1.Appearance = System.Windows.Forms.Appearance.Button;
            this.RBtn_Layout_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.RBtn_Layout_1.Checked = true;
            this.RBtn_Layout_1.Cursor = System.Windows.Forms.Cursors.Default;
            this.RBtn_Layout_1.FlatAppearance.BorderSize = 0;
            this.RBtn_Layout_1.Image = ((System.Drawing.Image)(resources.GetObject("RBtn_Layout_1.Image")));
            this.RBtn_Layout_1.Location = new System.Drawing.Point(36, 13);
            this.RBtn_Layout_1.Name = "RBtn_Layout_1";
            this.RBtn_Layout_1.Size = new System.Drawing.Size(24, 24);
            this.RBtn_Layout_1.TabIndex = 1;
            this.RBtn_Layout_1.TabStop = true;
            this.RBtn_Layout_1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.RBtn_Layout_1.UseVisualStyleBackColor = true;
            this.RBtn_Layout_1.CheckedChanged += new System.EventHandler(this.RBtn_Layout_1_CheckedChanged);
            // 
            // GBox_CustomFormat
            // 
            this.GBox_CustomFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_CustomFormat.Controls.Add(this.TBox_CustomFormat);
            this.GBox_CustomFormat.Location = new System.Drawing.Point(382, 5);
            this.GBox_CustomFormat.Name = "GBox_CustomFormat";
            this.GBox_CustomFormat.Size = new System.Drawing.Size(572, 39);
            this.GBox_CustomFormat.TabIndex = 2;
            this.GBox_CustomFormat.TabStop = false;
            this.GBox_CustomFormat.Text = "Custom preview format";
            // 
            // TBox_CustomFormat
            // 
            this.TBox_CustomFormat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TBox_CustomFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_CustomFormat.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TBox_CustomFormat.Location = new System.Drawing.Point(3, 15);
            this.TBox_CustomFormat.Name = "TBox_CustomFormat";
            this.TBox_CustomFormat.Size = new System.Drawing.Size(566, 19);
            this.TBox_CustomFormat.TabIndex = 0;
            this.TBox_CustomFormat.TextChanged += new System.EventHandler(this.TBox_CustomFormat_TextChanged);
            this.TBox_CustomFormat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBox_CustomFormat_KeyDown);
            // 
            // GBox_CharCode
            // 
            this.GBox_CharCode.Controls.Add(this.CBox_CharCode);
            this.GBox_CharCode.Location = new System.Drawing.Point(256, 4);
            this.GBox_CharCode.Name = "GBox_CharCode";
            this.GBox_CharCode.Size = new System.Drawing.Size(120, 40);
            this.GBox_CharCode.TabIndex = 1;
            this.GBox_CharCode.TabStop = false;
            this.GBox_CharCode.Text = "Character code";
            // 
            // CBox_CharCode
            // 
            this.CBox_CharCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_CharCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_CharCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CBox_CharCode.FormattingEnabled = true;
            this.CBox_CharCode.Location = new System.Drawing.Point(3, 15);
            this.CBox_CharCode.Name = "CBox_CharCode";
            this.CBox_CharCode.Size = new System.Drawing.Size(114, 20);
            this.CBox_CharCode.TabIndex = 0;
            this.CBox_CharCode.SelectedIndexChanged += new System.EventHandler(this.CBox_CharCode_SelectedIndexChanged);
            // 
            // GBox_PreviewByteNum
            // 
            this.GBox_PreviewByteNum.Controls.Add(this.label1);
            this.GBox_PreviewByteNum.Controls.Add(this.Num_PreviewDataSize);
            this.GBox_PreviewByteNum.Location = new System.Drawing.Point(110, 4);
            this.GBox_PreviewByteNum.Name = "GBox_PreviewByteNum";
            this.GBox_PreviewByteNum.Size = new System.Drawing.Size(140, 40);
            this.GBox_PreviewByteNum.TabIndex = 0;
            this.GBox_PreviewByteNum.TabStop = false;
            this.GBox_PreviewByteNum.Text = "Preview size";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "bytes";
            // 
            // Num_PreviewDataSize
            // 
            this.Num_PreviewDataSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Num_PreviewDataSize.Location = new System.Drawing.Point(3, 15);
            this.Num_PreviewDataSize.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.Num_PreviewDataSize.Name = "Num_PreviewDataSize";
            this.Num_PreviewDataSize.Size = new System.Drawing.Size(90, 19);
            this.Num_PreviewDataSize.TabIndex = 0;
            this.Num_PreviewDataSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_PreviewDataSize.ThousandsSeparator = true;
            this.Num_PreviewDataSize.ValueChanged += new System.EventHandler(this.Num_PreviewDataSize_ValueChanged);
            this.Num_PreviewDataSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Num_PreviewDataSize_KeyDown);
            // 
            // Split_Main
            // 
            this.Split_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Split_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.Split_Main.Location = new System.Drawing.Point(0, 49);
            this.Split_Main.Name = "Split_Main";
            this.Split_Main.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Split_Main.Panel1
            // 
            this.Split_Main.Panel1.Controls.Add(this.LView_Main);
            // 
            // Split_Main.Panel2
            // 
            this.Split_Main.Panel2.Controls.Add(this.Split_Sub);
            this.Split_Main.Panel2MinSize = 150;
            this.Split_Main.Size = new System.Drawing.Size(957, 462);
            this.Split_Main.SplitterDistance = 258;
            this.Split_Main.TabIndex = 1;
            // 
            // Split_Sub
            // 
            this.Split_Sub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Split_Sub.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.Split_Sub.Location = new System.Drawing.Point(0, 0);
            this.Split_Sub.Name = "Split_Sub";
            // 
            // Split_Sub.Panel1
            // 
            this.Split_Sub.Panel1.Controls.Add(this.BBox_Main);
            // 
            // Split_Sub.Panel2
            // 
            this.Split_Sub.Panel2.Controls.Add(this.LView_ExtInfo);
            this.Split_Sub.Panel2.Controls.Add(this.toolStrip1);
            this.Split_Sub.Size = new System.Drawing.Size(957, 200);
            this.Split_Sub.SplitterDistance = 640;
            this.Split_Sub.TabIndex = 0;
            // 
            // LView_ExtInfo
            // 
            this.LView_ExtInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LView_ExtInfoColumn_Name,
            this.LView_ExtInfoColumn_Value});
            this.LView_ExtInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LView_ExtInfo.FullRowSelect = true;
            this.LView_ExtInfo.GridLines = true;
            this.LView_ExtInfo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.LView_ExtInfo.Location = new System.Drawing.Point(0, 25);
            this.LView_ExtInfo.Name = "LView_ExtInfo";
            this.LView_ExtInfo.Size = new System.Drawing.Size(313, 175);
            this.LView_ExtInfo.TabIndex = 2;
            this.LView_ExtInfo.UseCompatibleStateImageBehavior = false;
            this.LView_ExtInfo.View = System.Windows.Forms.View.Details;
            // 
            // LView_ExtInfoColumn_Name
            // 
            this.LView_ExtInfoColumn_Name.Text = "Information name";
            this.LView_ExtInfoColumn_Name.Width = 300;
            // 
            // LView_ExtInfoColumn_Value
            // 
            this.LView_ExtInfoColumn_Value.Text = "Value";
            this.LView_ExtInfoColumn_Value.Width = 120;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_ExtView});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(313, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Menu_ExtView
            // 
            this.Menu_ExtView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Menu_ExtView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_ExtView_SelectPacketCount,
            this.Menu_ExtView_SelectTotalSize,
            this.Menu_ExtView_FirstPacketInfo,
            this.Menu_ExtView_LastPacketInfo,
            this.Menu_ExtView_SelectDelta,
            this.Menu_ExtView_SelectRate});
            this.Menu_ExtView.Image = ((System.Drawing.Image)(resources.GetObject("Menu_ExtView.Image")));
            this.Menu_ExtView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Menu_ExtView.Name = "Menu_ExtView";
            this.Menu_ExtView.Size = new System.Drawing.Size(132, 22);
            this.Menu_ExtView.Text = "Display item select";
            // 
            // Menu_ExtView_SelectPacketCount
            // 
            this.Menu_ExtView_SelectPacketCount.Name = "Menu_ExtView_SelectPacketCount";
            this.Menu_ExtView_SelectPacketCount.Size = new System.Drawing.Size(373, 22);
            this.Menu_ExtView_SelectPacketCount.Text = "Select packet count";
            this.Menu_ExtView_SelectPacketCount.Click += new System.EventHandler(this.Menu_ExtView_Click);
            // 
            // Menu_ExtView_SelectTotalSize
            // 
            this.Menu_ExtView_SelectTotalSize.Name = "Menu_ExtView_SelectTotalSize";
            this.Menu_ExtView_SelectTotalSize.Size = new System.Drawing.Size(373, 22);
            this.Menu_ExtView_SelectTotalSize.Text = "Select packet total size";
            this.Menu_ExtView_SelectTotalSize.Click += new System.EventHandler(this.Menu_ExtView_Click);
            // 
            // Menu_ExtView_FirstPacketInfo
            // 
            this.Menu_ExtView_FirstPacketInfo.Name = "Menu_ExtView_FirstPacketInfo";
            this.Menu_ExtView_FirstPacketInfo.Size = new System.Drawing.Size(373, 22);
            this.Menu_ExtView_FirstPacketInfo.Text = "Information on selected packet (first)";
            this.Menu_ExtView_FirstPacketInfo.Click += new System.EventHandler(this.Menu_ExtView_Click);
            // 
            // Menu_ExtView_LastPacketInfo
            // 
            this.Menu_ExtView_LastPacketInfo.Name = "Menu_ExtView_LastPacketInfo";
            this.Menu_ExtView_LastPacketInfo.Size = new System.Drawing.Size(373, 22);
            this.Menu_ExtView_LastPacketInfo.Text = "Information on selected packet (last)";
            this.Menu_ExtView_LastPacketInfo.Click += new System.EventHandler(this.Menu_ExtView_Click);
            // 
            // Menu_ExtView_SelectDelta
            // 
            this.Menu_ExtView_SelectDelta.Name = "Menu_ExtView_SelectDelta";
            this.Menu_ExtView_SelectDelta.Size = new System.Drawing.Size(373, 22);
            this.Menu_ExtView_SelectDelta.Text = "Time difference of selection packet (last-first)";
            this.Menu_ExtView_SelectDelta.Click += new System.EventHandler(this.Menu_ExtView_Click);
            // 
            // Menu_ExtView_SelectRate
            // 
            this.Menu_ExtView_SelectRate.Name = "Menu_ExtView_SelectRate";
            this.Menu_ExtView_SelectRate.Size = new System.Drawing.Size(373, 22);
            this.Menu_ExtView_SelectRate.Text = "Communication rate of Selection packet (last-first)";
            this.Menu_ExtView_SelectRate.Click += new System.EventHandler(this.Menu_ExtView_Click);
            // 
            // CMenu_Packet
            // 
            this.CMenu_Packet.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMenu_Packet_Copy,
            this.CMenu_Packet_Export});
            this.CMenu_Packet.Name = "CMenu_Data";
            this.CMenu_Packet.Size = new System.Drawing.Size(155, 48);
            // 
            // CMenu_Packet_Copy
            // 
            this.CMenu_Packet_Copy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMenu_Packet_Copy_AllInfo_Csv,
            this.toolStripSeparator1,
            this.CMenu_Packet_Copy_Class,
            this.CMenu_Packet_Copy_Alias,
            this.CMenu_Packet_Copy_DateTime,
            this.CMenu_Packet_Copy_Information,
            this.CMenu_Packet_Copy_Source,
            this.CMenu_Packet_Copy_Destination,
            this.CMenu_Packet_Copy_Message,
            this.CMenu_Packet_Copy_DataLF,
            this.CMenu_Packet_Copy_Data});
            this.CMenu_Packet_Copy.Name = "CMenu_Packet_Copy";
            this.CMenu_Packet_Copy.Size = new System.Drawing.Size(154, 22);
            this.CMenu_Packet_Copy.Text = "Copy";
            // 
            // CMenu_Packet_Copy_AllInfo_Csv
            // 
            this.CMenu_Packet_Copy_AllInfo_Csv.Name = "CMenu_Packet_Copy_AllInfo_Csv";
            this.CMenu_Packet_Copy_AllInfo_Csv.Size = new System.Drawing.Size(243, 22);
            this.CMenu_Packet_Copy_AllInfo_Csv.Text = "All information (CSV format)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(240, 6);
            // 
            // CMenu_Packet_Copy_Class
            // 
            this.CMenu_Packet_Copy_Class.Name = "CMenu_Packet_Copy_Class";
            this.CMenu_Packet_Copy_Class.Size = new System.Drawing.Size(243, 22);
            this.CMenu_Packet_Copy_Class.Text = "Class";
            // 
            // CMenu_Packet_Copy_Alias
            // 
            this.CMenu_Packet_Copy_Alias.Name = "CMenu_Packet_Copy_Alias";
            this.CMenu_Packet_Copy_Alias.Size = new System.Drawing.Size(243, 22);
            this.CMenu_Packet_Copy_Alias.Text = "Alias";
            // 
            // CMenu_Packet_Copy_DateTime
            // 
            this.CMenu_Packet_Copy_DateTime.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMenu_Packet_Copy_DateTime_UTC_ISO8601,
            this.CMenu_Packet_Copy_DateTime_UTC_Display,
            this.toolStripSeparator5,
            this.CMenu_Packet_Copy_DateTime_Local_ISO8601,
            this.CMenu_Packet_Copy_DateTime_Local_Display});
            this.CMenu_Packet_Copy_DateTime.Name = "CMenu_Packet_Copy_DateTime";
            this.CMenu_Packet_Copy_DateTime.Size = new System.Drawing.Size(243, 22);
            this.CMenu_Packet_Copy_DateTime.Text = "DateTime";
            // 
            // CMenu_Packet_Copy_DateTime_UTC_ISO8601
            // 
            this.CMenu_Packet_Copy_DateTime_UTC_ISO8601.Name = "CMenu_Packet_Copy_DateTime_UTC_ISO8601";
            this.CMenu_Packet_Copy_DateTime_UTC_ISO8601.Size = new System.Drawing.Size(209, 22);
            this.CMenu_Packet_Copy_DateTime_UTC_ISO8601.Text = "UTC: ISO8601 format";
            // 
            // CMenu_Packet_Copy_DateTime_UTC_Display
            // 
            this.CMenu_Packet_Copy_DateTime_UTC_Display.Name = "CMenu_Packet_Copy_DateTime_UTC_Display";
            this.CMenu_Packet_Copy_DateTime_UTC_Display.Size = new System.Drawing.Size(209, 22);
            this.CMenu_Packet_Copy_DateTime_UTC_Display.Text = "UTC: Display";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(206, 6);
            // 
            // CMenu_Packet_Copy_DateTime_Local_ISO8601
            // 
            this.CMenu_Packet_Copy_DateTime_Local_ISO8601.Name = "CMenu_Packet_Copy_DateTime_Local_ISO8601";
            this.CMenu_Packet_Copy_DateTime_Local_ISO8601.Size = new System.Drawing.Size(209, 22);
            this.CMenu_Packet_Copy_DateTime_Local_ISO8601.Text = "Local: ISO8601 format";
            // 
            // CMenu_Packet_Copy_DateTime_Local_Display
            // 
            this.CMenu_Packet_Copy_DateTime_Local_Display.Name = "CMenu_Packet_Copy_DateTime_Local_Display";
            this.CMenu_Packet_Copy_DateTime_Local_Display.Size = new System.Drawing.Size(209, 22);
            this.CMenu_Packet_Copy_DateTime_Local_Display.Text = "Local: Display";
            // 
            // CMenu_Packet_Copy_Information
            // 
            this.CMenu_Packet_Copy_Information.Name = "CMenu_Packet_Copy_Information";
            this.CMenu_Packet_Copy_Information.Size = new System.Drawing.Size(243, 22);
            this.CMenu_Packet_Copy_Information.Text = "Information";
            // 
            // CMenu_Packet_Copy_Source
            // 
            this.CMenu_Packet_Copy_Source.Name = "CMenu_Packet_Copy_Source";
            this.CMenu_Packet_Copy_Source.Size = new System.Drawing.Size(243, 22);
            this.CMenu_Packet_Copy_Source.Text = "Source";
            // 
            // CMenu_Packet_Copy_Destination
            // 
            this.CMenu_Packet_Copy_Destination.Name = "CMenu_Packet_Copy_Destination";
            this.CMenu_Packet_Copy_Destination.Size = new System.Drawing.Size(243, 22);
            this.CMenu_Packet_Copy_Destination.Text = "Destination";
            // 
            // CMenu_Packet_Copy_Message
            // 
            this.CMenu_Packet_Copy_Message.Name = "CMenu_Packet_Copy_Message";
            this.CMenu_Packet_Copy_Message.Size = new System.Drawing.Size(243, 22);
            this.CMenu_Packet_Copy_Message.Text = "Message";
            // 
            // CMenu_Packet_Copy_DataLF
            // 
            this.CMenu_Packet_Copy_DataLF.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMenu_Packet_Copy_DataLF_BitString,
            this.CMenu_Packet_Copy_DataLF_HexString,
            this.toolStripSeparator6,
            this.CMenu_Packet_Copy_DataLF_AsciiText,
            this.CMenu_Packet_Copy_DataLF_Utf8Text,
            this.CMenu_Packet_Copy_DataLF_Utf16BeText,
            this.CMenu_Packet_Copy_DataLF_Utf16LeText,
            this.toolStripSeparator7,
            this.CMenu_Packet_Copy_DataLF_ShiftJisText,
            this.CMenu_Packet_Copy_DataLF_EucJpText,
            this.toolStripSeparator8,
            this.CMenu_Packet_Copy_DataLF_Custom});
            this.CMenu_Packet_Copy_DataLF.Name = "CMenu_Packet_Copy_DataLF";
            this.CMenu_Packet_Copy_DataLF.Size = new System.Drawing.Size(243, 22);
            this.CMenu_Packet_Copy_DataLF.Text = "Data (with Line Feed)";
            // 
            // CMenu_Packet_Copy_DataLF_BitString
            // 
            this.CMenu_Packet_Copy_DataLF_BitString.Name = "CMenu_Packet_Copy_DataLF_BitString";
            this.CMenu_Packet_Copy_DataLF_BitString.Size = new System.Drawing.Size(215, 22);
            this.CMenu_Packet_Copy_DataLF_BitString.Text = "BIN string";
            // 
            // CMenu_Packet_Copy_DataLF_HexString
            // 
            this.CMenu_Packet_Copy_DataLF_HexString.Name = "CMenu_Packet_Copy_DataLF_HexString";
            this.CMenu_Packet_Copy_DataLF_HexString.Size = new System.Drawing.Size(215, 22);
            this.CMenu_Packet_Copy_DataLF_HexString.Text = "HEX string";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(212, 6);
            // 
            // CMenu_Packet_Copy_DataLF_AsciiText
            // 
            this.CMenu_Packet_Copy_DataLF_AsciiText.Name = "CMenu_Packet_Copy_DataLF_AsciiText";
            this.CMenu_Packet_Copy_DataLF_AsciiText.Size = new System.Drawing.Size(215, 22);
            this.CMenu_Packet_Copy_DataLF_AsciiText.Text = "ASCII text";
            // 
            // CMenu_Packet_Copy_DataLF_Utf8Text
            // 
            this.CMenu_Packet_Copy_DataLF_Utf8Text.Name = "CMenu_Packet_Copy_DataLF_Utf8Text";
            this.CMenu_Packet_Copy_DataLF_Utf8Text.Size = new System.Drawing.Size(215, 22);
            this.CMenu_Packet_Copy_DataLF_Utf8Text.Text = "UTF-8 text";
            // 
            // CMenu_Packet_Copy_DataLF_Utf16BeText
            // 
            this.CMenu_Packet_Copy_DataLF_Utf16BeText.Name = "CMenu_Packet_Copy_DataLF_Utf16BeText";
            this.CMenu_Packet_Copy_DataLF_Utf16BeText.Size = new System.Drawing.Size(215, 22);
            this.CMenu_Packet_Copy_DataLF_Utf16BeText.Text = "UTF-16BE text";
            // 
            // CMenu_Packet_Copy_DataLF_Utf16LeText
            // 
            this.CMenu_Packet_Copy_DataLF_Utf16LeText.Name = "CMenu_Packet_Copy_DataLF_Utf16LeText";
            this.CMenu_Packet_Copy_DataLF_Utf16LeText.Size = new System.Drawing.Size(215, 22);
            this.CMenu_Packet_Copy_DataLF_Utf16LeText.Text = "UTF-16LE text";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(212, 6);
            // 
            // CMenu_Packet_Copy_DataLF_ShiftJisText
            // 
            this.CMenu_Packet_Copy_DataLF_ShiftJisText.Name = "CMenu_Packet_Copy_DataLF_ShiftJisText";
            this.CMenu_Packet_Copy_DataLF_ShiftJisText.Size = new System.Drawing.Size(215, 22);
            this.CMenu_Packet_Copy_DataLF_ShiftJisText.Text = "Shift_JIS text";
            // 
            // CMenu_Packet_Copy_DataLF_EucJpText
            // 
            this.CMenu_Packet_Copy_DataLF_EucJpText.Name = "CMenu_Packet_Copy_DataLF_EucJpText";
            this.CMenu_Packet_Copy_DataLF_EucJpText.Size = new System.Drawing.Size(215, 22);
            this.CMenu_Packet_Copy_DataLF_EucJpText.Text = "EUC-JP text";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(212, 6);
            // 
            // CMenu_Packet_Copy_DataLF_Custom
            // 
            this.CMenu_Packet_Copy_DataLF_Custom.Name = "CMenu_Packet_Copy_DataLF_Custom";
            this.CMenu_Packet_Copy_DataLF_Custom.Size = new System.Drawing.Size(215, 22);
            this.CMenu_Packet_Copy_DataLF_Custom.Text = "Custom preview format";
            // 
            // CMenu_Packet_Copy_Data
            // 
            this.CMenu_Packet_Copy_Data.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMenu_Packet_Copy_Data_BitString,
            this.CMenu_Packet_Copy_Data_HexString,
            this.toolStripSeparator2,
            this.CMenu_Packet_Copy_Data_AsciiText,
            this.CMenu_Packet_Copy_Data_Utf8Text,
            this.CMenu_Packet_Copy_Data_Utf16BeText,
            this.CMenu_Packet_Copy_Data_Utf16LeText,
            this.toolStripSeparator3,
            this.CMenu_Packet_Copy_Data_ShiftJisText,
            this.CMenu_Packet_Copy_Data_EucJpText,
            this.toolStripSeparator4,
            this.CMenu_Packet_Copy_Data_Custom});
            this.CMenu_Packet_Copy_Data.Name = "CMenu_Packet_Copy_Data";
            this.CMenu_Packet_Copy_Data.Size = new System.Drawing.Size(243, 22);
            this.CMenu_Packet_Copy_Data.Text = "Data (without Line Feed)";
            // 
            // CMenu_Packet_Copy_Data_BitString
            // 
            this.CMenu_Packet_Copy_Data_BitString.Name = "CMenu_Packet_Copy_Data_BitString";
            this.CMenu_Packet_Copy_Data_BitString.Size = new System.Drawing.Size(215, 22);
            this.CMenu_Packet_Copy_Data_BitString.Text = "BIN string";
            // 
            // CMenu_Packet_Copy_Data_HexString
            // 
            this.CMenu_Packet_Copy_Data_HexString.Name = "CMenu_Packet_Copy_Data_HexString";
            this.CMenu_Packet_Copy_Data_HexString.Size = new System.Drawing.Size(215, 22);
            this.CMenu_Packet_Copy_Data_HexString.Text = "HEX string";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(212, 6);
            // 
            // CMenu_Packet_Copy_Data_AsciiText
            // 
            this.CMenu_Packet_Copy_Data_AsciiText.Name = "CMenu_Packet_Copy_Data_AsciiText";
            this.CMenu_Packet_Copy_Data_AsciiText.Size = new System.Drawing.Size(215, 22);
            this.CMenu_Packet_Copy_Data_AsciiText.Text = "ASCII text";
            // 
            // CMenu_Packet_Copy_Data_Utf8Text
            // 
            this.CMenu_Packet_Copy_Data_Utf8Text.Name = "CMenu_Packet_Copy_Data_Utf8Text";
            this.CMenu_Packet_Copy_Data_Utf8Text.Size = new System.Drawing.Size(215, 22);
            this.CMenu_Packet_Copy_Data_Utf8Text.Text = "UTF-8 text";
            // 
            // CMenu_Packet_Copy_Data_Utf16BeText
            // 
            this.CMenu_Packet_Copy_Data_Utf16BeText.Name = "CMenu_Packet_Copy_Data_Utf16BeText";
            this.CMenu_Packet_Copy_Data_Utf16BeText.Size = new System.Drawing.Size(215, 22);
            this.CMenu_Packet_Copy_Data_Utf16BeText.Text = "UTF-16BE text";
            // 
            // CMenu_Packet_Copy_Data_Utf16LeText
            // 
            this.CMenu_Packet_Copy_Data_Utf16LeText.Name = "CMenu_Packet_Copy_Data_Utf16LeText";
            this.CMenu_Packet_Copy_Data_Utf16LeText.Size = new System.Drawing.Size(215, 22);
            this.CMenu_Packet_Copy_Data_Utf16LeText.Text = "UTF-16LE text";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(212, 6);
            // 
            // CMenu_Packet_Copy_Data_ShiftJisText
            // 
            this.CMenu_Packet_Copy_Data_ShiftJisText.Name = "CMenu_Packet_Copy_Data_ShiftJisText";
            this.CMenu_Packet_Copy_Data_ShiftJisText.Size = new System.Drawing.Size(215, 22);
            this.CMenu_Packet_Copy_Data_ShiftJisText.Text = "Shift_JIS text";
            // 
            // CMenu_Packet_Copy_Data_EucJpText
            // 
            this.CMenu_Packet_Copy_Data_EucJpText.Name = "CMenu_Packet_Copy_Data_EucJpText";
            this.CMenu_Packet_Copy_Data_EucJpText.Size = new System.Drawing.Size(215, 22);
            this.CMenu_Packet_Copy_Data_EucJpText.Text = "EUC-JP text";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(212, 6);
            // 
            // CMenu_Packet_Copy_Data_Custom
            // 
            this.CMenu_Packet_Copy_Data_Custom.Name = "CMenu_Packet_Copy_Data_Custom";
            this.CMenu_Packet_Copy_Data_Custom.Size = new System.Drawing.Size(215, 22);
            this.CMenu_Packet_Copy_Data_Custom.Text = "Custom preview format";
            // 
            // CMenu_Packet_Export
            // 
            this.CMenu_Packet_Export.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMenu_Packet_Export_Data});
            this.CMenu_Packet_Export.Name = "CMenu_Packet_Export";
            this.CMenu_Packet_Export.Size = new System.Drawing.Size(154, 22);
            this.CMenu_Packet_Export.Text = "Export to File";
            // 
            // CMenu_Packet_Export_Data
            // 
            this.CMenu_Packet_Export_Data.Name = "CMenu_Packet_Export_Data";
            this.CMenu_Packet_Export_Data.Size = new System.Drawing.Size(104, 22);
            this.CMenu_Packet_Export_Data.Text = "Data";
            // 
            // LView_Main
            // 
            this.LView_Main.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LView_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LView_Main.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LView_Main.FullRowSelect = true;
            this.LView_Main.GridLines = true;
            this.LView_Main.ItemCountMax = 999999;
            this.LView_Main.Location = new System.Drawing.Point(0, 0);
            this.LView_Main.Name = "LView_Main";
            this.LView_Main.ReadOnly = true;
            this.LView_Main.Size = new System.Drawing.Size(957, 258);
            this.LView_Main.TabIndex = 0;
            this.LView_Main.UseCompatibleStateImageBehavior = false;
            this.LView_Main.View = System.Windows.Forms.View.Details;
            this.LView_Main.VirtualMode = true;
            this.LView_Main.ItemSelectBusyStatusChanged += new System.EventHandler(this.LView_Main_ItemSelectBusyStatusChanged);
            this.LView_Main.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LView_Main_ColumnClick);
            this.LView_Main.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.LView_Main_RetrieveVirtualItem);
            this.LView_Main.SelectedIndexChanged += new System.EventHandler(this.LView_Main_SelectedIndexChanged);
            this.LView_Main.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LView_Main_MouseClick);
            // 
            // BBox_Main
            // 
            this.BBox_Main.AllowDrop = true;
            this.BBox_Main.BackColor = System.Drawing.SystemColors.Window;
            this.BBox_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BBox_Main.EditEnable = false;
            this.BBox_Main.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BBox_Main.InsertEnable = false;
            this.BBox_Main.Location = new System.Drawing.Point(0, 0);
            this.BBox_Main.Name = "BBox_Main";
            this.BBox_Main.Size = new System.Drawing.Size(640, 200);
            this.BBox_Main.TabIndex = 1;
            this.BBox_Main.TextViewEnable = true;
            // 
            // PacketViewInstanceImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.Split_Main);
            this.Controls.Add(this.Panel_ToolBar);
            this.Name = "PacketViewInstanceImpl";
            this.Size = new System.Drawing.Size(957, 511);
            this.Panel_ToolBar.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.GBox_CustomFormat.ResumeLayout(false);
            this.GBox_CustomFormat.PerformLayout();
            this.GBox_CharCode.ResumeLayout(false);
            this.GBox_PreviewByteNum.ResumeLayout(false);
            this.GBox_PreviewByteNum.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_PreviewDataSize)).EndInit();
            this.Split_Main.Panel1.ResumeLayout(false);
            this.Split_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Split_Main)).EndInit();
            this.Split_Main.ResumeLayout(false);
            this.Split_Sub.Panel1.ResumeLayout(false);
            this.Split_Sub.Panel2.ResumeLayout(false);
            this.Split_Sub.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Split_Sub)).EndInit();
            this.Split_Sub.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.CMenu_Packet.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public PacketViewInstanceImpl() : base()
        {
            InitializeComponent();
        }

        public PacketViewInstanceImpl(PacketViewManager viewm, PacketViewClass viewd, PacketViewProperty viewp, Guid id) : base(viewm, viewd, viewp, id)
        {
            prop_ = Property as PacketViewPropertyImpl;

            Disposed += OnDisposed;

            InitializeComponent();
            InitializeContextMenu();
            InitializeCharCodeType();

            /* --- プロパティをUIに反映 --- */
            LView_Main.ItemCountMax = (int)ConfigManager.System.ApplicationCore.Packet_ViewPacketCountLimit.Value;
            BuildPacketViewHeader();
            Num_PreviewDataSize.Value = prop_.PreviewDataSize.Value;
            CBox_CharCode.SelectedItem = prop_.CharCode.Value;
            TBox_CustomFormat.Text = prop_.CustomFormat.Value;
            Menu_ExtView_SelectPacketCount.Checked = prop_.ExtViewSelectPacketCount.Value;
            Menu_ExtView_SelectTotalSize.Checked = prop_.ExtViewSelectTotalSize.Value;
            Menu_ExtView_FirstPacketInfo.Checked = prop_.ExtViewFirstPacketInfo.Value;
            Menu_ExtView_LastPacketInfo.Checked = prop_.ExtViewLastPacketInfo.Value;
            Menu_ExtView_SelectDelta.Checked = prop_.ExtViewSelectDelta.Value;
            Menu_ExtView_SelectRate.Checked = prop_.ExtViewSelectRate.Value;
            BuildExtView();

            /* --- 適用 --- */
            Apply();

            /* --- 表示更新 --- */
            UpdateView();
        }

        private void OnDisposed(object sender, EventArgs e)
        {
        }

        private void InitializeContextMenu()
        {
            void SetupMenuEvent(ToolStripMenuItem menu)
            {
                if (menu == null)return;

                if (menu.HasDropDownItems) {
                    foreach (var menu_sub in menu.DropDownItems) {
                        SetupMenuEvent(menu_sub as ToolStripMenuItem);
                    }
                } else {
                    menu.Click += OnMenuClick;
                }
            }

            /* イベント設定 */
            foreach (var item in CMenu_Packet.Items) {
                SetupMenuEvent(item as ToolStripMenuItem);
            }

            /* 言語設定 */
            var lang = ConfigManager.Language.PacketView.Packet;

            CMenu_Packet_Copy.Text                        = lang.CMenu_Packet_Copy.Value;
            CMenu_Packet_Copy_AllInfo_Csv.Text            = lang.CMenu_Packet_Copy_AllInfo_Csv.Value;
            CMenu_Packet_Copy_Class.Text                  = lang.CMenu_Packet_Copy_Class.Value;
            CMenu_Packet_Copy_Alias.Text                  = lang.CMenu_Packet_Copy_Alias.Value;
            CMenu_Packet_Copy_Information.Text            = lang.CMenu_Packet_Copy_Information.Value;
            CMenu_Packet_Copy_Source.Text                 = lang.CMenu_Packet_Copy_Source.Value;
            CMenu_Packet_Copy_Destination.Text            = lang.CMenu_Packet_Copy_Destination.Value;
            CMenu_Packet_Copy_Message.Text                = lang.CMenu_Packet_Copy_Message.Value;
            CMenu_Packet_Copy_DateTime.Text               = lang.CMenu_Packet_Copy_DateTime.Value;
            CMenu_Packet_Copy_DateTime_UTC_ISO8601.Text   = lang.CMenu_Packet_Copy_DateTime_UTC_ISO8601.Value;
            CMenu_Packet_Copy_DateTime_UTC_Display.Text   = lang.CMenu_Packet_Copy_DateTime_UTC_Display.Value;
            CMenu_Packet_Copy_DateTime_Local_ISO8601.Text = lang.CMenu_Packet_Copy_DateTime_Local_ISO8601.Value;
            CMenu_Packet_Copy_DateTime_Local_Display.Text = lang.CMenu_Packet_Copy_DateTime_Local_Display.Value;
            CMenu_Packet_Copy_Data.Text                   = lang.CMenu_Packet_Copy_Data.Value;
            CMenu_Packet_Copy_Data_BitString.Text         = lang.CMenu_Packet_Copy_Data_BitString.Value;
            CMenu_Packet_Copy_Data_HexString.Text         = lang.CMenu_Packet_Copy_Data_HexString.Value;
            CMenu_Packet_Copy_Data_AsciiText.Text         = lang.CMenu_Packet_Copy_Data_AsciiText.Value;
            CMenu_Packet_Copy_Data_Utf8Text.Text          = lang.CMenu_Packet_Copy_Data_Utf8Text.Value;
            CMenu_Packet_Copy_Data_Utf16BeText.Text       = lang.CMenu_Packet_Copy_Data_Utf16BeText.Value;
            CMenu_Packet_Copy_Data_Utf16LeText.Text       = lang.CMenu_Packet_Copy_Data_Utf16LeText.Value;
            CMenu_Packet_Copy_Data_ShiftJisText.Text      = lang.CMenu_Packet_Copy_Data_ShiftJisText.Value;
            CMenu_Packet_Copy_Data_EucJpText.Text         = lang.CMenu_Packet_Copy_Data_EucJpText.Value;
            CMenu_Packet_Copy_Data_Custom.Text            = lang.CMenu_Packet_Copy_Data_Custom.Value;
            CMenu_Packet_Copy_DataLF.Text                 = lang.CMenu_Packet_Copy_DataLF.Value;
            CMenu_Packet_Copy_DataLF_BitString.Text       = lang.CMenu_Packet_Copy_Data_BitString.Value;
            CMenu_Packet_Copy_DataLF_HexString.Text       = lang.CMenu_Packet_Copy_Data_HexString.Value;
            CMenu_Packet_Copy_DataLF_AsciiText.Text       = lang.CMenu_Packet_Copy_Data_AsciiText.Value;
            CMenu_Packet_Copy_DataLF_Utf8Text.Text        = lang.CMenu_Packet_Copy_Data_Utf8Text.Value;
            CMenu_Packet_Copy_DataLF_Utf16BeText.Text     = lang.CMenu_Packet_Copy_Data_Utf16BeText.Value;
            CMenu_Packet_Copy_DataLF_Utf16LeText.Text     = lang.CMenu_Packet_Copy_Data_Utf16LeText.Value;
            CMenu_Packet_Copy_DataLF_ShiftJisText.Text    = lang.CMenu_Packet_Copy_Data_ShiftJisText.Value;
            CMenu_Packet_Copy_DataLF_EucJpText.Text       = lang.CMenu_Packet_Copy_Data_EucJpText.Value;
            CMenu_Packet_Copy_DataLF_Custom.Text          = lang.CMenu_Packet_Copy_Data_Custom.Value;
            CMenu_Packet_Export.Text                      = lang.CMenu_Packet_Export.Value;

            CMenu_Packet_Copy.Tag                        = null;
            CMenu_Packet_Copy_AllInfo_Csv.Tag            = MenuActionId.Copy_Packet_AllInfo_Csv;
            CMenu_Packet_Copy_Alias.Tag                  = MenuActionId.Copy_Packet_Alias;
            CMenu_Packet_Copy_Information.Tag            = MenuActionId.Copy_Packet_Information;
            CMenu_Packet_Copy_Source.Tag                 = MenuActionId.Copy_Packet_Source;
            CMenu_Packet_Copy_Destination.Tag            = MenuActionId.Copy_Packet_Destination;
            CMenu_Packet_Copy_Message.Tag                = MenuActionId.Copy_Packet_Message;
            CMenu_Packet_Copy_DateTime.Tag               = null;
            CMenu_Packet_Copy_DateTime_UTC_ISO8601.Tag   = MenuActionId.Copy_Packet_DateTime_UTC_ISO8601;
            CMenu_Packet_Copy_DateTime_UTC_Display.Tag   = MenuActionId.Copy_Packet_DateTime_UTC_Display;
            CMenu_Packet_Copy_DateTime_Local_ISO8601.Tag = MenuActionId.Copy_Packet_DateTime_Local_ISO8601;
            CMenu_Packet_Copy_DateTime_Local_Display.Tag = MenuActionId.Copy_Packet_DateTime_Local_Display;
            CMenu_Packet_Copy_Data.Tag                   = null;
            CMenu_Packet_Copy_Data_BitString.Tag         = MenuActionId.Copy_Packet_Data_BitString;
            CMenu_Packet_Copy_Data_HexString.Tag         = MenuActionId.Copy_Packet_Data_HexString;
            CMenu_Packet_Copy_Data_AsciiText.Tag         = MenuActionId.Copy_Packet_Data_AsciiText;
            CMenu_Packet_Copy_Data_Utf8Text.Tag          = MenuActionId.Copy_Packet_Data_Utf8Text;
            CMenu_Packet_Copy_Data_Utf16BeText.Tag       = MenuActionId.Copy_Packet_Data_Utf16BeText;
            CMenu_Packet_Copy_Data_Utf16LeText.Tag       = MenuActionId.Copy_Packet_Data_Utf16LeText;
            CMenu_Packet_Copy_Data_ShiftJisText.Tag      = MenuActionId.Copy_Packet_Data_ShiftJisText;
            CMenu_Packet_Copy_Data_EucJpText.Tag         = MenuActionId.Copy_Packet_Data_EucJpText;
            CMenu_Packet_Copy_Data_Custom.Tag            = MenuActionId.Copy_Packet_Data_Custom;
            CMenu_Packet_Copy_DataLF.Tag                 = null;
            CMenu_Packet_Copy_DataLF_BitString.Tag       = MenuActionId.Copy_Packet_Data_LF_BitString;
            CMenu_Packet_Copy_DataLF_HexString.Tag       = MenuActionId.Copy_Packet_Data_LF_HexString;
            CMenu_Packet_Copy_DataLF_AsciiText.Tag       = MenuActionId.Copy_Packet_Data_LF_AsciiText;
            CMenu_Packet_Copy_DataLF_Utf8Text.Tag        = MenuActionId.Copy_Packet_Data_LF_Utf8Text;
            CMenu_Packet_Copy_DataLF_Utf16BeText.Tag     = MenuActionId.Copy_Packet_Data_LF_Utf16BeText;
            CMenu_Packet_Copy_DataLF_Utf16LeText.Tag     = MenuActionId.Copy_Packet_Data_LF_Utf16LeText;
            CMenu_Packet_Copy_DataLF_ShiftJisText.Tag    = MenuActionId.Copy_Packet_Data_LF_ShiftJisText;
            CMenu_Packet_Copy_DataLF_EucJpText.Tag       = MenuActionId.Copy_Packet_Data_LF_EucJpText;
            CMenu_Packet_Copy_DataLF_Custom.Tag          = MenuActionId.Copy_Packet_Data_LF_Custom;
            CMenu_Packet_Export_Data.Tag                 = MenuActionId.Export_Packet_Data;
        }

        private void InitializePreviewDataSize(decimal size)
        {
            Num_PreviewDataSize.Value = size;
        }

        private void InitializeCharCodeType()
        {
            CBox_CharCode.BeginUpdate();
            {
                CBox_CharCode.Items.Clear();
                foreach (var value in Enum.GetValues(typeof(CharCodeType))) {
                    CBox_CharCode.Items.Add(value);
                }
            }
            CBox_CharCode.EndUpdate();
        }

        private PacketSelectStatus GetDataPacketSelectStatus()
        {
            /* 選択パケットが存在しない場合は選択無しを返答 */
            if (LView_Main.SelectedIndices.Count == 0)return (PacketSelectStatus.NotSelect);

            var count = 0;
            var item_info = (PacketListViewItem)null;

            /* 選択中のパケットにデータパケットが存在するかチェック */
            if (LView_Main.SelectedIndices.Count > 0) {
                for (int index = 0; index < LView_Main.SelectedIndices.Count; index++) {
                    item_info = LView_Main.ItemElementAt(LView_Main.SelectedIndices[index]) as PacketListViewItem;

                    if (item_info == null)continue;

                    /* 2個以上のデータパケットを検出したらループ終了 */
                    if ((++count) > 1)break;
                }
            }

            if (count == 0) {
                return (PacketSelectStatus.NotSelect);
            } else if (count == 1) {
                return (PacketSelectStatus.SingleSelect);
            } else {
                return (PacketSelectStatus.MultiSelect);
            }
        }

        private void UpdateContextMenu()
        {
            var status = GetDataPacketSelectStatus();

            if (status != PacketSelectStatus.NotSelect) {
                CMenu_Packet_Copy_DataLF.Visible = (status == PacketSelectStatus.MultiSelect);
                LView_Main.ContextMenuStrip = CMenu_Packet;
            } else {
                LView_Main.ContextMenuStrip = null;
            }
        }

        private void Apply()
        {
            /* 文字列エンコーダー更新 */
            encoder_ = LoadTextEncoder(prop_.CharCode.Value);

            /* バイナリエディタの文字コード */
            BBox_Main.SetCharCode(prop_.ToBinEditBoxCharCode());
        }

        private void UpdateView()
        {
            Num_PreviewDataSize.ForeColor = (Num_PreviewDataSize.Value != prop_.PreviewDataSize.Value)
                                          ? (Color.Gray)
                                          : (Color.Black);

            TBox_CustomFormat.ForeColor = (TBox_CustomFormat.Text != prop_.CustomFormat.Value)
                                        ? (Color.Gray)
                                        : (Color.Black);
        }

        private void UpdateOperationBusyStatus()
        {
            OperationBusy = LView_Main.ItemSelectBusy;
        }

        private void UpdateSelectStatus()
        {
            UpdateBinEditBox();
            UpdateExtView();
        }

        private void UpdateBinEditBox()
        {
            var item = LView_Main.FocusedItem;
            var packet = (PacketObject)null;

            if (item != null) {
                if (item.Tag is PacketListViewItem item_i) {
                    packet = item_i.Packet;
                }
            }

            SetCurrentPacketStatus(packet);
        }

        private void UpdateExtView()
        {
            var indices = LView_Main.SelectedIndices;
            var index_first = 0;
            var index_last = 0;
            var packet_first = (PacketListViewItem)null;
            var packet_last = (PacketListViewItem)null;
            var select_total_size = (ulong)0;
            var select_delta = TimeSpan.Zero;

            if (indices.Count > 0) {
                index_first = indices[0];
                index_last = indices[indices.Count - 1];
                packet_first = LView_Main.ItemElementAt(index_first) as PacketListViewItem;
                packet_last = LView_Main.ItemElementAt(index_last) as PacketListViewItem;
            }

            /* 選択中のパケットサイズを取得 */
            if (   (ExtViewItem_SelectTotalSize != null)
                || (ExtViewItem_SelectRate != null)
            ) {
                var packet_d = (PacketListViewItem)null;

                foreach (int index in indices) {
                    packet_d = LView_Main.ItemElementAt(index) as PacketListViewItem;
                    if (packet_d == null)continue;
                    select_total_size += (ulong)packet_d.Packet.DataLength;
                }
            }

            /* 選択パケットの時間差を取得 */
            if (   (ExtViewItem_SelectDelta != null)
                || (ExtViewItem_SelectRate != null)
            ) {
                if ((packet_first != null) && (packet_last != null)) {
                    select_delta = packet_last.Packet.MakeTime - packet_first.Packet.MakeTime;
                }
            }

            /* 選択パケット数 */
            if (ExtViewItem_SelectPacketCount != null) {
                ExtViewItem_SelectPacketCount.SubItems[1].Text = indices.Count.ToString();
            }

            /* 選択パケットサイズ */
            if (ExtViewItem_SelectTotalSize != null) {
                ExtViewItem_SelectTotalSize.SubItems[1].Text = String.Format("{0} byte", select_total_size);
            }

            /* 選択パケット(最初)の情報 */
            if (ExtViewItem_FirstPacketInfo != null) {
                if (packet_first != null) {
                    ExtViewItem_FirstPacketInfo.SubItems[1].Text = String.Format("{0} - No.{1}", packet_first.Packet.MakeTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff"), index_first + 1);
                } else {
                    ExtViewItem_FirstPacketInfo.SubItems[1].Text = "";
                }
            }

            /* 選択パケット(最後)の情報 */
            if (ExtViewItem_LastPacketInfo != null) {
                if (packet_last != null) {
                    ExtViewItem_LastPacketInfo.SubItems[1].Text = String.Format("{0} - No.{1}", packet_last.Packet.MakeTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff"), index_last + 1);
                } else {
                    ExtViewItem_LastPacketInfo.SubItems[1].Text = "";
                }
            }

            /* 選択パケット(最後 - 最初)の差分時間 */
            if (ExtViewItem_SelectDelta != null) {
                if ((packet_first != null) && (packet_last != null)) {
                    ExtViewItem_SelectDelta.SubItems[1].Text = String.Format("{0} msec", (uint)(select_delta.TotalMilliseconds));
                } else {
                    ExtViewItem_SelectDelta.SubItems[1].Text = "";
                }
            }

            /* 選択パケット(最後 - 最初)の通信レート */
            if (ExtViewItem_SelectRate != null) {
                var comm_rate_bps = (ulong)(select_total_size / select_delta.TotalSeconds);

                ExtViewItem_SelectRate.SubItems[1].Text = String.Format("{0}B/s ({1})", TextUtil.DecToText(comm_rate_bps), comm_rate_bps);
            }
        }

        private void SetCurrentPacketStatus(PacketObject packet)
        {
            if (packet != null) {
                /* データパケットのときはデータ内容を表示する */
                if (packet.Attribute == PacketAttribute.Data) {
                    BBox_Main.SetData(packet.Data);
                }

            } else {
                BBox_Main.DataClear();
            }
        }

        private Encoding LoadTextEncoder(CharCodeType type)
        {
            switch (type) {
                case CharCodeType.ShiftJIS:     return (Encoding.GetEncoding(932));
                case CharCodeType.UTF8:         return (Encoding.UTF8);
                default:                        return (Encoding.ASCII);
            }
        }

        private ToolStripMenuItem BuildContextMenu()
        {
            var menu_root = new ToolStripMenuItem();


            return (menu_root);
        }

        private void BuildPacketViewHeader()
        {
            LView_Main.BeginUpdate();
            {
                /* 先にデータをすべて削除してからヘッダーを削除する */
                LView_Main.ItemClear();
                LView_Main.Columns.Clear();

                /* メインヘッダー */
                LView_Main.Columns.Add(
                    new ColumnHeader()
                    {
                        Text = "No.",
                        Width = 50,
                    }
                );

                /* 列要素をオブジェクト化 */
                column_data_ = prop_.ColumnList.Value.Select(v => new PacketListColumnData(v)).ToArray();

                foreach (var data in column_data_) {
                    LView_Main.Columns.Add(
                        new ColumnHeader()
                        {
                            Text = data.Config.Text,
                            Width = (int)data.Config.Width,
                        }
                    );
                }
            }
            LView_Main.EndUpdate();

        }

        private void BuildExtView()
        {
            LView_ExtInfo.BeginUpdate();
            {
                /* 先にデータをすべて削除してからヘッダーを削除する */
                LView_ExtInfo.Items.Clear();

                ExtViewItem_SelectPacketCount = null;
                ExtViewItem_SelectTotalSize = null;
                ExtViewItem_FirstPacketInfo = null;
                ExtViewItem_LastPacketInfo = null;
                ExtViewItem_SelectDelta = null;

                /* 選択パケット数 */
                if (Menu_ExtView_SelectPacketCount.Checked) {
                    ExtViewItem_SelectPacketCount = new ListViewItem() {
                        Text = "Select packet count"
                    };
                    ExtViewItem_SelectPacketCount.SubItems.Add("");
                    ExtViewItem_SelectPacketCount = LView_ExtInfo.Items.Add(ExtViewItem_SelectPacketCount);
                }

                /* 選択パケットサイズ */
                if (Menu_ExtView_SelectTotalSize.Checked) {
                    ExtViewItem_SelectTotalSize = new ListViewItem() {
                        Text = "Select packet total size"
                    };
                    ExtViewItem_SelectTotalSize.SubItems.Add("");
                    ExtViewItem_SelectTotalSize = LView_ExtInfo.Items.Add(ExtViewItem_SelectTotalSize);
                }

                /* 選択パケット(最初)の情報 */
                if (Menu_ExtView_FirstPacketInfo.Checked) {
                    ExtViewItem_FirstPacketInfo = new ListViewItem() {
                        Text = "Information on selected packet (first)"
                    };
                    ExtViewItem_FirstPacketInfo.SubItems.Add("");
                    ExtViewItem_FirstPacketInfo = LView_ExtInfo.Items.Add(ExtViewItem_FirstPacketInfo);
                }

                /* 選択パケット(最後)の情報 */
                if (Menu_ExtView_LastPacketInfo.Checked) {
                    ExtViewItem_LastPacketInfo = new ListViewItem() {
                        Text = "Information on selected packet (last)"
                    };
                    ExtViewItem_LastPacketInfo.SubItems.Add("");
                    ExtViewItem_LastPacketInfo = LView_ExtInfo.Items.Add(ExtViewItem_LastPacketInfo);
                }

                /* 選択パケット(最後 - 最初)の差分時間 */
                if (Menu_ExtView_SelectDelta.Checked) {
                    ExtViewItem_SelectDelta = new ListViewItem() {
                        Text = "Time difference of selection packet (last-first)"
                    };
                    ExtViewItem_SelectDelta.SubItems.Add("");
                    ExtViewItem_SelectDelta = LView_ExtInfo.Items.Add(ExtViewItem_SelectDelta);
                }

                /* 選択パケット(最後 - 最初)の通信レート */
                if (Menu_ExtView_SelectRate.Checked) {
                    ExtViewItem_SelectRate = new ListViewItem() {
                        Text = "Communication rate of selection packet (last-first)"
                    };
                    ExtViewItem_SelectRate.SubItems.Add("");
                    ExtViewItem_SelectRate = LView_ExtInfo.Items.Add(ExtViewItem_SelectRate);
                }
            }
            LView_ExtInfo.EndUpdate();

            UpdateExtView();
        }

        private string DataPacketToCustomText(PacketObject packet)
        {
            var text_prev = prop_.CustomFormat.Value;
            var text_new  = text_prev;
            var convert_count = 0;

            try {
                format_packet_ = packet;

                do {
                    /* 変換前のテキストをバックアップ */
                    text_prev = text_new;

                    /* 変換ブロックを検索 */
                    text_new = format_regex_.Replace(text_prev, GetPacketToCustomTask);

                    /* 変換回数更新 */
                    convert_count++;

                } while ((text_new != text_prev) && (convert_count < 100));
            } catch { }

            return (text_new);
        }

        private string GetPacketToCustomTask(Match match)
        {
            return (format_packet_.GetFormatString(match.Groups["value"].Captures[0].Value));
        }

        private ListViewItem ItemInfoToListViewItem(PacketListViewItem item_i)
        {
            /* メインアイテム */
            var item = new ListViewItem()
            {
                Text = item_i.No.ToString(),
                Tag = item_i,
            };

            /* サブサイテム */
            PacketToListViewItem_SubUpdate(item, item_i.Packet);

            return (item);
        }

        private void PacketToListViewItem_SubUpdate(ListViewItem item, PacketObject packet)
        {
            switch (packet.Attribute) {
                case PacketAttribute.Message:
                    PacketToListViewItem_Message(item, packet);
                    break;

                case PacketAttribute.Data:
                    PacketToListViewItem_Data(item, packet);
                    break;

                default:
                    break;
            }
        }

        private void PacketToListViewItem_Common(ColumnType type, ListViewItem item, PacketObject packet)
        {
            switch (type) {
                case ColumnType.Class:
                    item.SubItems.Add(packet.Class);
                    break;

                case ColumnType.Alias:
                    item.SubItems.Add(packet.Alias);
                    break;

                case ColumnType.Datetime_UTC:
                    item.SubItems.Add(packet.GetElementText(PacketElementID.DateTime_UTC_Display));
                    break;

                case ColumnType.Datetime_Local:
                    item.SubItems.Add(packet.GetElementText(PacketElementID.DateTime_Local_Display));
                    break;

                case ColumnType.Information:
                    item.SubItems.Add(packet.Information);
                    break;

                default:
                    item.SubItems.Add("");
                    break;
            }
        }

        private void PacketToListViewItem_Message(ListViewItem item, PacketObject packet)
        {
            foreach (var config in column_data_) {
                if (config.Input(packet)) {
                    switch (config.Config.Type) {
                        case ColumnType.DataPreviewBinary:
                        case ColumnType.DataPreviewText:
                        case ColumnType.DataPreviewCustom:
                            item.SubItems.Add(packet.Message);
                            break;

                        default:
                            PacketToListViewItem_Common(config.Config.Type, item, packet);
                            break;
                    }
                } else {
                    item.SubItems.Add("");
                }
            }

            /* 背景色 */
            item.BackColor = Color.LightGoldenrodYellow;
        }

        private void PacketToListViewItem_Data(ListViewItem item, PacketObject packet)
        {
            foreach (var config in column_data_) {
                if (config.Input(packet)) {
                    switch (config.Config.Type) {
                        case ColumnType.Source:
                            item.SubItems.Add(packet.Source);
                            break;

                        case ColumnType.Destination:
                            item.SubItems.Add(packet.Destination);
                            break;

                        case ColumnType.DataLength:
                            item.SubItems.Add(packet.DataLength.ToString());
                            break;

                        case ColumnType.DataPreviewBinary:
                            item.SubItems.Add(packet.DataToHexString(0, preview_data_size_bin_, " "));
                            break;

                        case ColumnType.DataPreviewBinaryWithoutDivider:
                            item.SubItems.Add(packet.DataToHexString(0, preview_data_size_bin_wd_, ""));
                            break;

                        case ColumnType.DataPreviewText:
                            item.SubItems.Add(encoder_.GetString(packet.GetBytes(0, preview_data_size_text_).ToArray()));
                            break;

                        case ColumnType.DataPreviewCustom:
                            item.SubItems.Add(DataPacketToCustomText(packet));
                            break;

                        default:
                            PacketToListViewItem_Common(config.Config.Type, item, packet);
                            break;
                    }
                } else {
                    item.SubItems.Add("");
                }
            }

            /* 背景色 */
            item.BackColor = (packet.Direction == PacketDirection.Recv)
                           ? (Color.LightGreen)
                           : (Color.LightPink);
        }

        private IEnumerable<PacketListViewItem> GetSelectItems()
        {
            var packet = (PacketListViewItem)null;

            for (int index = 0; index < LView_Main.SelectedIndices.Count; index++) {
                /* データパケットを取得 */
                packet = LView_Main.ItemElementAt(LView_Main.SelectedIndices[index]) as PacketListViewItem;

                if (packet != null) {
                    yield return (packet);
                }
            }
        }

        private string GetPacketInfoTextFromSelectPackets(PacketElementID elem_id, string divider)
        {
            var str = new StringBuilder(0xFFFF);

            foreach (var item_info in GetSelectItems()) {
                /* 16進文字列として追加 */
                str.Append(item_info.Packet.GetElementText(elem_id));

                /* 分割コード挿入 */
                str.Append(divider);
            }

            return (str.ToString());
        }

        private string GetPacketInfoCsvFromSelectPackets()
        {
            var str = new StringBuilder(0xFFFF);

            var elem_list = new[]
            {
                PacketElementID.Facility,
                PacketElementID.Alias,
                PacketElementID.Priority,
                PacketElementID.Attribute,
                PacketElementID.DateTime_UTC_Display,
                PacketElementID.DateTime_Local_Display,
                PacketElementID.Direction,
                PacketElementID.Information,
                PacketElementID.Source,
                PacketElementID.Destination,
                PacketElementID.Mark,
                PacketElementID.Message,
                PacketElementID.Data_HexString
            };

            /* ヘッダー */
            foreach (var elem in elem_list) {
                str.Append(elem.ToString());
                str.Append(',');
            }
            if (str.Length > 0) {
                str.Remove(str.Length - 1, 1);
                str.AppendLine();
            }

            /* データ */
            foreach (var item_info in GetSelectItems()) {
                foreach (var elem in elem_list) {
                    str.Append(item_info.Packet.GetElementText(elem));
                    str.Append(',');
                }
                if (str.Length > 0) {
                    str.Remove(str.Length - 1, 1);
                    str.AppendLine();
                }
            }

            return (str.ToString());
        }

        private string GetPacketInfoCustomFromSelectPackets(string divider)
        {
            var str = new StringBuilder(0xFFFF);

            foreach (var item_info in GetSelectItems()) {
                /* カスタム文字列として追加 */
                str.Append(DataPacketToCustomText(item_info.Packet));

                /* 分割コード挿入 */
                str.Append(divider);
            }

            return (str.ToString());
        }

        private void ExportSelectPacketsData(string file_path)
        {
            if (file_path == null)return;

            try {
                using (var file = File.Open(file_path, FileMode.Create)) {
                    var data = (byte[])null;

                    foreach (var item_info in GetSelectItems()) {
                        data = item_info.Packet.Data;

                        file.Write(data, 0, data.Length);
                    }
                }
            } catch {
            }
        }

        protected override void OnBackupProperty()
        {
            /* PreviewDataSize */
            prop_.PreviewDataSize.Value = Num_PreviewDataSize.Value;

            /* CharCode */
            prop_.CharCode.Value = (CharCodeType)CBox_CharCode.SelectedItem;

            /* CustomText */
            prop_.CustomFormat.Value = TBox_CustomFormat.Text;

            /* ExtView Backup */
            prop_.ExtViewSelectPacketCount.Value = Menu_ExtView_SelectPacketCount.Checked;
            prop_.ExtViewSelectTotalSize.Value = Menu_ExtView_SelectTotalSize.Checked;
            prop_.ExtViewFirstPacketInfo.Value = Menu_ExtView_FirstPacketInfo.Checked;
            prop_.ExtViewLastPacketInfo.Value = Menu_ExtView_LastPacketInfo.Checked;
            prop_.ExtViewSelectDelta.Value = Menu_ExtView_SelectDelta.Checked;
            prop_.ExtViewSelectRate.Value = Menu_ExtView_SelectRate.Checked;
        }

        protected override void OnClearPacket()
        {
            LView_Main.ItemClear();
            BBox_Main.DataClear();

            /* リストビューの最大数を再セットアップ */
            LView_Main.ItemCountMax = (int)ConfigManager.System.ApplicationCore.Packet_ViewPacketCountLimit.Value;

            next_item_no_ = 1;

            /* 列毎のパケットフィルターの状態をリセット */
            if (column_data_ != null) {
                Array.ForEach(column_data_, data => data.Reset());
            }
        }

        protected override void OnDrawPacketBegin(bool auto_scroll)
        {
            /* 表示パラメータ更新 */
            preview_data_size_bin_ = Math.Min((int)prop_.PreviewDataSize.Value, 260 / 3 - 1);
            preview_data_size_bin_wd_ = Math.Min((int)prop_.PreviewDataSize.Value, 260 / 2 - 1);
            preview_data_size_text_ = Math.Min((int)prop_.PreviewDataSize.Value, 260 * 6);

            /* ちらつき防止用の一時バッファ */
            list_items_temp_ = new List<PacketListViewItem>();

            /* リストビューの描画開始 */
            LView_Main.BeginUpdate();
        }

        protected override void OnDrawPacketEnd(bool auto_scroll, bool next_packet_exist)
        {
            /* 一時リストをリストビューに追加 */
            LView_Main.ItemAddRange(list_items_temp_);
            list_items_temp_ = null;

            /* 自動スクロール */
            if ((auto_scroll) && (LView_Main.ItemCount > 0)) {
                LView_Main.EnsureVisible(LView_Main.ItemCount - 1);
            }

            /* リストビューの描画完了 */
            LView_Main.EndUpdate();
        }

        protected override void OnDrawPacket(PacketObject packet)
        {
            next_item_no_ = Math.Max(next_item_no_, ITEM_NO_MIN);
            next_item_no_ = Math.Min(next_item_no_, ITEM_NO_MAX);

            list_items_temp_.Add(new PacketListViewItem(next_item_no_, packet));

            next_item_no_++;
        }

        private void LView_Main_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            /* 項目編集画面を表示 */
            var dialog = new Forms.ColumnHeaderEditForm(
                                    (IEnumerable<ColumnType>)Enum.GetValues(typeof(ColumnType)),
                                    prop_.ColumnList.Value);

            if (dialog.ShowDialog() != DialogResult.OK)return;

            /* プロパティを更新 */
            prop_.ColumnList.Value.Clear();
            foreach (var item in dialog.UserItems) {
                prop_.ColumnList.Value.Add(new Configs.ColumnHeaderConfig(item));
            }

            /* 列を再構築 */
            BuildPacketViewHeader();

            /* パケットを再描画 */
            RedrawPacket();
        }

        private void LView_Main_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSelectStatus();
        }

        private void LView_Main_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) {
                UpdateContextMenu();
            }
        }

        private void Menu_ExtView_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menu) {
                menu.Checked = !menu.Checked;

                BuildExtView();
            }
        }

        private void Num_PreviewDataSize_ValueChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void Num_PreviewDataSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                BackupProperty();
                UpdateView();
                RedrawPacket();
            }
        }

        private void TBox_CustomFormat_TextChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void TBox_CustomFormat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                BackupProperty();
                UpdateView();
                RedrawPacket();
            }
        }

        private void CBox_CharCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            BackupProperty();
            UpdateView();
            RedrawPacket();
        }

        private void OnMenuClick(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menu) {
                try {
                    switch ((MenuActionId)Enum.ToObject(typeof(MenuActionId), menu.Tag)) {
                        case MenuActionId.Copy_Packet_AllInfo_Csv:
                        {
                            Clipboard.SetText(GetPacketInfoCsvFromSelectPackets(), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Alias:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Alias, Environment.NewLine), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_DateTime_UTC_ISO8601:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.DateTime_UTC_ISO8601, Environment.NewLine), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_DateTime_UTC_Display:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.DateTime_UTC_Display, Environment.NewLine), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_DateTime_Local_ISO8601:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.DateTime_Local_ISO8601, Environment.NewLine), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_DateTime_Local_Display:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.DateTime_Local_Display, Environment.NewLine), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Information:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Information, Environment.NewLine), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Source:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Source, Environment.NewLine), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Destination:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Destination, Environment.NewLine), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Message:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Message, Environment.NewLine), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Data_BitString:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Data_BitString, ""), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Data_HexString:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Data_HexString, ""), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Data_AsciiText:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Data_TextAscii, ""), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Data_Utf8Text:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Data_TextUTF8, ""), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Data_Utf16BeText:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Data_TextUTF16BE, ""), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Data_Utf16LeText:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Data_TextUTF16LE, ""), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Data_ShiftJisText:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Data_TextShiftJIS, ""), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Data_EucJpText:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Data_TextEucJp, ""), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Data_Custom:
                        {
                            Clipboard.SetText(GetPacketInfoCustomFromSelectPackets(""), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Data_LF_BitString:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Data_BitString, Environment.NewLine), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Data_LF_HexString:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Data_HexString, Environment.NewLine), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Data_LF_AsciiText:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Data_TextAscii, Environment.NewLine), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Data_LF_Utf8Text:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Data_TextUTF8, Environment.NewLine), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Data_LF_Utf16BeText:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Data_TextUTF16BE, Environment.NewLine), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Data_LF_Utf16LeText:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Data_TextUTF16LE, Environment.NewLine), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Data_LF_ShiftJisText:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Data_TextShiftJIS, Environment.NewLine), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Data_LF_EucJpText:
                        {
                            Clipboard.SetText(GetPacketInfoTextFromSelectPackets(PacketElementID.Data_TextEucJp, Environment.NewLine), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Copy_Packet_Data_LF_Custom:
                        {
                            Clipboard.SetText(GetPacketInfoCustomFromSelectPackets(Environment.NewLine), TextDataFormat.Text);
                        }
                            break;

                        case MenuActionId.Export_Packet_Data:
                        {
                            ExportSelectPacketsData(FormUiManager.AnyFileSave());
                        }
                            break;
                    }

                } catch {
                }
            }
        }

        private void LView_Main_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = ItemInfoToListViewItem(LView_Main.ItemElementAt(e.ItemIndex) as PacketListViewItem);
        }

        private void LView_Main_ItemSelectBusyStatusChanged(object sender, EventArgs e)
        {
            UpdateOperationBusyStatus();
        }

        private void RBtn_Layout_0_CheckedChanged(object sender, EventArgs e)
        {
            Split_Main.Orientation = Orientation.Vertical;
            Split_Main.Panel2Collapsed = true;
        }

        private void RBtn_Layout_1_CheckedChanged(object sender, EventArgs e)
        {
            Split_Main.Orientation = Orientation.Horizontal;
            Split_Main.Panel2Collapsed = false;

            Split_Sub.Orientation = Orientation.Vertical;
            Split_Sub.SplitterDistance = 640;
            Split_Sub.FixedPanel = FixedPanel.Panel1;
        }

        private void RBtn_Layout_2_CheckedChanged(object sender, EventArgs e)
        {
            Split_Main.Orientation = Orientation.Vertical;
            Split_Main.Panel2Collapsed = false;

            Split_Sub.Orientation = Orientation.Horizontal;
            Split_Sub.SplitterDistance = 320;
            Split_Sub.FixedPanel = FixedPanel.None;
        }
    }
}
