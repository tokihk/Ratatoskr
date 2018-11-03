using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RtsCore.Generic;
using RtsCore.Utility;

namespace Ratatoskr.Forms.Controls
{
    internal partial class BinEditBox : UserControl
    {
        public enum CharCodeType
        {
            ASCII,
            ShiftJIS,
            UTF8,
        }

        private enum FocusPanel
        {
            None,
            Hex,
            Text,
        }

        private const int FOCUS_DOT_IVAL = 500;


        private Brush fore_brush_;
        private Brush back_brush_;

        private Color label_color_ = Color.Gray;
        private Pen   label_pen_;
        private Brush label_brush_;

        private Color select_fore_color_ = Color.White;
        private Brush select_fore_brush_;
        private Color select_back_color_ = Color.LightBlue;
        private Brush select_back_brush_;

        private Color focus_fore_color_ = Color.White;
        private Brush focus_fore_brush_;
        private Color focus_back_color_ = Color.Black;
        private Brush focus_back_brush_;

        private Timer focus_timer_ = new Timer();

        private ContextMenuStrip menu_select_;

        private SkipList<byte> data_all_ = new SkipList<byte>();

        private int    draw_addr_ = 0;
        private byte[] draw_data_ = null;

        private int draw_line_max_ = 0;
        private int draw_line_offset_ = 0;
        private int draw_line_num_ = 0;

        private bool is_edit_ = false;
        private bool is_insert_ = false;
        private bool is_text_ = true;

        private FocusPanel focus_panel_ = FocusPanel.None;
        private bool focus_dot_ = false;

        private int  focus_addr_main_ = 0;
        private int  focus_addr_sub_ = 0;
        private bool focus_edit_ = false;

        private int  select_addr_top_ = -1;
        private int  select_addr_end_ = -1;
        private bool select_busy_ = false;

        private Size render_char_size_ = Size.Empty;

        private CharCodeType char_code_ = CharCodeType.ASCII;
        private Encoding     char_encoder_ = Encoding.ASCII;


        public BinEditBox()
        {
            InitializeComponent();

            focus_timer_.Interval = FOCUS_DOT_IVAL;
            focus_timer_.Tick += OnFocusTimer;

            menu_select_ = CreateMenu_Select();

            Panel_DataBin.MouseWheel += Panel_Data_MouseWheel;
            Panel_DataText.MouseWheel += Panel_Data_MouseWheel;

            DataClear();

            RegionUpdate();

            UpdateGraphicsObject();

            ViewUpdate();
        }

        public bool EditEnable
        {
            get { return (is_edit_); }
            set { is_edit_ = value;  }
        }

        public bool InsertEnable
        {
            get { return (is_insert_); }
            set { is_insert_ = value;  }
        }

        public bool TextViewEnable
        {
            get { return (is_text_); }
            set
            {
                is_text_ = value;
                RegionUpdate();
            }
        }

        private void OnFocusTimer(object sender, EventArgs e)
        {
            focus_dot_ = !focus_dot_;
            ViewUpdate();
        }

        private void HexEditBox_SizeChanged(object sender, EventArgs e)
        {
            ViewUpdate();
        }

