using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Ratatoskr.Config;
using Ratatoskr.Forms;
using Ratatoskr.Scripts;
using Ratatoskr.Scripts.ScriptEngines;
using Ratatoskr.Config.Data.System;

namespace Ratatoskr.Forms.ScriptWindow
{
    internal partial class ScriptWindow_Control : UserControl
    {
        private FileExplorerEx           DC_FileExplorer;
        private ScriptWindow_Console     DC_Console;
        private ScriptWindow_ScriptList  DC_ScriptList;


        public ScriptWindow_Control()
        {
            InitializeComponent();
            InitializeMenuBar();

            Container_Main.ContentPanel.Controls.Add(DockPanel_Main);
        }

        private void InitializeMenuBar()
        {
            FormAction.SetupMenuAction<ScriptWindowActionId>(MenuBar_Root, OnMenuClick);
        }

        public void LoadConfig()
        {
            LoadMenuBarConfig();
            LoadFileExplorerConfig();
            LoadScriptListConfig();
            LoadConsoleConfig();

            LoadConfig_OpenText();

            UpdateCurrentEditorStatus();

			DockPanel_Main.ShowContents();
        }

        private void LoadMenuBarConfig()
        {
            FormAction.UpdateMenuKeyText(MenuBar_Root, ConfigManager.System.ScriptWindow.ShortcutKey.Value);
        }

        private void LoadFileExplorerConfig()
        {
            DC_FileExplorer = new FileExplorerEx();
            DC_FileExplorer.FileDoubleClick += OnFileDoubleClick;

            var root_path = GetScriptRootPath();

            /* スクリプトディレクトリが存在しない場合はテンプレートを出力 */
            if (!Directory.Exists(root_path)) {
                Directory.CreateDirectory(root_path);
                File.WriteAllText(GetScriptFilePath("SampleScript.gds"), Ratatoskr.Properties.Resources.Sample);
            }

            DC_FileExplorer.RootUrl = root_path;

			DockPanel_Main.AddDockContent(
                "DC_FileExplorer",
                "File Explorer",
                0,
                Icon.FromHandle(Ratatoskr.Resource.Images.memo_32x32.GetHicon()),
                DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockBottom | DockAreas.Float,
                DockState.DockLeft,
                false,
                DC_FileExplorer);
        }

        private void LoadConsoleConfig()
        {
            DC_Console = new ScriptWindow_Console();

			DockPanel_Main.AddDockContent(
                "DC_Console",
                "Output",
                0,
                Icon.FromHandle(Ratatoskr.Resource.Images.program_16x16.GetHicon()),
                DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockBottom | DockAreas.Float,
                DockState.DockBottom,
                false,
                DC_Console);
        }

        private void LoadScriptListConfig()
        {
            DC_ScriptList = new ScriptWindow_ScriptList();

            DC_ScriptList.LoadConfig();

			DockPanel_Main.AddDockContent(
                "DC_ScriptList",
                "Running Script List",
                0,
                Icon.FromHandle(Ratatoskr.Resource.Images.memo_32x32.GetHicon()),
                DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockBottom | DockAreas.Float,
                DockState.DockBottom,
                false,
                DC_ScriptList);
        }

        private void LoadConfig_OpenText()
        {
            foreach (var path in ConfigManager.User.Script_OpenFileList.Value) {
                var path_script = GetScriptFilePath(path);

                if (!File.Exists(path_script)) {
                    Directory.CreateDirectory(Path.GetDirectoryName(path_script));
                    File.WriteAllText(path_script, "");
                }

                OpenScriptFile(path_script);
            }
        }

        public void BackupConfig()
        {
            BackupConfig_OpenText();
        }

        private void BackupConfig_OpenText()
        {
            ConfigManager.User.Script_OpenFileList.Value.Clear();

            foreach (ScriptWindow_CodeEditor editor in DockPanel_Main.GetDocumentControls()) {
                /* 現在の状態を保存 */
                editor.SaveScriptFile();

                /* 開いているスクリプトを保存 */
                ConfigManager.User.Script_OpenFileList.Value.Add(editor.ScriptPath);
            }
        }

        private void BackupWindowConfig()
        {
        }

        private string GetScriptRootPath()
        {
            return (Program.GetWorkspaceDirectory("scripts"));
        }

        private string GetScriptFilePath(string path)
        {
            /* 相対パス→絶対パス変換 */
            if (!Path.IsPathRooted(path)) {
                path = GetScriptRootPath() + "/" + path;
            }

            return (path);
        }

        private ScriptWindow_CodeEditor GetOpenedScriptEditor(string path)
        {
            foreach (ScriptWindow_CodeEditor editor in DockPanel_Main.GetDocumentControls()) {
                if (editor.ScriptPath == path)return (editor);
            }

            return (null);
        }

        private void OpenScriptFile(string path)
        {
            var editor = GetOpenedScriptEditor(path);

            if (editor != null) {
				DockPanel_Main.SetActiveDocumentControl(editor);
                return;
            }

            editor = new ScriptWindow_CodeEditor();

            editor.BorderStyle = BorderStyle.None;
            editor.Tag = path;
            editor.ScriptPath = path;

            editor.EditorStatusUpdated += OnEditorStatusUpdated;
            editor.ScriptMessageAppended += OnScriptMessageOutput;
            editor.ScriptStatusChanged += OnScriptStatusChanged;

			/* 新しいタブでテキストを開く */
			DockPanel_Main.AddDockContent(
                "DC_CodeEditor",
                (path != null) ? (Path.GetFileName(path)) : ("(Temp)"),
                0,
                Icon.FromHandle(Ratatoskr.Resource.Images.memo_32x32.GetHicon()),
                DockAreas.Document,
                DockState.Document,
                true,
                editor);
        }

