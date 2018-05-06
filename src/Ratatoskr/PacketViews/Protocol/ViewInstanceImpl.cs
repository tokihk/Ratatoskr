using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Packet;
using Ratatoskr.PacketViews.Protocol.Configs;
using Ratatoskr.Protocol;
using RtsCore.Protocol;

namespace Ratatoskr.PacketViews.Protocol
{
    internal sealed class ViewInstanceImpl : ViewInstance
    {
        private class DataBlockBuffer
        {
            private List<ProtocolDecodeData> data_list_ = new List<ProtocolDecodeData>();

            public DataBlockBuffer(DateTime time)
            {
                BlockTime = time;
            }

            public DateTime                        BlockTime { get; set; }
            public IEnumerable<ProtocolDecodeData> DataList  { get { return (data_list_); } }

            public void AddData(ProtocolDecodeData data)
            {
                if ((data_list_.Count == 0) || (data.DataBlockIndex > data_list_.Last().DataBlockIndex)) {
                    /* === データが存在しないか、最後のデータよりもインデックス番号が新しい === */
                    data_list_.Add(data);
                } else {
                    var insert_pos = data_list_.FindLastIndex(item => data.DataBlockIndex > item.DataBlockIndex);

                    if (insert_pos >= 0) {
                        data_list_.Insert(insert_pos, data);
                    } else {
                        data_list_.Add(data);
                    }
                }
            }
        }

        private class DataChannelBuffer
        {
            private List<DataBlockBuffer> block_list_ = new List<DataBlockBuffer>();

            public ProtocolDecodeChannel        ChannelData { get; set; } = null;
            public IEnumerable<DataBlockBuffer> BlockList   { get { return (block_list_); } }

            public void AddData(ProtocolDecodeData data)
            {
                /* 挿入先ブロックを検索 */
                var block = block_list_.Find(item => item.BlockTime == data.DataBlockTime);

                /* ブロックが見つからない場合は新規作成 */
                if (block == null) {
                    block = new DataBlockBuffer(data.DataBlockTime);

                    /* 挿入先を検索 */
                    var insert_pos = block_list_.FindLastIndex(item => block.BlockTime > item.BlockTime);

                    if (insert_pos >= 0) {
                        block_list_.Insert(insert_pos, block);
                    } else {
                        block_list_.Add(block);
                    }
                }

                block.AddData(data);
            }
        }

        private ViewPropertyImpl prop_;

        private Guid            decoder_id_ = Guid.Empty;
        private ProtocolDecoder decoder_ = null;

        private DataChannelBuffer[] channel_data_ = null;

        private List<ListViewItem> list_items_temp_;

