namespace Ratatoskr.Forms.MainWindow
{
    partial class MainWindow_PacketConverter
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
            this.components = new System.ComponentModel.Container();
            this.Label_Handle = new System.Windows.Forms.Label();
            this.Panel_Contents = new System.Windows.Forms.Panel();
            this.Label_OutputCount = new System.Windows.Forms.Label();
            this.CMenu_Target = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ChkBox_FilterEnable = new System.Windows.Forms.ToolStripMenuItem();
            this.sep = new System.Windows.Forms.ToolStripSeparator();
            this.TBox_FilterValue = new System.Windows.Forms.ToolStripTextBox();
            this.Btn_Filter = new System.Windows.Forms.Button();
            this.Label_OutputIcon = new System.Windows.Forms.Label();
            this.Btn_Remove = new System.Windows.Forms.Button();
            this.ChkBox_Enable = new System.Windows.Forms.CheckBox();
            this.Label_Name = new System.Windows.Forms.Label();
            this.CMenu_Target.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label_Handle
            // 
            this.Label_Handle.BackColor = System.Drawing.SystemColors.Control;
            this.Label_Handle.Cursor = System.Windows.Forms.Cursors.NoMoveVert;
            this.Label_Handle.Image = global::Ratatoskr.Properties.Resources.item_change_16x16;
            this.Label_Handle.Location = new System.Drawing.Point(3, 0);
            this.Label_Handle.Name = "Label_Handle";
            this.Label_Handle.Size = new System.Drawing.Size(23, 24);
            this.Label_Handle.TabIndex = 0;
            this.Label_Handle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_Handle_MouseMove);
            // 
            // Panel_Contents
            // 
            this.Panel_Contents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Contents.Location = new System.Drawing.Point(189, 0);
            this.Panel_Contents.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Contents.Name = "Panel_Contents";
            this.Panel_Contents.Size = new System.Drawing.Size(579, 24);
            this.Panel_Contents.TabIndex = 2;
            // 
            // Label_OutputCount
            // 
            this.Label_OutputCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_OutputCount.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_OutputCount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Label_OutputCount.Location = new System.Drawing.Point(801, 0);
            this.Label_OutputCount.Name = "Label_OutputCount";
            this.Label_OutputCount.Size = new System.Drawing.Size(60, 24);
            this.Label_OutputCount.TabIndex = 6;
            this.Label_OutputCount.Text = "10000";
            this.Label_OutputCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CMenu_Target
            // 
            this.CMenu_Target.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ChkBox_FilterEnable,
            this.sep,
            this.TBox_FilterValue});
            this.CMenu_Target.Name = "CMenu_Target";
            this.CMenu_Target.Size = new System.Drawing.Size(661, 53);
            // 
            // ChkBox_FilterEnable
            // 
            this.ChkBox_FilterEnable.Name = "ChkBox_FilterEnable";
            this.ChkBox_FilterEnable.Size = new System.Drawing.Size(660, 22);
            this.ChkBox_FilterEnable.Text = "Target filter";
            this.ChkBox_FilterEnable.Click += new System.EventHandler(this.ChkBox_FilterEnable_Click);
            // 
            // sep
            // 
            this.sep.Name = "sep";
            this.sep.Size = new System.Drawing.Size(657, 6);
            // 
            // TBox_FilterValue
            // 
            this.TBox_FilterValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TBox_FilterValue.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TBox_FilterValue.Name = "TBox_FilterValue";
            this.TBox_FilterValue.Size = new System.Drawing.Size(600, 19);
            this.TBox_FilterValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBox_FilterValue_KeyDown);
            this.TBox_FilterValue.TextChanged += new System.EventHandler(this.TBox_FilterValue_TextChanged);
            // 
            // Btn_Filter
            // 
            this.Btn_Filter.FlatAppearance.BorderSize = 0;
            this.Btn_Filter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Filter.Image = global::Ratatoskr.Properties.Resources.target_22x22;
            this.Btn_Filter.Location = new System.Drawing.Point(163, 1);
            this.Btn_Filter.Name = "Btn_Filter";
            this.Btn_Filter.Size = new System.Drawing.Size(23, 23);
            this.Btn_Filter.TabIndex = 9;
            this.Btn_Filter.UseVisualStyleBackColor = true;
            this.Btn_Filter.Click += new System.EventHandler(this.Btn_Filter_Click);
            // 
            // Label_OutputIcon
            // 
            this.Label_OutputIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_OutputIcon.Image = global::Ratatoskr.Properties.Resources.output_16x16;
            this.Label_OutputIcon.Location = new System.Drawing.Point(771, 0);
            this.Label_OutputIcon.Name = "Label_OutputIcon";
            this.Label_OutputIcon.Size = new System.Drawing.Size(24, 24);
            this.Label_OutputIcon.TabIndex = 7;
            // 
            // Btn_Remove
            // 
            this.Btn_Remove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Remove.FlatAppearance.BorderSize = 0;
            this.Btn_Remove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Remove.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_Remove.Image = global::Ratatoskr.Properties.Resources.close_16x16;
            this.Btn_Remove.Location = new System.Drawing.Point(867, 0);
            this.Btn_Remove.Name = "Btn_Remove";
            this.Btn_Remove.Size = new System.Drawing.Size(24, 24);
            this.Btn_Remove.TabIndex = 3;
            this.Btn_Remove.UseVisualStyleBackColor = true;
            this.Btn_Remove.Click += new System.EventHandler(this.Btn_Remove_Click);
            // 
            // ChkBox_Enable
            // 
            this.ChkBox_Enable.AutoSize = true;
            this.ChkBox_Enable.Location = new System.Drawing.Point(32, 6);
            this.ChkBox_Enable.Name = "ChkBox_Enable";
            this.ChkBox_Enable.Size = new System.Drawing.Size(15, 14);
            this.ChkBox_Enable.TabIndex = 10;
            this.ChkBox_Enable.UseVisualStyleBackColor = true;
            this.ChkBox_Enable.CheckedChanged += new System.EventHandler(this.ChkBox_Enable_CheckedChanged);
            // 
            // Label_Name
            // 
            this.Label_Name.Location = new System.Drawing.Point(53, 1);
            this.Label_Name.Name = "Label_Name";
            this.Label_Name.Size = new System.Drawing.Size(104, 24);
            this.Label_Name.TabIndex = 11;
            this.Label_Name.Text = "label1";
            this.Label_Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MainWindow_PacketConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.Label_Name);
            this.Controls.Add(this.Btn_Filter);
            this.Controls.Add(this.ChkBox_Enable);
            this.Controls.Add(this.Label_OutputIcon);
            this.Controls.Add(this.Label_OutputCount);
            this.Controls.Add(this.Btn_Remove);
            this.Controls.Add(this.Panel_Contents);
            this.Controls.Add(this.Label_Handle);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MainWindow_PacketConverter";
            this.Size = new System.Drawing.Size(895, 27);
            this.CMenu_Target.ResumeLayout(false);
            this.CMenu_Target.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_Handle;
        private System.Windows.Forms.Panel Panel_Contents;
        private System.Windows.Forms.Button Btn_Remove;
        private System.Windows.Forms.Label Label_OutputCount;
        private System.Windows.Forms.Label Label_OutputIcon;
        private System.Windows.Forms.Button Btn_Filter;
        private System.Windows.Forms.ContextMenuStrip CMenu_Target;
        private System.Windows.Forms.ToolStripTextBox TBox_FilterValue;
        private System.Windows.Forms.ToolStripMenuItem ChkBox_FilterEnable;
        private System.Windows.Forms.ToolStripSeparator sep;
        private System.Windows.Forms.CheckBox ChkBox_Enable;
        private System.Windows.Forms.Label Label_Name;
    }
}
