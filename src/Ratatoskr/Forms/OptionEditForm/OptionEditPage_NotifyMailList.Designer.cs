namespace Ratatoskr.Forms.OptionEditForm
{
    partial class OptionEditPage_NotifyMailList
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.GBox_SettingList = new System.Windows.Forms.GroupBox();
            this.Btn_Remove = new System.Windows.Forms.Button();
            this.LBox_SettingList = new System.Windows.Forms.ListBox();
            this.Btn_Add = new System.Windows.Forms.Button();
            this.Btn_Edit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Label_SettingDetails = new System.Windows.Forms.Label();
            this.GBox_SettingList.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBox_SettingList
            // 
            this.GBox_SettingList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_SettingList.Controls.Add(this.Btn_Remove);
            this.GBox_SettingList.Controls.Add(this.LBox_SettingList);
            this.GBox_SettingList.Controls.Add(this.Btn_Add);
            this.GBox_SettingList.Controls.Add(this.Btn_Edit);
            this.GBox_SettingList.Location = new System.Drawing.Point(3, 3);
            this.GBox_SettingList.Name = "GBox_SettingList";
            this.GBox_SettingList.Size = new System.Drawing.Size(494, 180);
            this.GBox_SettingList.TabIndex = 5;
            this.GBox_SettingList.TabStop = false;
            this.GBox_SettingList.Text = "Mail setting list";
            // 
            // Btn_Remove
            // 
            this.Btn_Remove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Remove.Location = new System.Drawing.Point(408, 141);
            this.Btn_Remove.Name = "Btn_Remove";
            this.Btn_Remove.Size = new System.Drawing.Size(80, 32);
            this.Btn_Remove.TabIndex = 8;
            this.Btn_Remove.Text = "Remove";
            this.Btn_Remove.UseVisualStyleBackColor = true;
            this.Btn_Remove.Click += new System.EventHandler(this.Btn_Remove_Click);
            // 
            // LBox_SettingList
            // 
            this.LBox_SettingList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBox_SettingList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LBox_SettingList.FormattingEnabled = true;
            this.LBox_SettingList.ItemHeight = 12;
            this.LBox_SettingList.Location = new System.Drawing.Point(6, 15);
            this.LBox_SettingList.Name = "LBox_SettingList";
            this.LBox_SettingList.Size = new System.Drawing.Size(396, 158);
            this.LBox_SettingList.TabIndex = 0;
            this.LBox_SettingList.SelectedIndexChanged += new System.EventHandler(this.LBox_SettingList_SelectedIndexChanged);
            this.LBox_SettingList.DoubleClick += new System.EventHandler(this.LBox_SettingList_DoubleClick);
            // 
            // Btn_Add
            // 
            this.Btn_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Add.Location = new System.Drawing.Point(408, 15);
            this.Btn_Add.Name = "Btn_Add";
            this.Btn_Add.Size = new System.Drawing.Size(80, 32);
            this.Btn_Add.TabIndex = 1;
            this.Btn_Add.Text = "Add";
            this.Btn_Add.UseVisualStyleBackColor = true;
            this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // Btn_Edit
            // 
            this.Btn_Edit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Edit.Location = new System.Drawing.Point(408, 53);
            this.Btn_Edit.Name = "Btn_Edit";
            this.Btn_Edit.Size = new System.Drawing.Size(80, 32);
            this.Btn_Edit.TabIndex = 6;
            this.Btn_Edit.Text = "Edit...";
            this.Btn_Edit.UseVisualStyleBackColor = true;
            this.Btn_Edit.Click += new System.EventHandler(this.Btn_Edit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Label_SettingDetails);
            this.groupBox1.Location = new System.Drawing.Point(3, 189);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(494, 250);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select setting details";
            // 
            // Label_SettingDetails
            // 
            this.Label_SettingDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Label_SettingDetails.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_SettingDetails.Location = new System.Drawing.Point(3, 15);
            this.Label_SettingDetails.Name = "Label_SettingDetails";
            this.Label_SettingDetails.Size = new System.Drawing.Size(488, 232);
            this.Label_SettingDetails.TabIndex = 0;
            // 
            // ConfigPage_NotifyMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GBox_SettingList);
            this.Name = "ConfigPage_NotifyMail";
            this.Size = new System.Drawing.Size(500, 442);
            this.GBox_SettingList.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox GBox_SettingList;
        private System.Windows.Forms.Button Btn_Add;
        private System.Windows.Forms.Button Btn_Edit;
        private System.Windows.Forms.ListBox LBox_SettingList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label Label_SettingDetails;
        private System.Windows.Forms.Button Btn_Remove;
    }
}
