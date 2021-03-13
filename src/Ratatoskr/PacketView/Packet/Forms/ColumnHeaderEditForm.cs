using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Config;
using Ratatoskr.PacketView.Packet.Configs;
using Ratatoskr.General.Packet.Filter;

namespace Ratatoskr.PacketView.Packet.Forms
{
    internal partial class ColumnHeaderEditForm : Form
    {
        private static string GetDefaultDisplayText(ColumnType type)
        {
            switch (type) {
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
                default:                            return (type.ToString());
            }
        }


        private sealed class ColumnItemObject
        {
            public ColumnHeaderConfig Config { get; }


            public ColumnItemObject(ColumnHeaderConfig config)
            {
                Config = config;
            }

            public ColumnItemObject(ColumnType type)
            {
                Config = new ColumnHeaderConfig(type);
            }

            public override string ToString()
            {
                return (Config.Text);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj is ColumnType type) {
                    return (type == Config.Type);
                }

                return base.Equals(obj);
            }
        }


        private List<ColumnHeaderConfig> user_items_ = new List<ColumnHeaderConfig>();

        private ColumnItemObject user_select_item_ = null;
        private ColumnItemObject user_moving_item_ = null;

        public IEnumerable<ColumnHeaderConfig> UserItems { get; private set; } = null;


        public ColumnHeaderEditForm()
        {
            InitializeComponent();
        }

        public ColumnHeaderEditForm(IEnumerable<ColumnType> all_items, IEnumerable<ColumnHeaderConfig> user_items) : this()
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

            CBox_SelectItem_ItemType.BeginUpdate();
            {
                CBox_SelectItem_ItemType.Items.Clear();

                foreach (var column in items) {
                    CBox_SelectItem_ItemType.Items.Add(new ColumnItemObject(column));
                }
            }
            CBox_SelectItem_ItemType.EndUpdate();
        }

        private void InitializeUserItems(IEnumerable<ColumnHeaderConfig> items)
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

        private void UpdateUserItemView(ColumnItemObject item)
        {
            var index = LBox_UserItem.Items.IndexOf(item);

            if (index >= 0) {
                LBox_UserItem.BeginUpdate();

                LBox_UserItem.Items.Remove(item);
                LBox_UserItem.Items.Insert(index, item);

                LBox_UserItem.EndUpdate();
            }
        }

        private void LoadSelectItemConfig(ColumnItemObject obj)
        {
            CBox_SelectItem_ItemType.SelectedItem = obj.Config.Type;
            TBox_SelectItem_DisplayText.Text = obj.Config.Text;
            TBox_SelectItem_PacketFilter.Text = obj.Config.PacketFilter;
        }

        private void SaveSelectItemConfig(ColumnItemObject obj)
        {
            obj.Config.Type = (CBox_SelectItem_ItemType.SelectedItem as ColumnItemObject).Config.Type;
            obj.Config.Text = TBox_SelectItem_DisplayText.Text;
            obj.Config.PacketFilter = TBox_SelectItem_PacketFilter.Text;
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

        private void LBox_UserItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (user_select_item_ != null) {
                SaveSelectItemConfig(user_select_item_);
                UpdateUserItemView(user_select_item_);
            }

            user_select_item_ = LBox_UserItem.SelectedItem as ColumnItemObject;

            if (user_select_item_ != null) {
                LoadSelectItemConfig(user_select_item_);
            }

            LBox_UserItem.Update();
        }

        private void LBox_UserItem_MouseDown(object sender, MouseEventArgs e)
        {
            user_moving_item_ = LBox_UserItem.SelectedItem as ColumnItemObject;
        }

        private void LBox_UserItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (   (e.Button == MouseButtons.Left)
                && (user_moving_item_ != null)
            ) {
                /* 移動位置を取得 */
                var index_new = GetListBoxIndex(LBox_UserItem, e.Location);
                var index_now = LBox_UserItem.Items.IndexOf(user_moving_item_);

                if (index_now != index_new) {
                    LBox_UserItem.BeginUpdate();
                    {
                        LBox_UserItem.Items.RemoveAt(index_now);
                        LBox_UserItem.Items.Insert(Math.Min(index_new, LBox_UserItem.Items.Count), user_moving_item_);
                        LBox_UserItem.SelectedItem = user_moving_item_;
                    }
                    LBox_UserItem.EndUpdate();
                }
            }
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            if (LBox_AllItem.SelectedItem is ColumnItemObject item_new) {
                LBox_UserItem.Items.Add(new ColumnItemObject(item_new.Config.Type));
            }
        }

        private void Btn_Remove_Click(object sender, EventArgs e)
        {
            if (LBox_UserItem.SelectedItem is ColumnItemObject item) {
                LBox_UserItem.Items.Remove(item);
            }
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            if (user_select_item_ != null) {
                SaveSelectItemConfig(user_select_item_);
            }

            var items = new List<ColumnHeaderConfig>();

            foreach (ColumnItemObject item in LBox_UserItem.Items) {
                items.Add(item.Config);
            }

            UserItems = items;

            DialogResult = DialogResult.OK;
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void TBox_SelectItem_PacketFilter_TextChanged(object sender, EventArgs e)
        {
            var text = TBox_SelectItem_PacketFilter.Text;

            if (text != "") {
                TBox_SelectItem_PacketFilter.BackColor = (PacketFilterObject.Compile(TBox_SelectItem_PacketFilter.Text) != null)
                                                       ? (Ratatoskr.Resource.AppColors.Ok)
                                                       : (Ratatoskr.Resource.AppColors.Ng);
            } else {
                TBox_SelectItem_PacketFilter.BackColor = Color.White;
            }
        }
    }
}
