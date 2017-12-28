using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.Devices;
using Ratatoskr.Gate;
using Ratatoskr.Generic;

namespace Ratatoskr.Forms.GateEditForm
{
    internal partial class GateEditForm : Form
    {
        private GateProperty   gatep_ = null;
        private DeviceConfig   devconf_ = null;
        private Guid           devc_id_ = Guid.Empty;
        private DeviceProperty devp_ = null;

        private DeviceClass          devc_  = null;
        private DevicePropertyEditor devpe_ = null;


        public GateEditForm(GateProperty gatep, DeviceConfig devconf, Guid devc_id, DeviceProperty devp)
        {
            InitializeComponent();
            InitializeDeviceType();

            gatep_ = gatep;
            devconf_ = devconf;
            devc_id_ = devc_id;
            devp_ = (devp != null) ? (devp.Clone()) : (null);

            /* 初期値設定 */
            TBox_Alias.Text = gatep.Alias;
            CBox_DeviceType.SelectedItem = devc_id;
            Num_SendQueueLimit.Value = devconf.SendDataQueueLimit;
            Num_RedirectQueueLimit.Value = devconf.RedirectDataQueueLimit;
            TBox_ConnectCommand.Text = gatep.ConnectCommand;
            TBox_RedirectTargetAlias.Text = gatep.RedirectAlias;
            ChkBox_DaraRateTarget_Send.Checked = gatep.DataRateTarget.HasFlag(DeviceDataRateTarget.SendData);
            ChkBox_DaraRateTarget_Recv.Checked = gatep.DataRateTarget.HasFlag(DeviceDataRateTarget.RecvData);
            Num_DataRate_GraphLimit.Value = gatep.DataRateGraphLimit;
        }

        private void InitializeDeviceType()
        {
            CBox_DeviceType.BeginUpdate();
            {
                /* 全アイテムを消去 */
                CBox_DeviceType.Items.Clear();

                /* デバイスを追加 */
                CBox_DeviceType.Items.AddRange(GateManager.GetDeviceList());
            }
            CBox_DeviceType.EndUpdate();
        }

        public GateProperty   GateProperty   { get; private set; } = null;
        public DeviceConfig   DeviceConfig   { get; private set; } = null;
        public Guid           DeviceClassID  { get; private set; } = Guid.Empty;
        public DeviceProperty DeviceProperty { get; private set; } = null;

        private void UpdateDevice()
        {
            /* デバイスクラス読み込み */
            devc_ = GateManager.FindDeviceClass(devc_id_);

            /* 管理者権限専用の場合は注意文を表示 */
            if ((devc_ != null) && (devc_.AdminOnly) && (!Program.IsAdministratorMode)) {
                Label_DeviceNotice.Text = "Only administrator authority can be used.";
            } else {
                Label_DeviceNotice.Text = "";
            }

            /* デバイスプロパティ更新 */
            UpdateDeviceProperty();

            /* デバイスクラスに応じてチェックボックスの状態を設定 */
            UpdateOperationPermission();
        }

        private void UpdateDeviceStatus()
        {

        }

        private void UpdateDeviceProperty()
        {
            /* プロパティエディタをクリア */
            GBox_DeviceProperty.Controls.Clear();

            /* デバイスクラスが存在しない場合はプロパティリセット */
            if (devc_ == null) {
                devp_ = null;
                return;
            }

            /* 設定済みのデバイスプロパティの型が間違っている場合は初期化 */
            if (   (devp_ == null)
                || (devp_.GetType() != devc_.GetPropertyType())
            ) {
                devp_ = devc_.CreateProperty();
            }

            if (devp_ == null)return;

            /* プロパティエディタを設定 */
            devpe_ = devp_.GetPropertyEditor();

            if (devpe_ == null)return;

            devpe_.Dock = DockStyle.Fill;
            GBox_DeviceProperty.Controls.Add(devpe_);
        }

