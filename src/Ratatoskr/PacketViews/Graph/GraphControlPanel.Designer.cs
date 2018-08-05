namespace Ratatoskr.PacketViews.Graph
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
            this.CBox_DataCollectMode = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CBox_DataFormat = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CBox_DataEndian = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Num_DataChannel = new System.Windows.Forms.NumericUpDown();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.Btn_ChSet_Color = new System.Windows.Forms.Button();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.TBar_ChSet_ValueOffset = new System.Windows.Forms.TrackBar();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.Num_ChSet_ValueMag = new System.Windows.Forms.NumericUpDown();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.CBox_ChSet_Channel = new System.Windows.Forms.ComboBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.Num_SamplingPoint = new System.Windows.Forms.NumericUpDown();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.Num_DisplayPoint = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Num_SamplingInterval = new System.Windows.Forms.NumericUpDown();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.Num_AxisY_ValueMax = new System.Windows.Forms.NumericUpDown();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.Num_AxisY_ValueMin = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataChannel)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TBar_ChSet_ValueOffset)).BeginInit();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_ChSet_ValueMag)).BeginInit();
            this.groupBox11.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_SamplingPoint)).BeginInit();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DisplayPoint)).BeginInit();
            this.groupBox16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_SamplingInterval)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_AxisY_ValueMax)).BeginInit();
            this.groupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_AxisY_ValueMin)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.CBox_DataCollectMode);
            this.groupBox1.Location = new System.Drawing.Point(374, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(103, 42);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Collect Mode";
            // 
            // CBox_DataCollectMode
            // 
            this.CBox_DataCollectMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_DataCollectMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_DataCollectMode.FormattingEnabled = true;
            this.CBox_DataCollectMode.Location = new System.Drawing.Point(3, 15);
            this.CBox_DataCollectMode.Name = "CBox_DataCollectMode";
            this.CBox_DataCollectMode.Size = new System.Drawing.Size(97, 20);
            this.CBox_DataCollectMode.TabIndex = 1;
            this.CBox_DataCollectMode.SelectedIndexChanged += new System.EventHandler(this.OnSamplingSettingUpdated);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CBox_DataFormat);
            this.groupBox2.Location = new System.Drawing.Point(6, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(130, 42);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Format";
            // 
            // CBox_DataFormat
            // 
            this.CBox_DataFormat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_DataFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_DataFormat.FormattingEnabled = true;
            this.CBox_DataFormat.Location = new System.Drawing.Point(3, 15);
            this.CBox_DataFormat.Name = "CBox_DataFormat";
            this.CBox_DataFormat.Size = new System.Drawing.Size(124, 20);
            this.CBox_DataFormat.TabIndex = 0;
            this.CBox_DataFormat.SelectedIndexChanged += new System.EventHandler(this.OnSamplingSettingUpdated);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CBox_DataEndian);
            this.groupBox3.Location = new System.Drawing.Point(142, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(120, 42);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Data Endian";
            // 
            // CBox_DataEndian
            // 
            this.CBox_DataEndian.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_DataEndian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_DataEndian.FormattingEnabled = true;
            this.CBox_DataEndian.Location = new System.Drawing.Point(3, 15);
            this.CBox_DataEndian.Name = "CBox_DataEndian";
            this.CBox_DataEndian.Size = new System.Drawing.Size(114, 20);
            this.CBox_DataEndian.TabIndex = 0;
            this.CBox_DataEndian.SelectedIndexChanged += new System.EventHandler(this.OnSamplingSettingUpdated);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Num_DataChannel);
            this.groupBox4.Location = new System.Drawing.Point(268, 18);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(100, 42);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Data Channel";
            // 
            // Num_DataChannel
            // 
            this.Num_DataChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Num_DataChannel.Location = new System.Drawing.Point(3, 15);
            this.Num_DataChannel.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Num_DataChannel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_DataChannel.Name = "Num_DataChannel";
            this.Num_DataChannel.Size = new System.Drawing.Size(94, 19);
            this.Num_DataChannel.TabIndex = 0;
            this.Num_DataChannel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_DataChannel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_DataChannel.ValueChanged += new System.EventHandler(this.OnSamplingSettingUpdated);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.groupBox14);
            this.groupBox7.Controls.Add(this.groupBox13);
            this.groupBox7.Controls.Add(this.groupBox12);
            this.groupBox7.Location = new System.Drawing.Point(89, 269);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(232, 140);
            this.groupBox7.TabIndex = 8;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Channel Setting";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.Btn_ChSet_Color);
            this.groupBox14.Location = new System.Drawing.Point(7, 18);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(60, 43);
            this.groupBox14.TabIndex = 16;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Color";
            // 
            // Btn_ChSet_Color
            // 
            this.Btn_ChSet_Color.BackColor = System.Drawing.Color.Yellow;
            this.Btn_ChSet_Color.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_ChSet_Color.Location = new System.Drawing.Point(6, 18);
            this.Btn_ChSet_Color.Name = "Btn_ChSet_Color";
            this.Btn_ChSet_Color.Size = new System.Drawing.Size(48, 19);
            this.Btn_ChSet_Color.TabIndex = 0;
            this.Btn_ChSet_Color.UseVisualStyleBackColor = false;
            this.Btn_ChSet_Color.Click += new System.EventHandler(this.Btn_ChSet_Color_Click);
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.TBar_ChSet_ValueOffset);
            this.groupBox13.Location = new System.Drawing.Point(179, 18);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(45, 116);
            this.groupBox13.TabIndex = 17;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Pos";
            // 
            // TBar_ChSet_ValueOffset
            // 
            this.TBar_ChSet_ValueOffset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBar_ChSet_ValueOffset.Location = new System.Drawing.Point(3, 15);
            this.TBar_ChSet_ValueOffset.Maximum = 100000;
            this.TBar_ChSet_ValueOffset.Minimum = -100000;
            this.TBar_ChSet_ValueOffset.Name = "TBar_ChSet_ValueOffset";
            this.TBar_ChSet_ValueOffset.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TBar_ChSet_ValueOffset.Size = new System.Drawing.Size(39, 98);
            this.TBar_ChSet_ValueOffset.TabIndex = 13;
            this.TBar_ChSet_ValueOffset.TickFrequency = 20000;
            this.TBar_ChSet_ValueOffset.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.TBar_ChSet_ValueOffset.ValueChanged += new System.EventHandler(this.OnDisplaySettingUpdated);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.Num_ChSet_ValueMag);
            this.groupBox12.Location = new System.Drawing.Point(73, 18);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(100, 43);
            this.groupBox12.TabIndex = 16;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Magnification";
            // 
            // Num_ChSet_ValueMag
            // 
            this.Num_ChSet_ValueMag.DecimalPlaces = 4;
            this.Num_ChSet_ValueMag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Num_ChSet_ValueMag.Font = new System.Drawing.Font("ＭＳ ゴシック", 8F);
            this.Num_ChSet_ValueMag.Location = new System.Drawing.Point(3, 15);
            this.Num_ChSet_ValueMag.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.Num_ChSet_ValueMag.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            262144});
            this.Num_ChSet_ValueMag.Name = "Num_ChSet_ValueMag";
            this.Num_ChSet_ValueMag.Size = new System.Drawing.Size(94, 18);
            this.Num_ChSet_ValueMag.TabIndex = 12;
            this.Num_ChSet_ValueMag.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Num_ChSet_ValueMag.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_ChSet_ValueMag.ValueChanged += new System.EventHandler(this.OnDisplaySettingUpdated);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.CBox_ChSet_Channel);
            this.groupBox11.Location = new System.Drawing.Point(3, 269);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(80, 43);
            this.groupBox11.TabIndex = 15;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Channel";
            // 
            // CBox_ChSet_Channel
            // 
            this.CBox_ChSet_Channel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_ChSet_Channel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_ChSet_Channel.FormattingEnabled = true;
            this.CBox_ChSet_Channel.Location = new System.Drawing.Point(3, 15);
            this.CBox_ChSet_Channel.Name = "CBox_ChSet_Channel";
            this.CBox_ChSet_Channel.Size = new System.Drawing.Size(74, 20);
            this.CBox_ChSet_Channel.TabIndex = 0;
            this.CBox_ChSet_Channel.SelectedIndexChanged += new System.EventHandler(this.CBox_ChSet_Channel_SelectedIndexChanged);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.Num_SamplingPoint);
            this.groupBox9.Location = new System.Drawing.Point(6, 18);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(100, 42);
            this.groupBox9.TabIndex = 9;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Sampling Point";
            // 
            // Num_SamplingPoint
            // 
            this.Num_SamplingPoint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Num_SamplingPoint.Location = new System.Drawing.Point(3, 15);
            this.Num_SamplingPoint.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.Num_SamplingPoint.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Num_SamplingPoint.Name = "Num_SamplingPoint";
            this.Num_SamplingPoint.Size = new System.Drawing.Size(94, 19);
            this.Num_SamplingPoint.TabIndex = 0;
            this.Num_SamplingPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Num_SamplingPoint.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.Num_SamplingPoint.ValueChanged += new System.EventHandler(this.OnSamplingSettingUpdated);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.Num_DisplayPoint);
            this.groupBox10.Location = new System.Drawing.Point(6, 18);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(100, 42);
            this.groupBox10.TabIndex = 10;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Display Point";
            // 
            // Num_DisplayPoint
            // 
            this.Num_DisplayPoint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Num_DisplayPoint.Location = new System.Drawing.Point(3, 15);
            this.Num_DisplayPoint.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.Num_DisplayPoint.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.Num_DisplayPoint.Name = "Num_DisplayPoint";
            this.Num_DisplayPoint.Size = new System.Drawing.Size(94, 19);
            this.Num_DisplayPoint.TabIndex = 0;
            this.Num_DisplayPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Num_DisplayPoint.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Num_DisplayPoint.ValueChanged += new System.EventHandler(this.OnDisplaySettingUpdated);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(497, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Sampling Setting";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(3, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(497, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Display Setting";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.label4);
            this.groupBox16.Controls.Add(this.Num_SamplingInterval);
            this.groupBox16.Location = new System.Drawing.Point(112, 18);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(125, 42);
            this.groupBox16.TabIndex = 4;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Sampling Interval";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(92, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "sec";
            // 
            // Num_SamplingInterval
            // 
            this.Num_SamplingInterval.DecimalPlaces = 1;
            this.Num_SamplingInterval.Location = new System.Drawing.Point(6, 15);
            this.Num_SamplingInterval.Maximum = new decimal(new int[] {
            86400,
            0,
            0,
            0});
            this.Num_SamplingInterval.Name = "Num_SamplingInterval";
            this.Num_SamplingInterval.Size = new System.Drawing.Size(80, 19);
            this.Num_SamplingInterval.TabIndex = 0;
            this.Num_SamplingInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Num_SamplingInterval.Value = new decimal(new int[] {
            86400,
            0,
            0,
            0});
            this.Num_SamplingInterval.ValueChanged += new System.EventHandler(this.OnSamplingSettingUpdated);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox2);
            this.groupBox5.Controls.Add(this.groupBox4);
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Controls.Add(this.groupBox1);
            this.groupBox5.Location = new System.Drawing.Point(3, 23);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(489, 70);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Input Data";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.groupBox9);
            this.groupBox6.Controls.Add(this.groupBox16);
            this.groupBox6.Location = new System.Drawing.Point(3, 99);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(246, 69);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Sampling Setting";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.groupBox10);
            this.groupBox8.Location = new System.Drawing.Point(3, 196);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(116, 67);
            this.groupBox8.TabIndex = 14;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Axis X Setting";
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.groupBox18);
            this.groupBox15.Controls.Add(this.groupBox17);
            this.groupBox15.Location = new System.Drawing.Point(125, 196);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(222, 67);
            this.groupBox15.TabIndex = 15;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Axis Y Setting";
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.Num_AxisY_ValueMax);
            this.groupBox18.Location = new System.Drawing.Point(112, 18);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(100, 42);
            this.groupBox18.TabIndex = 11;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Max Value";
            // 
            // Num_AxisY_ValueMax
            // 
            this.Num_AxisY_ValueMax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Num_AxisY_ValueMax.Location = new System.Drawing.Point(3, 15);
            this.Num_AxisY_ValueMax.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.Num_AxisY_ValueMax.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.Num_AxisY_ValueMax.Name = "Num_AxisY_ValueMax";
            this.Num_AxisY_ValueMax.Size = new System.Drawing.Size(94, 19);
            this.Num_AxisY_ValueMax.TabIndex = 0;
            this.Num_AxisY_ValueMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Num_AxisY_ValueMax.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Num_AxisY_ValueMax.ValueChanged += new System.EventHandler(this.OnDisplaySettingUpdated);
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.Num_AxisY_ValueMin);
            this.groupBox17.Location = new System.Drawing.Point(6, 18);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(100, 42);
            this.groupBox17.TabIndex = 10;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Min Value";
            // 
            // Num_AxisY_ValueMin
            // 
            this.Num_AxisY_ValueMin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Num_AxisY_ValueMin.Location = new System.Drawing.Point(3, 15);
            this.Num_AxisY_ValueMin.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.Num_AxisY_ValueMin.Minimum = new decimal(new int[] {
            100000,
            0,
            0,
            -2147483648});
            this.Num_AxisY_ValueMin.Name = "Num_AxisY_ValueMin";
            this.Num_AxisY_ValueMin.Size = new System.Drawing.Size(94, 19);
            this.Num_AxisY_ValueMin.TabIndex = 0;
            this.Num_AxisY_ValueMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Num_AxisY_ValueMin.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Num_AxisY_ValueMin.ValueChanged += new System.EventHandler(this.OnDisplaySettingUpdated);
            // 
            // GraphControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox15);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox5);
            this.Name = "GraphControlPanel";
            this.Size = new System.Drawing.Size(503, 415);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataChannel)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TBar_ChSet_ValueOffset)).EndInit();
            this.groupBox12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_ChSet_ValueMag)).EndInit();
            this.groupBox11.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_SamplingPoint)).EndInit();
            this.groupBox10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_DisplayPoint)).EndInit();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_SamplingInterval)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.groupBox18.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_AxisY_ValueMax)).EndInit();
            this.groupBox17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_AxisY_ValueMin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox CBox_DataCollectMode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox CBox_DataFormat;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox CBox_DataEndian;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown Num_DataChannel;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.NumericUpDown Num_SamplingPoint;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.NumericUpDown Num_DisplayPoint;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.TrackBar TBar_ChSet_ValueOffset;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.ComboBox CBox_ChSet_Channel;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Button Btn_ChSet_Color;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown Num_SamplingInterval;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown Num_ChSet_ValueMag;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.NumericUpDown Num_AxisY_ValueMax;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.NumericUpDown Num_AxisY_ValueMin;
    }
}
