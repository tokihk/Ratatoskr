using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;

namespace Ratatoskr.Forms
{
	public partial class RadioButtonListBox : UserControl
	{
		private readonly Padding	RADIO_BUTTON_MARGIN = new Padding(5, 0, 5, 0);
		private readonly Font		RADIO_BUTTON_FONT   = new Font("MS Gothic", 9);


		public RadioButtonListBox()
		{
			InitializeComponent();
		}

		public event EventHandler RadioButtonClick;

		public int SelectedItemIndex
		{
			get
			{
				var index = 0;

				foreach (RadioButton control in FLPanel_Main.Controls) {
					if (control.Checked) {
						return (index);
					}
					index++;
				}

				return (-1);
			}

			set
			{
				if ((value >= 0) && (value < FLPanel_Main.Controls.Count)) {
					if (FLPanel_Main.Controls[value] is RadioButton obj) {
						obj.Checked = true;
					}
				}
			}
		}

		public object SelectedItem
		{
			get
			{
				foreach (RadioButton control in FLPanel_Main.Controls) {
					if (control.Checked) {
						return (control.Tag);
					}
				}

				return (null);
			}

			set
			{
				foreach (RadioButton control in FLPanel_Main.Controls) {
					if (control.Tag.Equals(value)) {
						control.Checked = true;
					}
				}
			}
		}

		public void ClearItems()
		{
			FLPanel_Main.Controls.Clear();
		}

		public void AddItem(object item)
		{
			var control = new RadioButton();

			control.Text = item.ToString();
			control.Tag = item;
			control.Margin = RADIO_BUTTON_MARGIN;
			control.Font = RADIO_BUTTON_FONT;
			control.AutoSize = true;
//			control.Click += RBtn_Click;
			control.CheckedChanged += RBtn_Click;

			FLPanel_Main.Controls.Add(control);
		}

		private void RBtn_Click(object sender, EventArgs e)
		{
			if (sender is RadioButton obj) {
				if (obj.Checked) {
					RadioButtonClick?.Invoke(this, e);
				}
			}
		}
	}
}
