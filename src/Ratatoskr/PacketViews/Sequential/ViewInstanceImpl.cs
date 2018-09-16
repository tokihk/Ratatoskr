#define RICHTEXTBOX_MODE
#define WINAPI_MODE
#define LINENO_VIEW

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Native;
using Ratatoskr.Packet;
using Ratatoskr.Utility;

namespace Ratatoskr.PacketViews.Sequential
{
    internal sealed class ViewInstanceImpl : ViewInstance
    {
        private const int  VIEW_LINE_NUMBER_UPDATE_IVAL_MIN  = 50;
        private const int  VIEW_LINE_NUMBER_UPDATE_IVAL_MAX  = 200;
        private const int  VIEW_LINE_NUMBER_UPDATE_IVAL_STEP = 50;

        private readonly bool WINAPI_MODE;
        private readonly bool VIEW_DATA_LIMIT;
        private readonly int  VIEW_DATA_LIMIT_SIZE;
        private readonly bool VIEW_LINE_NUMBER_VISIBLE;

        private ViewPropertyImpl prop_;

        private Encoding text_encoder_;

        private StringFormat view_line_number_format_;
        private int          view_line_number_scroll_;
        private int          view_line_height_ = 0;
        private Timer        view_line_update_timer_;
        private int          view_line_update_timer_ival_ = VIEW_LINE_NUMBER_UPDATE_IVAL_MIN;
        private bool         view_line_update_req_ = false;

        private bool select_busy_ = false;
        private int  select_pos_start_;
        private int  select_pos_end_;

        private UInt16 carry_data_ = 0;
        private Queue<byte> char_cache_ = new Queue<byte>();
        private int         char_cache_limit_ = 0;

        private byte[] lf_code_ = null;
        private int    lf_code_pos_ = 0;

        private StringBuilder   draw_buffer_ = null;

