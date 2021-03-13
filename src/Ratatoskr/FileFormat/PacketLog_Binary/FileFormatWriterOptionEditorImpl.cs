using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormat.PacketLog_Binary
{
    internal sealed class FileFormatWriterOptionEditorImpl : FileFormatOptionEditor
    {
        private sealed class CBoxItem_SaveDataType
        {
            public SaveDataType Value { get; }

            public CBoxItem_SaveDataType(SaveDataType type)
            {
                Value = type;
            }

            public override int GetHashCode()
            {
                return (base.GetHashCode());
            }

            public override bool Equals(object obj)
            {
                if (obj.GetType() == typeof(SaveDataType)) {
                    return ((SaveDataType)obj == Value);
                } else {
                    return (base.Equals(obj));
                }
            }

            public override string ToString()
            {
                return (Value.ToString());
            }
        }

        public FileFormatWriterOptionImpl Option { get; }

        private System.Windows.Forms.GroupBox GBox_SaveDataType;
        private System.Windows.Forms.ComboBox CBox_SaveDataType;


        public FileFormatWriterOptionEditorImpl(FileFormatWriterOptionImpl option)
        {
            Option = option;

            InitializeComponent();
            InitializeSaveDataType();

            SelectSaveDataType(Option.SaveData);
        }

        private void InitializeComponent()
        {
            this.GBox_SaveDataType = new System.Windows.Forms.GroupBox();
            this.CBox_SaveDataType = new System.Windows.Forms.ComboBox();
            this.GBox_SaveDataType.SuspendLayout();
            this.SuspendLayout();
            // 
            // GBox_SaveDataType
            // 
            this.GBox_SaveDataType.Controls.Add(this.CBox_SaveDataType);
            this.GBox_SaveDataType.Location = new System.Drawing.Point(3, 3);
            this.GBox_SaveDataType.Name = "GBox_SaveDataType";
            this.GBox_SaveDataType.Size = new System.Drawing.Size(200, 45);
            this.GBox_SaveDataType.TabIndex = 0;
            this.GBox_SaveDataType.TabStop = false;
            this.GBox_SaveDataType.Text = "保存データ種別";
            // 
            // CBox_SaveDataType
            // 
            this.CBox_SaveDataType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CBox_SaveDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_SaveDataType.FormattingEnabled = true;
            this.CBox_SaveDataType.Location = new System.Drawing.Point(3, 15);
            this.CBox_SaveDataType.Name = "CBox_SaveDataType";
            this.CBox_SaveDataType.Size = new System.Drawing.Size(194, 20);
            this.CBox_SaveDataType.TabIndex = 0;
            // 
            // FileFormatWriterOptionEditorImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.GBox_SaveDataType);
            this.Name = "FileFormatWriterOptionEditorImpl";
            this.Size = new System.Drawing.Size(208, 54);
            this.GBox_SaveDataType.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void InitializeSaveDataType()
        {
            CBox_SaveDataType.BeginUpdate();
            {
                foreach (SaveDataType type in Enum.GetValues(typeof(SaveDataType))) {
                    CBox_SaveDataType.Items.Add(new CBoxItem_SaveDataType(type));
                }
            }
            CBox_SaveDataType.EndUpdate();
        }

        private void SelectSaveDataType(SaveDataType type)
        {
            CBox_SaveDataType.SelectedItem = type;
        }

        public override void Flush()
        {
            Option.SaveData = (CBox_SaveDataType.SelectedItem as CBoxItem_SaveDataType).Value;
        }
    }
}
