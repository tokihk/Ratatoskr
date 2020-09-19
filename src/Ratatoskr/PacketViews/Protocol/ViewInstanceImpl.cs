using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Forms.Controls;
using Ratatoskr.PacketViews.Protocol.Configs;
using Ratatoskr.Protocol;
using RtsCore.Framework.PacketView;
using RtsCore.Packet;
using RtsCore.Protocol;

namespace Ratatoskr.PacketViews.Protocol
{
    internal sealed class PacketViewInstanceImpl : PacketViewInstance
    {
        private const ulong ITEM_NO_MIN = 1;
        private const ulong ITEM_NO_MAX = ulong.MaxValue;

		private readonly double[] TIME_CHART_AXIS_MAG_LIST = { 5.00, 2.00, 1.25, 1.00, 0.50, 0.20, 0.10, 0.01 };

		private readonly Padding MARGIN_TIME_CHART_BODY    = new Padding(10,  10, 10, 10);

		private const int TIME_CHART_MEASURE_HEIGHT            = 30;
		private const int TIME_CHART_MEASURE_LABEL_HEIGHT      = 25;
		private const int TIME_CHART_MEASURE_MAJOR_LINE_HEIGHT = 5;
		private const int TIME_CHART_MEASURE_MAJOR_LINE_STEP   = 20;

		private const int TIME_CHART_MEASURE_MINOR_LINE_HEIGHT = 2;
		private const int TIME_CHART_MEASURE_MINOR_LINE_STEP   = 2;

		private readonly Font  TIME_CHART_SCALE_FONT  = new Font("MS Gothic", 8);
		private readonly Pen   TIME_CHART_SCALE_PEN   = Pens.Gray;
		private readonly Brush TIME_CHART_SCALE_BRUSH = Brushes.Gray;

		private readonly Font  TIME_CHART_DATA_FONT      = new Font("Arial", 10);
		private          Pen   TIME_CHART_DATA_PEN_BASE  = new Pen(Brushes.Black, 1);
		private readonly Pen   TIME_CHART_DATA_PEN_EVENT = new Pen(Brushes.Black, 2);
		private readonly Brush TIME_CHART_DATA_BRUSH     = Brushes.Black;

		private const int TIME_CHART_SCALE_TEXT_WIDTH = 100;
		private const int TIME_CHART_SCALE_STEP = 5;

		private const int TIME_CHART_CHART_LABEL_WIDTH = 120;
		private const int TIME_CHART_CHART_HEIGHT      = 20;

		private const int TIME_CHART_DATA_DOT_SIZE = 6;


		private class AxisMagListItem
		{
			public AxisMagListItem(double value)
			{
				Value = value;
			}

			public double Value { get; }

			public override bool Equals(object obj)
			{
				if (obj is double obj_d) {
					return (obj_d == Value);
				}

				return base.Equals(obj);
			}

			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			public override string ToString()
			{
				return (Value.ToString("N2"));
			}
		}

		private class EventListViewItem : ListViewItem
        {
            public EventListViewItem(ulong no, string alias, ProtocolDecodeEvent prde)
            {
                No = no;
				Alias = alias;
				DecodeEvent = prde;
            }

            public ulong               No          { get; }
			public string              Alias       { get; }
            public ProtocolDecodeEvent DecodeEvent { get; }
        }

		private class DrawTimeChartParam
		{
			public Rectangle DrawRect_MeasureTop;
			public Rectangle DrawRect_MeasureBottom;
			public Rectangle DrawRect_ChartData;

			public string                Alias;
			public ProtocolDecodeChannel Channel;

			public DateTime FirstEventTime;
			public DateTime LastEventTime;

			public DateTime ViewFirstEventTime;
			public DateTime ViewLastEventTime;
			public int      ViewScaleIndex;
			public int      ViewScaleSize;

			public int      ViewVerticalOffset;

			public double   OneScaleTimeMsec;
			public double   OnePixelTimeWidth;

			public StringFormat ScaleStringFormat     = new StringFormat();
			public StringFormat DataLabelStringFormat = new StringFormat();
		}

		private PacketViewPropertyImpl prop_;

        private Guid decoder_id_ = Guid.Empty;

		private ProtocolDecoderClass                        prdc_ = null;
        private Dictionary<string, ProtocolDecoderInstance> prdi_map_ = new Dictionary<string, ProtocolDecoderInstance>();

        private List<EventListViewItem> list_items_temp_;

        private ulong next_item_no_ = 1;

		private DrawTimeChartParam draw_chart_param_ = new DrawTimeChartParam();

		private double draw_chart_axis_x_mag_ = 1.0f;
		private double draw_chart_axis_y_mag_ = 1.0f;
		private double scroll_pixel_time_ = 1.0f;

