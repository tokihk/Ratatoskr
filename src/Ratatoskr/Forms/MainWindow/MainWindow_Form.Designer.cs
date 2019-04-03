namespace Ratatoskr.Forms.MainWindow
{
    partial class MainWindow_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow_Form));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Label_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.PBar_Status = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.DDBtn_DataRate = new System.Windows.Forms.ToolStripDropDownButton();
            this.Menu_DataRate_SendData = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_DataRate_RecvData = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.Label_PktCount_Cache = new System.Windows.Forms.ToolStripStatusLabel();
            this.Label_PktCount_Raw = new System.Windows.Forms.ToolStripStatusLabel();
            this.Label_PktCount_View = new System.Windows.Forms.ToolStripStatusLabel();
            this.Label_PktCount_Busy = new System.Windows.Forms.ToolStripStatusLabel();
            this.Label_ViewDrawMode = new System.Windows.Forms.ToolStripStatusLabel();
            this.Panel_Center = new Ratatoskr.Forms.MainWindow.MainWindow_FrameCenter();
            this.SingleCmdPanel_Main = new Ratatoskr.Forms.MainWindow.MainWindow_SendPanelContainer();
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
            this.MenuBar_Gate = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Gate_Connect1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Gate_Connect2 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Gate_Connect3 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Gate_Connect4 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Gate_Connect5 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Tool = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Edit_TimeStamp = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Tool_AutoTimeStamp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuBar_Tool_ScriptManager = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuBar_Tool_Option = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Help_Document = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuBar_Help_About = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Profile_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Profile_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_ProfileList = new System.Windows.Forms.ToolStripComboBox();
            this.MenuBar_Profile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Profile_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuBar_Profile_Export = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(944, 506);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(944, 562);
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
            this.toolStripStatusLabel2,
            this.DDBtn_DataRate,
            this.toolStripStatusLabel1,
            this.Label_PktCount_Cache,
            this.Label_PktCount_Raw,
            this.Label_PktCount_View,
            this.Label_PktCount_Busy,
            this.Label_ViewDrawMode});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(944, 27);
            this.statusStrip1.TabIndex = 0;
            // 
            // Label_Status
            // 
            this.Label_Status.Name = "Label_Status";
            this.Label_Status.Size = new System.Drawing.Size(74, 22);
            this.Label_Status.Spring = true;
            this.Label_Status.Text = "起動完了";
            this.Label_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PBar_Status
            // 
            this.PBar_Status.Name = "PBar_Status";
            this.PBar_Status.Size = new System.Drawing.Size(200, 21);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(4, 22);
            // 
            // DDBtn_DataRate
            // 
            this.DDBtn_DataRate.AutoSize = false;
            this.DDBtn_DataRate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DDBtn_DataRate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_DataRate_SendData,
            this.Menu_DataRate_RecvData});
            this.DDBtn_DataRate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DDBtn_DataRate.Image = ((System.Drawing.Image)(resources.GetObject("DDBtn_DataRate.Image")));
            this.DDBtn_DataRate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DDBtn_DataRate.Name = "DDBtn_DataRate";
            this.DDBtn_DataRate.Size = new System.Drawing.Size(125, 25);
            this.DDBtn_DataRate.Text = "Rate: 99.999MB/s";
            // 
            // Menu_DataRate_SendData
            // 
            this.Menu_DataRate_SendData.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Menu_DataRate_SendData.Name = "Menu_DataRate_SendData";
            this.Menu_DataRate_SendData.Size = new System.Drawing.Size(148, 22);
            this.Menu_DataRate_SendData.Text = "Send data";
            this.Menu_DataRate_SendData.Click += new System.EventHandler(this.OnDataRateTargetUpdate);
            // 
            // Menu_DataRate_RecvData
            // 
            this.Menu_DataRate_RecvData.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Menu_DataRate_RecvData.Name = "Menu_DataRate_RecvData";
            this.Menu_DataRate_RecvData.Size = new System.Drawing.Size(148, 22);
            this.Menu_DataRate_RecvData.Text = "Receive data";
            this.Menu_DataRate_RecvData.Click += new System.EventHandler(this.OnDataRateTargetUpdate);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(4, 22);
            // 
            // Label_PktCount_Cache
            // 
            this.Label_PktCount_Cache.AutoSize = false;
            this.Label_PktCount_Cache.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.Label_PktCount_Cache.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_PktCount_Cache.Name = "Label_PktCount_Cache";
            this.Label_PktCount_Cache.Size = new System.Drawing.Size(115, 22);
            this.Label_PktCount_Cache.Text = "Cache: 999999999";
            // 
            // Label_PktCount_Raw
            // 
            this.Label_PktCount_Raw.AutoSize = false;
            this.Label_PktCount_Raw.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.Label_PktCount_Raw.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_PktCount_Raw.Name = "Label_PktCount_Raw";
            this.Label_PktCount_Raw.Size = new System.Drawing.Size(115, 22);
            this.Label_PktCount_Raw.Text = "Raw: 999999999";
            // 
            // Label_PktCount_View
            // 
            this.Label_PktCount_View.AutoSize = false;
            this.Label_PktCount_View.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.Label_PktCount_View.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_PktCount_View.Name = "Label_PktCount_View";
            this.Label_PktCount_View.Size = new System.Drawing.Size(115, 22);
            this.Label_PktCount_View.Text = "View: 999999999";
            // 
            // Label_PktCount_Busy
            // 
            this.Label_PktCount_Busy.AutoSize = false;
            this.Label_PktCount_Busy.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.Label_PktCount_Busy.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_PktCount_Busy.Name = "Label_PktCount_Busy";
            this.Label_PktCount_Busy.Size = new System.Drawing.Size(115, 22);
            this.Label_PktCount_Busy.Text = "Busy: 999999999";
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
            this.Label_ViewDrawMode.Size = new System.Drawing.Size(60, 22);
            this.Label_ViewDrawMode.Text = "High";
            this.Label_ViewDrawMode.DoubleClick += new System.EventHandler(this.Label_ViewDrawMode_DoubleClick);
            // 
            // Panel_Center
            // 
            this.Panel_Center.BackColor = System.Drawing.SystemColors.Control;
            this.Panel_Center.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Center.Location = new System.Drawing.Point(0, 0);
            this.Panel_Center.Name = "Panel_Center";
            this.Panel_Center.Size = new System.Drawing.Size(944, 462);
            this.Panel_Center.TabIndex = 3;
            // 
            // SingleCmdPanel_Main
            // 
            this.SingleCmdPanel_Main.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.SingleCmdPanel_Main.Location = new System.Drawing.Point(0, 462);
            this.SingleCmdPanel_Main.MaximumSize = new System.Drawing.Size(4096, 44);
            this.SingleCmdPanel_Main.MinimumSize = new System.Drawing.Size(0, 44);
            this.SingleCmdPanel_Main.Name = "SingleCmdPanel_Main";
            this.SingleCmdPanel_Main.Size = new System.Drawing.Size(944, 44);
            this.SingleCmdPanel_Main.TabIndex = 2;
            // 
            // MBar_Main
            // 
            this.MBar_Main.Dock = System.Windows.Forms.DockStyle.None;
            this.MBar_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_File,
            this.MenuBar_Edit,
            this.MenuBar_View,
            this.MenuBar_Gate,
            this.MenuBar_Tool,
            this.MenuBar_Help,
            this.MenuBar_Profile_Remove,
            this.MenuBar_Profile_Add,
            this.MenuBar_ProfileList,
            this.MenuBar_Profile});
            this.MBar_Main.Location = new System.Drawing.Point(0, 0);
            this.MBar_Main.Name = "MBar_Main";
            this.MBar_Main.Size = new System.Drawing.Size(944, 29);
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
            this.MenuBar_File.Size = new System.Drawing.Size(36, 25);
            this.MenuBar_File.Text = "File";
            // 
            // MenuBar_File_Open
            // 
            this.MenuBar_File_Open.Name = "MenuBar_File_Open";
            this.MenuBar_File_Open.Size = new System.Drawing.Size(111, 22);
            this.MenuBar_File_Open.Tag = "FileOpen";
            this.MenuBar_File_Open.Text = "Open";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(108, 6);
            // 
            // MenuBar_File_Save
            // 
            this.MenuBar_File_Save.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_File_Save_Original,
            this.MenuBar_File_Save_Shaping});
            this.MenuBar_File_Save.Name = "MenuBar_File_Save";
            this.MenuBar_File_Save.Size = new System.Drawing.Size(111, 22);
            this.MenuBar_File_Save.Text = "Save";
            this.MenuBar_File_Save.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // MenuBar_File_Save_Original
            // 
            this.MenuBar_File_Save_Original.Name = "MenuBar_File_Save_Original";
            this.MenuBar_File_Save_Original.Size = new System.Drawing.Size(136, 22);
            this.MenuBar_File_Save_Original.Tag = "PacketSaveConvertOff";
            this.MenuBar_File_Save_Original.Text = "Raw packets";
            // 
            // MenuBar_File_Save_Shaping
            // 
            this.MenuBar_File_Save_Shaping.Name = "MenuBar_File_Save_Shaping";
            this.MenuBar_File_Save_Shaping.Size = new System.Drawing.Size(136, 22);
            this.MenuBar_File_Save_Shaping.Tag = "PacketSaveConvertOn";
            this.MenuBar_File_Save_Shaping.Text = "View packet";
            // 
            // MenuBar_File_SaveAs
            // 
            this.MenuBar_File_SaveAs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_File_SaveAs_Original,
            this.MenuBar_File_SaveAs_Shaping});
            this.MenuBar_File_SaveAs.Name = "MenuBar_File_SaveAs";
            this.MenuBar_File_SaveAs.Size = new System.Drawing.Size(111, 22);
            this.MenuBar_File_SaveAs.Text = "Save as";
            // 
            // MenuBar_File_SaveAs_Original
            // 
            this.MenuBar_File_SaveAs_Original.Name = "MenuBar_File_SaveAs_Original";
            this.MenuBar_File_SaveAs_Original.Size = new System.Drawing.Size(145, 22);
            this.MenuBar_File_SaveAs_Original.Tag = "PacketSaveAsConvertOff";
            this.MenuBar_File_SaveAs_Original.Text = "Raw packets...";
            // 
            // MenuBar_File_SaveAs_Shaping
            // 
            this.MenuBar_File_SaveAs_Shaping.Name = "MenuBar_File_SaveAs_Shaping";
            this.MenuBar_File_SaveAs_Shaping.Size = new System.Drawing.Size(145, 22);
            this.MenuBar_File_SaveAs_Shaping.Tag = "PacketSaveAsConvertOn";
            this.MenuBar_File_SaveAs_Shaping.Text = "View packets...";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(108, 6);
            // 
            // MenuBar_File_Exit
            // 
            this.MenuBar_File_Exit.Name = "MenuBar_File_Exit";
            this.MenuBar_File_Exit.Size = new System.Drawing.Size(111, 22);
            this.MenuBar_File_Exit.Tag = "ApplicationExit";
            this.MenuBar_File_Exit.Text = "Exit";
            // 
            // MenuBar_Edit
            // 
            this.MenuBar_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_Edit_EventClear});
            this.MenuBar_Edit.Name = "MenuBar_Edit";
            this.MenuBar_Edit.Size = new System.Drawing.Size(37, 25);
            this.MenuBar_Edit.Text = "Edit";
            // 
            // MenuBar_Edit_EventClear
            // 
            this.MenuBar_Edit_EventClear.Name = "MenuBar_Edit_EventClear";
            this.MenuBar_Edit_EventClear.Size = new System.Drawing.Size(131, 22);
            this.MenuBar_Edit_EventClear.Tag = "PacketClear";
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
            this.MenuBar_View.Size = new System.Drawing.Size(42, 25);
            this.MenuBar_View.Text = "View";
            // 
            // MenuBar_View_PacketConverterAdd
            // 
            this.MenuBar_View_PacketConverterAdd.Name = "MenuBar_View_PacketConverterAdd";
            this.MenuBar_View_PacketConverterAdd.Size = new System.Drawing.Size(155, 22);
            this.MenuBar_View_PacketConverterAdd.Text = "Add converter";
            // 
            // MenuBar_View_PacketViewAdd
            // 
            this.MenuBar_View_PacketViewAdd.Name = "MenuBar_View_PacketViewAdd";
            this.MenuBar_View_PacketViewAdd.Size = new System.Drawing.Size(155, 22);
            this.MenuBar_View_PacketViewAdd.Text = "Add packet view";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(152, 6);
            // 
            // MenuBar_View_PacketViewRedraw
            // 
            this.MenuBar_View_PacketViewRedraw.Name = "MenuBar_View_PacketViewRedraw";
            this.MenuBar_View_PacketViewRedraw.Size = new System.Drawing.Size(155, 22);
            this.MenuBar_View_PacketViewRedraw.Tag = "PacketRedraw";
            this.MenuBar_View_PacketViewRedraw.Text = "Redraw";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(152, 6);
            // 
            // MenuBar_View_AutoScroll
            // 
            this.MenuBar_View_AutoScroll.Name = "MenuBar_View_AutoScroll";
            this.MenuBar_View_AutoScroll.Size = new System.Drawing.Size(155, 22);
            this.MenuBar_View_AutoScroll.Tag = "AutoScrollToggle";
            this.MenuBar_View_AutoScroll.Text = "Auto scroll";
            // 
            // MenuBar_Gate
            // 
            this.MenuBar_Gate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_Gate_Connect1,
            this.MenuBar_Gate_Connect2,
            this.MenuBar_Gate_Connect3,
            this.MenuBar_Gate_Connect4,
            this.MenuBar_Gate_Connect5});
            this.MenuBar_Gate.Name = "MenuBar_Gate";
            this.MenuBar_Gate.Size = new System.Drawing.Size(41, 25);
            this.MenuBar_Gate.Text = "Gate";
            // 
            // MenuBar_Gate_Connect1
            // 
            this.MenuBar_Gate_Connect1.Name = "MenuBar_Gate_Connect1";
            this.MenuBar_Gate_Connect1.Size = new System.Drawing.Size(209, 22);
            this.MenuBar_Gate_Connect1.Tag = "Gate1_Connect";
            this.MenuBar_Gate_Connect1.Text = "Gate1 Connect/Disconnect";
            // 
            // MenuBar_Gate_Connect2
            // 
            this.MenuBar_Gate_Connect2.Name = "MenuBar_Gate_Connect2";
            this.MenuBar_Gate_Connect2.Size = new System.Drawing.Size(209, 22);
            this.MenuBar_Gate_Connect2.Tag = "Gate2_Connect";
            this.MenuBar_Gate_Connect2.Text = "Gate2 Connect/Disconnect";
            // 
            // MenuBar_Gate_Connect3
            // 
            this.MenuBar_Gate_Connect3.Name = "MenuBar_Gate_Connect3";
            this.MenuBar_Gate_Connect3.Size = new System.Drawing.Size(209, 22);
            this.MenuBar_Gate_Connect3.Tag = "Gate3_Connect";
            this.MenuBar_Gate_Connect3.Text = "Gate3 Connect/Disconnect";
            // 
            // MenuBar_Gate_Connect4
            // 
            this.MenuBar_Gate_Connect4.Name = "MenuBar_Gate_Connect4";
            this.MenuBar_Gate_Connect4.Size = new System.Drawing.Size(209, 22);
            this.MenuBar_Gate_Connect4.Tag = "Gate4_Connect";
            this.MenuBar_Gate_Connect4.Text = "Gate4 Connect/Disconnect";
            // 
            // MenuBar_Gate_Connect5
            // 
            this.MenuBar_Gate_Connect5.Name = "MenuBar_Gate_Connect5";
            this.MenuBar_Gate_Connect5.Size = new System.Drawing.Size(209, 22);
            this.MenuBar_Gate_Connect5.Tag = "Gate5_Connect";
            this.MenuBar_Gate_Connect5.Text = "Gate5 Connect/Disconnect";
            // 
            // MenuBar_Tool
            // 
            this.MenuBar_Tool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_Edit_TimeStamp,
            this.MenuBar_Tool_AutoTimeStamp,
            this.toolStripSeparator11,
            this.MenuBar_Tool_ScriptManager,
            this.toolStripSeparator7,
            this.MenuBar_Tool_Option});
            this.MenuBar_Tool.Name = "MenuBar_Tool";
            this.MenuBar_Tool.Size = new System.Drawing.Size(39, 25);
            this.MenuBar_Tool.Text = "Tool";
            // 
            // MenuBar_Edit_TimeStamp
            // 
            this.MenuBar_Edit_TimeStamp.Name = "MenuBar_Edit_TimeStamp";
            this.MenuBar_Edit_TimeStamp.Size = new System.Drawing.Size(158, 22);
            this.MenuBar_Edit_TimeStamp.Tag = "TimeStamp";
            this.MenuBar_Edit_TimeStamp.Text = "Time stamp";
            // 
            // MenuBar_Tool_AutoTimeStamp
            // 
            this.MenuBar_Tool_AutoTimeStamp.Name = "MenuBar_Tool_AutoTimeStamp";
            this.MenuBar_Tool_AutoTimeStamp.Size = new System.Drawing.Size(158, 22);
            this.MenuBar_Tool_AutoTimeStamp.Tag = "AutoTimeStampToggle";
            this.MenuBar_Tool_AutoTimeStamp.Text = "Auto Time stamp";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(155, 6);
            // 
            // MenuBar_Tool_ScriptManager
            // 
            this.MenuBar_Tool_ScriptManager.Name = "MenuBar_Tool_ScriptManager";
            this.MenuBar_Tool_ScriptManager.Size = new System.Drawing.Size(158, 22);
            this.MenuBar_Tool_ScriptManager.Tag = "ShowScriptWindow";
            this.MenuBar_Tool_ScriptManager.Text = "Script Manager...";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(155, 6);
            // 
            // MenuBar_Tool_Option
            // 
            this.MenuBar_Tool_Option.Name = "MenuBar_Tool_Option";
            this.MenuBar_Tool_Option.Size = new System.Drawing.Size(158, 22);
            this.MenuBar_Tool_Option.Tag = "ShowOptionDialog";
            this.MenuBar_Tool_Option.Text = "Options...";
            // 
            // MenuBar_Help
            // 
            this.MenuBar_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_Help_Document,
            this.toolStripSeparator10,
            this.MenuBar_Help_About});
            this.MenuBar_Help.Name = "MenuBar_Help";
            this.MenuBar_Help.Size = new System.Drawing.Size(40, 25);
            this.MenuBar_Help.Text = "Help";
            // 
            // MenuBar_Help_Document
            // 
            this.MenuBar_Help_Document.Name = "MenuBar_Help_Document";
            this.MenuBar_Help_Document.Size = new System.Drawing.Size(180, 22);
            this.MenuBar_Help_Document.Tag = "ShowAppDocument";
            this.MenuBar_Help_Document.Text = "Contents";
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(177, 6);
            // 
            // MenuBar_Help_About
            // 
            this.MenuBar_Help_About.Name = "MenuBar_Help_About";
            this.MenuBar_Help_About.Size = new System.Drawing.Size(180, 22);
            this.MenuBar_Help_About.Tag = "ShowAppInformation";
            this.MenuBar_Help_About.Text = "About Application";
            // 
            // MenuBar_Profile_Remove
            // 
            this.MenuBar_Profile_Remove.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.MenuBar_Profile_Remove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuBar_Profile_Remove.Image = RtsCore.Resource.Images.database_remove_32x32;
            this.MenuBar_Profile_Remove.Name = "MenuBar_Profile_Remove";
            this.MenuBar_Profile_Remove.Size = new System.Drawing.Size(28, 25);
            this.MenuBar_Profile_Remove.Tag = "ProfileRemove";
            this.MenuBar_Profile_Remove.Text = "toolStripMenuItem1";
            this.MenuBar_Profile_Remove.ToolTipText = "Current profile remove";
            // 
            // MenuBar_Profile_Add
            // 
            this.MenuBar_Profile_Add.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.MenuBar_Profile_Add.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuBar_Profile_Add.Image = RtsCore.Resource.Images.database_add_32x32;
            this.MenuBar_Profile_Add.Name = "MenuBar_Profile_Add";
            this.MenuBar_Profile_Add.Size = new System.Drawing.Size(28, 25);
            this.MenuBar_Profile_Add.Tag = "ProfileAdd";
            this.MenuBar_Profile_Add.Text = "toolStripMenuItem1";
            this.MenuBar_Profile_Add.ToolTipText = "New profile add";
            // 
            // MenuBar_ProfileList
            // 
            this.MenuBar_ProfileList.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.MenuBar_ProfileList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MenuBar_ProfileList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MenuBar_ProfileList.Font = new System.Drawing.Font("メイリオ", 8F);
            this.MenuBar_ProfileList.Name = "MenuBar_ProfileList";
            this.MenuBar_ProfileList.Size = new System.Drawing.Size(180, 25);
            this.MenuBar_ProfileList.DropDown += new System.EventHandler(this.MenuBar_ProfileList_DropDown);
            this.MenuBar_ProfileList.SelectedIndexChanged += new System.EventHandler(this.MenuBar_ProfileList_SelectedIndexChanged);
            // 
            // MenuBar_Profile
            // 
            this.MenuBar_Profile.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.MenuBar_Profile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MenuBar_Profile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_Profile_Edit,
            this.toolStripSeparator5,
            this.MenuBar_Profile_Export});
            this.MenuBar_Profile.Image = RtsCore.Resource.Images.user_24x24;
            this.MenuBar_Profile.Name = "MenuBar_Profile";
            this.MenuBar_Profile.Size = new System.Drawing.Size(28, 25);
            this.MenuBar_Profile.Text = "Profile";
            // 
            // MenuBar_Profile_Edit
            // 
            this.MenuBar_Profile_Edit.Name = "MenuBar_Profile_Edit";
            this.MenuBar_Profile_Edit.Size = new System.Drawing.Size(145, 22);
            this.MenuBar_Profile_Edit.Tag = "ProfileEdit";
            this.MenuBar_Profile_Edit.Text = "Edit profile...";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(142, 6);
            // 
            // MenuBar_Profile_Export
            // 
            this.MenuBar_Profile_Export.Name = "MenuBar_Profile_Export";
            this.MenuBar_Profile_Export.Size = new System.Drawing.Size(145, 22);
            this.MenuBar_Profile_Export.Tag = "ProfileExport";
            this.MenuBar_Profile_Export.Text = "Export profile...";
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
            // MainWindow_Form
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 562);
            this.Controls.Add(this.toolStripContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.MBar_Main;
            this.MinimumSize = new System.Drawing.Size(480, 320);
            this.Name = "MainWindow_Form";
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
        private MainWindow_SendPanelContainer SingleCmdPanel_Main;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_View_PacketViewAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private MainWindow_FrameCenter Panel_Center;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Edit;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Edit_EventClear;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_View_PacketViewRedraw;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_View_PacketConverterAdd;
        private System.Windows.Forms.ToolStripStatusLabel Label_PktCount_View;
        private System.Windows.Forms.ToolStripStatusLabel Label_ViewDrawMode;
        private System.Windows.Forms.ToolStripStatusLabel Label_PktCount_Busy;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Edit_TimeStamp;
        private System.Windows.Forms.ToolStripDropDownButton DDBtn_DataRate;
        private System.Windows.Forms.ToolStripMenuItem Menu_DataRate_SendData;
        private System.Windows.Forms.ToolStripMenuItem Menu_DataRate_RecvData;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Profile;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Profile_Edit;
        private System.Windows.Forms.ToolStripComboBox MenuBar_ProfileList;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Profile_Export;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Tool_AutoTimeStamp;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Help_Document;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Tool_ScriptManager;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Profile_Remove;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Profile_Add;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Gate;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Gate_Connect1;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Gate_Connect2;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Gate_Connect3;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Gate_Connect4;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Gate_Connect5;
        private System.Windows.Forms.ToolStripStatusLabel Label_PktCount_Cache;
    }
}