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
    internal partial class DebugMonitorForm : Form
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

		private Dictionary<string, DebugEventInfo>	status_list_all_ = new Dictionary<string, DebugEventInfo>();
		private List<DebugEventInfo>				msg_list_all_ = new List<DebugEventInfo>();

		private object											draw_status_sync_ = new object();
        private Queue<KeyValuePair<string, DebugEventInfo>>		draw_status_queue_ = new Queue<KeyValuePair<string, DebugEventInfo>>();
		private bool											draw_status_enable_ = true;

		private object						draw_msg_sync_ = new object();
        private Queue<DebugEventInfo>		draw_msg_queue_ = new Queue<DebugEventInfo>();
		private bool						draw_msg_enable_ = true;
		private DebugEventInfo				draw_msg_last_ = null;

		private DrawItemAttr				draw_item_attr_ = (DrawItemAttr)(-1);

		private DebugEventSender			draw_msg_sender_ = DebugEventSender.Unknown;
		private DebugEventType				draw_msg_type_   = DebugEventType.NoCategory;


        public DebugMonitorForm()
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
			foreach (DebugEventSender sender in Enum.GetValues(typeof(DebugEventSender))) {
				var menu = new ToolStripMenuItem();

				menu.Text = sender.ToString();
				menu.Tag  = sender;
				menu.Checked = draw_msg_sender_.HasFlag(sender);
				menu.Click += MenuBar_Filter_Sender_Item_Click;

				MenuBar_Filter.DropDownItems.Add(menu);
			}

			MenuBar_Filter.DropDownItems.Add(new ToolStripSeparator());

			/* MenuBar - Filter - Type */
			foreach (DebugEventType type in Enum.GetValues(typeof(DebugEventType))) {
				var menu = new ToolStripMenuItem();

				menu.Text = type.ToString();
				menu.Tag  = type;
				menu.Checked = draw_msg_type_.HasFlag(type);
				menu.Click += MenuBar_Filter_Type_Item_Click;

				MenuBar_Filter.DropDownItems.Add(menu);
			}

			/* MenuBar - Debug */
			MenuBar_Debug_MessageWatch.Checked = draw_msg_enable_;
			MenuBar_Debug_StatusWatch.Checked = draw_status_enable_;
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
							case DebugEventSender sender:
								draw_msg_sender_ |= sender;
								break;
							case DebugEventType type:
								draw_msg_type_ |= type;
								break;
						}
					}
                }
			}
		}

		private void UpdateStatusValue()
		{
			var status_list = status_list_all_.ToList();

			status_list.Sort((a, b) => a.Key.CompareTo(b.Key));

			LView_Status.BeginUpdate();
			{
				foreach (var status in status_list) {
					var item = LView_Status.FindItemWithText(status.Key);

					if (item != null) {
						if (item.SubItems[1].Text != status.Value.Value) {
							item.SubItems[1].Text = status.Value.Value;
						}

					} else {
						item = new ListViewItem();
						item.Text = status.Key;
						item.SubItems.Add(status.Value.Value);
						LView_Status.Items.Add(item);
					}
				}
			}
			LView_Status.EndUpdate();
		}

		private string BuildMessage(DebugEventInfo minfo, DebugEventInfo minfo_prev, DrawItemAttr draw_item_attr = (DrawItemAttr)(-1))
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
			str.Append(minfo.Value);

			return (str.ToString());
		}

		private void DrawStatusProc()
		{
			var draw_timer = new System.Diagnostics.Stopwatch();
            var status = new KeyValuePair<string, DebugEventInfo>();

            draw_timer.Restart();

            while ((draw_status_enable_) && (PopStatus(ref status))) {
				status_list_all_[status.Key] = status.Value;

                if (draw_timer.ElapsedMilliseconds >= (DRAW_INTERVAL / 2))break;
            }

			UpdateStatusValue();
		}

		private void DrawMessageProc()
		{
			var draw_timer = new System.Diagnostics.Stopwatch();
            DebugEventInfo msg;

            draw_timer.Restart();

            while ((draw_msg_enable_) && ((msg = PopMessage()) != null)) {
				msg_list_all_.Add(msg);

				if (((draw_msg_sender_ & msg.Sender) != 0) && ((draw_msg_type_ & msg.Type) != 0)) {
					TBox_Message.AppendText(BuildMessage(msg, draw_msg_last_, draw_item_attr_) + Environment.NewLine);
					draw_msg_last_ = msg;
				}

                if (draw_timer.ElapsedMilliseconds >= (DRAW_INTERVAL / 2))break;
            }
		}

		private void OnDrawTimer(object sender, EventArgs e)
        {
            DrawStatusProc();
			DrawMessageProc();
        }

		public void StatusOut(string name, DebugEventInfo ei)
		{
			lock (draw_msg_sync_) {
				draw_status_queue_.Enqueue(new KeyValuePair<string, DebugEventInfo>(name, ei));
			}
		}

        private bool PopStatus(ref KeyValuePair<string, DebugEventInfo> ei)
        {
			if (draw_status_queue_.Count == 0)return (false);

			var get_ok = false;

            lock (draw_status_sync_) {
				if (draw_status_queue_.Count > 0) {
					ei = draw_status_queue_.Dequeue();
					get_ok = true;
				}
            }

			return (get_ok);
        }

        private void ClearStatus()
        {
            if (InvokeRequired) {
                Invoke((MethodInvoker)ClearStatus);
                return;
            }

            LView_Status.Items.Clear();

            draw_status_queue_ = new Queue<KeyValuePair<string, DebugEventInfo>>();
        }

		public void MessageOut(DebugEventInfo mi)
		{
            lock (draw_msg_sync_) {
                draw_msg_queue_.Enqueue(mi);
            }
		}

        private DebugEventInfo PopMessage()
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
				msg_list_all_ = new List<DebugEventInfo>();
                draw_msg_queue_ = new Queue<DebugEventInfo>();
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
				draw_msg_queue_ = new Queue<DebugEventInfo>();
				draw_msg_last_ = null;

				/* 全メッセージを表示待ちにする */
				msg_list_all_.ForEach(minfo => draw_msg_queue_.Enqueue(minfo));

				/* 表示済みメッセージリストを初期化 */
				msg_list_all_ = new List<DebugEventInfo>();
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

		private void MenuBar_Debug_StatusWatch_Click(object sender, EventArgs e)
		{
			draw_status_enable_ = !draw_status_enable_;

			MenuBar_Debug_StatusWatch.Checked = draw_status_enable_;
		}
	}
}
