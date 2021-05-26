using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config;
using Ratatoskr.Config.Data.User;
using Ratatoskr.Gate;
using Ratatoskr.Device;

namespace Ratatoskr.Forms.MainWindow
{
    internal partial class MainWindow_GatePanel : UserControl
    {
        private List<MainWindow_Gate> gatec_list_ = new List<MainWindow_Gate>();


        public MainWindow_GatePanel()
        {
            InitializeComponent();

			ConfigManager.Updated += OnConfigUpdated;
        }

		public void LoadConfig()
        {
            LoadGateListConfig();
        }

        private void LoadGateListConfig()
        {
            foreach (var config in ConfigManager.User.GateList.Value) {
                AddGate(
                    config.GateProperty,
                    config.DeviceConfig,
                    config.DeviceClassID,
                    config.DeviceProperty);
            }

			AdjustGateList();
        }

        public void BackupConfig()
        {
            BackupGateListConfig();
        }

        private void BackupGateListConfig()
        {
            /* 設定を削除 */
            ConfigManager.User.GateList.Value.Clear();

            /* ゲートのみをスキャン */
            foreach (var gatec in gatec_list_) {
                if (gatec.Gate == null)continue;

                var gate = gatec.Gate;

                ConfigManager.User.GateList.Value.Add(
                    new GateObjectConfig(
                        gate.GateProperty,
                        gate.DeviceConfig,
                        gate.DeviceClassID,
                        gate.DeviceProperty));
            }
        }

		private void OnConfigUpdated(object sender, EventArgs e)
		{
			AdjustGateList();
		}

		public void AdjustGateList()
		{
            /* Invoke */
            if (InvokeRequired) {
                Invoke(new MethodInvoker(AdjustGateList));
                return;
            }

			Color[] gate_color_default =
			{
				Color.LightGoldenrodYellow,
				Color.LightBlue,
				Color.LightPink,
				Color.LightGreen,
				Color.LightSalmon,
				Color.LightCyan,
				Color.LightSteelBlue,
				Color.Pink,
				Color.LightSeaGreen,
				Color.LightYellow,
				Color.LightSkyBlue,
				Color.AntiqueWhite,
			};
			
			/* システムに設定したゲート数に達するまでゲートを追加 */
			for (var gate_no = gatec_list_.Count; gate_no < ConfigManager.System.ApplicationCore.GateNum.Value; gate_no++) {
				var color = gate_color_default.Last();

				if (gate_no < gate_color_default.Length) {
					color = gate_color_default[gate_no];
				}

				AddGate(new GateProperty(String.Format("GATE_{0:000}", gate_no + 1), color), new DeviceConfig(), Guid.Empty, null);
			}

			/* システムに設定したゲート数以上のゲートを削除 */
			while (gatec_list_.Count > ConfigManager.System.ApplicationCore.GateNum.Value) {
				RemoveGateAt(gatec_list_.Count - 1);
			}
		}

        public void ClearGate()
        {
            /* Invoke */
            if (InvokeRequired) {
                Invoke(new MethodInvoker(ClearGate));
                return;
            }

            /* サブコントロールを全て削除 */
            Panel_GateList.Controls.Clear();
            gatec_list_.Clear();
        }

        public void AddGate(GateProperty gatep, DeviceConfig devconf, Guid devc_id, DeviceProperty devp)
        {
			if (gatec_list_.Count >= ConfigManager.System.ApplicationCore.GateNum.Value) {
				return;
			}

            var gate = GateManager.CreateGateObject(gatep, devconf, devc_id, devp);

            if (gate == null)return;

            var gatec = new MainWindow_Gate(gate);

            /* リストへ追加 */
            gatec_list_.Add(gatec);

            /* フォームに追加 */
            Panel_GateList.Controls.Add(gatec);
        }

		public void RemoveGateAt(int gate_no)
		{
			if ((gate_no < 0) || (gate_no >= gatec_list_.Count))return;

			var gatec = gatec_list_[gate_no];

			gatec_list_.Remove(gatec);

			Panel_GateList.Controls.Remove(gatec);

			if (gatec.Gate != null) {
				gatec.Gate.Dispose();
			}
		}
    }
}
