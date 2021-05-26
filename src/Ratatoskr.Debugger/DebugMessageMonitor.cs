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

namespace Ratatoskr.Debugger
{
    internal partial class DebugMessageMonitor : Form
    {
        private const int DRAW_INTERVAL = 100;

		[Flags]
		private enum DrawItemAttr
		{
			DateTime		= 1 << 1,
			DeltaTime		= 1 << 2,
			ThreadID		= 1 << 3,
		}


        private System.Windows.Forms.Timer draw_timer_ = new System.Windows.Forms.Timer();
        private Stopwatch draw_time_ = new Stopwatch();

		private List<DebugMessageInfo>	msg_list_all_ = new List<DebugMessageInfo>();

		private object						draw_msg_sync_ = new object();
        private Queue<DebugMessageInfo>		draw_msg_queue_ = new Queue<DebugMessageInfo>();
		private DebugMessageInfo			draw_msg_last_ = null;
		private bool						draw_msg_enable_ = true;

		private DrawItemAttr		draw_item_attr_ = (DrawItemAttr)(-1);

		private DebugMessageSender	draw_msg_sender_ = DebugMessageSender.Unknown;
		private DebugMessageType	draw_msg_type_   = DebugMessageType.NoCategory;


        public DebugMessageMonitor()
        {
            InitializeComponent();
			InitializeMenuBar();

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

			/* MenuBar - Filter - Sender */
			foreach (DebugMessageSender sender in Enum.GetValues(typeof(DebugMessageSender))) {
				var menu = new ToolStripMenuItem();

				menu.Text = sender.ToString();
				menu.Tag  = sender;
				menu.Checked = draw_msg_sender_.HasFlag(sender);
				menu.Click += MenuBar_Filter_Sender_Item_Click;

				MenuBar_Filter.DropDownItems.Add(menu);
			}

			MenuBar_Filter.DropDownItems.Add(new ToolStripSeparator());

			/* MenuBar - Filter - Type */
			foreach (DebugMessageType type in Enum.GetValues(typeof(DebugMessageType))) {
				var menu = new ToolStripMenuItem();

				menu.Text = type.ToString();
				menu.Tag  = type;
				menu.Checked = draw_msg_type_.HasFlag(type);
				menu.Click += MenuBar_Filter_Type_Item_Click;

				MenuBar_Filter.DropDownItems.Add(menu);
			}

			/* MenuBar - Debug */
			MenuBar_Debug_MessageWatch.Checked = draw_msg_enable_;
		}

		private void UpdateDrawItemType()
		{
			draw_item_attr_ = 0;

			foreach (var item in MenuBar_View.DropDownItems) {
				if (item is ToolStripMenuItem menu) {
					if (menu.Checked) {
						switch (menu.Tag) {
							case DrawItemAttr attr:
								draw_item_attr_ |= attr;
								break;
						}
					}
                }
			}
		}

		private void UpdateDrawMessageFilter()
		{
			draw_msg_sender_ = 0;
			draw_msg_type_ = 0;

			foreach (var item in MenuBar_Filter.DropDownItems) {
				if (item is ToolStripMenuItem menu) {
					if (menu.Checked) {
						switch (menu.Tag) {
							case DebugMessageSender sender:
								draw_msg_sender_ |= sender;
								break;
							case DebugMessageType type:
								draw_msg_type_ |= type;
								break;
						}
					}
                }
			}
		}

		private string BuildMessage(DebugMessageInfo minfo, DebugMessageInfo minfo_prev, DrawItemAttr draw_item_attr = (DrawItemAttr)(-1))
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
            DebugMessageInfo msg;

            draw_time_.Restart();

            while ((draw_msg_enable_) && ((msg = PopMessage()) != null)) {
				msg_list_all_.Add(msg);

				if (((draw_msg_sender_ & msg.Sender) != 0) && ((draw_msg_type_ & msg.Type) != 0)) {
					TBox_Message.AppendText(BuildMessage(msg, draw_msg_last_, draw_item_attr_) + Environment.NewLine);
					draw_msg_last_ = msg;
				}

                if (draw_time_.ElapsedMilliseconds >= (DRAW_INTERVAL / 2))break;
            }
        }

		public void MessageOut(DebugMessageInfo mi)
		{
            lock (draw_msg_sync_) {
                draw_msg_queue_.Enqueue(mi);
            }
		}

        private DebugMessageInfo PopMessage()
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
				msg_list_all_ = new List<DebugMessageInfo>();
                draw_msg_queue_ = new Queue<DebugMessageInfo>();
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
				/* 表示待ちのログと表示済みメッセージリストを結合させる */
				while (draw_msg_queue_.Count > 0) {
					msg_list_all_.Add(draw_msg_queue_.Dequeue());
				}

				/* 表示待ちメッセージキューを初期化 */
				draw_msg_queue_ = new Queue<DebugMessageInfo>();
				draw_msg_last_ = null;

				/* 全メッセージを表示待ちにする */
				msg_list_all_.ForEach(minfo => draw_msg_queue_.Enqueue(minfo));

				/* 表示済みメッセージリストを初期化 */
				msg_list_all_ = new List<DebugMessageInfo>();
			}
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

		private void MenuBar_Filter_Sender_Item_Click(object sender, EventArgs e)
		{
			if (sender is ToolStripMenuItem menu) {
				menu.Checked = !menu.Checked;

				UpdateDrawMessageFilter();
				RedrawMessage();
			}
		}

		private void MenuBar_Filter_Type_Item_Click(object sender, EventArgs e)
		{
			if (sender is ToolStripMenuItem menu) {
				menu.Checked = !menu.Checked;

				UpdateDrawMessageFilter();
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
