namespace Ratatoskr.Forms.Dialog
{
    partial class OpenFileSetupDialog
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Label_OpenListView_Color_PacketLog = new System.Windows.Forms.Label();
            this.Label_OpenListView_Color_UserConfig = new System.Windows.Forms.Label();
            this.LView_OpenFileList = new System.Windows.Forms.ListView();
            this.Btn_FileOrder_Down = new System.Windows.Forms.Button();
            this.Btn_FileOrder_Up = new System.Windows.Forms.Button();
            this.Btn_Ok = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.GBox_OpenFileSetting = new System.Windows.Forms.GroupBox();
            this.Btn_OpenFileSetting_Apply = new System.Windows.Forms.Button();
            this.GBox_FileFormatOption = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CBox_FileFormatType = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.GBox_OpenFileSetting.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Label_OpenListView_Color_PacketLog);
            this.groupBox1.Controls.Add(this.Label_OpenListView_Color_UserConfig);
            this.groupBox1.Controls.Add(this.LView_OpenFileList);
            this.groupBox1.Controls.Add(this.Btn_FileOrder_Down);
            this.groupBox1.Controls.Add(this.Btn_FileOrder_Up);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(854, 357);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Open file list";
            // 
            // Label_OpenListView_Color_PacketLog
            // 
            this.Label_OpenListView_Color_PacketLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_OpenListView_Color_PacketLog.BackColor = System.Drawing.Color.Aquamarine;
            this.Label_OpenListView_Color_PacketLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label_OpenListView_Color_PacketLog.Font = new System.Drawing.Font("MS UI Gothic", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_OpenListView_Color_PacketLog.Location = new System.Drawing.Point(763, 323);
            this.Label_OpenListView_Color_PacketLog.Name = "Label_OpenListView_Color_PacketLog";
            this.Label_OpenListView_Color_PacketLog.Size = new System.Drawing.Size(85, 24);
            this.Label_OpenListView_Color_PacketLog.TabIndex = 4;
            this.Label_OpenListView_Color_PacketLog.Text = "Packet Log";
            this.Label_OpenListView_Color_PacketLog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_OpenListView_Color_UserConfig
            // 
            this.Label_OpenListView_Color_UserConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_OpenListView_Color_UserConfig.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.Label_OpenListView_Color_UserConfig.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label_OpenListView_Color_UserConfig.Font = new System.Drawing.Font("MS UI Gothic", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_OpenListView_Color_UserConfig.Location = new System.Drawing.Point(672, 323);
            this.Label_OpenListView_Color_UserConfig.Name = "Label_OpenListView_Color_UserConfig";
            this.Label_OpenListView_Color_UserConfig.Size = new System.Drawing.Size(85, 24);
            this.Label_OpenListView_Color_UserConfig.TabIndex = 3;
            this.Label_OpenListView_Color_UserConfig.Text = "User Config";
            this.Label_OpenListView_Color_UserConfig.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LView_OpenFileList
            // 
            this.LView_OpenFileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LView_OpenFileList.FullRowSelect = true;
            this.LView_OpenFileList.HideSelection = false;
            this.LView_OpenFileList.Location = new System.Drawing.Point(6, 50);
            this.LView_OpenFileList.Name = "LView_OpenFileList";
            this.LView_OpenFileList.Size = new System.Drawing.Size(842, 270);
            this.LView_OpenFileList.TabIndex = 2;
            this.LView_OpenFileList.UseCompatibleStateImageBehavior = false;
            this.LView_OpenFileList.View = System.Windows.Forms.View.Details;
            this.LView_OpenFileList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LView_OpenFileList_ColumnClick);
            this.LView_OpenFileList.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.LView_OpenFileList_ItemDrag);
            this.LView_OpenFileList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.LView_OpenFileList_ItemSelectionChanged);
            this.LView_OpenFileList.DragDrop += new System.Windows.Forms.DragEventHandler(this.LView_OpenFileList_DragDrop);
            this.LView_OpenFileList.DragEnter += new System.Windows.Forms.DragEventHandler(this.LView_OpenFileList_DragEnter);
            this.LView_OpenFileList.DragOver += new System.Windows.Forms.DragEventHandler(this.LView_OpenFileList_DragOver);
            this.LView_OpenFileList.DragLeave += new System.EventHandler(this.LView_OpenFileList_DragLeave);
            // 
            // Btn_FileOrder_Down
            // 
            this.Btn_FileOrder_Down.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_FileOrder_Down.Location = new System.Drawing.Point(817, 18);
            this.Btn_FileOrder_Down.Name = "Btn_FileOrder_Down";
            this.Btn_FileOrder_Down.Size = new System.Drawing.Size(31, 26);
            this.Btn_FileOrder_Down.TabIndex = 1;
            this.Btn_FileOrder_Down.UseVisualStyleBackColor = true;
            this.Btn_FileOrder_Down.Click += new System.EventHandler(this.Btn_FileOrder_Down_Click);
            // 
            // Btn_FileOrder_Up
            // 
            this.Btn_FileOrder_Up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_FileOrder_Up.Location = new System.Drawing.Point(780, 18);
            this.Btn_FileOrder_Up.Name = "Btn_FileOrder_Up";
            this.Btn_FileOrder_Up.Size = new System.Drawing.Size(31, 26);
            this.Btn_FileOrder_Up.TabIndex = 0;
            this.Btn_FileOrder_Up.UseVisualStyleBackColor = true;
            this.Btn_FileOrder_Up.Click += new System.EventHandler(this.Btn_FileOrder_Up_Click);
            // 
            // Btn_Ok
            // 
            this.Btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Ok.Location = new System.Drawing.Point(710, 747);
            this.Btn_Ok.Name = "Btn_Ok";
            this.Btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.Btn_Ok.TabIndex = 2;
            this.Btn_Ok.Text = "OK";
            this.Btn_Ok.UseVisualStyleBackColor = true;
            this.Btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Cancel.Location = new System.Drawing.Point(791, 747);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Btn_Cancel.TabIndex = 3;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.GBox_OpenFileSetting);
            this.splitContainer1.Size = new System.Drawing.Size(854, 729);
            this.splitContainer1.SplitterDistance = 357;
            this.splitContainer1.TabIndex = 4;
            // 
            // GBox_OpenFileSetting
            // 
            this.GBox_OpenFileSetting.Controls.Add(this.Btn_OpenFileSetting_Apply);
            this.GBox_OpenFileSetting.Controls.Add(this.GBox_FileFormatOption);
            this.GBox_OpenFileSetting.Controls.Add(this.groupBox3);
            this.GBox_OpenFileSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GBox_OpenFileSetting.Location = new System.Drawing.Point(0, 0);
            this.GBox_OpenFileSetting.Name = "GBox_OpenFileSetting";
            this.GBox_OpenFileSetting.Size = new System.Drawing.Size(854, 368);
            this.GBox_OpenFileSetting.TabIndex = 5;
            this.GBox_OpenFileSetting.TabStop = false;
            this.GBox_OpenFileSetting.Text = "Open file setting";
            // 
            // Btn_OpenFileSetting_Apply
            // 
            this.Btn_OpenFileSetting_Apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_OpenFileSetting_Apply.Location = new System.Drawing.Point(6, 339);
            this.Btn_OpenFileSetting_Apply.Name = "Btn_OpenFileSetting_Apply";
            this.Btn_OpenFileSetting_Apply.Size = new System.Drawing.Size(75, 23);
            this.Btn_OpenFileSetting_Apply.TabIndex = 2;
            this.Btn_OpenFileSetting_Apply.Text = "Apply";
            this.Btn_OpenFileSetting_Apply.UseVisualStyleBackColor = true;
            this.Btn_OpenFileSetting_Apply.Click += new System.EventHandler(this.Btn_OpenFileSetting_Apply_Click);
            // 
            // GBox_FileFormatOption
            // 
            this.GBox_FileFormatOption.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_FileFormatOption.Location = new System.Drawing.Point(6, 70);
            this.GBox_FileFormatOption.Name = "GBox_FileFormatOption";
            this.GBox_FileFormatOption.Size = new System.Drawing.Size(842, 263);
            this.GBox_FileFormatOption.TabIndex = 1;
            this.GBox_FileFormatOption.TabStop = false;
            this.GBox_FileFormatOption.Text = "File format option";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CBox_FileFormatType);
            this.groupBox3.Location = new System.Drawing.Point(6, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(240, 46);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "File format";
            // 
            // CBox_FileFormatType
            // 
            this.CBox_FileFormatType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_FileFormatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_FileFormatType.FormattingEnabled = true;
            this.CBox_FileFormatType.Location = new System.Drawing.Point(3, 15);
            this.CBox_FileFormatType.Name = "CBox_FileFormatType";
            this.CBox_FileFormatType.Size = new System.Drawing.Size(234, 20);
            this.CBox_FileFormatType.TabIndex = 0;
            this.CBox_FileFormatType.SelectedIndexChanged += new System.EventHandler(this.CBox_FileFormatType_SelectedIndexChanged);
            // 
            // OpenFileSetupDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 782);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_Ok);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenFileSetupDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select file format";
            this.groupBox1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.GBox_OpenFileSetting.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button Btn_FileOrder_Up;
		private System.Windows.Forms.Button Btn_Ok;
		private System.Windows.Forms.Button Btn_Cancel;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.GroupBox GBox_OpenFileSetting;
		private System.Windows.Forms.Button Btn_FileOrder_Down;
		private System.Windows.Forms.ListView LView_OpenFileList;
		private System.Windows.Forms.GroupBox GBox_FileFormatOption;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ComboBox CBox_FileFormatType;
		private System.Windows.Forms.Label Label_OpenListView_Color_PacketLog;
		private System.Windows.Forms.Label Label_OpenListView_Color_UserConfig;
		private System.Windows.Forms.Button Btn_OpenFileSetting_Apply;
	}
}