        private void UpdateOperationPermission()
        {
            if (   (gatep_ == null)
                || (devconf_ == null)
            ) {
                return;
            }

            var can_send = false;
            var can_recv = false;
            var can_redirect = false;

            if (devc_ != null) {
                can_send = devc_.CanSend;
                can_recv = devc_.CanRecv;
                can_redirect = devc_.CanRedirect;
            }

            UpdateOperationPermission(ChkBox_SendEnable, can_send, devconf_.SendEnable);
            UpdateOperationPermission(ChkBox_RecvEnable, can_recv, devconf_.RecvEnable);
            UpdateOperationPermission(ChkBox_RedirectEnable, can_redirect, devconf_.RedirectEnable);
        }

        private void UpdateOperationPermission(CheckBox chkbox, bool can_flag, bool state_flag)
        {
            if (state_flag) {
                chkbox.CheckState = (can_flag) ? (CheckState.Checked) : (CheckState.Indeterminate);
            } else {
                chkbox.CheckState = CheckState.Unchecked;
            }
        }

        private void UpdateConnectCommand()
        {
            var exp = TBox_ConnectCommand.Text;

            if (exp.Length > 0) {
                TBox_ConnectCommand.BackColor = (HexTextEncoder.ToByteArray(exp) != null)
                                              ? (Color.LightSkyBlue)
                                              : (Color.LightPink);
            } else {
                TBox_ConnectCommand.BackColor = Color.White;
            }
        }

        private void CBox_DeviceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var devc = CBox_DeviceType.SelectedItem as DeviceClass;

            if (devc == null)return;

            devc_id_ = devc.ID;

            UpdateDevice();
        }

        private void OnClick_Ok(object sender, EventArgs e)
        {
            if (   (devc_ == null)
                || (devp_ == null)
                || (devpe_ == null)
            ) {
                this.DialogResult = DialogResult.Cancel;
            }

            /* 基本設定の設定値をオブジェクトに反映 */
            gatep_.Alias = TBox_Alias.Text;
            gatep_.ConnectCommand = TBox_ConnectCommand.Text;
            gatep_.RedirectAlias = TBox_RedirectTargetAlias.Text;
            gatep_.DataRateTarget = ((ChkBox_DaraRateTarget_Send.Checked) ? (DeviceDataRateTarget.SendData) : (0))
                                  | ((ChkBox_DaraRateTarget_Recv.Checked) ? (DeviceDataRateTarget.RecvData) : (0));
            gatep_.DataRateGraphLimit = (ulong)Num_DataRate_GraphLimit.Value;
            devconf_.RecvEnable = (ChkBox_RecvEnable.CheckState != CheckState.Unchecked);
            devconf_.SendEnable = (ChkBox_SendEnable.CheckState != CheckState.Unchecked);
            devconf_.RedirectEnable = (ChkBox_RedirectEnable.CheckState != CheckState.Unchecked);
            devconf_.SendDataQueueLimit = (uint)Num_SendQueueLimit.Value;
            devconf_.RedirectDataQueueLimit = (uint)Num_RedirectQueueLimit.Value;

            /* プロパティエディタの設定値をオブジェクトに反映 */
            devpe_.Flush();

            GateProperty = gatep_;
            DeviceConfig = devconf_;
            DeviceClassID = devc_id_;
            DeviceProperty = devp_;

            this.DialogResult = DialogResult.OK;
        }

        private void OnClick_Cancel(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void ChkBox_SendEnable_Click(object sender, EventArgs e)
        {
            devconf_.SendEnable = !devconf_.SendEnable;
            UpdateOperationPermission();
        }

        private void ChkBox_RecvEnable_Click(object sender, EventArgs e)
        {
            devconf_.RecvEnable = !devconf_.RecvEnable;
            UpdateOperationPermission();
        }

        private void ChkBox_RedirectEnable_Click(object sender, EventArgs e)
        {
            devconf_.RedirectEnable = !devconf_.RedirectEnable;
            UpdateOperationPermission();
        }

        private void TBox_ConnectCommand_TextChanged(object sender, EventArgs e)
        {
            UpdateConnectCommand();
        }
    }
}
