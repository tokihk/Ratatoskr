using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Scripts;
using Ratatoskr.Scripts.ScriptEngines;
using Ratatoskr.Config.Data.System;
using ScintillaNET;

namespace Ratatoskr.Forms.ScriptWindow
{
    internal class ScriptWindow_CodeEditor : SplitContainer
    {
        private static readonly IEnumerable<string> ACLIST_API = null;


        static ScriptWindow_CodeEditor()
        {
            var names_ignore = typeof(object).GetMembers().Select(obj => obj.Name);

            ACLIST_API = ScriptSandbox.GetApiNames().Where(name => !names_ignore.Contains(name)).OrderBy(name => name);
        }


        private CodeEditorEx   CEditor_Main;
        private RichTextBox    RTBox_Status;

        private string script_path_ = "";

        private ScriptController controller_ = null;
        private object           controller_sync_ = new object();

        private Font font_editor_ = new Font("MS Gothic", 10);

        public event EventHandler                 EditorStatusUpdated;
        public event ScriptMessageAppendedHandler ScriptMessageAppended;
        public event EventHandler                 ScriptStatusChanged;


        public ScriptWindow_CodeEditor()
        {
            InitializeComponent();

            Disposed += OnDisposed;

            EditorFont = new Font("MS Gothic", 10);

            LoadScriptFile();

            UpdateStatusPanel();
        }

        private void InitializeComponent()
        {
            CEditor_Main = new Forms.CodeEditorEx();
            CEditor_Main.Dock = DockStyle.Fill;
            CEditor_Main.BorderStyle = BorderStyle.None;
            CEditor_Main.Resize += CEditor_Main_Resize;
            CEditor_Main.UpdateUI += CEditor_Main_UpdateUI;
            CEditor_Main.CharAdded += CEditor_Main_CharAdded;

            RTBox_Status = new RichTextBox();
            RTBox_Status.Dock = DockStyle.Fill;
            RTBox_Status.ScrollBars = RichTextBoxScrollBars.Horizontal;
            RTBox_Status.BorderStyle = BorderStyle.None;
            RTBox_Status.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
            RTBox_Status.ReadOnly = true;

            Panel1.Controls.Add(CEditor_Main);
            Panel2.Controls.Add(RTBox_Status);

            FixedPanel = FixedPanel.Panel2;
        }

        private void OnDisposed(object sender, EventArgs e)
        {
            if (controller_ != null) {
                System.Diagnostics.Debug.WriteLine("ScriptManagerForm_CodeEditor.OnDisposed");
                controller_.MessageAppended -= Script_MessageAppended;
                controller_.CommentUpdated -= Script_CommentUpdated;
                controller_.StatusChanged -= Script_StatusChanged;
            }
        }

        public Font EditorFont
        {
            get { return (font_editor_); }
            set
            {
                if (value == null)return;

                font_editor_ = value;

                CEditor_Main.DefaultFontName = font_editor_.Name;
                CEditor_Main.DefaultFontSize = (int)font_editor_.Size;

                RTBox_Status.Font = font_editor_;

                UpdateStatusPanel();
            }
        }

        public Point CaretPosition
        {
            get { return (new Point(CEditor_Main.CurrentPosition, CEditor_Main.CurrentLine)); }
        }

        public string ScriptPath
        {
            get { return (script_path_); }
            set
            {
                script_path_ = value;
                LoadScriptFile();
            }
        }

        private string ScriptCode
        {
            get { return (CEditor_Main.Text); }
            set { CEditor_Main.Text = value;  }
        }

        public void LoadScriptFile()
        {
            var script_code = "";

            try {
                if (   (script_path_ != null)
                    && (File.Exists(script_path_))
                ) {
                    script_code = File.ReadAllText(script_path_);
                }
            } catch { }

            ScriptCode = script_code;
        }

        public void SaveScriptFile()
        {
            if ((script_path_ != null) && (script_path_ != "")) {
                File.WriteAllText(script_path_, ScriptCode);
            }
        }

        public bool IsScriptBusy
        {
            get { return ((controller_ != null) ? (controller_.IsRunning) : (false)); }
        }

