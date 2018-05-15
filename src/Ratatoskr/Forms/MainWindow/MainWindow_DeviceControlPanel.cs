using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.Forms.MainWindow
{
    public partial class MainWindow_DeviceControlPanel : Form
    {
        private Control control_ = null;


        public MainWindow_DeviceControlPanel()
        {
            InitializeComponent();
        }

        public Control ContentsControl
        {
            get { return (control_); }
            set {
                /* 既存のコントロールを全てクリア */
                Controls.Clear();

                /* 新しいコントロールを設定 */
                control_ = value;

                if (control_ != null) {
                    control_.Dock = DockStyle.Fill;
                    Controls.Add(control_);

                    /* サイズを設定 */
                    ClientSize = control_.Size;
                }
            }
        }
    }
}
