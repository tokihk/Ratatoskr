﻿namespace Ratatoskr.Forms.MainWindow
{
    partial class MainWindow_SendLogPanel
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
            this.CBox_LogList = new Ratatoskr.Forms.RoundComboBox();
            this.Btn_FileSelect = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CBox_PlayDataType = new Ratatoskr.Forms.RoundComboBox();
            this.Label_FileSize = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.PBar_Progress = new System.Windows.Forms.ProgressBar();
            this.Label_TransSize = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.Btn_Send);
            this.groupBox1.Controls.Add(this.CBox_LogList);
            this.groupBox1.Controls.Add(this.Btn_FileSelect);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(609, 44);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Play log path";
            // 
            // Btn_Send
            // 
            this.Btn_Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Send.Location = new System.Drawing.Point(502, 15);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(55, 21);
            this.Btn_Send.TabIndex = 5;
            this.Btn_Send.Text = "Send";
            this.Btn_Send.UseVisualStyleBackColor = true;
            this.Btn_Send.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // CBox_LogList
            // 
            this.CBox_LogList.AllowDrop = true;
            this.CBox_LogList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CBox_LogList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CBox_LogList.FormattingEnabled = true;
            this.CBox_LogList.Location = new System.Drawing.Point(6, 16);
            this.CBox_LogList.Name = "CBox_LogList";
            this.CBox_LogList.Size = new System.Drawing.Size(490, 20);
            this.CBox_LogList.TabIndex = 3;
            this.CBox_LogList.TextChanged += new System.EventHandler(this.CBox_LogList_TextChanged);
            this.CBox_LogList.DragDrop += new System.Windows.Forms.DragEventHandler(this.CBox_LogList_DragDrop);
            this.CBox_LogList.DragEnter += new System.Windows.Forms.DragEventHandler(this.CBox_LogList_DragEnter);
            this.CBox_LogList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CBox_LogList_KeyDown);
            // 
            // Btn_FileSelect
            // 
            this.Btn_FileSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_FileSelect.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_FileSelect.Location = new System.Drawing.Point(563, 12);
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
            this.groupBox2.Controls.Add(this.CBox_PlayDataType);
            this.groupBox2.Location = new System.Drawing.Point(615, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(120, 44);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Play data type";
            // 
            // CBox_PlayDataType
            // 
            this.CBox_PlayDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_PlayDataType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CBox_PlayDataType.FormattingEnabled = true;
            this.CBox_PlayDataType.Location = new System.Drawing.Point(6, 17);
            this.CBox_PlayDataType.Name = "CBox_PlayDataType";
            this.CBox_PlayDataType.Size = new System.Drawing.Size(108, 20);
            this.CBox_PlayDataType.TabIndex = 0;
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
            this.groupBox3.Text = "Play record status";
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
            // MainWindow_SendLogPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainWindow_SendLogPanel";
            this.Size = new System.Drawing.Size(961, 49);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Btn_FileSelect;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Label_FileSize;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ProgressBar PBar_Progress;
        private System.Windows.Forms.Label Label_TransSize;
        private System.Windows.Forms.Button Btn_Send;
        private Ratatoskr.Forms.RoundComboBox CBox_LogList;
        private Ratatoskr.Forms.RoundComboBox CBox_PlayDataType;
    }
}
