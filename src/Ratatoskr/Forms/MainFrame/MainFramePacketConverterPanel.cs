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
using Ratatoskr.PacketConverters;

namespace Ratatoskr.Forms.MainFrame
{
    internal partial class MainFramePacketConverterPanel : FlowLayoutPanel
    {

        public MainFramePacketConverterPanel() : base()
        {
            InitializeComponent();
        }

        public void LoadConfig()
        {
            LoadPacketConverterConfig();
        }

        private void LoadPacketConverterConfig()
        {
            /* 全ビュー解放 */
            ClearPacketConverter();

            /* コンフィグからパケットビューを復元 */
            ConfigManager.User.PacketConverter.Value.ForEach(
                config => AddPacketConverter(
                    config.ClassID, config.Property));
        }

        public void BackupConfig()
        {
            BackupPacketConverterConfig();
        }

        private void BackupPacketConverterConfig()
        {
            /* 全設定を削除 */
            ConfigManager.User.PacketConverter.Value.Clear();

            /* パケットコンバーターのみをスキャン */
            foreach (var pcvti in FormTaskManager.GetPacketConverterInstances()) {
                pcvti.BackupProperty();
                ConfigManager.User.PacketConverter.Value.Add(
                    new PacketConverterObjectConfig(pcvti.Class.ID, pcvti.Property));
            }
        }

        public void ClearPacketConverter()
        {
            while (Controls.Count > 0) {
                RemovePacketConverter(Controls[0] as MainFramePacketConverter);
            }
        }

        public void AddPacketConverter(Guid class_id, PacketConverterProperty pcvtp)
        {
            var pcvti = FormTaskManager.CreatePacketConverter(class_id, pcvtp);

            if (pcvti == null)return;

            var control = new MainFramePacketConverter(pcvti);

            Controls.Add(control);
        }

        public void RemovePacketConverter(MainFramePacketConverter control)
        {
            if (control == null)return;

            /* 変換器を削除 */
            FormTaskManager.RemovePacketConverter(control.Instance);

            /* コントロールを削除 */
            Controls.Remove(control);
        }

        private void AdjustControlSize()
        {
            var size_base = ClientSize;
            var rect_child = new Rectangle(0, 0, size_base.Width, 0);

            foreach (Control control in Controls) {
                /* 高さのみコントロールのサイズを適用する */
                rect_child.Height = control.Height;

                control.Location = rect_child.Location;
                control.Size = rect_child.Size;

                rect_child.Offset(0, rect_child.Height);
            }

            ClientSize = new Size(rect_child.Width, rect_child.Top);
        }

        public void MoveConverterIndex(MainFramePacketConverter control, Point pos_screen)
        {
            var index_old = Controls.GetChildIndex(control);
            var index_new = GetConverterIndex(PointToClient(pos_screen));

            if (index_new != index_old) {
                /* コントロールを入れ替え */
                Controls.SetChildIndex(control, (int)index_new);

                /* 変換器のインスタンスの順番も変更 */
                FormTaskManager.SetPacketConverterIndex(control.Instance, (int)index_new);

                ResumeLayout();
            }
        }

        private uint GetConverterIndex(Point pos_client)
        {
            if (pos_client.Y <= 0) {
                return (0);
            } else if (pos_client.Y > Height) {
                return ((uint)Math.Max(0, Controls.Count - 1));
            } else {
                var bottom = 0;
                var control_index = (uint)0;

                foreach (Control control in Controls) {
                    /* 高さのみコントロールのサイズを適用する */
                    bottom += control.Height;

                    if (pos_client.Y < bottom) {
                        break;
                    }

                    control_index++;
                }

                return (control_index);
            }
        }

        public void UpdateView()
        {
            foreach (MainFramePacketConverter control in Controls) {
                control.UpdatePacketCount();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            AdjustControlSize();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainFramePacketConverterPanel
            // 
            this.Name = "MainFramePacketConverterPanel";
            this.ResumeLayout(false);

        }
    }
}
