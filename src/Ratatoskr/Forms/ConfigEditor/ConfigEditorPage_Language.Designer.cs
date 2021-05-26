namespace Ratatoskr.Forms.ConfigEditor
{
    partial class ConfigEditorPage_Language
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
            this.GBox_LangType = new System.Windows.Forms.GroupBox();
            this.Btn_Lang_Ref = new System.Windows.Forms.Button();
            this.CBox_LangType = new System.Windows.Forms.ComboBox();
            this.GBox_WordList = new System.Windows.Forms.GroupBox();
            this.LView_WordList = new System.Windows.Forms.ListView();
            this.Btn_LangData_Edit = new System.Windows.Forms.Button();
            this.Btn_LangData_Export = new System.Windows.Forms.Button();
            this.GBox_LangType.SuspendLayout();
            this.GBox_WordList.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBox_LangType
            // 
            this.GBox_LangType.Controls.Add(this.Btn_Lang_Ref);
            this.GBox_LangType.Controls.Add(this.CBox_LangType);
            this.GBox_LangType.Location = new System.Drawing.Point(3, 3);
            this.GBox_LangType.Name = "GBox_LangType";
            this.GBox_LangType.Size = new System.Drawing.Size(320, 49);
            this.GBox_LangType.TabIndex = 4;
            this.GBox_LangType.TabStop = false;
            this.GBox_LangType.Text = "Current language";
            // 
            // Btn_Lang_Ref
            // 
            this.Btn_Lang_Ref.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Lang_Ref.Location = new System.Drawing.Point(243, 18);
            this.Btn_Lang_Ref.Name = "Btn_Lang_Ref";
            this.Btn_Lang_Ref.Size = new System.Drawing.Size(71, 20);
            this.Btn_Lang_Ref.TabIndex = 2;
            this.Btn_Lang_Ref.Text = "Browse...";
            this.Btn_Lang_Ref.UseVisualStyleBackColor = true;
            // 
            // CBox_LangType
            // 
            this.CBox_LangType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_LangType.FormattingEnabled = true;
            this.CBox_LangType.Location = new System.Drawing.Point(7, 19);
            this.CBox_LangType.Name = "CBox_LangType";
            this.CBox_LangType.Size = new System.Drawing.Size(230, 20);
            this.CBox_LangType.TabIndex = 0;
            // 
            // GBox_WordList
            // 
            this.GBox_WordList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_WordList.Controls.Add(this.LView_WordList);
            this.GBox_WordList.Location = new System.Drawing.Point(3, 58);
            this.GBox_WordList.Name = "GBox_WordList";
            this.GBox_WordList.Size = new System.Drawing.Size(514, 191);
            this.GBox_WordList.TabIndex = 5;
            this.GBox_WordList.TabStop = false;
            this.GBox_WordList.Text = "Word list";
            // 
            // LView_WordList
            // 
            this.LView_WordList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LView_WordList.Location = new System.Drawing.Point(7, 18);
            this.LView_WordList.Name = "LView_WordList";
            this.LView_WordList.Size = new System.Drawing.Size(501, 167);
            this.LView_WordList.TabIndex = 0;
            this.LView_WordList.UseCompatibleStateImageBehavior = false;
            // 
            // Btn_LangData_Edit
            // 
            this.Btn_LangData_Edit.Location = new System.Drawing.Point(329, 15);
            this.Btn_LangData_Edit.Name = "Btn_LangData_Edit";
            this.Btn_LangData_Edit.Size = new System.Drawing.Size(80, 32);
            this.Btn_LangData_Edit.TabIndex = 1;
            this.Btn_LangData_Edit.Text = "Customize";
            this.Btn_LangData_Edit.UseVisualStyleBackColor = true;
            // 
            // Btn_LangData_Export
            // 
            this.Btn_LangData_Export.Location = new System.Drawing.Point(415, 15);
            this.Btn_LangData_Export.Name = "Btn_LangData_Export";
            this.Btn_LangData_Export.Size = new System.Drawing.Size(80, 32);
            this.Btn_LangData_Export.TabIndex = 6;
            this.Btn_LangData_Export.Text = "Export...";
            this.Btn_LangData_Export.UseVisualStyleBackColor = true;
            // 
            // ConfigPage_Language
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Btn_LangData_Export);
            this.Controls.Add(this.Btn_LangData_Edit);
            this.Controls.Add(this.GBox_WordList);
            this.Controls.Add(this.GBox_LangType);
            this.Name = "ConfigPage_Language";
            this.Size = new System.Drawing.Size(520, 253);
            this.GBox_LangType.ResumeLayout(false);
            this.GBox_WordList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox GBox_LangType;
        private System.Windows.Forms.GroupBox GBox_WordList;
        private System.Windows.Forms.ComboBox CBox_LangType;
        private System.Windows.Forms.Button Btn_Lang_Ref;
        private System.Windows.Forms.ListView LView_WordList;
        private System.Windows.Forms.Button Btn_LangData_Edit;
        private System.Windows.Forms.Button Btn_LangData_Export;
    }
}
