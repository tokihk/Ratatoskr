using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Configs.UserConfigs;
using Ratatoskr.Devices;
using Ratatoskr.Gate;

namespace Ratatoskr.Forms.MainFrame
{
    internal partial class MainFrameGatePanel : UserControl
    {
        private List<MainFrameGate> gatec_list_ = new List<MainFrameGate>();


        public MainFrameGatePanel()
        {
            InitializeComponent();
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

        private delegate void ClearGateDelegate();
        public void ClearGate()
        {
            /* Invoke */
            if (InvokeRequired) {
                Invoke(new ClearGateDelegate(ClearGate));
                return;
            }

            /* サブコントロールを全て削除 */
            Panel_GateList.Controls.Clear();
            gatec_list_.Clear();
        }

        public void AddGate(GateProperty gatep, DeviceConfig devconf, Guid devc_id, DeviceProperty devp)
        {
            var gate = GateManager.CreateGateObject(gatep, devconf, devc_id, devp);

            if (gate == null)return;

            var gatec = new MainFrameGate(gate);

            /* リストへ追加 */
            gatec_list_.Add(gatec);

            /* フォームに追加 */
            Panel_GateList.Controls.Add(gatec);
        }
    }
}
