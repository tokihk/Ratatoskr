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
            this.Btn_ControlPanel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_Main
            // 
            this.Btn_Main.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.Btn_Main.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Main.Font = new System.Drawing.Font("Meiryo UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_Main.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_Main.Location = new System.Drawing.Point(0, 0);
            this.Btn_Main.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_Main.Name = "Btn_Main";
            this.Btn_Main.Size = new System.Drawing.Size(160, 30);
            this.Btn_Main.TabIndex = 0;
            this.Btn_Main.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_Main.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Btn_Main.UseVisualStyleBackColor = true;
            this.Btn_Main.Paint += new System.Windows.Forms.PaintEventHandler(this.Btn_Main_Paint);
            this.Btn_Main.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
            this.Btn_Main.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            // 
            // Btn_ControlPanel
            // 
            this.Btn_ControlPanel.FlatAppearance.BorderSize = 0;
            this.Btn_ControlPanel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_ControlPanel.Image = global::Ratatoskr.Properties.Resources.setting00_16x16;
            this.Btn_ControlPanel.Location = new System.Drawing.Point(160, 0);
            this.Btn_ControlPanel.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_ControlPanel.Name = "Btn_ControlPanel";
            this.Btn_ControlPanel.Size = new System.Drawing.Size(30, 30);
            this.Btn_ControlPanel.TabIndex = 1;
            this.Btn_ControlPanel.UseVisualStyleBackColor = true;
            this.Btn_ControlPanel.Click += new System.EventHandler(this.Btn_ControlPanel_Click);
            // 
            // MainFrameGate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 11F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Btn_ControlPanel);
            this.Controls.Add(this.Btn_Main);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Name = "MainFrameGate";
            this.Size = new System.Drawing.Size(190, 30);
            this.ResumeLayout(false);

        }

        #endregion
        private ButtonEx Btn_Main;
        private System.Windows.Forms.Button Btn_ControlPanel;
    }
}
