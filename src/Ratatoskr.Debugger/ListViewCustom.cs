using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.Debugger
{
	internal class ListViewCustom : ListView
	{
		public ListViewCustom()
		{
			DoubleBuffered = true;
		}
	}
}