        private string GetCurrentScript()
        {
            if (DockPanel_Main.GetActiveDocumentControl() is CodeEditorEx dock) {
                return (dock.Text);
            } else {
                return (null);
            }
        }

        private void UpdateCurrentEditorStatus()
        {
            var editor = DockPanel_Main.GetActiveDocumentControl() as ScriptWindow_CodeEditor;

            UpdateEditorCursorStatus(editor);
            UpdateEditorScriptStatus(editor);
        }

        private void UpdateEditorCursorStatus(ScriptWindow_CodeEditor editor)
        {
            var row_text = "";
            var col_text = "";

            if (editor != null) {
                var caret_pos = editor.CaretPosition;

                row_text = string.Format("Row: {0}", caret_pos.Y);
                col_text = string.Format("Column: {0}", caret_pos.X);
            }

            Label_CodeRowNo.Text = row_text;
            Label_CodeColumnNo.Text = col_text;
        }

        private void UpdateEditorScriptStatus(ScriptWindow_CodeEditor editor)
        {
            if (editor != null) {
                MenuBar_Script_Run.Enabled = !editor.IsScriptBusy;
                MenuBar_Script_Stop.Enabled = editor.IsScriptBusy;

            } else {
                MenuBar_Script_Run.Enabled = false;
                MenuBar_Script_Stop.Enabled = false;
            }

            Btn_Script_Run.Enabled = MenuBar_Script_Run.Enabled;
            Btn_Script_Stop.Enabled = MenuBar_Script_Stop.Enabled;

            DC_Console.ClearMessage();
            if (editor != null) {
                foreach (var msg in editor.ScriptMessageList) {
                    DC_Console.AddMessage(msg);
                }
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
                case ScriptWindowActionId.FormExit:
                    Hide();
                    FormUiManager.MainFrameMenuBarUpdate();
                    break;
                
                case ScriptWindowActionId.OpenScriptDirectory:
                    System.Diagnostics.Process.Start(GetScriptRootPath());
                    break;
            }

            if (DockPanel_Main.GetActiveDocumentControl() is ScriptWindow_CodeEditor editor) {
                editor.FormKeyAction(id);
            }
        }

        private void ScriptWindow_Form_KeyDown(object sender, KeyEventArgs e)
        {
            var config = ConfigManager.System.ScriptWindow.ShortcutKey.Value.Find(
                            value => (
                                   (value.KeyPattern.IsControl == e.Control)
                                && (value.KeyPattern.IsShift == e.Shift)
                                && (value.KeyPattern.IsAlt == e.Alt)
                                && (value.KeyPattern.KeyCode == e.KeyCode)));

            if (config == null)return;

            FormKeyAction(config.ActionID);
        }

        private void OnMenuClick(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menu) {
                if (menu.Tag is ScriptWindowActionId) {
                    FormKeyAction((ScriptWindowActionId)menu.Tag);
                }
            }
        }

        private void MenuBar_Script_Run_Click(object sender, EventArgs e)
        {
            if (DockPanel_Main.GetActiveDocumentControl() is ScriptWindow_CodeEditor editor) {
                editor.ScriptRun();
            }
        }

        private void MenuBar_Script_Stop_Click(object sender, EventArgs e)
        {
            if (DockPanel_Main.GetActiveDocumentControl() is ScriptWindow_CodeEditor editor) {
                editor.ScriptStop();
            }
        }

        private void Btn_Script_Run_Click(object sender, EventArgs e)
        {
            MenuBar_Script_Run_Click(sender, e);
        }

        private void Btn_Script_Stop_Click(object sender, EventArgs e)
        {
            MenuBar_Script_Stop_Click(sender, e);
        }

        private void OnFileDoubleClick(object sender, string url)
        {
            OpenScriptFile(url);
        }

        private void DockPanel_Main_DockContentClosing(object sender, Control control, FormClosingEventArgs e)
        {
            if (control is ScriptWindow_CodeEditor editor) {
                editor.SaveScriptFile();
            }
        }

        private void DockPanel_Main_DockContentClosed(object sender, Control control, FormClosedEventArgs e)
        {
        }

        private void DockPanel_Main_ActiveDocumentChanged(object sender, EventArgs e)
        {
            UpdateCurrentEditorStatus();
        }

        private void OnEditorStatusUpdated(object sender, EventArgs e)
        {
            UpdateCurrentEditorStatus();
        }

        private void OnScriptMessageClear(object sender, EventArgs e)
        {
            if (InvokeRequired) {
                Invoke((EventHandler)OnScriptMessageClear, sender, e);
                return;
            }

            if (sender == (DockPanel_Main.GetActiveDocumentControl() as CodeEditorEx)) {
                DC_Console.ClearMessage();
            }
        }

        private void OnScriptMessageOutput(object sender, ScriptMessageData msg)
        {
            if (InvokeRequired) {
                Invoke((ScriptMessageAppendedHandler)OnScriptMessageOutput, sender, msg);
                return;
            }

            if (sender == (DockPanel_Main.GetActiveDocumentControl() as CodeEditorEx)) {
                DC_Console.AddMessage(msg);
            }
        }

        private void OnScriptStatusChanged(object sender, EventArgs e)
        {
            if (InvokeRequired) {
                Invoke((EventHandler)OnScriptStatusChanged, sender, e);
                return;
            }

            UpdateCurrentEditorStatus();
        }
    }
}
