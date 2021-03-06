﻿namespace Ratatoskr.Forms.MainWindow
{
    partial class MainWindow_SendFilePanel
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
            this.Btn_Send = new System.Windows.Forms.Button();
            this.CBox_FileList = new Ratatoskr.Forms.RoundComboBox();
            this.Btn_FileSelect = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Num_SendBlockSize = new System.Windows.Forms.NumericUpDown();
            this.Label_FileSize = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.PBar_Progress = new System.Windows.Forms.ProgressBar();
            this.Label_TransSize = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Num_SendDelay = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_SendBlockSize)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_SendDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Btn_Send);
            this.groupBox1.Controls.Add(this.CBox_FileList);
            this.groupBox1.Controls.Add(this.Btn_FileSelect);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(477, 44);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Send file path";
            // 
            // Btn_Send
            // 
            this.Btn_Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Send.Location = new System.Drawing.Point(370, 15);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(55, 21);
            this.Btn_Send.TabIndex = 4;
            this.Btn_Send.Text = "Send";
            this.Btn_Send.UseVisualStyleBackColor = true;
            this.Btn_Send.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // CBox_FileList
            // 
            this.CBox_FileList.AllowDrop = true;
            this.CBox_FileList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CBox_FileList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CBox_FileList.FormattingEnabled = true;
            this.CBox_FileList.Location = new System.Drawing.Point(6, 16);
            this.CBox_FileList.Name = "CBox_FileList";
            this.CBox_FileList.Size = new System.Drawing.Size(358, 20);
            this.CBox_FileList.TabIndex = 3;
            this.CBox_FileList.TextChanged += new System.EventHandler(this.CBox_FileList_TextChanged);
            this.CBox_FileList.DragDrop += new System.Windows.Forms.DragEventHandler(this.CBox_FileList_DragDrop);
            this.CBox_FileList.DragEnter += new System.Windows.Forms.DragEventHandler(this.CBox_FileList_DragEnter);
            this.CBox_FileList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CBox_FileList_KeyDown);
            // 
            // Btn_FileSelect
            // 
            this.Btn_FileSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_FileSelect.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_FileSelect.Location = new System.Drawing.Point(431, 12);
            this.Btn_FileSelect.Name = "Btn_FileSelect";
            this.Btn_FileSelect.Size = new System.Drawing.Size(40, 26);
            this.Btn_FileSelect.TabIndex = 1;
            this.Btn_FileSelect.Text = "...";
            this.Btn_FileSelect.UseVisualStyleBackColor = true;
            this.Btn_FileSelect.Click += new System.EventHandler(this.Btn_FileSelect_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.Num_SendBlockSize);
            this.groupBox2.Location = new System.Drawing.Point(483, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(120, 44);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Send block size";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "byte";
            // 
            // Num_SendBlockSize
            // 
            this.Num_SendBlockSize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Num_SendBlockSize.Location = new System.Drawing.Point(6, 17);
            this.Num_SendBlockSize.Maximum = new decimal(new int[] {
            524288,
            0,
            0,
            0});
            this.Num_SendBlockSize.Name = "Num_SendBlockSize";
            this.Num_SendBlockSize.Size = new System.Drawing.Size(75, 19);
            this.Num_SendBlockSize.TabIndex = 0;
            this.Num_SendBlockSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_SendBlockSize.ThousandsSeparator = true;
            this.Num_SendBlockSize.Value = new decimal(new int[] {
            524288,
            0,
            0,
            0});
            // 
            // Label_FileSize
            // 
            this.Label_FileSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label_FileSize.Font = new System.Drawing.Font("ＭＳ ゴシック", 8F);
            this.Label_FileSize.Location = new System.Drawing.Point(119, 15);
            this.Label_FileSize.Name = "Label_FileSize";
            this.Label_FileSize.Size = new System.Drawing.Size(90, 14);
            this.Label_FileSize.TabIndex = 4;
            this.Label_FileSize.Text = "9,999,999,999";
            this.Label_FileSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.label2.Location = new System.Drawing.Point(102, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 11);
            this.label2.TabIndex = 3;
            this.label2.Text = "/";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.PBar_Progress);
            this.groupBox3.Controls.Add(this.Label_TransSize);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.Label_FileSize);
            this.groupBox3.Location = new System.Drawing.Point(741, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(217, 44);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Transfer size (byte)";
            // 
            // PBar_Progress
            // 
            this.PBar_Progress.Location = new System.Drawing.Point(6, 31);
            this.PBar_Progress.Name = "PBar_Progress";
            this.PBar_Progress.Size = new System.Drawing.Size(203, 9);
            this.PBar_Progress.TabIndex = 6;
            // 
            // Label_TransSize
            // 
            this.Label_TransSize.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label_TransSize.Font = new System.Drawing.Font("ＭＳ ゴシック", 8F);
            this.Label_TransSize.Location = new System.Drawing.Point(6, 15);
            this.Label_TransSize.Name = "Label_TransSize";
            this.Label_TransSize.Size = new System.Drawing.Size(90, 14);
            this.Label_TransSize.TabIndex = 5;
            this.Label_TransSize.Text = "9,999,999,999";
            this.Label_TransSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.Num_SendDelay);
            this.groupBox4.Location = new System.Drawing.Point(609, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(126, 44);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Send delay";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(87, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "msec";
            // 
            // Num_SendDelay
            // 
            this.Num_SendDelay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Num_SendDelay.Location = new System.Drawing.Point(6, 17);
            this.Num_SendDelay.Maximum = new decimal(new int[] {
            180000,
            0,
            0,
            0});
            this.Num_SendDelay.Name = "Num_SendDelay";
            this.Num_SendDelay.Size = new System.Drawing.Size(75, 19);
            this.Num_SendDelay.TabIndex = 0;
            this.Num_SendDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_SendDelay.ThousandsSeparator = true;
            this.Num_SendDelay.Value = new decimal(new int[] {
            180000,
            0,
            0,
            0});
            // 
            // MainWindow_SendFilePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainWindow_SendFilePanel";
            this.Size = new System.Drawing.Size(961, 49);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_SendBlockSize)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_SendDelay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Btn_FileSelect;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown Num_SendBlockSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Label_FileSize;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ProgressBar PBar_Progress;
        private System.Windows.Forms.Label Label_TransSize;
        private System.Windows.Forms.Button Btn_Send;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown Num_SendDelay;
        private Ratatoskr.Forms.RoundComboBox CBox_FileList;
    }
}
