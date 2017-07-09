namespace Ratatoskr.Forms.MainFrame
{
    partial class MainFrame
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
            if (disposing && (components != null))
            {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrame));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Label_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.PBar_Status = new System.Windows.Forms.ToolStripProgressBar();
            this.Label_PktCount_Raw = new System.Windows.Forms.ToolStripStatusLabel();
            this.Label_PktCount_View = new System.Windows.Forms.ToolStripStatusLabel();
            this.Label_PktCount_Busy = new System.Windows.Forms.ToolStripStatusLabel();
            this.Label_ViewDrawMode = new System.Windows.Forms.ToolStripStatusLabel();
            this.Panel_Center = new Ratatoskr.Forms.MainFrame.MainFrameCenter();
            this.SingleCmdPanel_Main = new Ratatoskr.Forms.MainFrame.MainFrameSingleCommandPanel();
            this.MBar_Main = new System.Windows.Forms.MenuStrip();
            this.MenuBar_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_File_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuBar_File_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_File_Save_Original = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_File_Save_Shaping = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_File_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_File_SaveAs_Original = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_File_SaveAs_Shaping = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuBar_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Edit_EventClear = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_View = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_View_PacketConverterAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_View_PacketViewAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuBar_View_PacketViewRedraw = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuBar_View_AutoScroll = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Tool = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Edit_TimeStamp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuBar_Tool_Option = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Help_About = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.MBar_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.Panel_Center);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.SingleCmdPanel_Main);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(784, 515);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(784, 562);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.MBar_Main);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Label_Status,
            this.PBar_Status,
            this.Label_PktCount_Raw,
            this.Label_PktCount_View,
            this.Label_PktCount_Busy,
            this.Label_ViewDrawMode});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 23);
            this.statusStrip1.TabIndex = 0;
            // 
            // Label_Status
            // 
            this.Label_Status.Name = "Label_Status";
            this.Label_Status.Size = new System.Drawing.Size(117, 18);
            this.Label_Status.Spring = true;
            this.Label_Status.Text = "起動完了";
            this.Label_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PBar_Status
            // 
            this.PBar_Status.Name = "PBar_Status";
            this.PBar_Status.Size = new System.Drawing.Size(200, 17);
            // 
            // Label_PktCount_Raw
            // 
            this.Label_PktCount_Raw.AutoSize = false;
            this.Label_PktCount_Raw.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.Label_PktCount_Raw.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_PktCount_Raw.Name = "Label_PktCount_Raw";
            this.Label_PktCount_Raw.Size = new System.Drawing.Size(130, 18);
            this.Label_PktCount_Raw.Text = "Raw: 9999999";
            // 
            // Label_PktCount_View
            // 
            this.Label_PktCount_View.AutoSize = false;
            this.Label_PktCount_View.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.Label_PktCount_View.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_PktCount_View.Name = "Label_PktCount_View";
            this.Label_PktCount_View.Size = new System.Drawing.Size(130, 18);
            this.Label_PktCount_View.Text = "View: 9999999";
            // 
            // Label_PktCount_Busy
            // 
            this.Label_PktCount_Busy.AutoSize = false;
            this.Label_PktCount_Busy.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.Label_PktCount_Busy.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_PktCount_Busy.Name = "Label_PktCount_Busy";
            this.Label_PktCount_Busy.Size = new System.Drawing.Size(130, 18);
            this.Label_PktCount_Busy.Text = "Busy: 9999999";
            // 
            // Label_ViewDrawMode
            // 
            this.Label_ViewDrawMode.AutoSize = false;
            this.Label_ViewDrawMode.BackColor = System.Drawing.Color.Aqua;
            this.Label_ViewDrawMode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Label_ViewDrawMode.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.Label_ViewDrawMode.DoubleClickEnabled = true;
            this.Label_ViewDrawMode.Font = new System.Drawing.Font("メイリオ", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_ViewDrawMode.Name = "Label_ViewDrawMode";
            this.Label_ViewDrawMode.Size = new System.Drawing.Size(60, 18);
            this.Label_ViewDrawMode.Text = "High";
            this.Label_ViewDrawMode.DoubleClick += new System.EventHandler(this.Label_ViewDrawMode_DoubleClick);
            // 
            // Panel_Center
            // 
            this.Panel_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Center.Location = new System.Drawing.Point(0, 0);
            this.Panel_Center.Name = "Panel_Center";
            this.Panel_Center.Size = new System.Drawing.Size(784, 471);
            this.Panel_Center.TabIndex = 3;
            // 
            // SingleCmdPanel_Main
            // 
            this.SingleCmdPanel_Main.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SingleCmdPanel_Main.Location = new System.Drawing.Point(0, 471);
            this.SingleCmdPanel_Main.MaximumSize = new System.Drawing.Size(4096, 44);
            this.SingleCmdPanel_Main.MinimumSize = new System.Drawing.Size(0, 44);
            this.SingleCmdPanel_Main.Name = "SingleCmdPanel_Main";
            this.SingleCmdPanel_Main.Size = new System.Drawing.Size(784, 44);
            this.SingleCmdPanel_Main.TabIndex = 2;
            // 
            // MBar_Main
            // 
            this.MBar_Main.Dock = System.Windows.Forms.DockStyle.None;
            this.MBar_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_File,
            this.MenuBar_Edit,
            this.MenuBar_View,
            this.MenuBar_Tool,
            this.MenuBar_Help});
            this.MBar_Main.Location = new System.Drawing.Point(0, 0);
            this.MBar_Main.Name = "MBar_Main";
            this.MBar_Main.Size = new System.Drawing.Size(784, 24);
            this.MBar_Main.TabIndex = 0;
            this.MBar_Main.Text = "menuStrip1";
            // 
            // MenuBar_File
            // 
            this.MenuBar_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_File_Open,
            this.toolStripSeparator2,
            this.MenuBar_File_Save,
            this.MenuBar_File_SaveAs,
            this.toolStripSeparator1,
            this.MenuBar_File_Exit});
            this.MenuBar_File.Name = "MenuBar_File";
            this.MenuBar_File.Size = new System.Drawing.Size(37, 20);
            this.MenuBar_File.Text = "File";
            // 
            // MenuBar_File_Open
            // 
            this.MenuBar_File_Open.Name = "MenuBar_File_Open";
            this.MenuBar_File_Open.Size = new System.Drawing.Size(152, 22);
            this.MenuBar_File_Open.Tag = "FileOpen";
            this.MenuBar_File_Open.Text = "Open";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // MenuBar_File_Save
            // 
            this.MenuBar_File_Save.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_File_Save_Original,
            this.MenuBar_File_Save_Shaping});
            this.MenuBar_File_Save.Name = "MenuBar_File_Save";
            this.MenuBar_File_Save.Size = new System.Drawing.Size(152, 22);
            this.MenuBar_File_Save.Text = "Save";
            this.MenuBar_File_Save.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // MenuBar_File_Save_Original
            // 
            this.MenuBar_File_Save_Original.Name = "MenuBar_File_Save_Original";
            this.MenuBar_File_Save_Original.Size = new System.Drawing.Size(152, 22);
            this.MenuBar_File_Save_Original.Tag = "PacketSaveRuleOff";
            this.MenuBar_File_Save_Original.Text = "Original";
            // 
            // MenuBar_File_Save_Shaping
            // 
            this.MenuBar_File_Save_Shaping.Name = "MenuBar_File_Save_Shaping";
            this.MenuBar_File_Save_Shaping.Size = new System.Drawing.Size(152, 22);
            this.MenuBar_File_Save_Shaping.Tag = "PacketSaveRuleOn";
            this.MenuBar_File_Save_Shaping.Text = "Shaping";
            // 
            // MenuBar_File_SaveAs
            // 
            this.MenuBar_File_SaveAs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_File_SaveAs_Original,
            this.MenuBar_File_SaveAs_Shaping});
            this.MenuBar_File_SaveAs.Name = "MenuBar_File_SaveAs";
            this.MenuBar_File_SaveAs.Size = new System.Drawing.Size(152, 22);
            this.MenuBar_File_SaveAs.Text = "Save as";
            // 
            // MenuBar_File_SaveAs_Original
            // 
            this.MenuBar_File_SaveAs_Original.Name = "MenuBar_File_SaveAs_Original";
            this.MenuBar_File_SaveAs_Original.Size = new System.Drawing.Size(152, 22);
            this.MenuBar_File_SaveAs_Original.Tag = "PacketSaveAsRuleOff";
            this.MenuBar_File_SaveAs_Original.Text = "Original...";
            // 
            // MenuBar_File_SaveAs_Shaping
            // 
            this.MenuBar_File_SaveAs_Shaping.Name = "MenuBar_File_SaveAs_Shaping";
            this.MenuBar_File_SaveAs_Shaping.Size = new System.Drawing.Size(152, 22);
            this.MenuBar_File_SaveAs_Shaping.Tag = "PacketSaveAsRuleOn";
            this.MenuBar_File_SaveAs_Shaping.Text = "Shaping...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // MenuBar_File_Exit
            // 
            this.MenuBar_File_Exit.Name = "MenuBar_File_Exit";
            this.MenuBar_File_Exit.Size = new System.Drawing.Size(152, 22);
            this.MenuBar_File_Exit.Tag = "ApplicationExit";
            this.MenuBar_File_Exit.Text = "Exit";
            // 
            // MenuBar_Edit
            // 
            this.MenuBar_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_Edit_EventClear});
            this.MenuBar_Edit.Name = "MenuBar_Edit";
            this.MenuBar_Edit.Size = new System.Drawing.Size(39, 20);
            this.MenuBar_Edit.Text = "Edit";
            // 
            // MenuBar_Edit_EventClear
            // 
            this.MenuBar_Edit_EventClear.Name = "MenuBar_Edit_EventClear";
            this.MenuBar_Edit_EventClear.Size = new System.Drawing.Size(152, 22);
            this.MenuBar_Edit_EventClear.Tag = "EventPacketClear";
            this.MenuBar_Edit_EventClear.Text = "Buffer clear";
            // 
            // MenuBar_View
            // 
            this.MenuBar_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_View_PacketConverterAdd,
            this.MenuBar_View_PacketViewAdd,
            this.toolStripSeparator3,
            this.MenuBar_View_PacketViewRedraw,
            this.toolStripSeparator4,
            this.MenuBar_View_AutoScroll});
            this.MenuBar_View.Name = "MenuBar_View";
            this.MenuBar_View.Size = new System.Drawing.Size(44, 20);
            this.MenuBar_View.Text = "View";
            // 
            // MenuBar_View_PacketConverterAdd
            // 
            this.MenuBar_View_PacketConverterAdd.Name = "MenuBar_View_PacketConverterAdd";
            this.MenuBar_View_PacketConverterAdd.Size = new System.Drawing.Size(161, 22);
            this.MenuBar_View_PacketConverterAdd.Text = "Add converter";
            // 
            // MenuBar_View_PacketViewAdd
            // 
            this.MenuBar_View_PacketViewAdd.Name = "MenuBar_View_PacketViewAdd";
            this.MenuBar_View_PacketViewAdd.Size = new System.Drawing.Size(161, 22);
            this.MenuBar_View_PacketViewAdd.Text = "Add packet view";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(158, 6);
            // 
            // MenuBar_View_PacketViewRedraw
            // 
            this.MenuBar_View_PacketViewRedraw.Name = "MenuBar_View_PacketViewRedraw";
            this.MenuBar_View_PacketViewRedraw.Size = new System.Drawing.Size(161, 22);
            this.MenuBar_View_PacketViewRedraw.Tag = "EventPacketRedraw";
            this.MenuBar_View_PacketViewRedraw.Text = "Redraw";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(158, 6);
            // 
            // MenuBar_View_AutoScroll
            // 
            this.MenuBar_View_AutoScroll.Name = "MenuBar_View_AutoScroll";
            this.MenuBar_View_AutoScroll.Size = new System.Drawing.Size(161, 22);
            this.MenuBar_View_AutoScroll.Tag = "AutoScrollToggle";
            this.MenuBar_View_AutoScroll.Text = "Auto scroll";
            // 
            // MenuBar_Tool
            // 
            this.MenuBar_Tool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_Edit_TimeStamp,
            this.toolStripSeparator7,
            this.MenuBar_Tool_Option});
            this.MenuBar_Tool.Name = "MenuBar_Tool";
            this.MenuBar_Tool.Size = new System.Drawing.Size(41, 20);
            this.MenuBar_Tool.Text = "Tool";
            // 
            // MenuBar_Edit_TimeStamp
            // 
            this.MenuBar_Edit_TimeStamp.Name = "MenuBar_Edit_TimeStamp";
            this.MenuBar_Edit_TimeStamp.Size = new System.Drawing.Size(152, 22);
            this.MenuBar_Edit_TimeStamp.Tag = "TimeStampRun";
            this.MenuBar_Edit_TimeStamp.Text = "Time stamp";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(149, 6);
            // 
            // MenuBar_Tool_Option
            // 
            this.MenuBar_Tool_Option.Name = "MenuBar_Tool_Option";
            this.MenuBar_Tool_Option.Size = new System.Drawing.Size(152, 22);
            this.MenuBar_Tool_Option.Tag = "ShowConfigDialog";
            this.MenuBar_Tool_Option.Text = "Options...";
            // 
            // MenuBar_Help
            // 
            this.MenuBar_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_Help_About});
            this.MenuBar_Help.Name = "MenuBar_Help";
            this.MenuBar_Help.Size = new System.Drawing.Size(44, 20);
            this.MenuBar_Help.Text = "Help";
            // 
            // MenuBar_Help_About
            // 
            this.MenuBar_Help_About.Name = "MenuBar_Help_About";
            this.MenuBar_Help_About.Size = new System.Drawing.Size(159, 22);
            this.MenuBar_Help_About.Tag = "ShowAppInformation";
            this.MenuBar_Help_About.Text = "About Ratatoskr";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1, 0);
            this.toolStrip1.TabIndex = 1;
            // 
            // MainFrame
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.MBar_Main;
            this.MinimumSize = new System.Drawing.Size(480, 320);
            this.Name = "MainFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Ratatoskr";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainFrame_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainFrame_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainFrame_KeyDown);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.MBar_Main.ResumeLayout(false);
            this.MBar_Main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip MBar_Main;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_File;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_File_Exit;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_File_Save;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_File_Open;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_File_Save_Original;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_File_Save_Shaping;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_File_SaveAs;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_File_SaveAs_Original;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_File_SaveAs_Shaping;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_View;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_View_AutoScroll;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Help;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Help_About;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Tool;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Tool_Option;
        private System.Windows.Forms.ToolStripStatusLabel Label_Status;
        private System.Windows.Forms.ToolStripStatusLabel Label_PktCount_Raw;
        private System.Windows.Forms.ToolStripProgressBar PBar_Status;
        private MainFrameSingleCommandPanel SingleCmdPanel_Main;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_View_PacketViewAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private MainFrameCenter Panel_Center;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Edit;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Edit_EventClear;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_View_PacketViewRedraw;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_View_PacketConverterAdd;
        private System.Windows.Forms.ToolStripStatusLabel Label_PktCount_View;
        private System.Windows.Forms.ToolStripStatusLabel Label_ViewDrawMode;
        private System.Windows.Forms.ToolStripStatusLabel Label_PktCount_Busy;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Edit_TimeStamp;
    }
}