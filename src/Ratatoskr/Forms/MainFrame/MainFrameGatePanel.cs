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
            foreach (var config in ConfigManager.User.Gate.Value) {
                AddGate(
                    config.Color,
                    config.Alias,
                    config.ConnectRequest,
                    GateManager.CreateDeviceObject(
                        config.DeviceClassID,
                        config.Alias,
                        config.DeviceProperty));
            }
        }

        public void BackupConfig()
        {
            BackupGateListConfig();
        }

        private void BackupGateListConfig()
        {
            /* 設定を削除 */
            ConfigManager.User.Gate.Value.Clear();

            /* ゲートのみをスキャン */
            foreach (var gatec in gatec_list_) {
                if (gatec.Gate == null)continue;

                ConfigManager.User.Gate.Value.Add(
                    new GateObjectConfig(
                        gatec.Gate.Alias,
                        gatec.ImageColor,
                        gatec.Gate.ConnectRequest,
                        (gatec.Gate.Device != null) ? (gatec.Gate.Device.Class.ID) : (Guid.Empty),
                        (gatec.Gate.Device != null) ? (gatec.Gate.Device.Property) : (null)));
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

        public void AddGate(Color color, string alias, bool connect, DeviceInstance devi)
        {
            var gate = GateManager.CreateGateObject(alias, connect, devi);

            if (gate == null)return;

            var gatec = new MainFrameGate();

            gatec.ImageColor = color;
            gatec.Gate = gate;

            /* リストへ追加 */
            gatec_list_.Add(gatec);

            /* フォームに追加 */
            Panel_GateList.Controls.Add(gatec);
        }
    }
}
