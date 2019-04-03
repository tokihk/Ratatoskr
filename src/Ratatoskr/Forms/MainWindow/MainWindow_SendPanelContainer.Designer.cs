namespace Ratatoskr.Forms.MainWindow
{
    partial class MainWindow_SendPanelContainer
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
            if (disposing && (components != null))
            {
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
            this.GBox_Target = new System.Windows.Forms.GroupBox();
            this.Panel_Contents = new System.Windows.Forms.Panel();
            this.RBtn_ModeLog = new System.Windows.Forms.RadioButton();
            this.RBtn_ModeFile = new System.Windows.Forms.RadioButton();
            this.RBtn_ModeData = new System.Windows.Forms.RadioButton();
            this.CBox_TargetList = new RtsCore.Framework.Controls.ComboBoxEx();
            this.GBox_Target.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBox_Target
            // 
            this.GBox_Target.Controls.Add(this.CBox_TargetList);
            this.GBox_Target.Dock = System.Windows.Forms.DockStyle.Left;
            this.GBox_Target.Location = new System.Drawing.Point(0, 0);
            this.GBox_Target.Name = "GBox_Target";
            this.GBox_Target.Size = new System.Drawing.Size(157, 44);
            this.GBox_Target.TabIndex = 1;
            this.GBox_Target.TabStop = false;
            this.GBox_Target.Text = "Target alias";
            // 
            // Panel_Contents
            // 
            this.Panel_Contents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Contents.Location = new System.Drawing.Point(160, 0);
            this.Panel_Contents.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Contents.Name = "Panel_Contents";
            this.Panel_Contents.Size = new System.Drawing.Size(481, 44);
            this.Panel_Contents.TabIndex = 2;
            // 
            // RBtn_ModeLog
            // 
            this.RBtn_ModeLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RBtn_ModeLog.Appearance = System.Windows.Forms.Appearance.Button;
            this.RBtn_ModeLog.FlatAppearance.BorderSize = 0;
            this.RBtn_ModeLog.Image = RtsCore.Resource.Images.play2_32x32;
            this.RBtn_ModeLog.Location = new System.Drawing.Point(730, 5);
            this.RBtn_ModeLog.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.RBtn_ModeLog.Name = "RBtn_ModeLog";
            this.RBtn_ModeLog.Size = new System.Drawing.Size(42, 38);
            this.RBtn_ModeLog.TabIndex = 5;
            this.RBtn_ModeLog.UseVisualStyleBackColor = true;
            this.RBtn_ModeLog.CheckedChanged += new System.EventHandler(this.Mode_CheckedChanged);
            // 
            // RBtn_ModeFile
            // 
            this.RBtn_ModeFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RBtn_ModeFile.Appearance = System.Windows.Forms.Appearance.Button;
            this.RBtn_ModeFile.FlatAppearance.BorderSize = 0;
            this.RBtn_ModeFile.Image = RtsCore.Resource.Images.file_32x32;
            this.RBtn_ModeFile.Location = new System.Drawing.Point(686, 5);
            this.RBtn_ModeFile.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.RBtn_ModeFile.Name = "RBtn_ModeFile";
            this.RBtn_ModeFile.Size = new System.Drawing.Size(42, 38);
            this.RBtn_ModeFile.TabIndex = 4;
            this.RBtn_ModeFile.UseVisualStyleBackColor = true;
            this.RBtn_ModeFile.CheckedChanged += new System.EventHandler(this.Mode_CheckedChanged);
            // 
            // RBtn_ModeData
            // 
            this.RBtn_ModeData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.RBtn_ModeData.Appearance = System.Windows.Forms.Appearance.Button;
            this.RBtn_ModeData.Checked = true;
            this.RBtn_ModeData.Image = RtsCore.Resource.Images.pen_32x32;
            this.RBtn_ModeData.Location = new System.Drawing.Point(642, 5);
            this.RBtn_ModeData.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.RBtn_ModeData.Name = "RBtn_ModeData";
            this.RBtn_ModeData.Size = new System.Drawing.Size(42, 38);
            this.RBtn_ModeData.TabIndex = 3;
            this.RBtn_ModeData.TabStop = true;
            this.RBtn_ModeData.UseVisualStyleBackColor = true;
            this.RBtn_ModeData.CheckedChanged += new System.EventHandler(this.Mode_CheckedChanged);
            // 
            // CBox_TargetList
            // 
            this.CBox_TargetList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CBox_TargetList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CBox_TargetList.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBox_TargetList.FormattingEnabled = true;
            this.CBox_TargetList.Location = new System.Drawing.Point(3, 17);
            this.CBox_TargetList.Name = "CBox_TargetList";
            this.CBox_TargetList.Size = new System.Drawing.Size(151, 20);
            this.CBox_TargetList.TabIndex = 0;
            this.CBox_TargetList.DropDown += new System.EventHandler(this.CBox_TargetList_DropDown);
            this.CBox_TargetList.TextChanged += new System.EventHandler(this.CBox_TargetList_TextChanged);
            this.CBox_TargetList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CBox_TargetList_KeyDown);
            // 
            // MainWindow_SendPanelContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RBtn_ModeLog);
            this.Controls.Add(this.RBtn_ModeFile);
            this.Controls.Add(this.RBtn_ModeData);
            this.Controls.Add(this.Panel_Contents);
            this.Controls.Add(this.GBox_Target);
            this.MaximumSize = new System.Drawing.Size(4096, 44);
            this.MinimumSize = new System.Drawing.Size(0, 44);
            this.Name = "MainWindow_SendPanelContainer";
            this.Size = new System.Drawing.Size(773, 44);
            this.GBox_Target.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox GBox_Target;
        private RtsCore.Framework.Controls.ComboBoxEx CBox_TargetList;
        private System.Windows.Forms.Panel Panel_Contents;
        private System.Windows.Forms.RadioButton RBtn_ModeData;
        private System.Windows.Forms.RadioButton RBtn_ModeFile;
        private System.Windows.Forms.RadioButton RBtn_ModeLog;
    }
}
