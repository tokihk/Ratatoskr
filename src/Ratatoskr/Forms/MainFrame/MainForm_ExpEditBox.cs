using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.Forms.MainFrame
{
    internal class MainForm_ExpEditBox : ToolStripComboBox
    {
        public MainForm_ExpEditBox()
        {

        }

#if false
        public override Size GetPreferredSize(Size constrainingSize)
        {
            var owner_width = Owner.DisplayRectangle.Width;
            var item_offset = Owner.Margin.Horizontal;

            if (Owner.OverflowButton.Visible) {
                owner_width -= Owner.OverflowButton.Width + Owner.OverflowButton.Margin.Horizontal;
            }

            foreach (ToolStripItem item in Owner.Items) {
                item_offset += item.Margin.Horizontal + Owner.Margin.Horizontal;
                if (item == this)break;
                item_offset += item.Width;
            }

            var size = base.GetPreferredSize(constrainingSize);

            size.Width = owner_width - item_offset - 5;

            return (size);
        }
#endif
    }
}
