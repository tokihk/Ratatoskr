namespace Ratatoskr.Forms.OptionEditForm
{
    partial class OptionEditPage_AutoUpdate
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
            this.ChkBox_AutoUpdate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ChkBox_AutoUpdate
            // 
            this.ChkBox_AutoUpdate.Location = new System.Drawing.Point(3, 9);
            this.ChkBox_AutoUpdate.Name = "ChkBox_AutoUpdate";
            this.ChkBox_AutoUpdate.Size = new System.Drawing.Size(260, 24);
            this.ChkBox_AutoUpdate.TabIndex = 0;
            this.ChkBox_AutoUpdate.Text = "Automatically update applications";
            this.ChkBox_AutoUpdate.UseVisualStyleBackColor = true;
            // 
            // ConfigPage_AutoUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ChkBox_AutoUpdate);
            this.Name = "ConfigPage_AutoUpdate";
            this.Size = new System.Drawing.Size(441, 227);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox ChkBox_AutoUpdate;
    }
}
