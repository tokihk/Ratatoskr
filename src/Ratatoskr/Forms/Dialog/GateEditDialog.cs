using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Debugger;
using Ratatoskr.Device;
using Ratatoskr.General;
using Ratatoskr.Gate;

namespace Ratatoskr.Forms.Dialog
{
    internal partial class GateEditDialog : Form
    {
        private GateProperty   gatep_ = null;
        private DeviceConfig   devconf_ = null;
        private Guid           devc_id_ = Guid.Empty;
        private DeviceProperty devp_ = null;

        private DeviceClass          devc_  = null;
        private DevicePropertyEditor devpe_ = null;


        public GateEditDialog(GateProperty gatep, DeviceConfig devconf, Guid devc_id, DeviceProperty devp)
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

			Btn_SelectColor.BackColor = gatep_.Color;

			ChkBox_Notify_DataSendCompleted.Checked = devconf.DataSendCompletedNotify;
			ChkBox_Notify_DataRecvCompleted.Checked = devconf.DataRecvCompletedNotify;
			ChkBox_Notify_DeviceConnect.Checked = devconf.DeviceConnectNotify;

			ChkBox_DataSendEnable.Checked = devconf.DataSendEnable;
            Num_DataSendQueueLimit.Value = devconf.DataSendQueueLimit;

			ChkBox_DataRedirectEnable.Checked = devconf.DataRedirectEnable;
            Num_DataRedirectQueueLimit.Value = devconf.DataRedirectQueueLimit;
            TBox_SendDataRedirectTargetAlias.Text = gatep.SendRedirectAlias;
            TBox_RecvDataRedirectTargetAlias.Text = gatep.RecvRedirectAlias;

            TBox_ConnectCommand.Text = gatep.ConnectCommand;
        }

        private void InitializeDeviceType()
        {
            CBox_DeviceType.BeginUpdate();
            {
                /* 全アイテムを消去 */
                CBox_DeviceType.Items.Clear();

                /* デバイスを追加 */
                CBox_DeviceType.Items.AddRange(DeviceManager.Instance.GetDeviceList().ToArray());
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
            devc_ = DeviceManager.Instance.FindClass(devc_id_);

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
            var can_redirect = false;

            if (devc_ != null) {
                can_send = devc_.CanSend;
                can_redirect = devc_.CanRedirect;
            }

            UpdateOperationPermission(ChkBox_DataSendEnable, can_send, devconf_.DataSendEnable);
            UpdateOperationPermission(ChkBox_DataRedirectEnable, can_redirect, devconf_.DataRedirectEnable);
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
                                              ? (Ratatoskr.Resource.AppColors.Ok)
                                              : (Ratatoskr.Resource.AppColors.Ng);
            } else {
                TBox_ConnectCommand.BackColor = Color.White;
            }
        }

        private void CBox_DeviceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBox_DeviceType.SelectedItem is DeviceClass devc) {
                devc_id_ = devc.ID;

                DebugManager.MessageOut(DebugEventSender.User, DebugEventType.ControlEvent, "Gate Device Change Start");

                UpdateDevice();

                DebugManager.MessageOut(DebugEventSender.User, DebugEventType.ControlEvent, "Gate Device Change End");
            }
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
			gatep_.Color = Btn_SelectColor.BackColor;
            gatep_.ConnectCommand = TBox_ConnectCommand.Text;

            gatep_.SendRedirectAlias = TBox_SendDataRedirectTargetAlias.Text;
            gatep_.RecvRedirectAlias = TBox_RecvDataRedirectTargetAlias.Text;

			devconf_.DataSendCompletedNotify = ChkBox_Notify_DataSendCompleted.Checked;
			devconf_.DataRecvCompletedNotify = ChkBox_Notify_DataRecvCompleted.Checked;
			devconf_.DeviceConnectNotify     = ChkBox_Notify_DeviceConnect.Checked;

            devconf_.DataSendEnable = (ChkBox_DataSendEnable.CheckState != CheckState.Unchecked);
            devconf_.DataSendQueueLimit = (uint)Num_DataSendQueueLimit.Value;

            devconf_.DataRedirectEnable = (ChkBox_DataRedirectEnable.CheckState != CheckState.Unchecked);
            devconf_.DataRedirectQueueLimit = (uint)Num_DataRedirectQueueLimit.Value;

            /* プロパティエディタの設定値をオブジェクトに反映 */
            if (devpe_ != null) {
                devpe_.Flush();
            }

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

		private void ChkBox_DataSendEnable_Click(object sender, EventArgs e)
		{
            devconf_.DataSendEnable = !devconf_.DataSendEnable;
            UpdateOperationPermission();
		}

		private void ChkBox_DataRedirectEnable_Click(object sender, EventArgs e)
		{
            devconf_.DataRedirectEnable = !devconf_.DataRedirectEnable;
            UpdateOperationPermission();
		}

        private void TBox_ConnectCommand_TextChanged(object sender, EventArgs e)
        {
            UpdateConnectCommand();
        }

		private void Btn_SelectColor_Click(object sender, EventArgs e)
		{
			var dialog = new ColorDialog();

			dialog.Color = gatep_.Color;
			dialog.FullOpen = true;

			if (dialog.ShowDialog() != DialogResult.OK)return;

			gatep_.Color = dialog.Color;

			Btn_SelectColor.BackColor = gatep_.Color;
		}
	}
}