        private System.Windows.Forms.Panel panel1;
        private ViewTextBox TBox_Main;
        private System.Windows.Forms.GroupBox GBox_ShiftBit;
        private System.Windows.Forms.GroupBox GBox_EndLine;
        private System.Windows.Forms.TextBox TBox_LFCode;
        private System.Windows.Forms.GroupBox GBox_ViewMode;
        private System.Windows.Forms.ComboBox CBox_DrawType;
        private System.Windows.Forms.CheckBox ChkBox_EchoBack;
        private System.Windows.Forms.SplitContainer Splitter_Main;
        private System.Windows.Forms.SplitContainer Splitter_Compare;
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart_CompareRate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TBox_BoundaryText;
        private System.Windows.Forms.Panel Panel_CompareSetting;
        private System.Windows.Forms.TextBox TBox_ComparePattern;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel Panel_CompareStatus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox GBox_Input;
        private System.Windows.Forms.NumericUpDown Num_ShiftBit;
        private System.Windows.Forms.PictureBox PBox_LineNo;
        private PacketAttribute draw_data_type_ = 0;


        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.GBox_ViewMode = new System.Windows.Forms.GroupBox();
            this.CBox_DrawType = new System.Windows.Forms.ComboBox();
            this.GBox_EndLine = new System.Windows.Forms.GroupBox();
            this.TBox_LFCode = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TBox_BoundaryText = new System.Windows.Forms.TextBox();
            this.GBox_Input = new System.Windows.Forms.GroupBox();
            this.GBox_ShiftBit = new System.Windows.Forms.GroupBox();
            this.Num_ShiftBit = new System.Windows.Forms.NumericUpDown();
            this.ChkBox_EchoBack = new System.Windows.Forms.CheckBox();
            this.TBox_Main = new Ratatoskr.PacketViews.Sequential.ViewTextBox();
            this.Splitter_Main = new System.Windows.Forms.SplitContainer();
            this.PBox_LineNo = new System.Windows.Forms.PictureBox();
            this.Splitter_Compare = new System.Windows.Forms.SplitContainer();
            this.TBox_ComparePattern = new System.Windows.Forms.TextBox();
            this.Panel_CompareSetting = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.Chart_CompareRate = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Panel_CompareStatus = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.GBox_ViewMode.SuspendLayout();
            this.GBox_EndLine.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.GBox_Input.SuspendLayout();
            this.GBox_ShiftBit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_ShiftBit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Splitter_Main)).BeginInit();
            this.Splitter_Main.Panel1.SuspendLayout();
            this.Splitter_Main.Panel2.SuspendLayout();
            this.Splitter_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBox_LineNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Splitter_Compare)).BeginInit();
            this.Splitter_Compare.Panel1.SuspendLayout();
            this.Splitter_Compare.Panel2.SuspendLayout();
            this.Splitter_Compare.SuspendLayout();
            this.Panel_CompareSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chart_CompareRate)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.GBox_Input);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(922, 74);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.GBox_ViewMode);
            this.groupBox2.Controls.Add(this.GBox_EndLine);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Location = new System.Drawing.Point(193, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(505, 64);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "View setting";
            // 
            // GBox_ViewMode
            // 
            this.GBox_ViewMode.Controls.Add(this.CBox_DrawType);
            this.GBox_ViewMode.Location = new System.Drawing.Point(6, 16);
            this.GBox_ViewMode.Name = "GBox_ViewMode";
            this.GBox_ViewMode.Size = new System.Drawing.Size(120, 42);
            this.GBox_ViewMode.TabIndex = 1;
            this.GBox_ViewMode.TabStop = false;
            this.GBox_ViewMode.Text = "Draw type";
            // 
            // CBox_DrawType
            // 
            this.CBox_DrawType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_DrawType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_DrawType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CBox_DrawType.FormattingEnabled = true;
            this.CBox_DrawType.Location = new System.Drawing.Point(3, 15);
            this.CBox_DrawType.Name = "CBox_DrawType";
            this.CBox_DrawType.Size = new System.Drawing.Size(114, 20);
            this.CBox_DrawType.TabIndex = 0;
            // 
            // GBox_EndLine
            // 
            this.GBox_EndLine.Controls.Add(this.TBox_LFCode);
            this.GBox_EndLine.Location = new System.Drawing.Point(318, 16);
            this.GBox_EndLine.Name = "GBox_EndLine";
            this.GBox_EndLine.Size = new System.Drawing.Size(180, 41);
            this.GBox_EndLine.TabIndex = 2;
            this.GBox_EndLine.TabStop = false;
            this.GBox_EndLine.Text = "LF(Line Feed) Code";
            // 
            // TBox_LFCode
            // 
            this.TBox_LFCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TBox_LFCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_LFCode.Location = new System.Drawing.Point(3, 15);
            this.TBox_LFCode.Name = "TBox_LFCode";
            this.TBox_LFCode.Size = new System.Drawing.Size(174, 19);
            this.TBox_LFCode.TabIndex = 0;
            this.TBox_LFCode.TextChanged += new System.EventHandler(this.TBox_LFCode_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TBox_BoundaryText);
            this.groupBox1.Location = new System.Drawing.Point(132, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(180, 41);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HEX/BIN Boundary text";
            // 
            // TBox_BoundaryText
            // 
            this.TBox_BoundaryText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TBox_BoundaryText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_BoundaryText.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TBox_BoundaryText.Location = new System.Drawing.Point(3, 15);
            this.TBox_BoundaryText.Name = "TBox_BoundaryText";
            this.TBox_BoundaryText.Size = new System.Drawing.Size(174, 19);
            this.TBox_BoundaryText.TabIndex = 0;
            // 
            // GBox_Input
            // 
            this.GBox_Input.Controls.Add(this.GBox_ShiftBit);
            this.GBox_Input.Controls.Add(this.ChkBox_EchoBack);
            this.GBox_Input.Location = new System.Drawing.Point(3, 3);
            this.GBox_Input.Name = "GBox_Input";
            this.GBox_Input.Size = new System.Drawing.Size(184, 64);
            this.GBox_Input.TabIndex = 6;
            this.GBox_Input.TabStop = false;
            this.GBox_Input.Text = "Input setting";
            // 
            // GBox_ShiftBit
            // 
            this.GBox_ShiftBit.Controls.Add(this.Num_ShiftBit);
            this.GBox_ShiftBit.Location = new System.Drawing.Point(6, 18);
            this.GBox_ShiftBit.Name = "GBox_ShiftBit";
            this.GBox_ShiftBit.Size = new System.Drawing.Size(80, 41);
            this.GBox_ShiftBit.TabIndex = 1;
            this.GBox_ShiftBit.TabStop = false;
            this.GBox_ShiftBit.Text = "Bit shift";
            // 
            // Num_ShiftBit
            // 
            this.Num_ShiftBit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Num_ShiftBit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Num_ShiftBit.Location = new System.Drawing.Point(3, 15);
            this.Num_ShiftBit.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.Num_ShiftBit.Name = "Num_ShiftBit";
            this.Num_ShiftBit.Size = new System.Drawing.Size(74, 19);
            this.Num_ShiftBit.TabIndex = 0;
            this.Num_ShiftBit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ChkBox_EchoBack
            // 
            this.ChkBox_EchoBack.AutoSize = true;
            this.ChkBox_EchoBack.Location = new System.Drawing.Point(92, 34);
            this.ChkBox_EchoBack.Name = "ChkBox_EchoBack";
            this.ChkBox_EchoBack.Size = new System.Drawing.Size(77, 16);
            this.ChkBox_EchoBack.TabIndex = 3;
            this.ChkBox_EchoBack.Text = "Echo back";
            this.ChkBox_EchoBack.UseVisualStyleBackColor = true;
            this.ChkBox_EchoBack.CheckedChanged += new System.EventHandler(this.ChkBox_EchoBack_CheckedChanged);
            // 
            // TBox_Main
            // 
            this.TBox_Main.BackColor = System.Drawing.Color.White;
            this.TBox_Main.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBox_Main.DetectUrls = false;
            this.TBox_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_Main.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TBox_Main.HideSelection = false;
            this.TBox_Main.Location = new System.Drawing.Point(70, 0);
            this.TBox_Main.Name = "TBox_Main";
            this.TBox_Main.ReadOnly = true;
            this.TBox_Main.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.TBox_Main.Size = new System.Drawing.Size(237, 490);
            this.TBox_Main.TabIndex = 1;
            this.TBox_Main.Text = "";
            this.TBox_Main.VScroll += new System.EventHandler(this.TBox_Main_VScroll);
            this.TBox_Main.TextChanged += new System.EventHandler(this.TBox_Main_TextChanged);
            this.TBox_Main.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TBox_Main_MouseDown);
            this.TBox_Main.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TBox_Main_MouseUp);
            this.TBox_Main.Resize += new System.EventHandler(this.TBox_Main_Resize);
            // 
            // Splitter_Main
            // 
            this.Splitter_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Splitter_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.Splitter_Main.Location = new System.Drawing.Point(0, 74);
            this.Splitter_Main.Name = "Splitter_Main";
            // 
            // Splitter_Main.Panel1
            // 
            this.Splitter_Main.Panel1.Controls.Add(this.TBox_Main);
            this.Splitter_Main.Panel1.Controls.Add(this.PBox_LineNo);
            // 
            // Splitter_Main.Panel2
            // 
            this.Splitter_Main.Panel2.Controls.Add(this.Splitter_Compare);
            this.Splitter_Main.Size = new System.Drawing.Size(922, 490);
            this.Splitter_Main.SplitterDistance = 307;
            this.Splitter_Main.TabIndex = 2;
            // 
            // PBox_LineNo
            // 
            this.PBox_LineNo.Dock = System.Windows.Forms.DockStyle.Left;
            this.PBox_LineNo.Location = new System.Drawing.Point(0, 0);
            this.PBox_LineNo.Name = "PBox_LineNo";
            this.PBox_LineNo.Size = new System.Drawing.Size(70, 490);
            this.PBox_LineNo.TabIndex = 2;
            this.PBox_LineNo.TabStop = false;
            this.PBox_LineNo.Paint += new System.Windows.Forms.PaintEventHandler(this.PBox_LineNo_Paint);
            // 
            // Splitter_Compare
            // 
            this.Splitter_Compare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Splitter_Compare.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.Splitter_Compare.Location = new System.Drawing.Point(0, 0);
            this.Splitter_Compare.Name = "Splitter_Compare";
            this.Splitter_Compare.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Splitter_Compare.Panel1
            // 
            this.Splitter_Compare.Panel1.Controls.Add(this.TBox_ComparePattern);
            this.Splitter_Compare.Panel1.Controls.Add(this.Panel_CompareSetting);
            // 
            // Splitter_Compare.Panel2
            // 
            this.Splitter_Compare.Panel2.Controls.Add(this.Chart_CompareRate);
            this.Splitter_Compare.Panel2.Controls.Add(this.Panel_CompareStatus);
            this.Splitter_Compare.Size = new System.Drawing.Size(611, 490);
            this.Splitter_Compare.SplitterDistance = 208;
            this.Splitter_Compare.TabIndex = 0;
            // 
            // TBox_ComparePattern
            // 
            this.TBox_ComparePattern.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_ComparePattern.Location = new System.Drawing.Point(79, 0);
            this.TBox_ComparePattern.Multiline = true;
            this.TBox_ComparePattern.Name = "TBox_ComparePattern";
            this.TBox_ComparePattern.Size = new System.Drawing.Size(532, 208);
            this.TBox_ComparePattern.TabIndex = 1;
            // 
            // Panel_CompareSetting
            // 
            this.Panel_CompareSetting.Controls.Add(this.button1);
            this.Panel_CompareSetting.Dock = System.Windows.Forms.DockStyle.Left;
            this.Panel_CompareSetting.Location = new System.Drawing.Point(0, 0);
            this.Panel_CompareSetting.Name = "Panel_CompareSetting";
            this.Panel_CompareSetting.Size = new System.Drawing.Size(79, 208);
            this.Panel_CompareSetting.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(3, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Preset";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Chart_CompareRate
            // 
            this.Chart_CompareRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Chart_CompareRate.Location = new System.Drawing.Point(0, 68);
            this.Chart_CompareRate.Name = "Chart_CompareRate";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series6.Name = "Series_Rate";
            series6.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.UInt32;
            series6.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.UInt32;
            this.Chart_CompareRate.Series.Add(series6);
            this.Chart_CompareRate.Size = new System.Drawing.Size(611, 210);
            this.Chart_CompareRate.TabIndex = 0;
            this.Chart_CompareRate.Text = "chart1";
            // 
            // Panel_CompareStatus
            // 
            this.Panel_CompareStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_CompareStatus.Location = new System.Drawing.Point(0, 0);
            this.Panel_CompareStatus.Name = "Panel_CompareStatus";
            this.Panel_CompareStatus.Size = new System.Drawing.Size(611, 68);
            this.Panel_CompareStatus.TabIndex = 1;
            // 
            // ViewInstanceImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.Splitter_Main);
            this.Controls.Add(this.panel1);
            this.Name = "ViewInstanceImpl";
            this.Size = new System.Drawing.Size(922, 564);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.GBox_ViewMode.ResumeLayout(false);
            this.GBox_EndLine.ResumeLayout(false);
            this.GBox_EndLine.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.GBox_Input.ResumeLayout(false);
            this.GBox_Input.PerformLayout();
            this.GBox_ShiftBit.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_ShiftBit)).EndInit();
            this.Splitter_Main.Panel1.ResumeLayout(false);
            this.Splitter_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Splitter_Main)).EndInit();
            this.Splitter_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PBox_LineNo)).EndInit();
            this.Splitter_Compare.Panel1.ResumeLayout(false);
            this.Splitter_Compare.Panel1.PerformLayout();
            this.Splitter_Compare.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Splitter_Compare)).EndInit();
            this.Splitter_Compare.ResumeLayout(false);
            this.Panel_CompareSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Chart_CompareRate)).EndInit();
            this.ResumeLayout(false);

        }

        public ViewInstanceImpl() : base()
        {
            InitializeComponent();
        }

        public ViewInstanceImpl(ViewManager viewm, ViewClass viewd, ViewProperty viewp, Guid id) : base(viewm, viewd, viewp, id)
        {
            prop_ = Property as ViewPropertyImpl;
            WINAPI_MODE = ConfigManager.System.ApplicationCore.Sequential_WinApiMode.Value;
            VIEW_DATA_LIMIT = (WINAPI_MODE && ConfigManager.System.ApplicationCore.Sequential_ViewCharCountLimitEnable.Value);
            VIEW_DATA_LIMIT_SIZE = (int)ConfigManager.System.ApplicationCore.Sequential_ViewCharCountLimit.Value;
            VIEW_LINE_NUMBER_VISIBLE = (WINAPI_MODE && ConfigManager.System.ApplicationCore.Sequential_LineNoVisible.Value);

            InitializeComponent();
            InitializeDrawType();
            InitializeLineNumberPanel();

            Num_ShiftBit.Value = prop_.ShiftBit.Value;
            ChkBox_EchoBack.Checked = prop_.EchoBack.Value;

            CBox_DrawType.SelectedItem = prop_.DrawType.Value;
            TBox_BoundaryText.Text = prop_.BoundaryText.Value.TrimEnd(new char[] { '\r', '\n' });
            TBox_LFCode.Text = prop_.EndLinePattern.Value.Trim();

            /* TODO: 未実装なので今は非表示 */
            Splitter_Main.Panel2Collapsed = true;

            UpdateViewLineNumber();
        }

        private void InitializeLineNumberPanel()
        {
            PBox_LineNo.Visible = VIEW_LINE_NUMBER_VISIBLE;

            if (PBox_LineNo.Visible) {
                view_line_number_format_ = new StringFormat()
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Center,
                };

                view_line_update_timer_ = new Timer();
                view_line_update_timer_.Tick += OnViewLineNumberUpdateTimer;
                view_line_update_timer_.Start();
            }
        }

        private void InitializeDrawType()
        {
            CBox_DrawType.BeginUpdate();
            {
                CBox_DrawType.Items.Clear();
                foreach (var value in Enum.GetValues(typeof(DrawDataType))) {
                    CBox_DrawType.Items.Add(value);
                }
            }
            CBox_DrawType.EndUpdate();
        }

        protected override void OnBackupProperty()
        {
            prop_ = Property as ViewPropertyImpl;

            prop_.ShiftBit.Value = Num_ShiftBit.Value;
            prop_.EchoBack.Value = ChkBox_EchoBack.Checked;

            prop_.DrawType.Value = (DrawDataType)CBox_DrawType.SelectedItem;
            prop_.BoundaryText.Value = TBox_BoundaryText.Text;
            prop_.EndLinePattern.Value = TBox_LFCode.Text;
        }

        private void UpdateViewLineNumber()
        {
            if (PBox_LineNo.Visible) {
                view_line_update_req_ = true;
            }
        }

        private void OnViewLineNumberUpdateTimer(object sender, EventArgs e)
        {
            var timer_ival = view_line_update_timer_ival_;

            if (view_line_update_req_) {
                view_line_update_req_ = false;

                PBox_LineNo.Invalidate();

                timer_ival += VIEW_LINE_NUMBER_UPDATE_IVAL_STEP;
                if (timer_ival > VIEW_LINE_NUMBER_UPDATE_IVAL_MAX) {
                    timer_ival = VIEW_LINE_NUMBER_UPDATE_IVAL_MAX;
                }
                
            } else {
                timer_ival -= VIEW_LINE_NUMBER_UPDATE_IVAL_STEP;
                if (timer_ival < VIEW_LINE_NUMBER_UPDATE_IVAL_MIN) {
                    timer_ival = VIEW_LINE_NUMBER_UPDATE_IVAL_MIN;
                }
            }

            if (view_line_update_timer_ival_ != timer_ival) {
                view_line_update_timer_ival_ = timer_ival;

                view_line_update_timer_.Stop();
                view_line_update_timer_.Interval = view_line_update_timer_ival_;
                view_line_update_timer_.Start();
            }
        }

        private void DrawBufferReset()
        {
            draw_buffer_ = new StringBuilder(2048);
        }

        private void DrawBufferPushBegin(PacketAttribute type)
        {
            /* データタイプが変化した場合は現在溜まっているバッファを出力 */
            if (draw_data_type_ != type) {
                if (TBox_Main.TextLength > 0) {
                    DrawBufferPushEndLine();
                }

                DrawBufferFlush();
            }

            draw_data_type_ = type;
        }

        private void DrawBufferPush(string text)
        {
            draw_buffer_.Append(text);
        }

        private void DrawBufferPushEndLine()
        {
            draw_buffer_.AppendLine();
        }

        private unsafe void DrawBufferFlush()
        {
            var color_fore = Color.Black;

            switch (draw_data_type_) {
                case PacketAttribute.Message:
                    color_fore = Color.Green;
                    break;
                case PacketAttribute.Data:
                    switch (prop_.DrawType.Value) {
                        case DrawDataType.HEX: color_fore = Color.Blue;     break;
                        case DrawDataType.BIN: color_fore = Color.Brown;    break;
                        default:               color_fore = Color.Red;      break;
                    }
                    break;
            }

            /* エディターの最後にキャレットを移動 */
            TBox_Main.SelectionStart = TBox_Main.TextLength;

            /* 入力色設定 */
            TBox_Main.SelectionColor = color_fore;

            /* テキスト追加 */
            if (WINAPI_MODE) {
                WinAPI.SendMessage(TBox_Main.Handle, WinAPI.EM_REPLACESEL, 0, draw_buffer_.ToString());
            } else {
                TBox_Main.AppendText(draw_buffer_.ToString());
            }

            DrawBufferReset();
        }


        private void LoadCurrentProperty()
        {
            
        }

        private Encoding LoadTextEncoder(DrawDataType type)
        {
            switch (type) {
                case DrawDataType.ShiftJIS:     return (Encoding.GetEncoding(932));
                case DrawDataType.UTF8:         return (Encoding.UTF8);
                default:                        return (Encoding.ASCII);
            }
        }

        private void UpdateLFCodeView()
        {
            /* 改行パターン取得 */
            lf_code_ = HexTextEncoder.ToByteArray(TBox_LFCode.Text);

            /* 背景色設定 */
            if (TBox_LFCode.Text.Length == 0) {
                TBox_LFCode.BackColor = Color.White;
            } else {
                TBox_LFCode.BackColor = (lf_code_ != null) ? (Color.SkyBlue) : (Color.Pink);
            }
        }

        private void DrawMessagePacket(PacketObject packet)
        {
            var str = new StringBuilder();

            str.AppendFormat("[{0}] {1}", packet.MakeTime.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss.fff"), packet.Alias);

            if (packet.Information.Length > 0) {
                str.AppendFormat(" - {0}", packet.Information);
            }

            if (packet.Message.Length > 0) {
                str.AppendFormat(" - {0}", packet.Message);
            }

            str.AppendLine();

            DrawBufferPushBegin(PacketAttribute.Message);
            DrawBufferPush(str.ToString());
        }

        private void DrawDataPacket(PacketObject packet)
        {
            if (   (!prop_.EchoBack.Value)
                && (packet.Direction != PacketDirection.Recv)
            ) {
                return;
            }

            /* 表示処理 */
            var draw_data = (byte)0;

            DrawBufferPushBegin(PacketAttribute.Data);
            foreach (var data_one in packet.Data) {
                /* 入力データをシフト処理 */
                draw_data = DataShift(data_one);

                /* 表示文字列を作成 */
                DrawBufferPush(DataToText(prop_.DrawType.Value, draw_data));

                /* 改行判定 */
                if (DataDivideCheck(draw_data)) {
                    DrawBufferPushEndLine();
                }
            }
        }

        private byte DataShift(byte data)
        {
            /* シフト処理 */
            if (prop_.ShiftBit.Value > 0) {
                var data_temp = (UInt16)(carry_data_ << 8);

                data_temp |= (UInt16)((UInt16)data << (8 - (UInt16)prop_.ShiftBit.Value));
                data = (byte)(data_temp >> 8);
                carry_data_ = (UInt16)(data_temp & 0x00FF);
            }

            return (data);
        }

        private string DataToText(DrawDataType type, byte data)
        {
            var view_text = (string)null;

            switch (type) {
                case DrawDataType.ASCII:
                    view_text = DataToText_Ascii(data);
                    break;

                case DrawDataType.ShiftJIS:
                    view_text = DataToText_ShiftJIS(data);
                    break;

                case DrawDataType.UTF8:
                    view_text = DataToText_UTF8(data);
                    break;

                case DrawDataType.BIN:
                    view_text = HexTextEncoder.BinCode[data] + prop_.BoundaryText.Value;
                    break;

                case DrawDataType.HEX:
                    view_text = HexTextEncoder.HexCode[data] + prop_.BoundaryText.Value;
                    break;

                default:
                    view_text = "";
                    break;
            }

            /* リッチテキストは何故か\r単体でも改行してしまうので\rをスペースに変換する */
            view_text = view_text.Replace('\r', ' ');

            return (view_text);
        }

        private string DataToText_Ascii(byte data)
        {
            return (text_encoder_.GetString(new byte[] { data }));
        }

        private string DataToText_ShiftJIS(byte data)
        {
            char_cache_.Enqueue(data);

            if (   ((data >= 0x81) && (data <= 0x9F))
                || ((data >= 0xE0) && (data <= 0xFC))
            ) {
                /* マルチバイトの１文字目 */
                return ("");

            } else {
                /* マルチバイトの２文字目 */
                var text = text_encoder_.GetString(char_cache_.ToArray());

                char_cache_.Clear();

                return (text);
            }
        }

        private string DataToText_UTF8(byte data)
        {
                 if ((data & 0xFE) == 0xFC) { char_cache_.Clear(); char_cache_limit_ = 6; }
            else if ((data & 0xFC) == 0xF8) { char_cache_.Clear(); char_cache_limit_ = 5; }
            else if ((data & 0xF8) == 0xF0) { char_cache_.Clear(); char_cache_limit_ = 4; }
            else if ((data & 0xF0) == 0xE0) { char_cache_.Clear(); char_cache_limit_ = 3; }
            else if ((data & 0xE0) == 0xC0) { char_cache_.Clear(); char_cache_limit_ = 2; }
            else if ((data & 0x80) == 0x00) { char_cache_.Clear(); char_cache_limit_ = 1; }

            char_cache_.Enqueue(data);

            if (char_cache_.Count >= char_cache_limit_) {
                var result = text_encoder_.GetString(char_cache_.ToArray());

                char_cache_.Clear();
                char_cache_limit_ = 0;

                return (result);

            } else {
                return ("");
            }
        }

        private bool DataDivideCheck(byte data)
        {
            if (   (lf_code_ == null)
                || (lf_code_.Length == 0)
            ) {
                return (false);
            }

            if (lf_code_[lf_code_pos_] == data) {
                /* --- 連続一致 --- */
                lf_code_pos_++;
            } else if (lf_code_[0] == data) {
                /* --- 最初の文字に一致 --- */
                lf_code_pos_ = 1;
            } else {
                /* --- どれにも一致しない --- */
                lf_code_pos_ = 0;
            }

            if (lf_code_pos_ >= lf_code_.Length) {
                lf_code_pos_ = 0;
                return (true);

            } else {
                return (false);
            }
        }

        protected override void OnClearPacket()
        {
            TBox_Main.Clear();
        }

        protected unsafe override void OnDrawPacketBegin(bool auto_scroll)
        {
            /* プロパティ更新 */
            BackupProperty();
            text_encoder_ = LoadTextEncoder((DrawDataType)CBox_DrawType.SelectedItem);

            /* スクロール位置をバックアップ */
            view_line_number_scroll_ = WinAPI.SendMessage(TBox_Main.Handle, WinAPI.EM_GETFIRSTVISIBLELINE, 0, 0).ToInt32();

            /* 選択状態をバックアップ */
            if (WINAPI_MODE) {
                fixed (int *select_pos_start_p = &select_pos_start_) {
                    fixed (int *select_pos_end_p = &select_pos_end_) {
                        WinAPI.SendMessage(TBox_Main.Handle, WinAPI.EM_GETSEL, select_pos_start_p, select_pos_end_p);
                    }
                }
            } else {
                select_pos_start_ = TBox_Main.SelectionStart;
                select_pos_end_ = select_pos_start_ + TBox_Main.SelectionLength;
            }

            /* 出力バッファ初期化 */
            DrawBufferReset();

            /* 描画を一時停止 */
            WinAPI.SendMessage(TBox_Main.Handle, WinAPI.WM_SETREDRAW, 0, 0);
        }

        protected unsafe override void OnDrawPacketEnd(bool auto_scroll, bool next_packet_exist)
        {
            /* 未出力データを表示 */
            DrawBufferFlush();

            /* 自動スクロールの場合はここでスクロール位置を取得。
             * テキストが追加されていれば、この時点でスクロール位置は
             * 終端になっている */
            if (auto_scroll) {
                view_line_number_scroll_ = WinAPI.SendMessage(TBox_Main.Handle, WinAPI.EM_GETFIRSTVISIBLELINE, 0, 0).ToInt32();
            }

            /* 選択状態を復元 */
            if (WINAPI_MODE) {
                WinAPI.SendMessage(TBox_Main.Handle, WinAPI.EM_SETSEL, select_pos_start_, select_pos_end_);
            } else {
                TBox_Main.SelectionStart = select_pos_start_;
                TBox_Main.SelectionLength = select_pos_end_ - select_pos_start_;
            }

            /* スクロール位置を補正 */
            WinAPI.SendMessage(
                TBox_Main.Handle,
                WinAPI.EM_LINESCROLL,
                0,
                view_line_number_scroll_ - WinAPI.SendMessage(TBox_Main.Handle, WinAPI.EM_GETFIRSTVISIBLELINE, 0, 0).ToInt32());

            /* 表示量を制限 */
            if (VIEW_DATA_LIMIT) {
                var text_len = TBox_Main.TextLength;
                var text_len_max = VIEW_DATA_LIMIT_SIZE;

                if (text_len > text_len_max) {
                    fixed (int *select_pos_start_p = &select_pos_start_) {
                        fixed (int *select_pos_end_p = &select_pos_end_) {
                            /* 選択状態をバックアップ */
                            WinAPI.SendMessage(TBox_Main.Handle, WinAPI.EM_GETSEL, select_pos_start_p, select_pos_end_p);

                            /* 削除対象を選択状態にする */
                            WinAPI.SendMessage(TBox_Main.Handle, WinAPI.EM_SETSEL, 0, text_len - text_len_max + text_len_max / 2);

                            /* 削除実行 */
                            WinAPI.SendMessage(TBox_Main.Handle, WinAPI.EM_REPLACESEL, 0, "");

                            /* 選択状態を戻す */
                            WinAPI.SendMessage(TBox_Main.Handle, WinAPI.EM_SETSEL, select_pos_start_, select_pos_end_);
                        }
                    }
                }
            }

            /* 描画を再開 */
            WinAPI.SendMessage(TBox_Main.Handle, WinAPI.WM_SETREDRAW, 1, 0);
            TBox_Main.Invalidate();
        }

        protected override void OnDrawPacket(PacketObject packet)
        {
            switch (packet.Attribute) {
                case PacketAttribute.Message:
                    DrawMessagePacket(packet);
                    break;
                case PacketAttribute.Data:
                    DrawDataPacket(packet);
                    break;
            }
        }

        private void TBox_LFCode_TextChanged(object sender, EventArgs e)
        {
            UpdateLFCodeView();
        }

        private void ChkBox_EchoBack_CheckedChanged(object sender, EventArgs e)
        {
            BackupProperty();
        }

        private unsafe void TBox_Main_TextChanged(object sender, EventArgs e)
        {
            UpdateViewLineNumber();
        }

        private unsafe void TBox_Main_Resize(object sender, EventArgs e)
        {
            UpdateViewLineNumber();
        }

        private void TBox_Main_MouseDown(object sender, MouseEventArgs e)
        {
            select_busy_ = true;
            OperationBusy = select_busy_;
        }

        private void TBox_Main_MouseUp(object sender, MouseEventArgs e)
        {
            select_busy_ = false;
            OperationBusy = select_busy_;
        }

        private void TBox_Main_VScroll(object sender, EventArgs e)
        {
            UpdateViewLineNumber();
        }

        private unsafe void PBox_LineNo_Paint(object sender, PaintEventArgs e)
        {
            var line_no_max = WinAPI.SendMessage(TBox_Main.Handle, WinAPI.EM_GETLINECOUNT, 0, 0).ToInt32();
            var line_no = WinAPI.SendMessage(TBox_Main.Handle, WinAPI.EM_GETFIRSTVISIBLELINE, 0, 0).ToInt32();
            var line_no_char_index = WinAPI.SendMessage(TBox_Main.Handle, WinAPI.EM_LINEINDEX, line_no, 0).ToInt32();
            var line_no_pos_y = TBox_Main.GetPositionFromCharIndex(line_no_char_index).Y;

            /* 1行の長さを計測 */
            if ((view_line_height_ == 0) && (line_no_max > 1)) {
                var line_no_char_index_2 = WinAPI.SendMessage(TBox_Main.Handle, WinAPI.EM_LINEINDEX, line_no + 1, 0).ToInt32();
                var line_no_pos_y_2 = TBox_Main.GetPositionFromCharIndex(line_no_char_index_2).Y;

                view_line_height_ = line_no_pos_y_2 - line_no_pos_y;
            }

            var draw_region = PBox_LineNo.ClientSize;
            var draw_rect = new Rectangle(0, line_no_pos_y + 1, draw_region.Width - 10, view_line_height_);

            /* 0ベースから1ベースに変更 */
            line_no++;

            while ((line_no <= line_no_max) && (draw_rect.Y < draw_region.Height)) {
                e.Graphics.DrawString(line_no.ToString(), TBox_Main.Font, Brushes.Gray, draw_rect, view_line_number_format_);

                line_no++;
                draw_rect.Y += view_line_height_;
            }
        }
    }
}