        private ulong next_item_no_ = 1;

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox GBox_ViewMode;
        private System.Windows.Forms.ComboBox CBox_ProtocolType;
        private System.Windows.Forms.SplitContainer Splitter_Main;
        private System.Windows.Forms.SplitContainer Splitter_Sub;
        private System.Windows.Forms.TreeView TView_DataDetails;
        private System.Windows.Forms.Panel Panel_FrameDetails;
        private System.Windows.Forms.Label Label_DetailsFilter;
        private System.Windows.Forms.TextBox TBox_DetailsFilter;
        private System.Windows.Forms.TabControl TabControl_Chart;
        private System.Windows.Forms.TabPage TabPage_TimeChart;
        private System.Windows.Forms.FlowLayoutPanel Panel_TimeChart;
        private System.Windows.Forms.TabPage TabPage_FrameErrorRate;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_FrameErrorRate;
        private System.Windows.Forms.FlowLayoutPanel Panel_FrameErrorRate;
        private Generic.Controls.ListViewEx LView_DataList;
        private System.Windows.Forms.Panel Panel_FrameList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TBox_CustomText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox PBox_TimeChart;
        private System.Windows.Forms.HScrollBar HScroll_TimeChart;
        private System.Windows.Forms.VScrollBar VScroll_TimeChart;


        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.GBox_ViewMode = new System.Windows.Forms.GroupBox();
            this.CBox_ProtocolType = new System.Windows.Forms.ComboBox();
            this.Splitter_Main = new System.Windows.Forms.SplitContainer();
            this.Splitter_Sub = new System.Windows.Forms.SplitContainer();
            this.LView_DataList = new Ratatoskr.Generic.Controls.ListViewEx();
            this.Panel_FrameList = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.TBox_CustomText = new System.Windows.Forms.TextBox();
            this.TView_DataDetails = new System.Windows.Forms.TreeView();
            this.Panel_FrameDetails = new System.Windows.Forms.Panel();
            this.Label_DetailsFilter = new System.Windows.Forms.Label();
            this.TBox_DetailsFilter = new System.Windows.Forms.TextBox();
            this.TabControl_Chart = new System.Windows.Forms.TabControl();
            this.TabPage_TimeChart = new System.Windows.Forms.TabPage();
            this.PBox_TimeChart = new System.Windows.Forms.PictureBox();
            this.HScroll_TimeChart = new System.Windows.Forms.HScrollBar();
            this.VScroll_TimeChart = new System.Windows.Forms.VScrollBar();
            this.Panel_TimeChart = new System.Windows.Forms.FlowLayoutPanel();
            this.TabPage_FrameErrorRate = new System.Windows.Forms.TabPage();
            this.Chart_FrameErrorRate = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Panel_FrameErrorRate = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.GBox_ViewMode.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.PBox_TimeChart)).BeginInit();
            this.TabPage_FrameErrorRate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_FrameErrorRate)).BeginInit();
            this.Panel_FrameErrorRate.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.GBox_ViewMode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(922, 51);
            this.panel1.TabIndex = 0;
            // 
            // GBox_ViewMode
            // 
            this.GBox_ViewMode.Controls.Add(this.CBox_ProtocolType);
            this.GBox_ViewMode.Location = new System.Drawing.Point(3, 3);
            this.GBox_ViewMode.Name = "GBox_ViewMode";
            this.GBox_ViewMode.Size = new System.Drawing.Size(200, 42);
            this.GBox_ViewMode.TabIndex = 1;
            this.GBox_ViewMode.TabStop = false;
            this.GBox_ViewMode.Text = "Protocol type";
            // 
            // CBox_ProtocolType
            // 
            this.CBox_ProtocolType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_ProtocolType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_ProtocolType.FormattingEnabled = true;
            this.CBox_ProtocolType.Location = new System.Drawing.Point(3, 15);
            this.CBox_ProtocolType.Name = "CBox_ProtocolType";
            this.CBox_ProtocolType.Size = new System.Drawing.Size(194, 20);
            this.CBox_ProtocolType.TabIndex = 0;
            this.CBox_ProtocolType.SelectedIndexChanged += new System.EventHandler(this.CBox_ProtocolType_SelectedIndexChanged);
            // 
            // Splitter_Main
            // 
            this.Splitter_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Splitter_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.Splitter_Main.Location = new System.Drawing.Point(0, 51);
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
            this.Splitter_Main.Size = new System.Drawing.Size(922, 513);
            this.Splitter_Main.SplitterDistance = 307;
            this.Splitter_Main.TabIndex = 2;
            // 
            // Splitter_Sub
            // 
            this.Splitter_Sub.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Splitter_Sub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Splitter_Sub.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.Splitter_Sub.Location = new System.Drawing.Point(0, 0);
            this.Splitter_Sub.Name = "Splitter_Sub";
            // 
            // Splitter_Sub.Panel1
            // 
            this.Splitter_Sub.Panel1.Controls.Add(this.LView_DataList);
            this.Splitter_Sub.Panel1.Controls.Add(this.Panel_FrameList);
            // 
            // Splitter_Sub.Panel2
            // 
            this.Splitter_Sub.Panel2.Controls.Add(this.TView_DataDetails);
            this.Splitter_Sub.Panel2.Controls.Add(this.Panel_FrameDetails);
            this.Splitter_Sub.Size = new System.Drawing.Size(922, 307);
            this.Splitter_Sub.SplitterDistance = 487;
            this.Splitter_Sub.TabIndex = 0;
            // 
            // LView_DataList
            // 
            this.LView_DataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LView_DataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LView_DataList.FullRowSelect = true;
            this.LView_DataList.GridLines = true;
            this.LView_DataList.HideSelection = false;
            this.LView_DataList.ItemCountMax = ((ulong)(9223372036854775807ul));
            this.LView_DataList.Location = new System.Drawing.Point(0, 25);
            this.LView_DataList.Name = "LView_DataList";
            this.LView_DataList.ReadOnly = false;
            this.LView_DataList.Size = new System.Drawing.Size(485, 280);
            this.LView_DataList.TabIndex = 1;
            this.LView_DataList.UseCompatibleStateImageBehavior = false;
            this.LView_DataList.View = System.Windows.Forms.View.Details;
            this.LView_DataList.VirtualMode = true;
            this.LView_DataList.SelectedIndexChanged += new System.EventHandler(this.LView_DataList_SelectedIndexChanged);
            // 
            // Panel_FrameList
            // 
            this.Panel_FrameList.AutoSize = true;
            this.Panel_FrameList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Panel_FrameList.Controls.Add(this.label1);
            this.Panel_FrameList.Controls.Add(this.TBox_CustomText);
            this.Panel_FrameList.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_FrameList.Location = new System.Drawing.Point(0, 0);
            this.Panel_FrameList.Name = "Panel_FrameList";
            this.Panel_FrameList.Size = new System.Drawing.Size(485, 25);
            this.Panel_FrameList.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Custom Text";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TBox_CustomText
            // 
            this.TBox_CustomText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBox_CustomText.Location = new System.Drawing.Point(80, 3);
            this.TBox_CustomText.Name = "TBox_CustomText";
            this.TBox_CustomText.Size = new System.Drawing.Size(402, 19);
            this.TBox_CustomText.TabIndex = 2;
            // 
            // TView_DataDetails
            // 
            this.TView_DataDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TView_DataDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TView_DataDetails.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TView_DataDetails.FullRowSelect = true;
            this.TView_DataDetails.Location = new System.Drawing.Point(0, 25);
            this.TView_DataDetails.Name = "TView_DataDetails";
            this.TView_DataDetails.Size = new System.Drawing.Size(429, 280);
            this.TView_DataDetails.TabIndex = 2;
            // 
            // Panel_FrameDetails
            // 
            this.Panel_FrameDetails.AutoSize = true;
            this.Panel_FrameDetails.Controls.Add(this.Label_DetailsFilter);
            this.Panel_FrameDetails.Controls.Add(this.TBox_DetailsFilter);
            this.Panel_FrameDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_FrameDetails.Location = new System.Drawing.Point(0, 0);
            this.Panel_FrameDetails.Name = "Panel_FrameDetails";
            this.Panel_FrameDetails.Size = new System.Drawing.Size(429, 25);
            this.Panel_FrameDetails.TabIndex = 1;
            // 
            // Label_DetailsFilter
            // 
            this.Label_DetailsFilter.AutoSize = true;
            this.Label_DetailsFilter.Location = new System.Drawing.Point(3, 7);
            this.Label_DetailsFilter.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.Label_DetailsFilter.Name = "Label_DetailsFilter";
            this.Label_DetailsFilter.Size = new System.Drawing.Size(32, 12);
            this.Label_DetailsFilter.TabIndex = 0;
            this.Label_DetailsFilter.Text = "Filter";
            this.Label_DetailsFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TBox_DetailsFilter
            // 
            this.TBox_DetailsFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBox_DetailsFilter.Location = new System.Drawing.Point(41, 3);
            this.TBox_DetailsFilter.Name = "TBox_DetailsFilter";
            this.TBox_DetailsFilter.Size = new System.Drawing.Size(384, 19);
            this.TBox_DetailsFilter.TabIndex = 1;
            // 
            // TabControl_Chart
            // 
            this.TabControl_Chart.Controls.Add(this.TabPage_TimeChart);
            this.TabControl_Chart.Controls.Add(this.TabPage_FrameErrorRate);
            this.TabControl_Chart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl_Chart.Location = new System.Drawing.Point(0, 0);
            this.TabControl_Chart.Name = "TabControl_Chart";
            this.TabControl_Chart.SelectedIndex = 0;
            this.TabControl_Chart.Size = new System.Drawing.Size(922, 202);
            this.TabControl_Chart.TabIndex = 0;
            // 
            // TabPage_TimeChart
            // 
            this.TabPage_TimeChart.Controls.Add(this.PBox_TimeChart);
            this.TabPage_TimeChart.Controls.Add(this.HScroll_TimeChart);
            this.TabPage_TimeChart.Controls.Add(this.VScroll_TimeChart);
            this.TabPage_TimeChart.Controls.Add(this.Panel_TimeChart);
            this.TabPage_TimeChart.Location = new System.Drawing.Point(4, 22);
            this.TabPage_TimeChart.Name = "TabPage_TimeChart";
            this.TabPage_TimeChart.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_TimeChart.Size = new System.Drawing.Size(914, 176);
            this.TabPage_TimeChart.TabIndex = 0;
            this.TabPage_TimeChart.Text = "Time Chart";
            this.TabPage_TimeChart.UseVisualStyleBackColor = true;
            // 
            // PBox_TimeChart
            // 
            this.PBox_TimeChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PBox_TimeChart.Location = new System.Drawing.Point(3, 43);
            this.PBox_TimeChart.Name = "PBox_TimeChart";
            this.PBox_TimeChart.Size = new System.Drawing.Size(891, 113);
            this.PBox_TimeChart.TabIndex = 3;
            this.PBox_TimeChart.TabStop = false;
            // 
            // HScroll_TimeChart
            // 
            this.HScroll_TimeChart.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.HScroll_TimeChart.Location = new System.Drawing.Point(3, 156);
            this.HScroll_TimeChart.Name = "HScroll_TimeChart";
            this.HScroll_TimeChart.Size = new System.Drawing.Size(891, 17);
            this.HScroll_TimeChart.TabIndex = 2;
            // 
            // VScroll_TimeChart
            // 
            this.VScroll_TimeChart.Dock = System.Windows.Forms.DockStyle.Right;
            this.VScroll_TimeChart.Location = new System.Drawing.Point(894, 43);
            this.VScroll_TimeChart.Name = "VScroll_TimeChart";
            this.VScroll_TimeChart.Size = new System.Drawing.Size(17, 130);
            this.VScroll_TimeChart.TabIndex = 1;
            // 
            // Panel_TimeChart
            // 
            this.Panel_TimeChart.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_TimeChart.Location = new System.Drawing.Point(3, 3);
            this.Panel_TimeChart.Name = "Panel_TimeChart";
            this.Panel_TimeChart.Size = new System.Drawing.Size(908, 40);
            this.Panel_TimeChart.TabIndex = 0;
            // 
            // TabPage_FrameErrorRate
            // 
            this.TabPage_FrameErrorRate.Controls.Add(this.Chart_FrameErrorRate);
            this.TabPage_FrameErrorRate.Controls.Add(this.Panel_FrameErrorRate);
            this.TabPage_FrameErrorRate.Location = new System.Drawing.Point(4, 22);
            this.TabPage_FrameErrorRate.Name = "TabPage_FrameErrorRate";
            this.TabPage_FrameErrorRate.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_FrameErrorRate.Size = new System.Drawing.Size(914, 176);
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
            this.Chart_FrameErrorRate.Location = new System.Drawing.Point(3, 28);
            this.Chart_FrameErrorRate.Name = "Chart_FrameErrorRate";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.Chart_FrameErrorRate.Series.Add(series1);
            this.Chart_FrameErrorRate.Size = new System.Drawing.Size(908, 145);
            this.Chart_FrameErrorRate.TabIndex = 2;
            this.Chart_FrameErrorRate.Text = "chart1";
            // 
            // Panel_FrameErrorRate
            // 
            this.Panel_FrameErrorRate.Controls.Add(this.label2);
            this.Panel_FrameErrorRate.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_FrameErrorRate.Location = new System.Drawing.Point(3, 3);
            this.Panel_FrameErrorRate.Name = "Panel_FrameErrorRate";
            this.Panel_FrameErrorRate.Size = new System.Drawing.Size(908, 25);
            this.Panel_FrameErrorRate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Custom Text";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ViewInstanceImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.Splitter_Main);
            this.Controls.Add(this.panel1);
            this.Name = "ViewInstanceImpl";
            this.Size = new System.Drawing.Size(922, 564);
            this.panel1.ResumeLayout(false);
            this.GBox_ViewMode.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.PBox_TimeChart)).EndInit();
            this.TabPage_FrameErrorRate.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Chart_FrameErrorRate)).EndInit();
            this.Panel_FrameErrorRate.ResumeLayout(false);
            this.Panel_FrameErrorRate.PerformLayout();
            this.ResumeLayout(false);

        }

        public ViewInstanceImpl() : base()
        {
            InitializeComponent();
        }

        public ViewInstanceImpl(ViewManager viewm, ViewClass viewd, ViewProperty viewp, Guid id) : base(viewm, viewd, viewp, id)
        {
            prop_ = Property as ViewPropertyImpl;

            InitializeComponent();

            UpdateProtocolTypeList();
            UpdateFrameListHeader();

            UpdateDecoder();
        }

        private void UpdateProtocolTypeList()
        {
            CBox_ProtocolType.BeginUpdate();
            {
                CBox_ProtocolType.Items.Clear();
                CBox_ProtocolType.Items.AddRange(Ratatoskr.Protocol.ProtocolManager.DecoderList);
                CBox_ProtocolType.SelectedItem = prop_.ProtocolType.Value;
                if ((CBox_ProtocolType.SelectedIndex < 0) && (CBox_ProtocolType.Items.Count > 0)) {
                    CBox_ProtocolType.SelectedIndex = 0;
                }
            }
            CBox_ProtocolType.EndUpdate();
        }

        private void UpdateFrameListHeader()
        {
            LView_DataList.BeginUpdate();
            {
                /* 先にデータをすべて削除してからヘッダーを削除する */
                LView_DataList.ItemClear();
                LView_DataList.Columns.Clear();

                /* メインヘッダー */
                var column_main = new ColumnHeader();

                column_main.Text = "No.";
                column_main.Width = 50;

                LView_DataList.Columns.Add(column_main);

                foreach (var config in prop_.FrameListColumn.Value) {
                    var column_sub = new ColumnHeader();

                    column_sub.Text = GetDataListViewHeaderName(config.Type);
                    column_sub.Width = (int)config.Width;
                    column_sub.Tag = config.Type;

                    LView_DataList.Columns.Add(column_sub);
                }
            }
            LView_DataList.EndUpdate();
        }

        private void UpdateDecoder()
        {
            var decoder = (ProtocolDecoder)null;
            var decoder_info = CBox_ProtocolType.SelectedItem as ProtocolDecoderInfo;

            if (decoder_info != null) {
                decoder = decoder_info.LoadModule();
            }

            decoder_ = decoder;
        }

        private string GetDataListViewHeaderName(FrameListColumnType type)
        {
            switch (type) {
                case FrameListColumnType.Alias:             return ("Alias");
                case FrameListColumnType.Channel:           return ("Channel");
                case FrameListColumnType.BlockTime_UTC:     return ("BlockTime(UTC)");
                case FrameListColumnType.BlockTime_Local:   return ("BlockTime(Local)");
                case FrameListColumnType.BlockIndex:        return ("Index");
                case FrameListColumnType.DateTime_UTC:      return ("DateTime(UTC)");
                case FrameListColumnType.DateTime_Local:    return ("DateTime(Local)");
                case FrameListColumnType.Outline:           return ("Outline");
                case FrameListColumnType.Outline_Custom:    return ("Outline(Custom)");
                default:                                    return (type.ToString());
            }
        }

        private void ProtocolDecodeDataToDataListViewItem_Sub(ListViewItem item, PacketObject packet, ProtocolDecodeData decode_data)
        {
            foreach (var config in prop_.FrameListColumn.Value) {
                switch (config.Type) {
                    case FrameListColumnType.Alias:
                        item.SubItems.Add(packet.Alias);
                        break;

                    case FrameListColumnType.Channel:
                        item.SubItems.Add(
                              (decode_data.DataChannel < channel_data_.Length)
                            ? (channel_data_[decode_data.DataChannel].ChannelData.ToString())
                            : (decode_data.DataChannel.ToString()));
                        break;

                    case FrameListColumnType.BlockTime_UTC:
                        item.SubItems.Add(decode_data.DataBlockTime.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        break;

                    case FrameListColumnType.BlockTime_Local:
                        item.SubItems.Add(decode_data.DataBlockTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        break;

                    case FrameListColumnType.BlockIndex:
                        item.SubItems.Add(decode_data.DataBlockIndex.ToString());
                        break;

                    case FrameListColumnType.DateTime_UTC:
                        item.SubItems.Add(packet.MakeTime.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        break;

                    case FrameListColumnType.DateTime_Local:
                        item.SubItems.Add(packet.MakeTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        break;

                    case FrameListColumnType.Outline:
                        if (decode_data.Data != null) {
                            item.SubItems.Add(decode_data.Data.ToString());
                        } else {
                            item.SubItems.Add("");
                        }
                        break;

                    case FrameListColumnType.Outline_Custom:
                        break;

                    default:
                        item.SubItems.Add("");
                        break;
                }
            }

            /* 背景色 */
            switch (decode_data.Data.ErrorStatus) {
                case ProtocolDataErrorLevel.NoError:    item.BackColor = Color.LightSkyBlue;    break;
                case ProtocolDataErrorLevel.LevelLow:   item.BackColor = Color.LightYellow;     break;
                default:                                item.BackColor = Color.LightPink;       break;
            }
        }

        private ListViewItem ProtocolDecodeDataToDataListViewItem(PacketObject packet, ProtocolDecodeData decode_data)
        {
            var item = new ListViewItem();

            /* メインアイテム */
            item.Text = (next_item_no_).ToString();
            item.Tag = decode_data;

            /* サブサイテム */
            ProtocolDecodeDataToDataListViewItem_Sub(item, packet, decode_data);

            /* 次のインデックス番号を更新 */
            next_item_no_ = Math.Max(1, next_item_no_ + 1);

            return (item);
        }

        private void InputDataList(PacketObject packet, ProtocolDecodeData decode_data)
        {
            var item = ProtocolDecodeDataToDataListViewItem(packet, decode_data);

            if (item == null)return;

            list_items_temp_.Add(item);
        }

        private TreeNode BuildTreeNodeFromFrameElement(ProtocolFrameElement frame_element)
        {
            if (frame_element == null)return (null);

            try {
                var node = new TreeNode();
                var value_name = frame_element.Name;
                var value_text = frame_element.ToString();

                if (value_text.Length > 0) {
                    node.Text = string.Format("{0,-32}: {1}", value_name, value_text);
                } else {
                    node.Text = value_name;
                }

                node.Tag = frame_element;

                var sub_elements = frame_element.GetUnpackElement();
                var sub_node = (TreeNode)null;

                if (sub_elements != null) {
                    foreach (var sub_element in frame_element.GetUnpackElement()) {
                        sub_node = BuildTreeNodeFromFrameElement(sub_element);

                        if (sub_node == null)continue;
                    
                        node.Nodes.Add(sub_node);
                    }
                }

                return (node);

            } catch {
                return (null);
            }
        }

        private void SetDataDetails(ProtocolDecodeData decode_data)
        {
            TView_DataDetails.Nodes.Clear();

            var node = BuildTreeNodeFromFrameElement(decode_data.Data);

            if (node == null)return;

            node.Expand();

            TView_DataDetails.Nodes.Add(node);
        }

        private void StretchChannelBufferList(uint num)
        {
            if (   (channel_data_ != null)
                && (num < (uint)channel_data_.Length)
            ) {
                return;
            }

            var channel_data_new = new DataChannelBuffer[num + 1];

            /* 既にデータが存在する場合は新しいリストにコピー */
            if ((channel_data_ != null) && (channel_data_.Length > 0)) {
                Array.Copy(channel_data_new, channel_data_, Math.Min(channel_data_new.Length, channel_data_.Length));
            }

            /* 新規チャンネル情報を更新 */
            for (var index = 0; index < channel_data_new.Length; index++) {
                if (channel_data_new[index] == null) {
                    channel_data_new[index] = new DataChannelBuffer();
                    channel_data_new[index].ChannelData = decoder_.GetChannelData((uint)index);
                }
            }

            /* チャンネルバッファをスワップ */
            channel_data_ = channel_data_new;
        }

        private void AddDecodeData(PacketObject packet, ProtocolDecodeData decode_data)
        {
            if (decode_data == null)return;

            /* 新しいデコードチャンネルを検出した場合はリストを拡張 */
            StretchChannelBufferList(decode_data.DataChannel);

            /* チャンネルバッファにデータを追加 */
            if (decode_data.DataChannel < (uint)channel_data_.Length) {
                channel_data_[decode_data.DataChannel].AddData(decode_data);
            }

            /* データ追加 */
            InputDataList(packet, decode_data);
        }

        private void AddDecodeData(PacketObject packet, ProtocolDecodeData[] decode_data_list)
        {
            if (decode_data_list == null)return;

            foreach (var decode_data in decode_data_list) {
                AddDecodeData(packet, decode_data);
            }
        }

        private void DecodePacket(PacketObject packet)
        {
            /* 受信データパケット以外は無視 */
            if (   (packet.Attribute != PacketAttribute.Data)
                || (packet.Direction != PacketDirection.Recv)
            ) {
                return;
            }

            /* デコーダーによる解析 */
            AddDecodeData(packet, decoder_.Decode(packet.MakeTime, packet.Data));
        }

        protected override void OnBackupProperty()
        {
            prop_ = Property as ViewPropertyImpl;

        }

        protected override void OnClearPacket()
        {
            UpdateDecoder();

            LView_DataList.ItemClear();

            next_item_no_ = 1;
        }

        protected override void OnDrawPacketBegin(bool auto_scroll)
        {
            /* ちらつき防止用の一時バッファ */
            list_items_temp_ = new List<ListViewItem>();

            /* リストビューの描画開始 */
            LView_DataList.BeginUpdate();
        }

        protected override void OnDrawPacketEnd(bool auto_scroll)
        {
            /* 一時リストをリストビューに追加 */
            LView_DataList.ItemAddRange(list_items_temp_);
            list_items_temp_ = null;

            /* 自動スクロール */
            if ((auto_scroll) && (LView_DataList.ItemCount > 0)) {
                LView_DataList.EnsureVisible(LView_DataList.ItemCount - 1);
            }

            /* リストビューの描画完了 */
            LView_DataList.EndUpdate();
        }

        protected override void OnDrawPacket(PacketObject packet)
        {
            DecodePacket(packet);
        }

        private void CBox_ProtocolType_SelectedIndexChanged(object sender, EventArgs e)
        {
            RedrawPacket();
        }

        private void LView_DataList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var decode_data = LView_DataList.FocusedItem.Tag as ProtocolDecodeData;

            if (decode_data == null)return;

            SetDataDetails(decode_data);
        }
    }
}
