namespace Ratatoskr.Forms.MainFrame
{
    partial class MainFrameSingleCommandPanel
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
            this.components = new System.ComponentModel.Container();
            this.GBox_CommandList = new System.Windows.Forms.GroupBox();
            this.Btn_FileFunction = new System.Windows.Forms.Button();
            this.Btn_CmdCancel = new System.Windows.Forms.Button();
            this.CBox_CommandList = new System.Windows.Forms.ComboBox();
            this.GBox_Target = new System.Windows.Forms.GroupBox();
            this.CBox_TargetList = new System.Windows.Forms.ComboBox();
            this.CMenu_FileFunction = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Menu_FileFunc_Transfer = new System.Windows.Forms.ToolStripMenuItem();
            this.GBox_CommandList.SuspendLayout();
            this.GBox_Target.SuspendLayout();
            this.CMenu_FileFunction.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBox_CommandList
            // 
            this.GBox_CommandList.AutoSize = true;
            this.GBox_CommandList.Controls.Add(this.Btn_FileFunction);
            this.GBox_CommandList.Controls.Add(this.Btn_CmdCancel);
            this.GBox_CommandList.Controls.Add(this.CBox_CommandList);
            this.GBox_CommandList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GBox_CommandList.Location = new System.Drawing.Point(157, 0);
            this.GBox_CommandList.Name = "GBox_CommandList";
            this.GBox_CommandList.Size = new System.Drawing.Size(421, 44);
            this.GBox_CommandList.TabIndex = 0;
            this.GBox_CommandList.TabStop = false;
            this.GBox_CommandList.Text = "コマンド";
            // 
            // Btn_FileFunction
            // 
            this.Btn_FileFunction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_FileFunction.FlatAppearance.BorderSize = 0;
            this.Btn_FileFunction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_FileFunction.Image = global::Ratatoskr.Properties.Resources.document_24x24;
            this.Btn_FileFunction.Location = new System.Drawing.Point(389, 11);
            this.Btn_FileFunction.Name = "Btn_FileFunction";
            this.Btn_FileFunction.Size = new System.Drawing.Size(26, 26);
            this.Btn_FileFunction.TabIndex = 2;
            this.Btn_FileFunction.UseVisualStyleBackColor = true;
            this.Btn_FileFunction.Click += new System.EventHandler(this.Btn_FileFunction_Click);
            // 
            // Btn_CmdCancel
            // 
            this.Btn_CmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_CmdCancel.FlatAppearance.BorderSize = 0;
            this.Btn_CmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_CmdCancel.Image = global::Ratatoskr.Properties.Resources.cancel_red_16x16;
            this.Btn_CmdCancel.Location = new System.Drawing.Point(360, 11);
            this.Btn_CmdCancel.Margin = new System.Windows.Forms.Padding(0);
            this.Btn_CmdCancel.Name = "Btn_CmdCancel";
            this.Btn_CmdCancel.Size = new System.Drawing.Size(26, 26);
            this.Btn_CmdCancel.TabIndex = 1;
            this.Btn_CmdCancel.UseVisualStyleBackColor = true;
            this.Btn_CmdCancel.Visible = false;
            this.Btn_CmdCancel.Click += new System.EventHandler(this.Btn_CmdCancel_Click);
            // 
            // CBox_CommandList
            // 
            this.CBox_CommandList.AllowDrop = true;
            this.CBox_CommandList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CBox_CommandList.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBox_CommandList.FormattingEnabled = true;
            this.CBox_CommandList.Location = new System.Drawing.Point(3, 15);
            this.CBox_CommandList.Name = "CBox_CommandList";
            this.CBox_CommandList.Size = new System.Drawing.Size(354, 20);
            this.CBox_CommandList.TabIndex = 0;
            this.CBox_CommandList.TextChanged += new System.EventHandler(this.CBox_CommandList_OnTextChanged);
            this.CBox_CommandList.DragDrop += new System.Windows.Forms.DragEventHandler(this.CBox_CommandList_DragDrop);
            this.CBox_CommandList.DragEnter += new System.Windows.Forms.DragEventHandler(this.CBox_CommandList_DragEnter);
            this.CBox_CommandList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CBox_CommandList_KeyDown);
            // 
            // GBox_Target
            // 
            this.GBox_Target.Controls.Add(this.CBox_TargetList);
            this.GBox_Target.Dock = System.Windows.Forms.DockStyle.Left;
            this.GBox_Target.Location = new System.Drawing.Point(0, 0);
            this.GBox_Target.Name = "GBox_Target";
            this.GBox_Target.Size = new System.Drawing.Size(157, 44);
            this.GBox_Target.TabIndex = 1;
            this.GBox_Target.TabStop = false;
            this.GBox_Target.Text = "ターゲット";
            // 
            // CBox_TargetList
            // 
            this.CBox_TargetList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_TargetList.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBox_TargetList.FormattingEnabled = true;
            this.CBox_TargetList.Location = new System.Drawing.Point(3, 15);
            this.CBox_TargetList.Name = "CBox_TargetList";
            this.CBox_TargetList.Size = new System.Drawing.Size(151, 20);
            this.CBox_TargetList.TabIndex = 0;
            this.CBox_TargetList.TextChanged += new System.EventHandler(this.CBox_TargetList_TextChanged);
            this.CBox_TargetList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CBox_TargetList_KeyDown);
            // 
            // CMenu_FileFunction
            // 
            this.CMenu_FileFunction.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_FileFunc_Transfer});
            this.CMenu_FileFunction.Name = "CMenu_FileFunction";
            this.CMenu_FileFunction.Size = new System.Drawing.Size(149, 26);
            // 
            // Menu_FileFunc_Transfer
            // 
            this.Menu_FileFunc_Transfer.Name = "Menu_FileFunc_Transfer";
            this.Menu_FileFunc_Transfer.Size = new System.Drawing.Size(148, 22);
            this.Menu_FileFunc_Transfer.Text = "ファイル転送";
            this.Menu_FileFunc_Transfer.Click += new System.EventHandler(this.Menu_FileFunc_Transfer_Click);
            // 
            // MainFrameSingleCommandPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GBox_CommandList);
            this.Controls.Add(this.GBox_Target);
            this.MaximumSize = new System.Drawing.Size(4096, 44);
            this.MinimumSize = new System.Drawing.Size(0, 44);
            this.Name = "MainFrameSingleCommandPanel";
            this.Size = new System.Drawing.Size(578, 44);
            this.GBox_CommandList.ResumeLayout(false);
            this.GBox_Target.ResumeLayout(false);
            this.CMenu_FileFunction.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GBox_CommandList;
        private System.Windows.Forms.ComboBox CBox_CommandList;
        private System.Windows.Forms.GroupBox GBox_Target;
        private System.Windows.Forms.ComboBox CBox_TargetList;
        private System.Windows.Forms.Button Btn_CmdCancel;
        private System.Windows.Forms.Button Btn_FileFunction;
        private System.Windows.Forms.ContextMenuStrip CMenu_FileFunction;
        private System.Windows.Forms.ToolStripMenuItem Menu_FileFunc_Transfer;
    }
}
