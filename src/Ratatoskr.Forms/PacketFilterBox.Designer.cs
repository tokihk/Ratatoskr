namespace Ratatoskr.Forms
{
    partial class PacketFilterBox
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
            this.CBox_Filter = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // CBox_Filter
            // 
            this.CBox_Filter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_Filter.FormattingEnabled = true;
            this.CBox_Filter.Location = new System.Drawing.Point(0, 0);
            this.CBox_Filter.Name = "CBox_Filter";
            this.CBox_Filter.Size = new System.Drawing.Size(561, 20);
            this.CBox_Filter.TabIndex = 2;
            // 
            // PacketFilterBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CBox_Filter);
            this.Name = "PacketFilterBox";
            this.Size = new System.Drawing.Size(561, 20);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox CBox_Filter;
    }
}
