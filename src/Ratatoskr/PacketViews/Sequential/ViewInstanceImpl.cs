using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Native;
using Ratatoskr.Generic;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;

namespace Ratatoskr.PacketViews.Sequential
{
    internal sealed class ViewInstanceImpl : ViewInstance
    {
        private enum DrawDataType
        {
            Control,
            Message,
            Data,
        }


        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox RTBox_Main;
        private System.Windows.Forms.GroupBox GBox_ShiftBit;
        private System.Windows.Forms.GroupBox GBox_EndLine;
        private System.Windows.Forms.TextBox TBox_EndLine;
        private System.Windows.Forms.GroupBox GBox_ViewMode;
        private System.Windows.Forms.ComboBox CBox_ViewMode;
        private System.Windows.Forms.ComboBox CBox_ShiftBit;
        private System.Windows.Forms.GroupBox GBox_CharCode;
        private System.Windows.Forms.ComboBox CBox_CharCode;
        private System.Windows.Forms.CheckBox ChkBox_EchoBack;

        private ViewPropertyImpl prop_;
        private Encoding encoder_;

        private int select_pos_start_;
        private int select_pos_length_;

        private UInt16 carry_data_ = 0;
        private Queue<byte> char_cache_ = new Queue<byte>();
        private int         char_cache_limit_ = 0;

        private byte[] lf_code_ = null;
        private int    lf_code_pos_ = 0;

