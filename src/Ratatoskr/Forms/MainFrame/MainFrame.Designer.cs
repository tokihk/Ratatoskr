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
            this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_File_OpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuBar_File_SavePacket = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_File_SavePacket_FilterOff = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_File_SavePacket_FilterOn = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_File_SaveAsPacket = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_File_SaveAsPacket_FilterOff = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_File_SaveAsPacket_FilterOn = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_File_SavePacket_AutoSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuBar_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.編集EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Edit_TimeStamp = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Edit_TimeStamp_Run = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuBar_Edit_TimeStamp_Auto = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuBar_Edit_EventClear = new System.Windows.Forms.ToolStripMenuItem();
            this.表示VToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_View_PacketConverterAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_View_PacketViewAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuBar_View_PacketViewRedraw = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuBar_View_AutoScroll = new System.Windows.Forms.ToolStripMenuItem();
            this.ツールTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Tool_Option = new System.Windows.Forms.ToolStripMenuItem();
            this.ヘルプHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuBar_Help_Information = new System.Windows.Forms.ToolStripMenuItem();
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
            this.ファイルFToolStripMenuItem,
            this.編集EToolStripMenuItem,
            this.表示VToolStripMenuItem,
            this.ツールTToolStripMenuItem,
            this.ヘルプHToolStripMenuItem});
            this.MBar_Main.Location = new System.Drawing.Point(0, 0);
            this.MBar_Main.Name = "MBar_Main";
            this.MBar_Main.Size = new System.Drawing.Size(784, 24);
            this.MBar_Main.TabIndex = 0;
            this.MBar_Main.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            this.ファイルFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_File_OpenFile,
            this.toolStripSeparator2,
            this.MenuBar_File_SavePacket,
            this.MenuBar_File_SaveAsPacket,
            this.MenuBar_File_SavePacket_AutoSave,
            this.toolStripSeparator1,
            this.MenuBar_File_Exit});
            this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // MenuBar_File_OpenFile
            // 
            this.MenuBar_File_OpenFile.Name = "MenuBar_File_OpenFile";
            this.MenuBar_File_OpenFile.Size = new System.Drawing.Size(223, 22);
            this.MenuBar_File_OpenFile.Tag = "FileOpen";
            this.MenuBar_File_OpenFile.Text = "ファイルを開く(&O)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(220, 6);
            // 
            // MenuBar_File_SavePacket
            // 
            this.MenuBar_File_SavePacket.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_File_SavePacket_FilterOff,
            this.MenuBar_File_SavePacket_FilterOn});
            this.MenuBar_File_SavePacket.Name = "MenuBar_File_SavePacket";
            this.MenuBar_File_SavePacket.Size = new System.Drawing.Size(223, 22);
            this.MenuBar_File_SavePacket.Text = "パケットログを保存";
            this.MenuBar_File_SavePacket.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // MenuBar_File_SavePacket_FilterOff
            // 
            this.MenuBar_File_SavePacket_FilterOff.Name = "MenuBar_File_SavePacket_FilterOff";
            this.MenuBar_File_SavePacket_FilterOff.Size = new System.Drawing.Size(151, 22);
            this.MenuBar_File_SavePacket_FilterOff.Tag = "PacketSaveRuleOff";
            this.MenuBar_File_SavePacket_FilterOff.Text = "パケット制御なし";
            // 
            // MenuBar_File_SavePacket_FilterOn
            // 
            this.MenuBar_File_SavePacket_FilterOn.Name = "MenuBar_File_SavePacket_FilterOn";
            this.MenuBar_File_SavePacket_FilterOn.Size = new System.Drawing.Size(151, 22);
            this.MenuBar_File_SavePacket_FilterOn.Tag = "PacketSaveRuleOn";
            this.MenuBar_File_SavePacket_FilterOn.Text = "パケット制御あり";
            // 
            // MenuBar_File_SaveAsPacket
            // 
            this.MenuBar_File_SaveAsPacket.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_File_SaveAsPacket_FilterOff,
            this.MenuBar_File_SaveAsPacket_FilterOn});
            this.MenuBar_File_SaveAsPacket.Name = "MenuBar_File_SaveAsPacket";
            this.MenuBar_File_SaveAsPacket.Size = new System.Drawing.Size(223, 22);
            this.MenuBar_File_SaveAsPacket.Text = "名前を付けてパケットログを保存";
            // 
            // MenuBar_File_SaveAsPacket_FilterOff
            // 
            this.MenuBar_File_SaveAsPacket_FilterOff.Name = "MenuBar_File_SaveAsPacket_FilterOff";
            this.MenuBar_File_SaveAsPacket_FilterOff.Size = new System.Drawing.Size(151, 22);
            this.MenuBar_File_SaveAsPacket_FilterOff.Tag = "PacketSaveAsRuleOff";
            this.MenuBar_File_SaveAsPacket_FilterOff.Text = "パケット制御なし";
            // 
            // MenuBar_File_SaveAsPacket_FilterOn
            // 
            this.MenuBar_File_SaveAsPacket_FilterOn.Name = "MenuBar_File_SaveAsPacket_FilterOn";
            this.MenuBar_File_SaveAsPacket_FilterOn.Size = new System.Drawing.Size(151, 22);
            this.MenuBar_File_SaveAsPacket_FilterOn.Tag = "PacketSaveAsRuleOn";
            this.MenuBar_File_SaveAsPacket_FilterOn.Text = "パケット制御あり";
            // 
            // MenuBar_File_SavePacket_AutoSave
            // 
            this.MenuBar_File_SavePacket_AutoSave.Name = "MenuBar_File_SavePacket_AutoSave";
            this.MenuBar_File_SavePacket_AutoSave.Size = new System.Drawing.Size(223, 22);
            this.MenuBar_File_SavePacket_AutoSave.Tag = "AutoSaveToggle";
            this.MenuBar_File_SavePacket_AutoSave.Text = "パケットログを自動保存(&A)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(220, 6);
            // 
            // MenuBar_File_Exit
            // 
            this.MenuBar_File_Exit.Name = "MenuBar_File_Exit";
            this.MenuBar_File_Exit.Size = new System.Drawing.Size(223, 22);
            this.MenuBar_File_Exit.Tag = "ApplicationExit";
            this.MenuBar_File_Exit.Text = "終了(&X)";
            // 
            // 編集EToolStripMenuItem
            // 
            this.編集EToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_Edit_TimeStamp,
            this.toolStripSeparator5,
            this.MenuBar_Edit_EventClear});
            this.編集EToolStripMenuItem.Name = "編集EToolStripMenuItem";
            this.編集EToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.編集EToolStripMenuItem.Text = "編集(&E)";
            // 
            // MenuBar_Edit_TimeStamp
            // 
            this.MenuBar_Edit_TimeStamp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_Edit_TimeStamp_Run,
            this.toolStripSeparator6,
            this.MenuBar_Edit_TimeStamp_Auto});
            this.MenuBar_Edit_TimeStamp.Name = "MenuBar_Edit_TimeStamp";
            this.MenuBar_Edit_TimeStamp.Size = new System.Drawing.Size(151, 22);
            this.MenuBar_Edit_TimeStamp.Text = "タイムスタンプ(&T)";
            // 
            // MenuBar_Edit_TimeStamp_Run
            // 
            this.MenuBar_Edit_TimeStamp_Run.Name = "MenuBar_Edit_TimeStamp_Run";
            this.MenuBar_Edit_TimeStamp_Run.Size = new System.Drawing.Size(140, 22);
            this.MenuBar_Edit_TimeStamp_Run.Tag = "TimeStampRun";
            this.MenuBar_Edit_TimeStamp_Run.Text = "すぐに実行(&R)";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(137, 6);
            // 
            // MenuBar_Edit_TimeStamp_Auto
            // 
            this.MenuBar_Edit_TimeStamp_Auto.Name = "MenuBar_Edit_TimeStamp_Auto";
            this.MenuBar_Edit_TimeStamp_Auto.Size = new System.Drawing.Size(140, 22);
            this.MenuBar_Edit_TimeStamp_Auto.Tag = "AutoTimeStampToggle";
            this.MenuBar_Edit_TimeStamp_Auto.Text = "自動挿入(&A)";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(148, 6);
            // 
            // MenuBar_Edit_EventClear
            // 
            this.MenuBar_Edit_EventClear.Name = "MenuBar_Edit_EventClear";
            this.MenuBar_Edit_EventClear.Size = new System.Drawing.Size(151, 22);
            this.MenuBar_Edit_EventClear.Tag = "EventPacketClear";
            this.MenuBar_Edit_EventClear.Text = "バッファクリア(&C)";
            // 
            // 表示VToolStripMenuItem
            // 
            this.表示VToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_View_PacketConverterAdd,
            this.MenuBar_View_PacketViewAdd,
            this.toolStripSeparator3,
            this.MenuBar_View_PacketViewRedraw,
            this.toolStripSeparator4,
            this.MenuBar_View_AutoScroll});
            this.表示VToolStripMenuItem.Name = "表示VToolStripMenuItem";
            this.表示VToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.表示VToolStripMenuItem.Text = "表示(&V)";
            // 
            // MenuBar_View_PacketConverterAdd
            // 
            this.MenuBar_View_PacketConverterAdd.Name = "MenuBar_View_PacketConverterAdd";
            this.MenuBar_View_PacketConverterAdd.Size = new System.Drawing.Size(193, 22);
            this.MenuBar_View_PacketConverterAdd.Text = "パケット制御を追加(&C)";
            // 
            // MenuBar_View_PacketViewAdd
            // 
            this.MenuBar_View_PacketViewAdd.Name = "MenuBar_View_PacketViewAdd";
            this.MenuBar_View_PacketViewAdd.Size = new System.Drawing.Size(193, 22);
            this.MenuBar_View_PacketViewAdd.Text = "パケットビューを追加(&V)";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(190, 6);
            // 
            // MenuBar_View_PacketViewRedraw
            // 
            this.MenuBar_View_PacketViewRedraw.Name = "MenuBar_View_PacketViewRedraw";
            this.MenuBar_View_PacketViewRedraw.Size = new System.Drawing.Size(193, 22);
            this.MenuBar_View_PacketViewRedraw.Tag = "EventPacketRedraw";
            this.MenuBar_View_PacketViewRedraw.Text = "パケットビューを再描画(&R)";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(190, 6);
            // 
            // MenuBar_View_AutoScroll
            // 
            this.MenuBar_View_AutoScroll.Name = "MenuBar_View_AutoScroll";
            this.MenuBar_View_AutoScroll.Size = new System.Drawing.Size(193, 22);
            this.MenuBar_View_AutoScroll.Tag = "AutoScrollToggle";
            this.MenuBar_View_AutoScroll.Text = "自動スクロール(&S)";
            // 
            // ツールTToolStripMenuItem
            // 
            this.ツールTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_Tool_Option});
            this.ツールTToolStripMenuItem.Name = "ツールTToolStripMenuItem";
            this.ツールTToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.ツールTToolStripMenuItem.Text = "ツール(&T)";
            // 
            // MenuBar_Tool_Option
            // 
            this.MenuBar_Tool_Option.Name = "MenuBar_Tool_Option";
            this.MenuBar_Tool_Option.Size = new System.Drawing.Size(135, 22);
            this.MenuBar_Tool_Option.Tag = "ShowConfigDialog";
            this.MenuBar_Tool_Option.Text = "オプション(&O)";
            // 
            // ヘルプHToolStripMenuItem
            // 
            this.ヘルプHToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuBar_Help_Information});
            this.ヘルプHToolStripMenuItem.Name = "ヘルプHToolStripMenuItem";
            this.ヘルプHToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.ヘルプHToolStripMenuItem.Text = "ヘルプ(H)";
            // 
            // MenuBar_Help_Information
            // 
            this.MenuBar_Help_Information.Name = "MenuBar_Help_Information";
            this.MenuBar_Help_Information.Size = new System.Drawing.Size(158, 22);
            this.MenuBar_Help_Information.Tag = "ShowAppInformation";
            this.MenuBar_Help_Information.Text = "バージョン情報(&A)";
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
        private System.Windows.Forms.ToolStripMenuItem ファイルFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_File_Exit;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_File_SavePacket;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_File_OpenFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_File_SavePacket_FilterOff;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_File_SavePacket_FilterOn;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_File_SaveAsPacket;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_File_SaveAsPacket_FilterOff;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_File_SaveAsPacket_FilterOn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 表示VToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_View_AutoScroll;
        private System.Windows.Forms.ToolStripMenuItem ヘルプHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Help_Information;
        private System.Windows.Forms.ToolStripMenuItem ツールTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Tool_Option;
        private System.Windows.Forms.ToolStripStatusLabel Label_Status;
        private System.Windows.Forms.ToolStripStatusLabel Label_PktCount_Raw;
        private System.Windows.Forms.ToolStripProgressBar PBar_Status;
        private MainFrameSingleCommandPanel SingleCmdPanel_Main;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_View_PacketViewAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private MainFrameCenter Panel_Center;
        private System.Windows.Forms.ToolStripMenuItem 編集EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Edit_EventClear;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Edit_TimeStamp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Edit_TimeStamp_Run;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_Edit_TimeStamp_Auto;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_View_PacketViewRedraw;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_View_PacketConverterAdd;
        private System.Windows.Forms.ToolStripStatusLabel Label_PktCount_View;
        private System.Windows.Forms.ToolStripStatusLabel Label_ViewDrawMode;
        private System.Windows.Forms.ToolStripStatusLabel Label_PktCount_Busy;
        private System.Windows.Forms.ToolStripMenuItem MenuBar_File_SavePacket_AutoSave;
    }
}