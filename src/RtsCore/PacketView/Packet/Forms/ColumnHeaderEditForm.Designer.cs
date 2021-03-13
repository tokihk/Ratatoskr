namespace Ratatoskr.PacketViews.Packet.Forms
{
    partial class ColumnHeaderEditForm
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
            this.Btn_Ok = new System.Windows.Forms.Button();
            this.Btn_Cancel = new System.Windows.Forms.Button();
            this.LBox_AllItem = new System.Windows.Forms.ListBox();
            this.LBox_UserItem = new System.Windows.Forms.ListBox();
            this.Btn_Remove = new System.Windows.Forms.Button();
            this.Btn_Add = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.CBox_SelectItem_ItemType = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.TBox_SelectItem_PacketFilter = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.TBox_SelectItem_DisplayText = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Ok
            // 
            this.Btn_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Ok.Location = new System.Drawing.Point(453, 420);
            this.Btn_Ok.Name = "Btn_Ok";
            this.Btn_Ok.Size = new System.Drawing.Size(75, 28);
            this.Btn_Ok.TabIndex = 3;
            this.Btn_Ok.Text = "OK";
            this.Btn_Ok.UseVisualStyleBackColor = true;
            this.Btn_Ok.Click += new System.EventHandler(this.Btn_Ok_Click);
            // 
            // Btn_Cancel
            // 
            this.Btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Cancel.Location = new System.Drawing.Point(534, 420);
            this.Btn_Cancel.Name = "Btn_Cancel";
            this.Btn_Cancel.Size = new System.Drawing.Size(75, 28);
            this.Btn_Cancel.TabIndex = 4;
            this.Btn_Cancel.Text = "Cancel";
            this.Btn_Cancel.UseVisualStyleBackColor = true;
            this.Btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // LBox_AllItem
            // 
            this.LBox_AllItem.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LBox_AllItem.FormattingEnabled = true;
            this.LBox_AllItem.ItemHeight = 12;
            this.LBox_AllItem.Location = new System.Drawing.Point(6, 18);
            this.LBox_AllItem.Name = "LBox_AllItem";
            this.LBox_AllItem.Size = new System.Drawing.Size(238, 244);
            this.LBox_AllItem.TabIndex = 0;
            // 
            // LBox_UserItem
            // 
            this.LBox_UserItem.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LBox_UserItem.FormattingEnabled = true;
            this.LBox_UserItem.ItemHeight = 12;
            this.LBox_UserItem.Location = new System.Drawing.Point(6, 18);
            this.LBox_UserItem.Name = "LBox_UserItem";
            this.LBox_UserItem.Size = new System.Drawing.Size(255, 244);
            this.LBox_UserItem.TabIndex = 5;
            this.LBox_UserItem.SelectedIndexChanged += new System.EventHandler(this.LBox_UserItem_SelectedIndexChanged);
            this.LBox_UserItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LBox_UserItem_MouseDown);
            this.LBox_UserItem.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LBox_UserItem_MouseMove);
            // 
            // Btn_Remove
            // 
            this.Btn_Remove.Location = new System.Drawing.Point(268, 159);
            this.Btn_Remove.Name = "Btn_Remove";
            this.Btn_Remove.Size = new System.Drawing.Size(69, 23);
            this.Btn_Remove.TabIndex = 8;
            this.Btn_Remove.Text = "Remove";
            this.Btn_Remove.UseVisualStyleBackColor = true;
            this.Btn_Remove.Click += new System.EventHandler(this.Btn_Remove_Click);
            // 
            // Btn_Add
            // 
            this.Btn_Add.Location = new System.Drawing.Point(268, 130);
            this.Btn_Add.Name = "Btn_Add";
            this.Btn_Add.Size = new System.Drawing.Size(69, 23);
            this.Btn_Add.TabIndex = 9;
            this.Btn_Add.Text = "Add";
            this.Btn_Add.UseVisualStyleBackColor = true;
            this.Btn_Add.Click += new System.EventHandler(this.Btn_Add_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.LBox_AllItem);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 274);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "All Display Item";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LBox_UserItem);
            this.groupBox2.Location = new System.Drawing.Point(343, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(267, 274);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Current Display Item";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Location = new System.Drawing.Point(12, 293);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(598, 119);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Current Display Item - Select Item Setting";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.CBox_SelectItem_ItemType);
            this.groupBox6.Location = new System.Drawing.Point(215, 18);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(260, 43);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Item Type";
            // 
            // CBox_SelectItem_ItemType
            // 
            this.CBox_SelectItem_ItemType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_SelectItem_ItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_SelectItem_ItemType.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBox_SelectItem_ItemType.FormattingEnabled = true;
            this.CBox_SelectItem_ItemType.Location = new System.Drawing.Point(3, 15);
            this.CBox_SelectItem_ItemType.Name = "CBox_SelectItem_ItemType";
            this.CBox_SelectItem_ItemType.Size = new System.Drawing.Size(254, 20);
            this.CBox_SelectItem_ItemType.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.TBox_SelectItem_PacketFilter);
            this.groupBox5.Location = new System.Drawing.Point(6, 67);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(582, 43);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Packet Filter";
            // 
            // TBox_SelectItem_PacketFilter
            // 
            this.TBox_SelectItem_PacketFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_SelectItem_PacketFilter.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TBox_SelectItem_PacketFilter.Location = new System.Drawing.Point(3, 15);
            this.TBox_SelectItem_PacketFilter.Name = "TBox_SelectItem_PacketFilter";
            this.TBox_SelectItem_PacketFilter.Size = new System.Drawing.Size(576, 19);
            this.TBox_SelectItem_PacketFilter.TabIndex = 0;
            this.TBox_SelectItem_PacketFilter.TextChanged += new System.EventHandler(this.TBox_SelectItem_PacketFilter_TextChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.TBox_SelectItem_DisplayText);
            this.groupBox4.Location = new System.Drawing.Point(9, 18);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 43);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Display Text";
            // 
            // TBox_SelectItem_DisplayText
            // 
            this.TBox_SelectItem_DisplayText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_SelectItem_DisplayText.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TBox_SelectItem_DisplayText.Location = new System.Drawing.Point(3, 15);
            this.TBox_SelectItem_DisplayText.Name = "TBox_SelectItem_DisplayText";
            this.TBox_SelectItem_DisplayText.Size = new System.Drawing.Size(194, 19);
            this.TBox_SelectItem_DisplayText.TabIndex = 0;
            // 
            // ColumnHeaderEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 460);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Btn_Add);
            this.Controls.Add(this.Btn_Remove);
            this.Controls.Add(this.Btn_Cancel);
            this.Controls.Add(this.Btn_Ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColumnHeaderEditForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Column setting";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button Btn_Ok;
        private System.Windows.Forms.Button Btn_Cancel;
        private System.Windows.Forms.ListBox LBox_AllItem;
        private System.Windows.Forms.ListBox LBox_UserItem;
        private System.Windows.Forms.Button Btn_Remove;
        private System.Windows.Forms.Button Btn_Add;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox TBox_SelectItem_PacketFilter;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox TBox_SelectItem_DisplayText;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox CBox_SelectItem_ItemType;
    }
}