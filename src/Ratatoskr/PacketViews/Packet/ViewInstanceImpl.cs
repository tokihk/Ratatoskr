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
using Ratatoskr.Generic;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;
using Ratatoskr.Generic.Controls;

namespace Ratatoskr.PacketViews.Packet
{
    internal sealed class ViewInstanceImpl : ViewInstance
    {
        private enum PacketSelectStatus
        {
            NotSelect,
            SingleSelect,
            MultiSelect,
        }


        private ViewPropertyImpl prop_;
        private Encoding encoder_;

        private readonly Regex   format_regex_ = new Regex(@"\$\{(?<value>[^\}]*)\}", RegexOptions.Compiled);
        private DataPacketObject format_packet_ = null;

        private List<ListViewItem> list_items_temp_;

        private ListViewItem ExtViewItem_SelectPacketCount = null;
        private ListViewItem ExtViewItem_SelectTotalSize = null;
        private ListViewItem ExtViewItem_FirstPacketInfo = null;
        private ListViewItem ExtViewItem_LastPacketInfo = null;
        private ListViewItem ExtViewItem_SelectDelta = null;

        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Panel Panel_ToolBar;
        private System.Windows.Forms.GroupBox GBox_CharCode;
        private System.Windows.Forms.ComboBox CBox_CharCode;
        private System.Windows.Forms.GroupBox GBox_PreviewByteNum;
        private System.Windows.Forms.SplitContainer Split_Main;
        private ListViewEx LView_Main;

        private SplitContainer splitContainer1;

        private BinEditBox BBox_Main;

        private ContextMenuStrip CMenu_Packet;
        private ToolStripMenuItem CMenu_Packet_CopyToClipboard;
        private ToolStripMenuItem CMenu_Packet_CopyToClipboard_AllInfo_Csv;
        private ToolStripMenuItem CMenu_Packet_CopyToClipboard_Data_String;
        private ToolStripMenuItem CMenu_Packet_CopyToClipboard_Data_String_NewLineOn;
        private ToolStripMenuItem CMenu_Packet_CopyToClipboard_Data_String_NewLineOff;
        private ToolStripMenuItem CMenu_Packet_CopyToClipboard_Data_Hex;
        private ToolStripMenuItem CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOn;
        private ToolStripMenuItem CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOff;
        private ToolStripMenuItem CMenu_Packet_CopyToPacketList;

