using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RtsCore;
using RtsCore.Utility;

namespace Ratatoskr.Forms.DebugWindow
{
    internal partial class DebugWindow_Form : Form
    {
        private const int DRAW_INTERVAL = 100;

		[Flags]
		private enum DrawItemAttr
		{
			DateTime		= 1 << 1,
			DeltaTime		= 1 << 2,
			ThreadID		= 1 << 3,
		}


		private static DebugWindow_Form form_ = null;

        private System.Windows.Forms.Timer draw_timer_ = new System.Windows.Forms.Timer();
        private Stopwatch draw_time_ = new Stopwatch();

		private List<SystemMessageInfo>	msg_list_all_ = new List<SystemMessageInfo>();

		private object						draw_msg_sync_ = new object();
        private Queue<SystemMessageInfo>	draw_msg_queue_ = new Queue<SystemMessageInfo>();
		private SystemMessageInfo			draw_msg_last_ = null;
		private bool						draw_msg_enable_ = true;

		private DrawItemAttr		draw_item_attr_ = (DrawItemAttr)(-1);
		private SystemMessageAttr	draw_msg_attr_ = (SystemMessageAttr)(-1);


        [Conditional("DEBUG")]
        public static void Startup()
        {
            form_ = new DebugWindow_Form();
            form_.Visible = true;
        }

        public DebugWindow_Form()
        {
            InitializeComponent();
			InitializeMenuBar();

			Kernel.DebugMessageSetup += Kernel_DebugMessageSetup;

            draw_timer_.Tick += OnDrawTimer;
            draw_timer_.Interval = DRAW_INTERVAL;
            draw_timer_.Start();

            Show();
        }

		private void InitializeMenuBar()
		{
			/* MenuBar - View */
			foreach (DrawItemAttr item_type in Enum.GetValues(typeof(DrawItemAttr))) {
				var menu = new ToolStripMenuItem();

				menu.Text = item_type.ToString();
				menu.Tag  = item_type;
				menu.Checked = draw_item_attr_.HasFlag(item_type);
				menu.Click += MenuBar_View_Item_Click;

				MenuBar_View.DropDownItems.Add(menu);
			}

			/* MenuBar - Filter */
			foreach (SystemMessageAttr attr in Enum.GetValues(typeof(SystemMessageAttr))) {
				var menu = new ToolStripMenuItem();

				menu.Text = attr.ToString();
				menu.Tag  = attr;
				menu.Checked = draw_msg_attr_.HasFlag(attr);
				menu.Click += MenuBar_Filter_Item_Click;

				MenuBar_Filter.DropDownItems.Add(menu);
			}

			/* MenuBar - Debug */
			MenuBar_Debug_MessageWatch.Checked = draw_msg_enable_;
		}

		private void UpdateDrawItemType()
		{
			draw_item_attr_ = 0;

			foreach (ToolStripMenuItem item in MenuBar_View.DropDownItems) {
				if (item.Checked) {
					draw_item_attr_ |= (DrawItemAttr)item.Tag;
				}
			}
		}

		private void UpdateDrawMessageAttribute()
		{
			draw_msg_attr_ = 0;

			foreach (ToolStripMenuItem item in MenuBar_View.DropDownItems) {
				if (item.Checked) {
					draw_msg_attr_ |= (SystemMessageAttr)item.Tag;
				}
			}
		}

