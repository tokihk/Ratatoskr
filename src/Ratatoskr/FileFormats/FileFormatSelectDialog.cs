using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RtsCore.Framework.FileFormat;

namespace Ratatoskr.FileFormats
{
    internal partial class FileFormatSelectDialog : Form
    {
        private const int      ITEM_HEIGHT = 40;
        private const int      ITEM_MARGIN_ROUND = 2;
        private const int      ITEM_MARGIN_TEXT  = 5;

        private readonly Font  ITEM_NAME_FONT  = new Font("Meiryo", 9);
        private readonly Brush ITEM_NAME_COLOR = Brushes.Black;

        private readonly Font  ITEM_DETAIL_FONT  = new Font("Meiryo", 8, FontStyle.Italic);
        private readonly Brush ITEM_DETAIL_COLOR = Brushes.Gray;


        public FileFormatClass Format { get; private set; } = null;


        internal FileFormatSelectDialog()
        {
            InitializeComponent();
        }

        internal FileFormatSelectDialog(IEnumerable<FileFormatClass> formats) : this()
        {
            LBox_Main.ItemHeight = ITEM_HEIGHT;

            InitializeFormatList(formats);
        }

        private void InitializeFormatList(IEnumerable<FileFormatClass> formats)
        {
            LBox_Main.BeginUpdate();
            {
                LBox_Main.Items.Clear();
                LBox_Main.Items.AddRange(formats.ToArray());
            }
            LBox_Main.EndUpdate();
        }

        private void LBox_Main_DrawItem(object sender, DrawItemEventArgs e)
        {
            /* 背景描画 */
            e.DrawBackground();

            if (e.Index >= 0) {
                var item = LBox_Main.Items[e.Index] as FileFormatClass;
                var icon = item.Icon;
                var name = item.Name;
                var detail = item.Detail;

                var region_icon = new Rectangle();
                var region_name = new Rectangle();
                var region_detail = new Rectangle();

                region_icon.X = e.Bounds.X + ITEM_MARGIN_ROUND;
                region_icon.Y = e.Bounds.Y + ITEM_MARGIN_ROUND;
                region_icon.Width = e.Bounds.Height - (ITEM_MARGIN_ROUND * 2);
                region_icon.Height = region_icon.Width;

                region_name.X = region_icon.Right + ITEM_MARGIN_TEXT;
                region_name.Y = e.Bounds.Y + ITEM_MARGIN_ROUND;
                region_name.Width = e.Bounds.Right - region_icon.Right - ITEM_MARGIN_TEXT - ITEM_MARGIN_ROUND;
                region_name.Height = region_icon.Height / 2;

                region_detail.X = region_name.X;
                region_detail.Y = region_name.Bottom;
                region_detail.Width = region_name.Width;
                region_detail.Height = region_name.Height;

                /* アイコン */
                if (icon != null) {
                    e.Graphics.DrawImage(icon, region_icon);
                }

                /* 名称 */
                if (name != null) {
                    e.Graphics.DrawString(name, ITEM_NAME_FONT, ITEM_NAME_COLOR, region_name.Location);
                }

                /* 説明 */
                if (detail != null) {
                    e.Graphics.DrawString(detail, ITEM_DETAIL_FONT, ITEM_DETAIL_COLOR, region_detail.Location);
                }
            }
        }

        private void LBox_Main_DoubleClick(object sender, EventArgs e)
        {
            Format = LBox_Main.SelectedItem as FileFormatClass;

            DialogResult = DialogResult.OK;
        }
    }
}
