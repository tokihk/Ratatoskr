using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.FileFormats;
using RtsCore.Framework.FileFormat;

namespace Ratatoskr.Forms.Dialog
{
    internal partial class PacketLogMultiOpenDialog : Form
    {
        public class FileInfo
        {
            public string          FilePath { get; set; } = "";
            public FileFormatClass Format   { get; set; } = null;
        }

        public PacketLogMultiOpenDialog()
        {
            InitializeComponent();
        }

        public IEnumerable<FileInfo> Files { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            CBox_TargetPattern_FileFormat.Items.AddRange(FileManager.PacketLogOpen.Formats.ToArray());
        }
    }
}
