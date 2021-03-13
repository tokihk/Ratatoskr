namespace Ratatoskr.Debugger
{
    partial class DebugMessageMonitor
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
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.TBox_Message = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Edit_ScreenClear = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_View = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Filter = new System.Windows.Forms.ToolStripMenuItem();
            this.デバッグDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Debug_MessageWatch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.TBox_Message);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(1008, 578);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(1008, 602);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            // 
            // TBox_Message
            // 
            this.TBox_Message.BackColor = System.Drawing.SystemColors.Window;
            this.TBox_Message.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_Message.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TBox_Message.Location = new System.Drawing.Point(0, 0);
            this.TBox_Message.Multiline = true;
            this.TBox_Message.Name = "TBox_Message";
            this.TBox_Message.ReadOnly = true;
            this.TBox_Message.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TBox_Message.Size = new System.Drawing.Size(1008, 578);
            this.TBox_Message.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.編集EToolStripMenuItem,
            this.MenuBar_View,
            this.MenuBar_Filter,
            this.デバッグDToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 編集EToolStripMenuItem
            // 
            this.編集EToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_Edit_ScreenClear});
            this.編集EToolStripMenuItem.Name = "編集EToolStripMenuItem";
            this.編集EToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.編集EToolStripMenuItem.Text = "編集(&E)";
            // 
            // MenuBar_Edit_ScreenClear
            // 
            this.MenuBar_Edit_ScreenClear.Name = "MenuBar_Edit_ScreenClear";
            this.MenuBar_Edit_ScreenClear.Size = new System.Drawing.Size(139, 22);
            this.MenuBar_Edit_ScreenClear.Text = "画面クリア(&C)";
            this.MenuBar_Edit_ScreenClear.Click += new System.EventHandler(this.MenuBar_Edit_ScreenClear_Click);
            // 
            // MenuBar_View
            // 
            this.MenuBar_View.Name = "MenuBar_View";
            this.MenuBar_View.Size = new System.Drawing.Size(58, 20);
            this.MenuBar_View.Text = "表示(&V)";
            // 
            // MenuBar_Filter
            // 
            this.MenuBar_Filter.Name = "MenuBar_Filter";
            this.MenuBar_Filter.Size = new System.Drawing.Size(67, 20);
            this.MenuBar_Filter.Text = "フィルタ(&F)";
            // 
            // デバッグDToolStripMenuItem
            // 
            this.デバッグDToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_Debug_MessageWatch});
            this.デバッグDToolStripMenuItem.Name = "デバッグDToolStripMenuItem";
            this.デバッグDToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.デバッグDToolStripMenuItem.Text = "デバッグ(&D)";
            // 
            // MenuBar_Debug_MessageWatch
            // 
            this.MenuBar_Debug_MessageWatch.Name = "MenuBar_Debug_MessageWatch";
            this.MenuBar_Debug_MessageWatch.Size = new System.Drawing.Size(180, 22);
            this.MenuBar_Debug_MessageWatch.Text = "メッセージ監視";
            this.MenuBar_Debug_MessageWatch.Click += new System.EventHandler(this.MenuBar_Debug_MessageWatch_Click);
            // 
            // DebugWindow_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 602);
            this.Controls.Add(this.toolStripContainer1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DebugWindow_Form";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Debug Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugForm_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.DebugForm_VisibleChanged);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.TextBox TBox_Message;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 編集EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Edit_ScreenClear;
		private System.Windows.Forms.ToolStripMenuItem MenuBar_View;
		private System.Windows.Forms.ToolStripMenuItem MenuBar_Filter;
		private System.Windows.Forms.ToolStripMenuItem デバッグDToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem MenuBar_Debug_MessageWatch;
	}
}