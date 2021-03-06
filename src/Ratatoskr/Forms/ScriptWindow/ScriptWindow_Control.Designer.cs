﻿namespace Ratatoskr.Forms.ScriptWindow
{
    partial class ScriptWindow_Control
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptWindow_Control));
            this.Container_Main = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Label_CodeRowNo = new System.Windows.Forms.ToolStripStatusLabel();
            this.Label_CodeColumnNo = new System.Windows.Forms.ToolStripStatusLabel();
            this.DockPanel_Main = new Ratatoskr.Forms.DockPanelEx();
            this.MenuBar_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_File_OpenScriptDir = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuBar_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Script = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Script_Run = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Script_Stop = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Root = new System.Windows.Forms.MenuStrip();
            this.Btn_Script_Run = new System.Windows.Forms.ToolStripButton();
            this.Btn_Script_Stop = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Container_Main.BottomToolStripPanel.SuspendLayout();
            this.Container_Main.ContentPanel.SuspendLayout();
            this.Container_Main.TopToolStripPanel.SuspendLayout();
            this.Container_Main.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DockPanel_Main)).BeginInit();
            this.MenuBar_Root.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Container_Main
            // 
            // 
            // Container_Main.BottomToolStripPanel
            // 
            this.Container_Main.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // Container_Main.ContentPanel
            // 
            this.Container_Main.ContentPanel.Controls.Add(this.DockPanel_Main);
            this.Container_Main.ContentPanel.Size = new System.Drawing.Size(872, 449);
            this.Container_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Container_Main.Location = new System.Drawing.Point(0, 0);
            this.Container_Main.Name = "Container_Main";
            this.Container_Main.Size = new System.Drawing.Size(872, 546);
            this.Container_Main.TabIndex = 0;
            this.Container_Main.Text = "toolStripContainer1";
            // 
            // Container_Main.TopToolStripPanel
            // 
            this.Container_Main.TopToolStripPanel.Controls.Add(this.MenuBar_Root);
            this.Container_Main.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.Label_CodeRowNo,
            this.Label_CodeColumnNo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(872, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(657, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // Label_CodeRowNo
            // 
            this.Label_CodeRowNo.AutoSize = false;
            this.Label_CodeRowNo.Name = "Label_CodeRowNo";
            this.Label_CodeRowNo.Size = new System.Drawing.Size(100, 17);
            this.Label_CodeRowNo.Text = "Row: ";
            this.Label_CodeRowNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_CodeColumnNo
            // 
            this.Label_CodeColumnNo.AutoSize = false;
            this.Label_CodeColumnNo.Name = "Label_CodeColumnNo";
            this.Label_CodeColumnNo.Size = new System.Drawing.Size(100, 17);
            this.Label_CodeColumnNo.Text = "Column: ";
            this.Label_CodeColumnNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DockPanel_Main
            // 
            this.DockPanel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DockPanel_Main.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(242)))));
            this.DockPanel_Main.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.DockPanel_Main.Location = new System.Drawing.Point(0, 0);
            this.DockPanel_Main.Name = "DockPanel_Main";
            this.DockPanel_Main.Padding = new System.Windows.Forms.Padding(6);
            this.DockPanel_Main.ShowAutoHideContentOnHover = false;
            this.DockPanel_Main.Size = new System.Drawing.Size(872, 449);
            this.DockPanel_Main.TabIndex = 0;
            this.DockPanel_Main.DockContentClosing += new Ratatoskr.Forms.DockPanelEx.DockContentClosingHandler(this.DockPanel_Main_DockContentClosing);
            this.DockPanel_Main.DockContentClosed += new Ratatoskr.Forms.DockPanelEx.DockContentClosedHandler(this.DockPanel_Main_DockContentClosed);
            this.DockPanel_Main.ActiveDocumentChanged += new System.EventHandler(this.DockPanel_Main_ActiveDocumentChanged);
            // 
            // MenuBar_File
            // 
            this.MenuBar_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_File_OpenScriptDir,
            this.toolStripSeparator1,
            this.MenuBar_File_Exit});
            this.MenuBar_File.Name = "MenuBar_File";
            this.MenuBar_File.Size = new System.Drawing.Size(37, 20);
            this.MenuBar_File.Text = "File";
            // 
            // MenuBar_File_OpenScriptDir
            // 
            this.MenuBar_File_OpenScriptDir.Name = "MenuBar_File_OpenScriptDir";
            this.MenuBar_File_OpenScriptDir.Size = new System.Drawing.Size(193, 22);
            this.MenuBar_File_OpenScriptDir.Tag = "OpenScriptDirectory";
            this.MenuBar_File_OpenScriptDir.Text = "Open script directory...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(190, 6);
            // 
            // MenuBar_File_Exit
            // 
            this.MenuBar_File_Exit.Name = "MenuBar_File_Exit";
            this.MenuBar_File_Exit.Size = new System.Drawing.Size(193, 22);
            this.MenuBar_File_Exit.Tag = "FormExit";
            this.MenuBar_File_Exit.Text = "Exit";
            // 
            // MenuBar_Script
            // 
            this.MenuBar_Script.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_Script_Run,
            this.MenuBar_Script_Stop});
            this.MenuBar_Script.Name = "MenuBar_Script";
            this.MenuBar_Script.Size = new System.Drawing.Size(49, 20);
            this.MenuBar_Script.Text = "Script";
            // 
            // MenuBar_Script_Run
            // 
            this.MenuBar_Script_Run.Image = ((System.Drawing.Image)(resources.GetObject("MenuBar_Script_Run.Image")));
            this.MenuBar_Script_Run.Name = "MenuBar_Script_Run";
            this.MenuBar_Script_Run.Size = new System.Drawing.Size(173, 22);
            this.MenuBar_Script_Run.Tag = "ScriptRun";
            this.MenuBar_Script_Run.Text = "Current Script Run";
            this.MenuBar_Script_Run.Click += new System.EventHandler(this.MenuBar_Script_Run_Click);
            // 
            // MenuBar_Script_Stop
            // 
            this.MenuBar_Script_Stop.Image = ((System.Drawing.Image)(resources.GetObject("MenuBar_Script_Stop.Image")));
            this.MenuBar_Script_Stop.Name = "MenuBar_Script_Stop";
            this.MenuBar_Script_Stop.Size = new System.Drawing.Size(173, 22);
            this.MenuBar_Script_Stop.Tag = "ScriptStop";
            this.MenuBar_Script_Stop.Text = "Current Script Stop";
            this.MenuBar_Script_Stop.Click += new System.EventHandler(this.MenuBar_Script_Stop_Click);
            // 
            // MenuBar_Root
            // 
            this.MenuBar_Root.Dock = System.Windows.Forms.DockStyle.None;
            this.MenuBar_Root.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_File,
            this.MenuBar_Script});
            this.MenuBar_Root.Location = new System.Drawing.Point(0, 0);
            this.MenuBar_Root.Name = "MenuBar_Root";
            this.MenuBar_Root.Size = new System.Drawing.Size(872, 24);
            this.MenuBar_Root.TabIndex = 0;
            this.MenuBar_Root.Text = "menuStrip1";
            // 
            // Btn_Script_Run
            // 
            this.Btn_Script_Run.AutoSize = false;
            this.Btn_Script_Run.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Script_Run.Image")));
            this.Btn_Script_Run.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_Script_Run.Name = "Btn_Script_Run";
            this.Btn_Script_Run.Size = new System.Drawing.Size(48, 48);
            this.Btn_Script_Run.Text = "Run";
            this.Btn_Script_Run.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Script_Run.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Btn_Script_Run.Click += new System.EventHandler(this.Btn_Script_Run_Click);
            // 
            // Btn_Script_Stop
            // 
            this.Btn_Script_Stop.AutoSize = false;
            this.Btn_Script_Stop.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Script_Stop.Image")));
            this.Btn_Script_Stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_Script_Stop.Name = "Btn_Script_Stop";
            this.Btn_Script_Stop.Size = new System.Drawing.Size(48, 48);
            this.Btn_Script_Stop.Text = "Stop";
            this.Btn_Script_Stop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Script_Stop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Btn_Script_Stop.Click += new System.EventHandler(this.Btn_Script_Stop_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Btn_Script_Run,
            this.Btn_Script_Stop});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(872, 51);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 1;
            // 
            // ScriptWindow_Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Container_Main);
            this.Name = "ScriptWindow_Control";
            this.Size = new System.Drawing.Size(872, 546);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ScriptWindow_Form_KeyDown);
            this.Container_Main.BottomToolStripPanel.ResumeLayout(false);
            this.Container_Main.BottomToolStripPanel.PerformLayout();
            this.Container_Main.ContentPanel.ResumeLayout(false);
            this.Container_Main.TopToolStripPanel.ResumeLayout(false);
            this.Container_Main.TopToolStripPanel.PerformLayout();
            this.Container_Main.ResumeLayout(false);
            this.Container_Main.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DockPanel_Main)).EndInit();
            this.MenuBar_Root.ResumeLayout(false);
            this.MenuBar_Root.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer Container_Main;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel Label_CodeRowNo;
        private System.Windows.Forms.ToolStripStatusLabel Label_CodeColumnNo;
		private DockPanelEx DockPanel_Main;
		private System.Windows.Forms.MenuStrip MenuBar_Root;
		private System.Windows.Forms.ToolStripMenuItem MenuBar_File;
		private System.Windows.Forms.ToolStripMenuItem MenuBar_File_OpenScriptDir;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem MenuBar_File_Exit;
		private System.Windows.Forms.ToolStripMenuItem MenuBar_Script;
		private System.Windows.Forms.ToolStripMenuItem MenuBar_Script_Run;
		private System.Windows.Forms.ToolStripMenuItem MenuBar_Script_Stop;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton Btn_Script_Run;
		private System.Windows.Forms.ToolStripButton Btn_Script_Stop;
	}
}