using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Ratatoskr.Generic;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;

namespace Ratatoskr.PacketViews.Graph
{
    internal sealed class ViewInstanceImpl : ViewInstance
    {
        private int GRAPH_LINE_Y_MAJOR_NUM = 10;
        private int GRAPH_LINE_Y_MINOR_NUM = 50;

        private ViewPropertyImpl prop_;

        private GraphChartObject chart_ = null;

        private System.Windows.Forms.Timer update_timer_ = new Timer();

        private System.Windows.Forms.Panel Panel_ToolBar;
        private System.Windows.Forms.Panel Panel_Bottom;
        private System.Windows.Forms.Panel Panel_Graph;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox GBox_InputDataType;
        private System.Windows.Forms.ComboBox CBox_InputDataType;
        private System.Windows.Forms.GroupBox GBox_InputByteEndian;
        private System.Windows.Forms.ComboBox CBox_InputByteEndian;
        private GroupBox groupBox4;
        private GroupBox GBox_DrawPoint;
        private NumericUpDown Num_DrawPointNum;
        private GroupBox groupBox3;
        private GroupBox GBox_SamplingInterval;
        private Label label2;
        private NumericUpDown Num_SamplingInterval;
        private GroupBox groupBox1;
        private NumericUpDown Num_DataValueMax;
        private NumericUpDown Num_DataValueMin;
        private GroupBox groupBox5;
        private NumericUpDown Num_DataValueOffset;
        private GroupBox groupBox6;
        private Label label3;
        private NumericUpDown Num_InputCycle;
        private GroupBox groupBox7;
        private ComboBox CBox_GraphType;
        private GroupBox groupBox8;
        private Label label4;
        private NumericUpDown Num_DrawInterval;
        private CheckBox ChkBox_DataValueMax;
        private CheckBox ChkBox_DataValueMin;
        private Label label1;
        private GroupBox GBox_ViewMode;
        private ComboBox CBox_ViewMode;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_Data;


        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.Panel_ToolBar = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Num_DrawInterval = new System.Windows.Forms.NumericUpDown();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.CBox_GraphType = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ChkBox_DataValueMax = new System.Windows.Forms.CheckBox();
            this.ChkBox_DataValueMin = new System.Windows.Forms.CheckBox();
            this.Num_DataValueMax = new System.Windows.Forms.NumericUpDown();
            this.Num_DataValueMin = new System.Windows.Forms.NumericUpDown();
            this.GBox_DrawPoint = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Num_DrawPointNum = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.GBox_SamplingInterval = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Num_SamplingInterval = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Num_InputCycle = new System.Windows.Forms.NumericUpDown();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.Num_DataValueOffset = new System.Windows.Forms.NumericUpDown();
            this.GBox_InputByteEndian = new System.Windows.Forms.GroupBox();
            this.CBox_InputByteEndian = new System.Windows.Forms.ComboBox();
            this.GBox_InputDataType = new System.Windows.Forms.GroupBox();
            this.CBox_InputDataType = new System.Windows.Forms.ComboBox();
            this.Panel_Bottom = new System.Windows.Forms.Panel();
            this.Panel_Graph = new System.Windows.Forms.Panel();
            this.Chart_Data = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.GBox_ViewMode = new System.Windows.Forms.GroupBox();
            this.CBox_ViewMode = new System.Windows.Forms.ComboBox();
            this.Panel_ToolBar.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DrawInterval)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataValueMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataValueMin)).BeginInit();
            this.GBox_DrawPoint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DrawPointNum)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.GBox_SamplingInterval.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_SamplingInterval)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_InputCycle)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataValueOffset)).BeginInit();
            this.GBox_InputByteEndian.SuspendLayout();
            this.GBox_InputDataType.SuspendLayout();
            this.Panel_Graph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_Data)).BeginInit();
            this.GBox_ViewMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_ToolBar
            // 
            this.Panel_ToolBar.Controls.Add(this.groupBox4);
            this.Panel_ToolBar.Controls.Add(this.groupBox3);
            this.Panel_ToolBar.Controls.Add(this.groupBox2);
            this.Panel_ToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_ToolBar.Location = new System.Drawing.Point(0, 0);
            this.Panel_ToolBar.Name = "Panel_ToolBar";
            this.Panel_ToolBar.Size = new System.Drawing.Size(997, 158);
            this.Panel_ToolBar.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.GBox_ViewMode);
            this.groupBox4.Controls.Add(this.groupBox8);
            this.groupBox4.Controls.Add(this.groupBox7);
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Controls.Add(this.GBox_DrawPoint);
            this.groupBox4.Location = new System.Drawing.Point(3, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(420, 148);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "グラフ設定";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label4);
            this.groupBox8.Controls.Add(this.Num_DrawInterval);
            this.groupBox8.Location = new System.Drawing.Point(238, 18);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(120, 45);
            this.groupBox8.TabIndex = 8;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "表示間隔";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(94, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "ms";
            // 
            // Num_DrawInterval
            // 
            this.Num_DrawInterval.Location = new System.Drawing.Point(6, 18);
            this.Num_DrawInterval.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.Num_DrawInterval.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.Num_DrawInterval.Name = "Num_DrawInterval";
            this.Num_DrawInterval.Size = new System.Drawing.Size(80, 19);
            this.Num_DrawInterval.TabIndex = 0;
            this.Num_DrawInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_DrawInterval.ThousandsSeparator = true;
            this.Num_DrawInterval.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.Num_DrawInterval.ValueChanged += new System.EventHandler(this.Setting_ValueChanged);
            this.Num_DrawInterval.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Setting_KeyDown);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.CBox_GraphType);
            this.groupBox7.Location = new System.Drawing.Point(112, 18);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(120, 45);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "グラフ種別";
            // 
            // CBox_GraphType
            // 
            this.CBox_GraphType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_GraphType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_GraphType.FormattingEnabled = true;
            this.CBox_GraphType.Location = new System.Drawing.Point(3, 15);
            this.CBox_GraphType.Name = "CBox_GraphType";
            this.CBox_GraphType.Size = new System.Drawing.Size(114, 20);
            this.CBox_GraphType.TabIndex = 1;
            this.CBox_GraphType.SelectedIndexChanged += new System.EventHandler(this.Setting_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ChkBox_DataValueMax);
            this.groupBox1.Controls.Add(this.ChkBox_DataValueMin);
            this.groupBox1.Controls.Add(this.Num_DataValueMax);
            this.groupBox1.Controls.Add(this.Num_DataValueMin);
            this.groupBox1.Location = new System.Drawing.Point(212, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 71);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Y軸設定";
            // 
            // ChkBox_DataValueMax
            // 
            this.ChkBox_DataValueMax.Location = new System.Drawing.Point(7, 46);
            this.ChkBox_DataValueMax.Name = "ChkBox_DataValueMax";
            this.ChkBox_DataValueMax.Size = new System.Drawing.Size(80, 16);
            this.ChkBox_DataValueMax.TabIndex = 4;
            this.ChkBox_DataValueMax.Text = "最大値";
            this.ChkBox_DataValueMax.UseVisualStyleBackColor = true;
            this.ChkBox_DataValueMax.CheckedChanged += new System.EventHandler(this.Setting_SelectedIndexChanged);
            // 
            // ChkBox_DataValueMin
            // 
            this.ChkBox_DataValueMin.Location = new System.Drawing.Point(7, 16);
            this.ChkBox_DataValueMin.Name = "ChkBox_DataValueMin";
            this.ChkBox_DataValueMin.Size = new System.Drawing.Size(80, 24);
            this.ChkBox_DataValueMin.TabIndex = 3;
            this.ChkBox_DataValueMin.Text = "最小値";
            this.ChkBox_DataValueMin.UseVisualStyleBackColor = true;
            this.ChkBox_DataValueMin.CheckedChanged += new System.EventHandler(this.Setting_SelectedIndexChanged);
            // 
            // Num_DataValueMax
            // 
            this.Num_DataValueMax.Location = new System.Drawing.Point(93, 44);
            this.Num_DataValueMax.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.Num_DataValueMax.Minimum = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147483648});
            this.Num_DataValueMax.Name = "Num_DataValueMax";
            this.Num_DataValueMax.Size = new System.Drawing.Size(100, 19);
            this.Num_DataValueMax.TabIndex = 2;
            this.Num_DataValueMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_DataValueMax.ThousandsSeparator = true;
            this.Num_DataValueMax.Value = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.Num_DataValueMax.ValueChanged += new System.EventHandler(this.Num_DataValueMax_ValueChanged);
            this.Num_DataValueMax.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Setting_KeyDown);
            // 
            // Num_DataValueMin
            // 
            this.Num_DataValueMin.Location = new System.Drawing.Point(93, 19);
            this.Num_DataValueMin.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.Num_DataValueMin.Minimum = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147483648});
            this.Num_DataValueMin.Name = "Num_DataValueMin";
            this.Num_DataValueMin.Size = new System.Drawing.Size(100, 19);
            this.Num_DataValueMin.TabIndex = 0;
            this.Num_DataValueMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_DataValueMin.ThousandsSeparator = true;
            this.Num_DataValueMin.Value = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147483648});
            this.Num_DataValueMin.ValueChanged += new System.EventHandler(this.Num_DataValueMin_ValueChanged);
            this.Num_DataValueMin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Setting_KeyDown);
            // 
            // GBox_DrawPoint
            // 
            this.GBox_DrawPoint.Controls.Add(this.label1);
            this.GBox_DrawPoint.Controls.Add(this.Num_DrawPointNum);
            this.GBox_DrawPoint.Location = new System.Drawing.Point(6, 69);
            this.GBox_DrawPoint.Name = "GBox_DrawPoint";
            this.GBox_DrawPoint.Size = new System.Drawing.Size(200, 45);
            this.GBox_DrawPoint.TabIndex = 5;
            this.GBox_DrawPoint.TabStop = false;
            this.GBox_DrawPoint.Text = "X軸設定";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "ポイント数";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Num_DrawPointNum
            // 
            this.Num_DrawPointNum.Location = new System.Drawing.Point(93, 18);
            this.Num_DrawPointNum.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.Num_DrawPointNum.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.Num_DrawPointNum.Name = "Num_DrawPointNum";
            this.Num_DrawPointNum.Size = new System.Drawing.Size(100, 19);
            this.Num_DrawPointNum.TabIndex = 0;
            this.Num_DrawPointNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_DrawPointNum.ThousandsSeparator = true;
            this.Num_DrawPointNum.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.Num_DrawPointNum.ValueChanged += new System.EventHandler(this.Setting_ValueChanged);
            this.Num_DrawPointNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Setting_KeyDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.GBox_SamplingInterval);
            this.groupBox3.Location = new System.Drawing.Point(724, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(139, 71);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "モード別設定: データ量";
            // 
            // GBox_SamplingInterval
            // 
            this.GBox_SamplingInterval.Controls.Add(this.label2);
            this.GBox_SamplingInterval.Controls.Add(this.Num_SamplingInterval);
            this.GBox_SamplingInterval.Location = new System.Drawing.Point(6, 18);
            this.GBox_SamplingInterval.Name = "GBox_SamplingInterval";
            this.GBox_SamplingInterval.Size = new System.Drawing.Size(128, 45);
            this.GBox_SamplingInterval.TabIndex = 3;
            this.GBox_SamplingInterval.TabStop = false;
            this.GBox_SamplingInterval.Text = "サンプリング間隔";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(99, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "ms";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Num_SamplingInterval
            // 
            this.Num_SamplingInterval.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Num_SamplingInterval.Location = new System.Drawing.Point(3, 15);
            this.Num_SamplingInterval.Maximum = new decimal(new int[] {
            3600000,
            0,
            0,
            0});
            this.Num_SamplingInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_SamplingInterval.Name = "Num_SamplingInterval";
            this.Num_SamplingInterval.Size = new System.Drawing.Size(90, 19);
            this.Num_SamplingInterval.TabIndex = 0;
            this.Num_SamplingInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_SamplingInterval.ThousandsSeparator = true;
            this.Num_SamplingInterval.Value = new decimal(new int[] {
            3600000,
            0,
            0,
            0});
            this.Num_SamplingInterval.ValueChanged += new System.EventHandler(this.Setting_ValueChanged);
            this.Num_SamplingInterval.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Setting_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox6);
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.GBox_InputByteEndian);
            this.groupBox2.Controls.Add(this.GBox_InputDataType);
            this.groupBox2.Location = new System.Drawing.Point(429, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 125);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "モード別設定: 連続データ";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.Num_InputCycle);
            this.groupBox6.Location = new System.Drawing.Point(152, 68);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(108, 45);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "有効データ間隔";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "1 / ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Num_InputCycle
            // 
            this.Num_InputCycle.Location = new System.Drawing.Point(31, 16);
            this.Num_InputCycle.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.Num_InputCycle.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Num_InputCycle.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_InputCycle.Name = "Num_InputCycle";
            this.Num_InputCycle.Size = new System.Drawing.Size(65, 19);
            this.Num_InputCycle.TabIndex = 0;
            this.Num_InputCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_InputCycle.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Num_InputCycle.ValueChanged += new System.EventHandler(this.Setting_ValueChanged);
            this.Num_InputCycle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Setting_KeyDown);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.Num_DataValueOffset);
            this.groupBox5.Location = new System.Drawing.Point(152, 18);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(108, 45);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "データ値補正";
            // 
            // Num_DataValueOffset
            // 
            this.Num_DataValueOffset.DecimalPlaces = 1;
            this.Num_DataValueOffset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Num_DataValueOffset.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Num_DataValueOffset.Location = new System.Drawing.Point(3, 15);
            this.Num_DataValueOffset.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.Num_DataValueOffset.Minimum = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147483648});
            this.Num_DataValueOffset.Name = "Num_DataValueOffset";
            this.Num_DataValueOffset.Size = new System.Drawing.Size(102, 19);
            this.Num_DataValueOffset.TabIndex = 0;
            this.Num_DataValueOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_DataValueOffset.ThousandsSeparator = true;
            this.Num_DataValueOffset.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.Num_DataValueOffset.ValueChanged += new System.EventHandler(this.Setting_ValueChanged);
            this.Num_DataValueOffset.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Setting_KeyDown);
            // 
            // GBox_InputByteEndian
            // 
            this.GBox_InputByteEndian.Controls.Add(this.CBox_InputByteEndian);
            this.GBox_InputByteEndian.Location = new System.Drawing.Point(6, 69);
            this.GBox_InputByteEndian.Name = "GBox_InputByteEndian";
            this.GBox_InputByteEndian.Size = new System.Drawing.Size(140, 45);
            this.GBox_InputByteEndian.TabIndex = 3;
            this.GBox_InputByteEndian.TabStop = false;
            this.GBox_InputByteEndian.Text = "バイトエンディアン";
            // 
            // CBox_InputByteEndian
            // 
            this.CBox_InputByteEndian.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_InputByteEndian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_InputByteEndian.FormattingEnabled = true;
            this.CBox_InputByteEndian.Location = new System.Drawing.Point(3, 15);
            this.CBox_InputByteEndian.Name = "CBox_InputByteEndian";
            this.CBox_InputByteEndian.Size = new System.Drawing.Size(134, 20);
            this.CBox_InputByteEndian.TabIndex = 1;
            this.CBox_InputByteEndian.SelectedIndexChanged += new System.EventHandler(this.Setting_SelectedIndexChanged);
            // 
            // GBox_InputDataType
            // 
            this.GBox_InputDataType.Controls.Add(this.CBox_InputDataType);
            this.GBox_InputDataType.Location = new System.Drawing.Point(6, 18);
            this.GBox_InputDataType.Name = "GBox_InputDataType";
            this.GBox_InputDataType.Size = new System.Drawing.Size(140, 45);
            this.GBox_InputDataType.TabIndex = 2;
            this.GBox_InputDataType.TabStop = false;
            this.GBox_InputDataType.Text = "データタイプ";
            // 
            // CBox_InputDataType
            // 
            this.CBox_InputDataType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_InputDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_InputDataType.FormattingEnabled = true;
            this.CBox_InputDataType.Location = new System.Drawing.Point(3, 15);
            this.CBox_InputDataType.Name = "CBox_InputDataType";
            this.CBox_InputDataType.Size = new System.Drawing.Size(134, 20);
            this.CBox_InputDataType.TabIndex = 1;
            this.CBox_InputDataType.SelectedIndexChanged += new System.EventHandler(this.Setting_SelectedIndexChanged);
            // 
            // Panel_Bottom
            // 
            this.Panel_Bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel_Bottom.Location = new System.Drawing.Point(0, 551);
            this.Panel_Bottom.Name = "Panel_Bottom";
            this.Panel_Bottom.Size = new System.Drawing.Size(997, 25);
            this.Panel_Bottom.TabIndex = 1;
            // 
            // Panel_Graph
            // 
            this.Panel_Graph.Controls.Add(this.Chart_Data);
            this.Panel_Graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Graph.Location = new System.Drawing.Point(0, 158);
            this.Panel_Graph.Name = "Panel_Graph";
            this.Panel_Graph.Size = new System.Drawing.Size(997, 393);
            this.Panel_Graph.TabIndex = 2;
            // 
            // Chart_Data
            // 
            this.Chart_Data.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.None;
            chartArea2.Name = "ChartArea_Data";
            this.Chart_Data.ChartAreas.Add(chartArea2);
            this.Chart_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Chart_Data.IsSoftShadows = false;
            this.Chart_Data.Location = new System.Drawing.Point(0, 0);
            this.Chart_Data.Name = "Chart_Data";
            series2.ChartArea = "ChartArea_Data";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Name = "Series1";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.Chart_Data.Series.Add(series2);
            this.Chart_Data.Size = new System.Drawing.Size(997, 393);
            this.Chart_Data.TabIndex = 0;
            this.Chart_Data.Text = "chart1";
            this.Chart_Data.TextAntiAliasingQuality = System.Windows.Forms.DataVisualization.Charting.TextAntiAliasingQuality.Normal;
            // 
            // GBox_ViewMode
            // 
            this.GBox_ViewMode.Controls.Add(this.CBox_ViewMode);
            this.GBox_ViewMode.Location = new System.Drawing.Point(6, 18);
            this.GBox_ViewMode.Name = "GBox_ViewMode";
            this.GBox_ViewMode.Size = new System.Drawing.Size(100, 45);
            this.GBox_ViewMode.TabIndex = 9;
            this.GBox_ViewMode.TabStop = false;
            this.GBox_ViewMode.Text = "モード";
            // 
            // CBox_ViewMode
            // 
            this.CBox_ViewMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_ViewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_ViewMode.FormattingEnabled = true;
            this.CBox_ViewMode.Location = new System.Drawing.Point(3, 15);
            this.CBox_ViewMode.Name = "CBox_ViewMode";
            this.CBox_ViewMode.Size = new System.Drawing.Size(94, 20);
            this.CBox_ViewMode.TabIndex = 0;
            this.CBox_ViewMode.SelectedIndexChanged += new System.EventHandler(this.Setting_SelectedIndexChanged);
            // 
            // ViewInstanceImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.Panel_Graph);
            this.Controls.Add(this.Panel_Bottom);
            this.Controls.Add(this.Panel_ToolBar);
            this.Name = "ViewInstanceImpl";
            this.Size = new System.Drawing.Size(997, 576);
            this.Panel_ToolBar.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DrawInterval)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataValueMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataValueMin)).EndInit();
            this.GBox_DrawPoint.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_DrawPointNum)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.GBox_SamplingInterval.ResumeLayout(false);
            this.GBox_SamplingInterval.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_SamplingInterval)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_InputCycle)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_DataValueOffset)).EndInit();
            this.GBox_InputByteEndian.ResumeLayout(false);
            this.GBox_InputDataType.ResumeLayout(false);
            this.Panel_Graph.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Chart_Data)).EndInit();
            this.GBox_ViewMode.ResumeLayout(false);
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

            InitializeViewMode();
            InitializeGraphType();
            InitializeInputByteEndian();
            InitializeInputDataType();

            Disposed += OnDisposed;

            CBox_ViewMode.SelectedItem = prop_.ViewMode.Value;
            Num_DrawPointNum.Value = prop_.DrawPointNum.Value;
            Num_DrawInterval.Value = prop_.DrawInterval.Value;
            Num_DataValueMin.Value = prop_.DataValueMin.Value;
            ChkBox_DataValueMin.Checked = !prop_.DataValueMinAuto.Value;
            Num_DataValueMax.Value = prop_.DataValueMax.Value;
            ChkBox_DataValueMax.Checked = !prop_.DataValueMaxAuto.Value;
            Num_DataValueOffset.Value = prop_.DataValueOffset.Value;
            CBox_InputByteEndian.SelectedItem = prop_.InputByteEndian.Value;
            CBox_InputDataType.SelectedItem = prop_.InputDataType.Value;
            Num_InputCycle.Value = prop_.InputCycle.Value;
            Num_SamplingInterval.Value = prop_.SamplingInterval.Value;

            update_timer_.Tick += OnUpdateTimer;
            UpdateDrawTimer();
        }

        private void OnDisposed(object sender, EventArgs e)
        {
            if (chart_ != null) {
                chart_.Dispose();
            }
            update_timer_.Dispose();
        }

        private void InitializeViewMode()
        {
            CBox_ViewMode.BeginUpdate();
            {
                foreach (ViewModeType type in Enum.GetValues(typeof(ViewModeType))) {
                    CBox_ViewMode.Items.Add(type);
                }
                CBox_ViewMode.SelectedIndex = 0;
            }
            CBox_ViewMode.EndUpdate();
        }

        private void InitializeGraphType()
        {
            CBox_GraphType.BeginUpdate();
            {
                CBox_GraphType.Items.Add(SeriesChartType.Line);
                CBox_GraphType.Items.Add(SeriesChartType.Area);
                CBox_GraphType.Items.Add(SeriesChartType.Point);
                CBox_GraphType.SelectedIndex = 0;
            }
            CBox_GraphType.EndUpdate();
        }

        private void InitializeInputByteEndian()
        {
            CBox_InputByteEndian.BeginUpdate();
            {
                foreach (EndianType type in Enum.GetValues(typeof(EndianType))) {
                    CBox_InputByteEndian.Items.Add(type);
                }
                CBox_InputByteEndian.SelectedIndex = 0;
            }
            CBox_InputByteEndian.EndUpdate();
        }

        private void InitializeInputDataType()
        {
            CBox_InputDataType.BeginUpdate();
            {
                foreach (DataType type in Enum.GetValues(typeof(DataType))) {
                    CBox_InputDataType.Items.Add(type);
                }
                CBox_InputDataType.SelectedIndex = 0;
            }
            CBox_InputDataType.EndUpdate();
        }

        private void UpdateView()
        {
            Num_DrawPointNum.ForeColor = (Num_DrawPointNum.Value == prop_.DrawPointNum.Value)
                                       ? (Color.Black)
                                       : (Color.Gray);

            Num_DrawInterval.ForeColor = (Num_DrawInterval.Value == prop_.DrawInterval.Value)
                                       ? (Color.Black)
                                       : (Color.Gray);

            Num_DataValueMin.Enabled = !prop_.DataValueMinAuto.Value;
            Num_DataValueMin.ForeColor = (Num_DataValueMin.Value == prop_.DataValueMin.Value)
                                       ? (Color.Black)
                                       : (Color.Gray);

            Num_DataValueMax.Enabled = !prop_.DataValueMaxAuto.Value;
            Num_DataValueMax.ForeColor = (Num_DataValueMax.Value == prop_.DataValueMax.Value)
                                       ? (Color.Black)
                                       : (Color.Gray);

            Num_DataValueOffset.ForeColor = (Num_DataValueOffset.Value == prop_.DataValueOffset.Value)
                                          ? (Color.Black)
                                          : (Color.Gray);

            Num_InputCycle.ForeColor = (Num_InputCycle.Value == prop_.InputCycle.Value)
                                     ? (Color.Black)
                                     : (Color.Gray);

            Num_SamplingInterval.ForeColor = (Num_SamplingInterval.Value == prop_.SamplingInterval.Value)
                                           ? (Color.Black)
                                           : (Color.Gray);

        }

        private void Apply()
        {
            /* 新しいチャートオブジェクトを読み込み */
            chart_ = CreateChartObject();

            /* グラフ設定を更新 */
            var area_chart = Chart_Data.ChartAreas[0];

            area_chart.AxisY.Title = "";
            area_chart.AxisY.Minimum = (double)prop_.DataValueMin.Value;
            area_chart.AxisY.Maximum = (double)prop_.DataValueMax.Value;
            area_chart.AxisY.MajorGrid.Interval = (area_chart.AxisY.Maximum - area_chart.AxisY.Minimum) / GRAPH_LINE_Y_MAJOR_NUM;
            area_chart.AxisY.MinorGrid.Interval = (area_chart.AxisY.Maximum - area_chart.AxisY.Minimum) / GRAPH_LINE_Y_MINOR_NUM;

            /* 再描画 */
            RedrawPacket();

            /* 描画タイマー更新 */
            UpdateDrawTimer();
        }

        private GraphChartObject CreateChartObject()
        {
            switch (prop_.ViewMode.Value) {
                case ViewModeType.Data:
                    return (new GraphType.Data.GraphChartObjectImpl(prop_));
                case ViewModeType.Amount:
                    return (new GraphType.Amount.GraphChartObjectImpl(prop_));
                default:
                    return (null);
            }
        }

        private void UpdateGraph(GraphViewData view_data)
        {
            if (view_data == null)return;

            /* データ範囲設定 */
            var area_chart = Chart_Data.ChartAreas[0];

            area_chart.AxisY.Minimum = (prop_.DataValueMinAuto.Value) ? (view_data.DataValueMin) : ((double)prop_.DataValueMin.Value);
            area_chart.AxisY.Maximum = (prop_.DataValueMaxAuto.Value) ? (view_data.DataValueMax * 1.2) : ((double)prop_.DataValueMax.Value);
            area_chart.AxisY.MajorGrid.Interval = (area_chart.AxisY.Maximum - area_chart.AxisY.Minimum) / GRAPH_LINE_Y_MAJOR_NUM;
            area_chart.AxisY.MinorGrid.Interval = (area_chart.AxisY.Maximum - area_chart.AxisY.Minimum) / GRAPH_LINE_Y_MINOR_NUM;

            /* データ設定 */
            var series = new Series();

            series.ChartType = prop_.GraphType.Value;
            series.XValueType = ChartValueType.Double;

            foreach (var data in view_data.DataValues) {
                series.Points.Add(data);
            }

            Chart_Data.Series[0] = series;
        }

        private void UpdateDrawTimer()
        {
            update_timer_.Stop();
            update_timer_.Interval = (int)prop_.DrawInterval.Value;
            update_timer_.Start();
        }

        protected override void OnBackupProperty()
        {
            var prop = Property as ViewPropertyImpl;

            prop.ViewMode.Value = (ViewModeType)CBox_ViewMode.SelectedItem;
            prop.GraphType.Value = (SeriesChartType)CBox_GraphType.SelectedItem;
            prop.DrawPointNum.Value = Num_DrawPointNum.Value;
            prop.DrawInterval.Value = Num_DrawInterval.Value;
            prop.DataValueMin.Value = Num_DataValueMin.Value;
            prop.DataValueMinAuto.Value = !ChkBox_DataValueMin.Checked;
            prop.DataValueMax.Value = Num_DataValueMax.Value;
            prop.DataValueMaxAuto.Value = !ChkBox_DataValueMax.Checked;
            prop.DataValueOffset.Value = Num_DataValueOffset.Value;
            prop.InputDataType.Value = (DataType)CBox_InputDataType.SelectedItem;
            prop.InputByteEndian.Value = (EndianType)CBox_InputByteEndian.SelectedItem;
            prop.InputCycle.Value = Num_InputCycle.Value;
            prop.SamplingInterval.Value = Num_SamplingInterval.Value;
        }

        protected override void OnClearPacket()
        {
            /* グラフ情報をリセット */
            chart_ = CreateChartObject();
        }

        protected override void OnDrawPacketBegin(bool auto_scroll)
        {
        }

        protected override void OnDrawPacketEnd(bool auto_scroll)
        {
        }

        protected override void OnDrawPacket(PacketObject packet)
        {
            if (chart_ == null)return;

            var packet_d = packet as DataPacketObject;

            /* データパケット以外は無視 */
            if (packet_d == null)return;

            /* チャートマネージャーに入力 */
            chart_.InputPacket(packet_d);
        }

        private void OnUpdateTimer(object sender, EventArgs e)
        {
            if (chart_ == null)return;

            if (chart_.IsViewUpdate) {
                /* ビュー情報を更新 */
                UpdateGraph(chart_.LoadViewData());
            }
        }

        private void Setting_ValueChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void Setting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                BackupProperty();
                UpdateView();
                Apply();
            }
        }

        private void Setting_SelectedIndexChanged(object sender, EventArgs e)
        {
            BackupProperty();
            UpdateView();
            Apply();
        }

        private void Num_DataValueMin_ValueChanged(object sender, EventArgs e)
        {
            Num_DataValueMax.Value = Math.Max(Num_DataValueMax.Value, Num_DataValueMin.Value);
            UpdateView();
        }

        private void Num_DataValueMax_ValueChanged(object sender, EventArgs e)
        {
            Num_DataValueMin.Value = Math.Min(Num_DataValueMin.Value, Num_DataValueMax.Value);
            UpdateView();
        }
    }
}
