using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.FileFormats
{
    public sealed class FileFormatManager
    {
        public List<FileFormatClass> Formats { get; } = new List<FileFormatClass>();


        private string GetDialogFilter(IEnumerable<FileFormatClass> formats, bool any_file)
        {
            var str = new StringBuilder();

            /* フォーマットリストから作成 */
            foreach (var format in formats) {
                var ext_filter = GetDialogExtFilter(format);

                str.AppendFormat("{0}({1})|{1}|", format.Name, ext_filter);
            }
            str.Remove(str.Length - 1, 1);

            /* 全てのフォーマットを追加 */
            if (any_file) {
                if (str.Length > 0) {
                    str.Append('|');
                }
                str.Append("All Files(*.*)|*.*");
            }

            return (str.ToString());
        }

        private string GetDialogExtFilter(FileFormatClass format)
        {
            var str = new StringBuilder();

            foreach (var ext in format.FileExtension) {
                str.AppendFormat("*.{0};", ext);
            }
            str.Remove(str.Length - 1, 1);

            return (str.ToString());
        }

        private FileFormatClass GetFormatFromPath(IEnumerable<FileFormatClass> formats, string path)
        {
            try {
                var path_ext = Path.GetExtension(path);

                if ((path_ext == null) || (path_ext.Length == 0))return (null);

                return (formats.First(obj => obj.FileExtension.Contains(path_ext.Substring(1))));

            } catch {
                return (null);
            }
        }

        public FileFormatClass GetOpenFormatFromPath(string path)
        {
            var formats = Formats.FindAll(obj => obj.CanRead);
            var format = GetFormatFromPath(formats, path);

            if (format == null) {
                /* フォーマットが見つからなかった場合は手動で選択 */
                var dialog = new FileFormatSelectDialog(formats);

                if (dialog.ShowDialog() != DialogResult.OK)return (null);

                format = dialog.Format;
            }

            return (format);
        }

        public FileFormatClass GetSaveFormatFromPath(string path)
        {
            var formats = Formats.FindAll(format => format.CanWrite);

            return (GetFormatFromPath(formats, path));
        }

        public FileFormatClass OpenDialog(string init_dir, bool multi_select, bool any_file, ref string[] paths)
        {
            /* ファイルを選択する */
            var dialog = new OpenFileDialog();
            var formats = Formats.FindAll(format => format.CanRead);

            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = false;
            dialog.InitialDirectory = init_dir;
            dialog.Multiselect = multi_select;
            dialog.Filter = GetDialogFilter(formats, any_file);

            if (dialog.ShowDialog() != DialogResult.OK)return (null);

            /* 選択したファイルパスを格納 */
            paths = dialog.FileNames;

            if ((dialog.FilterIndex > 0) && (dialog.FilterIndex <= formats.Count)) {
                /* === 指定したフォーマットを返す === */
                return (formats.ElementAt(dialog.FilterIndex - 1));
            } else {
                /* === ファイル名からフォーマットを選出 === */
                return (GetOpenFormatFromPath(paths.First()));
            }
        }

        public FileFormatClass SaveDialog(string init_dir, ref string path)
        {
            var dialog = new SaveFileDialog();
            var formats = Formats.FindAll(format => format.CanWrite);

            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = false;
            dialog.InitialDirectory = init_dir;
            dialog.Filter = GetDialogFilter(formats, false);

            if (dialog.ShowDialog() != DialogResult.OK)return (null);

            /* 選択したファイルパスを格納 */
            path = dialog.FileName;

            if ((dialog.FilterIndex > 0) && (dialog.FilterIndex <= formats.Count)) {
                /* === 指定したフォーマットを返す === */
                return (formats.ElementAt(dialog.FilterIndex - 1));
            } else {
                /* === ファイル名からフォーマットを選出 === */
                return (GetFormatFromPath(formats, path));
            }
        }
    }
}
