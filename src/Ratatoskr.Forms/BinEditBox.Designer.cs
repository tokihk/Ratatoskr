namespace Ratatoskr.Forms
{
    partial class BinEditBox
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
            this.components = new System.ComponentModel.Container();
            this.TLPanel_Main = new System.Windows.Forms.TableLayoutPanel();
            this.Panel_Address_Page = new PanelEx();
            this.Panel_DataBin = new PanelEx();
            this.Panel_DataText = new PanelEx();
            this.Panel_AddressOffset_Bin = new PanelEx();
            this.Panel_AddressOffset_Text = new PanelEx();
            this.Panel_Address = new PanelEx();
            this.VScrl_Data = new System.Windows.Forms.VScrollBar();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TLPanel_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // TLPanel_Main
            // 
            this.TLPanel_Main.ColumnCount = 4;
            this.TLPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.TLPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.TLPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLPanel_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.TLPanel_Main.Controls.Add(this.Panel_Address_Page, 0, 1);
            this.TLPanel_Main.Controls.Add(this.Panel_DataBin, 1, 1);
            this.TLPanel_Main.Controls.Add(this.Panel_DataText, 2, 1);
            this.TLPanel_Main.Controls.Add(this.Panel_AddressOffset_Bin, 1, 0);
            this.TLPanel_Main.Controls.Add(this.Panel_AddressOffset_Text, 2, 0);
            this.TLPanel_Main.Controls.Add(this.Panel_Address, 0, 0);
            this.TLPanel_Main.Controls.Add(this.VScrl_Data, 3, 1);
            this.TLPanel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TLPanel_Main.Location = new System.Drawing.Point(0, 0);
            this.TLPanel_Main.Name = "TLPanel_Main";
            this.TLPanel_Main.RowCount = 2;
            this.TLPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TLPanel_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.TLPanel_Main.Size = new System.Drawing.Size(741, 545);
            this.TLPanel_Main.TabIndex = 0;
            // 
            // Panel_Address_Page
            // 
            this.Panel_Address_Page.AutoSize = true;
            this.Panel_Address_Page.BackColor = System.Drawing.SystemColors.Window;
            this.Panel_Address_Page.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Address_Page.Location = new System.Drawing.Point(1, 31);
            this.Panel_Address_Page.Margin = new System.Windows.Forms.Padding(1);
            this.Panel_Address_Page.Name = "Panel_Address_Page";
            this.Panel_Address_Page.Size = new System.Drawing.Size(118, 513);
            this.Panel_Address_Page.TabIndex = 3;
            this.Panel_Address_Page.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Address_Page_Paint);
            this.Panel_Address_Page.Resize += new System.EventHandler(this.Panel_Address_Page_Resize);
            // 
            // Panel_DataBin
            // 
            this.Panel_DataBin.BackColor = System.Drawing.SystemColors.Window;
            this.Panel_DataBin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_DataBin.Location = new System.Drawing.Point(121, 31);
            this.Panel_DataBin.Margin = new System.Windows.Forms.Padding(1);
            this.Panel_DataBin.Name = "Panel_DataBin";
            this.Panel_DataBin.Size = new System.Drawing.Size(398, 513);
            this.Panel_DataBin.TabIndex = 4;
            this.Panel_DataBin.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_DataBin_Paint);
            this.Panel_DataBin.Enter += new System.EventHandler(this.OnFocusUpdated);
            this.Panel_DataBin.Leave += new System.EventHandler(this.OnFocusUpdated);
            this.Panel_DataBin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_DataBin_MouseDown);
            this.Panel_DataBin.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_DataBin_MouseMove);
            this.Panel_DataBin.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_DataBin_MouseUp);
            this.Panel_DataBin.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Panel_DataBin_PreviewKeyDown);
            // 
            // Panel_DataText
            // 
            this.Panel_DataText.BackColor = System.Drawing.SystemColors.Window;
            this.Panel_DataText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_DataText.Location = new System.Drawing.Point(521, 31);
            this.Panel_DataText.Margin = new System.Windows.Forms.Padding(1);
            this.Panel_DataText.Name = "Panel_DataText";
            this.Panel_DataText.Size = new System.Drawing.Size(199, 513);
            this.Panel_DataText.TabIndex = 5;
            this.Panel_DataText.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_DataText_Paint);
            this.Panel_DataText.Enter += new System.EventHandler(this.OnFocusUpdated);
            this.Panel_DataText.Leave += new System.EventHandler(this.OnFocusUpdated);
            this.Panel_DataText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_DataText_MouseDown);
            this.Panel_DataText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_DataText_MouseMove);
            this.Panel_DataText.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_DataText_MouseUp);
            this.Panel_DataText.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Panel_DataText_PreviewKeyDown);
            // 
            // Panel_AddressOffset_Bin
            // 
            this.Panel_AddressOffset_Bin.AutoSize = true;
            this.Panel_AddressOffset_Bin.BackColor = System.Drawing.SystemColors.Window;
            this.Panel_AddressOffset_Bin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_AddressOffset_Bin.Location = new System.Drawing.Point(121, 1);
            this.Panel_AddressOffset_Bin.Margin = new System.Windows.Forms.Padding(1);
            this.Panel_AddressOffset_Bin.Name = "Panel_AddressOffset_Bin";
            this.Panel_AddressOffset_Bin.Size = new System.Drawing.Size(398, 28);
            this.Panel_AddressOffset_Bin.TabIndex = 1;
            this.Panel_AddressOffset_Bin.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_AddressOffset_Bin_Paint);
            // 
            // Panel_AddressOffset_Text
            // 
            this.Panel_AddressOffset_Text.BackColor = System.Drawing.SystemColors.Window;
            this.Panel_AddressOffset_Text.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_AddressOffset_Text.Location = new System.Drawing.Point(521, 1);
            this.Panel_AddressOffset_Text.Margin = new System.Windows.Forms.Padding(1);
            this.Panel_AddressOffset_Text.Name = "Panel_AddressOffset_Text";
            this.Panel_AddressOffset_Text.Size = new System.Drawing.Size(199, 28);
            this.Panel_AddressOffset_Text.TabIndex = 2;
            this.Panel_AddressOffset_Text.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_AddressOffset_Text_Paint);
            // 
            // Panel_Address
            // 
            this.Panel_Address.AutoSize = true;
            this.Panel_Address.BackColor = System.Drawing.SystemColors.Window;
            this.Panel_Address.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Address.Location = new System.Drawing.Point(1, 1);
            this.Panel_Address.Margin = new System.Windows.Forms.Padding(1);
            this.Panel_Address.Name = "Panel_Address";
            this.Panel_Address.Size = new System.Drawing.Size(118, 28);
            this.Panel_Address.TabIndex = 0;
            this.Panel_Address.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Address_Paint);
            // 
            // VScrl_Data
            // 
            this.VScrl_Data.Location = new System.Drawing.Point(721, 30);
            this.VScrl_Data.Name = "VScrl_Data";
            this.VScrl_Data.Size = new System.Drawing.Size(20, 495);
            this.VScrl_Data.TabIndex = 6;
            this.VScrl_Data.ValueChanged += new System.EventHandler(this.VScrl_Data_ValueChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // HexEditBox
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.TLPanel_Main);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "HexEditBox";
            this.Size = new System.Drawing.Size(741, 545);
            this.FontChanged += new System.EventHandler(this.HexEditBox_FontChanged);
            this.SizeChanged += new System.EventHandler(this.HexEditBox_SizeChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.HexEditBox_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.HexEditBox_DragEnter);
            this.TLPanel_Main.ResumeLayout(false);
            this.TLPanel_Main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TLPanel_Main;
        private PanelEx Panel_Address;
        private PanelEx Panel_Address_Page;
        private PanelEx Panel_DataBin;
        private PanelEx Panel_DataText;
        private PanelEx Panel_AddressOffset_Bin;
        private PanelEx Panel_AddressOffset_Text;
        private System.Windows.Forms.VScrollBar VScrl_Data;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}
