using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Devices;
using Ratatoskr.Gate;

namespace Ratatoskr.Forms.GateEditForm
{
    internal partial class GateEditForm : Form
    {
        public string         Alias    { get; set; } = "";
        public DeviceClass    Class    { get; set; } = null;
        public DeviceProperty Property { get; set; } = null;

        private DevicePropertyEditor devpe_ = null;


        public GateEditForm()
        {
            InitializeComponent();
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

        private void UpdateDeviceProperty()
        {
            /* プロパティエディタをクリア */
            GBox_DeviceProperty.Controls.Clear();

            if (Class == null) {
                Property = null;
                return;
            }

            if (   (Property == null)
                || (Property.GetType() != Class.GetPropertyType())
            ) {
                Property = Class.CreateProperty();
            }

            if (Property == null)return;

            /* プロパティエディタを設定 */
            devpe_ = Property.GetPropertyEditor();

            if (devpe_ == null)return;

            devpe_.Dock = DockStyle.Fill;
            GBox_DeviceProperty.Controls.Add(devpe_);
        }

        private void SetDeviceType(DeviceClass devd)
        {
            if (devd != null) {
                CBox_DeviceType.SelectedItem = devd;
            } else {
                CBox_DeviceType.SelectedIndex = 0;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            TBox_Alias.Text = Alias;

            InitializeDeviceType();

            SetDeviceType(Class);
        }

        private void OnDeviceChanged(object sender, EventArgs e)
        {
            var cbox = sender as ComboBox;

            if (cbox == null)return;

            Class = cbox.SelectedItem as DeviceClass;

            UpdateDeviceProperty();
        }

        private void OnClick_Ok(object sender, EventArgs e)
        {
            if (   (Class == null)
                || (Property == null)
                || (devpe_ == null)
            ) {
                this.DialogResult = DialogResult.Cancel;
            }

            Alias = TBox_Alias.Text;

            /* エディタの設定値をオブジェクトに反映 */
            devpe_.Flush();

            this.DialogResult = DialogResult.OK;
        }

        private void OnClick_Cancel(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
