namespace Ratatoskr.Debugger
{
    partial class DebugMonitorForm
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.LView_Status = new ListViewCustom();
			this.LView_Status_Column_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.LView_Status_Column_Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.TBox_Message = new System.Windows.Forms.TextBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuBar_Edit_ScreenClear = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuBar_View = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuBar_Filter = new System.Windows.Forms.ToolStripMenuItem();
			this.デバッグDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuBar_Debug_MessageWatch = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuBar_Debug_StatusWatch = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer1
			// 
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
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
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.LView_Status);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.TBox_Message);
			this.splitContainer1.Size = new System.Drawing.Size(1008, 578);
			this.splitContainer1.SplitterDistance = 360;
			this.splitContainer1.TabIndex = 2;
			// 
			// LView_Status
			// 
			this.LView_Status.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LView_Status_Column_Name,
            this.LView_Status_Column_Value});
			this.LView_Status.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LView_Status.HideSelection = false;
			this.LView_Status.Location = new System.Drawing.Point(0, 0);
			this.LView_Status.Name = "LView_Status";
			this.LView_Status.Size = new System.Drawing.Size(360, 578);
			this.LView_Status.TabIndex = 0;
			this.LView_Status.UseCompatibleStateImageBehavior = false;
			this.LView_Status.View = System.Windows.Forms.View.Details;
			// 
			// LView_Status_Column_Name
			// 
			this.LView_Status_Column_Name.Text = "Name";
			this.LView_Status_Column_Name.Width = 160;
			// 
			// LView_Status_Column_Value
			// 
			this.LView_Status_Column_Value.Text = "Value";
			this.LView_Status_Column_Value.Width = 296;
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
			this.TBox_Message.Size = new System.Drawing.Size(644, 578);
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
            this.MenuBar_Debug_MessageWatch,
            this.MenuBar_Debug_StatusWatch});
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
			// MenuBar_Debug_StatusWatch
			// 
			this.MenuBar_Debug_StatusWatch.Name = "MenuBar_Debug_StatusWatch";
			this.MenuBar_Debug_StatusWatch.Size = new System.Drawing.Size(180, 22);
			this.MenuBar_Debug_StatusWatch.Text = "ステータス監視";
			this.MenuBar_Debug_StatusWatch.Click += new System.EventHandler(this.MenuBar_Debug_StatusWatch_Click);
			// 
			// DebugMonitorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1008, 602);
			this.Controls.Add(this.toolStripContainer1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "DebugMonitorForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Debug Window";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugForm_FormClosing);
			this.VisibleChanged += new System.EventHandler(this.DebugForm_VisibleChanged);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
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
		private System.Windows.Forms.SplitContainer splitContainer1;
		private ListViewCustom LView_Status;
		private System.Windows.Forms.ToolStripMenuItem MenuBar_Debug_StatusWatch;
		private System.Windows.Forms.ColumnHeader LView_Status_Column_Name;
		private System.Windows.Forms.ColumnHeader LView_Status_Column_Value;
	}
}