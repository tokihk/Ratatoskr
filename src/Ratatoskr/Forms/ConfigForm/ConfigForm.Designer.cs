namespace Ratatoskr.Forms.ConfigForm
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.Btn_Ok = new System.Windows.Forms.Button();
            this.Split_Main = new System.Windows.Forms.SplitContainer();
            this.TView_Menu = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Panel_PageContents = new System.Windows.Forms.Panel();
            this.Label_PageTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Split_Main)).BeginInit();
            this.Split_Main.Panel1.SuspendLayout();
            this.Split_Main.Panel2.SuspendLayout();
            this.Split_Main.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Cancel.Location = new System.Drawing.Point(662, 400);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(90, 30);
            this.Btn_Cancel.TabIndex = 0;
            this.Btn_Cancel.Text = "キャンセル";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Btn_Ok
            // 
            this.Btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Ok.Location = new System.Drawing.Point(566, 400);
            this.Btn_Ok.Name = "Btn_Ok";
            this.Btn_Ok.Size = new System.Drawing.Size(90, 30);
            this.Btn_Ok.TabIndex = 1;
            this.Btn_Ok.Text = "OK";
            this.Btn_Ok.UseVisualStyleBackColor = true;
            this.Btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            // 
            // Split_Main
            // 
            this.Split_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Split_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.Split_Main.IsSplitterFixed = true;
            this.Split_Main.Location = new System.Drawing.Point(13, 13);
            this.Split_Main.Name = "Split_Main";
            // 
            // Split_Main.Panel1
            // 
            this.Split_Main.Panel1.Controls.Add(this.TView_Menu);
            // 
            // Split_Main.Panel2
            // 
            this.Split_Main.Panel2.Controls.Add(this.panel1);
            this.Split_Main.Size = new System.Drawing.Size(739, 381);
            this.Split_Main.SplitterDistance = 209;
            this.Split_Main.TabIndex = 2;
            // 
            // TView_Menu
            // 
            this.TView_Menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TView_Menu.Location = new System.Drawing.Point(0, 0);
            this.TView_Menu.Name = "TView_Menu";
            this.TView_Menu.Size = new System.Drawing.Size(209, 381);
            this.TView_Menu.TabIndex = 0;
            this.TView_Menu.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TView_Menu_NodeMouseClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Panel_PageContents);
            this.panel1.Controls.Add(this.Label_PageTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(526, 381);
            this.panel1.TabIndex = 0;
            // 
            // Panel_PageContents
            // 
            this.Panel_PageContents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_PageContents.Location = new System.Drawing.Point(3, 30);
            this.Panel_PageContents.Name = "Panel_PageContents";
            this.Panel_PageContents.Size = new System.Drawing.Size(520, 348);
            this.Panel_PageContents.TabIndex = 1;
            // 
            // Label_PageTitle
            // 
            this.Label_PageTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_PageTitle.BackColor = System.Drawing.Color.LightSkyBlue;
            this.Label_PageTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Label_PageTitle.Font = new System.Drawing.Font("メイリオ", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Label_PageTitle.Location = new System.Drawing.Point(4, 4);
            this.Label_PageTitle.Name = "Label_PageTitle";
            this.Label_PageTitle.Size = new System.Drawing.Size(519, 23);
            this.Label_PageTitle.TabIndex = 0;
            this.Label_PageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 442);
            this.Controls.Add(this.Split_Main);
            this.Controls.Add(this.Btn_Ok);
            this.Controls.Add(this.Btn_Cancel);
            this.Name = "ConfigForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConfigForm";
            this.Split_Main.Panel1.ResumeLayout(false);
            this.Split_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Split_Main)).EndInit();
            this.Split_Main.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.Button Btn_Ok;
        private System.Windows.Forms.SplitContainer Split_Main;
        private System.Windows.Forms.TreeView TView_Menu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel Panel_PageContents;
        private System.Windows.Forms.Label Label_PageTitle;
    }
}