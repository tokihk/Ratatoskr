namespace RtsCore.Framework.Controls
{
    partial class TreeListView
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
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
            this.HScroll_Main = new System.Windows.Forms.HScrollBar();
            this.Panel_Data = new System.Windows.Forms.Panel();
            this.Panel_Header = new System.Windows.Forms.Panel();
            this.VScroll_Main = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // HScroll_Main
            // 
            this.HScroll_Main.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.HScroll_Main.Location = new System.Drawing.Point(0, 236);
            this.HScroll_Main.Name = "HScroll_Main";
            this.HScroll_Main.Size = new System.Drawing.Size(580, 17);
            this.HScroll_Main.TabIndex = 0;
            // 
            // Panel_Data
            // 
            this.Panel_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Data.Location = new System.Drawing.Point(0, 0);
            this.Panel_Data.Name = "Panel_Data";
            this.Panel_Data.Size = new System.Drawing.Size(580, 236);
            this.Panel_Data.TabIndex = 1;
            // 
            // Panel_Header
            // 
            this.Panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Panel_Header.Location = new System.Drawing.Point(0, 0);
            this.Panel_Header.Name = "Panel_Header";
            this.Panel_Header.Size = new System.Drawing.Size(580, 28);
            this.Panel_Header.TabIndex = 2;
            // 
            // VScroll_Main
            // 
            this.VScroll_Main.Dock = System.Windows.Forms.DockStyle.Right;
            this.VScroll_Main.Location = new System.Drawing.Point(563, 28);
            this.VScroll_Main.Name = "VScroll_Main";
            this.VScroll_Main.Size = new System.Drawing.Size(17, 208);
            this.VScroll_Main.TabIndex = 3;
            // 
            // TreeListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Panel_Data);
            this.Controls.Add(this.HScroll_Main);
            this.Controls.Add(this.VScroll_Main);
            this.Controls.Add(this.Panel_Header);
            this.Name = "TreeListView";
            this.Size = new System.Drawing.Size(580, 253);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HScrollBar HScroll_Main;
        private System.Windows.Forms.Panel Panel_Data;
        private System.Windows.Forms.Panel Panel_Header;
        private System.Windows.Forms.VScrollBar VScroll_Main;
    }
}
