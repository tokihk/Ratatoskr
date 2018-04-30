using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Scripts;
using Ratatoskr.Scripts.ScriptEngines;
using ScintillaNET;

namespace Ratatoskr.Forms.ScriptManagerForm
{
    internal class ScriptManagerForm_CodeEditor : Controls.CodeEditorEx
    {
        private static readonly IEnumerable<string> ACLIST_API = null;


        static ScriptManagerForm_CodeEditor()
        {
            var names_ignore = typeof(object).GetMembers().Select(obj => obj.Name);

            ACLIST_API = ScriptSandbox.GetApiNames().Where(name => !names_ignore.Contains(name)).OrderBy(name => name);
        }


        private string script_path_ = "";

        private ScriptController controller_ = null;
        private object           controller_sync_ = new object();
        private StringBuilder    controller_message_ = new StringBuilder();

        public event EventHandler                 ScriptMessageClear;
        public event ScriptMessageAppendedHandler ScriptMessageOutput;
        public event EventHandler                 ScriptStatusChanged;


        public ScriptManagerForm_CodeEditor()
        {
            Disposed += OnDisposed;

            LoadScriptFile();
        }

        private void OnDisposed(object sender, EventArgs e)
        {
            if (controller_ != null) {
                System.Diagnostics.Debug.WriteLine("ScriptManagerForm_CodeEditor.OnDisposed");
                controller_.MessageAppended -= Script_MessageOutput;
                controller_.StatusChanged -= Script_StatusChanged;
            }
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

            Text = script_code;
        }

        public void SaveScriptFile()
        {
            if ((script_path_ != null) && (script_path_ != "")) {
                File.WriteAllText(script_path_, Text);
            }
        }

        public bool IsScriptBusy
        {
            get { return ((controller_ != null) ? (controller_.IsRunning) : (false)); }
        }

        public string ScriptMessage
        {
            get { return (controller_message_.ToString()); }
        }

        public void ClearScriptMessage()
        {
            controller_message_ = new StringBuilder();

            ScriptMessageClear?.Invoke(this, EventArgs.Empty);
        }

        public void ScriptRun()
        {
            if (InvokeRequired) {
                Invoke((MethodInvoker)ScriptRun);
                return;
            }

            var code = Text;

            /* 現在のスクリプトをバックアップ */
            SaveScriptFile();

            /* コンソールログをクリア */
            ClearScriptMessage();

            /* スクリプト実行 */
            controller_ = ScriptManager.Register(script_path_, ScriptRunMode.OneShot);
            if (controller_ != null) {
                controller_.MessageAppended += Script_MessageOutput;
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

        protected override void OnCharAdded(CharAddedEventArgs e)
        {
            base.OnCharAdded(e);

            /* 入力中ワード取得 */
            var input_word_data = GetWordFromPosition(CurrentPosition);

            if (input_word_data.Length == 0)return;

            /* 入力中ワードを含むAPIを抽出 */
            var aclist = ACLIST_API.Where(name => name.Contains(input_word_data));

            AutoCShow(input_word_data.Length, string.Join(" ", aclist));
        }

        private void Script_MessageOutput(object sender, ScriptMessageData msg)
        {
            if (InvokeRequired) {
                Invoke((ScriptMessageAppendedHandler)Script_MessageOutput, sender, msg);
                return;
            }

            controller_message_.AppendLine(msg.Message);

            ScriptMessageOutput?.Invoke(this, msg);
        }

        private void Script_StatusChanged(object sender, EventArgs e)
        {
            if (InvokeRequired) {
                Invoke((EventHandler)Script_StatusChanged, sender, e);
                return;
            }

            if (IsDisposed)return;

            ReadOnly = controller_.IsRunning;

            ScriptStatusChanged?.Invoke(this, e);
        }
    }
}
