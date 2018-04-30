using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.Debugger
{
    public partial class DebugForm : Form
    {
        private const int DRAW_INTERVAL = 100;


        private sealed class MessageInfo
        {
            public DateTime DateTime { get; }
            public string   Message  { get; }

            public MessageInfo(DateTime date, string msg)
            {
                DateTime = date;
                Message = msg;
            }
        }


        private Timer     draw_timer_ = new Timer();
        private Stopwatch draw_time_ = new Stopwatch();

        private Queue<MessageInfo> msg_queue_ = new Queue<MessageInfo>();


        public DebugForm()
        {
            InitializeComponent();

            draw_timer_.Tick += OnDrawTimer;
            draw_timer_.Interval = DRAW_INTERVAL;
            draw_timer_.Start();

            Show();
        }

        private void OnDrawTimer(object sender, EventArgs e)
        {
            var msg = (MessageInfo)null;

            draw_time_.Restart();
            while ((msg = PopMessage()) != null) {
                TBox_Message.AppendText(
                    String.Format(
                        "{0} {1} {2}",
                        msg.DateTime.ToLocalTime().ToString("yyyy-MM-dd hh:mm:ss.fff"),
                        msg.Message,
                        Environment.NewLine));

                if (draw_time_.ElapsedMilliseconds >= (DRAW_INTERVAL / 2))break;
            }
        }

        private void PushMessage(MessageInfo msg)
        {
            lock (msg_queue_) {
                msg_queue_.Enqueue(msg);
            }
        }

        private MessageInfo PopMessage()
        {
            if (msg_queue_.Count == 0)return (null);

            lock (msg_queue_) {
                return (msg_queue_.Dequeue());
            }
        }

        private delegate void ClearMessageDelegate();
        private void ClearMessage()
        {
            if (InvokeRequired) {
                Invoke(new ClearMessageDelegate(ClearMessage));
                return;
            }

            lock (msg_queue_) {
                msg_queue_.Clear();
            }

            TBox_Message.Clear();
        }

        public void AddMessage(string text)
        {
            PushMessage(new MessageInfo(DateTime.Now, string.Copy(text)));

            System.Diagnostics.Debug.WriteLine(text);
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
    }
}
