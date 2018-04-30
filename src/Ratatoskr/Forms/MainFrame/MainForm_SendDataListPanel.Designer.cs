namespace Ratatoskr.Forms.MainFrame
{
    partial class MainForm_SendDataListPanel
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
            this.GView_CmdList = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.GBox_CommonTarget = new System.Windows.Forms.GroupBox();
            this.TBox_CommonTarget = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_Play = new System.Windows.Forms.Button();
            this.Btn_Stop = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Label_RepeatCount = new System.Windows.Forms.Label();
            this.Num_RepeatCount = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GView_CmdList)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.GBox_CommonTarget.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_RepeatCount)).BeginInit();
            this.SuspendLayout();
            // 
            // GView_CmdList
            // 
            this.GView_CmdList.AllowUserToOrderColumns = true;
            this.GView_CmdList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.GView_CmdList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GView_CmdList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.GView_CmdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GView_CmdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GView_CmdList.Location = new System.Drawing.Point(0, 62);
            this.GView_CmdList.Name = "GView_CmdList";
            this.GView_CmdList.RowTemplate.Height = 21;
            this.GView_CmdList.Size = new System.Drawing.Size(777, 411);
            this.GView_CmdList.TabIndex = 1;
            this.GView_CmdList.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.GView_CmdList_CellBeginEdit);
            this.GView_CmdList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GView_CmdList_CellEndEdit);
            this.GView_CmdList.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.GView_CmdList_DefaultValuesNeeded);
            this.GView_CmdList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.GView_CmdList_EditingControlShowing);
            this.GView_CmdList.RowContextMenuStripNeeded += new System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventHandler(this.GView_CmdList_RowContextMenuStripNeeded);
            this.GView_CmdList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GView_CmdList_KeyDown);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.GBox_CommonTarget);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.Btn_Play);
            this.flowLayoutPanel1.Controls.Add(this.Btn_Stop);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(777, 62);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // GBox_CommonTarget
            // 
            this.GBox_CommonTarget.Controls.Add(this.TBox_CommonTarget);
            this.GBox_CommonTarget.Location = new System.Drawing.Point(3, 8);
            this.GBox_CommonTarget.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.GBox_CommonTarget.Name = "GBox_CommonTarget";
            this.GBox_CommonTarget.Size = new System.Drawing.Size(140, 40);
            this.GBox_CommonTarget.TabIndex = 5;
            this.GBox_CommonTarget.TabStop = false;
            this.GBox_CommonTarget.Text = "Common target";
            // 
            // TBox_CommonTarget
            // 
            this.TBox_CommonTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_CommonTarget.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TBox_CommonTarget.Location = new System.Drawing.Point(3, 15);
            this.TBox_CommonTarget.Name = "TBox_CommonTarget";
            this.TBox_CommonTarget.Size = new System.Drawing.Size(134, 19);
            this.TBox_CommonTarget.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(149, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(2, 50);
            this.label1.TabIndex = 9;
            // 
            // Btn_Play
            // 
            this.Btn_Play.FlatAppearance.BorderSize = 0;
            this.Btn_Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Play.Image = global::Ratatoskr.Properties.Resources.play_32x32;
            this.Btn_Play.Location = new System.Drawing.Point(157, 3);
            this.Btn_Play.Name = "Btn_Play";
            this.Btn_Play.Size = new System.Drawing.Size(65, 54);
            this.Btn_Play.TabIndex = 6;
            this.Btn_Play.Text = "Run";
            this.Btn_Play.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Play.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Btn_Play.UseVisualStyleBackColor = true;
            this.Btn_Play.Click += new System.EventHandler(this.Btn_Play_Click);
            // 
            // Btn_Stop
            // 
            this.Btn_Stop.FlatAppearance.BorderSize = 0;
            this.Btn_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Stop.Image = global::Ratatoskr.Properties.Resources.stop_32x32;
            this.Btn_Stop.Location = new System.Drawing.Point(228, 3);
            this.Btn_Stop.Name = "Btn_Stop";
            this.Btn_Stop.Size = new System.Drawing.Size(65, 54);
            this.Btn_Stop.TabIndex = 7;
            this.Btn_Stop.Text = "Stop";
            this.Btn_Stop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Stop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Btn_Stop.UseVisualStyleBackColor = true;
            this.Btn_Stop.Click += new System.EventHandler(this.Btn_Stop_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.Label_RepeatCount);
            this.groupBox2.Controls.Add(this.Num_RepeatCount);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(299, 8);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 40);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Number of repetitions";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(82, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "/";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_RepeatCount
            // 
            this.Label_RepeatCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label_RepeatCount.Location = new System.Drawing.Point(6, 15);
            this.Label_RepeatCount.Name = "Label_RepeatCount";
            this.Label_RepeatCount.Size = new System.Drawing.Size(70, 19);
            this.Label_RepeatCount.TabIndex = 1;
            this.Label_RepeatCount.Text = "-";
            this.Label_RepeatCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Num_RepeatCount
            // 
            this.Num_RepeatCount.Location = new System.Drawing.Point(108, 15);
            this.Num_RepeatCount.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.Num_RepeatCount.Name = "Num_RepeatCount";
            this.Num_RepeatCount.Size = new System.Drawing.Size(70, 19);
            this.Num_RepeatCount.TabIndex = 0;
            this.Num_RepeatCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_RepeatCount.Value = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(184, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "(0: Infinite)";
            // 
            // MainFrameSendDataListPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GView_CmdList);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "MainFrameSendDataListPanel";
            this.Size = new System.Drawing.Size(777, 473);
            ((System.ComponentModel.ISupportInitialize)(this.GView_CmdList)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.GBox_CommonTarget.ResumeLayout(false);
            this.GBox_CommonTarget.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_RepeatCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GView_CmdList;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox GBox_CommonTarget;
        private System.Windows.Forms.TextBox TBox_CommonTarget;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_Play;
        private System.Windows.Forms.Button Btn_Stop;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown Num_RepeatCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Label_RepeatCount;
        private System.Windows.Forms.Label label2;
    }
}
