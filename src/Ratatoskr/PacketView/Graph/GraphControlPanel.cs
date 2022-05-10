using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.PacketView.Graph.Configs;

namespace Ratatoskr.PacketView.Graph
{
    internal partial class GraphControlPanel : UserControl
    {
		private readonly RadioButton[] CH_RBTN_LIST;


        private PacketViewPropertyImpl prop_;

		private int				ch_no_ = -1;
		private ChannelConfig	ch_config_ = null;


        public event EventHandler SamplingSettingUpdated;
        public event EventHandler DisplaySettingUpdated;
        public event EventHandler ChannelSettingUpdated;


        public GraphControlPanel()
        {
            InitializeComponent();
            InitializeComboBox<GraphTargetType>(CBox_GraphTarget);
            InitializeComboBox<SamplingSettingTemplateType>(CBox_SamplingSettingTemplate);
            InitializeComboBox<SamplingTriggerType>(CBox_SamplingTrigger);
            InitializeComboBox<SamplingIntervalUnitType>(CBox_SamplingInterval_Unit);
            InitializeComboBox<DisplayModeType>(CBox_DisplayMode);
            InitializeComboBox<VertRangeType>(CBox_ChSet_Oscillo_Range);

			CH_RBTN_LIST = new RadioButton[]
			{
				RBtn_ChSet_CH1,
				RBtn_ChSet_CH2,
				RBtn_ChSet_CH3,
				RBtn_ChSet_CH4,
				RBtn_ChSet_CH5,
				RBtn_ChSet_CH6,
				RBtn_ChSet_CH7,
				RBtn_ChSet_CH8,
			};
        }

        private void InitializeComboBox<EnumType>(ComboBox control)
        {
            control.BeginUpdate();
            {
                control.Items.Clear();
                foreach (EnumType value in Enum.GetValues(typeof(EnumType))) {
                    control.Items.Add(value);
                }
                control.SelectedIndex = 0;
            }
            control.EndUpdate();
        }

        public void LoadConfig(PacketViewPropertyImpl prop)
        {
            prop_ = prop;

			/* --- Sampling Setting --- */
            CBox_GraphTarget.SelectedItem = prop_.GraphTarget.Value;

			CBox_SamplingTrigger.SelectedItem = prop_.SamplingTrigger.Value;
            Num_SamplingInterval_Value.Value = prop_.SamplingInterval.Value;
			CBox_SamplingInterval_Unit.SelectedItem = prop_.SamplingIntervalUnit.Value;
			Num_InputDataBlockSize.Value = prop_.InputDataBlockSize.Value;
            Num_InputDataChannelNum.Value = prop_.InputDataChannelNum.Value;

			/* --- Display Setting --- */
			CBox_DisplayMode.SelectedItem = prop_.DisplayMode.Value;
            Num_Oscillo_RecordPoint.Value = prop_.Oscillo_RecordPoint.Value;
            Num_Oscillo_DisplayPoint.Value = prop_.Oscillo_DisplayPoint.Value;

			/* --- Channel Setting --- */
			SetCurrentChannel(0);
			LoadCurrentChannelConfig();
        }

        private void LoadCurrentChannelConfig()
        {
            if (ch_config_ != null) {
                Btn_ChSet_Color.BackColor = ch_config_.ForeColor;
				Num_ValueBitSize.Value = ch_config_.ValueBitSize;
				ChkBox_ByteEndian_Reverse.Checked = ch_config_.ReverseByteEndian;
				ChkBox_BitEndian_Reverse.Checked = ch_config_.ReverseBitEndian;
				ChkBox_SignedValue.Checked = ch_config_.SignedValue;
				CBox_ChSet_Oscillo_Range.SelectedItem = ch_config_.OscilloVertRange;
				Num_ChSet_Oscillo_Range_Custom.Value = ch_config_.OscilloVertRangeCustom;
                TBar_ChSet_Oscillo_VertOffset.Value = (int)Math.Max((decimal)TBar_ChSet_Oscillo_VertOffset.Minimum, Math.Min((decimal)TBar_ChSet_Oscillo_VertOffset.Maximum, ch_config_.OscilloVertOffset));
            }
        }

        public void BackupConfig()
        {
            if (prop_ == null)return;

            BackupSamplingConfig();

            BackupDisplayConfig();

			BackupCurrentChannelConfig();
        }

