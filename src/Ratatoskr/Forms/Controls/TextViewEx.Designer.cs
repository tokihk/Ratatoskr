namespace Ratatoskr.Forms.Controls
{
    partial class TextViewEx
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
            this.VScrollBar_Text = new System.Windows.Forms.VScrollBar();
            this.Panel_Text = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // VScrollBar_Text
            // 
            this.VScrollBar_Text.Dock = System.Windows.Forms.DockStyle.Right;
            this.VScrollBar_Text.Location = new System.Drawing.Point(364, 0);
            this.VScrollBar_Text.Name = "VScrollBar_Text";
            this.VScrollBar_Text.Size = new System.Drawing.Size(17, 258);
            this.VScrollBar_Text.TabIndex = 0;
            // 
            // Panel_Text
            // 
            this.Panel_Text.BackColor = System.Drawing.SystemColors.Window;
            this.Panel_Text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Text.Location = new System.Drawing.Point(0, 0);
            this.Panel_Text.Name = "Panel_Text";
            this.Panel_Text.Size = new System.Drawing.Size(364, 258);
            this.Panel_Text.TabIndex = 1;
            // 
            // TextViewEx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Panel_Text);
            this.Controls.Add(this.VScrollBar_Text);
            this.Name = "TextViewEx";
            this.Size = new System.Drawing.Size(381, 258);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar VScrollBar_Text;
        private System.Windows.Forms.Panel Panel_Text;
    }
}
