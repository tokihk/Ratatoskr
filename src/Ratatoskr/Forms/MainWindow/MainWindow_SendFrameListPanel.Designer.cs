namespace Ratatoskr.Forms.MainWindow
{
    partial class MainWindow_SendFrameListPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow_SendFrameListPanel));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.GBox_SendTarget = new System.Windows.Forms.GroupBox();
            this.TBox_SendTarget = new System.Windows.Forms.TextBox();
            this.Btn_Play = new System.Windows.Forms.Button();
            this.Btn_Stop = new System.Windows.Forms.Button();
            this.GBox_NumberOfRepeat = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Label_RepeatCount = new System.Windows.Forms.Label();
            this.Num_RepeatCount = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TView_FrameTemplateList = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.LView_SendFrameList = new RtsCore.Framework.Controls.ListViewEx();
            this.TLView_SendFrameDetails = new RtsCore.Framework.Controls.TreeListView();
            this.flowLayoutPanel1.SuspendLayout();
            this.GBox_SendTarget.SuspendLayout();
            this.GBox_NumberOfRepeat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_RepeatCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.GBox_SendTarget);
            this.flowLayoutPanel1.Controls.Add(this.Btn_Play);
            this.flowLayoutPanel1.Controls.Add(this.Btn_Stop);
            this.flowLayoutPanel1.Controls.Add(this.GBox_NumberOfRepeat);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(892, 62);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // GBox_SendTarget
            // 
            this.GBox_SendTarget.Controls.Add(this.TBox_SendTarget);
            this.GBox_SendTarget.Location = new System.Drawing.Point(3, 8);
            this.GBox_SendTarget.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.GBox_SendTarget.Name = "GBox_SendTarget";
            this.GBox_SendTarget.Size = new System.Drawing.Size(120, 40);
            this.GBox_SendTarget.TabIndex = 5;
            this.GBox_SendTarget.TabStop = false;
            this.GBox_SendTarget.Text = "Send target";
            // 
            // TBox_SendTarget
            // 
            this.TBox_SendTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_SendTarget.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TBox_SendTarget.Location = new System.Drawing.Point(3, 15);
            this.TBox_SendTarget.Name = "TBox_SendTarget";
            this.TBox_SendTarget.Size = new System.Drawing.Size(114, 19);
            this.TBox_SendTarget.TabIndex = 0;
            this.TBox_SendTarget.Text = "WWWWWWWWWWWWWWWW";
            // 
            // Btn_Play
            // 
            this.Btn_Play.FlatAppearance.BorderSize = 0;
            this.Btn_Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Play.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Play.Image")));
            this.Btn_Play.Location = new System.Drawing.Point(129, 3);
            this.Btn_Play.Name = "Btn_Play";
            this.Btn_Play.Size = new System.Drawing.Size(54, 54);
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
            this.Btn_Stop.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Stop.Image")));
            this.Btn_Stop.Location = new System.Drawing.Point(189, 3);
            this.Btn_Stop.Name = "Btn_Stop";
            this.Btn_Stop.Size = new System.Drawing.Size(54, 54);
            this.Btn_Stop.TabIndex = 7;
            this.Btn_Stop.Text = "Stop";
            this.Btn_Stop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.Btn_Stop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Btn_Stop.UseVisualStyleBackColor = true;
            this.Btn_Stop.Click += new System.EventHandler(this.Btn_Stop_Click);
            // 
            // GBox_NumberOfRepeat
            // 
            this.GBox_NumberOfRepeat.Controls.Add(this.label3);
            this.GBox_NumberOfRepeat.Controls.Add(this.Label_RepeatCount);
            this.GBox_NumberOfRepeat.Controls.Add(this.Num_RepeatCount);
            this.GBox_NumberOfRepeat.Controls.Add(this.label2);
            this.GBox_NumberOfRepeat.Location = new System.Drawing.Point(249, 8);
            this.GBox_NumberOfRepeat.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.GBox_NumberOfRepeat.Name = "GBox_NumberOfRepeat";
            this.GBox_NumberOfRepeat.Size = new System.Drawing.Size(260, 40);
            this.GBox_NumberOfRepeat.TabIndex = 8;
            this.GBox_NumberOfRepeat.TabStop = false;
            this.GBox_NumberOfRepeat.Text = "Number of repetitions";
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
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 62);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.TView_FrameTemplateList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(892, 411);
            this.splitContainer1.SplitterDistance = 258;
            this.splitContainer1.TabIndex = 3;
            // 
            // TView_FrameTemplateList
            // 
            this.TView_FrameTemplateList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TView_FrameTemplateList.Location = new System.Drawing.Point(0, 0);
            this.TView_FrameTemplateList.Name = "TView_FrameTemplateList";
            this.TView_FrameTemplateList.Size = new System.Drawing.Size(258, 411);
            this.TView_FrameTemplateList.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.LView_SendFrameList);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.TLView_SendFrameDetails);
            this.splitContainer2.Size = new System.Drawing.Size(630, 411);
            this.splitContainer2.SplitterDistance = 344;
            this.splitContainer2.TabIndex = 0;
            // 
            // LView_SendFrameList
            // 
            this.LView_SendFrameList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LView_SendFrameList.ItemCountMax = 0;
            this.LView_SendFrameList.Location = new System.Drawing.Point(0, 0);
            this.LView_SendFrameList.Name = "LView_SendFrameList";
            this.LView_SendFrameList.ReadOnly = false;
            this.LView_SendFrameList.Size = new System.Drawing.Size(344, 411);
            this.LView_SendFrameList.TabIndex = 0;
            this.LView_SendFrameList.UseCompatibleStateImageBehavior = false;
            this.LView_SendFrameList.VirtualMode = true;
            // 
            // TLView_SendFrameDetails
            // 
            this.TLView_SendFrameDetails.BackColor = System.Drawing.SystemColors.Window;
            this.TLView_SendFrameDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TLView_SendFrameDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLView_SendFrameDetails.Location = new System.Drawing.Point(0, 0);
            this.TLView_SendFrameDetails.Name = "TLView_SendFrameDetails";
            this.TLView_SendFrameDetails.Size = new System.Drawing.Size(282, 411);
            this.TLView_SendFrameDetails.TabIndex = 0;
            // 
            // MainWindow_SendFrameListPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "MainWindow_SendFrameListPanel";
            this.Size = new System.Drawing.Size(892, 473);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.GBox_SendTarget.ResumeLayout(false);
            this.GBox_SendTarget.PerformLayout();
            this.GBox_NumberOfRepeat.ResumeLayout(false);
            this.GBox_NumberOfRepeat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_RepeatCount)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox GBox_SendTarget;
        private System.Windows.Forms.TextBox TBox_SendTarget;
        private System.Windows.Forms.Button Btn_Play;
        private System.Windows.Forms.Button Btn_Stop;
        private System.Windows.Forms.GroupBox GBox_NumberOfRepeat;
        private System.Windows.Forms.NumericUpDown Num_RepeatCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Label_RepeatCount;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.TreeView TView_FrameTemplateList;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private RtsCore.Framework.Controls.ListViewEx LView_SendFrameList;
		private RtsCore.Framework.Controls.TreeListView TLView_SendFrameDetails;
	}
}
