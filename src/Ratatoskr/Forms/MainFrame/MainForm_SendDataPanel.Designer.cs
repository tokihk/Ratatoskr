namespace Ratatoskr.Forms.MainFrame
{
    partial class MainForm_SendDataPanel
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
            this.GBox_Exp = new System.Windows.Forms.GroupBox();
            this.Btn_Send = new System.Windows.Forms.Button();
            this.ChkBox_Preview = new System.Windows.Forms.CheckBox();
            this.CBox_ExpList = new System.Windows.Forms.ComboBox();
            this.GBox_Exp.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBox_Exp
            // 
            this.GBox_Exp.Controls.Add(this.Btn_Send);
            this.GBox_Exp.Controls.Add(this.ChkBox_Preview);
            this.GBox_Exp.Controls.Add(this.CBox_ExpList);
            this.GBox_Exp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GBox_Exp.Location = new System.Drawing.Point(0, 0);
            this.GBox_Exp.Name = "GBox_Exp";
            this.GBox_Exp.Size = new System.Drawing.Size(623, 44);
            this.GBox_Exp.TabIndex = 1;
            this.GBox_Exp.TabStop = false;
            this.GBox_Exp.Text = "Send data";
            // 
            // Btn_Send
            // 
            this.Btn_Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Send.Location = new System.Drawing.Point(471, 17);
            this.Btn_Send.Name = "Btn_Send";
            this.Btn_Send.Size = new System.Drawing.Size(55, 21);
            this.Btn_Send.TabIndex = 2;
            this.Btn_Send.Text = "Send";
            this.Btn_Send.UseVisualStyleBackColor = true;
            this.Btn_Send.Click += new System.EventHandler(this.Btn_Send_Click);
            // 
            // ChkBox_Preview
            // 
            this.ChkBox_Preview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChkBox_Preview.Location = new System.Drawing.Point(532, 16);
            this.ChkBox_Preview.Name = "ChkBox_Preview";
            this.ChkBox_Preview.Size = new System.Drawing.Size(85, 24);
            this.ChkBox_Preview.TabIndex = 1;
            this.ChkBox_Preview.Text = "Preview";
            this.ChkBox_Preview.UseVisualStyleBackColor = true;
            this.ChkBox_Preview.CheckedChanged += new System.EventHandler(this.ChkBox_Preview_CheckedChanged);
            // 
            // CBox_ExpList
            // 
            this.CBox_ExpList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CBox_ExpList.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBox_ExpList.FormattingEnabled = true;
            this.CBox_ExpList.Location = new System.Drawing.Point(3, 18);
            this.CBox_ExpList.Name = "CBox_ExpList";
            this.CBox_ExpList.Size = new System.Drawing.Size(462, 20);
            this.CBox_ExpList.TabIndex = 0;
            this.CBox_ExpList.TextChanged += new System.EventHandler(this.CBox_ExpList_TextChanged);
            this.CBox_ExpList.Enter += new System.EventHandler(this.CBox_ExpList_Enter);
            this.CBox_ExpList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CBox_Exp_KeyDown);
            this.CBox_ExpList.Leave += new System.EventHandler(this.CBox_ExpList_Leave);
            // 
            // MainForm_SendDataPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GBox_Exp);
            this.Name = "MainForm_SendDataPanel";
            this.Size = new System.Drawing.Size(623, 44);
            this.GBox_Exp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox GBox_Exp;
        private System.Windows.Forms.ComboBox CBox_ExpList;
        private System.Windows.Forms.CheckBox ChkBox_Preview;
        private System.Windows.Forms.Button Btn_Send;
    }
}
