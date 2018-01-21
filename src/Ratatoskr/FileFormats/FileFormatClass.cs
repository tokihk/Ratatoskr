using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormats
{
    internal abstract class FileFormatClass
    {
        public abstract string   Name { get; }

        public abstract string[] FileExtension { get; }

        public virtual string Detail { get { return (Name); } }
        public virtual Image  Icon   { get { return (null); } }

        public virtual bool CanRead  { get; } = true;
        public virtual bool CanWrite { get; } = true;

        public virtual FileFormatOption CreateReaderOption() { return (null); }
        public virtual FileFormatReader CreateReader() { return (null); }

        public virtual FileFormatOption CreateWriterOption() { return (null); }
        public virtual FileFormatWriter CreateWriter() { return (null); }

        public (FileFormatReader reader, FileFormatOption option) GetReader()
        {
            /* リーダー取得 */
            var reader = CreateReader();

            if (reader == null)return (null, null);

            /* オプション取得 */
            var option = CreateReaderOption();

            /* オプション編集 */
            if (option != null) {
                var editor = option.GetEditor();

                if (editor != null) {
                    if ((new FileFormatOptionForm(editor)).ShowDialog() != System.Windows.Forms.DialogResult.OK) {
                        return (null, null);
                    }
                }
            }

            return (reader, option);
        }

        public (FileFormatWriter writer, FileFormatOption option) GetWriter()
        {
            /* ライター取得 */
            var writer = CreateWriter();

            if (writer == null)return (null, null);

            /* オプション取得 */
            var option = CreateWriterOption();

            /* オプション編集 */
            if (option != null) {
                var editor = option.GetEditor();

                if (editor != null) {
                    if ((new FileFormatOptionForm(editor)).ShowDialog() != System.Windows.Forms.DialogResult.OK) {
                        return (null, null);
                    }
                }
            }

            return (writer, option);
        }
    }
}
