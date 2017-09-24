namespace Ratatoskr.Forms.MainFrame
{
    partial class MainFrameGate
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
            this.Btn_Main = new Ratatoskr.Forms.MainFrame.MainFrameGate.ButtonEx();
            this.PBox_DataRate = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PBox_DataRate)).BeginInit();
            this.SuspendLayout();
            // 
            // Btn_Main
            // 
            this.Btn_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Btn_Main.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.Btn_Main.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Main.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_Main.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_Main.Location = new System.Drawing.Point(0, 0);
            this.Btn_Main.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_Main.Name = "Btn_Main";
            this.Btn_Main.Size = new System.Drawing.Size(160, 32);
            this.Btn_Main.TabIndex = 0;
            this.Btn_Main.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_Main.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Btn_Main.UseVisualStyleBackColor = true;
            this.Btn_Main.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_Main_Paint);
            this.Btn_Main.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.Btn_Main.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // PBox_DataRate
            // 
            this.PBox_DataRate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PBox_DataRate.Location = new System.Drawing.Point(0, 32);
            this.PBox_DataRate.Name = "PBox_DataRate";
            this.PBox_DataRate.Size = new System.Drawing.Size(160, 22);
            this.PBox_DataRate.TabIndex = 1;
            this.PBox_DataRate.TabStop = false;
            this.PBox_DataRate.Paint += new System.Windows.Forms.PaintEventHandler(this.PBox_DataRate_Paint);
            // 
            // MainFrameGate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 11F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Btn_Main);
            this.Controls.Add(this.PBox_DataRate);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Name = "MainFrameGate";
            this.Size = new System.Drawing.Size(160, 54);
            ((System.ComponentModel.ISupportInitialize)(this.PBox_DataRate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ButtonEx Btn_Main;
        private System.Windows.Forms.PictureBox PBox_DataRate;
    }
}
