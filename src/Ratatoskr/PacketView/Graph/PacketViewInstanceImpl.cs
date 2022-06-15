using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Forms;
using Ratatoskr.PacketView.Graph.DisplayModules;
using Ratatoskr.PacketView.Graph.DataCollectModules;
using Ratatoskr.PacketView;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketView.Graph
{
    internal sealed class PacketViewInstanceImpl : PacketViewInstance
    {
        private PacketViewPropertyImpl prop_;

        private DisplayModule		disp_mod_ = null;

        private DataCollectModule data_collect_mod_ = null;

        private Timer	graph_update_timer_ = new Timer();
		private bool	graph_update_req_ = false;

        private Panel panel1;
        private TrackBar TBar_GraphHorizontalOffset;
        private PictureBox PBox_GraphDetails;
        private GraphControlPanel GCPanel_Main;
        private System.Windows.Forms.Panel Panel_Graph;


        private void InitializeComponent()
        {
			this.Panel_Graph = new System.Windows.Forms.Panel();
			this.GCPanel_Main = new Ratatoskr.PacketView.Graph.GraphControlPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.PBox_GraphDetails = new System.Windows.Forms.PictureBox();
			this.TBar_GraphHorizontalOffset = new System.Windows.Forms.TrackBar();
			this.Panel_Graph.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PBox_GraphDetails)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.TBar_GraphHorizontalOffset)).BeginInit();
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
			this.GCPanel_Main.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GCPanel_Main.Location = new System.Drawing.Point(484, 3);
			this.GCPanel_Main.Name = "GCPanel_Main";
			this.GCPanel_Main.Size = new System.Drawing.Size(550, 570);
			this.GCPanel_Main.TabIndex = 1;
			this.GCPanel_Main.SamplingSettingUpdated += new System.EventHandler(this.GCPanel_Main_SamplingSettingUpdated);
			this.GCPanel_Main.DisplaySettingUpdated += new System.EventHandler(this.GCPanel_Main_DisplaySettingUpdated);
			this.GCPanel_Main.ChannelSettingUpdated += new System.EventHandler(this.GCPanel_Main_ChannelSettingUpdated);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.PBox_GraphDetails);
			this.panel1.Controls.Add(this.TBar_GraphHorizontalOffset);
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(475, 570);
			this.panel1.TabIndex = 0;
			// 
			// PBox_GraphDetails
			// 
			this.PBox_GraphDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(0)))));
			this.PBox_GraphDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PBox_GraphDetails.Location = new System.Drawing.Point(0, 0);
			this.PBox_GraphDetails.Name = "PBox_GraphDetails";
			this.PBox_GraphDetails.Size = new System.Drawing.Size(475, 525);
			this.PBox_GraphDetails.TabIndex = 3;
			this.PBox_GraphDetails.TabStop = false;
			this.PBox_GraphDetails.Paint += new System.Windows.Forms.PaintEventHandler(this.PBox_GraphDetails_Paint);
			this.PBox_GraphDetails.Resize += new System.EventHandler(this.PBox_GraphDetails_Resize);
			// 
			// TBar_GraphHorizontalOffset
			// 
			this.TBar_GraphHorizontalOffset.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.TBar_GraphHorizontalOffset.Location = new System.Drawing.Point(0, 525);
			this.TBar_GraphHorizontalOffset.Maximum = 100;
			this.TBar_GraphHorizontalOffset.Name = "TBar_GraphHorizontalOffset";
			this.TBar_GraphHorizontalOffset.Size = new System.Drawing.Size(475, 45);
			this.TBar_GraphHorizontalOffset.TabIndex = 2;
			this.TBar_GraphHorizontalOffset.TickFrequency = 0;
			this.TBar_GraphHorizontalOffset.Value = 50;
			this.TBar_GraphHorizontalOffset.Scroll += new System.EventHandler(this.TBar_GraphHorizontalOffset_Scroll);
			// 
			// PacketViewInstanceImpl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.Controls.Add(this.Panel_Graph);
			this.Name = "PacketViewInstanceImpl";
			this.Size = new System.Drawing.Size(1037, 576);
			this.Panel_Graph.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PBox_GraphDetails)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.TBar_GraphHorizontalOffset)).EndInit();
			this.ResumeLayout(false);

        }

        public PacketViewInstanceImpl() : base()
        {
            InitializeComponent();
        }

        public PacketViewInstanceImpl(PacketViewManager viewm, PacketViewClass viewd, PacketViewProperty viewp, Guid id) : base(viewm, viewd, viewp, id)
        {
            prop_ = viewp as PacketViewPropertyImpl;

            InitializeComponent();

            Disposed += OnDisposed;

            GCPanel_Main.LoadConfig(prop_);

            graph_update_timer_.Interval = 100;
            graph_update_timer_.Tick += OnUpdateGraphRequestTimer;

			UpdateGraphRequest();
            UpdateModule();
        }

        private void OnDisposed(object sender, EventArgs e)
        {
			if (disp_mod_ != null) {
				disp_mod_.Dispose();
				disp_mod_ = null;
			}
        }

        protected override void OnBackupProperty()
        {
            var prop = Property as PacketViewPropertyImpl;

            GCPanel_Main.BackupConfig();
        }

        private void UpdateModule()
        {
			/* Data Collect Module */
			switch (prop_.GraphTarget.Value)
			{
				case GraphTargetType.DataValue:
					data_collect_mod_ = new DataCollect_Value(prop_);
					break;
				case GraphTargetType.DataValueSum:
					data_collect_mod_ = new DataCollect_ValueSum(prop_);
					break;
				case GraphTargetType.DataBlockCount:
					data_collect_mod_ = new DataCollect_BlockCount(prop_);
					break;
				default:
					data_collect_mod_ = null;
					break;
			}

			if (data_collect_mod_ != null) {
				data_collect_mod_.Sampled += OnValueSampled;
			}

			/* Display Module */
			disp_mod_?.Dispose();

			switch (prop_.DisplayMode.Value) {
				case DisplayModeType.OscilloScope:
					disp_mod_ = new Display_Oscillo();
					break;
				default:
					disp_mod_ = null;
					break;
			}

            UpdateDisplayConfig();
        }

        private void UpdateDisplayTrackBar()
        {
            var track_value_max = (decimal)0;

            if (disp_mod_ != null) {
                track_value_max = (int)disp_mod_.PointCount;
            }

            track_value_max = (int)Math.Max(0, track_value_max - prop_.Oscillo_DisplayPoint.Value);

            if (TBar_GraphHorizontalOffset.Maximum != track_value_max ) {
                TBar_GraphHorizontalOffset.Maximum = (int)track_value_max;
                TBar_GraphHorizontalOffset.Value = TBar_GraphHorizontalOffset.Maximum;
                TBar_GraphHorizontalOffset.Enabled = (TBar_GraphHorizontalOffset.Maximum > 0);
            }
        }

		private void UpdateDisplayConfig()
		{
			UpdateDisplayTrackBar();

			if (disp_mod_ != null) {
				disp_mod_.SetDisplayConfig(new DisplayConfig()
				{
					GraphRecordPoint  = (uint)prop_.Oscillo_RecordPoint.Value,
					GraphDisplayPoint = (uint)prop_.Oscillo_DisplayPoint.Value,
					GraphDisplayOffset = (uint)TBar_GraphHorizontalOffset.Value,
					GraphChannelConfigs = (data_collect_mod_ != null) ? (data_collect_mod_.ChannelConfigs) : (null),
					DisplayRect = PBox_GraphDetails.ClientRectangle
				});

				UpdateDisplayTrackBar();
			}

			UpdateGraph();
		}

		private void UpdateGraphRequest()
		{
            if (!graph_update_timer_.Enabled) {
                graph_update_timer_.Start();
            }
		}

		private void UpdateGraphRequestCancel()
		{
            if (graph_update_timer_.Enabled) {
                graph_update_timer_.Stop();
            }
		}

		public void UpdateGraph()
		{
            PBox_GraphDetails.Refresh();
		}

        private void OnUpdateGraphRequestTimer(object sender, EventArgs e)
        {
			graph_update_req_ = true;

            UpdateGraph();
        }

        private void OnValueSampled(object sender, long[] value)
        {
            disp_mod_.InputValue(value);
        }

        protected override void OnIdle()
        {
			data_collect_mod_?.Poll();
        }

        protected override void OnClearPacket()
        {
            UpdateModule();
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
            ) {
                return;
            }

            /* データパケット以外は無視 */
            if (packet.Attribute != PacketAttribute.Data)return;

            /* データ解析 */
            data_collect_mod_.InputData(packet.MakeTime, packet.Data);
        }

		private void PBox_GraphDetails_Paint(object sender, PaintEventArgs e)
		{
			Debugger.DebugManager.MessageOut("Paint Begin");

			disp_mod_?.DrawDisplay(new DisplayContext()
			{
				Canvas = e.Graphics,
			});

			graph_update_req_ = false;

			Debugger.DebugManager.MessageOut("Paint End");
        }

        private void TBar_GraphHorizontalOffset_Scroll(object sender, EventArgs e)
        {
            UpdateDisplayConfig();
        }

        private void PBox_GraphDetails_Resize(object sender, EventArgs e)
        {
            UpdateDisplayConfig();
        }

        private void GCPanel_Main_SamplingSettingUpdated(object sender, EventArgs e)
        {
            UpdateModule();
        }

        private void GCPanel_Main_DisplaySettingUpdated(object sender, EventArgs e)
        {
			UpdateDisplayConfig();
        }

        private void GCPanel_Main_ChannelSettingUpdated(object sender, EventArgs e)
        {
			UpdateDisplayConfig();
        }
    }
}