		private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox GBox_Decoder;
        private System.Windows.Forms.ComboBox CBox_DecoderType;
        private System.Windows.Forms.SplitContainer Splitter_Main;
        private System.Windows.Forms.SplitContainer Splitter_Sub;
        private System.Windows.Forms.Panel Panel_FrameDetails;
        private System.Windows.Forms.Label Label_EventDetailsSearch;
        private System.Windows.Forms.TextBox TBox_EventDetailsSearch;
        private System.Windows.Forms.TabControl TabControl_Chart;
        private System.Windows.Forms.TabPage TabPage_TimeChart;
        private System.Windows.Forms.TabPage TabPage_FrameErrorRate;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_FrameErrorRate;
        private System.Windows.Forms.FlowLayoutPanel Panel_FrameErrorRate;
        private RtsCore.Framework.Controls.ListViewEx LView_EventList;
        private System.Windows.Forms.Panel Panel_FrameList;
        private System.Windows.Forms.Label Label_EventListSearch;
        private System.Windows.Forms.TextBox TBox_EventListSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox PBox_EventTimeChart;
        private System.Windows.Forms.HScrollBar HScroll_TimeChart;
        private System.Windows.Forms.VScrollBar VScroll_TimeChart;
		private GroupBox groupBox1;
		private ComboBox CBox_ChartAxisMag_X;
		private Label label1;
		private ComboBox CBox_ChartAxisMag_Y;
		private Label label3;
		private TreeListView TLView_EventDetails;

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CBox_ChartAxisMag_Y = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CBox_ChartAxisMag_X = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GBox_Decoder = new System.Windows.Forms.GroupBox();
            this.CBox_DecoderType = new System.Windows.Forms.ComboBox();
            this.Splitter_Main = new System.Windows.Forms.SplitContainer();
            this.Splitter_Sub = new System.Windows.Forms.SplitContainer();
            this.LView_EventList = new RtsCore.Framework.Controls.ListViewEx();
            this.Panel_FrameList = new System.Windows.Forms.Panel();
            this.Label_EventListSearch = new System.Windows.Forms.Label();
            this.TBox_EventListSearch = new System.Windows.Forms.TextBox();
            this.TLView_EventDetails = new Ratatoskr.Forms.Controls.TreeListView();
            this.Panel_FrameDetails = new System.Windows.Forms.Panel();
            this.Label_EventDetailsSearch = new System.Windows.Forms.Label();
            this.TBox_EventDetailsSearch = new System.Windows.Forms.TextBox();
            this.TabControl_Chart = new System.Windows.Forms.TabControl();
            this.TabPage_TimeChart = new System.Windows.Forms.TabPage();
            this.PBox_EventTimeChart = new System.Windows.Forms.PictureBox();
            this.HScroll_TimeChart = new System.Windows.Forms.HScrollBar();
            this.VScroll_TimeChart = new System.Windows.Forms.VScrollBar();
            this.TabPage_FrameErrorRate = new System.Windows.Forms.TabPage();
            this.Chart_FrameErrorRate = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Panel_FrameErrorRate = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.GBox_Decoder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Splitter_Main)).BeginInit();
            this.Splitter_Main.Panel1.SuspendLayout();
            this.Splitter_Main.Panel2.SuspendLayout();
            this.Splitter_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Splitter_Sub)).BeginInit();
            this.Splitter_Sub.Panel1.SuspendLayout();
            this.Splitter_Sub.Panel2.SuspendLayout();
            this.Splitter_Sub.SuspendLayout();
            this.Panel_FrameList.SuspendLayout();
            this.Panel_FrameDetails.SuspendLayout();
            this.TabControl_Chart.SuspendLayout();
            this.TabPage_TimeChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBox_EventTimeChart)).BeginInit();
            this.TabPage_FrameErrorRate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_FrameErrorRate)).BeginInit();
            this.Panel_FrameErrorRate.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.GBox_Decoder);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1408, 66);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CBox_ChartAxisMag_Y);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.CBox_ChartAxisMag_X);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(279, 5);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(320, 51);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chart/Graph Axis Magnification";
            // 
            // CBox_ChartAxisMag_Y
            // 
            this.CBox_ChartAxisMag_Y.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_ChartAxisMag_Y.FormattingEnabled = true;
            this.CBox_ChartAxisMag_Y.Location = new System.Drawing.Point(199, 18);
            this.CBox_ChartAxisMag_Y.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CBox_ChartAxisMag_Y.Name = "CBox_ChartAxisMag_Y";
            this.CBox_ChartAxisMag_Y.Size = new System.Drawing.Size(105, 23);
            this.CBox_ChartAxisMag_Y.TabIndex = 3;
            this.CBox_ChartAxisMag_Y.SelectedIndexChanged += new System.EventHandler(this.CBox_ChartAxisMag_Y_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(167, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Y :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CBox_ChartAxisMag_X
            // 
            this.CBox_ChartAxisMag_X.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_ChartAxisMag_X.FormattingEnabled = true;
            this.CBox_ChartAxisMag_X.Location = new System.Drawing.Point(40, 18);
            this.CBox_ChartAxisMag_X.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CBox_ChartAxisMag_X.Name = "CBox_ChartAxisMag_X";
            this.CBox_ChartAxisMag_X.Size = new System.Drawing.Size(105, 23);
            this.CBox_ChartAxisMag_X.TabIndex = 1;
            this.CBox_ChartAxisMag_X.SelectedIndexChanged += new System.EventHandler(this.CBox_ChartAxisMag_X_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "X :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GBox_Decoder
            // 
            this.GBox_Decoder.Controls.Add(this.CBox_DecoderType);
            this.GBox_Decoder.Location = new System.Drawing.Point(4, 4);
            this.GBox_Decoder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GBox_Decoder.Name = "GBox_Decoder";
            this.GBox_Decoder.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GBox_Decoder.Size = new System.Drawing.Size(267, 52);
            this.GBox_Decoder.TabIndex = 1;
            this.GBox_Decoder.TabStop = false;
            this.GBox_Decoder.Text = "Decoder";
            // 
            // CBox_DecoderType
            // 
            this.CBox_DecoderType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_DecoderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_DecoderType.FormattingEnabled = true;
            this.CBox_DecoderType.Location = new System.Drawing.Point(4, 19);
            this.CBox_DecoderType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CBox_DecoderType.Name = "CBox_DecoderType";
            this.CBox_DecoderType.Size = new System.Drawing.Size(259, 23);
            this.CBox_DecoderType.TabIndex = 0;
            this.CBox_DecoderType.SelectedIndexChanged += new System.EventHandler(this.CBox_DecoderType_SelectedIndexChanged);
            // 
            // Splitter_Main
            // 
            this.Splitter_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Splitter_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.Splitter_Main.Location = new System.Drawing.Point(0, 66);
            this.Splitter_Main.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Splitter_Main.Name = "Splitter_Main";
            this.Splitter_Main.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Splitter_Main.Panel1
            // 
            this.Splitter_Main.Panel1.Controls.Add(this.Splitter_Sub);
            // 
            // Splitter_Main.Panel2
            // 
            this.Splitter_Main.Panel2.Controls.Add(this.TabControl_Chart);
            this.Splitter_Main.Size = new System.Drawing.Size(1408, 732);
            this.Splitter_Main.SplitterDistance = 525;
            this.Splitter_Main.SplitterWidth = 5;
            this.Splitter_Main.TabIndex = 2;
            // 
            // Splitter_Sub
            // 
            this.Splitter_Sub.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Splitter_Sub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Splitter_Sub.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.Splitter_Sub.Location = new System.Drawing.Point(0, 0);
            this.Splitter_Sub.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Splitter_Sub.Name = "Splitter_Sub";
            // 
            // Splitter_Sub.Panel1
            // 
            this.Splitter_Sub.Panel1.Controls.Add(this.LView_EventList);
            this.Splitter_Sub.Panel1.Controls.Add(this.Panel_FrameList);
            // 
            // Splitter_Sub.Panel2
            // 
            this.Splitter_Sub.Panel2.Controls.Add(this.TLView_EventDetails);
            this.Splitter_Sub.Panel2.Controls.Add(this.Panel_FrameDetails);
            this.Splitter_Sub.Size = new System.Drawing.Size(1408, 525);
            this.Splitter_Sub.SplitterDistance = 911;
            this.Splitter_Sub.SplitterWidth = 5;
            this.Splitter_Sub.TabIndex = 0;
            // 
            // LView_EventList
            // 
            this.LView_EventList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LView_EventList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LView_EventList.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LView_EventList.FullRowSelect = true;
            this.LView_EventList.GridLines = true;
            this.LView_EventList.HideSelection = false;
            this.LView_EventList.ItemCountMax = 99999999;
            this.LView_EventList.Location = new System.Drawing.Point(0, 30);
            this.LView_EventList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LView_EventList.Name = "LView_EventList";
            this.LView_EventList.ReadOnly = false;
            this.LView_EventList.Size = new System.Drawing.Size(909, 493);
            this.LView_EventList.TabIndex = 1;
            this.LView_EventList.UseCompatibleStateImageBehavior = false;
            this.LView_EventList.View = System.Windows.Forms.View.Details;
            this.LView_EventList.VirtualMode = true;
            this.LView_EventList.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.LView_EventList_RetrieveVirtualItem);
            this.LView_EventList.SelectedIndexChanged += new System.EventHandler(this.LView_EventList_SelectedIndexChanged);
            // 
            // Panel_FrameList
            // 
            this.Panel_FrameList.AutoSize = true;
            this.Panel_FrameList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Panel_FrameList.Controls.Add(this.Label_EventListSearch);
            this.Panel_FrameList.Controls.Add(this.TBox_EventListSearch);
            this.Panel_FrameList.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_FrameList.Location = new System.Drawing.Point(0, 0);
            this.Panel_FrameList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Panel_FrameList.Name = "Panel_FrameList";
            this.Panel_FrameList.Size = new System.Drawing.Size(909, 30);
            this.Panel_FrameList.TabIndex = 0;
            // 
            // Label_EventListSearch
            // 
            this.Label_EventListSearch.AutoSize = true;
            this.Label_EventListSearch.Location = new System.Drawing.Point(4, 9);
            this.Label_EventListSearch.Margin = new System.Windows.Forms.Padding(4, 9, 4, 0);
            this.Label_EventListSearch.Name = "Label_EventListSearch";
            this.Label_EventListSearch.Size = new System.Drawing.Size(52, 15);
            this.Label_EventListSearch.TabIndex = 1;
            this.Label_EventListSearch.Text = "Search";
            this.Label_EventListSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TBox_EventListSearch
            // 
            this.TBox_EventListSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBox_EventListSearch.Location = new System.Drawing.Point(65, 4);
            this.TBox_EventListSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TBox_EventListSearch.Name = "TBox_EventListSearch";
            this.TBox_EventListSearch.Size = new System.Drawing.Size(838, 22);
            this.TBox_EventListSearch.TabIndex = 2;
            // 
            // TLView_EventDetails
            // 
            this.TLView_EventDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TLView_EventDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLView_EventDetails.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TLView_EventDetails.FullRowSelect = true;
            this.TLView_EventDetails.GridLines = true;
            this.TLView_EventDetails.HideSelection = false;
            this.TLView_EventDetails.Location = new System.Drawing.Point(0, 30);
            this.TLView_EventDetails.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TLView_EventDetails.Name = "TLView_EventDetails";
            this.TLView_EventDetails.OwnerDraw = true;
            this.TLView_EventDetails.Size = new System.Drawing.Size(490, 493);
            this.TLView_EventDetails.TabIndex = 3;
            this.TLView_EventDetails.UseCompatibleStateImageBehavior = false;
            this.TLView_EventDetails.View = System.Windows.Forms.View.Details;
            // 
            // Panel_FrameDetails
            // 
            this.Panel_FrameDetails.AutoSize = true;
            this.Panel_FrameDetails.Controls.Add(this.Label_EventDetailsSearch);
            this.Panel_FrameDetails.Controls.Add(this.TBox_EventDetailsSearch);
            this.Panel_FrameDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_FrameDetails.Location = new System.Drawing.Point(0, 0);
            this.Panel_FrameDetails.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Panel_FrameDetails.Name = "Panel_FrameDetails";
            this.Panel_FrameDetails.Size = new System.Drawing.Size(490, 30);
            this.Panel_FrameDetails.TabIndex = 1;
            // 
            // Label_EventDetailsSearch
            // 
            this.Label_EventDetailsSearch.AutoSize = true;
            this.Label_EventDetailsSearch.Location = new System.Drawing.Point(4, 9);
            this.Label_EventDetailsSearch.Margin = new System.Windows.Forms.Padding(4, 9, 4, 0);
            this.Label_EventDetailsSearch.Name = "Label_EventDetailsSearch";
            this.Label_EventDetailsSearch.Size = new System.Drawing.Size(52, 15);
            this.Label_EventDetailsSearch.TabIndex = 0;
            this.Label_EventDetailsSearch.Text = "Search";
            this.Label_EventDetailsSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TBox_EventDetailsSearch
            // 
            this.TBox_EventDetailsSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBox_EventDetailsSearch.Location = new System.Drawing.Point(65, 4);
            this.TBox_EventDetailsSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TBox_EventDetailsSearch.Name = "TBox_EventDetailsSearch";
            this.TBox_EventDetailsSearch.Size = new System.Drawing.Size(418, 22);
            this.TBox_EventDetailsSearch.TabIndex = 1;
            // 
            // TabControl_Chart
            // 
            this.TabControl_Chart.Controls.Add(this.TabPage_TimeChart);
            this.TabControl_Chart.Controls.Add(this.TabPage_FrameErrorRate);
            this.TabControl_Chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl_Chart.Location = new System.Drawing.Point(0, 0);
            this.TabControl_Chart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TabControl_Chart.Name = "TabControl_Chart";
            this.TabControl_Chart.SelectedIndex = 0;
            this.TabControl_Chart.Size = new System.Drawing.Size(1408, 202);
            this.TabControl_Chart.TabIndex = 0;
            // 
            // TabPage_TimeChart
            // 
            this.TabPage_TimeChart.Controls.Add(this.PBox_EventTimeChart);
            this.TabPage_TimeChart.Controls.Add(this.HScroll_TimeChart);
            this.TabPage_TimeChart.Controls.Add(this.VScroll_TimeChart);
            this.TabPage_TimeChart.Location = new System.Drawing.Point(4, 25);
            this.TabPage_TimeChart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TabPage_TimeChart.Name = "TabPage_TimeChart";
            this.TabPage_TimeChart.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TabPage_TimeChart.Size = new System.Drawing.Size(1400, 173);
            this.TabPage_TimeChart.TabIndex = 0;
            this.TabPage_TimeChart.Text = "Time Chart";
            this.TabPage_TimeChart.UseVisualStyleBackColor = true;
            // 
            // PBox_EventTimeChart
            // 
            this.PBox_EventTimeChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PBox_EventTimeChart.Location = new System.Drawing.Point(4, 4);
            this.PBox_EventTimeChart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PBox_EventTimeChart.Name = "PBox_EventTimeChart";
            this.PBox_EventTimeChart.Size = new System.Drawing.Size(1375, 148);
            this.PBox_EventTimeChart.TabIndex = 3;
            this.PBox_EventTimeChart.TabStop = false;
            this.PBox_EventTimeChart.SizeChanged += new System.EventHandler(this.PBox_EventTimeChart_SizeChanged);
            this.PBox_EventTimeChart.Paint += new System.Windows.Forms.PaintEventHandler(this.PBox_EventTimeChart_Paint);
            // 
            // HScroll_TimeChart
            // 
            this.HScroll_TimeChart.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.HScroll_TimeChart.Location = new System.Drawing.Point(4, 152);
            this.HScroll_TimeChart.Name = "HScroll_TimeChart";
            this.HScroll_TimeChart.Size = new System.Drawing.Size(1375, 17);
            this.HScroll_TimeChart.TabIndex = 2;
            this.HScroll_TimeChart.Scroll += new System.Windows.Forms.ScrollEventHandler(this.HScroll_TimeChart_Scroll);
            // 
            // VScroll_TimeChart
            // 
            this.VScroll_TimeChart.Dock = System.Windows.Forms.DockStyle.Right;
            this.VScroll_TimeChart.Location = new System.Drawing.Point(1379, 4);
            this.VScroll_TimeChart.Name = "VScroll_TimeChart";
            this.VScroll_TimeChart.Size = new System.Drawing.Size(17, 165);
            this.VScroll_TimeChart.TabIndex = 1;
            this.VScroll_TimeChart.Scroll += new System.Windows.Forms.ScrollEventHandler(this.VScroll_TimeChart_Scroll);
            // 
            // TabPage_FrameErrorRate
            // 
            this.TabPage_FrameErrorRate.Controls.Add(this.Chart_FrameErrorRate);
            this.TabPage_FrameErrorRate.Controls.Add(this.Panel_FrameErrorRate);
            this.TabPage_FrameErrorRate.Location = new System.Drawing.Point(4, 25);
            this.TabPage_FrameErrorRate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TabPage_FrameErrorRate.Name = "TabPage_FrameErrorRate";
            this.TabPage_FrameErrorRate.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TabPage_FrameErrorRate.Size = new System.Drawing.Size(1400, 223);
            this.TabPage_FrameErrorRate.TabIndex = 1;
            this.TabPage_FrameErrorRate.Text = "Frame Error Rate";
            this.TabPage_FrameErrorRate.UseVisualStyleBackColor = true;
            // 
            // Chart_FrameErrorRate
            // 
            chartArea1.Name = "ChartArea1";
            this.Chart_FrameErrorRate.ChartAreas.Add(chartArea1);
            this.Chart_FrameErrorRate.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.Chart_FrameErrorRate.Legends.Add(legend1);
            this.Chart_FrameErrorRate.Location = new System.Drawing.Point(4, 35);
            this.Chart_FrameErrorRate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Chart_FrameErrorRate.Name = "Chart_FrameErrorRate";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.Chart_FrameErrorRate.Series.Add(series1);
            this.Chart_FrameErrorRate.Size = new System.Drawing.Size(1392, 184);
            this.Chart_FrameErrorRate.TabIndex = 2;
            this.Chart_FrameErrorRate.Text = "chart1";
            // 
            // Panel_FrameErrorRate
            // 
            this.Panel_FrameErrorRate.Controls.Add(this.label2);
            this.Panel_FrameErrorRate.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_FrameErrorRate.Location = new System.Drawing.Point(4, 4);
            this.Panel_FrameErrorRate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Panel_FrameErrorRate.Name = "Panel_FrameErrorRate";
            this.Panel_FrameErrorRate.Size = new System.Drawing.Size(1392, 31);
            this.Panel_FrameErrorRate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 9, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Custom Text";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PacketViewInstanceImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.Controls.Add(this.Splitter_Main);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "PacketViewInstanceImpl";
            this.Size = new System.Drawing.Size(1408, 798);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GBox_Decoder.ResumeLayout(false);
            this.Splitter_Main.Panel1.ResumeLayout(false);
            this.Splitter_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Splitter_Main)).EndInit();
            this.Splitter_Main.ResumeLayout(false);
            this.Splitter_Sub.Panel1.ResumeLayout(false);
            this.Splitter_Sub.Panel1.PerformLayout();
            this.Splitter_Sub.Panel2.ResumeLayout(false);
            this.Splitter_Sub.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Splitter_Sub)).EndInit();
            this.Splitter_Sub.ResumeLayout(false);
            this.Panel_FrameList.ResumeLayout(false);
            this.Panel_FrameList.PerformLayout();
            this.Panel_FrameDetails.ResumeLayout(false);
            this.Panel_FrameDetails.PerformLayout();
            this.TabControl_Chart.ResumeLayout(false);
            this.TabPage_TimeChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PBox_EventTimeChart)).EndInit();
            this.TabPage_FrameErrorRate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Chart_FrameErrorRate)).EndInit();
            this.Panel_FrameErrorRate.ResumeLayout(false);
            this.Panel_FrameErrorRate.PerformLayout();
            this.ResumeLayout(false);

        }

        public PacketViewInstanceImpl() : base()
        {
            InitializeComponent();
        }

        public PacketViewInstanceImpl(PacketViewManager viewm, PacketViewClass viewd, PacketViewProperty viewp, Guid id) : base(viewm, viewd, viewp, id)
        {
            prop_ = Property as PacketViewPropertyImpl;

            InitializeComponent();
            InitializeFrameDetailsColumn();
			InitializeDecorderType();
			InitializeChartAxisMagList();

			TIME_CHART_DATA_PEN_BASE.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

			draw_chart_param_.ScaleStringFormat.Alignment = StringAlignment.Near;
			draw_chart_param_.ScaleStringFormat.LineAlignment = StringAlignment.Near;

			draw_chart_param_.DataLabelStringFormat.Alignment = StringAlignment.Near;
			draw_chart_param_.DataLabelStringFormat.LineAlignment = StringAlignment.Center;

			CBox_DecoderType.SelectedItem = prop_.DecoderClassID.Value;
			if ((CBox_DecoderType.SelectedIndex < 0) && (CBox_DecoderType.Items.Count > 0)) {
				CBox_DecoderType.SelectedIndex = 0;
			}

			CBox_ChartAxisMag_X.SelectedItem = (double)prop_.ChartAxisMag_X.Value;
			CBox_ChartAxisMag_Y.SelectedItem = (double)prop_.ChartAxisMag_Y.Value;

			UpdateEventListHeader();

            UpdateDecoder();

			UpdateTimeChartSize();
			UpdateTimeChartScrollRange();
			UpdateTimeChartView();
		}

		private void InitializeFrameDetailsColumn()
        {
            TLView_EventDetails.Columns.Add("Name", 220);
            TLView_EventDetails.Columns.Add("Value", 200);
        }

		private void InitializeDecorderType()
		{
			CBox_DecoderType.BeginUpdate();
			{
				CBox_DecoderType.Items.Clear();
				CBox_DecoderType.Items.AddRange(Ratatoskr.Protocol.ProtocolManager.GetDecoderList());
			}
			CBox_DecoderType.EndUpdate();
		}

		private void InitializeChartAxisMagList()
		{
			CBox_ChartAxisMag_X.BeginUpdate();
			{
				foreach (var item in TIME_CHART_AXIS_MAG_LIST) {
					CBox_ChartAxisMag_X.Items.Add(new AxisMagListItem(item));
				}
			}
			CBox_ChartAxisMag_X.EndUpdate();

			CBox_ChartAxisMag_Y.BeginUpdate();
			{
				foreach (var item in TIME_CHART_AXIS_MAG_LIST) {
					CBox_ChartAxisMag_Y.Items.Add(new AxisMagListItem(item));
				}
			}
			CBox_ChartAxisMag_Y.EndUpdate();
		}

		private (int channel_count, DateTime first_event_time, DateTime last_event_time) GetChannelStatus()
		{
			var channel_count = 0;
			var dt_first = DateTime.MaxValue;
			var dt_last = DateTime.MinValue;
			var dt_temp = DateTime.MaxValue;

			/* 全イベントから最も早いイベント時刻を取得 */
			foreach (var prdi in prdi_map_.Values) {
				foreach (var prdch in prdi.GetProtocolDecodeChannels()) {
					channel_count++;

					dt_temp = prdch.FirstEventTime;
					if (dt_first > dt_temp) {
						dt_first = dt_temp;
					}

					dt_temp = prdch.LastEventTime;
					if (dt_last < dt_temp) {
						dt_last = dt_temp;
					}
				}
			}

			if (dt_first == DateTime.MaxValue) {
				dt_first = DateTime.MinValue;
			}

			return (channel_count, dt_first, dt_last);
		}

		private void UpdateEventListHeader()
        {
            LView_EventList.BeginUpdate();
            {
                /* 先にデータをすべて削除してからヘッダーを削除する */
                LView_EventList.ItemClear();
                LView_EventList.Columns.Clear();

                /* メインヘッダー */
                LView_EventList.Columns.Add(
                    new ColumnHeader()
                    {
                        Text = "No.",
                        Width = 50,
                    }
                );

                foreach (var config in prop_.EventListColumn.Value) {
                    LView_EventList.Columns.Add(
                        new ColumnHeader()
                        {
                            Text = GetEventListViewHeaderName(config.Type),
                            Width = (int)config.Width,
                            Tag = config.Type,
                        }
                    );
                }
            }
            LView_EventList.EndUpdate();
        }

        private void UpdateDecoder()
        {
            prdc_ = CBox_DecoderType.SelectedItem as ProtocolDecoderClass;
			prdi_map_ = new Dictionary<string, ProtocolDecoderInstance>();
        }

		private void UpdateEventDetails(ProtocolDecodeEvent prde)
		{
			TLView_EventDetails.Nodes.Clear();

			var node = BuildTreeNodeFromProtocolDecodeEvent(prde);

			if (node == null) return;

			node.ExpandAll();

			TLView_EventDetails.Nodes.Add(node);
			TLView_EventDetails.UpdateView();
		}

		private void UpdateTimeChartSize()
		{
			var client_size = PBox_EventTimeChart.ClientSize;

			draw_chart_param_.DrawRect_MeasureTop.X = MARGIN_TIME_CHART_BODY.Left + TIME_CHART_CHART_LABEL_WIDTH;
			draw_chart_param_.DrawRect_MeasureTop.Y = MARGIN_TIME_CHART_BODY.Top;
			draw_chart_param_.DrawRect_MeasureTop.Width = client_size.Width - draw_chart_param_.DrawRect_MeasureTop.X - MARGIN_TIME_CHART_BODY.Right;
			draw_chart_param_.DrawRect_MeasureTop.Height = TIME_CHART_MEASURE_HEIGHT;

			draw_chart_param_.DrawRect_MeasureBottom.Width = draw_chart_param_.DrawRect_MeasureTop.Width;
			draw_chart_param_.DrawRect_MeasureBottom.Height = draw_chart_param_.DrawRect_MeasureTop.Height;
			draw_chart_param_.DrawRect_MeasureBottom.X = draw_chart_param_.DrawRect_MeasureTop.X;
			draw_chart_param_.DrawRect_MeasureBottom.Y = client_size.Height - MARGIN_TIME_CHART_BODY.Bottom - draw_chart_param_.DrawRect_MeasureBottom.Height;

			draw_chart_param_.DrawRect_ChartData.X = MARGIN_TIME_CHART_BODY.Left;
			draw_chart_param_.DrawRect_ChartData.Y = draw_chart_param_.DrawRect_MeasureTop.Bottom;
			draw_chart_param_.DrawRect_ChartData.Width = Math.Max(0, client_size.Width - MARGIN_TIME_CHART_BODY.Horizontal);
			draw_chart_param_.DrawRect_ChartData.Height = Math.Max(0, draw_chart_param_.DrawRect_MeasureBottom.Top - draw_chart_param_.DrawRect_MeasureTop.Bottom);
		}

		private void UpdateTimeChartScrollRange()
		{
			var status = GetChannelStatus();

			draw_chart_param_.FirstEventTime = status.first_event_time;
			draw_chart_param_.LastEventTime  = status.last_event_time;
			draw_chart_param_.ViewScaleSize  = draw_chart_param_.DrawRect_MeasureTop.Width / TIME_CHART_SCALE_STEP;

			var dt_delta = (draw_chart_param_.LastEventTime - draw_chart_param_.FirstEventTime).TotalMilliseconds;

			var scroll_now = (double)HScroll_TimeChart.Value / HScroll_TimeChart.Maximum;
			var scroll_scale_max  = (int)(Math.Min(dt_delta / draw_chart_param_.OneScaleTimeMsec, int.MaxValue));

			if (double.IsNaN(scroll_now)) {
				scroll_now = 0;
			}

			HScroll_TimeChart.Minimum = 0;
			HScroll_TimeChart.Maximum = (int)Math.Max(scroll_scale_max - draw_chart_param_.ViewScaleSize, 0);
			HScroll_TimeChart.Value = Math.Min((int)(HScroll_TimeChart.Maximum * scroll_now), HScroll_TimeChart.Maximum);

			VScroll_TimeChart.Minimum = 0;
			VScroll_TimeChart.Maximum = Math.Max(0, status.channel_count * TIME_CHART_CHART_HEIGHT - draw_chart_param_.DrawRect_ChartData.Height);
			VScroll_TimeChart.Value = Math.Min(VScroll_TimeChart.Value, VScroll_TimeChart.Maximum);

			UpdateTimeChartScrollPos();
		}

		private void UpdateTimeChartScrollPos()
		{
			draw_chart_param_.ViewVerticalOffset = VScroll_TimeChart.Value;
			draw_chart_param_.ViewScaleIndex = HScroll_TimeChart.Value;

			draw_chart_param_.ViewFirstEventTime = draw_chart_param_.FirstEventTime.AddMilliseconds(draw_chart_param_.OneScaleTimeMsec * draw_chart_param_.ViewScaleIndex);
			draw_chart_param_.ViewLastEventTime = draw_chart_param_.ViewFirstEventTime.AddMilliseconds(draw_chart_param_.OneScaleTimeMsec * draw_chart_param_.ViewScaleSize);

			PBox_EventTimeChart.Invalidate();
		}

		private void UpdateTimeChartView()
		{
			PBox_EventTimeChart.Invalidate();
		}

		private string GetEventListViewHeaderName(EventListColumnType type)
        {
            switch (type) {
                case EventListColumnType.Channel:           return ("Channel");
                case EventListColumnType.BlockTime_UTC:     return ("BlockTime(UTC)");
                case EventListColumnType.BlockTime_Local:   return ("BlockTime(Local)");
                case EventListColumnType.EventTime_UTC:     return ("EventTime(UTC)");
                case EventListColumnType.EventTime_Local:   return ("EventTime(Local)");
                case EventListColumnType.EventType:         return ("Type");
				case EventListColumnType.Information:       return ("Information");
				default:                                    return (type.ToString());
            }
        }

        private void ProtocolDecodeEventToDataListViewItem_Sub(ListViewItem item, ProtocolDecodeEvent prde)
        {
            foreach (var config in prop_.EventListColumn.Value) {
                switch (config.Type) {
					case EventListColumnType.Alias:
						break;

                    case EventListColumnType.Channel:
						item.SubItems.Add(prde.Channel.Name);
						break;

                    case EventListColumnType.BlockTime_UTC:
                        item.SubItems.Add(prde.BlockDateTime.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.ffffff"));
                        break;

                    case EventListColumnType.BlockTime_Local:
                        item.SubItems.Add(prde.BlockDateTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.ffffff"));
                        break;

                    case EventListColumnType.EventTime_UTC:
                        item.SubItems.Add(prde.EventDateTime.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.ffffff"));
                        break;

                    case EventListColumnType.EventTime_Local:
                        item.SubItems.Add(prde.EventDateTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.ffffff"));
                        break;

					case EventListColumnType.EventType:
						item.SubItems.Add(prde.EventType.ToString());
						break;

					case EventListColumnType.Information:
                        item.SubItems.Add(prde.ToString());
                        break;

                    default:
                        item.SubItems.Add("");
                        break;
                }
            }

            /* 背景色 */
			if (prde is ProtocolDecodeEvent_Frame prde_f) {
				switch (prde_f.Frame.Status) {
					case ProtocolFrameStatus.Normal:    item.BackColor = Color.LightGreen;      break;
					default:                            item.BackColor = Color.LightPink;       break;
				}
			}
        }

        private ListViewItem EventListViewItemToListViewItem(EventListViewItem elvi)
        {
            var item = new ListViewItem() {
                Text = elvi.No.ToString(),
                Tag  = elvi,
            };

			/* サブサイテム */
			foreach (var config in prop_.EventListColumn.Value) {
				switch (config.Type) {
					case EventListColumnType.Alias:
						item.SubItems.Add(elvi.Alias);
						break;

					case EventListColumnType.Channel:
						item.SubItems.Add(string.Format("{0} - {1}", elvi.Alias, elvi.DecodeEvent.Channel.Name));
						break;

					case EventListColumnType.BlockTime_UTC:
						item.SubItems.Add(elvi.DecodeEvent.BlockDateTime.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.ffffff"));
						break;

					case EventListColumnType.BlockTime_Local:
						item.SubItems.Add(elvi.DecodeEvent.BlockDateTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.ffffff"));
						break;

					case EventListColumnType.EventTime_UTC:
						item.SubItems.Add(elvi.DecodeEvent.EventDateTime.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.ffffff"));
						break;

					case EventListColumnType.EventTime_Local:
						item.SubItems.Add(elvi.DecodeEvent.EventDateTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.ffffff"));
						break;

					case EventListColumnType.EventType:
						item.SubItems.Add(elvi.DecodeEvent.EventType.ToString());
						break;

					case EventListColumnType.Information:
						item.SubItems.Add(elvi.DecodeEvent.ToString());
						break;

					default:
						item.SubItems.Add("");
						break;
				}
			}

			/* 背景色 */
			if (elvi.DecodeEvent is ProtocolDecodeEvent_Frame prde_f) {
				switch (prde_f.Frame.Status) {
					case ProtocolFrameStatus.Normal: item.BackColor = Color.LightGreen; break;
					default: item.BackColor = Color.LightPink; break;
				}
			}

			return (item);
        }

		private TreeListViewNode BuildTreeNodeFromProtocolDecodeEvent(ProtocolDecodeEvent prde)
		{
			if (prde == null)return (null);

			if (prde is ProtocolDecodeEvent_Frame prde_f) {
				return (BuildTreeNodeFromProtocolDecodeEvent_Frame(prde_f.Frame));
			}

			return (null);
		}

		private TreeListViewNode BuildTreeNodeFromProtocolDecodeEvent_Frame(ProtocolFrameElement pfe)
        {
			if (pfe == null)return (null);

            try {
                var node  = new TreeListViewNode();

                var value_name = pfe.Name;
                var value_text = pfe.ToString();

                node.Text = value_name;
				node.Tag  = pfe;

				node.Values.Add(value_text);

                var sub_elements = pfe.GetUnpackElements();

                if (sub_elements != null) {
					var sub_node = (TreeListViewNode)null;

					foreach (var sub_element in pfe.GetUnpackElements()) {
                        sub_node = BuildTreeNodeFromProtocolDecodeEvent_Frame(sub_element);
                        if (sub_node != null) {
							node.Nodes.Add(sub_node);
						}
                    }
                }

                return (node);

            } catch {
                return (null);
            }
        }

		private void AddDecodeEvent(string alias, ProtocolDecodeEvent prde)
		{
			/* フレームリスト */
			next_item_no_ = Math.Max(next_item_no_, ITEM_NO_MIN);
			next_item_no_ = Math.Min(next_item_no_, ITEM_NO_MAX);

			list_items_temp_.Add(new EventListViewItem(next_item_no_, alias, prde));

			next_item_no_++;
		}

		private void DrawTimeChart(Graphics graphics, Size graphics_size)
		{
			/* 目盛り描画 */
			DrawTimeChart_TimeMeasure(graphics, draw_chart_param_);

//			graphics.DrawRectangle(Pens.LightBlue, draw_chart_param_.DrawRect_MeasureTop);
//			graphics.DrawRectangle(Pens.LightPink, draw_chart_param_.DrawRect_MeasureBottom);
//			graphics.DrawRectangle(Pens.LightGreen, draw_chart_param_.DrawRect_ChartData);

			/* データ描画 */
			DrawTimeChart_ChartData(graphics, draw_chart_param_);

#if false
			/* タイムチャート描画 */
			foreach (var prdi in prdi_map_) {
				if (draw_offset.Y > graphics_size.Height)break;

				foreach (var prdch in prdi.Value.GetProtocolDecodeChannels()) {
					/* 描画範囲を取得 */
					view_rect.Width = canvas_size.Width - MARGIN_TIME_CHART_BODY.Horizontal;
					view_rect.Height = canvas_size.Height - MARGIN_TIME_CHART_BODY.Vertical;
					view_rect.X = MARGIN_TIME_CHART_BODY.Left;
					view_rect.Y = MARGIN_TIME_CHART_BODY.Top;

					if (draw_offset.Y > graphics_rect.Bottom)break;

					draw_offset.Y += DrawTimeChart_Group(graphics, graphics_rect, draw_offset, dt_first, prdcg);
				}
			}

			graphics_layer.Render(graphics);
			graphics_layer.Dispose();
#endif
		}

		private void DrawTimeChart_TimeMeasure(Graphics graphics, DrawTimeChartParam param)
		{
			var scale_index = param.ViewScaleIndex;

			var draw_measure_top_s    = new Point(param.DrawRect_MeasureTop.Left, param.DrawRect_MeasureTop.Top + TIME_CHART_MEASURE_LABEL_HEIGHT);
			var draw_measure_bottom_s = new Point(param.DrawRect_MeasureTop.Left, param.DrawRect_MeasureBottom.Bottom - TIME_CHART_MEASURE_LABEL_HEIGHT);

			var dt_scale_minor = param.ViewFirstEventTime;
			var dt_scale_major = ((scale_index % TIME_CHART_MEASURE_MAJOR_LINE_STEP) == 0)
							   ? (dt_scale_minor)
							   : (dt_scale_minor.AddMilliseconds((TIME_CHART_MEASURE_MAJOR_LINE_STEP - (scale_index % TIME_CHART_MEASURE_MAJOR_LINE_STEP)) * param.OneScaleTimeMsec));

			/* 基本線(上側) */
			graphics.DrawLine(
				TIME_CHART_SCALE_PEN,
				param.DrawRect_MeasureTop.Left,
				param.DrawRect_MeasureTop.Top + TIME_CHART_MEASURE_LABEL_HEIGHT,
				param.DrawRect_MeasureTop.Right,
				param.DrawRect_MeasureTop.Top + TIME_CHART_MEASURE_LABEL_HEIGHT);

			/* 基本線(下側) */
			graphics.DrawLine(
				TIME_CHART_SCALE_PEN,
				param.DrawRect_MeasureBottom.Left,
				param.DrawRect_MeasureBottom.Bottom - TIME_CHART_MEASURE_LABEL_HEIGHT,
				param.DrawRect_MeasureBottom.Right,
				param.DrawRect_MeasureBottom.Bottom - TIME_CHART_MEASURE_LABEL_HEIGHT);

			/* 目盛り */
			var draw_pos = param.DrawRect_MeasureTop.Left;
			var draw_scale_text = new Point();

			/* メイン */
			draw_scale_text.X = draw_pos;
			draw_scale_text.Y = draw_measure_top_s.Y - TIME_CHART_MEASURE_LABEL_HEIGHT;

			graphics.DrawString(
				dt_scale_major.ToString("yyyy-MM-dd"),
				TIME_CHART_SCALE_FONT,
				TIME_CHART_SCALE_BRUSH,
				draw_scale_text,
				param.ScaleStringFormat);

			while (draw_pos < param.DrawRect_MeasureTop.Right) {
				if ((scale_index % TIME_CHART_MEASURE_MAJOR_LINE_STEP) == 0) {
					/* メイン */
					draw_scale_text.X = draw_pos;
					draw_scale_text.Y = draw_measure_top_s.Y - TIME_CHART_MEASURE_LABEL_HEIGHT / 2;

					graphics.DrawString(
//						dt_scale_major.ToString("HH:mm:ss.fff"),
						(dt_scale_major - param.FirstEventTime).ToString("hh\\:mm\\:ss\\.fff"),
						TIME_CHART_SCALE_FONT,
						TIME_CHART_SCALE_BRUSH,
						draw_scale_text,
						param.ScaleStringFormat);

					graphics.DrawLine(
						TIME_CHART_SCALE_PEN,
						draw_pos,
						draw_measure_top_s.Y,
						draw_pos,
						draw_measure_top_s.Y + TIME_CHART_MEASURE_MAJOR_LINE_HEIGHT);

					graphics.DrawLine(
						TIME_CHART_SCALE_PEN,
						draw_pos,
						draw_measure_bottom_s.Y,
						draw_pos,
						draw_measure_bottom_s.Y - TIME_CHART_MEASURE_MAJOR_LINE_HEIGHT);

					dt_scale_major = dt_scale_major.AddMilliseconds(TIME_CHART_MEASURE_MAJOR_LINE_STEP * param.OneScaleTimeMsec);

				} else if ((scale_index % TIME_CHART_MEASURE_MINOR_LINE_STEP) == 0) {
					/* サブ */
					graphics.DrawLine(
						TIME_CHART_SCALE_PEN,
						draw_pos,
						draw_measure_top_s.Y,
						draw_pos,
						draw_measure_top_s.Y + TIME_CHART_MEASURE_MINOR_LINE_HEIGHT);

					graphics.DrawLine(
						TIME_CHART_SCALE_PEN,
						draw_pos,
						draw_measure_bottom_s.Y,
						draw_pos,
						draw_measure_bottom_s.Y - TIME_CHART_MEASURE_MINOR_LINE_HEIGHT);
				}

				scale_index++;
				draw_pos += TIME_CHART_SCALE_STEP;
			}
		}

		private void DrawTimeChart_ChartData(Graphics graphics, DrawTimeChartParam param)
		{
			var graphics_layer = BufferedGraphicsManager.Current.Allocate(graphics, param.DrawRect_ChartData);

			graphics_layer.Graphics.FillRectangle(SystemBrushes.Window, param.DrawRect_ChartData);

			{
				var draw_pos_base = new Point(param.DrawRect_ChartData.X, param.DrawRect_ChartData.Y - param.ViewVerticalOffset);

				var draw_pos_center_s = Point.Empty;
				var draw_pos_center_e = Point.Empty;

				var draw_dot_rect = new Rectangle(0, 0, TIME_CHART_DATA_DOT_SIZE, TIME_CHART_DATA_DOT_SIZE);

				var dt_event_e = DateTime.MinValue;

				foreach (var prdi in prdi_map_) {
					foreach (var prdch in prdi.Value.GetProtocolDecodeChannels()) {
						/* 描画範囲を超えた場合は終了 */
						if (draw_pos_base.Y >= param.DrawRect_ChartData.Bottom)break;

						/* 描画対象 */
						if (draw_pos_base.Y + TIME_CHART_CHART_HEIGHT >= param.DrawRect_ChartData.Top) {
							draw_pos_base.X = param.DrawRect_ChartData.X;
							draw_pos_center_s.X = draw_pos_center_e.X = draw_pos_base.X;
							draw_pos_center_s.Y = draw_pos_center_e.Y = draw_pos_base.Y + TIME_CHART_CHART_HEIGHT / 2;

							/* ラベル */
							graphics_layer.Graphics.DrawString(
								string.Format("{0} - {1}", prdi.Key, prdch.Name),
								TIME_CHART_DATA_FONT,
								TIME_CHART_DATA_BRUSH,
								draw_pos_center_s,
								param.DataLabelStringFormat);

							/* 基準線 */
							draw_pos_base.X += TIME_CHART_CHART_LABEL_WIDTH;
							draw_pos_center_s.X += TIME_CHART_CHART_LABEL_WIDTH;
							draw_pos_center_e.X = param.DrawRect_ChartData.Right;
							graphics_layer.Graphics.DrawLine(TIME_CHART_DATA_PEN_BASE, draw_pos_center_s, draw_pos_center_e);

							/* データ */
							draw_dot_rect.Y = draw_pos_center_s.Y - draw_dot_rect.Height / 2;

							foreach (var prde in prdch.Events) {
								/* 描画範囲を超えたので終了 */
								if (prde.EventDateTime >= param.ViewLastEventTime) break;

								/* イベントの終端時刻を取得 */
								dt_event_e = prde.EventDateTime.AddMilliseconds(prde.EventTickMsec);

								/* 描画対象 */
								if (dt_event_e >= param.ViewFirstEventTime) {
									draw_pos_center_s.X = draw_pos_base.X + (int)((prde.EventDateTime - param.ViewFirstEventTime).TotalMilliseconds * draw_chart_axis_x_mag_);
									draw_pos_center_e.X = draw_pos_center_s.X + (int)(prde.EventTickMsec * draw_chart_axis_x_mag_);

									draw_dot_rect.X = draw_pos_center_s.X - draw_dot_rect.Width / 2;

									/* イベント線 */
									graphics_layer.Graphics.DrawLine(TIME_CHART_DATA_PEN_EVENT, draw_pos_center_s, draw_pos_center_e);

									/* イベント点 */
									graphics_layer.Graphics.FillRectangle(TIME_CHART_DATA_BRUSH, draw_dot_rect);
								}
							}
						}

						draw_pos_base.Y += TIME_CHART_CHART_HEIGHT;
					}
				}
			}

			graphics_layer.Render(graphics);
			graphics_layer.Dispose();
		}

		private void DecodePacket(PacketObject packet)
        {
            /* 受信データパケット以外は無視 */
            if (   (packet.Attribute != PacketAttribute.Data)
                || (packet.Direction != PacketDirection.Recv)
            ) {
                return;
            }

			var prdi = (ProtocolDecoderInstance)null;

			/* 処理用デコーダー取得 */
			if (!prdi_map_.TryGetValue(packet.Alias, out prdi)) {
				/* 受信エイリアスに該当するインスタンスがないので新規作成 */
				prdi = prdc_.CreateInstance();
				prdi_map_.Add(packet.Alias, prdi);
			}

			var prde_list = new List<ProtocolDecodeEvent>();

			/* デコード処理 */
			prdi.InputData(packet.MakeTime, packet.Data, prde_list);

			/* 表示 */
			prde_list.ForEach(prde => AddDecodeEvent(packet.Alias, prde));
		}

		protected override void OnBackupProperty()
        {
			prop_.DecoderClassID.Value = (CBox_DecoderType.SelectedItem as ProtocolDecoderClass).ID;
        }

        protected override void OnClearPacket()
        {
            UpdateDecoder();

            LView_EventList.ItemClear();
            TLView_EventDetails.Nodes.Clear();
            TLView_EventDetails.UpdateView();

			UpdateTimeChartScrollRange();

            next_item_no_ = 1;
        }

        protected override void OnDrawPacketBegin(bool auto_scroll)
        {
            /* ちらつき防止用の一時バッファ */
            list_items_temp_ = new List<EventListViewItem>();

            /* リストビューの描画開始 */
            LView_EventList.BeginUpdate();
        }

        protected override void OnDrawPacketEnd(bool auto_scroll, bool next_packet_exist)
        {
            /* 一時リストをリストビューに追加 */
            LView_EventList.ItemAddRange(list_items_temp_);
            list_items_temp_ = null;

            /* 自動スクロール */
            if ((auto_scroll) && (LView_EventList.ItemCount > 0)) {
                LView_EventList.EnsureVisible(LView_EventList.ItemCount - 1);
            }

            /* リストビューの描画完了 */
            LView_EventList.EndUpdate();

			UpdateTimeChartScrollRange();
		}

        protected override void OnDrawPacket(PacketObject packet)
        {
            if (prdc_ == null)return;

            DecodePacket(packet);
        }

		protected override void OnIdle()
		{
			foreach (var prdi in prdi_map_) {
				var prde_list = new List<ProtocolDecodeEvent>();

				/* ポーリング処理 */
				prdi.Value.Poll(prde_list);

				/* 表示 */
				prde_list.ForEach(prde => AddDecodeEvent(prdi.Key, prde));
			}
		}

		private void CBox_DecoderType_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateDecoder();

			RedrawPacket();
		}

		private void LView_EventList_SelectedIndexChanged(object sender, EventArgs e)
		{
            if (LView_EventList.FocusedItem.Tag is EventListViewItem item_e) {
                UpdateEventDetails(item_e.DecodeEvent);
            }
        }

		private void LView_EventList_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
		{
			e.Item = EventListViewItemToListViewItem(LView_EventList.ItemElementAt(e.ItemIndex) as EventListViewItem);
		}

		private void PBox_EventTimeChart_Paint(object sender, PaintEventArgs e)
		{
			DrawTimeChart(e.Graphics, (sender as Control).ClientSize);
		}

		private void PBox_EventTimeChart_SizeChanged(object sender, EventArgs e)
		{
			UpdateTimeChartSize();
			UpdateTimeChartScrollRange();
		}

		private void CBox_ChartAxisMag_X_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!double.TryParse(CBox_ChartAxisMag_X.Text, out draw_chart_axis_x_mag_)) {
				draw_chart_axis_x_mag_ = 1.0f;
			}

			/* 1pixel当たりの時間を更新 */
			draw_chart_param_.OnePixelTimeWidth = 1 / draw_chart_axis_x_mag_;
			draw_chart_param_.OneScaleTimeMsec = TIME_CHART_SCALE_STEP / draw_chart_axis_x_mag_;

			UpdateTimeChartScrollRange();
		}

		private void CBox_ChartAxisMag_Y_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!double.TryParse(CBox_ChartAxisMag_Y.Text, out draw_chart_axis_y_mag_)) {
				draw_chart_axis_y_mag_ = 1.0f;
			}

			UpdateTimeChartScrollRange();
		}

		private void HScroll_TimeChart_Scroll(object sender, ScrollEventArgs e)
		{
			UpdateTimeChartScrollPos();
		}

		private void VScroll_TimeChart_Scroll(object sender, ScrollEventArgs e)
		{
			UpdateTimeChartScrollPos();
		}
	}
}
