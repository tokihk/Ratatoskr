namespace Ratatoskr.Forms.MainFrame
{
    partial class MainFramePacketView
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
            this.Panel_Contents = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TBox_Filter = new System.Windows.Forms.TextBox();
            this.ChkBox_Filter = new System.Windows.Forms.CheckBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Contents
            // 
            this.Panel_Contents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Contents.Location = new System.Drawing.Point(0, 35);
            this.Panel_Contents.Name = "Panel_Contents";
            this.Panel_Contents.Size = new System.Drawing.Size(872, 472);
            this.Panel_Contents.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.TBox_Filter);
            this.panel2.Controls.Add(this.ChkBox_Filter);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(872, 35);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(1, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(869, 4);
            this.panel1.TabIndex = 2;
            // 
            // TBox_Filter
            // 
            this.TBox_Filter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBox_Filter.Location = new System.Drawing.Point(60, 3);
            this.TBox_Filter.Name = "TBox_Filter";
            this.TBox_Filter.Size = new System.Drawing.Size(809, 19);
            this.TBox_Filter.TabIndex = 1;
            this.TBox_Filter.TextChanged += new System.EventHandler(this.TBox_Filter_TextChanged);
            this.TBox_Filter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBox_Filter_KeyDown);
            // 
            // ChkBox_Filter
            // 
            this.ChkBox_Filter.AutoSize = true;
            this.ChkBox_Filter.Location = new System.Drawing.Point(3, 5);
            this.ChkBox_Filter.Name = "ChkBox_Filter";
            this.ChkBox_Filter.Size = new System.Drawing.Size(51, 16);
            this.ChkBox_Filter.TabIndex = 0;
            this.ChkBox_Filter.Text = "Filter";
            this.ChkBox_Filter.UseVisualStyleBackColor = true;
            this.ChkBox_Filter.CheckedChanged += new System.EventHandler(this.ChkBox_Filter_CheckedChanged);
            // 
            // MainFramePacketView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Panel_Contents);
            this.Controls.Add(this.panel2);
            this.Name = "MainFramePacketView";
            this.Size = new System.Drawing.Size(872, 507);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Contents;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox TBox_Filter;
        private System.Windows.Forms.CheckBox ChkBox_Filter;
        private System.Windows.Forms.Panel panel1;
    }
}
