namespace Ratatoskr.Forms.MainWindow
{
    partial class MainWindow_SendDataListPanel
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
            this.GView_SendDataList = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.GBox_Style = new System.Windows.Forms.GroupBox();
            this.RBtn_Style_Details = new System.Windows.Forms.RadioButton();
            this.RBtn_Style_Simple = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.GBox_CommonTarget = new System.Windows.Forms.GroupBox();
            this.TBox_CommonTarget = new System.Windows.Forms.TextBox();
            this.Btn_Play = new System.Windows.Forms.Button();
            this.Btn_Stop = new System.Windows.Forms.Button();
            this.GBox_NumberOfRepeat = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Label_RepeatCount = new System.Windows.Forms.Label();
            this.Num_RepeatCount = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.GBox_SendInterval = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Num_SendInterval_Fixed = new System.Windows.Forms.NumericUpDown();
            this.Num_SendInterval_Random = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GView_SendDataList)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.GBox_Style.SuspendLayout();
            this.GBox_CommonTarget.SuspendLayout();
            this.GBox_NumberOfRepeat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_RepeatCount)).BeginInit();
            this.GBox_SendInterval.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_SendInterval_Fixed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_SendInterval_Random)).BeginInit();
            this.SuspendLayout();
            // 
            // GView_SendDataList
            // 
            this.GView_SendDataList.AllowUserToOrderColumns = true;
            this.GView_SendDataList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.GView_SendDataList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GView_SendDataList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.GView_SendDataList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GView_SendDataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GView_SendDataList.Location = new System.Drawing.Point(0, 62);
            this.GView_SendDataList.Name = "GView_SendDataList";
            this.GView_SendDataList.RowTemplate.Height = 21;
            this.GView_SendDataList.Size = new System.Drawing.Size(892, 411);
            this.GView_SendDataList.TabIndex = 1;
            this.GView_SendDataList.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.GView_SendDataList_CellBeginEdit);
            this.GView_SendDataList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GView_SendDataList_CellContentClick);
            this.GView_SendDataList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GView_SendDataList_CellEndEdit);
            this.GView_SendDataList.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.GView_SendDataList_DataError);
            this.GView_SendDataList.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.GView_SendDataList_DefaultValuesNeeded);
            this.GView_SendDataList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.GView_SendDataList_EditingControlShowing);
            this.GView_SendDataList.RowContextMenuStripNeeded += new System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventHandler(this.GView_SendDataList_RowContextMenuStripNeeded);
            this.GView_SendDataList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GView_CmdList_KeyDown);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.GBox_Style);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.GBox_CommonTarget);
            this.flowLayoutPanel1.Controls.Add(this.Btn_Play);
            this.flowLayoutPanel1.Controls.Add(this.Btn_Stop);
            this.flowLayoutPanel1.Controls.Add(this.GBox_NumberOfRepeat);
            this.flowLayoutPanel1.Controls.Add(this.GBox_SendInterval);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(892, 62);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // GBox_Style
            // 
            this.GBox_Style.Controls.Add(this.RBtn_Style_Details);
            this.GBox_Style.Controls.Add(this.RBtn_Style_Simple);
            this.GBox_Style.Location = new System.Drawing.Point(3, 8);
            this.GBox_Style.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.GBox_Style.Name = "GBox_Style";
            this.GBox_Style.Size = new System.Drawing.Size(82, 48);
            this.GBox_Style.TabIndex = 10;
            this.GBox_Style.TabStop = false;
            this.GBox_Style.Text = "Style";
            // 
            // RBtn_Style_Details
            // 
            this.RBtn_Style_Details.Appearance = System.Windows.Forms.Appearance.Button;
            this.RBtn_Style_Details.AutoSize = true;
            this.RBtn_Style_Details.Image = RtsCore.Resource.Images.list_deails_24x24;
            this.RBtn_Style_Details.Location = new System.Drawing.Point(42, 12);
            this.RBtn_Style_Details.Name = "RBtn_Style_Details";
            this.RBtn_Style_Details.Size = new System.Drawing.Size(30, 30);
            this.RBtn_Style_Details.TabIndex = 1;
            this.RBtn_Style_Details.TabStop = true;
            this.RBtn_Style_Details.UseVisualStyleBackColor = true;
            this.RBtn_Style_Details.CheckedChanged += new System.EventHandler(this.RBtn_Style_Details_CheckedChanged);
            // 
            // RBtn_Style_Simple
            // 
            this.RBtn_Style_Simple.Appearance = System.Windows.Forms.Appearance.Button;
            this.RBtn_Style_Simple.AutoSize = true;
            this.RBtn_Style_Simple.Image = RtsCore.Resource.Images.list_simple_24x24;
            this.RBtn_Style_Simple.Location = new System.Drawing.Point(6, 12);
            this.RBtn_Style_Simple.Name = "RBtn_Style_Simple";
            this.RBtn_Style_Simple.Size = new System.Drawing.Size(30, 30);
            this.RBtn_Style_Simple.TabIndex = 0;
            this.RBtn_Style_Simple.TabStop = true;
            this.RBtn_Style_Simple.UseVisualStyleBackColor = true;
            this.RBtn_Style_Simple.CheckedChanged += new System.EventHandler(this.RBtn_Style_Simple_CheckedChanged);
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(91, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(2, 50);
            this.label4.TabIndex = 11;
            // 
            // GBox_CommonTarget
            // 
            this.GBox_CommonTarget.Controls.Add(this.TBox_CommonTarget);
            this.GBox_CommonTarget.Location = new System.Drawing.Point(99, 8);
            this.GBox_CommonTarget.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.GBox_CommonTarget.Name = "GBox_CommonTarget";
            this.GBox_CommonTarget.Size = new System.Drawing.Size(120, 40);
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
            this.TBox_CommonTarget.Size = new System.Drawing.Size(114, 19);
            this.TBox_CommonTarget.TabIndex = 0;
            this.TBox_CommonTarget.Text = "WWWWWWWWWWWWWWWW";
            // 
            // Btn_Play
            // 
            this.Btn_Play.FlatAppearance.BorderSize = 0;
            this.Btn_Play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Play.Image = RtsCore.Resource.Images.play_32x32;
            this.Btn_Play.Location = new System.Drawing.Point(225, 3);
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
            this.Btn_Stop.Image = RtsCore.Resource.Images.stop_32x32;
            this.Btn_Stop.Location = new System.Drawing.Point(285, 3);
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
            this.GBox_NumberOfRepeat.Location = new System.Drawing.Point(345, 8);
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
            // GBox_SendInterval
            // 
            this.GBox_SendInterval.Controls.Add(this.label5);
            this.GBox_SendInterval.Controls.Add(this.Num_SendInterval_Fixed);
            this.GBox_SendInterval.Controls.Add(this.Num_SendInterval_Random);
            this.GBox_SendInterval.Controls.Add(this.label7);
            this.GBox_SendInterval.Location = new System.Drawing.Point(611, 8);
            this.GBox_SendInterval.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.GBox_SendInterval.Name = "GBox_SendInterval";
            this.GBox_SendInterval.Size = new System.Drawing.Size(229, 40);
            this.GBox_SendInterval.TabIndex = 12;
            this.GBox_SendInterval.TabStop = false;
            this.GBox_SendInterval.Text = "Send interval (Fixed - Random)";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(82, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 19);
            this.label5.TabIndex = 5;
            this.label5.Text = "-";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Num_SendInterval_Fixed
            // 
            this.Num_SendInterval_Fixed.Location = new System.Drawing.Point(6, 15);
            this.Num_SendInterval_Fixed.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.Num_SendInterval_Fixed.Name = "Num_SendInterval_Fixed";
            this.Num_SendInterval_Fixed.Size = new System.Drawing.Size(70, 19);
            this.Num_SendInterval_Fixed.TabIndex = 4;
            this.Num_SendInterval_Fixed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_SendInterval_Fixed.Value = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            // 
            // Num_SendInterval_Random
            // 
            this.Num_SendInterval_Random.Location = new System.Drawing.Point(108, 15);
            this.Num_SendInterval_Random.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.Num_SendInterval_Random.Name = "Num_SendInterval_Random";
            this.Num_SendInterval_Random.Size = new System.Drawing.Size(70, 19);
            this.Num_SendInterval_Random.TabIndex = 0;
            this.Num_SendInterval_Random.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_SendInterval_Random.Value = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.Location = new System.Drawing.Point(184, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "msec";
            // 
            // MainWindow_SendDataListPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GView_SendDataList);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "MainWindow_SendDataListPanel";
            this.Size = new System.Drawing.Size(892, 473);
            ((System.ComponentModel.ISupportInitialize)(this.GView_SendDataList)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.GBox_Style.ResumeLayout(false);
            this.GBox_Style.PerformLayout();
            this.GBox_CommonTarget.ResumeLayout(false);
            this.GBox_CommonTarget.PerformLayout();
            this.GBox_NumberOfRepeat.ResumeLayout(false);
            this.GBox_NumberOfRepeat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_RepeatCount)).EndInit();
            this.GBox_SendInterval.ResumeLayout(false);
            this.GBox_SendInterval.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_SendInterval_Fixed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Num_SendInterval_Random)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GView_SendDataList;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox GBox_CommonTarget;
        private System.Windows.Forms.TextBox TBox_CommonTarget;
        private System.Windows.Forms.Button Btn_Play;
        private System.Windows.Forms.Button Btn_Stop;
        private System.Windows.Forms.GroupBox GBox_NumberOfRepeat;
        private System.Windows.Forms.NumericUpDown Num_RepeatCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Label_RepeatCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox GBox_Style;
        private System.Windows.Forms.RadioButton RBtn_Style_Simple;
        private System.Windows.Forms.RadioButton RBtn_Style_Details;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox GBox_SendInterval;
        private System.Windows.Forms.NumericUpDown Num_SendInterval_Random;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown Num_SendInterval_Fixed;
    }
}
