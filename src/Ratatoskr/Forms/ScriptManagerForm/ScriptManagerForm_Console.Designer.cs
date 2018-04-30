namespace Ratatoskr.Forms.ScriptManagerForm
{
    partial class ScriptManagerForm_Console
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
            this.RTBox_Output = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // RTBox_Output
            // 
            this.RTBox_Output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RTBox_Output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RTBox_Output.Location = new System.Drawing.Point(0, 0);
            this.RTBox_Output.Name = "RTBox_Output";
            this.RTBox_Output.ReadOnly = true;
            this.RTBox_Output.Size = new System.Drawing.Size(431, 388);
            this.RTBox_Output.TabIndex = 0;
            this.RTBox_Output.Text = "";
            // 
            // ScriptManagerForm_Console
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.RTBox_Output);
            this.Name = "ScriptManagerForm_Console";
            this.Size = new System.Drawing.Size(431, 388);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox RTBox_Output;
    }
}