        private StringBuilder   draw_buffer_ = null;
        private PacketAttribute draw_data_type_ = 0;


        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.ChkBox_EchoBack = new System.Windows.Forms.CheckBox();
            this.GBox_CharCode = new System.Windows.Forms.GroupBox();
            this.CBox_CharCode = new System.Windows.Forms.ComboBox();
            this.GBox_EndLine = new System.Windows.Forms.GroupBox();
            this.TBox_EndLine = new System.Windows.Forms.TextBox();
            this.GBox_ShiftBit = new System.Windows.Forms.GroupBox();
            this.CBox_ShiftBit = new System.Windows.Forms.ComboBox();
            this.GBox_ViewMode = new System.Windows.Forms.GroupBox();
            this.CBox_ViewMode = new System.Windows.Forms.ComboBox();
            this.RTBox_Main = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.GBox_CharCode.SuspendLayout();
            this.GBox_EndLine.SuspendLayout();
            this.GBox_ShiftBit.SuspendLayout();
            this.GBox_ViewMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ChkBox_EchoBack);
            this.panel1.Controls.Add(this.GBox_CharCode);
            this.panel1.Controls.Add(this.GBox_EndLine);
            this.panel1.Controls.Add(this.GBox_ShiftBit);
            this.panel1.Controls.Add(this.GBox_ViewMode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(922, 54);
            this.panel1.TabIndex = 0;
            // 
            // ChkBox_EchoBack
            // 
            this.ChkBox_EchoBack.AutoSize = true;
            this.ChkBox_EchoBack.Location = new System.Drawing.Point(493, 21);
            this.ChkBox_EchoBack.Name = "ChkBox_EchoBack";
            this.ChkBox_EchoBack.Size = new System.Drawing.Size(77, 16);
            this.ChkBox_EchoBack.TabIndex = 3;
            this.ChkBox_EchoBack.Text = "Echo back";
            this.ChkBox_EchoBack.UseVisualStyleBackColor = true;
            this.ChkBox_EchoBack.CheckedChanged += new System.EventHandler(this.ChkBox_EchoBack_CheckedChanged);
            // 
            // GBox_CharCode
            // 
            this.GBox_CharCode.Controls.Add(this.CBox_CharCode);
            this.GBox_CharCode.Location = new System.Drawing.Point(366, 7);
            this.GBox_CharCode.Name = "GBox_CharCode";
            this.GBox_CharCode.Size = new System.Drawing.Size(120, 41);
            this.GBox_CharCode.TabIndex = 2;
            this.GBox_CharCode.TabStop = false;
            this.GBox_CharCode.Text = "Character code";
            // 
            // CBox_CharCode
            // 
            this.CBox_CharCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_CharCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_CharCode.FormattingEnabled = true;
            this.CBox_CharCode.Location = new System.Drawing.Point(3, 15);
            this.CBox_CharCode.Name = "CBox_CharCode";
            this.CBox_CharCode.Size = new System.Drawing.Size(114, 20);
            this.CBox_CharCode.TabIndex = 0;
            // 
            // GBox_EndLine
            // 
            this.GBox_EndLine.Controls.Add(this.TBox_EndLine);
            this.GBox_EndLine.Location = new System.Drawing.Point(215, 7);
            this.GBox_EndLine.Name = "GBox_EndLine";
            this.GBox_EndLine.Size = new System.Drawing.Size(145, 41);
            this.GBox_EndLine.TabIndex = 2;
            this.GBox_EndLine.TabStop = false;
            this.GBox_EndLine.Text = "Line feed pattern";
            // 
            // TBox_EndLine
            // 
            this.TBox_EndLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_EndLine.Location = new System.Drawing.Point(3, 15);
            this.TBox_EndLine.Name = "TBox_EndLine";
            this.TBox_EndLine.Size = new System.Drawing.Size(139, 19);
            this.TBox_EndLine.TabIndex = 0;
            this.TBox_EndLine.TextChanged += new System.EventHandler(this.TBox_EndLine_TextChanged);
            // 
            // GBox_ShiftBit
            // 
            this.GBox_ShiftBit.Controls.Add(this.CBox_ShiftBit);
            this.GBox_ShiftBit.Location = new System.Drawing.Point(129, 7);
            this.GBox_ShiftBit.Name = "GBox_ShiftBit";
            this.GBox_ShiftBit.Size = new System.Drawing.Size(80, 41);
            this.GBox_ShiftBit.TabIndex = 1;
            this.GBox_ShiftBit.TabStop = false;
            this.GBox_ShiftBit.Text = "Bit shift";
            // 
            // CBox_ShiftBit
            // 
            this.CBox_ShiftBit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_ShiftBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_ShiftBit.FormattingEnabled = true;
            this.CBox_ShiftBit.Location = new System.Drawing.Point(3, 15);
            this.CBox_ShiftBit.Name = "CBox_ShiftBit";
            this.CBox_ShiftBit.Size = new System.Drawing.Size(74, 20);
            this.CBox_ShiftBit.TabIndex = 0;
            // 
            // GBox_ViewMode
            // 
            this.GBox_ViewMode.Controls.Add(this.CBox_ViewMode);
            this.GBox_ViewMode.Location = new System.Drawing.Point(3, 6);
            this.GBox_ViewMode.Name = "GBox_ViewMode";
            this.GBox_ViewMode.Size = new System.Drawing.Size(120, 42);
            this.GBox_ViewMode.TabIndex = 1;
            this.GBox_ViewMode.TabStop = false;
            this.GBox_ViewMode.Text = "View mode";
            // 
            // CBox_ViewMode
            // 
            this.CBox_ViewMode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_ViewMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_ViewMode.FormattingEnabled = true;
            this.CBox_ViewMode.Location = new System.Drawing.Point(3, 15);
            this.CBox_ViewMode.Name = "CBox_ViewMode";
            this.CBox_ViewMode.Size = new System.Drawing.Size(114, 20);
            this.CBox_ViewMode.TabIndex = 0;
            // 
            // RTBox_Main
            // 
            this.RTBox_Main.BackColor = System.Drawing.Color.White;
            this.RTBox_Main.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RTBox_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RTBox_Main.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RTBox_Main.Location = new System.Drawing.Point(0, 54);
            this.RTBox_Main.Name = "RTBox_Main";
            this.RTBox_Main.ReadOnly = true;
            this.RTBox_Main.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.RTBox_Main.Size = new System.Drawing.Size(922, 510);
            this.RTBox_Main.TabIndex = 1;
            this.RTBox_Main.Text = "";
            // 
            // ViewInstanceImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.RTBox_Main);
            this.Controls.Add(this.panel1);
            this.Name = "ViewInstanceImpl";
            this.Size = new System.Drawing.Size(922, 564);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.GBox_CharCode.ResumeLayout(false);
            this.GBox_EndLine.ResumeLayout(false);
            this.GBox_EndLine.PerformLayout();
            this.GBox_ShiftBit.ResumeLayout(false);
            this.GBox_ViewMode.ResumeLayout(false);
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
            InitializeViewMode(prop_.DataView.Value);
            InitializeShiftBit((int)prop_.ShiftBit.Value);
            InitializeEndLineCode(prop_.EndLinePattern.Value);
            InitializeCharCodeType(prop_.CharCode.Value);

            ChkBox_EchoBack.Checked = prop_.EchoBack.Value;
        }

        private void InitializeViewMode(DataViewType type)
        {
            CBox_ViewMode.BeginUpdate();
            {
                CBox_ViewMode.Items.Clear();
                foreach (var value in Enum.GetValues(typeof(DataViewType))) {
                    CBox_ViewMode.Items.Add(value);
                }
                CBox_ViewMode.SelectedItem = type;
            }
            CBox_ViewMode.EndUpdate();
        }

        private void InitializeShiftBit(int bitnum)
        {
            CBox_ShiftBit.BeginUpdate();
            {
                CBox_ShiftBit.Items.Clear();
                foreach (var value in Enumerable.Range(0, 7)) {
                    CBox_ShiftBit.Items.Add(value);
                }
                CBox_ShiftBit.SelectedItem = bitnum;
            }
            CBox_ShiftBit.EndUpdate();
        }

        private void InitializeEndLineCode(string code)
        {
            TBox_EndLine.Text = code;
        }

        private void InitializeCharCodeType(CharCodeType type)
        {
            CBox_CharCode.BeginUpdate();
            {
                CBox_CharCode.Items.Clear();
                foreach (var value in Enum.GetValues(typeof(CharCodeType))) {
                    CBox_CharCode.Items.Add(value);
                }
                CBox_CharCode.SelectedItem = type;
            }
            CBox_CharCode.EndUpdate();
        }

