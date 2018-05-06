using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Native;
using Ratatoskr.Packet;
using Ratatoskr.Utility;

namespace Ratatoskr.PacketViews.Compare
{
    internal sealed class ViewInstanceImpl : ViewInstance
    {
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox RTBox_Main;
        private System.Windows.Forms.GroupBox GBox_ShiftBit;
        private System.Windows.Forms.GroupBox GBox_EndLine;
        private System.Windows.Forms.TextBox TBox_LFCode;
        private System.Windows.Forms.GroupBox GBox_ViewMode;
        private System.Windows.Forms.ComboBox CBox_DrawType;
        private System.Windows.Forms.CheckBox ChkBox_EchoBack;

        private ViewPropertyImpl prop_;

        private Encoding text_encoder_;

        private int select_pos_start_;
        private int select_pos_length_;

        private UInt16 carry_data_ = 0;
        private Queue<byte> char_cache_ = new Queue<byte>();
        private int         char_cache_limit_ = 0;

        private byte[] lf_code_ = null;
        private int    lf_code_pos_ = 0;

        private StringBuilder   draw_buffer_ = null;
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
        private PacketAttribute draw_data_type_ = 0;


        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            this.RTBox_Main = new System.Windows.Forms.RichTextBox();
            this.Splitter_Main = new System.Windows.Forms.SplitContainer();
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
            // RTBox_Main
            // 
            this.RTBox_Main.BackColor = System.Drawing.Color.White;
            this.RTBox_Main.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RTBox_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RTBox_Main.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RTBox_Main.Location = new System.Drawing.Point(0, 0);
            this.RTBox_Main.Name = "RTBox_Main";
            this.RTBox_Main.ReadOnly = true;
            this.RTBox_Main.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RTBox_Main.Size = new System.Drawing.Size(307, 490);
            this.RTBox_Main.TabIndex = 1;
            this.RTBox_Main.Text = "";
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
            this.Splitter_Main.Panel1.Controls.Add(this.RTBox_Main);
            // 
            // Splitter_Main.Panel2
            // 
            this.Splitter_Main.Panel2.Controls.Add(this.Splitter_Compare);
            this.Splitter_Main.Size = new System.Drawing.Size(922, 490);
            this.Splitter_Main.SplitterDistance = 307;
            this.Splitter_Main.TabIndex = 2;
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
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Name = "Series_Rate";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.UInt32;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.UInt32;
            this.Chart_CompareRate.Series.Add(series1);
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

            InitializeComponent();
            InitializeDrawType();

            Num_ShiftBit.Value = prop_.ShiftBit.Value;
            ChkBox_EchoBack.Checked = prop_.EchoBack.Value;

            CBox_DrawType.SelectedItem = prop_.DrawType.Value;
            TBox_BoundaryText.Text = prop_.BoundaryText.Value;
            TBox_LFCode.Text = prop_.EndLinePattern.Value;

            /* TODO: 未実装なので今は非表示 */
            Splitter_Main.Panel2Collapsed = true;
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

        private void DrawBufferReset()
        {
            draw_buffer_ = new StringBuilder(2048);
        }

        private void DrawBufferPushBegin(PacketAttribute type)
        {
            /* データタイプが変化した場合は現在溜まっているバッファを出力 */
            if (draw_data_type_ != type) {
                if (RTBox_Main.TextLength > 0) {
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

        private void DrawBufferFlush()
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
            RTBox_Main.SelectionStart = RTBox_Main.TextLength;
            RTBox_Main.SelectionColor = color_fore;
            RTBox_Main.AppendText(draw_buffer_.ToString());

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
            RTBox_Main.Clear();
        }

        protected override void OnDrawPacketBegin(bool auto_scroll)
        {
            /* プロパティ更新 */
            BackupProperty();
            text_encoder_ = LoadTextEncoder((DrawDataType)CBox_DrawType.SelectedItem);

            /* 選択状態をバックアップ */
            select_pos_start_ = RTBox_Main.SelectionStart;
            select_pos_length_ = RTBox_Main.SelectionLength;

            /* 出力バッファ初期化 */
            DrawBufferReset();

            if (auto_scroll) {
                /* 自動スクロール中は選択状態を解除して強制的にスクロール */
                RTBox_Main.HideSelection = false;
            }

            /* 描画を一時停止 */
//            NativeMethods.SendMessage(Handle, NativeMethods.WM_SETREDRAW, IntPtr.Zero, IntPtr.Zero);
        }

        protected override void OnDrawPacketEnd(bool auto_scroll)
        {
            /* 未出力データを表示 */
            DrawBufferFlush();

            if (!auto_scroll) {
                /* 選択状態を復元 */
                RTBox_Main.SelectionStart = select_pos_start_;
                RTBox_Main.SelectionLength = select_pos_length_;
            }

            /* 描画を再開 */
//            NativeMethods.SendMessage(Handle, NativeMethods.WM_SETREDRAW, new IntPtr(1), IntPtr.Zero);
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
    }
}
