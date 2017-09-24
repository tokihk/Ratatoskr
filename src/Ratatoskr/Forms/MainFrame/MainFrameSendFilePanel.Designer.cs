namespace Ratatoskr.Forms.MainFrame
{
    partial class MainFrameSendFilePanel
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CBox_FileList = new System.Windows.Forms.ComboBox();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_FileSelect = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Label_FileSize = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Num_BlockSize = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_BlockSize)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.CBox_FileList);
            this.groupBox1.Controls.Add(this.Btn_Cancel);
            this.groupBox1.Controls.Add(this.Btn_FileSelect);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(556, 44);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Send file path";
            // 
            // CBox_FileList
            // 
            this.CBox_FileList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CBox_FileList.FormattingEnabled = true;
            this.CBox_FileList.Location = new System.Drawing.Point(6, 16);
            this.CBox_FileList.Name = "CBox_FileList";
            this.CBox_FileList.Size = new System.Drawing.Size(413, 20);
            this.CBox_FileList.TabIndex = 3;
            this.CBox_FileList.TextChanged += new System.EventHandler(this.CBox_FileList_TextChanged);
            this.CBox_FileList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CBox_FileList_KeyDown);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Cancel.FlatAppearance.BorderSize = 0;
            this.Btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Cancel.Image = global::Ratatoskr.Properties.Resources.cancel_red_16x16;
            this.Btn_Cancel.Location = new System.Drawing.Point(422, 13);
            this.Btn_Cancel.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(26, 26);
            this.Btn_Cancel.TabIndex = 2;
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Visible = false;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Btn_FileSelect
            // 
            this.Btn_FileSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_FileSelect.Location = new System.Drawing.Point(451, 16);
            this.Btn_FileSelect.Name = "Btn_FileSelect";
            this.Btn_FileSelect.Size = new System.Drawing.Size(75, 20);
            this.Btn_FileSelect.TabIndex = 1;
            this.Btn_FileSelect.Text = "Browse...";
            this.Btn_FileSelect.UseVisualStyleBackColor = true;
            this.Btn_FileSelect.Click += new System.EventHandler(this.Btn_FileSelect_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.Label_FileSize);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.Num_BlockSize);
            this.groupBox2.Location = new System.Drawing.Point(562, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(236, 41);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Send block size";
            // 
            // Label_FileSize
            // 
            this.Label_FileSize.Location = new System.Drawing.Point(6, 15);
            this.Label_FileSize.Name = "Label_FileSize";
            this.Label_FileSize.Size = new System.Drawing.Size(77, 20);
            this.Label_FileSize.TabIndex = 4;
            this.Label_FileSize.Text = "9,999,999,999";
            this.Label_FileSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(89, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "/";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(189, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "bytes";
            // 
            // Num_BlockSize
            // 
            this.Num_BlockSize.Location = new System.Drawing.Point(106, 14);
            this.Num_BlockSize.Maximum = new decimal(new int[] {
            524288,
            0,
            0,
            0});
            this.Num_BlockSize.Name = "Num_BlockSize";
            this.Num_BlockSize.Size = new System.Drawing.Size(77, 19);
            this.Num_BlockSize.TabIndex = 0;
            this.Num_BlockSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_BlockSize.ThousandsSeparator = true;
            this.Num_BlockSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // MainFrameSendFilePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainFrameSendFilePanel";
            this.Size = new System.Drawing.Size(801, 49);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_BlockSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Btn_FileSelect;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown Num_BlockSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Label_FileSize;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.ComboBox CBox_FileList;
    }
}