        private void BackupSamplingConfig()
        {
            if (prop_ == null)return;

			prop_.GraphTarget.Value = (GraphTargetType)CBox_GraphTarget.SelectedItem;

			prop_.SamplingTrigger.Value = (SamplingTriggerType)CBox_SamplingTrigger.SelectedItem;
			prop_.SamplingInterval.Value = Num_SamplingInterval_Value.Value;
			prop_.SamplingIntervalUnit.Value = (SamplingIntervalUnitType)CBox_SamplingInterval_Unit.SelectedItem;
			prop_.InputDataBlockSize.Value = Num_InputDataBlockSize.Value;
			prop_.InputDataChannelNum.Value = Num_InputDataChannelNum.Value;
        }

        private void BackupDisplayConfig()
        {
            if (prop_ == null)return;

			prop_.DisplayMode.Value = (DisplayModeType)CBox_DisplayMode.SelectedItem;

            prop_.Oscillo_RecordPoint.Value = Num_Oscillo_RecordPoint.Value;
            prop_.Oscillo_DisplayPoint.Value = Num_Oscillo_DisplayPoint.Value;
        }
		
        private void BackupCurrentChannelConfig()
        {
			if (ch_config_ != null) {
				ch_config_.ForeColor = Btn_ChSet_Color.BackColor;
				ch_config_.ValueBitSize = (uint)Num_ValueBitSize.Value;
				ch_config_.ReverseByteEndian = ChkBox_ByteEndian_Reverse.Checked;
				ch_config_.ReverseBitEndian = ChkBox_BitEndian_Reverse.Checked;
				ch_config_.SignedValue = ChkBox_SignedValue.Checked;

				ch_config_.OscilloVertRange = (VertRangeType)CBox_ChSet_Oscillo_Range.SelectedItem;
				ch_config_.OscilloVertRangeCustom = (uint)Num_ChSet_Oscillo_Range_Custom.Value;
				ch_config_.OscilloVertOffset = TBar_ChSet_Oscillo_VertOffset.Value;
            }
        }

		private void SetCurrentChannel(int ch_no)
		{
			if ((ch_no >= 0) && (ch_no < CH_RBTN_LIST.Length)) {
				CH_RBTN_LIST[ch_no].Checked = true;

				ch_no_ = ch_no;
				ch_config_ = prop_.ChannelList.Value[ch_no_];
			}
		}

		private int GetCurrentChannel()
		{
			var index = 0;

			while (index < CH_RBTN_LIST.Length) {
				if (CH_RBTN_LIST[index].Checked) {
					break;
				}
				index++;
			}

			return (index);
		}

        private void OnSamplingSettingUpdated(object sender, EventArgs e)
        {
            BackupSamplingConfig();

            SamplingSettingUpdated?.Invoke(this, EventArgs.Empty);
        }

        private void OnDisplaySettingUpdated(object sender, EventArgs e)
        {
            BackupDisplayConfig();

            DisplaySettingUpdated?.Invoke(this, EventArgs.Empty);
        }

		private void OnChannelSettingUpdated(object sender, EventArgs e)
		{
			BackupCurrentChannelConfig();

			ChannelSettingUpdated?.Invoke(this, EventArgs.Empty);
		}

