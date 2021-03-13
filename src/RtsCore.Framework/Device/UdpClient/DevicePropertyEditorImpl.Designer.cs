namespace Ratatoskr.Devices.UdpClient
{
    partial class DevicePropertyEditorImpl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevicePropertyEditorImpl));
            this.GBox_Local = new System.Windows.Forms.GroupBox();
            this.CBox_LocalBindMode = new System.Windows.Forms.ComboBox();
            this.GBox_LocalPortNo = new System.Windows.Forms.GroupBox();
            this.Num_LocalPortNo = new System.Windows.Forms.NumericUpDown();
            this.GBox_LocalIPAddress = new System.Windows.Forms.GroupBox();
            this.DnsAddrList_Local = new Ratatoskr.Forms.Controls.DnsAddressSelectBox();
            this.GBox_Remote = new System.Windows.Forms.GroupBox();
            this.IPAddrList_Remote = new Ratatoskr.Forms.Controls.HostAddressSelectBox();
            this.CBox_RemoteAddressType = new System.Windows.Forms.ComboBox();
            this.GBox_RemotePortNo = new System.Windows.Forms.GroupBox();
            this.Num_RemotePortNo = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.GBox_Multicast = new System.Windows.Forms.GroupBox();
            this.ChkBox_MulticastGroupAddress = new System.Windows.Forms.CheckBox();
            this.ChkBox_MulticastInterface = new System.Windows.Forms.CheckBox();
            this.GBox_MulticastGroupAddress = new System.Windows.Forms.GroupBox();
            this.TBox_MulticastGroupAddress = new System.Windows.Forms.TextBox();
            this.Btn_MulticastGroupAddress_Remove = new System.Windows.Forms.Button();
            this.Btn_MulticastGroupAddress_Add = new System.Windows.Forms.Button();
            this.LBox_MulticastGroupAddress = new System.Windows.Forms.ListBox();
            this.ChkBox_Multicast_TTL = new System.Windows.Forms.CheckBox();
            this.GBox_MulticastInterface = new System.Windows.Forms.GroupBox();
            this.RBtnList_MulticastInterface = new Ratatoskr.Forms.Controls.RadioButtonListBox();
            this.Num_Multicast_TTL = new System.Windows.Forms.NumericUpDown();
            this.ChkBox_Multicast_Loopback = new System.Windows.Forms.CheckBox();
            this.GBox_Unicast = new System.Windows.Forms.GroupBox();
            this.ChkBox_Unicast_TTL = new System.Windows.Forms.CheckBox();
            this.Num_Unicast_TTL = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.CBox_AddressFamily = new System.Windows.Forms.ComboBox();
            this.GBox_Local.SuspendLayout();
            this.GBox_LocalPortNo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_LocalPortNo)).BeginInit();
            this.GBox_LocalIPAddress.SuspendLayout();
            this.GBox_Remote.SuspendLayout();
            this.GBox_RemotePortNo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_RemotePortNo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.GBox_Multicast.SuspendLayout();
            this.GBox_MulticastGroupAddress.SuspendLayout();
            this.GBox_MulticastInterface.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Multicast_TTL)).BeginInit();
            this.GBox_Unicast.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Unicast_TTL)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBox_Local
            // 
            this.GBox_Local.Controls.Add(this.CBox_LocalBindMode);
            this.GBox_Local.Controls.Add(this.GBox_LocalPortNo);
            this.GBox_Local.Controls.Add(this.GBox_LocalIPAddress);
            this.GBox_Local.Location = new System.Drawing.Point(4, 51);
            this.GBox_Local.Name = "GBox_Local";
            this.GBox_Local.Size = new System.Drawing.Size(404, 206);
            this.GBox_Local.TabIndex = 5;
            this.GBox_Local.TabStop = false;
            this.GBox_Local.Text = "Local";
            // 
            // CBox_LocalBindMode
            // 
            this.CBox_LocalBindMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_LocalBindMode.FormattingEnabled = true;
            this.CBox_LocalBindMode.Location = new System.Drawing.Point(6, 18);
            this.CBox_LocalBindMode.Name = "CBox_LocalBindMode";
            this.CBox_LocalBindMode.Size = new System.Drawing.Size(150, 20);
            this.CBox_LocalBindMode.TabIndex = 3;
            this.CBox_LocalBindMode.SelectedIndexChanged += new System.EventHandler(this.CBox_BindMode_SelectedIndexChanged);
            // 
            // GBox_LocalPortNo
            // 
            this.GBox_LocalPortNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_LocalPortNo.Controls.Add(this.Num_LocalPortNo);
            this.GBox_LocalPortNo.Location = new System.Drawing.Point(297, 44);
            this.GBox_LocalPortNo.Name = "GBox_LocalPortNo";
            this.GBox_LocalPortNo.Size = new System.Drawing.Size(100, 47);
            this.GBox_LocalPortNo.TabIndex = 6;
            this.GBox_LocalPortNo.TabStop = false;
            this.GBox_LocalPortNo.Text = "Port number";
            // 
            // Num_LocalPortNo
            // 
            this.Num_LocalPortNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Num_LocalPortNo.Location = new System.Drawing.Point(3, 15);
            this.Num_LocalPortNo.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.Num_LocalPortNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_LocalPortNo.Name = "Num_LocalPortNo";
            this.Num_LocalPortNo.Size = new System.Drawing.Size(94, 19);
            this.Num_LocalPortNo.TabIndex = 0;
            this.Num_LocalPortNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_LocalPortNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // GBox_LocalIPAddress
            // 
            this.GBox_LocalIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_LocalIPAddress.Controls.Add(this.DnsAddrList_Local);
            this.GBox_LocalIPAddress.Location = new System.Drawing.Point(7, 44);
            this.GBox_LocalIPAddress.Name = "GBox_LocalIPAddress";
            this.GBox_LocalIPAddress.Size = new System.Drawing.Size(284, 156);
            this.GBox_LocalIPAddress.TabIndex = 0;
            this.GBox_LocalIPAddress.TabStop = false;
            this.GBox_LocalIPAddress.Text = "Bind IP Address";
            // 
            // DnsAddrList_Local
            // 
            this.DnsAddrList_Local.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork;
            this.DnsAddrList_Local.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DnsAddrList_Local.HostName = "";
            this.DnsAddrList_Local.HostNames = new string[] {
        ""};
            this.DnsAddrList_Local.Location = new System.Drawing.Point(3, 15);
            this.DnsAddrList_Local.Name = "DnsAddrList_Local";
            this.DnsAddrList_Local.SelectedIPAddress = ((System.Net.IPAddress)(resources.GetObject("DnsAddrList_Local.SelectedIPAddress")));
            this.DnsAddrList_Local.Size = new System.Drawing.Size(278, 138);
            this.DnsAddrList_Local.TabIndex = 0;
            // 
            // GBox_Remote
            // 
            this.GBox_Remote.Controls.Add(this.IPAddrList_Remote);
            this.GBox_Remote.Controls.Add(this.CBox_RemoteAddressType);
            this.GBox_Remote.Controls.Add(this.GBox_RemotePortNo);
            this.GBox_Remote.Location = new System.Drawing.Point(414, 51);
            this.GBox_Remote.Name = "GBox_Remote";
            this.GBox_Remote.Size = new System.Drawing.Size(481, 206);
            this.GBox_Remote.TabIndex = 6;
            this.GBox_Remote.TabStop = false;
            this.GBox_Remote.Text = "Remote";
            // 
            // IPAddrList_Remote
            // 
            this.IPAddrList_Remote.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork;
            this.IPAddrList_Remote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IPAddrList_Remote.HostName = "";
            this.IPAddrList_Remote.Location = new System.Drawing.Point(6, 44);
            this.IPAddrList_Remote.Name = "IPAddrList_Remote";
            this.IPAddrList_Remote.SelectedIPAddress = ((System.Net.IPAddress)(resources.GetObject("IPAddrList_Remote.SelectedIPAddress")));
            this.IPAddrList_Remote.Size = new System.Drawing.Size(363, 160);
            this.IPAddrList_Remote.TabIndex = 5;
            // 
            // CBox_RemoteAddressType
            // 
            this.CBox_RemoteAddressType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_RemoteAddressType.FormattingEnabled = true;
            this.CBox_RemoteAddressType.Location = new System.Drawing.Point(6, 18);
            this.CBox_RemoteAddressType.Name = "CBox_RemoteAddressType";
            this.CBox_RemoteAddressType.Size = new System.Drawing.Size(150, 20);
            this.CBox_RemoteAddressType.TabIndex = 4;
            // 
            // GBox_RemotePortNo
            // 
            this.GBox_RemotePortNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.GBox_RemotePortNo.Controls.Add(this.Num_RemotePortNo);
            this.GBox_RemotePortNo.Location = new System.Drawing.Point(375, 48);
            this.GBox_RemotePortNo.Name = "GBox_RemotePortNo";
            this.GBox_RemotePortNo.Size = new System.Drawing.Size(100, 47);
            this.GBox_RemotePortNo.TabIndex = 6;
            this.GBox_RemotePortNo.TabStop = false;
            this.GBox_RemotePortNo.Text = "Port number";
            // 
            // Num_RemotePortNo
            // 
            this.Num_RemotePortNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Num_RemotePortNo.Location = new System.Drawing.Point(3, 15);
            this.Num_RemotePortNo.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.Num_RemotePortNo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Num_RemotePortNo.Name = "Num_RemotePortNo";
            this.Num_RemotePortNo.Size = new System.Drawing.Size(94, 19);
            this.Num_RemotePortNo.TabIndex = 0;
            this.Num_RemotePortNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Num_RemotePortNo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.GBox_Multicast);
            this.groupBox1.Controls.Add(this.GBox_Unicast);
            this.groupBox1.Location = new System.Drawing.Point(4, 263);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(891, 319);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Option";
            // 
            // GBox_Multicast
            // 
            this.GBox_Multicast.Controls.Add(this.ChkBox_MulticastGroupAddress);
            this.GBox_Multicast.Controls.Add(this.ChkBox_MulticastInterface);
            this.GBox_Multicast.Controls.Add(this.GBox_MulticastGroupAddress);
            this.GBox_Multicast.Controls.Add(this.ChkBox_Multicast_TTL);
            this.GBox_Multicast.Controls.Add(this.GBox_MulticastInterface);
            this.GBox_Multicast.Controls.Add(this.Num_Multicast_TTL);
            this.GBox_Multicast.Controls.Add(this.ChkBox_Multicast_Loopback);
            this.GBox_Multicast.Location = new System.Drawing.Point(167, 18);
            this.GBox_Multicast.Name = "GBox_Multicast";
            this.GBox_Multicast.Size = new System.Drawing.Size(715, 291);
            this.GBox_Multicast.TabIndex = 4;
            this.GBox_Multicast.TabStop = false;
            this.GBox_Multicast.Text = "Multicast";
            // 
            // ChkBox_MulticastGroupAddress
            // 
            this.ChkBox_MulticastGroupAddress.AutoSize = true;
            this.ChkBox_MulticastGroupAddress.Location = new System.Drawing.Point(231, 13);
            this.ChkBox_MulticastGroupAddress.Name = "ChkBox_MulticastGroupAddress";
            this.ChkBox_MulticastGroupAddress.Size = new System.Drawing.Size(213, 16);
            this.ChkBox_MulticastGroupAddress.TabIndex = 11;
            this.ChkBox_MulticastGroupAddress.Text = "Multicast Group Address (IPv4/IPv6)";
            this.ChkBox_MulticastGroupAddress.UseVisualStyleBackColor = true;
            this.ChkBox_MulticastGroupAddress.CheckedChanged += new System.EventHandler(this.ChkBox_MulticastGroupAddress_CheckedChanged);
            // 
            // ChkBox_MulticastInterface
            // 
            this.ChkBox_MulticastInterface.AutoSize = true;
            this.ChkBox_MulticastInterface.Location = new System.Drawing.Point(21, 167);
            this.ChkBox_MulticastInterface.Name = "ChkBox_MulticastInterface";
            this.ChkBox_MulticastInterface.Size = new System.Drawing.Size(167, 16);
            this.ChkBox_MulticastInterface.TabIndex = 5;
            this.ChkBox_MulticastInterface.Text = "Multicast Interface for Send";
            this.ChkBox_MulticastInterface.UseVisualStyleBackColor = true;
            this.ChkBox_MulticastInterface.CheckedChanged += new System.EventHandler(this.ChkBox_MulticastInterface_CheckedChanged);
            // 
            // GBox_MulticastGroupAddress
            // 
            this.GBox_MulticastGroupAddress.Controls.Add(this.TBox_MulticastGroupAddress);
            this.GBox_MulticastGroupAddress.Controls.Add(this.Btn_MulticastGroupAddress_Remove);
            this.GBox_MulticastGroupAddress.Controls.Add(this.Btn_MulticastGroupAddress_Add);
            this.GBox_MulticastGroupAddress.Controls.Add(this.LBox_MulticastGroupAddress);
            this.GBox_MulticastGroupAddress.Location = new System.Drawing.Point(224, 15);
            this.GBox_MulticastGroupAddress.Name = "GBox_MulticastGroupAddress";
            this.GBox_MulticastGroupAddress.Size = new System.Drawing.Size(485, 147);
            this.GBox_MulticastGroupAddress.TabIndex = 10;
            this.GBox_MulticastGroupAddress.TabStop = false;
            // 
            // TBox_MulticastGroupAddress
            // 
            this.TBox_MulticastGroupAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TBox_MulticastGroupAddress.Location = new System.Drawing.Point(6, 20);
            this.TBox_MulticastGroupAddress.Name = "TBox_MulticastGroupAddress";
            this.TBox_MulticastGroupAddress.Size = new System.Drawing.Size(330, 19);
            this.TBox_MulticastGroupAddress.TabIndex = 11;
            // 
            // Btn_MulticastGroupAddress_Remove
            // 
            this.Btn_MulticastGroupAddress_Remove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_MulticastGroupAddress_Remove.Location = new System.Drawing.Point(412, 18);
            this.Btn_MulticastGroupAddress_Remove.Name = "Btn_MulticastGroupAddress_Remove";
            this.Btn_MulticastGroupAddress_Remove.Size = new System.Drawing.Size(64, 23);
            this.Btn_MulticastGroupAddress_Remove.TabIndex = 10;
            this.Btn_MulticastGroupAddress_Remove.Text = "Remove";
            this.Btn_MulticastGroupAddress_Remove.UseVisualStyleBackColor = true;
            this.Btn_MulticastGroupAddress_Remove.Click += new System.EventHandler(this.Btn_MulticastGroupAddress_Remove_Click);
            // 
            // Btn_MulticastGroupAddress_Add
            // 
            this.Btn_MulticastGroupAddress_Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_MulticastGroupAddress_Add.Location = new System.Drawing.Point(342, 18);
            this.Btn_MulticastGroupAddress_Add.Name = "Btn_MulticastGroupAddress_Add";
            this.Btn_MulticastGroupAddress_Add.Size = new System.Drawing.Size(64, 23);
            this.Btn_MulticastGroupAddress_Add.TabIndex = 2;
            this.Btn_MulticastGroupAddress_Add.Text = "Add";
            this.Btn_MulticastGroupAddress_Add.UseVisualStyleBackColor = true;
            this.Btn_MulticastGroupAddress_Add.Click += new System.EventHandler(this.Btn_MulticastGroupAddress_Add_Click);
            // 
            // LBox_MulticastGroupAddress
            // 
            this.LBox_MulticastGroupAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LBox_MulticastGroupAddress.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LBox_MulticastGroupAddress.FormattingEnabled = true;
            this.LBox_MulticastGroupAddress.ItemHeight = 12;
            this.LBox_MulticastGroupAddress.Location = new System.Drawing.Point(6, 47);
            this.LBox_MulticastGroupAddress.Name = "LBox_MulticastGroupAddress";
            this.LBox_MulticastGroupAddress.Size = new System.Drawing.Size(470, 88);
            this.LBox_MulticastGroupAddress.TabIndex = 1;
            // 
            // ChkBox_Multicast_TTL
            // 
            this.ChkBox_Multicast_TTL.AutoSize = true;
            this.ChkBox_Multicast_TTL.Location = new System.Drawing.Point(12, 18);
            this.ChkBox_Multicast_TTL.Name = "ChkBox_Multicast_TTL";
            this.ChkBox_Multicast_TTL.Size = new System.Drawing.Size(95, 16);
            this.ChkBox_Multicast_TTL.TabIndex = 4;
            this.ChkBox_Multicast_TTL.Text = "Multicast TTL";
            this.ChkBox_Multicast_TTL.UseVisualStyleBackColor = true;
            this.ChkBox_Multicast_TTL.CheckedChanged += new System.EventHandler(this.ChkBox_Multicast_TTL_CheckedChanged);
            // 
            // GBox_MulticastInterface
            // 
            this.GBox_MulticastInterface.Controls.Add(this.RBtnList_MulticastInterface);
            this.GBox_MulticastInterface.Location = new System.Drawing.Point(12, 170);
            this.GBox_MulticastInterface.Name = "GBox_MulticastInterface";
            this.GBox_MulticastInterface.Size = new System.Drawing.Size(697, 113);
            this.GBox_MulticastInterface.TabIndex = 9;
            this.GBox_MulticastInterface.TabStop = false;
            // 
            // RBtnList_MulticastInterface
            // 
            this.RBtnList_MulticastInterface.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RBtnList_MulticastInterface.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RBtnList_MulticastInterface.Location = new System.Drawing.Point(6, 15);
            this.RBtnList_MulticastInterface.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.RBtnList_MulticastInterface.Name = "RBtnList_MulticastInterface";
            this.RBtnList_MulticastInterface.SelectedItem = null;
            this.RBtnList_MulticastInterface.SelectedItemIndex = -1;
            this.RBtnList_MulticastInterface.Size = new System.Drawing.Size(682, 91);
            this.RBtnList_MulticastInterface.TabIndex = 2;
            // 
            // Num_Multicast_TTL
            // 
            this.Num_Multicast_TTL.Location = new System.Drawing.Point(117, 15);
            this.Num_Multicast_TTL.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Num_Multicast_TTL.Name = "Num_Multicast_TTL";
            this.Num_Multicast_TTL.Size = new System.Drawing.Size(70, 19);
            this.Num_Multicast_TTL.TabIndex = 5;
            this.Num_Multicast_TTL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ChkBox_Multicast_Loopback
            // 
            this.ChkBox_Multicast_Loopback.AutoSize = true;
            this.ChkBox_Multicast_Loopback.Location = new System.Drawing.Point(12, 42);
            this.ChkBox_Multicast_Loopback.Name = "ChkBox_Multicast_Loopback";
            this.ChkBox_Multicast_Loopback.Size = new System.Drawing.Size(123, 16);
            this.ChkBox_Multicast_Loopback.TabIndex = 3;
            this.ChkBox_Multicast_Loopback.Text = "Multicast Loopback";
            this.ChkBox_Multicast_Loopback.UseVisualStyleBackColor = true;
            // 
            // GBox_Unicast
            // 
            this.GBox_Unicast.Controls.Add(this.ChkBox_Unicast_TTL);
            this.GBox_Unicast.Controls.Add(this.Num_Unicast_TTL);
            this.GBox_Unicast.Location = new System.Drawing.Point(7, 18);
            this.GBox_Unicast.Name = "GBox_Unicast";
            this.GBox_Unicast.Size = new System.Drawing.Size(154, 50);
            this.GBox_Unicast.TabIndex = 3;
            this.GBox_Unicast.TabStop = false;
            this.GBox_Unicast.Text = "Unicast";
            // 
            // ChkBox_Unicast_TTL
            // 
            this.ChkBox_Unicast_TTL.AutoSize = true;
            this.ChkBox_Unicast_TTL.Location = new System.Drawing.Point(10, 19);
            this.ChkBox_Unicast_TTL.Name = "ChkBox_Unicast_TTL";
            this.ChkBox_Unicast_TTL.Size = new System.Drawing.Size(44, 16);
            this.ChkBox_Unicast_TTL.TabIndex = 0;
            this.ChkBox_Unicast_TTL.Text = "TTL";
            this.ChkBox_Unicast_TTL.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.ChkBox_Unicast_TTL.UseVisualStyleBackColor = true;
            // 
            // Num_Unicast_TTL
            // 
            this.Num_Unicast_TTL.Location = new System.Drawing.Point(72, 17);
            this.Num_Unicast_TTL.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.Num_Unicast_TTL.Name = "Num_Unicast_TTL";
            this.Num_Unicast_TTL.Size = new System.Drawing.Size(70, 19);
            this.Num_Unicast_TTL.TabIndex = 0;
            this.Num_Unicast_TTL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CBox_AddressFamily);
            this.groupBox2.Location = new System.Drawing.Point(4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(160, 47);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Address Family";
            // 
            // CBox_AddressFamily
            // 
            this.CBox_AddressFamily.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CBox_AddressFamily.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_AddressFamily.FormattingEnabled = true;
            this.CBox_AddressFamily.Location = new System.Drawing.Point(6, 18);
            this.CBox_AddressFamily.Name = "CBox_AddressFamily";
            this.CBox_AddressFamily.Size = new System.Drawing.Size(150, 20);
            this.CBox_AddressFamily.TabIndex = 0;
            this.CBox_AddressFamily.SelectedIndexChanged += new System.EventHandler(this.CBox_AddressFamily_SelectedIndexChanged);
            // 
            // DevicePropertyEditorImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GBox_Remote);
            this.Controls.Add(this.GBox_Local);
            this.Name = "DevicePropertyEditorImpl";
            this.Size = new System.Drawing.Size(904, 620);
            this.GBox_Local.ResumeLayout(false);
            this.GBox_LocalPortNo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_LocalPortNo)).EndInit();
            this.GBox_LocalIPAddress.ResumeLayout(false);
            this.GBox_Remote.ResumeLayout(false);
            this.GBox_RemotePortNo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_RemotePortNo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.GBox_Multicast.ResumeLayout(false);
            this.GBox_Multicast.PerformLayout();
            this.GBox_MulticastGroupAddress.ResumeLayout(false);
            this.GBox_MulticastGroupAddress.PerformLayout();
            this.GBox_MulticastInterface.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Num_Multicast_TTL)).EndInit();
            this.GBox_Unicast.ResumeLayout(false);
            this.GBox_Unicast.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Num_Unicast_TTL)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox GBox_Local;
        private System.Windows.Forms.GroupBox GBox_LocalPortNo;
        private System.Windows.Forms.GroupBox GBox_LocalIPAddress;
        private System.Windows.Forms.GroupBox GBox_Remote;
        private System.Windows.Forms.GroupBox GBox_RemotePortNo;
        private System.Windows.Forms.NumericUpDown Num_LocalPortNo;
        private System.Windows.Forms.NumericUpDown Num_RemotePortNo;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox GBox_Multicast;
		private System.Windows.Forms.CheckBox ChkBox_Multicast_TTL;
		private System.Windows.Forms.NumericUpDown Num_Multicast_TTL;
		private System.Windows.Forms.CheckBox ChkBox_Multicast_Loopback;
		private System.Windows.Forms.GroupBox GBox_Unicast;
		private System.Windows.Forms.CheckBox ChkBox_Unicast_TTL;
		private System.Windows.Forms.NumericUpDown Num_Unicast_TTL;
		private System.Windows.Forms.GroupBox GBox_MulticastInterface;
		private System.Windows.Forms.GroupBox GBox_MulticastGroupAddress;
		private System.Windows.Forms.Button Btn_MulticastGroupAddress_Remove;
		private System.Windows.Forms.Button Btn_MulticastGroupAddress_Add;
		private System.Windows.Forms.ListBox LBox_MulticastGroupAddress;
		private System.Windows.Forms.TextBox TBox_MulticastGroupAddress;
		private Forms.Controls.RadioButtonListBox RBtnList_MulticastInterface;
		private System.Windows.Forms.CheckBox ChkBox_MulticastInterface;
		private System.Windows.Forms.ComboBox CBox_LocalBindMode;
		private System.Windows.Forms.ComboBox CBox_RemoteAddressType;
		private Forms.Controls.HostAddressSelectBox IPAddrList_Remote;
		private Forms.Controls.DnsAddressSelectBox DnsAddrList_Local;
		private System.Windows.Forms.CheckBox ChkBox_MulticastGroupAddress;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox CBox_AddressFamily;
	}
}