        private void HexEditBox_FontChanged(object sender, EventArgs e)
        {
            UpdateGraphicsObject();
            ViewUpdate();
        }
        private void HexEditBox_DragEnter(object sender, DragEventArgs e)
        {
            if (!is_edit_)return;
            if (!is_insert_)return;

            if (e.Data.GetDataPresent(DataFormats.FileDrop)) {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void HexEditBox_DragDrop(object sender, DragEventArgs e)
        {
            if (!is_edit_)return;
            if (!is_insert_)return;

            if (e.Data.GetData(DataFormats.FileDrop) is string[] files) {
                if (files.Length == 0)return;

                /* --- ファイル読み込み --- */
                try {
                    var data = File.ReadAllBytes(files.First());

                    SetData(data);

                } catch (Exception) {
                }
            }
        }

        private void MenuAct_Copy_Hex(object sender, EventArgs e)
        {
            if (select_addr_top_ < 0)return;
            if (select_addr_end_ < 0)return;

            var data = DataPickUp(select_addr_top_, select_addr_end_ - select_addr_top_ + 1);

            if (data == null)return;
            if (data.Length == 0)return;
            
            Clipboard.SetData(DataFormats.Text, HexTextEncoder.ToHexText(data, " "));
        }

        private void MenuAct_Copy_Utf8(object sender, EventArgs e)
        {
            if (select_addr_top_ < 0)return;
            if (select_addr_end_ < 0)return;

            var data = DataPickUp(select_addr_top_, select_addr_end_ - select_addr_top_ + 1);

            if (data == null)return;
            if (data.Length == 0)return;
            
            Clipboard.SetData(DataFormats.UnicodeText, Encoding.UTF8.GetString(data));
        }

        private void MenuAct_Copy_Utf16(object sender, EventArgs e)
        {
            if (select_addr_top_ < 0)return;
            if (select_addr_end_ < 0)return;

            var data = DataPickUp(select_addr_top_, select_addr_end_ - select_addr_top_ + 1);

            if (data == null)return;
            if (data.Length == 0)return;

            Clipboard.SetData(DataFormats.UnicodeText, Encoding.Unicode.GetString(data));
        }

        private void Panel_Address_Paint(object sender, PaintEventArgs e)
        {
            /* === ダブルバッファリング === */
            var graphics_p = BufferedGraphicsManager.Current.Allocate(e.Graphics, e.ClipRectangle);
            var graphics = graphics_p.Graphics;

            /* --- 背景初期化 --- */
            graphics.FillRectangle(Brushes.White, e.ClipRectangle);

            RenderCharSizeUpdate(graphics);

            graphics.DrawString("ADDRESS", Font, label_brush_, Point.Empty);
            graphics.DrawLine(
                label_pen_,
                e.ClipRectangle.Left,
                e.ClipRectangle.Bottom - 1,
                e.ClipRectangle.Right,
                e.ClipRectangle.Bottom - 1);

            graphics_p.Render(e.Graphics);
            graphics_p.Dispose();
        }

        private void Panel_Address_Page_Paint(object sender, PaintEventArgs e)
        {
            /* === ダブルバッファリング === */
            var graphics_p = BufferedGraphicsManager.Current.Allocate(e.Graphics, e.ClipRectangle);
            var graphics = graphics_p.Graphics;

            /* --- 背景初期化 --- */
            graphics.FillRectangle(back_brush_, e.ClipRectangle);

            /* --- ページアドレス描画 --- */
            var line_height = render_char_size_.Height;
            var draw_addr = (long)draw_line_offset_ * 16;

            for (var line_no = 0; line_no < draw_line_num_; line_no++) {
                if (draw_addr >= (long)(data_all_.Count + 1))break;

                graphics.DrawString(String.Format("{0:X8}", draw_addr), Font, label_brush_, 0, line_no * line_height);
                draw_addr += 16;
            }

            graphics_p.Render(e.Graphics);
            graphics_p.Dispose();
        }

        private void Panel_AddressOffset_Bin_Paint(object sender, PaintEventArgs e)
        {
            /* === ダブルバッファリング === */
            var graphics_p = BufferedGraphicsManager.Current.Allocate(e.Graphics, e.ClipRectangle);
            var graphics = graphics_p.Graphics;

            /* --- 文字サイズ情報を更新 --- */
            RenderCharSizeUpdate(graphics);

            /* --- 背景初期化 --- */
            graphics.FillRectangle(back_brush_, e.ClipRectangle);

            /* --- 描画 - アドレスオフセット値 --- */
            for (var offset = 0; offset < 16; offset++) {
                graphics.DrawString(
                    "+" + offset.ToString("X"),
                    Font,
                    label_brush_,
                    offset * render_char_size_.Width * 3,
                    0);
            }
            
            /* --- 描画 - 境界線 --- */
            graphics.DrawLine(
                label_pen_,
                e.ClipRectangle.Left,
                e.ClipRectangle.Bottom - 1,
                e.ClipRectangle.Right,
                e.ClipRectangle.Bottom - 1);

            graphics_p.Render(e.Graphics);
            graphics_p.Dispose();
        }

        private void Panel_DataBin_Paint(object sender, PaintEventArgs e)
        {
            /* === ダブルバッファリング === */
            var graphics_p = BufferedGraphicsManager.Current.Allocate(e.Graphics, e.ClipRectangle);
            var graphics = graphics_p.Graphics;

            /* --- 文字サイズ情報を更新 --- */
            RenderCharSizeUpdate(graphics);

            /* --- 背景初期化 --- */
            graphics.FillRectangle(Brushes.White, e.ClipRectangle);

            /* --- データ描画 --- */
            if (   (draw_data_ != null)     // 表示データがあるかどうか
                && (draw_line_num_ > 0)     // 表示行数があるかどうか
            ) {
                var brush_fore = (Brush)null;
                var brush_back = (Brush)null;

                var address = draw_addr_;
                var address_offset = 0;

                var draw_rect = new Rectangle();
                var draw_str = "";

                var focus_addr = GetFocusPos();

                foreach (var data in draw_data_) {
                    /* === 描画文字列取得 === */
                    draw_str = HexTextEncoder.HexCode[data];
                    if (address >= (int)data_all_.Count) {
                        draw_str = null;
                    }

                    /* === 描画座標取得 === */
                    draw_rect.X = ((address_offset % 16) * render_char_size_.Width) * 3;
                    draw_rect.Y = ((address_offset / 16) * render_char_size_.Height);

                    /* === 描画オブジェクト/サイズを決定 === */
                    if ((select_busy_) && (address >= select_addr_top_) && (address <= select_addr_end_)) {
                    /* --- 選択中 --- */
                        brush_fore = select_fore_brush_;
                        brush_back = select_back_brush_;
                        draw_rect.Width = render_char_size_.Width * 3;
                        draw_rect.Height = render_char_size_.Height;

                    } else if ((!select_busy_) && (focus_addr >= 0) && (address == focus_addr)) {
                    /* --- フォーカス --- */
                        brush_fore = focus_fore_brush_;
                        brush_back = focus_back_brush_;
                        if ((focus_panel_ == FocusPanel.Hex) && (focus_dot_)) {
                            brush_fore = fore_brush_;
                            brush_back = null;
                        }

                        draw_rect.Width = render_char_size_.Width * 2;
                        draw_rect.Height = render_char_size_.Height;
                    } else {
                    /* --- 通常 --- */
                        brush_fore = fore_brush_;
                        brush_back = null;
                        draw_rect.Width = render_char_size_.Width * 3;
                        draw_rect.Height = render_char_size_.Height;
                    }

                    /* === 背景描画 === */
                    if (brush_back != null) {
                        graphics.FillRectangle(brush_back, draw_rect);
                    }

                    /* === 文字描画 === */
                    if ((draw_str != null) && (brush_fore != null)) {
                        graphics.DrawString(
                            draw_str,
                            Font,
                            brush_fore,
                            draw_rect.Location
                            );
                    }

                    address++;
                    address_offset++;
                }
            }

            graphics_p.Render(e.Graphics);
            graphics_p.Dispose();
        }

        private void Panel_AddressOffset_Text_Paint(object sender, PaintEventArgs e)
        {
            if (!is_text_)return;

            /* === ダブルバッファリング === */
            var graphics_p = BufferedGraphicsManager.Current.Allocate(e.Graphics, e.ClipRectangle);
            var graphics = graphics_p.Graphics;

            /* --- 文字サイズ情報を更新 --- */
            RenderCharSizeUpdate(graphics);

            /* --- 背景初期化 --- */
            graphics.FillRectangle(back_brush_, e.ClipRectangle);

            /* --- 描画 - アドレスオフセット値 --- */
            for (var offset = 0; offset < 16; offset++) {
                graphics.DrawString(
                    offset.ToString("X"),
                    Font,
                    label_brush_,
                    offset * render_char_size_.Width,
                    0);
            }

            /* --- 描画 - 境界線 --- */
            graphics.DrawLine(
                label_pen_,
                e.ClipRectangle.Left,
                e.ClipRectangle.Bottom - 1,
                e.ClipRectangle.Right,
                e.ClipRectangle.Bottom - 1);

            graphics_p.Render(e.Graphics);
            graphics_p.Dispose();
        }

        private void Panel_DataText_Paint(object sender, PaintEventArgs e)
        {
            if (!is_text_)return;

            /* === ダブルバッファリング === */
            var graphics_p = BufferedGraphicsManager.Current.Allocate(e.Graphics, e.ClipRectangle);
            var graphics = graphics_p.Graphics;

            /* --- 文字サイズ情報を更新 --- */
            RenderCharSizeUpdate(graphics);

            /* --- 背景初期化 --- */
            graphics.FillRectangle(back_brush_, e.ClipRectangle);

            /* --- データ描画 --- */
            if (   (draw_data_ != null)     // 表示データがあるかどうか
                && (draw_line_num_ > 0)     // 表示行数があるかどうか
                && (char_encoder_ != null)  // エンコーダーが設定されているかどうか
            ) {
                var brush_fore = (Brush)null;
                var brush_back = (Brush)null;

                var address = draw_addr_;

                var draw_rect = new Rectangle();
                var draw_str = "";

                var draw_offset = 0;
                var load_offset = 0;
                var take_offset = 0;

                var focus_addr = GetFocusPos();

                while ((draw_str = TakeString(char_code_, draw_data_, ref load_offset, ref take_offset)) != null) {
                    while (draw_offset < load_offset) {
                        /* === 描画座標取得 === */
                        draw_rect.X = ((draw_offset % 16) * render_char_size_.Width);
                        draw_rect.Y = ((draw_offset / 16) * render_char_size_.Height);

                        /* === 描画オブジェクト/サイズを決定 === */
                        if ((select_busy_) && (address >= select_addr_top_) && (address <= select_addr_end_)) {
                        /* --- 選択中 --- */
                            brush_fore = select_fore_brush_;
                            brush_back = select_back_brush_;
                            draw_rect.Width = render_char_size_.Width;
                            draw_rect.Height = render_char_size_.Height;

                        } else if ((focus_addr >= 0) && (address == focus_addr)) {
                        /* --- フォーカス --- */
                            brush_fore = focus_fore_brush_;
                            brush_back = focus_back_brush_;
                            if ((focus_panel_ == FocusPanel.Text) && (focus_dot_)) {
                                brush_fore = fore_brush_;
                                brush_back = null;
                            }
                            draw_rect.Width = render_char_size_.Width;
                            draw_rect.Height = render_char_size_.Height;
                        } else {
                        /* --- 通常 --- */
                            brush_fore = fore_brush_;
                            brush_back = null;
                            draw_rect.Width = render_char_size_.Width;
                            draw_rect.Height = render_char_size_.Height;
                        }

                        /* === 背景描画 === */
                        if (brush_back != null) {
                            graphics.FillRectangle(brush_back, draw_rect);
                        }

                        /* === 文字描画 === */
                        if ((draw_offset == take_offset) && (brush_fore != null)) {
                            graphics.DrawString(
                                draw_str,
                                Font,
                                brush_fore,
                                draw_rect.Location
                                );
                        }

                        address++;
                        draw_offset++;
                    }
                }
            }

            graphics_p.Render(e.Graphics);
            graphics_p.Dispose();
        }

        private void Panel_Address_Page_Resize(object sender, EventArgs e)
        {
            /* --- 文字サイズ情報を更新 --- */
            RenderCharSizeUpdate();

            /* --- 描画行数を更新 --- */
            draw_line_num_ = Panel_Address_Page.ClientSize.Height / render_char_size_.Height;

            /* --- 再描画 --- */
            ViewUpdate();
        }

        private void VScrl_Data_ValueChanged(object sender, EventArgs e)
        {
            /* --- 描画位置を更新 --- */
            draw_line_offset_ = VScrl_Data.Value;

            /* --- 再描画 --- */
            ViewUpdate();
        }

        private void Panel_DataBin_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is Panel panel) {
                /* パネルにフォーカスを移動する
                 * パネルは明示的にフォーカスを指定しないと移動しないため */
                panel.Focus();

                if (e.Button == System.Windows.Forms.MouseButtons.Left) {
                    panel.Capture = true;

                    SetFocusPos(AddressPickUpAtPoint(e.X, e.Y, 3), true);

                    ViewUpdate();
                }
            }
        }