        protected override void OnBackupProperty()
        {
            prop_ = Property as ViewPropertyImpl;

            prop_.DataView.Value = (DataViewType)CBox_ViewMode.SelectedItem;
            prop_.ShiftBit.Value = (int)CBox_ShiftBit.SelectedItem;
            prop_.EndLinePattern.Value = TBox_EndLine.Text;
            prop_.CharCode.Value = (CharCodeType)CBox_CharCode.SelectedItem;
            prop_.EchoBack.Value = ChkBox_EchoBack.Checked;
        }

        private void DrawBufferReset()
        {
            draw_buffer_ = new StringBuilder(2048);
            draw_data_type_ = 0;
        }

        private void DrawBufferPushBegin(PacketAttribute type)
        {
            /* データタイプが変化した場合は現在溜まっているバッファを出力 */
            if (draw_data_type_ != type) {
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
                    switch (prop_.DataView.Value) {
                        case DataViewType.Char:    color_fore = Color.Red;      break;
                        case DataViewType.HexText: color_fore = Color.Blue;     break;
                        case DataViewType.BinCode: color_fore = Color.Brown;    break;
                    }
                    break;
            }

            RTBox_Main.SelectionColor = color_fore;
            RTBox_Main.AppendText(draw_buffer_.ToString());

            DrawBufferReset();
        }


        private void LoadCurrentProperty()
        {
            
        }

        private Encoding LoadTextEncoder(CharCodeType type)
        {
            switch (type) {
                case CharCodeType.ShiftJIS:     return (Encoding.GetEncoding(932));
                case CharCodeType.UTF8:         return (Encoding.UTF8);
                default:                        return (Encoding.ASCII);
            }
        }

        private void UpdateEndLineView()
        {
            /* 改行パターン取得 */
            lf_code_ = HexTextEncoder.ToByteArray(TBox_EndLine.Text);

            /* 背景色設定 */
            if (TBox_EndLine.Text.Length == 0) {
                TBox_EndLine.BackColor = Color.White;
            } else {
                TBox_EndLine.BackColor = (lf_code_ != null) ? (Color.SkyBlue) : (Color.Pink);
            }
        }

        private void DrawMessagePacket(MessagePacketObject packet)
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

        private void DrawDataPacket(DataPacketObject packet)
        {
            if (   (!prop_.EchoBack.Value)
                && (packet.Direction != PacketDirection.Recv)
            ) {
                return;
            }

            /* 表示処理 */
            var draw_data = (byte)0;

            DrawBufferPushBegin(PacketAttribute.Data);
            foreach (var data_one in packet.GetData()) {
                /* 入力データをシフト処理 */
                draw_data = DataShift(data_one);

                /* 表示文字列を作成 */
                DrawBufferPush(DataToText(prop_.DataView.Value, draw_data));

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

        private string DataToText(DataViewType type, byte data)
        {
            switch (type) {
                case DataViewType.Char:
                    /* リッチテキストは何故か\r単体でも改行してしまうので\rをスペースに変換する */
                    return (CharToText(data).Replace('\r', ' '));

                case DataViewType.BinCode:
                    return (HexTextEncoder.BinCode[data] + ' ');

                case DataViewType.HexText:
                    return (HexTextEncoder.HexCode[data] + ' ');

                default:
                    return ("");
            }
        }

        private string CharToText(byte data)
        {
            switch (prop_.CharCode.Value) {
                case CharCodeType.ASCII:
                    return (DataToText_Ascii(data));
                case CharCodeType.ShiftJIS:
                    return (DataToText_ShiftJIS(data));
                case CharCodeType.UTF8:
                    return (DataToText_UTF8(data));
                default:
                    return ("");
            }
        }

        private string DataToText_Ascii(byte data)
        {
            return (encoder_.GetString(new byte[] { data }));
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
                var text = encoder_.GetString(char_cache_.ToArray());

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
                var result = encoder_.GetString(char_cache_.ToArray());

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
            encoder_ = LoadTextEncoder((CharCodeType)CBox_CharCode.SelectedItem);

            /* 選択状態をバックアップ */
            select_pos_start_ = RTBox_Main.SelectionStart;
            select_pos_length_ = RTBox_Main.SelectionLength;

            /* エディターの最後にキャレットを移動 */
            RTBox_Main.SelectionStart = RTBox_Main.TextLength;

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
                    DrawMessagePacket(packet as MessagePacketObject);
                    break;
                case PacketAttribute.Data:
                    DrawDataPacket(packet as DataPacketObject);
                    break;
            }
        }

        private void TBox_EndLine_TextChanged(object sender, EventArgs e)
        {
            UpdateEndLineView();
        }

        private void ChkBox_EchoBack_CheckedChanged(object sender, EventArgs e)
        {
            BackupProperty();
        }
    }
}
