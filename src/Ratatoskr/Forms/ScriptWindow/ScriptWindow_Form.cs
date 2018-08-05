﻿using System;
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
using Ratatoskr.Configs;
using Ratatoskr.Configs.SystemConfigs;
using Ratatoskr.Forms.Controls;
using Ratatoskr.Scripts;
using Ratatoskr.Scripts.ScriptEngines;

namespace Ratatoskr.Forms.ScriptWindow
{
    internal partial class ScriptWindow_Form : Form
    {
        private DockPanelEx Panel_DockMain;

        private FileExplorerEx           DC_FileExplorer;
        private ScriptWindow_Console     DC_Console;
        private ScriptWindow_ScriptList  DC_ScriptList;


        public ScriptWindow_Form()
        {
            InitializeComponent();
            InitializeMenuBar();

            Panel_DockMain = new DockPanelEx();
            Panel_DockMain.Dock = DockStyle.Fill;
            Panel_DockMain.DocumentStyle = DocumentStyle.DockingWindow;
            Panel_DockMain.DockContentClosing += Panel_DockMain_DockContentClosing;
            Panel_DockMain.DockContentClosed += Panel_DockMain_DockContentClosed;
            Panel_DockMain.ActiveDocumentChanged += Panel_DockMain_ActiveDocumentChanged;
            
            Container_Main.ContentPanel.Controls.Add(Panel_DockMain);
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

            Location = ConfigManager.System.ScriptWindow.Position.Value;
            Size = ConfigManager.System.ScriptWindow.Size.Value;
            if (ConfigManager.System.ScriptWindow.Maximize.Value) {
                WindowState = FormWindowState.Maximized;
            }
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
                File.WriteAllText(GetScriptFilePath("SampleScript.gds"), Properties.Resources.Sample);
            }

            DC_FileExplorer.RootUrl = root_path;

            Panel_DockMain.AddDockContent(
                "DC_FileExplorer",
                "File Explorer",
                Icon.FromHandle(Properties.Resources.memo_32x32.GetHicon()),
                DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockBottom | DockAreas.Float,
                DockState.DockLeft,
                false,
                DC_FileExplorer);
        }

        private void LoadConsoleConfig()
        {
            DC_Console = new ScriptWindow_Console();

            Panel_DockMain.AddDockContent(
                "DC_Console",
                "Output",
                Icon.FromHandle(Properties.Resources.program_16x16.GetHicon()),
                DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockBottom | DockAreas.Float,
                DockState.DockBottom,
                false,
                DC_Console);
        }

        private void LoadScriptListConfig()
        {
            DC_ScriptList = new ScriptWindow_ScriptList();

            DC_ScriptList.LoadConfig();

            Panel_DockMain.AddDockContent(
                "DC_ScriptList",
                "Running Script List",
                Icon.FromHandle(Properties.Resources.memo_32x32.GetHicon()),
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

            foreach (ScriptWindow_CodeEditor editor in Panel_DockMain.GetDocumentControls()) {
                /* 現在の状態を保存 */
                editor.SaveScriptFile();

                /* 開いているスクリプトを保存 */
                ConfigManager.User.Script_OpenFileList.Value.Add(editor.ScriptPath);
            }
        }

        private void BackupWindowConfig()
        {
            if (WindowState == FormWindowState.Normal) {
                ConfigManager.System.ScriptWindow.Position.Value = Location;
                ConfigManager.System.ScriptWindow.Size.Value = Size;
            }

            ConfigManager.System.ScriptWindow.Maximize.Value = (WindowState == FormWindowState.Maximized);
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
            foreach (ScriptWindow_CodeEditor editor in Panel_DockMain.GetDocumentControls()) {
                if (editor.ScriptPath == path)return (editor);
            }

            return (null);
        }

        private void OpenScriptFile(string path)
        {
            var editor = GetOpenedScriptEditor(path);

            if (editor != null) {
                Panel_DockMain.SetActiveDocumentControl(editor);
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
            Panel_DockMain.AddDockContent(
                "DC_CodeEditor",
                (path != null) ? (Path.GetFileName(path)) : ("(Temp)"),
                Icon.FromHandle(Properties.Resources.memo_32x32.GetHicon()),
                DockAreas.Document,
                DockState.Document,
                true,
                editor);
        }

        private string GetCurrentScript()
        {
            var dock = Panel_DockMain.GetActiveDocumentControl() as CodeEditorEx;

            if (dock == null)return (null);

            return (dock.Text);
        }

        private void UpdateCurrentEditorStatus()
        {
            var editor = Panel_DockMain.GetActiveDocumentControl() as ScriptWindow_CodeEditor;

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

            var editor = Panel_DockMain.GetActiveDocumentControl() as ScriptWindow_CodeEditor;

            if (editor != null) {
                editor.FormKeyAction(id);
            }
        }

        private void ScriptWindow_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* 終了させずに非表示 */
            e.Cancel = true;

            FormKeyAction(ScriptWindowActionId.FormExit);
        }

        private void ScriptWindow_Form_Move(object sender, EventArgs e)
        {
            BackupWindowConfig();
        }

        private void ScriptWindow_Form_Resize(object sender, EventArgs e)
        {
            BackupWindowConfig();
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
            var menu = sender as ToolStripMenuItem;

            if (menu == null)return;
            
            if (menu.Tag is ScriptWindowActionId) {
                FormKeyAction((ScriptWindowActionId)menu.Tag);
            }
        }

        private void MenuBar_Script_Run_Click(object sender, EventArgs e)
        {
            var editor = Panel_DockMain.GetActiveDocumentControl() as ScriptWindow_CodeEditor;

            if (editor == null)return;

            editor.ScriptRun();
        }

        private void MenuBar_Script_Stop_Click(object sender, EventArgs e)
        {
            var editor = Panel_DockMain.GetActiveDocumentControl() as ScriptWindow_CodeEditor;

            if (editor == null)return;

            editor.ScriptStop();
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

        private void Panel_DockMain_DockContentClosing(object sender, Control control, FormClosingEventArgs e)
        {
            var editor = control as ScriptWindow_CodeEditor;

            if (editor == null)return;

            editor.SaveScriptFile();
        }

        private void Panel_DockMain_DockContentClosed(object sender, Control control, FormClosedEventArgs e)
        {
        }

        private void Panel_DockMain_ActiveDocumentChanged(object sender, EventArgs e)
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

            if (sender == (Panel_DockMain.GetActiveDocumentControl() as CodeEditorEx)) {
                DC_Console.ClearMessage();
            }
        }

        private void OnScriptMessageOutput(object sender, ScriptMessageData msg)
        {
            if (InvokeRequired) {
                Invoke((ScriptMessageAppendedHandler)OnScriptMessageOutput, sender, msg);
                return;
            }

            if (sender == (Panel_DockMain.GetActiveDocumentControl() as CodeEditorEx)) {
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