		private string BuildMessage(SystemMessageInfo minfo, SystemMessageInfo minfo_prev, DrawItemAttr draw_item_attr = (DrawItemAttr)(-1))
		{
			var str = new StringBuilder();

			if (minfo_prev == null) {
				minfo_prev = minfo;
			}

			/* DateTime - Local */
			if (draw_item_attr.HasFlag(DrawItemAttr.DateTime)) {
				str.AppendFormat("{0} ", minfo.DateTime.ToLocalTime().ToString("yyyy-MM-dd hh:mm:ss.ffffff"));
			}

			/* DeltaTime */
			if (draw_item_attr.HasFlag(DrawItemAttr.DeltaTime)) {
				str.AppendFormat("{{{0,10}}} ", (minfo.TickTime - minfo_prev.TickTime));
			}

			/* Thread ID */
			if (draw_item_attr.HasFlag(DrawItemAttr.ThreadID)) {
				str.AppendFormat("[{0}] ", minfo.ThreadID);
				str.Append(" ");
			}

			/* Message */
			str.Append(minfo.Message);

			return (str.ToString());
		}

		private void OnDrawTimer(object sender, EventArgs e)
        {
            var msg = (SystemMessageInfo)null;

            draw_time_.Restart();

            while ((draw_msg_enable_) && ((msg = PopMessage()) != null)) {
				msg_list_all_.Add(msg);

				if (draw_msg_attr_.HasFlag(msg.Attr)) {
					TBox_Message.AppendText(BuildMessage(msg, draw_msg_last_, draw_item_attr_) + Environment.NewLine);
					draw_msg_last_ = msg;
				}

                if (draw_time_.ElapsedMilliseconds >= (DRAW_INTERVAL / 2))break;
            }
        }

		private void PushMessage(SystemMessageInfo mi)
		{
            lock (draw_msg_sync_) {
                draw_msg_queue_.Enqueue(mi);
            }
		}

        private SystemMessageInfo PopMessage()
        {
            if (draw_msg_queue_.Count == 0)return (null);

            lock (draw_msg_sync_) {
                return (draw_msg_queue_.Dequeue());
            }
        }

        private void ClearMessage()
        {
            if (InvokeRequired) {
                Invoke((MethodInvoker)ClearMessage);
                return;
            }

            TBox_Message.Clear();

            lock (draw_msg_sync_) {
				msg_list_all_ = new List<SystemMessageInfo>();
                draw_msg_queue_ = new Queue<SystemMessageInfo>();
				draw_msg_last_ = null;
            }
        }

		private void RedrawMessage()
		{
            if (InvokeRequired) {
                Invoke((MethodInvoker)RedrawMessage);
                return;
            }

			TBox_Message.Clear();

			lock (draw_msg_sync_) {
				/* 表示待ちのログをメッセージリストに結合させる */
				while (draw_msg_queue_.Count > 0) {
					msg_list_all_.Add(draw_msg_queue_.Dequeue());
				}

				draw_msg_queue_ = new Queue<SystemMessageInfo>();
				draw_msg_last_ = null;

				msg_list_all_.ForEach(minfo => draw_msg_queue_.Enqueue(minfo));
			}
		}

        public void Kernel_DebugMessageSetup(SystemMessageInfo mi)
        {
//            System.Diagnostics.Debug.WriteLine(BuildMessage(mi, null));
            PushMessage(mi);
        }

        private void DebugForm_VisibleChanged(object sender, EventArgs e)
        {
            if (!Visible) {
                ClearMessage();
            }
        }

        private void DebugForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void MenuBar_Edit_ScreenClear_Click(object sender, EventArgs e)
        {
            ClearMessage();
        }

		private void MenuBar_View_Item_Click(object sender, EventArgs e)
		{
			if (sender is ToolStripMenuItem menu) {
				menu.Checked = !menu.Checked;

				UpdateDrawItemType();
				RedrawMessage();
			}
		}

		private void MenuBar_Filter_Item_Click(object sender, EventArgs e)
		{
			if (sender is ToolStripMenuItem menu) {
				menu.Checked = !menu.Checked;

				UpdateDrawMessageAttribute();
				RedrawMessage();
			}
		}

		private void MenuBar_Debug_MessageWatch_Click(object sender, EventArgs e)
		{
			draw_msg_enable_ = !draw_msg_enable_;
			MenuBar_Debug_MessageWatch.Checked = draw_msg_enable_;
		}
	}
}
