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
using Ratatoskr.PacketView.Graph.DataFormatModules;
using Ratatoskr.PacketView;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketView.Graph
{
    internal sealed class PacketViewInstanceImpl : PacketViewInstance
    {
        private PacketViewPropertyImpl prop_;

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
            this.GCPanel_Main = new Ratatoskr.PacketView.Graph.GraphControlPanel();
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
            this.GCPanel_Main.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GCPanel_Main.Location = new System.Drawing.Point(528, 3);
            this.GCPanel_Main.Name = "GCPanel_Main";
            this.GCPanel_Main.Size = new System.Drawing.Size(506, 570);
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
            ((System.ComponentModel.ISupportInitialize)(this.TBar_GraphOffset)).EndInit();
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

            GCPanel_Main.LoadConfig(prop_);

            Disposed += OnDisposed;

            disp_update_timer_.Interval = 1000;
            disp_update_timer_.Tick += OnDispUpdateTimer;

            UpdateModule();
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
            var prop = Property as PacketViewPropertyImpl;

            GCPanel_Main.BackupConfig();
        }

        private void UpdateTrackBar()
        {
            var track_value_max = (decimal)0;

            if (disp_mod_ != null) {
                track_value_max = (int)disp_mod_.PointCount;
            }

            track_value_max = (int)Math.Max(0, track_value_max - prop_.Oscillo_DisplayPoint.Value);

            if (TBar_GraphOffset.Maximum != track_value_max ) {
                TBar_GraphOffset.Maximum = (int)track_value_max;
                TBar_GraphOffset.Value = TBar_GraphOffset.Maximum;
                TBar_GraphOffset.Enabled = (TBar_GraphOffset.Maximum > 0);

                PBox_GraphDetails.Refresh();
            }
        }

        private void UpdateModule()
        {
			var setting_template = new []
			{
				new { format = SamplingSettingTemplateType.PCM_8kHz_8bit_1ch },
				new { format = SamplingSettingTemplateType.PCM_8kHz_8bit_2ch },
				new { format = SamplingSettingTemplateType.PCM_8kHz_16bit_1ch },
				new { format = SamplingSettingTemplateType.PCM_8kHz_16bit_2ch },
				new { format = SamplingSettingTemplateType.PCM_44_1kHz_8bit_1ch },
				new { format = SamplingSettingTemplateType.PCM_44_1kHz_8bit_2ch },
				new { format = SamplingSettingTemplateType.PCM_44_1kHz_16bit_1ch },
				new { format = SamplingSettingTemplateType.PCM_44_1kHz_16bit_2ch },
				new { format = SamplingSettingTemplateType.PCM_48kHz_8bit_1ch },
				new { format = SamplingSettingTemplateType.PCM_48kHz_8bit_2ch },
				new { format = SamplingSettingTemplateType.PCM_48kHz_16bit_1ch },
				new { format = SamplingSettingTemplateType.PCM_48kHz_16bit_2ch },
			};

			var input_data_type = prop_.InputDataType.Value;


            /* Data Format Module */
            switch (prop_.InputDataType.Value) {
                case DataType.UnsignedByte:     data_format_mod_ = new DataFormat_UnsignedByte(prop_);    break;
                case DataType.UnsignedWord:     data_format_mod_ = new DataFormat_UnsignedWord(prop_);    break;
                case DataType.UnsignedDword:    data_format_mod_ = new DataFormat_UnsignedDword(prop_);   break;
                case DataType.UnsignedQword:    data_format_mod_ = new DataFormat_UnsignedQword(prop_);   break;
                case DataType.SignedByte:       data_format_mod_ = new DataFormat_SignedByte(prop_);      break;
                case DataType.SignedWord:       data_format_mod_ = new DataFormat_SignedWord(prop_);      break;
                case DataType.SignedDword:      data_format_mod_ = new DataFormat_SignedDword(prop_);     break;
                case DataType.SignedQword:      data_format_mod_ = new DataFormat_SignedQword(prop_);     break;
                case DataType.IEEE754_Float:    data_format_mod_ = new DataFormat_IEEE754_Float(prop_);   break;
                case DataType.IEEE754_Double:   data_format_mod_ = new DataFormat_IEEE754_Double(prop_);  break;
                default:                        data_format_mod_ = null;                                  break;
            }

            if (data_format_mod_ != null) {
                data_format_mod_.Extracted += OnValueExtracted;
            }

			/* Data Collect Module */
			switch (prop_.GraphValue.Value)
			{
				case GraphValueType.DataValue:
					data_collect_mod_ = new DataCollect_Value(prop_);
					break;
				case GraphValueType.DataValueSum:
					data_collect_mod_ = new DataCollect_ValueSum(prop_);
					break;
				case GraphValueType.DataCount:
					data_collect_mod_ = new DataCollect_Count(prop_);
					break;
				default:
					data_collect_mod_ = null;
					break;
			}

			if (data_collect_mod_ != null) {
				data_collect_mod_.Sampled += OnValueSampled;
			}

			/* Display Module */
			switch (prop_.DisplayMode.Value) {
				case DisplayModeType.OscilloScope:
					disp_mod_ = new Display_Oscillo(prop_);
					break;
				default:
					disp_mod_ = null;
					break;
			}

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

        private void OnValueExtracted(object sender, decimal value)
        {
//			Debugger.DebugManager.MessageOut(value);

            data_collect_mod_.InputValue(value);
        }

        private void OnValueSampled(object sender, decimal[] value)
        {
			/* for Debug */
			if (value.Length > 0) {
				Debugger.DebugManager.MessageOut(string.Format("ValueSampled: {0}", value));
			}

            disp_mod_.InputValue(value);
        }

        protected override void OnIdle()
        {
            if (data_collect_mod_ != null) {
                data_collect_mod_.RealtimeMode = FormTaskManager.IsRedrawBusy;

				data_collect_mod_.Poll();
            }
        }

        protected override void OnClearPacket()
        {
            UpdateModule();

            if (data_collect_mod_ != null) {
                data_collect_mod_.RealtimeMode = false;
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

			data_collect_mod_.SetValueDatetime(packet.MakeTime);

            /* データ抽出 */
            data_format_mod_.InputData(packet.Data);
        }

        private void PBox_GraphDetails_Paint(object sender, PaintEventArgs e)
        {
			Debugger.DebugManager.MessageOut("Paint");

            disp_mod_?.DrawDisplay(
                new DisplayContext(
                    e.Graphics,
                    PBox_GraphDetails.ClientRectangle,
                    (uint)TBar_GraphOffset.Value,
                    prop_,
                    prop_.ChannelList.Value.ToArray())
            );

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

        private void GCPanel_Main_SamplingSettingUpdated(object sender, EventArgs e)
        {
            UpdateModule();
        }

        private void GCPanel_Main_DisplaySettingUpdated(object sender, EventArgs e)
        {
            UpdateTrackBar();
        }
    }
}
