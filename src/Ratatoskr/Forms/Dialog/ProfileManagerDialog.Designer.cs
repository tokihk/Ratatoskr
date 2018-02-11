namespace Ratatoskr.Forms.Dialog
{
    partial class ProfileManagerDialog
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
            this.LView_ProfileList = new System.Windows.Forms.ListView();
            this.Btn_Add = new System.Windows.Forms.Button();
            this.Btn_Import = new System.Windows.Forms.Button();
            this.Btn_Remove = new System.Windows.Forms.Button();
            this.Btn_Export = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LView_ProfileList
            // 
            this.LView_ProfileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LView_ProfileList.HideSelection = false;
            this.LView_ProfileList.Location = new System.Drawing.Point(12, 50);
            this.LView_ProfileList.MultiSelect = false;
            this.LView_ProfileList.Name = "LView_ProfileList";
            this.LView_ProfileList.Size = new System.Drawing.Size(420, 260);
            this.LView_ProfileList.TabIndex = 0;
            this.LView_ProfileList.UseCompatibleStateImageBehavior = false;
            this.LView_ProfileList.View = System.Windows.Forms.View.Details;
            // 
            // Btn_Add
            // 
            this.Btn_Add.Location = new System.Drawing.Point(12, 12);
            this.Btn_Add.Name = "Btn_Add";
            this.Btn_Add.Size = new System.Drawing.Size(80, 32);
            this.Btn_Add.TabIndex = 1;
            this.Btn_Add.Text = "Add";
            this.Btn_Add.UseVisualStyleBackColor = true;
            this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // Btn_Import
            // 
            this.Btn_Import.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Import.Location = new System.Drawing.Point(266, 12);
            this.Btn_Import.Name = "Btn_Import";
            this.Btn_Import.Size = new System.Drawing.Size(80, 32);
            this.Btn_Import.TabIndex = 2;
            this.Btn_Import.Text = "Import";
            this.Btn_Import.UseVisualStyleBackColor = true;
            this.Btn_Import.Click += new System.EventHandler(this.Btn_Import_Click);
            // 
            // Btn_Remove
            // 
            this.Btn_Remove.Location = new System.Drawing.Point(98, 12);
            this.Btn_Remove.Name = "Btn_Remove";
            this.Btn_Remove.Size = new System.Drawing.Size(80, 32);
            this.Btn_Remove.TabIndex = 3;
            this.Btn_Remove.Text = "Remove";
            this.Btn_Remove.UseVisualStyleBackColor = true;
            this.Btn_Remove.Click += new System.EventHandler(this.Btn_Remove_Click);
            // 
            // Btn_Export
            // 
            this.Btn_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Export.Location = new System.Drawing.Point(352, 12);
            this.Btn_Export.Name = "Btn_Export";
            this.Btn_Export.Size = new System.Drawing.Size(80, 32);
            this.Btn_Export.TabIndex = 4;
            this.Btn_Export.Text = "Export";
            this.Btn_Export.UseVisualStyleBackColor = true;
            this.Btn_Export.Click += new System.EventHandler(this.Btn_Export_Click);
            // 
            // ProfileManagerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 322);
            this.Controls.Add(this.Btn_Export);
            this.Controls.Add(this.Btn_Remove);
            this.Controls.Add(this.Btn_Import);
            this.Controls.Add(this.Btn_Add);
            this.Controls.Add(this.LView_ProfileList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProfileManagerDialog";
            this.ShowIcon = false;
            this.Text = "Profile Manager";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView LView_ProfileList;
        private System.Windows.Forms.Button Btn_Add;
        private System.Windows.Forms.Button Btn_Import;
        private System.Windows.Forms.Button Btn_Remove;
        private System.Windows.Forms.Button Btn_Export;
    }
}