        public ScriptMessageData[] ScriptMessageList
        {
            get { return ((controller_ != null) ? (controller_.GetMessageList()) : (new ScriptMessageData[] { })); }
        }

        public void ScriptRun()
        {
            if (InvokeRequired) {
                Invoke((MethodInvoker)ScriptRun);
                return;
            }

            var code = ScriptCode;

            /* 現在のスクリプトをバックアップ */
            SaveScriptFile();

            /* スクリプト実行 */
            controller_ = ScriptManager.Register(script_path_, ScriptRunMode.OneShot);
            if (controller_ != null) {
                controller_.MessageAppended += Script_MessageAppended;
                controller_.CommentUpdated += Script_CommentUpdated;
                controller_.StatusChanged += Script_StatusChanged;
                controller_.RunAsync();
            }
        }

        public void ScriptStop()
        {
            if (InvokeRequired) {
                Invoke((MethodInvoker)ScriptStop);
                return;
            }

            controller_?.StopAsync();
        }

        private void UpdateStatusPanel()
        {
            RTBox_Status.Clear();

            if (controller_ == null)return;

            var comment_list = controller_.GetCommentList();

            if (comment_list == null)return;

            var line_no_begin = CEditor_Main.FirstVisibleLine;
            var line_no_end = line_no_begin + CEditor_Main.LinesOnScreen;

            for (var line_no = line_no_begin; line_no < line_no_end; line_no++) {
                if (line_no >= comment_list.Length)break;

                if (comment_list[line_no] != null) {
                    RTBox_Status.AppendText(comment_list[line_no].Message);
                }
                RTBox_Status.AppendText(Environment.NewLine);
            }
        }

        private delegate void FormKeyActionHandler(ScriptWindowActionId id);
        public void FormKeyAction(ScriptWindowActionId id)
        {
            if (InvokeRequired) {
                Invoke((FormKeyActionHandler)FormKeyAction, id);
                return;
            }

            switch (id) {
                case ScriptWindowActionId.ScriptRun:
                    ScriptRun();
                    break;

                case ScriptWindowActionId.ScriptStop:
                    ScriptStop();
                    break;
            }
        }

        private void CEditor_Main_Resize(object sender, EventArgs e)
        {
            UpdateStatusPanel();
        }

        private void CEditor_Main_UpdateUI(object sender, UpdateUIEventArgs e)
        {
            if (e.Change == UpdateChange.VScroll) {
                UpdateStatusPanel();
            }

            EditorStatusUpdated?.Invoke(this, EventArgs.Empty);
        }

        private void CEditor_Main_CharAdded(object sender, CharAddedEventArgs e)
        {
            /* 入力中ワード取得 */
            var input_word_data = CEditor_Main.GetWordFromPosition(CEditor_Main.CurrentPosition);

            if (input_word_data.Length == 0)return;

            /* 入力中ワードを含むAPIを抽出 */
            var aclist = ACLIST_API.Where(name => name.Contains(input_word_data));

            CEditor_Main.AutoCShow(input_word_data.Length, string.Join(" ", aclist));
        }

        private void Script_MessageAppended(object sender, ScriptMessageData msg)
        {
            if (InvokeRequired) {
                BeginInvoke((ScriptMessageAppendedHandler)Script_MessageAppended, sender, msg);
                return;
            }

            if (IsDisposed)return;

            ScriptMessageAppended?.Invoke(this, msg);
        }

        private void Script_CommentUpdated(object sender, int line_no, ScriptMessageData msg)
        {
            if (InvokeRequired) {
                BeginInvoke((ScriptCommentUpdatedHandler)Script_CommentUpdated, sender, line_no, msg);
                return;
            }

            if (IsDisposed)return;

            UpdateStatusPanel();
        }

        private void Script_StatusChanged(object sender, EventArgs e)
        {
            if (InvokeRequired) {
                BeginInvoke((EventHandler)Script_StatusChanged, sender, e);
                return;
            }

            if (IsDisposed)return;

            CEditor_Main.ReadOnly = controller_.IsRunning;

            ScriptStatusChanged?.Invoke(this, e);
        }
    }
}
