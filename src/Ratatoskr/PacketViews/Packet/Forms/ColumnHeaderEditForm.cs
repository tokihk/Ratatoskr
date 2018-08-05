using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;

namespace Ratatoskr.PacketViews.Packet.Forms
{
    internal partial class ColumnHeaderEditForm : Form
    {
        private sealed class ColumnItemObject
        {
            public ColumnType Type { get; }


            public ColumnItemObject(ColumnType type)
            {
                Type = type;
            }

            public override string ToString()
            {
                switch (Type) {
                    case ColumnType.Class:              return (ConfigManager.Language.PacketView.Packet.Column_Class.Value);
                    case ColumnType.Alias:              return (ConfigManager.Language.PacketView.Packet.Column_Alias.Value);
                    case ColumnType.Datetime_UTC:       return (ConfigManager.Language.PacketView.Packet.Column_Datetime_UTC.Value);
                    case ColumnType.Datetime_Local:     return (ConfigManager.Language.PacketView.Packet.Column_Datetime_Local.Value);
                    case ColumnType.Information:        return (ConfigManager.Language.PacketView.Packet.Column_Information.Value);
                    case ColumnType.Mark:               return (ConfigManager.Language.PacketView.Packet.Column_Mark.Value);
                    case ColumnType.Source:             return (ConfigManager.Language.PacketView.Packet.Column_Source.Value);
                    case ColumnType.Destination:        return (ConfigManager.Language.PacketView.Packet.Column_Destination.Value);
                    case ColumnType.DataLength:         return (ConfigManager.Language.PacketView.Packet.Column_DataLength.Value);
                    case ColumnType.DataPreviewBinary:  return (ConfigManager.Language.PacketView.Packet.Column_DataPreviewBinary.Value);
                    case ColumnType.DataPreviewText:    return (ConfigManager.Language.PacketView.Packet.Column_DataPreviewText.Value);
                    case ColumnType.DataPreviewCustom:  return (ConfigManager.Language.PacketView.Packet.Column_DataPreviewCustom.Value);
                    default:                            return (Type.ToString());
                }
            }
        }


        private ColumnItemObject user_select_item_ = null;

        public IEnumerable<ColumnType> UserItems { get; private set; } = null;


        public ColumnHeaderEditForm()
        {
            InitializeComponent();
        }

        public ColumnHeaderEditForm(IEnumerable<ColumnType> all_items, IEnumerable<ColumnType> user_items) : this()
        {
            InitializeAllItems(all_items);
            InitializeUserItems(user_items);
        }

        private void InitializeAllItems(IEnumerable<ColumnType> items)
        {
            LBox_AllItem.BeginUpdate();
            {
                LBox_AllItem.Items.Clear();

                foreach (var column in items) {
                    LBox_AllItem.Items.Add(new ColumnItemObject(column));
                }
            }
            LBox_AllItem.EndUpdate();
        }

        private void InitializeUserItems(IEnumerable<ColumnType> items)
        {
            LBox_UserItem.BeginUpdate();
            {
                LBox_UserItem.Items.Clear();

                foreach (var column in items) {
                    LBox_UserItem.Items.Add(new ColumnItemObject(column));
                }
            }
            LBox_UserItem.EndUpdate();
        }

        private int GetListBoxIndex(ListBox control, Point client_pos)
        {
            var range = 0;
            var item_height = control.ItemHeight;
            var item_index = 0;

            foreach (var item in control.Items) {
                range += item_height;

                if (client_pos.Y < range) {
                    break;
                }

                item_index++;
            }

            return (item_index);
        }

        private void LBox_UserItem_MouseDown(object sender, MouseEventArgs e)
        {
            user_select_item_ = LBox_UserItem.SelectedItem as ColumnItemObject;
        }

        private void LBox_UserItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (   (e.Button == MouseButtons.Left)
                && (user_select_item_ != null)
            ) {
                /* 移動位置を取得 */
                var index_new = GetListBoxIndex(LBox_UserItem, e.Location);
                var index_now = LBox_UserItem.Items.IndexOf(user_select_item_);

                if (index_now != index_new) {
                    var item = LBox_UserItem.SelectedItem;

                    LBox_UserItem.BeginUpdate();
                    {
                        LBox_UserItem.Items.RemoveAt(index_now);
                        LBox_UserItem.Items.Insert(Math.Min(index_new, LBox_UserItem.Items.Count), item);
                        LBox_UserItem.SelectedItem = user_select_item_;
                    }
                    LBox_UserItem.EndUpdate();
                }
            }
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            var item_new = LBox_AllItem.SelectedItem as ColumnItemObject;

            if (item_new == null)return;

            LBox_UserItem.Items.Add(new ColumnItemObject(item_new.Type));
        }

        private void Btn_Remove_Click(object sender, EventArgs e)
        {
            var item = LBox_UserItem.SelectedItem as ColumnItemObject;

            if (item == null)return;

            LBox_UserItem.Items.Remove(item);
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            var items = new List<ColumnType>();

            foreach (ColumnItemObject item in LBox_UserItem.Items) {
                items.Add(item.Type);
            }
            UserItems = items;

            DialogResult = DialogResult.OK;
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
