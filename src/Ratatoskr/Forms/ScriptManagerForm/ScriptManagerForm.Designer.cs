namespace Ratatoskr.Forms.ScriptManagerForm
{
    partial class ScriptManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptManagerForm));
            this.Container_Main = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Label_CodeRowNo = new System.Windows.Forms.ToolStripStatusLabel();
            this.Label_CodeColumnNo = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.スクリプトSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Script_Run = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Script_Stop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Btn_Script_Run = new System.Windows.Forms.ToolStripButton();
            this.Btn_Script_Stop = new System.Windows.Forms.ToolStripButton();
            this.Container_Main.BottomToolStripPanel.SuspendLayout();
            this.Container_Main.TopToolStripPanel.SuspendLayout();
            this.Container_Main.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            this.Container_Main.TopToolStripPanel.Controls.Add(this.menuStrip1);
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
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.スクリプトSToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(872, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // スクリプトSToolStripMenuItem
            // 
            this.スクリプトSToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_Script_Run,
            this.MenuBar_Script_Stop});
            this.スクリプトSToolStripMenuItem.Name = "スクリプトSToolStripMenuItem";
            this.スクリプトSToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.スクリプトSToolStripMenuItem.Text = "Script";
            // 
            // MenuBar_Script_Run
            // 
            this.MenuBar_Script_Run.Image = global::Ratatoskr.Properties.Resources.play_32x32;
            this.MenuBar_Script_Run.Name = "MenuBar_Script_Run";
            this.MenuBar_Script_Run.Size = new System.Drawing.Size(169, 22);
            this.MenuBar_Script_Run.Text = "Current Script Run";
            this.MenuBar_Script_Run.Click += new System.EventHandler(this.MenuBar_Script_Run_Click);
            // 
            // MenuBar_Script_Stop
            // 
            this.MenuBar_Script_Stop.Image = global::Ratatoskr.Properties.Resources.stop_32x32;
            this.MenuBar_Script_Stop.Name = "MenuBar_Script_Stop";
            this.MenuBar_Script_Stop.Size = new System.Drawing.Size(169, 22);
            this.MenuBar_Script_Stop.Text = "Current Script Stop";
            this.MenuBar_Script_Stop.Click += new System.EventHandler(this.MenuBar_Script_Stop_Click);
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
            // Btn_Script_Run
            // 
            this.Btn_Script_Run.AutoSize = false;
            this.Btn_Script_Run.Image = global::Ratatoskr.Properties.Resources.play_32x32;
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
            this.Btn_Script_Stop.Image = global::Ratatoskr.Properties.Resources.stop_32x32;
            this.Btn_Script_Stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_Script_Stop.Name = "Btn_Script_Stop";
            this.Btn_Script_Stop.Size = new System.Drawing.Size(48, 48);
            this.Btn_Script_Stop.Text = "Stop";
            this.Btn_Script_Stop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Script_Stop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Btn_Script_Stop.Click += new System.EventHandler(this.Btn_Script_Stop_Click);
            // 
            // ScriptManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 546);
            this.Controls.Add(this.Container_Main);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ScriptManagerForm";
            this.Text = "Script Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScriptManagerForm_FormClosing);
            this.Move += new System.EventHandler(this.ScriptManagerForm_Move);
            this.Resize += new System.EventHandler(this.ScriptManagerForm_Resize);
            this.Container_Main.BottomToolStripPanel.ResumeLayout(false);
            this.Container_Main.BottomToolStripPanel.PerformLayout();
            this.Container_Main.TopToolStripPanel.ResumeLayout(false);
            this.Container_Main.TopToolStripPanel.PerformLayout();
            this.Container_Main.ResumeLayout(false);
            this.Container_Main.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer Container_Main;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem スクリプトSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Script_Run;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel Label_CodeRowNo;
        private System.Windows.Forms.ToolStripStatusLabel Label_CodeColumnNo;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Script_Stop;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Btn_Script_Run;
        private System.Windows.Forms.ToolStripButton Btn_Script_Stop;
    }
}