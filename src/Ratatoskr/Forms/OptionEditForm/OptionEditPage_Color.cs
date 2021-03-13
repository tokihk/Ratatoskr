using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config.Data.User;

namespace Ratatoskr.Forms.OptionEditForm
{
    internal partial class OptionEditPage_Color : OptionEditPage
    {
        public OptionEditPage_Color()
        {
            InitializeComponent();
        }

        protected override void OnLoadConfig()
        {
            Btn_Packet_Msg.BackColor = Config.Packet_MsgColor;
            Btn_Packet_Recv.BackColor = Config.Packet_RecvColor;
            Btn_Packet_Send.BackColor = Config.Packet_SendColor;
        }

        protected override void OnFlushConfig()
        {
            Config.Packet_MsgColor = Btn_Packet_Msg.BackColor;
            Config.Packet_RecvColor = Btn_Packet_Recv.BackColor;
            Config.Packet_SendColor = Btn_Packet_Send.BackColor;
        }

        private int GetWin32Color(Color color)
        {
            return ((int)(
                      ((uint)color.B << 16)
                    | ((uint)color.G << 8)
                    | ((uint)color.R << 0)));
        }

        private ColorDialog CreateColorDialog(Color color)
        {
            var dialog = new ColorDialog();
            var config_def = new UserConfig();

            dialog.Color = color;
            dialog.AllowFullOpen = true;
            dialog.FullOpen = true;
            dialog.CustomColors = new int[]
            {
                GetWin32Color(color),
                GetWin32Color(config_def.PacketView_Packet_MsgColor.Value),
                GetWin32Color(config_def.PacketView_Packet_RecvColor.Value),
                GetWin32Color(config_def.PacketView_Packet_SendColor.Value),
            };
            
            return (dialog);
        }

        private void Btn_PacketColor_Click(object sender, EventArgs e)
        {
            if (sender is Button button) {
                var dialog = CreateColorDialog(button.BackColor);

                if (dialog.ShowDialog() != DialogResult.OK)return;

                button.BackColor = dialog.Color;
            }
        }
    }
}
