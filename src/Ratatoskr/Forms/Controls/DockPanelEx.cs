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
using WeifenLuo.WinFormsUI.Docking;

namespace Ratatoskr.Forms.Controls
{
    internal class DockPanelEx : DockPanel
    {
        public partial class DockContentEx : DockContent
        {
            public string    ContentName      { get; } = "";
            public uint      ContentGroup     { get; } = 0;

            public Control   ContentControl   { get; }

            public DockState DefaultDockState { get; }

            public DockContentEx(DockPanelEx panel, string name, string title, uint group, Icon icon, DockAreas areas, DockState state, bool can_close, Control control)
            {
                ContentName = name;
                ContentGroup = group;

                ContentControl = control;
                DefaultDockState = state;

                Text = title;
                Icon = icon;
                
                DockAreas = areas;
                CloseButton = can_close;
                CloseButtonVisible = can_close;
                HideOnClose = can_close;

                FormClosing += panel.OnDockContentClosing;
                FormClosed += panel.OnDockContentClosed;
                Activated += panel.OnDockContentActivated;

                ContentControl.Dock = DockStyle.Fill;

                Controls.Add(ContentControl);
            }

            protected override string GetPersistString()
            {
                return (ContentName);
            }
        }


        private List<DockContentEx> dock_contents_ = new List<DockContentEx>();
        private bool show_contents_ = false;


        public delegate void DockContentClosingHandler(object sender, Control control, FormClosingEventArgs e);
        public event DockContentClosingHandler DockContentClosing = delegate (object sender, Control control, FormClosingEventArgs e) { };

        public delegate void DockContentClosedHandler(object sender, Control control, FormClosedEventArgs e);
        public event DockContentClosedHandler DockContentClosed = delegate (object sender, Control control, FormClosedEventArgs e) { };

        public delegate void DockContentActivatedHandler(object sender, Control control,  EventArgs e);
        public event DockContentActivatedHandler DockContentActivated = delegate (object sender, Control control, EventArgs e) { };


        public DockPanelEx()
        {
        }

        public void ShowContents(string layout_file_path = null)
        {
            show_contents_ = true;

            if ((layout_file_path != null) && (File.Exists(layout_file_path))) {
                LoadFromXml(layout_file_path, GetDockContentFromPersistString);
            }

            /* 設定ファイルから復元できなかったものはデフォルト値で初期化 */
            foreach (var content in dock_contents_) {
                if (content.DockState == DockState.Unknown) {
                    content.Show(this, content.DefaultDockState);
                }
            }
        }

        private IDockContent GetDockContentFromPersistString(string persistString)
        {
            return (dock_contents_.Find(item => item.ContentName == persistString));
        }

        public void ClearDockContents()
        {
            foreach (var content in dock_contents_) {
                Controls.Remove(content);
            }

            dock_contents_.Clear();
        }

        public void RemoveDockContent(DockContentEx content)
        {
            if (content == null)return;

            Controls.Remove(content);

            dock_contents_.Remove(content);
        }

        public void RemoveDockContent(Control control)
        {
            RemoveDockContent(dock_contents_.Find(item => item.ContentControl == control));
        }

        public void RemoveDockContents(uint group)
        {
            foreach (var content in dock_contents_.Where(content => content.ContentGroup == group)) {
                RemoveDockContent(content);
            }
        }

        public void AddDockContent(string name, string title, uint group, Icon icon, DockAreas areas, DockState state, bool can_close, Control control)
        {
            var content = new DockContentEx(this, name, title, group, icon, areas, state, can_close, control);

            dock_contents_.Add(content);

            if (show_contents_) {
                content.Show(this, state);
            }
        }

        private void AddDockContent(string name, string title, uint group, DockAreas areas, DockState state, bool can_close, Control control)
        {
            AddDockContent(name, title, group, null, areas, state, can_close, control);
        }

        public IEnumerable<DockContentEx> GetDockContents()
        {
            return (dock_contents_.ToArray());
        }

        public IEnumerable<Control> GetDocumentControls()
        {
            return (Documents.Select(content => (content as DockContentEx).ContentControl));
        }

        private DockContentEx FindDocument(Control control)
        {
            foreach (DockContentEx content in Documents) {
                if (content.ContentControl == control)return (content);
            }

            return (null);
        }

        public Control GetActiveDocumentControl()
        {
            var content = ActiveDocument as DockContentEx;

            if (content == null)return (null);

            return (content.ContentControl);
        }

        public void SetActiveDocumentControl(Control control)
        {
            var content = FindDocument(control);

            if (content == null)return;

            content.Activate();
        }

        public IEnumerable<Control> GetDockContentControls()
        {
            return (dock_contents_.Select(content => content.ContentControl));
        }

        public DockState GetDockContentState(Control control)
        {
            return (Panes.First(pane => pane.Controls[0] == control).DockState);
        }

        private void OnDockContentClosing(object sender, FormClosingEventArgs e)
        {
            var content = sender as DockContentEx;

            if (content == null)return;

            var control = content.ContentControl;

            if (control != null) {
                OnDockContentClosing(control, e);
            }
        }

        private void OnDockContentClosed(object sender, FormClosedEventArgs e)
        {
            var content = sender as DockContentEx;

            if (content == null)return;

            var control = content.ContentControl;

            if (control != null) {
                OnDockContentClosed(control, e);
            }
        }

        private void OnDockContentActivated(object sender, EventArgs e)
        {
            var content = sender as DockContentEx;

            if (content == null)return;

            var control = content.ContentControl;

            if (control != null) {
                OnDockContentActivated(control, e);
            }
        }

        protected virtual void OnDockContentClosing(Control control, FormClosingEventArgs e)
        {
            DockContentClosing(this, control, e);
        }

        protected virtual void OnDockContentClosed(Control control, FormClosedEventArgs e)
        {
            DockContentClosed(this, control, e);
        }

        protected virtual void OnDockContentActivated(Control control, EventArgs e)
        {
            DockContentActivated(this, control, e);
        }
    }
}