        private void Panel_DataBin_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender is Panel panel) {
                if (   (e.Button == System.Windows.Forms.MouseButtons.Left)
                    && (focus_addr_main_ >= 0)
                ) {
                    panel.Capture = false;

                    SetFocusPos(AddressPickUpAtPoint(e.X, e.Y, 3), false);

                    ViewUpdate();
                }
            }
        }

        private void Panel_DataBin_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Panel panel) {
                if (   (e.Button == System.Windows.Forms.MouseButtons.Left)
                    && (focus_addr_main_ >= 0)
                ) {
                    /* --- ウィンドウ外までマウスを引っ張った時に自動でスクロール --- */
                    if ((e.Y < panel.ClientRectangle.Top) && (VScrl_Data.Value > 0)) {
                        VScrl_Data.Value--;
                    } else if ((e.Y > panel.ClientRectangle.Bottom) && (VScrl_Data.Value < VScrl_Data.Maximum)) {
                        VScrl_Data.Value++;
                    }

                    SetFocusPos(AddressPickUpAtPoint(e.X, e.Y, 3), false);

                    ViewUpdate();
                }
            }
        }

        private void Panel_Data_MouseWheel(object sender, MouseEventArgs e)
        {
            if (sender is Panel panel) {
                if ((e.Delta > 0) && (VScrl_Data.Value > 0)) {
                    VScrl_Data.Value--;
                } else if ((e.Delta < 0) && (VScrl_Data.Value < VScrl_Data.Maximum)) {
                    VScrl_Data.Value++;
                }
            }
        }

        private void Panel_DataText_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is Panel panel) {
                /* パネルにフォーカスを移動する
                 * パネルは明示的にフォーカスを指定しないと移動しないため */
                panel.Focus();

                if (e.Button == System.Windows.Forms.MouseButtons.Left) {
                    panel.Capture = true;

                    SetFocusPos(AddressPickUpAtPoint(e.X, e.Y, 1), true);

                    ViewUpdate();
                }
            }
        }

        private void Panel_DataText_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender is Panel panel) {
                if (   (e.Button == System.Windows.Forms.MouseButtons.Left)
                    && (select_addr_top_ >= 0)
                ) {
                    panel.Capture = false;

                    SetFocusPos(AddressPickUpAtPoint(e.X, e.Y, 1), false);

                    ViewUpdate();
                }
            }
        }

        private void Panel_DataText_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Panel panel) {
                if (   (e.Button == System.Windows.Forms.MouseButtons.Left)
                    && (select_addr_top_ >= 0)
                ) {
                    /* --- ウィンドウ外までマウスを引っ張った時に自動でスクロール --- */
                    if ((e.Y < panel.ClientRectangle.Top) && (VScrl_Data.Value > 0)) {
                        VScrl_Data.Value--;
                    } else if ((e.Y > panel.ClientRectangle.Bottom) && (VScrl_Data.Value < VScrl_Data.Maximum)) {
                        VScrl_Data.Value++;
                    }

                    SetFocusPos(AddressPickUpAtPoint(e.X, e.Y, 1), false);

                    ViewUpdate();
                }
            }
        }

        private void Panel_DataBin_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if ((e.KeyCode >= Keys.A) && (e.KeyCode <= Keys.F)) {
            /* --- 英字入力 --- */
                DataEdit((byte)(e.KeyCode - Keys.A + 10), false);

            } else if ((e.KeyCode >= Keys.D0) && (e.KeyCode <= Keys.D9)) {
            /* --- 数字入力 --- */
                DataEdit((byte)(e.KeyCode - Keys.D0), false);

            } else if ((e.KeyCode >= Keys.NumPad0) && (e.KeyCode <= Keys.NumPad9)) {
            /* --- NumPadの数字入力 --- */
                DataEdit((byte)(e.KeyCode - Keys.NumPad0), false);
            }

            switch (e.KeyCode) {
                case Keys.Left:   MoveFocusPos(-1, true);   break;
                case Keys.Right:  MoveFocusPos(1, true);    break;
                case Keys.Up:     MoveFocusPos(-16, true);  break;
                case Keys.Down:   MoveFocusPos(16, true);   break;
            }

            switch (e.KeyCode) {
                case Keys.Back:   DataDelete(true);  break;
                case Keys.Delete: DataDelete(false); break;
            }

            ViewUpdate();
        }

        private void Panel_DataText_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if ((e.KeyCode >= Keys.A) && (e.KeyCode <= Keys.Z)) {
            /* --- 英字入力 --- */
                DataEdit((byte)(e.KeyCode - Keys.A + ((e.Shift) ? ('A') : ('a'))), true);

            } else if ((e.KeyCode >= Keys.D0) && (e.KeyCode <= Keys.D9)) {
            /* --- 数字入力 --- */
                DataEdit((byte)(e.KeyCode - Keys.D0 + '0'), true);

            } else if ((e.KeyCode >= Keys.NumPad0) && (e.KeyCode <= Keys.NumPad9)) {
            /* --- NumPadの数字入力 --- */
                DataEdit((byte)(e.KeyCode - Keys.NumPad0 + '0'), true);
            } else if (e.KeyCode == Keys.Space) {
            /* --- スペース入力 --- */
                DataEdit((byte)(e.KeyCode - Keys.NumPad0), false);
            }

            switch (e.KeyCode) {
                case Keys.Left:   MoveFocusPos(-1, true);   break;
                case Keys.Right:  MoveFocusPos(1, true);    break;
                case Keys.Up:     MoveFocusPos(-16, true);  break;
                case Keys.Down:   MoveFocusPos(16, true);   break;
            }

            switch (e.KeyCode) {
                case Keys.Back:   DataDelete(true);  break;
                case Keys.Delete: DataDelete(false); break;
            }

            ViewUpdate();
        }

        private ContextMenuStrip CreateMenu_Select()
        {
            var menu_root = new ContextMenuStrip();
            var menu = menu_root;

            menu.Items.Add("Copy (Hex string)", null, MenuAct_Copy_Hex);
            menu.Items.Add("Copy (UTF-8)", null, MenuAct_Copy_Utf8);
            menu.Items.Add("Copy (UTF-16)", null, MenuAct_Copy_Utf8);

            return (menu_root);
        }

        private void RenderCharSizeUpdate(Graphics graphics)
        {
            var sformat = new StringFormat();

            sformat.SetMeasurableCharacterRanges(new []{ new CharacterRange(0, 1) });

            var region = (Region[])null;
            var size = SizeF.Empty;

            region = graphics.MeasureCharacterRanges("W", Font, new RectangleF(0, 0, 100, 100), sformat);

            render_char_size_ = region[0].GetBounds(graphics).Size.ToSize();
            render_char_size_.Width += 1;
        }

        private void RenderCharSizeUpdate()
        {
            var graphics = CreateGraphics();

            RenderCharSizeUpdate(graphics);

            graphics.Dispose();
        }

        public void SetCharCode(CharCodeType type)
        {
            char_code_ = type;

            switch (type) {
                case CharCodeType.ASCII:    char_encoder_ = Encoding.ASCII;             break;
                case CharCodeType.ShiftJIS: char_encoder_ = Encoding.GetEncoding(932);  break;
                case CharCodeType.UTF8:     char_encoder_ = Encoding.UTF8;              break;
                default:                    char_encoder_ = null;                       break;
            }

            ViewUpdate();
        }

        private bool IsTopCharData(CharCodeType type, byte data, ref int byte_num)
        {
            var result = false;

            switch (char_code_) {
                case CharCodeType.ASCII:
                {
                    byte_num = 1;
                    result = true;
                }
                    break;

                case CharCodeType.ShiftJIS:
                {
                    if (   ((data >= 0x81) && (data <= 0x9F))
                        || ((data >= 0xE0) && (data <= 0xFC))
                    ) {
                        /* マルチバイトの1文字目 */
                        byte_num = 2;
                    } else {
                        /* 1バイト文字 */
                        byte_num = 1;
                    }
                    result = true;
                }
                    break;

                case CharCodeType.UTF8:
                {
                         if ((data & 0xFE) == 0xFC) { byte_num = 6; result = true; }
                    else if ((data & 0xFC) == 0xF8) { byte_num = 5; result = true; }
                    else if ((data & 0xF8) == 0xF0) { byte_num = 4; result = true; }
                    else if ((data & 0xF0) == 0xE0) { byte_num = 3; result = true; }
                    else if ((data & 0xE0) == 0xC0) { byte_num = 2; result = true; }
                    else if ((data & 0x80) == 0x00) { byte_num = 1; result = true; }
                }
                    break;
            }

            return (result);
        }

        private string TakeString(CharCodeType type, byte[] data, ref int offset, ref int take_pos)
        {
            var byte_num = 0;

            /* 文字の最初のデータを検索 */
            while (offset < data.Length) {
                if (IsTopCharData(type, data[offset], ref byte_num)) {
                    break;
                }
                offset++;
            }

            /* 有効なデータが見つからなかった場合は終了 */
            if (offset == data.Length)return (null);

            /* 有効な文字が見つかってもデータが足りなければ終了 */
            if ((offset + byte_num) > data.Length) {
                offset = data.Length;
                return (null);
            }

            /* 文字列に変換 */
            var result = char_encoder_.GetString(data.Skip(offset).Take(byte_num).ToArray());

            take_pos = offset;
            offset += byte_num;

            return (result);
        }

        public void DataClear()
        {
            data_all_ = new SkipList<byte>();

            draw_line_offset_ = 0;

            draw_addr_ = 0;
            draw_data_ = null;

            SetFocusPos(0, true);

            ViewUpdate();
        }

        public IEnumerable<byte> GetData()
        {
            return (data_all_);
        }

        public void SetData(IEnumerable<byte> data)
        {
            data_all_ = new SkipList<byte>(data);

            /* --- 再描画 --- */
            ViewUpdate();
        }

        public void AddData(IEnumerable<byte> data)
        {
            data_all_.AddRange(data);

            /* --- 再描画 --- */
            ViewUpdate();
        }

        public void InsertData(int index, IEnumerable<byte> data)
        {
            data_all_.InsertRange(index, data);

            /* --- 再描画 --- */
            ViewUpdate();
        }

        private void OverrideData(int index, IEnumerable<byte> data)
        {
            data_all_.SetAt((ulong)index, data);

            /* --- 再描画 --- */
            ViewUpdate();
        }

        private byte[] DataPickUp(int offset, int size)
        {
            var data = new List<byte>(data_all_.Skip(offset).Take(size).ToArray());

            /* --- 挿入が許可されている場合は最後のデータ以降にもフォーカスを移動できるようにする --- */
            if (   (is_edit_)
                && (is_insert_)
                && ((offset + size) >= (int)data_all_.Count)
            ) {
                data.Add(0x00);
            }

            return (data.ToArray());
        }

        private int AddressPickUpAtPoint(int x, int y, int data_width)
        {
            x = Math.Max(0, x);
            x = Math.Min(15, x / (data_width * render_char_size_.Width));

            y = Math.Max(0, y);
            y = (y / render_char_size_.Height + draw_line_offset_) * 16;

            var addr_max = ((is_edit_) && (is_insert_)) ? (data_all_.Count) : (data_all_.Count - 1);

            return (Math.Min(x + y, (int)addr_max));
        }

        private void RegionUpdate()
        {
            RenderCharSizeUpdate();

            TLPanel_Main.RowStyles[0].Height = render_char_size_.Height + 5;
            TLPanel_Main.ColumnStyles[0].Width = render_char_size_.Width * 8 + 1;
            TLPanel_Main.ColumnStyles[1].Width = render_char_size_.Width * 3 * 16 + 1;

            if (is_text_) {
                TLPanel_Main.ColumnStyles[2].SizeType = SizeType.Percent;
                TLPanel_Main.ColumnStyles[2].Width = 100;
            } else {
                TLPanel_Main.ColumnStyles[2].SizeType = SizeType.Absolute;
                TLPanel_Main.ColumnStyles[2].Width = 0;
            }
        }

        private void ViewUpdate()
        {
            draw_line_max_ = (int)((data_all_.Count + 1) / 16) + 1;
            draw_line_offset_ = Math.Min(draw_line_max_ - 1, draw_line_offset_);

            draw_addr_ = draw_line_offset_ * 16;
            draw_data_ = DataPickUp(draw_addr_, draw_line_num_ * 16);

            VScrl_Data.Maximum = draw_line_max_ + draw_line_num_;
            VScrl_Data.Value = Math.Min(draw_line_offset_, VScrl_Data.Maximum);
            VScrl_Data.Enabled = (draw_line_max_ > draw_line_num_);

            Panel_Address.Invalidate();
            Panel_Address_Page.Invalidate();
            Panel_AddressOffset_Bin.Invalidate();
            Panel_AddressOffset_Text.Invalidate();
            Panel_DataBin.Invalidate();
            Panel_DataText.Invalidate();

            MenuUpdate();
        }

        private void MenuUpdate()
        {
            if (   (select_addr_top_ >= 0)
                && (select_addr_end_ >= 0)
            ) {
            /* --- データが選択されているとき --- */
                Panel_DataBin.ContextMenuStrip = menu_select_;
                Panel_DataText.ContextMenuStrip = menu_select_;
            } else {
            /* --- データが選択されていないとき --- */
                Panel_DataBin.ContextMenuStrip = null;
                Panel_DataText.ContextMenuStrip = null;
            }
        }

        void FocusUpdate()
        {
            if (Panel_DataBin.Focused) {
                focus_panel_ = FocusPanel.Hex;
            } else if (Panel_DataText.Focused) {
                focus_panel_ = FocusPanel.Text;
            } else {
                focus_panel_ = FocusPanel.None;
            }

            if (focus_panel_ != FocusPanel.None) {
                focus_timer_.Start();
                focus_dot_ = false;

            } else {
                focus_timer_.Stop();
            }
        }

        private int GetFocusPos()
        {
            return (focus_addr_main_);
        }

        private void SetFocusPos(int index, bool reset)
        {
            var focus_max = (int)(((is_edit_) && (is_insert_)) ? (data_all_.Count) : (data_all_.Count - 1));
            var select_max = (int)(((is_edit_) && (is_insert_) && (reset)) ? (data_all_.Count) : (data_all_.Count - 1));

            focus_addr_main_ = Math.Min(index, focus_max);
            focus_addr_main_ = Math.Max(focus_addr_main_, 0);
            focus_edit_ = false;

            if (reset) {
                focus_addr_sub_ = focus_addr_main_;
                select_busy_ = false;
            }

            select_addr_top_ = Math.Min(focus_addr_main_, focus_addr_sub_);
            select_addr_top_ = Math.Min(select_addr_top_, select_max);
            select_addr_end_ = Math.Max(focus_addr_main_, focus_addr_sub_);
            select_addr_end_ = Math.Min(select_addr_end_, select_max);

            if ((!reset) && (!select_busy_)) {
                select_busy_ = (select_addr_top_ != select_addr_end_);
            }

            focus_dot_ = false;
        }

        private bool MoveFocusPos(int offset, bool reset)
        {
            var addr_old = GetFocusPos();

            SetFocusPos(GetFocusPos() + offset, reset);

            return (addr_old != GetFocusPos());
        }

        private void DataEdit(byte data, bool fulldata)
        {
            if (!is_edit_)return;

            /* === 4ビット編集ではなく8ビット編集時 === */
            if (fulldata) {
                focus_edit_ = false;
            }

            /* === 上書きのため、選択中のデータを削除 === */
            if (focus_addr_main_ != focus_addr_sub_) {
                DataDelete(false);
            }

            /* === 新規データを挿入 === */
            if ((is_insert_) && (!focus_edit_)) {
                data_all_.Insert(GetFocusPos(), 0x00);
            }

            /* === データ更新 === */
            var data_new = data_all_[(ulong)GetFocusPos()];

            if (fulldata) {
                data_new = data;
            } else {
                if (focus_edit_) {
                /* --- 後半4ビットを編集 --- */
                    data_new = (byte)((data_new & 0xF0) | (data & 0x0F));
                } else {
                /* --- 前半4ビットを編集 --- */
                    data_new = (byte)((data_new & 0x0F) | ((data & 0x0F) << 4));
                }
            }
            data_all_[(ulong)GetFocusPos()] = data_new;

            /* === フォーカス移動 === */
            if ((focus_edit_) || (fulldata)) {
                MoveFocusPos(1, true);
            } else {
                focus_edit_ = true;
            }
        }

        private void DataDelete(bool is_back)
        {
            if (!is_edit_)return;
            if (!is_insert_)return;

            var addr_top = Math.Min(focus_addr_main_, focus_addr_sub_);
            var addr_end = Math.Max(focus_addr_main_, focus_addr_sub_);

            /* === バックスペース処理かどうか === */
            if ((is_back) && (addr_top == addr_end)) {
                /* --- 移動できなければ削除処理は無し --- */
                if (!MoveFocusPos(-1, true))return;

                addr_top = Math.Min(focus_addr_main_, focus_addr_sub_);
                addr_end = Math.Max(focus_addr_main_, focus_addr_sub_);
            }

            /* === 削除 === */
            data_all_.RemoveRange((ulong)addr_top, (ulong)(addr_end - addr_top + 1));

            /* === フォーカスリセット === */
            SetFocusPos(addr_top, true);
        }

        private void UpdateGraphicsObject()
        {
            fore_brush_ = new SolidBrush(ForeColor);

            back_brush_ = new SolidBrush(BackColor);

            label_pen_ = new Pen(label_color_);
            label_brush_ = new SolidBrush(label_color_);

            select_fore_brush_ = new SolidBrush(select_fore_color_);
            select_back_brush_ = new SolidBrush(select_back_color_);

            focus_fore_brush_ = new SolidBrush(focus_fore_color_);
            focus_back_brush_ = new SolidBrush(focus_back_color_);
        }

        private void DeleteGraphicsObject()
        {
            if (fore_brush_ != null) {
                fore_brush_.Dispose();
                fore_brush_ = null;
            }

            if (back_brush_ != null) {
                back_brush_.Dispose();
                back_brush_ = null;
            }

            if (label_pen_ != null) {
                label_pen_.Dispose();
                label_pen_ = null;
            }

            if (label_brush_ != null) {
                label_brush_.Dispose();
                label_brush_ = null;
            }

            if (select_fore_brush_ != null) {
                select_fore_brush_.Dispose();
                select_fore_brush_ = null;
            }

            if (select_back_brush_ != null) {
                select_back_brush_.Dispose();
                select_back_brush_ = null;
            }

            if (focus_fore_brush_ != null) {
                focus_fore_brush_.Dispose();
                focus_fore_brush_ = null;
            }

            if (focus_back_brush_ != null) {
                focus_back_brush_.Dispose();
                focus_back_brush_ = null;
            }
        }

        private void OnFocusUpdated(object sender, EventArgs e)
        {
            FocusUpdate();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Refresh();
            base.OnPaintBackground(e);
        }
    }
}
