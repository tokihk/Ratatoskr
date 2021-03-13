using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RtsCore.FileFormat
{
    public partial class FileFormatOptionForm : Form
    {
        private FileFormatOptionEditor editor_;


        public FileFormatOptionForm(FileFormatOptionEditor editor)
        {
            InitializeComponent();
            InitializeEditor(editor);
        }

        private void InitializeEditor(FileFormatOptionEditor editor)
        {
            editor_ = editor;
            editor_.Dock = DockStyle.Fill;
            Panel_Editor.Controls.Add(editor_);
        }

        private void Btn_Ok_Click(object sender, EventArgs e)
        {
            editor_.Flush();
            DialogResult = DialogResult.OK;
        }

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
