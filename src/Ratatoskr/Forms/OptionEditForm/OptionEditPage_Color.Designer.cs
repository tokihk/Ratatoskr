namespace Ratatoskr.Forms.OptionEditForm
{
    partial class OptionEditPage_Color
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Label_Packet_Msg = new System.Windows.Forms.Label();
            this.Btn_Packet_Msg = new System.Windows.Forms.Button();
            this.Btn_Packet_Recv = new System.Windows.Forms.Button();
            this.Btn_Packet_Send = new System.Windows.Forms.Button();
            this.Label_Packet_Recv = new System.Windows.Forms.Label();
            this.Label_Packet_Send = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Label_Packet_Msg);
            this.groupBox4.Controls.Add(this.Btn_Packet_Msg);
            this.groupBox4.Controls.Add(this.Btn_Packet_Recv);
            this.groupBox4.Controls.Add(this.Btn_Packet_Send);
            this.groupBox4.Controls.Add(this.Label_Packet_Recv);
            this.groupBox4.Controls.Add(this.Label_Packet_Send);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(275, 107);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Packet view setting - Packet";
            // 
            // Label_Packet_Msg
            // 
            this.Label_Packet_Msg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Packet_Msg.Location = new System.Drawing.Point(9, 73);
            this.Label_Packet_Msg.Name = "Label_Packet_Msg";
            this.Label_Packet_Msg.Size = new System.Drawing.Size(173, 23);
            this.Label_Packet_Msg.TabIndex = 7;
            this.Label_Packet_Msg.Text = "Message packet";
            this.Label_Packet_Msg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Btn_Packet_Msg
            // 
            this.Btn_Packet_Msg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Packet_Msg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Packet_Msg.Location = new System.Drawing.Point(213, 73);
            this.Btn_Packet_Msg.Name = "Btn_Packet_Msg";
            this.Btn_Packet_Msg.Size = new System.Drawing.Size(50, 23);
            this.Btn_Packet_Msg.TabIndex = 6;
            this.Btn_Packet_Msg.UseVisualStyleBackColor = true;
            this.Btn_Packet_Msg.Click += new System.EventHandler(this.Btn_PacketColor_Click);
            // 
            // Btn_Packet_Recv
            // 
            this.Btn_Packet_Recv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Packet_Recv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Packet_Recv.Location = new System.Drawing.Point(213, 44);
            this.Btn_Packet_Recv.Name = "Btn_Packet_Recv";
            this.Btn_Packet_Recv.Size = new System.Drawing.Size(50, 23);
            this.Btn_Packet_Recv.TabIndex = 5;
            this.Btn_Packet_Recv.UseVisualStyleBackColor = true;
            this.Btn_Packet_Recv.Click += new System.EventHandler(this.Btn_PacketColor_Click);
            // 
            // Btn_Packet_Send
            // 
            this.Btn_Packet_Send.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Packet_Send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Packet_Send.Location = new System.Drawing.Point(213, 15);
            this.Btn_Packet_Send.Name = "Btn_Packet_Send";
            this.Btn_Packet_Send.Size = new System.Drawing.Size(50, 23);
            this.Btn_Packet_Send.TabIndex = 4;
            this.Btn_Packet_Send.UseVisualStyleBackColor = true;
            this.Btn_Packet_Send.Click += new System.EventHandler(this.Btn_PacketColor_Click);
            // 
            // Label_Packet_Recv
            // 
            this.Label_Packet_Recv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Packet_Recv.Location = new System.Drawing.Point(9, 44);
            this.Label_Packet_Recv.Name = "Label_Packet_Recv";
            this.Label_Packet_Recv.Size = new System.Drawing.Size(173, 23);
            this.Label_Packet_Recv.TabIndex = 3;
            this.Label_Packet_Recv.Text = "Receive packet";
            this.Label_Packet_Recv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_Packet_Send
            // 
            this.Label_Packet_Send.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Packet_Send.Location = new System.Drawing.Point(9, 15);
            this.Label_Packet_Send.Name = "Label_Packet_Send";
            this.Label_Packet_Send.Size = new System.Drawing.Size(173, 23);
            this.Label_Packet_Send.TabIndex = 2;
            this.Label_Packet_Send.Text = "Send packet";
            this.Label_Packet_Send.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // OptionEditPage_Color
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Name = "OptionEditPage_Color";
            this.Size = new System.Drawing.Size(281, 153);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label Label_Packet_Send;
        private System.Windows.Forms.Label Label_Packet_Msg;
        private System.Windows.Forms.Button Btn_Packet_Msg;
        private System.Windows.Forms.Button Btn_Packet_Recv;
        private System.Windows.Forms.Button Btn_Packet_Send;
        private System.Windows.Forms.Label Label_Packet_Recv;
    }
}
