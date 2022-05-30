namespace Ratatoskr.PacketView.Graph
{
    partial class GraphControlPanel
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.CBox_GraphTarget = new System.Windows.Forms.ComboBox();
			this.groupBox9 = new System.Windows.Forms.GroupBox();
			this.RBtn_ChSet_CH8 = new System.Windows.Forms.RadioButton();
			this.RBtn_ChSet_CH7 = new System.Windows.Forms.RadioButton();
			this.RBtn_ChSet_CH6 = new System.Windows.Forms.RadioButton();
			this.RBtn_ChSet_CH5 = new System.Windows.Forms.RadioButton();
			this.RBtn_ChSet_CH4 = new System.Windows.Forms.RadioButton();
			this.RBtn_ChSet_CH3 = new System.Windows.Forms.RadioButton();
			this.RBtn_ChSet_CH2 = new System.Windows.Forms.RadioButton();
			this.RBtn_ChSet_CH1 = new System.Windows.Forms.RadioButton();
			this.Btn_ChSet_Color = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox13 = new System.Windows.Forms.GroupBox();
			this.TBar_ChSet_Oscillo_VertOffset = new System.Windows.Forms.TrackBar();
			this.label8 = new System.Windows.Forms.Label();
			this.CBox_ChSet_Oscillo_Range = new System.Windows.Forms.ComboBox();
			this.Num_ChSet_Oscillo_Range_Custom = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.CBox_SamplingInterval_Unit = new System.Windows.Forms.ComboBox();
			this.Num_SamplingInterval_Value = new System.Windows.Forms.NumericUpDown();
			this.groupBox8 = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.Num_Oscillo_DisplayPoint = new System.Windows.Forms.NumericUpDown();
			this.Num_Oscillo_RecordPoint = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox19 = new System.Windows.Forms.GroupBox();
			this.Btn_SamplingSettingTemplate_Load = new System.Windows.Forms.Button();
			this.CBox_SamplingSettingTemplate = new System.Windows.Forms.ComboBox();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.Num_InputDataBlockSize = new System.Windows.Forms.NumericUpDown();
			this.label15 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.Num_InputDataChannelNum = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.CBox_DisplayMode = new System.Windows.Forms.ComboBox();
			this.Btn_SamplingSetting_Apply = new System.Windows.Forms.Button();
			this.label11 = new System.Windows.Forms.Label();
			this.Btn_DisplaySetting_Apply = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.ChkBox_Visible = new System.Windows.Forms.CheckBox();
			this.ChkBox_SignedValue = new System.Windows.Forms.CheckBox();
			this.label14 = new System.Windows.Forms.Label();
			this.ChkBox_BitEndian_Reverse = new System.Windows.Forms.CheckBox();
			this.label12 = new System.Windows.Forms.Label();
			this.Num_ValueBitSize = new System.Windows.Forms.NumericUpDown();
			this.label13 = new System.Windows.Forms.Label();
			this.ChkBox_ByteEndian_Reverse = new System.Windows.Forms.CheckBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.CBox_SamplingTrigger = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox9.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox13.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TBar_ChSet_Oscillo_VertOffset)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_ChSet_Oscillo_Range_Custom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_SamplingInterval_Value)).BeginInit();
			this.groupBox8.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Num_Oscillo_DisplayPoint)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_Oscillo_RecordPoint)).BeginInit();
			this.groupBox19.SuspendLayout();
			this.groupBox6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Num_InputDataBlockSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_InputDataChannelNum)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Num_ValueBitSize)).BeginInit();
			this.groupBox5.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.CBox_GraphTarget);
			this.groupBox1.Location = new System.Drawing.Point(3, 31);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(120, 42);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Target";
			// 
			// CBox_GraphTarget
			// 
			this.CBox_GraphTarget.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CBox_GraphTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBox_GraphTarget.FormattingEnabled = true;
			this.CBox_GraphTarget.Location = new System.Drawing.Point(3, 15);
			this.CBox_GraphTarget.Name = "CBox_GraphTarget";
			this.CBox_GraphTarget.Size = new System.Drawing.Size(114, 20);
			this.CBox_GraphTarget.TabIndex = 1;
			// 
			// groupBox9
			// 
			this.groupBox9.Controls.Add(this.RBtn_ChSet_CH8);
			this.groupBox9.Controls.Add(this.RBtn_ChSet_CH7);
			this.groupBox9.Controls.Add(this.RBtn_ChSet_CH6);
			this.groupBox9.Controls.Add(this.RBtn_ChSet_CH5);
			this.groupBox9.Controls.Add(this.RBtn_ChSet_CH4);
			this.groupBox9.Controls.Add(this.RBtn_ChSet_CH3);
			this.groupBox9.Controls.Add(this.RBtn_ChSet_CH2);
			this.groupBox9.Controls.Add(this.RBtn_ChSet_CH1);
			this.groupBox9.Location = new System.Drawing.Point(6, 290);
			this.groupBox9.Name = "groupBox9";
			this.groupBox9.Size = new System.Drawing.Size(69, 197);
			this.groupBox9.TabIndex = 21;
			this.groupBox9.TabStop = false;
			this.groupBox9.Text = "Select";
			// 
			// RBtn_ChSet_CH8
			// 
			this.RBtn_ChSet_CH8.AutoSize = true;
			this.RBtn_ChSet_CH8.Location = new System.Drawing.Point(13, 172);
			this.RBtn_ChSet_CH8.Name = "RBtn_ChSet_CH8";
			this.RBtn_ChSet_CH8.Size = new System.Drawing.Size(45, 16);
			this.RBtn_ChSet_CH8.TabIndex = 7;
			this.RBtn_ChSet_CH8.TabStop = true;
			this.RBtn_ChSet_CH8.Text = "CH8";
			this.RBtn_ChSet_CH8.UseVisualStyleBackColor = true;
			this.RBtn_ChSet_CH8.Click += new System.EventHandler(this.RBtn_ChSet_CH_Click);
			// 
			// RBtn_ChSet_CH7
			// 
			this.RBtn_ChSet_CH7.AutoSize = true;
			this.RBtn_ChSet_CH7.Location = new System.Drawing.Point(13, 150);
			this.RBtn_ChSet_CH7.Name = "RBtn_ChSet_CH7";
			this.RBtn_ChSet_CH7.Size = new System.Drawing.Size(45, 16);
			this.RBtn_ChSet_CH7.TabIndex = 6;
			this.RBtn_ChSet_CH7.TabStop = true;
			this.RBtn_ChSet_CH7.Text = "CH7";
			this.RBtn_ChSet_CH7.UseVisualStyleBackColor = true;
			this.RBtn_ChSet_CH7.Click += new System.EventHandler(this.RBtn_ChSet_CH_Click);
			// 
			// RBtn_ChSet_CH6
			// 
			this.RBtn_ChSet_CH6.AutoSize = true;
			this.RBtn_ChSet_CH6.Location = new System.Drawing.Point(13, 128);
			this.RBtn_ChSet_CH6.Name = "RBtn_ChSet_CH6";
			this.RBtn_ChSet_CH6.Size = new System.Drawing.Size(45, 16);
			this.RBtn_ChSet_CH6.TabIndex = 5;
			this.RBtn_ChSet_CH6.TabStop = true;
			this.RBtn_ChSet_CH6.Text = "CH6";
			this.RBtn_ChSet_CH6.UseVisualStyleBackColor = true;
			this.RBtn_ChSet_CH6.Click += new System.EventHandler(this.RBtn_ChSet_CH_Click);
			// 
			// RBtn_ChSet_CH5
			// 
			this.RBtn_ChSet_CH5.AutoSize = true;
			this.RBtn_ChSet_CH5.Location = new System.Drawing.Point(13, 106);
			this.RBtn_ChSet_CH5.Name = "RBtn_ChSet_CH5";
			this.RBtn_ChSet_CH5.Size = new System.Drawing.Size(45, 16);
			this.RBtn_ChSet_CH5.TabIndex = 4;
			this.RBtn_ChSet_CH5.TabStop = true;
			this.RBtn_ChSet_CH5.Text = "CH5";
			this.RBtn_ChSet_CH5.UseVisualStyleBackColor = true;
			this.RBtn_ChSet_CH5.Click += new System.EventHandler(this.RBtn_ChSet_CH_Click);
			// 
			// RBtn_ChSet_CH4
			// 
			this.RBtn_ChSet_CH4.AutoSize = true;
			this.RBtn_ChSet_CH4.Location = new System.Drawing.Point(13, 84);
			this.RBtn_ChSet_CH4.Name = "RBtn_ChSet_CH4";
			this.RBtn_ChSet_CH4.Size = new System.Drawing.Size(45, 16);
			this.RBtn_ChSet_CH4.TabIndex = 3;
			this.RBtn_ChSet_CH4.TabStop = true;
			this.RBtn_ChSet_CH4.Text = "CH4";
			this.RBtn_ChSet_CH4.UseVisualStyleBackColor = true;
			this.RBtn_ChSet_CH4.Click += new System.EventHandler(this.RBtn_ChSet_CH_Click);
			// 
			// RBtn_ChSet_CH3
			// 
			this.RBtn_ChSet_CH3.AutoSize = true;
			this.RBtn_ChSet_CH3.Location = new System.Drawing.Point(13, 62);
			this.RBtn_ChSet_CH3.Name = "RBtn_ChSet_CH3";
			this.RBtn_ChSet_CH3.Size = new System.Drawing.Size(45, 16);
			this.RBtn_ChSet_CH3.TabIndex = 2;
			this.RBtn_ChSet_CH3.TabStop = true;
			this.RBtn_ChSet_CH3.Text = "CH3";
			this.RBtn_ChSet_CH3.UseVisualStyleBackColor = true;
			this.RBtn_ChSet_CH3.Click += new System.EventHandler(this.RBtn_ChSet_CH_Click);
			// 
			// RBtn_ChSet_CH2
			// 
			this.RBtn_ChSet_CH2.AutoSize = true;
			this.RBtn_ChSet_CH2.Location = new System.Drawing.Point(13, 40);
			this.RBtn_ChSet_CH2.Name = "RBtn_ChSet_CH2";
			this.RBtn_ChSet_CH2.Size = new System.Drawing.Size(45, 16);
			this.RBtn_ChSet_CH2.TabIndex = 1;
			this.RBtn_ChSet_CH2.TabStop = true;
			this.RBtn_ChSet_CH2.Text = "CH2";
			this.RBtn_ChSet_CH2.UseVisualStyleBackColor = true;
			this.RBtn_ChSet_CH2.Click += new System.EventHandler(this.RBtn_ChSet_CH_Click);
			// 
			// RBtn_ChSet_CH1
			// 
			this.RBtn_ChSet_CH1.AutoSize = true;
			this.RBtn_ChSet_CH1.Location = new System.Drawing.Point(13, 18);
			this.RBtn_ChSet_CH1.Name = "RBtn_ChSet_CH1";
			this.RBtn_ChSet_CH1.Size = new System.Drawing.Size(45, 16);
			this.RBtn_ChSet_CH1.TabIndex = 0;
			this.RBtn_ChSet_CH1.TabStop = true;
			this.RBtn_ChSet_CH1.Text = "CH1";
			this.RBtn_ChSet_CH1.UseVisualStyleBackColor = true;
			this.RBtn_ChSet_CH1.Click += new System.EventHandler(this.RBtn_ChSet_CH_Click);
			// 
			// Btn_ChSet_Color
			// 
			this.Btn_ChSet_Color.BackColor = System.Drawing.Color.Yellow;
			this.Btn_ChSet_Color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Btn_ChSet_Color.Location = new System.Drawing.Point(112, 40);
			this.Btn_ChSet_Color.Name = "Btn_ChSet_Color";
			this.Btn_ChSet_Color.Size = new System.Drawing.Size(48, 19);
			this.Btn_ChSet_Color.TabIndex = 0;
			this.Btn_ChSet_Color.UseVisualStyleBackColor = false;
			this.Btn_ChSet_Color.Click += new System.EventHandler(this.Btn_ChSet_Color_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.groupBox13);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.CBox_ChSet_Oscillo_Range);
			this.groupBox2.Controls.Add(this.Num_ChSet_Oscillo_Range_Custom);
			this.groupBox2.Location = new System.Drawing.Point(291, 290);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(243, 139);
			this.groupBox2.TabIndex = 18;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Oscilloscope Setting";
			// 
			// groupBox13
			// 
			this.groupBox13.Controls.Add(this.TBar_ChSet_Oscillo_VertOffset);
			this.groupBox13.Location = new System.Drawing.Point(192, 18);
			this.groupBox13.Name = "groupBox13";
			this.groupBox13.Size = new System.Drawing.Size(45, 115);
			this.groupBox13.TabIndex = 21;
			this.groupBox13.TabStop = false;
			this.groupBox13.Text = "Pos";
			// 
			// TBar_ChSet_Oscillo_VertOffset
			// 
			this.TBar_ChSet_Oscillo_VertOffset.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TBar_ChSet_Oscillo_VertOffset.Location = new System.Drawing.Point(3, 15);
			this.TBar_ChSet_Oscillo_VertOffset.Maximum = 500;
			this.TBar_ChSet_Oscillo_VertOffset.Minimum = -500;
			this.TBar_ChSet_Oscillo_VertOffset.Name = "TBar_ChSet_Oscillo_VertOffset";
			this.TBar_ChSet_Oscillo_VertOffset.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.TBar_ChSet_Oscillo_VertOffset.Size = new System.Drawing.Size(39, 97);
			this.TBar_ChSet_Oscillo_VertOffset.TabIndex = 13;
			this.TBar_ChSet_Oscillo_VertOffset.TickFrequency = 100;
			this.TBar_ChSet_Oscillo_VertOffset.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.TBar_ChSet_Oscillo_VertOffset.ValueChanged += new System.EventHandler(this.OnChannelSettingUpdated);
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(121, 43);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(65, 19);
			this.label8.TabIndex = 19;
			this.label8.Text = "Value/DIV";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CBox_ChSet_Oscillo_Range
			// 
			this.CBox_ChSet_Oscillo_Range.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBox_ChSet_Oscillo_Range.FormattingEnabled = true;
			this.CBox_ChSet_Oscillo_Range.Location = new System.Drawing.Point(6, 18);
			this.CBox_ChSet_Oscillo_Range.Name = "CBox_ChSet_Oscillo_Range";
			this.CBox_ChSet_Oscillo_Range.Size = new System.Drawing.Size(180, 20);
			this.CBox_ChSet_Oscillo_Range.TabIndex = 18;
			this.CBox_ChSet_Oscillo_Range.SelectedIndexChanged += new System.EventHandler(this.OnChannelSettingUpdated);
			// 
			// Num_ChSet_Oscillo_Range_Custom
			// 
			this.Num_ChSet_Oscillo_Range_Custom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Num_ChSet_Oscillo_Range_Custom.Location = new System.Drawing.Point(6, 44);
			this.Num_ChSet_Oscillo_Range_Custom.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
			this.Num_ChSet_Oscillo_Range_Custom.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.Num_ChSet_Oscillo_Range_Custom.Name = "Num_ChSet_Oscillo_Range_Custom";
			this.Num_ChSet_Oscillo_Range_Custom.Size = new System.Drawing.Size(109, 19);
			this.Num_ChSet_Oscillo_Range_Custom.TabIndex = 12;
			this.Num_ChSet_Oscillo_Range_Custom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.Num_ChSet_Oscillo_Range_Custom.ThousandsSeparator = true;
			this.Num_ChSet_Oscillo_Range_Custom.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.Num_ChSet_Oscillo_Range_Custom.ValueChanged += new System.EventHandler(this.OnChannelSettingUpdated);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label3.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label3.Location = new System.Drawing.Point(3, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(544, 28);
			this.label3.TabIndex = 11;
			this.label3.Text = "Sampling Setting";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
			this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label2.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label2.Location = new System.Drawing.Point(3, 154);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(544, 28);
			this.label2.TabIndex = 12;
			this.label2.Text = "Display Setting";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// CBox_SamplingInterval_Unit
			// 
			this.CBox_SamplingInterval_Unit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBox_SamplingInterval_Unit.FormattingEnabled = true;
			this.CBox_SamplingInterval_Unit.Location = new System.Drawing.Point(228, 44);
			this.CBox_SamplingInterval_Unit.Name = "CBox_SamplingInterval_Unit";
			this.CBox_SamplingInterval_Unit.Size = new System.Drawing.Size(60, 20);
			this.CBox_SamplingInterval_Unit.TabIndex = 17;
			// 
			// Num_SamplingInterval_Value
			// 
			this.Num_SamplingInterval_Value.DecimalPlaces = 1;
			this.Num_SamplingInterval_Value.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Num_SamplingInterval_Value.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.Num_SamplingInterval_Value.Location = new System.Drawing.Point(122, 44);
			this.Num_SamplingInterval_Value.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.Num_SamplingInterval_Value.Name = "Num_SamplingInterval_Value";
			this.Num_SamplingInterval_Value.Size = new System.Drawing.Size(100, 19);
			this.Num_SamplingInterval_Value.TabIndex = 0;
			this.Num_SamplingInterval_Value.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.Num_SamplingInterval_Value.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// groupBox8
			// 
			this.groupBox8.Controls.Add(this.label7);
			this.groupBox8.Controls.Add(this.Num_Oscillo_DisplayPoint);
			this.groupBox8.Controls.Add(this.Num_Oscillo_RecordPoint);
			this.groupBox8.Controls.Add(this.label6);
			this.groupBox8.Location = new System.Drawing.Point(139, 185);
			this.groupBox8.Name = "groupBox8";
			this.groupBox8.Size = new System.Drawing.Size(222, 71);
			this.groupBox8.TabIndex = 14;
			this.groupBox8.TabStop = false;
			this.groupBox8.Text = "Oscilloscope Setting";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(9, 43);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 19);
			this.label7.TabIndex = 19;
			this.label7.Text = "Display Point";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Num_Oscillo_DisplayPoint
			// 
			this.Num_Oscillo_DisplayPoint.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Num_Oscillo_DisplayPoint.Location = new System.Drawing.Point(115, 43);
			this.Num_Oscillo_DisplayPoint.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.Num_Oscillo_DisplayPoint.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.Num_Oscillo_DisplayPoint.Name = "Num_Oscillo_DisplayPoint";
			this.Num_Oscillo_DisplayPoint.Size = new System.Drawing.Size(94, 19);
			this.Num_Oscillo_DisplayPoint.TabIndex = 20;
			this.Num_Oscillo_DisplayPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.Num_Oscillo_DisplayPoint.ThousandsSeparator = true;
			this.Num_Oscillo_DisplayPoint.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// Num_Oscillo_RecordPoint
			// 
			this.Num_Oscillo_RecordPoint.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Num_Oscillo_RecordPoint.Location = new System.Drawing.Point(115, 18);
			this.Num_Oscillo_RecordPoint.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.Num_Oscillo_RecordPoint.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.Num_Oscillo_RecordPoint.Name = "Num_Oscillo_RecordPoint";
			this.Num_Oscillo_RecordPoint.Size = new System.Drawing.Size(94, 19);
			this.Num_Oscillo_RecordPoint.TabIndex = 19;
			this.Num_Oscillo_RecordPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.Num_Oscillo_RecordPoint.ThousandsSeparator = true;
			this.Num_Oscillo_RecordPoint.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(9, 18);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 19);
			this.label6.TabIndex = 18;
			this.label6.Text = "Record Point";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox19
			// 
			this.groupBox19.Controls.Add(this.Btn_SamplingSettingTemplate_Load);
			this.groupBox19.Controls.Add(this.CBox_SamplingSettingTemplate);
			this.groupBox19.Location = new System.Drawing.Point(129, 31);
			this.groupBox19.Name = "groupBox19";
			this.groupBox19.Size = new System.Drawing.Size(320, 42);
			this.groupBox19.TabIndex = 17;
			this.groupBox19.TabStop = false;
			this.groupBox19.Text = "Template";
			// 
			// Btn_SamplingSettingTemplate_Load
			// 
			this.Btn_SamplingSettingTemplate_Load.Location = new System.Drawing.Point(275, 14);
			this.Btn_SamplingSettingTemplate_Load.Name = "Btn_SamplingSettingTemplate_Load";
			this.Btn_SamplingSettingTemplate_Load.Size = new System.Drawing.Size(40, 22);
			this.Btn_SamplingSettingTemplate_Load.TabIndex = 1;
			this.Btn_SamplingSettingTemplate_Load.Text = "Load";
			this.Btn_SamplingSettingTemplate_Load.UseVisualStyleBackColor = true;
			this.Btn_SamplingSettingTemplate_Load.Click += new System.EventHandler(this.Btn_SamplingSettingTemplate_Load_Click);
			// 
			// CBox_SamplingSettingTemplate
			// 
			this.CBox_SamplingSettingTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBox_SamplingSettingTemplate.FormattingEnabled = true;
			this.CBox_SamplingSettingTemplate.Location = new System.Drawing.Point(3, 15);
			this.CBox_SamplingSettingTemplate.Name = "CBox_SamplingSettingTemplate";
			this.CBox_SamplingSettingTemplate.Size = new System.Drawing.Size(265, 20);
			this.CBox_SamplingSettingTemplate.TabIndex = 0;
			this.CBox_SamplingSettingTemplate.SelectedIndexChanged += new System.EventHandler(this.OnSamplingSettingUpdated);
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.Num_InputDataBlockSize);
			this.groupBox6.Controls.Add(this.label15);
			this.groupBox6.Controls.Add(this.label10);
			this.groupBox6.Controls.Add(this.Num_InputDataChannelNum);
			this.groupBox6.Controls.Add(this.label5);
			this.groupBox6.Location = new System.Drawing.Point(3, 79);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(225, 72);
			this.groupBox6.TabIndex = 5;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Input Data Format";
			// 
			// Num_InputDataBlockSize
			// 
			this.Num_InputDataBlockSize.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Num_InputDataBlockSize.Location = new System.Drawing.Point(122, 15);
			this.Num_InputDataBlockSize.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
			this.Num_InputDataBlockSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.Num_InputDataBlockSize.Name = "Num_InputDataBlockSize";
			this.Num_InputDataBlockSize.Size = new System.Drawing.Size(60, 19);
			this.Num_InputDataBlockSize.TabIndex = 25;
			this.Num_InputDataBlockSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.Num_InputDataBlockSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(6, 15);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(100, 19);
			this.label15.TabIndex = 26;
			this.label15.Text = "Data Block Size";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(188, 15);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(34, 19);
			this.label10.TabIndex = 22;
			this.label10.Text = "byte";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Num_InputDataChannelNum
			// 
			this.Num_InputDataChannelNum.Location = new System.Drawing.Point(121, 40);
			this.Num_InputDataChannelNum.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.Num_InputDataChannelNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.Num_InputDataChannelNum.Name = "Num_InputDataChannelNum";
			this.Num_InputDataChannelNum.Size = new System.Drawing.Size(60, 19);
			this.Num_InputDataChannelNum.TabIndex = 8;
			this.Num_InputDataChannelNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.Num_InputDataChannelNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 39);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(110, 19);
			this.label5.TabIndex = 7;
			this.label5.Text = "Data Channel Num";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(6, 44);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(100, 19);
			this.label17.TabIndex = 27;
			this.label17.Text = "Sampling Interval";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.CBox_DisplayMode);
			this.groupBox3.Location = new System.Drawing.Point(3, 185);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(130, 43);
			this.groupBox3.TabIndex = 19;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Display Mode";
			// 
			// CBox_DisplayMode
			// 
			this.CBox_DisplayMode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CBox_DisplayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBox_DisplayMode.FormattingEnabled = true;
			this.CBox_DisplayMode.Location = new System.Drawing.Point(3, 15);
			this.CBox_DisplayMode.Name = "CBox_DisplayMode";
			this.CBox_DisplayMode.Size = new System.Drawing.Size(124, 20);
			this.CBox_DisplayMode.TabIndex = 0;
			// 
			// Btn_SamplingSetting_Apply
			// 
			this.Btn_SamplingSetting_Apply.Location = new System.Drawing.Point(145, 4);
			this.Btn_SamplingSetting_Apply.Name = "Btn_SamplingSetting_Apply";
			this.Btn_SamplingSetting_Apply.Size = new System.Drawing.Size(70, 21);
			this.Btn_SamplingSetting_Apply.TabIndex = 21;
			this.Btn_SamplingSetting_Apply.Text = "Apply";
			this.Btn_SamplingSetting_Apply.UseVisualStyleBackColor = true;
			this.Btn_SamplingSetting_Apply.Click += new System.EventHandler(this.OnSamplingSettingUpdated);
			// 
			// label11
			// 
			this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label11.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.label11.Location = new System.Drawing.Point(3, 259);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(544, 28);
			this.label11.TabIndex = 22;
			this.label11.Text = "Channel Setting";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Btn_DisplaySetting_Apply
			// 
			this.Btn_DisplaySetting_Apply.Location = new System.Drawing.Point(145, 158);
			this.Btn_DisplaySetting_Apply.Name = "Btn_DisplaySetting_Apply";
			this.Btn_DisplaySetting_Apply.Size = new System.Drawing.Size(70, 21);
			this.Btn_DisplaySetting_Apply.TabIndex = 23;
			this.Btn_DisplaySetting_Apply.Text = "Apply";
			this.Btn_DisplaySetting_Apply.UseVisualStyleBackColor = true;
			this.Btn_DisplaySetting_Apply.Click += new System.EventHandler(this.OnDisplaySettingUpdated);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.ChkBox_Visible);
			this.groupBox4.Controls.Add(this.ChkBox_SignedValue);
			this.groupBox4.Controls.Add(this.label14);
			this.groupBox4.Controls.Add(this.Btn_ChSet_Color);
			this.groupBox4.Controls.Add(this.ChkBox_BitEndian_Reverse);
			this.groupBox4.Controls.Add(this.label12);
			this.groupBox4.Controls.Add(this.Num_ValueBitSize);
			this.groupBox4.Controls.Add(this.label13);
			this.groupBox4.Controls.Add(this.ChkBox_ByteEndian_Reverse);
			this.groupBox4.Location = new System.Drawing.Point(81, 290);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(204, 156);
			this.groupBox4.TabIndex = 24;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Common Setting";
			// 
			// ChkBox_Visible
			// 
			this.ChkBox_Visible.AutoSize = true;
			this.ChkBox_Visible.Location = new System.Drawing.Point(6, 18);
			this.ChkBox_Visible.Name = "ChkBox_Visible";
			this.ChkBox_Visible.Size = new System.Drawing.Size(59, 16);
			this.ChkBox_Visible.TabIndex = 29;
			this.ChkBox_Visible.Text = "Visible";
			this.ChkBox_Visible.UseVisualStyleBackColor = true;
			this.ChkBox_Visible.CheckedChanged += new System.EventHandler(this.OnChannelSettingUpdated);
			// 
			// ChkBox_SignedValue
			// 
			this.ChkBox_SignedValue.AutoSize = true;
			this.ChkBox_SignedValue.Location = new System.Drawing.Point(12, 134);
			this.ChkBox_SignedValue.Name = "ChkBox_SignedValue";
			this.ChkBox_SignedValue.Size = new System.Drawing.Size(91, 16);
			this.ChkBox_SignedValue.TabIndex = 28;
			this.ChkBox_SignedValue.Text = "Signed Value";
			this.ChkBox_SignedValue.UseVisualStyleBackColor = true;
			this.ChkBox_SignedValue.CheckedChanged += new System.EventHandler(this.OnChannelSettingUpdated);
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(168, 65);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(27, 19);
			this.label14.TabIndex = 25;
			this.label14.Text = "bit";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ChkBox_BitEndian_Reverse
			// 
			this.ChkBox_BitEndian_Reverse.AutoSize = true;
			this.ChkBox_BitEndian_Reverse.Location = new System.Drawing.Point(12, 112);
			this.ChkBox_BitEndian_Reverse.Name = "ChkBox_BitEndian_Reverse";
			this.ChkBox_BitEndian_Reverse.Size = new System.Drawing.Size(123, 16);
			this.ChkBox_BitEndian_Reverse.TabIndex = 26;
			this.ChkBox_BitEndian_Reverse.Text = "Reverse Bit Endian";
			this.ChkBox_BitEndian_Reverse.UseVisualStyleBackColor = true;
			this.ChkBox_BitEndian_Reverse.CheckedChanged += new System.EventHandler(this.OnChannelSettingUpdated);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(6, 65);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(100, 19);
			this.label12.TabIndex = 19;
			this.label12.Text = "Value Size";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// Num_ValueBitSize
			// 
			this.Num_ValueBitSize.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
			this.Num_ValueBitSize.Location = new System.Drawing.Point(112, 65);
			this.Num_ValueBitSize.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
			this.Num_ValueBitSize.Name = "Num_ValueBitSize";
			this.Num_ValueBitSize.Size = new System.Drawing.Size(50, 19);
			this.Num_ValueBitSize.TabIndex = 20;
			this.Num_ValueBitSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.Num_ValueBitSize.ThousandsSeparator = true;
			this.Num_ValueBitSize.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.Num_ValueBitSize.ValueChanged += new System.EventHandler(this.OnChannelSettingUpdated);
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(6, 40);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(100, 19);
			this.label13.TabIndex = 18;
			this.label13.Text = "Color";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ChkBox_ByteEndian_Reverse
			// 
			this.ChkBox_ByteEndian_Reverse.AutoSize = true;
			this.ChkBox_ByteEndian_Reverse.Location = new System.Drawing.Point(12, 90);
			this.ChkBox_ByteEndian_Reverse.Name = "ChkBox_ByteEndian_Reverse";
			this.ChkBox_ByteEndian_Reverse.Size = new System.Drawing.Size(132, 16);
			this.ChkBox_ByteEndian_Reverse.TabIndex = 25;
			this.ChkBox_ByteEndian_Reverse.Text = "Reverse Byte Endian";
			this.ChkBox_ByteEndian_Reverse.UseVisualStyleBackColor = true;
			this.ChkBox_ByteEndian_Reverse.CheckedChanged += new System.EventHandler(this.OnChannelSettingUpdated);
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.CBox_SamplingTrigger);
			this.groupBox5.Controls.Add(this.label1);
			this.groupBox5.Controls.Add(this.label17);
			this.groupBox5.Controls.Add(this.CBox_SamplingInterval_Unit);
			this.groupBox5.Controls.Add(this.Num_SamplingInterval_Value);
			this.groupBox5.Location = new System.Drawing.Point(234, 79);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(298, 72);
			this.groupBox5.TabIndex = 28;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Sampling Setting";
			// 
			// CBox_SamplingTrigger
			// 
			this.CBox_SamplingTrigger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBox_SamplingTrigger.FormattingEnabled = true;
			this.CBox_SamplingTrigger.Location = new System.Drawing.Point(122, 18);
			this.CBox_SamplingTrigger.Name = "CBox_SamplingTrigger";
			this.CBox_SamplingTrigger.Size = new System.Drawing.Size(140, 20);
			this.CBox_SamplingTrigger.TabIndex = 29;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 19);
			this.label1.TabIndex = 28;
			this.label1.Text = "Sampling Trigger";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// GraphControlPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox8);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.Btn_DisplaySetting_Apply);
			this.Controls.Add(this.groupBox6);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox9);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.Btn_SamplingSetting_Apply);
			this.Controls.Add(this.groupBox19);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Name = "GraphControlPanel";
			this.Size = new System.Drawing.Size(550, 673);
			this.groupBox1.ResumeLayout(false);
			this.groupBox9.ResumeLayout(false);
			this.groupBox9.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox13.ResumeLayout(false);
			this.groupBox13.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.TBar_ChSet_Oscillo_VertOffset)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_ChSet_Oscillo_Range_Custom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_SamplingInterval_Value)).EndInit();
			this.groupBox8.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Num_Oscillo_DisplayPoint)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_Oscillo_RecordPoint)).EndInit();
			this.groupBox19.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Num_InputDataBlockSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Num_InputDataChannelNum)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Num_ValueBitSize)).EndInit();
			this.groupBox5.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox CBox_GraphTarget;
        private System.Windows.Forms.Button Btn_ChSet_Color;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown Num_SamplingInterval_Value;
        private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.GroupBox groupBox19;
		private System.Windows.Forms.ComboBox CBox_SamplingSettingTemplate;
		private System.Windows.Forms.ComboBox CBox_SamplingInterval_Unit;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown Num_InputDataChannelNum;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox CBox_ChSet_Oscillo_Range;
		private System.Windows.Forms.NumericUpDown Num_ChSet_Oscillo_Range_Custom;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown Num_Oscillo_DisplayPoint;
		private System.Windows.Forms.NumericUpDown Num_Oscillo_RecordPoint;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ComboBox CBox_DisplayMode;
		private System.Windows.Forms.GroupBox groupBox9;
		private System.Windows.Forms.RadioButton RBtn_ChSet_CH8;
		private System.Windows.Forms.RadioButton RBtn_ChSet_CH7;
		private System.Windows.Forms.RadioButton RBtn_ChSet_CH6;
		private System.Windows.Forms.RadioButton RBtn_ChSet_CH5;
		private System.Windows.Forms.RadioButton RBtn_ChSet_CH4;
		private System.Windows.Forms.RadioButton RBtn_ChSet_CH3;
		private System.Windows.Forms.RadioButton RBtn_ChSet_CH2;
		private System.Windows.Forms.RadioButton RBtn_ChSet_CH1;
		private System.Windows.Forms.GroupBox groupBox13;
		private System.Windows.Forms.TrackBar TBar_ChSet_Oscillo_VertOffset;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Button Btn_SamplingSettingTemplate_Load;
		private System.Windows.Forms.Button Btn_SamplingSetting_Apply;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Button Btn_DisplaySetting_Apply;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.NumericUpDown Num_ValueBitSize;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.NumericUpDown Num_InputDataBlockSize;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.CheckBox ChkBox_SignedValue;
		private System.Windows.Forms.CheckBox ChkBox_BitEndian_Reverse;
		private System.Windows.Forms.CheckBox ChkBox_ByteEndian_Reverse;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.ComboBox CBox_SamplingTrigger;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox ChkBox_Visible;
	}
}
