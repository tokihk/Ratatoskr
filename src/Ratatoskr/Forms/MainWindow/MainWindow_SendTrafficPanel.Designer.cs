namespace Ratatoskr.Forms.MainWindow
{
    partial class MainWindow_SendTrafficPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow_SendTrafficPanel));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.Btn_Play = new System.Windows.Forms.Button();
            this.Btn_Stop = new System.Windows.Forms.Button();
            this.GBox_NumberOfRepeat = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Label_RepeatCount = new System.Windows.Forms.Label();
            this.Num_RepeatCount = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.trafficEditor1 = new Ratatoskr.Forms.Controls.TrafficEditor();
            this.flowLayoutPanel1.SuspendLayout();
            this.GBox_NumberOfRepeat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_RepeatCount)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.Btn_Play);
            this.flowLayoutPanel1.Controls.Add(this.Btn_Stop);
            this.flowLayoutPanel1.Controls.Add(this.GBox_NumberOfRepeat);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1189, 78);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // Btn_Play
            // 
            this.Btn_Play.FlatAppearance.BorderSize = 0;
            this.Btn_Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Play.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Play.Image")));
            this.Btn_Play.Location = new System.Drawing.Point(4, 4);
            this.Btn_Play.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Play.Name = "Btn_Play";
            this.Btn_Play.Size = new System.Drawing.Size(72, 68);
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
            this.Btn_Stop.Location = new System.Drawing.Point(84, 4);
            this.Btn_Stop.Margin = new System.Windows.Forms.Padding(4);
            this.Btn_Stop.Name = "Btn_Stop";
            this.Btn_Stop.Size = new System.Drawing.Size(72, 68);
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
            this.GBox_NumberOfRepeat.Location = new System.Drawing.Point(164, 10);
            this.GBox_NumberOfRepeat.Margin = new System.Windows.Forms.Padding(4, 10, 4, 4);
            this.GBox_NumberOfRepeat.Name = "GBox_NumberOfRepeat";
            this.GBox_NumberOfRepeat.Padding = new System.Windows.Forms.Padding(4);
            this.GBox_NumberOfRepeat.Size = new System.Drawing.Size(347, 50);
            this.GBox_NumberOfRepeat.TabIndex = 8;
            this.GBox_NumberOfRepeat.TabStop = false;
            this.GBox_NumberOfRepeat.Text = "Number of repetitions";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(109, 19);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "/";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label_RepeatCount
            // 
            this.Label_RepeatCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Label_RepeatCount.Location = new System.Drawing.Point(8, 19);
            this.Label_RepeatCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label_RepeatCount.Name = "Label_RepeatCount";
            this.Label_RepeatCount.Size = new System.Drawing.Size(93, 24);
            this.Label_RepeatCount.TabIndex = 1;
            this.Label_RepeatCount.Text = "-";
            this.Label_RepeatCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Num_RepeatCount
            // 
            this.Num_RepeatCount.Location = new System.Drawing.Point(144, 19);
            this.Num_RepeatCount.Margin = new System.Windows.Forms.Padding(4);
            this.Num_RepeatCount.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.Num_RepeatCount.Name = "Num_RepeatCount";
            this.Num_RepeatCount.Size = new System.Drawing.Size(93, 22);
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
            this.label2.Location = new System.Drawing.Point(245, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "(0: Infinite)";
            // 
            // trafficEditor1
            // 
            this.trafficEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trafficEditor1.Location = new System.Drawing.Point(0, 78);
            this.trafficEditor1.Name = "trafficEditor1";
            this.trafficEditor1.Size = new System.Drawing.Size(1189, 513);
            this.trafficEditor1.TabIndex = 3;
            // 
            // MainWindow_SendTrafficPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trafficEditor1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainWindow_SendTrafficPanel";
            this.Size = new System.Drawing.Size(1189, 591);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.GBox_NumberOfRepeat.ResumeLayout(false);
            this.GBox_NumberOfRepeat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_RepeatCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button Btn_Play;
        private System.Windows.Forms.Button Btn_Stop;
        private System.Windows.Forms.GroupBox GBox_NumberOfRepeat;
        private System.Windows.Forms.NumericUpDown Num_RepeatCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Label_RepeatCount;
        private System.Windows.Forms.Label label2;
		private Controls.TrafficEditor trafficEditor1;
	}
}
