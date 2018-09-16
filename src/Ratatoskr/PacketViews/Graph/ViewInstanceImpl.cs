using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Ratatoskr.Generic;
using Ratatoskr.Packet;
using Ratatoskr.PacketViews.Graph.DisplayModules;
using Ratatoskr.PacketViews.Graph.DataCollectModules;
using Ratatoskr.PacketViews.Graph.DataFormatModules;

namespace Ratatoskr.PacketViews.Graph
{
    internal sealed class ViewInstanceImpl : ViewInstance
    {
        private ViewPropertyImpl prop_;

        private DisplayLayerParam[] layer_params_ = null;

        private DisplayModule     disp_mod_ = null;
        private DataCollectModule data_collect_mod_ = null;
        private DataFormatModule  data_format_mod_ = null;

        private Timer disp_update_timer_ = new Timer();

        private Panel panel1;
        private TrackBar TBar_GraphOffset;
        private PictureBox PBox_GraphDetails;
        private GraphControlPanel GCPanel_Main;
        private System.Windows.Forms.Panel Panel_Graph;


        private void InitializeComponent()
        {
            this.Panel_Graph = new System.Windows.Forms.Panel();
            this.GCPanel_Main = new Ratatoskr.PacketViews.Graph.GraphControlPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PBox_GraphDetails = new System.Windows.Forms.PictureBox();
            this.TBar_GraphOffset = new System.Windows.Forms.TrackBar();
            this.Panel_Graph.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBox_GraphDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBar_GraphOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel_Graph
            // 
            this.Panel_Graph.Controls.Add(this.GCPanel_Main);
            this.Panel_Graph.Controls.Add(this.panel1);
            this.Panel_Graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Graph.Location = new System.Drawing.Point(0, 0);
            this.Panel_Graph.Name = "Panel_Graph";
            this.Panel_Graph.Size = new System.Drawing.Size(1037, 576);
            this.Panel_Graph.TabIndex = 2;
            // 
            // GCPanel_Main
            // 
            this.GCPanel_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GCPanel_Main.Location = new System.Drawing.Point(528, 3);
            this.GCPanel_Main.Name = "GCPanel_Main";
            this.GCPanel_Main.Size = new System.Drawing.Size(506, 416);
            this.GCPanel_Main.TabIndex = 1;
            this.GCPanel_Main.SamplingSettingUpdated += new System.EventHandler(this.GCPanel_Main_SamplingSettingUpdated);
            this.GCPanel_Main.DisplaySettingUpdated += new System.EventHandler(this.GCPanel_Main_DisplaySettingUpdated);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.PBox_GraphDetails);
            this.panel1.Controls.Add(this.TBar_GraphOffset);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(519, 570);
            this.panel1.TabIndex = 0;
            // 
            // PBox_GraphDetails
            // 
            this.PBox_GraphDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(0)))));
            this.PBox_GraphDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PBox_GraphDetails.Location = new System.Drawing.Point(0, 0);
            this.PBox_GraphDetails.Name = "PBox_GraphDetails";
            this.PBox_GraphDetails.Size = new System.Drawing.Size(519, 525);
            this.PBox_GraphDetails.TabIndex = 3;
            this.PBox_GraphDetails.TabStop = false;
            this.PBox_GraphDetails.Paint += new System.Windows.Forms.PaintEventHandler(this.PBox_GraphDetails_Paint);
            this.PBox_GraphDetails.Resize += new System.EventHandler(this.PBox_GraphDetails_Resize);
            // 
            // TBar_GraphOffset
            // 
            this.TBar_GraphOffset.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TBar_GraphOffset.Location = new System.Drawing.Point(0, 525);
            this.TBar_GraphOffset.Maximum = 100;
            this.TBar_GraphOffset.Name = "TBar_GraphOffset";
            this.TBar_GraphOffset.Size = new System.Drawing.Size(519, 45);
            this.TBar_GraphOffset.TabIndex = 2;
            this.TBar_GraphOffset.TickFrequency = 0;
            this.TBar_GraphOffset.Value = 50;
            this.TBar_GraphOffset.Scroll += new System.EventHandler(this.TBar_GraphOffset_Scroll);
            // 
            // ViewInstanceImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.Panel_Graph);
            this.Name = "ViewInstanceImpl";
            this.Size = new System.Drawing.Size(1037, 576);
            this.Panel_Graph.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBox_GraphDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TBar_GraphOffset)).EndInit();
            this.ResumeLayout(false);

        }

        public ViewInstanceImpl() : base()
        {
            InitializeComponent();
        }

        public ViewInstanceImpl(ViewManager viewm, ViewClass viewd, ViewProperty viewp, Guid id) : base(viewm, viewd, viewp, id)
        {
            prop_ = viewp as ViewPropertyImpl;

            InitializeComponent();

            GCPanel_Main.LoadConfig(prop_);

            Disposed += OnDisposed;

            disp_update_timer_.Interval = 1000;
            disp_update_timer_.Tick += OnDispUpdateTimer;

            UpdateModule();
            UpdateLayerParam();
        }

        private void OnDispUpdateTimer(object sender, EventArgs e)
        {
            PBox_GraphDetails.Refresh();
        }

        private void OnDisposed(object sender, EventArgs e)
        {
        }

        protected override void OnBackupProperty()
        {
            var prop = Property as ViewPropertyImpl;

            GCPanel_Main.BackupConfig();
        }

        private void UpdateLayerParam()
        {
            var layer_params = new List<DisplayLayerParam>();
            var layer_param = (DisplayLayerParam)null;

            foreach (var conf in prop_.ChannelList.Value) {
                layer_params.Add(layer_param = new DisplayLayerParam());
                layer_param.ForeColor = conf.ForeColor;
                layer_param.AxisY_Offset = conf.Offset;
                layer_param.AxisY_Magnification = conf.Magnification;
            }

            layer_params_ = layer_params.ToArray();

            PBox_GraphDetails.Refresh();
        }

        private void UpdateTrackBar()
        {
            var track_value_max = (decimal)0;

            if (disp_mod_ != null) {
                track_value_max = (int)disp_mod_.PointCount;
            }

            track_value_max = (int)Math.Max(0, track_value_max - prop_.DisplayPoint.Value);

            if (TBar_GraphOffset.Maximum != track_value_max ) {
                TBar_GraphOffset.Maximum = (int)track_value_max;
                TBar_GraphOffset.Value = TBar_GraphOffset.Maximum;
                TBar_GraphOffset.Enabled = (TBar_GraphOffset.Maximum > 0);

                PBox_GraphDetails.Refresh();
            }
        }

        private void UpdateModule()
        {
            /* Display */
            switch (prop_.DisplayMode.Value) {
                case DisplayModeType.Oscillo:   disp_mod_ = new Display_Oscillo((uint)prop_.SamplingPoint.Value);  break;
                default:                        disp_mod_ = null;                   break;
            }

            /* Data Collect */
            switch (prop_.DataCollectMode.Value) {
                case DataCollectModeType.Value:    data_collect_mod_ = new DataCollect_Value((uint)prop_.DataChannelNum.Value, (uint)prop_.SamplingInterval.Value);    break;
                case DataCollectModeType.ValueSum: data_collect_mod_ = new DataCollect_ValueSum((uint)prop_.DataChannelNum.Value, (uint)prop_.SamplingInterval.Value);    break;
                case DataCollectModeType.Count:    data_collect_mod_ = new DataCollect_Count((uint)prop_.DataChannelNum.Value, (uint)prop_.SamplingInterval.Value);    break;
                default:                           data_collect_mod_ = null;                       break;
            }

            if (data_collect_mod_ != null) {
                data_collect_mod_.Sampled += OnDataSampled;
            }

            /* Data Format */
            switch (prop_.DataFormat.Value) {
                case DataFormatType.UnsignedByte:     data_format_mod_ = new DataFormat_UnsignedByte();                          break;
                case DataFormatType.UnsignedWord:     data_format_mod_ = new DataFormat_UnsignedWord(prop_.DataEndian.Value);    break;
                case DataFormatType.UnsignedDword:    data_format_mod_ = new DataFormat_UnsignedDword(prop_.DataEndian.Value);   break;
                case DataFormatType.UnsignedQword:    data_format_mod_ = new DataFormat_UnsignedQword(prop_.DataEndian.Value);   break;
                case DataFormatType.SignedByte:       data_format_mod_ = new DataFormat_SignedByte();                            break;
                case DataFormatType.SignedWord:       data_format_mod_ = new DataFormat_SignedWord(prop_.DataEndian.Value);      break;
                case DataFormatType.SignedDword:      data_format_mod_ = new DataFormat_SignedDword(prop_.DataEndian.Value);     break;
                case DataFormatType.SignedQword:      data_format_mod_ = new DataFormat_SignedQword(prop_.DataEndian.Value);     break;
                case DataFormatType.IEEE754_Float:    data_format_mod_ = new DataFormat_IEEE754_Float(prop_.DataEndian.Value);   break;
                case DataFormatType.IEEE754_Double:   data_format_mod_ = new DataFormat_IEEE754_Double(prop_.DataEndian.Value);  break;
                default:                              data_format_mod_ = null;                                                   break;
            }

            if (data_format_mod_ != null) {
                data_format_mod_.Extracted += OnDataExtracted;
            }

            UpdateLayerParam();
            UpdateTrackBar();

            PBox_GraphDetails.Refresh();
        }

        private void UpdateDisplay()
        {
            if (!disp_update_timer_.Enabled) {
                disp_update_timer_.Start();
            }
        }

        private void UpdateDisplayComplete()
        {
            if (disp_update_timer_.Enabled) {
                disp_update_timer_.Stop();
            }
        }

        private void OnDataExtracted(object sender, PacketObject base_packet, decimal data)
        {
            data_collect_mod_.InputData(base_packet, data);
        }

        private void OnDataSampled(object sender, decimal[] data)
        {
            disp_mod_.InputData(data);
        }

        protected override void OnIdle()
        {
            if (data_collect_mod_ != null) {
                data_collect_mod_.AutoUpdate = true;
            }
        }

        protected override void OnClearPacket()
        {
            UpdateModule();

            if (data_collect_mod_ != null) {
                data_collect_mod_.AutoUpdate = false;
            }
        }

        protected override void OnDrawPacketBegin(bool auto_scroll)
        {
        }

        protected override void OnDrawPacketEnd(bool auto_scroll, bool next_packet_exist)
        {
            PBox_GraphDetails.Refresh();
        }

        protected override void OnDrawPacket(PacketObject packet)
        {
            if (   (disp_mod_ == null)
                || (data_collect_mod_ == null)
                || (data_format_mod_ == null)
            ) {
                return;
            }

            /* データパケット以外は無視 */
            if (packet.Attribute != PacketAttribute.Data)return;

            /* データ収集 */
            data_format_mod_.InputData(packet, packet.Data);
        }

        private void PBox_GraphDetails_Paint(object sender, PaintEventArgs e)
        {
            disp_mod_?.DrawGraph(
                new DisplayParam(
                    e.Graphics,
                    PBox_GraphDetails.ClientRectangle,
                    (uint)TBar_GraphOffset.Value,
                    (uint)(TBar_GraphOffset.Value + prop_.DisplayPoint.Value),
                    prop_.AxisY_ValueMin.Value,
                    prop_.AxisY_ValueMax.Value),
                layer_params_);

            UpdateDisplayComplete();
        }

        private void TBar_GraphOffset_Scroll(object sender, EventArgs e)
        {
            PBox_GraphDetails.Refresh();
        }

        private void PBox_GraphDetails_Resize(object sender, EventArgs e)
        {
            PBox_GraphDetails.Refresh();
        }

        private void GCPanel_Main_DisplaySettingUpdated(object sender, EventArgs e)
        {
            UpdateLayerParam();
            UpdateTrackBar();
        }

        private void GCPanel_Main_SamplingSettingUpdated(object sender, EventArgs e)
        {
            UpdateModule();
        }
    }
}
