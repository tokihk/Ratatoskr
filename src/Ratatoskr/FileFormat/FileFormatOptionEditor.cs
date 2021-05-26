using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.FileFormat
{
    internal partial class FileFormatOptionEditor : UserControl
    {
        public FileFormatOptionEditor()
        {
            InitializeComponent();
        }

		public void LoadOption(FileFormatOption option)
		{
			if (option == null)return;

			OnLoadOption(option);
		}

		public void BackupOption(FileFormatOption option)
		{
			if (option == null)return;

			OnBackupOption(option);
		}

		protected virtual void OnLoadOption(FileFormatOption option) { }
		protected virtual void OnBackupOption(FileFormatOption option) { }
    }
}