		private void Btn_SamplingSettingTemplate_Load_Click(object sender, EventArgs e)
		{
			var template_table = new []
			{
				/* PCM_8kHz_8bit_1ch */
				new {
					id					= SamplingSettingTemplateType.PCM_8kHz_8bit_1ch,
					sampling_ival		= (decimal)8,
					sampling_ival_unit	= SamplingIntervalUnitType.kHz,
					data_block_size		= 1,
					ch_num				= 1,
				},

				/* PCM_8kHz_8bit_2ch */
				new {
					id					= SamplingSettingTemplateType.PCM_8kHz_8bit_2ch,
					sampling_ival		= (decimal)8,
					sampling_ival_unit	= SamplingIntervalUnitType.kHz,
					data_block_size		= 2,
					ch_num				= 2,
				},

				/* PCM_8kHz_16bit_1ch */
				new {
					id					= SamplingSettingTemplateType.PCM_8kHz_16bit_1ch,
					sampling_ival		= (decimal)8,
					sampling_ival_unit	= SamplingIntervalUnitType.kHz,
					data_block_size		= 2,
					ch_num				= 1,
				},

				/* PCM_8kHz_16bit_2ch */
				new {
					id					= SamplingSettingTemplateType.PCM_8kHz_8bit_2ch,
					sampling_ival		= (decimal)8,
					sampling_ival_unit	= SamplingIntervalUnitType.kHz,
					data_block_size		= 4,
					ch_num				= 2,
				},

				/* PCM_44_1kHz_8bit_1ch */
				new {
					id					= SamplingSettingTemplateType.PCM_44_1kHz_8bit_1ch,
					sampling_ival		= (decimal)8,
					sampling_ival_unit	= SamplingIntervalUnitType.kHz,
					data_block_size		= 1,
					ch_num				= 1,
				},

				/* PCM_44_1kHz_8bit_2ch */
				new {
					id					= SamplingSettingTemplateType.PCM_44_1kHz_8bit_2ch,
					sampling_ival		= (decimal)8,
					sampling_ival_unit	= SamplingIntervalUnitType.kHz,
					data_block_size		= 2,
					ch_num				= 2,
				},

				/* PCM_44_1kHz_16bit_1ch */
				new {
					id					= SamplingSettingTemplateType.PCM_44_1kHz_16bit_1ch,
					sampling_ival		= (decimal)8,
					sampling_ival_unit	= SamplingIntervalUnitType.kHz,
					data_block_size		= 2,
					ch_num				= 1,
				},

				/* PCM_44_1kHz_16bit_2ch */
				new {
					id					= SamplingSettingTemplateType.PCM_44_1kHz_16bit_2ch,
					sampling_ival		= (decimal)8,
					sampling_ival_unit	= SamplingIntervalUnitType.kHz,
					data_block_size		= 4,
					ch_num				= 2,
				},

				/* PCM_48kHz_8bit_1ch */
				new {
					id					= SamplingSettingTemplateType.PCM_48kHz_8bit_1ch,
					sampling_ival		= (decimal)8,
					sampling_ival_unit	= SamplingIntervalUnitType.kHz,
					data_block_size		= 1,
					ch_num				= 1,
				},

				/* PCM_48kHz_8bit_2ch */
				new {
					id					= SamplingSettingTemplateType.PCM_48kHz_8bit_2ch,
					sampling_ival		= (decimal)8,
					sampling_ival_unit	= SamplingIntervalUnitType.kHz,
					data_block_size		= 2,
					ch_num				= 1,
				},

				/* PCM_48kHz_16bit_1ch */
				new {
					id					= SamplingSettingTemplateType.PCM_48kHz_16bit_1ch,
					sampling_ival		= (decimal)8,
					sampling_ival_unit	= SamplingIntervalUnitType.kHz,
					data_block_size		= 2,
					ch_num				= 1,
				},

				/* PCM_48kHz_16bit_2ch */
				new {
					id					= SamplingSettingTemplateType.PCM_48kHz_16bit_2ch,
					sampling_ival		= (decimal)8,
					sampling_ival_unit	= SamplingIntervalUnitType.kHz,
					data_block_size		= 4,
					ch_num				= 2,
				},
			};

			var select_id = (SamplingSettingTemplateType)CBox_SamplingSettingTemplate.SelectedItem;

			foreach (var table in template_table) {
				if (select_id == table.id) {
					Num_SamplingInterval_Value.Value = table.sampling_ival;
					CBox_SamplingInterval_Unit.SelectedItem = table.sampling_ival_unit;
					Num_InputDataBlockSize.Value = table.data_block_size;
					Num_InputDataChannelNum.Value = table.ch_num;
				}
			}
		}

		private void RBtn_ChSet_CH_Click(object sender, EventArgs e)
		{
			BackupCurrentChannelConfig();

			SetCurrentChannel(GetCurrentChannel());

			LoadCurrentChannelConfig();
		}

        private void Btn_ChSet_Color_Click(object sender, EventArgs e)
        {
			if (ch_config_ == null)return;

            var dialog = new ColorDialog();

            dialog.Color = ch_config_.ForeColor;

            if (dialog.ShowDialog() != DialogResult.OK)return;

            Btn_ChSet_Color.BackColor = dialog.Color;

			BackupCurrentChannelConfig();

            OnChannelSettingUpdated(sender, e);
        }
	}
}