        private GroupBox GBox_CustomFormat;
        private NumericUpDown Num_PreviewDataSize;
        private ToolStripMenuItem CMenu_Packet_CopyToClipboard_Data_Custom;
        private ToolStripMenuItem CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOn;
        private ToolStripMenuItem CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOff;
        private ToolStrip toolStrip1;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem Menu_ExtView_SelectPacketCount;
        private ToolStripMenuItem Menu_ExtView_SelectTotalSize;
        private ToolStripMenuItem Menu_ExtView_FirstPacketInfo;
        private ToolStripMenuItem Menu_ExtView_LastPacketInfo;
        private ToolStripMenuItem Menu_ExtView_SelectDelta;
        private ListView LView_ExtInfo;
        private ColumnHeader LView_ExtInfoColumn_Name;
        private ColumnHeader LView_ExtInfoColumn_Value;
        private TextBox TBox_CustomFormat;


        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewInstanceImpl));
            this.Panel_ToolBar = new System.Windows.Forms.Panel();
            this.GBox_CustomFormat = new System.Windows.Forms.GroupBox();
            this.TBox_CustomFormat = new System.Windows.Forms.TextBox();
            this.GBox_CharCode = new System.Windows.Forms.GroupBox();
            this.CBox_CharCode = new System.Windows.Forms.ComboBox();
            this.GBox_PreviewByteNum = new System.Windows.Forms.GroupBox();
            this.Num_PreviewDataSize = new System.Windows.Forms.NumericUpDown();
            this.Split_Main = new System.Windows.Forms.SplitContainer();
            this.LView_Main = new Ratatoskr.Generic.Controls.ListViewEx();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.BBox_Main = new Ratatoskr.Generic.Controls.BinEditBox();
            this.LView_ExtInfo = new System.Windows.Forms.ListView();
            this.LView_ExtInfoColumn_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LView_ExtInfoColumn_Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.Menu_ExtView_SelectPacketCount = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_ExtView_SelectTotalSize = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_ExtView_FirstPacketInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_ExtView_LastPacketInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_ExtView_SelectDelta = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CMenu_Packet_CopyToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_CopyToClipboard_AllInfo_Csv = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_CopyToClipboard_Data_String = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_CopyToClipboard_Data_String_NewLineOn = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_CopyToClipboard_Data_String_NewLineOff = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_CopyToClipboard_Data_Hex = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOn = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOff = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_CopyToClipboard_Data_Custom = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOn = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOff = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Packet_CopyToPacketList = new System.Windows.Forms.ToolStripMenuItem();
            this.Panel_ToolBar.SuspendLayout();
            this.GBox_CustomFormat.SuspendLayout();
            this.GBox_CharCode.SuspendLayout();
            this.GBox_PreviewByteNum.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_PreviewDataSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Split_Main)).BeginInit();
            this.Split_Main.Panel1.SuspendLayout();
            this.Split_Main.Panel2.SuspendLayout();
            this.Split_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.CMenu_Packet.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_ToolBar
            // 
            this.Panel_ToolBar.Controls.Add(this.GBox_CustomFormat);
            this.Panel_ToolBar.Controls.Add(this.GBox_CharCode);
            this.Panel_ToolBar.Controls.Add(this.GBox_PreviewByteNum);
            this.Panel_ToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_ToolBar.Location = new System.Drawing.Point(0, 0);
            this.Panel_ToolBar.Name = "Panel_ToolBar";
            this.Panel_ToolBar.Size = new System.Drawing.Size(957, 49);
            this.Panel_ToolBar.TabIndex = 0;
            // 
            // GBox_CustomFormat
            // 
            this.GBox_CustomFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_CustomFormat.Controls.Add(this.TBox_CustomFormat);
            this.GBox_CustomFormat.Location = new System.Drawing.Point(255, 5);
            this.GBox_CustomFormat.Name = "GBox_CustomFormat";
            this.GBox_CustomFormat.Size = new System.Drawing.Size(699, 39);
            this.GBox_CustomFormat.TabIndex = 2;
            this.GBox_CustomFormat.TabStop = false;
            this.GBox_CustomFormat.Text = "カスタムフォーマット";
            // 
            // TBox_CustomFormat
            // 
            this.TBox_CustomFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_CustomFormat.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TBox_CustomFormat.Location = new System.Drawing.Point(3, 15);
            this.TBox_CustomFormat.Name = "TBox_CustomFormat";
            this.TBox_CustomFormat.Size = new System.Drawing.Size(693, 19);
            this.TBox_CustomFormat.TabIndex = 0;
            this.TBox_CustomFormat.TextChanged += new System.EventHandler(this.TBox_CustomFormat_TextChanged);
            this.TBox_CustomFormat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBox_CustomFormat_KeyDown);
            // 
            // GBox_CharCode
            // 
            this.GBox_CharCode.Controls.Add(this.CBox_CharCode);
            this.GBox_CharCode.Location = new System.Drawing.Point(129, 4);
            this.GBox_CharCode.Name = "GBox_CharCode";
            this.GBox_CharCode.Size = new System.Drawing.Size(120, 40);
            this.GBox_CharCode.TabIndex = 1;
            this.GBox_CharCode.TabStop = false;
            this.GBox_CharCode.Text = "文字列: 文字コード";
            // 
            // CBox_CharCode
            // 
            this.CBox_CharCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_CharCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_CharCode.FormattingEnabled = true;
            this.CBox_CharCode.Location = new System.Drawing.Point(3, 15);
            this.CBox_CharCode.Name = "CBox_CharCode";
            this.CBox_CharCode.Size = new System.Drawing.Size(114, 20);
            this.CBox_CharCode.TabIndex = 0;
            this.CBox_CharCode.SelectedIndexChanged += new System.EventHandler(this.CBox_CharCode_SelectedIndexChanged);
            // 
            // GBox_PreviewByteNum
            // 
            this.GBox_PreviewByteNum.Controls.Add(this.Num_PreviewDataSize);
            this.GBox_PreviewByteNum.Location = new System.Drawing.Point(3, 4);
            this.GBox_PreviewByteNum.Name = "GBox_PreviewByteNum";
            this.GBox_PreviewByteNum.Size = new System.Drawing.Size(120, 40);
            this.GBox_PreviewByteNum.TabIndex = 0;
            this.GBox_PreviewByteNum.TabStop = false;
            this.GBox_PreviewByteNum.Text = "プレビューバイト数";
            // 
            // Num_PreviewDataSize
            // 
            this.Num_PreviewDataSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Num_PreviewDataSize.Location = new System.Drawing.Point(3, 15);
            this.Num_PreviewDataSize.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.Num_PreviewDataSize.Name = "Num_PreviewDataSize";
            this.Num_PreviewDataSize.Size = new System.Drawing.Size(114, 19);
            this.Num_PreviewDataSize.TabIndex = 0;
            this.Num_PreviewDataSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.Split_Main.Panel2.Controls.Add(this.splitContainer1);
            this.Split_Main.Panel2MinSize = 150;
            this.Split_Main.Size = new System.Drawing.Size(957, 462);
            this.Split_Main.SplitterDistance = 258;
            this.Split_Main.TabIndex = 1;
            // 
            // LView_Main
            // 
            this.LView_Main.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LView_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LView_Main.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LView_Main.FullRowSelect = true;
            this.LView_Main.GridLines = true;
            this.LView_Main.Location = new System.Drawing.Point(0, 0);
            this.LView_Main.Name = "LView_Main";
            this.LView_Main.ReadOnly = true;
            this.LView_Main.Size = new System.Drawing.Size(957, 258);
            this.LView_Main.TabIndex = 0;
            this.LView_Main.UseCompatibleStateImageBehavior = false;
            this.LView_Main.View = System.Windows.Forms.View.Details;
            this.LView_Main.VirtualMode = true;
            this.LView_Main.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LView_Main_ColumnClick);
            this.LView_Main.SelectedIndexChanged += new System.EventHandler(this.LView_Main_SelectedIndexChanged);
            this.LView_Main.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LView_Main_MouseClick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.BBox_Main);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.LView_ExtInfo);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(957, 200);
            this.splitContainer1.SplitterDistance = 640;
            this.splitContainer1.TabIndex = 0;
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
            this.LView_ExtInfoColumn_Name.Text = "情報名";
            this.LView_ExtInfoColumn_Name.Width = 160;
            // 
            // LView_ExtInfoColumn_Value
            // 
            this.LView_ExtInfoColumn_Value.Text = "値";
            this.LView_ExtInfoColumn_Value.Width = 280;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(313, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_ExtView_SelectPacketCount,
            this.Menu_ExtView_SelectTotalSize,
            this.Menu_ExtView_FirstPacketInfo,
            this.Menu_ExtView_LastPacketInfo,
            this.Menu_ExtView_SelectDelta});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(105, 22);
            this.toolStripDropDownButton1.Text = "表示情報の選択";
            // 
            // Menu_ExtView_SelectPacketCount
            // 
            this.Menu_ExtView_SelectPacketCount.Name = "Menu_ExtView_SelectPacketCount";
            this.Menu_ExtView_SelectPacketCount.Size = new System.Drawing.Size(267, 22);
            this.Menu_ExtView_SelectPacketCount.Text = "選択パケット数";
            this.Menu_ExtView_SelectPacketCount.Click += new System.EventHandler(this.Menu_ExtView_Click);
            // 
            // Menu_ExtView_SelectTotalSize
            // 
            this.Menu_ExtView_SelectTotalSize.Name = "Menu_ExtView_SelectTotalSize";
            this.Menu_ExtView_SelectTotalSize.Size = new System.Drawing.Size(267, 22);
            this.Menu_ExtView_SelectTotalSize.Text = "選択パケットの合計データサイズ";
            this.Menu_ExtView_SelectTotalSize.Click += new System.EventHandler(this.Menu_ExtView_Click);
            // 
            // Menu_ExtView_FirstPacketInfo
            // 
            this.Menu_ExtView_FirstPacketInfo.Name = "Menu_ExtView_FirstPacketInfo";
            this.Menu_ExtView_FirstPacketInfo.Size = new System.Drawing.Size(267, 22);
            this.Menu_ExtView_FirstPacketInfo.Text = "選択パケット(先頭)の情報";
            this.Menu_ExtView_FirstPacketInfo.Click += new System.EventHandler(this.Menu_ExtView_Click);
            // 
            // Menu_ExtView_LastPacketInfo
            // 
            this.Menu_ExtView_LastPacketInfo.Name = "Menu_ExtView_LastPacketInfo";
            this.Menu_ExtView_LastPacketInfo.Size = new System.Drawing.Size(267, 22);
            this.Menu_ExtView_LastPacketInfo.Text = "選択パケット(最後)の情報";
            this.Menu_ExtView_LastPacketInfo.Click += new System.EventHandler(this.Menu_ExtView_Click);
            // 
            // Menu_ExtView_SelectDelta
            // 
            this.Menu_ExtView_SelectDelta.Name = "Menu_ExtView_SelectDelta";
            this.Menu_ExtView_SelectDelta.Size = new System.Drawing.Size(267, 22);
            this.Menu_ExtView_SelectDelta.Text = "選択パケット(最後 - 先頭)の時刻差";
            this.Menu_ExtView_SelectDelta.Click += new System.EventHandler(this.Menu_ExtView_Click);
            // 
            // CMenu_Packet
            // 
            this.CMenu_Packet.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMenu_Packet_CopyToClipboard,
            this.CMenu_Packet_CopyToPacketList});
            this.CMenu_Packet.Name = "CMenu_Data";
            this.CMenu_Packet.Size = new System.Drawing.Size(209, 48);
            // 
            // CMenu_Packet_CopyToClipboard
            // 
            this.CMenu_Packet_CopyToClipboard.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMenu_Packet_CopyToClipboard_AllInfo_Csv,
            this.CMenu_Packet_CopyToClipboard_Data_String,
            this.CMenu_Packet_CopyToClipboard_Data_Hex,
            this.CMenu_Packet_CopyToClipboard_Data_Custom});
            this.CMenu_Packet_CopyToClipboard.Name = "CMenu_Packet_CopyToClipboard";
            this.CMenu_Packet_CopyToClipboard.Size = new System.Drawing.Size(208, 22);
            this.CMenu_Packet_CopyToClipboard.Text = "クリップボードへコピー";
            // 
            // CMenu_Packet_CopyToClipboard_AllInfo_Csv
            // 
            this.CMenu_Packet_CopyToClipboard_AllInfo_Csv.Name = "CMenu_Packet_CopyToClipboard_AllInfo_Csv";
            this.CMenu_Packet_CopyToClipboard_AllInfo_Csv.Size = new System.Drawing.Size(205, 22);
            this.CMenu_Packet_CopyToClipboard_AllInfo_Csv.Text = "全情報(CSV形式)";
            this.CMenu_Packet_CopyToClipboard_AllInfo_Csv.Click += new System.EventHandler(this.CMenu_Packet_CopyToClipboard_AllInfo_Csv_Click);
            // 
            // CMenu_Packet_CopyToClipboard_Data_String
            // 
            this.CMenu_Packet_CopyToClipboard_Data_String.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMenu_Packet_CopyToClipboard_Data_String_NewLineOn,
            this.CMenu_Packet_CopyToClipboard_Data_String_NewLineOff});
            this.CMenu_Packet_CopyToClipboard_Data_String.Name = "CMenu_Packet_CopyToClipboard_Data_String";
            this.CMenu_Packet_CopyToClipboard_Data_String.Size = new System.Drawing.Size(205, 22);
            this.CMenu_Packet_CopyToClipboard_Data_String.Tag = "0";
            this.CMenu_Packet_CopyToClipboard_Data_String.Text = "データ: 文字列";
            this.CMenu_Packet_CopyToClipboard_Data_String.Click += new System.EventHandler(this.CMenu_Packet_CopyToClipboard_Data_String_Click);
            // 
            // CMenu_Packet_CopyToClipboard_Data_String_NewLineOn
            // 
            this.CMenu_Packet_CopyToClipboard_Data_String_NewLineOn.Name = "CMenu_Packet_CopyToClipboard_Data_String_NewLineOn";
            this.CMenu_Packet_CopyToClipboard_Data_String_NewLineOn.Size = new System.Drawing.Size(196, 22);
            this.CMenu_Packet_CopyToClipboard_Data_String_NewLineOn.Tag = "1";
            this.CMenu_Packet_CopyToClipboard_Data_String_NewLineOn.Text = "パケット間の改行あり";
            this.CMenu_Packet_CopyToClipboard_Data_String_NewLineOn.Click += new System.EventHandler(this.CMenu_Packet_CopyToClipboard_Data_String_Click);
            // 
            // CMenu_Packet_CopyToClipboard_Data_String_NewLineOff
            // 
            this.CMenu_Packet_CopyToClipboard_Data_String_NewLineOff.Name = "CMenu_Packet_CopyToClipboard_Data_String_NewLineOff";
            this.CMenu_Packet_CopyToClipboard_Data_String_NewLineOff.Size = new System.Drawing.Size(196, 22);
            this.CMenu_Packet_CopyToClipboard_Data_String_NewLineOff.Tag = "0";
            this.CMenu_Packet_CopyToClipboard_Data_String_NewLineOff.Text = "パケット間の改行なし";
            this.CMenu_Packet_CopyToClipboard_Data_String_NewLineOff.Click += new System.EventHandler(this.CMenu_Packet_CopyToClipboard_Data_String_Click);
            // 
            // CMenu_Packet_CopyToClipboard_Data_Hex
            // 
            this.CMenu_Packet_CopyToClipboard_Data_Hex.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOn,
            this.CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOff});
            this.CMenu_Packet_CopyToClipboard_Data_Hex.Name = "CMenu_Packet_CopyToClipboard_Data_Hex";
            this.CMenu_Packet_CopyToClipboard_Data_Hex.Size = new System.Drawing.Size(205, 22);
            this.CMenu_Packet_CopyToClipboard_Data_Hex.Tag = "0";
            this.CMenu_Packet_CopyToClipboard_Data_Hex.Text = "データ: 16進文字列";
            this.CMenu_Packet_CopyToClipboard_Data_Hex.Click += new System.EventHandler(this.CMenu_Packet_CopyToClipboard_Data_Hex_Click);
            // 
            // CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOn
            // 
            this.CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOn.Name = "CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOn";
            this.CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOn.Size = new System.Drawing.Size(196, 22);
            this.CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOn.Tag = "1";
            this.CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOn.Text = "パケット間の改行あり";
            this.CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOn.Click += new System.EventHandler(this.CMenu_Packet_CopyToClipboard_Data_Hex_Click);
            // 
            // CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOff
            // 
            this.CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOff.Name = "CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOff";
            this.CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOff.Size = new System.Drawing.Size(196, 22);
            this.CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOff.Tag = "0";
            this.CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOff.Text = "パケット間の改行なし";
            this.CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOff.Click += new System.EventHandler(this.CMenu_Packet_CopyToClipboard_Data_Hex_Click);
            // 
            // CMenu_Packet_CopyToClipboard_Data_Custom
            // 
            this.CMenu_Packet_CopyToClipboard_Data_Custom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOn,
            this.CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOff});
            this.CMenu_Packet_CopyToClipboard_Data_Custom.Name = "CMenu_Packet_CopyToClipboard_Data_Custom";
            this.CMenu_Packet_CopyToClipboard_Data_Custom.Size = new System.Drawing.Size(205, 22);
            this.CMenu_Packet_CopyToClipboard_Data_Custom.Tag = "0";
            this.CMenu_Packet_CopyToClipboard_Data_Custom.Text = "データ: カスタム文字列";
            this.CMenu_Packet_CopyToClipboard_Data_Custom.Click += new System.EventHandler(this.CMenu_Packet_CopyToClipboard_Data_Custom_Click);
            // 
            // CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOn
            // 
            this.CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOn.Name = "CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOn";
            this.CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOn.Size = new System.Drawing.Size(196, 22);
            this.CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOn.Tag = "1";
            this.CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOn.Text = "パケット間の改行あり";
            this.CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOn.Click += new System.EventHandler(this.CMenu_Packet_CopyToClipboard_Data_Custom_Click);
            // 
            // CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOff
            // 
            this.CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOff.Name = "CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOff";
            this.CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOff.Size = new System.Drawing.Size(196, 22);
            this.CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOff.Tag = "0";
            this.CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOff.Text = "パケット間の改行なし";
            this.CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOff.Click += new System.EventHandler(this.CMenu_Packet_CopyToClipboard_Data_Custom_Click);
            // 
            // CMenu_Packet_CopyToPacketList
            // 
            this.CMenu_Packet_CopyToPacketList.Name = "CMenu_Packet_CopyToPacketList";
            this.CMenu_Packet_CopyToPacketList.Size = new System.Drawing.Size(208, 22);
            this.CMenu_Packet_CopyToPacketList.Text = "パケットリストへコピー";
            this.CMenu_Packet_CopyToPacketList.Click += new System.EventHandler(this.CMenu_MultiData_CopyToPacketList_Click);
            // 
            // ViewInstanceImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.Split_Main);
            this.Controls.Add(this.Panel_ToolBar);
            this.Name = "ViewInstanceImpl";
            this.Size = new System.Drawing.Size(957, 511);
            this.Panel_ToolBar.ResumeLayout(false);
            this.GBox_CustomFormat.ResumeLayout(false);
            this.GBox_CustomFormat.PerformLayout();
            this.GBox_CharCode.ResumeLayout(false);
            this.GBox_PreviewByteNum.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_PreviewDataSize)).EndInit();
            this.Split_Main.Panel1.ResumeLayout(false);
            this.Split_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Split_Main)).EndInit();
            this.Split_Main.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.CMenu_Packet.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public ViewInstanceImpl() : base()
        {
            InitializeComponent();
        }

        public ViewInstanceImpl(ViewManager viewm, ViewClass viewd, ViewProperty viewp, Guid id) : base(viewm, viewd, viewp, id)
        {
            prop_ = Property as ViewPropertyImpl;

            Disposed += OnDisposed;

            InitializeComponent();
            InitializeContextMenu();
            InitializeCharCodeType();

            /* --- プロパティをUIに反映 --- */
            BuildPacketViewHeader();
            Num_PreviewDataSize.Value = prop_.PreviewDataSize.Value;
            CBox_CharCode.SelectedItem = prop_.CharCode.Value;
            TBox_CustomFormat.Text = prop_.CustomFormat.Value;
            Menu_ExtView_SelectPacketCount.Checked = prop_.ExtViewSelectPacketCount.Value;
            Menu_ExtView_SelectTotalSize.Checked = prop_.ExtViewSelectTotalSize.Value;
            Menu_ExtView_FirstPacketInfo.Checked = prop_.ExtViewFirstPacketInfo.Value;
            Menu_ExtView_LastPacketInfo.Checked = prop_.ExtViewLastPacketInfo.Value;
            Menu_ExtView_SelectDelta.Checked = prop_.ExtViewSelectDelta.Value;
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
            var language = ConfigManager.Language.PacketView.Packet;

            CMenu_Packet_CopyToClipboard.Text = language.CMenu_Packet_CopyToClipboard.Value;
            CMenu_Packet_CopyToClipboard_AllInfo_Csv.Text = language.CMenu_Packet_CopyToClipboard_AllInfo_Csv.Value;
            CMenu_Packet_CopyToClipboard_Data_String.Text = language.CMenu_Packet_CopyToClipboard_DataString.Value;
            CMenu_Packet_CopyToClipboard_Data_String_NewLineOn.Text = language.CMenu_Packet_CopyToClipboard_NewLineOn.Value;
            CMenu_Packet_CopyToClipboard_Data_String_NewLineOff.Text = language.CMenu_Packet_CopyToClipboard_NewLineOff.Value;
            CMenu_Packet_CopyToClipboard_Data_Hex.Text = language.CMenu_Packet_CopyToClipboard_DataHex.Value;
            CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOn.Text = language.CMenu_Packet_CopyToClipboard_NewLineOn.Value;
            CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOff.Text = language.CMenu_Packet_CopyToClipboard_NewLineOff.Value;
            CMenu_Packet_CopyToClipboard_Data_Custom.Text = language.CMenu_Packet_CopyToClipboard_DataCustom.Value;
            CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOn.Text = language.CMenu_Packet_CopyToClipboard_NewLineOn.Value;
            CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOff.Text = language.CMenu_Packet_CopyToClipboard_NewLineOff.Value;
            CMenu_Packet_CopyToPacketList.Text = language.CMenu_Packet_CopyToPacketList.Value;
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
            var packet = (DataPacketObject)null;

            /* 選択中のパケットにデータパケットが存在するかチェック */
            if (LView_Main.SelectedIndices.Count > 0) {
                foreach (int index in LView_Main.SelectedIndices) {
                    packet = LView_Main.ItemAt(index).Tag as DataPacketObject;
                    if (packet == null) continue;

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
                CMenu_Packet_CopyToClipboard_Data_String_NewLineOff.Visible = (status == PacketSelectStatus.MultiSelect);
                CMenu_Packet_CopyToClipboard_Data_String_NewLineOn.Visible = (status == PacketSelectStatus.MultiSelect);
                CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOff.Visible = (status == PacketSelectStatus.MultiSelect);
                CMenu_Packet_CopyToClipboard_Data_Hex_NewLineOn.Visible = (status == PacketSelectStatus.MultiSelect);
                CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOff.Visible = (status == PacketSelectStatus.MultiSelect);
                CMenu_Packet_CopyToClipboard_Data_Custom_NewLineOn.Visible = (status == PacketSelectStatus.MultiSelect);
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

        private void UpdateSelectStatus()
        {
            UpdateBinEditBox();
            UpdateExtView();
        }

        private void UpdateBinEditBox()
        {
            var item = LView_Main.FocusedItem;

            SetCurrentPacketStatus((item != null) ? (item.Tag as PacketObject) : (null));
        }

        private void UpdateExtView()
        {
            var indices = LView_Main.SelectedIndices;
            var index_first = 0;
            var index_last = 0;
            var packet_first = (PacketObject)null;
            var packet_last = (PacketObject)null;

            if (indices.Count > 0) {
                index_first = indices[0];
                index_last = indices[indices.Count - 1];
                packet_first = LView_Main.ItemAt(index_first).Tag as PacketObject;
                packet_last = LView_Main.ItemAt(index_last).Tag as PacketObject;
            }

            /* 選択パケット数 */
            if (ExtViewItem_SelectPacketCount != null) {
                ExtViewItem_SelectPacketCount.SubItems[1].Text = indices.Count.ToString();
            }

            /* 選択パケットサイズ */
            if (ExtViewItem_SelectTotalSize != null) {
                var total_size = (ulong)0;
                var packet_d = (DataPacketObject)null;

                foreach (int index in indices) {
                    packet_d = LView_Main.ItemAt(index).Tag as DataPacketObject;
                    if (packet_d == null)continue;
                    total_size += (ulong)packet_d.GetDataSize();
                }

                ExtViewItem_SelectTotalSize.SubItems[1].Text = String.Format("{0} byte", total_size);
            }

            /* 選択パケット(最初)の情報 */
            if (ExtViewItem_FirstPacketInfo != null) {
                if (packet_first != null) {
                    ExtViewItem_FirstPacketInfo.SubItems[1].Text = String.Format("{0} - No.{1}", packet_first.MakeTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff"), index_first + 1);
                } else {
                    ExtViewItem_FirstPacketInfo.SubItems[1].Text = "";
                }
            }

            /* 選択パケット(最後)の情報 */
            if (ExtViewItem_LastPacketInfo != null) {
                if (packet_last != null) {
                    ExtViewItem_LastPacketInfo.SubItems[1].Text = String.Format("{0} - No.{1}", packet_last.MakeTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff"), index_last + 1);
                } else {
                    ExtViewItem_LastPacketInfo.SubItems[1].Text = "";
                }
            }

            /* 選択パケット(最後 - 最初)の差分時間 */
            if (ExtViewItem_SelectDelta != null) {
                if ((packet_first != null) && (packet_last != null)) {
                    ExtViewItem_SelectDelta.SubItems[1].Text = String.Format("{0} ms", (uint)((packet_last.MakeTime - packet_first.MakeTime).TotalMilliseconds));
                } else {
                    ExtViewItem_SelectDelta.SubItems[1].Text = "";
                }
            }
        }

#if false
        private void UpdateSelectInformation()
        {
            /* 全消去 */
            RTBox_Info.Clear();

            /* 複数選択以外は情報無し */
            if (LView_Main.SelectedIndices.Count <= 1)return;

            var indices = LView_Main.SelectedIndices;
            var index_first = indices[0];
            var index_last = indices[indices.Count - 1];
            var packet_first = LView_Main.ItemAt(index_first).Tag as PacketObject;
            var packet_last = LView_Main.ItemAt(index_last).Tag as PacketObject;
            var delta = packet_last.MakeTime - packet_first.MakeTime;

            /* 選択パケット数 */
            RTBox_Info.AppendText(String.Format("<Select Information>\n", indices.Count));
            RTBox_Info.AppendText(String.Format("  Select Number       : {0}\n", indices.Count));
            RTBox_Info.AppendText(
                String.Format(
                    "  Delta(Last - First) : {0} ({1}ms)\n",
                    delta.ToString("hh\\:mm\\:ss\\.fff"),
                    (uint)delta.TotalMilliseconds));
            RTBox_Info.AppendText("\n");

            /* 最初のパケット情報 */
            RTBox_Info.AppendText(String.Format("<Information on the first packet selected>\n"));
            RTBox_Info.AppendText(String.Format("  Packet No.       : {0}\n", index_first + 1));
            RTBox_Info.AppendText(String.Format("  Date Time(Local) : {0}\n", packet_first.MakeTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff")));
            RTBox_Info.AppendText("\n");

            /* 最終のパケット情報 */
            RTBox_Info.AppendText(String.Format("<Information on the last selected packet>\n"));
            RTBox_Info.AppendText(String.Format("  Packet No.       : {0}\n", index_last + 1));
            RTBox_Info.AppendText(String.Format("  Date Time(Local) : {0}\n", packet_last.MakeTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff")));
            RTBox_Info.AppendText("\n");
        }
#endif

        private void SetCurrentPacketStatus(PacketObject packet)
        {
            if (packet != null) {
                /* データパケットのときはデータ内容を表示する */
                if (packet.Attribute == PacketAttribute.Data) {
                    BBox_Main.SetData((packet as DataPacketObject).GetData());
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

        private void BuildPacketViewHeader()
        {
            LView_Main.BeginUpdate();
            {
                /* 先にデータをすべて削除してからヘッダーを削除する */
                LView_Main.ItemClear();
                LView_Main.Columns.Clear();

                /* メインヘッダー */
                var column_main = new ColumnHeader();

                column_main.Text = "No.";
                column_main.Width = 50;

                LView_Main.Columns.Add(column_main);

                foreach (var config in prop_.ColumnList.Value) {
                    var column_sub = new ColumnHeader();

                    column_sub.Text = GetListViewHeaderName(config.Type);
                    column_sub.Width = (int)config.Width;
                    column_sub.Tag = config.Type;

                    LView_Main.Columns.Add(column_sub);
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
                    ExtViewItem_SelectPacketCount = new ListViewItem();
                    ExtViewItem_SelectPacketCount.Text = "選択パケット数";
                    ExtViewItem_SelectPacketCount.SubItems.Add("");
                    ExtViewItem_SelectPacketCount = LView_ExtInfo.Items.Add(ExtViewItem_SelectPacketCount);
                }

                /* 選択パケットサイズ */
                if (Menu_ExtView_SelectTotalSize.Checked) {
                    ExtViewItem_SelectTotalSize = new ListViewItem();
                    ExtViewItem_SelectTotalSize.Text = "選択データパケット合計サイズ";
                    ExtViewItem_SelectTotalSize.SubItems.Add("");
                    ExtViewItem_SelectTotalSize = LView_ExtInfo.Items.Add(ExtViewItem_SelectTotalSize);
                }

                /* 選択パケット(最初)の情報 */
                if (Menu_ExtView_FirstPacketInfo.Checked) {
                    ExtViewItem_FirstPacketInfo = new ListViewItem();
                    ExtViewItem_FirstPacketInfo.Text = "選択パケット(First)";
                    ExtViewItem_FirstPacketInfo.SubItems.Add("");
                    ExtViewItem_FirstPacketInfo = LView_ExtInfo.Items.Add(ExtViewItem_FirstPacketInfo);
                }

                /* 選択パケット(最後)の情報 */
                if (Menu_ExtView_LastPacketInfo.Checked) {
                    ExtViewItem_LastPacketInfo = new ListViewItem();
                    ExtViewItem_LastPacketInfo.Text = "選択パケット(Last)";
                    ExtViewItem_LastPacketInfo.SubItems.Add("");
                    ExtViewItem_LastPacketInfo = LView_ExtInfo.Items.Add(ExtViewItem_LastPacketInfo);
                }

                /* 選択パケット(最後 - 最初)の差分時間 */
                if (Menu_ExtView_SelectDelta.Checked) {
                    ExtViewItem_SelectDelta = new ListViewItem();
                    ExtViewItem_SelectDelta.Text = "差分時間(Last - First)";
                    ExtViewItem_SelectDelta.SubItems.Add("");
                    ExtViewItem_SelectDelta = LView_ExtInfo.Items.Add(ExtViewItem_SelectDelta);
                }
            }
            LView_ExtInfo.EndUpdate();

            UpdateExtView();
        }

        private string GetListViewHeaderName(ColumnType type)
        {
            switch (type) {
                case ColumnType.Alias:                  return ("エイリアス");
                case ColumnType.Datetime_UTC:           return ("時間(UTC)");
                case ColumnType.Datetime_Local:         return ("時間(ローカル)");
                case ColumnType.Mark:                   return ("マーク");
                case ColumnType.Information:            return ("情報");
                case ColumnType.Source:                 return ("送信元");
                case ColumnType.Destination:            return ("送信先");
                case ColumnType.DataLength:             return ("データ長");
                case ColumnType.DataPreviewBinary:      return ("データ:バイナリ");
                case ColumnType.DataPreviewText:        return ("データ:文字列");
                case ColumnType.DataPreviewCustom:      return ("データ:カスタム");
                default:                                return ("Unknown");
            }
        }

        private string DataPacketToCustomText(DataPacketObject packet)
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

        private ListViewItem PacketToListViewItem(PacketObject packet)
        {
            var item = new ListViewItem();

            /* メインアイテム */
            item.Text = (LView_Main.ItemCount + list_items_temp_.Count + 1).ToString();
            item.Tag = packet;

            /* サブサイテム */
            PacketToListViewItem_SubUpdate(item, packet);

            return (item);
        }

        private void PacketToListViewItem_SubUpdate(ListViewItem item, PacketObject packet)
        {
            switch (packet.Attribute) {
                case PacketAttribute.Message:
                    PacketToListViewItem_Message(item, packet as MessagePacketObject);
                    break;

                case PacketAttribute.Data:
                    PacketToListViewItem_Data(item, packet as DataPacketObject);
                    break;

                default:
                    break;
            }
        }

        private void PacketToListViewItem_Message(ListViewItem item, MessagePacketObject packet)
        {
            foreach (var config in prop_.ColumnList.Value) {
                switch (config.Type) {
                    case ColumnType.Alias:
                        item.SubItems.Add(packet.Alias);
                        break;

                    case ColumnType.Datetime_UTC:
                        item.SubItems.Add(packet.MakeTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        break;

                    case ColumnType.Datetime_Local:
                        item.SubItems.Add(packet.MakeTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        break;

                    case ColumnType.Information:
                        item.SubItems.Add(packet.Information);
                        break;

                    case ColumnType.DataPreviewBinary:
                        item.SubItems.Add(packet.Message);
                        break;

                    case ColumnType.DataPreviewText:
                        item.SubItems.Add(packet.Message);
                        break;

                    default:
                        item.SubItems.Add("");
                        break;
                }
            }

            /* 背景色 */
            item.BackColor = Color.LightGoldenrodYellow;
        }

        private void PacketToListViewItem_Data(ListViewItem item, DataPacketObject packet)
        {
            foreach (var config in prop_.ColumnList.Value) {
                switch (config.Type) {
                    case ColumnType.Alias:
                        item.SubItems.Add(packet.Alias);
                        break;

                    case ColumnType.Datetime_UTC:
                        item.SubItems.Add(packet.MakeTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        break;

                    case ColumnType.Datetime_Local:
                        item.SubItems.Add(packet.MakeTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        break;

                    case ColumnType.Information:
                        item.SubItems.Add(packet.Information);
                        break;

                    case ColumnType.Source:
                        item.SubItems.Add(packet.Source);
                        break;

                    case ColumnType.Destination:
                        item.SubItems.Add(packet.Destination);
                        break;

                    case ColumnType.DataLength:
                        item.SubItems.Add(packet.GetDataSize().ToString());
                        break;

                    case ColumnType.DataPreviewBinary:
                        item.SubItems.Add(packet.GetHexText(0, (int)prop_.PreviewDataSize.Value, " "));
                        break;

                    case ColumnType.DataPreviewText:
                        item.SubItems.Add(encoder_.GetString(packet.GetBytes(0, (int)prop_.PreviewDataSize.Value).ToArray()));
                        break;

                    case ColumnType.DataPreviewCustom:
                        item.SubItems.Add(DataPacketToCustomText(packet));
                        break;

                    default:
                        break;
                }
            }

            /* 背景色 */
            item.BackColor = (packet.Direction == PacketDirection.Recv)
                           ? (Color.LightGreen)
                           : (Color.LightPink);
        }

        private string GetSelectPacketsData_CsvText()
        {
            var str = new StringBuilder(0xFFFF);
            var packet = (PacketObject)null;

            /* ヘッダー */
            str.AppendLine(PacketObject.GetCsvHeaderString());

            /* データ */
            foreach (int index in LView_Main.SelectedIndices) {
                /* データパケットを取得 */
                packet = LView_Main.ItemAt(index).Tag as PacketObject;
                if (packet == null)continue;

                /* CSV文字列として追加 */
                str.AppendLine(packet.GetCsvDataString());
            }

            return (str.ToString());
        }

        private string GetSelectPacketsData_String(bool new_line)
        {
            var str = new StringBuilder(0xFFFF);
            var packet = (DataPacketObject)null;

            foreach (int index in LView_Main.SelectedIndices) {
                /* データパケットを取得 */
                packet = LView_Main.ItemAt(index).Tag as DataPacketObject;
                if (packet == null)continue;

                /* 文字列として追加 */
                str.Append(encoder_.GetString(packet.GetData()));

                /* 改行コード挿入 */
                if (new_line) {
                    str.AppendLine();
                }
            }

            return (str.ToString());
        }

        private string GetSelectPacketsData_HexString(bool new_line)
        {
            var str = new StringBuilder(0xFFFF);
            var packet = (DataPacketObject)null;

            foreach (int index in LView_Main.SelectedIndices) {
                /* データパケットを取得 */
                packet = LView_Main.ItemAt(index).Tag as DataPacketObject;
                if (packet == null) continue;

                /* 16進文字列として追加 */
                str.Append(packet.GetHexText());

                /* 改行コード挿入 */
                if (new_line) {
                    str.AppendLine();
                }
            }

            return (str.ToString());
        }

        private string GetSelectPacketsData_CustomString(bool new_line)
        {
            var str = new StringBuilder(0xFFFF);
            var packet = (DataPacketObject)null;

            foreach (int index in LView_Main.SelectedIndices) {
                /* データパケットを取得 */
                packet = LView_Main.ItemAt(index).Tag as DataPacketObject;
                if (packet == null) continue;

                /* カスタム文字列として追加 */
                str.Append(DataPacketToCustomText(packet));

                /* 改行コード挿入 */
                if (new_line) {
                    str.AppendLine();
                }
            }

            return (str.ToString());
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
        }

        protected override void OnClearPacket()
        {
            LView_Main.ItemClear();
            BBox_Main.DataClear();
        }

        protected override void OnDrawPacketBegin(bool auto_scroll)
        {
            /* ちらつき防止用の一時バッファ */
            list_items_temp_ = new List<ListViewItem>();

            /* リストビューの描画開始 */
            LView_Main.BeginUpdate();
        }

        protected override void OnDrawPacketEnd(bool auto_scroll)
        {
            /* 一時リストをリストビューに追加 */
            LView_Main.AddItem(list_items_temp_);
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
            list_items_temp_.Add(PacketToListViewItem(packet));
        }

        private void LView_Main_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            /* 項目編集画面を表示 */
            var dialog = new Forms.ColumnHeaderEditForm(
                                    (IEnumerable<ColumnType>)Enum.GetValues(typeof(ColumnType)),
                                    prop_.ColumnList.Value.Select(obj => obj.Type));

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
            var menu = sender as ToolStripMenuItem;

            if (menu == null)return;

            menu.Checked = !menu.Checked;

            BuildExtView();
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

        private void CMenu_MultiData_CopyToPacketList_Click(object sender, EventArgs e)
        {
            foreach (int index in LView_Main.SelectedIndices) {
                var list_item = LView_Main.ItemAt(index);

                if (list_item == null)continue;

                var packet = list_item.Tag as DataPacketObject;

                if (packet == null)continue;

                FormUiManager.AddPacket("", packet.GetData(), (uint)packet.GetDataSize() * 8);
            }
        }

        private void CMenu_Packet_CopyToClipboard_AllInfo_Csv_Click(object sender, EventArgs e)
        {
            /* クリップボードへセット */
            Clipboard.SetText(
                GetSelectPacketsData_CsvText(),
                TextDataFormat.Text);
        }

        private void CMenu_Packet_CopyToClipboard_Data_String_Click(object sender, EventArgs e)
        {
            var menu = sender as ToolStripMenuItem;

            if (menu == null)return;

            /* クリップボードへセット */
            Clipboard.SetText(
                GetSelectPacketsData_String((menu.Tag is string) && ((menu.Tag as string) == "1")),
                TextDataFormat.Text);
        }

        private void CMenu_Packet_CopyToClipboard_Data_Hex_Click(object sender, EventArgs e)
        {
            var menu = sender as ToolStripMenuItem;

            if (menu == null)return;

            /* クリップボードへセット */
            Clipboard.SetText(
                GetSelectPacketsData_HexString((menu.Tag is string) && ((menu.Tag as string) == "1")),
                TextDataFormat.Text);
        }

        private void CMenu_Packet_CopyToClipboard_Data_Custom_Click(object sender, EventArgs e)
        {
            var menu = sender as ToolStripMenuItem;

            if (menu == null)return;

            /* クリップボードへセット */
            Clipboard.SetText(
                GetSelectPacketsData_CustomString((menu.Tag is string) && ((menu.Tag as string) == "1")),
                TextDataFormat.Text);
        }
    }
}
