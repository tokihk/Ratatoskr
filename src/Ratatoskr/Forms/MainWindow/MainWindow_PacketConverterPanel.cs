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
using Ratatoskr.Debugger;
using Ratatoskr.PacketConverter;

namespace Ratatoskr.Forms.MainWindow
{
    internal partial class MainWindow_PacketConverterPanel : Panel
    {
        private string btn_collapse_text_ = "";

        private FlowLayoutPanel Panel_ConverterList;
        private Button          Btn_Collapse;


        public MainWindow_PacketConverterPanel() : base()
        {
            Btn_Collapse = new Button();
            Btn_Collapse.FlatStyle = FlatStyle.Flat;
            Btn_Collapse.FlatAppearance.BorderSize = 0;
            Btn_Collapse.TextAlign = ContentAlignment.MiddleLeft;
            Btn_Collapse.Font = new Font("Segoe UI", 10);
            Btn_Collapse.ForeColor = SystemColors.GrayText;
            Btn_Collapse.Image = Ratatoskr.Resource.Images.collapse_16x16;
            Btn_Collapse.ImageAlign = ContentAlignment.MiddleLeft;
            Btn_Collapse.TextImageRelation = TextImageRelation.ImageBeforeText;
            Btn_Collapse.Height = Btn_Collapse.Image.Height + Btn_Collapse.Margin.Vertical;
            Btn_Collapse.Paint += Btn_Collapse_Paint;
            Btn_Collapse.Click += Btn_Collapse_Click;

            Panel_ConverterList = new FlowLayoutPanel();
            Panel_ConverterList.FlowDirection = FlowDirection.TopDown;

            Controls.Add(Btn_Collapse);
            Controls.Add(Panel_ConverterList);
        }

        public void LoadConfig()
        {
            LoadPacketConverterConfig();

            SetPacketConverterVisible(ConfigManager.System.MainWindow.PacketConverterVisible.Value);
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

        private void SetPacketConverterVisible(bool show)
        {
            ConfigManager.System.MainWindow.PacketConverterVisible.Value = show;

            Panel_ConverterList.Visible = ConfigManager.System.MainWindow.PacketConverterVisible.Value;

            Btn_Collapse.Image = (ConfigManager.System.MainWindow.PacketConverterVisible.Value)
                               ? (Ratatoskr.Resource.Images.expand_16x16)
                               : (Ratatoskr.Resource.Images.collapse_16x16);

            AdjustControlSize();
        }

        public void ClearPacketConverter()
        {
            while (Panel_ConverterList.Controls.Count > 0) {
                RemovePacketConverter(Panel_ConverterList.Controls[0] as MainWindow_PacketConverter);
            }
        }

        public void AddPacketConverter(Guid class_id, PacketConverterProperty pcvtp)
        {
            var pcvti = FormTaskManager.CreatePacketConverter(class_id, pcvtp);

            if (pcvti == null)return;

            var control = new MainWindow_PacketConverter(this, pcvti);

            Panel_ConverterList.Controls.Add(control);

            AdjustControlSize();

            UpdateView();
        }

        public void RemovePacketConverter(MainWindow_PacketConverter control)
        {
            if (control == null)return;

            /* 変換器を削除 */
            FormTaskManager.RemovePacketConverter(control.Instance);

            /* コントロールを削除 */
            Panel_ConverterList.Controls.Remove(control);

            AdjustControlSize();

            UpdateView();
        }

        private void AdjustControlSize()
        {
            /* 高さのみコントロールのサイズを適用する */
            var rect_total = new Rectangle(0, 0, ClientSize.Width, 0);

            /* 伸縮ボタンの調整 */
            Btn_Collapse.Location = new Point(0, 0);
            Btn_Collapse.Size = new Size(rect_total.Width, Btn_Collapse.Height);
            rect_total.Height += Btn_Collapse.Height;

            /* 変換器毎の調整 */
            if (Panel_ConverterList.Visible) {
                var rect_child = new Rectangle(0, 0, rect_total.Width, 0);

                foreach (Control control in Panel_ConverterList.Controls) {
                    rect_child.Height = control.Height;

                    control.Location = rect_child.Location;
                    control.Size = rect_child.Size;

                    rect_child.Offset(0, rect_child.Height);
                }

                /* 変換器パネルの調整 */
                Panel_ConverterList.Location = new Point(0, rect_total.Bottom);
                Panel_ConverterList.Size = new Size(rect_child.Width, rect_child.Top);
                rect_total.Height += Panel_ConverterList.Height;
            }

            Size = new Size(rect_total.Width, rect_total.Bottom);
        }

        public void MoveConverterIndex(MainWindow_PacketConverter control, Point pos_screen)
        {
            var index_old = Panel_ConverterList.Controls.GetChildIndex(control);
            var index_new = GetConverterIndex(Panel_ConverterList.PointToClient(pos_screen));

            if (index_new != index_old) {
                DebugManager.MessageOut(string.Format("MoveConverterIndex {0} => {1}", index_new, index_old));

                /* コントロールを入れ替え */
                Panel_ConverterList.Controls.SetChildIndex(control, (int)index_new);

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
                return ((uint)Math.Max(0, Panel_ConverterList.Controls.Count - 1));
            } else {
                var bottom = 0;
                var control_index = (uint)0;

                foreach (Control control in Panel_ConverterList.Controls) {
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
            var count_all = 0;
            var count_enable = 0;

            foreach (MainWindow_PacketConverter control in Panel_ConverterList.Controls) {
                count_all++;
                if (control.ConverterEnable) {
                    count_enable++;
                }

                control.UpdatePacketCount();
            }

            btn_collapse_text_ = string.Format("Packet Converter (Enable: {0} , Total: {1})", count_enable, count_all);
            Btn_Collapse.Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            AdjustControlSize();
        }

        private void Btn_Collapse_Paint(object sender, PaintEventArgs e)
        {
            if (sender is Button control) {
                var rect_text = new Rectangle();

                rect_text.X = control.Image.Width + control.Margin.Horizontal;
                rect_text.Y = 0;
                rect_text.Width = control.ClientSize.Width - rect_text.X;
                rect_text.Height = control.ClientSize.Height;

                var text_format = new StringFormat();

                text_format.Alignment = StringAlignment.Near;
                text_format.LineAlignment = StringAlignment.Center;

                /* 標準テキストだと枠に入らないため、自前で描画 */
                e.Graphics.DrawString(
                    btn_collapse_text_,
                    control.Font,
                    new SolidBrush(control.ForeColor),
                    rect_text, 
                    text_format);
            }
        }

        private void Btn_Collapse_Click(object sender, EventArgs e)
        {
            SetPacketConverterVisible(!ConfigManager.System.MainWindow.PacketConverterVisible.Value);
        }
    }
}
