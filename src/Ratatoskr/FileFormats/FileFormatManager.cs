using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.FileFormats
{
    internal sealed class FileFormatManager
    {
        public List<FileFormatClass> Formats { get; } = new List<FileFormatClass>();


        private string GetDialogFilter(IEnumerable<FileFormatClass> formats, bool any_file)
        {
            var str = new StringBuilder();

            /* フォーマットリストからフィルター文字列を作成 */
            foreach (var format in formats) {
                var ext_filter = GetDialogExtFilter(format);

                str.AppendFormat("{0}({1})|{1}|", format.Name, ext_filter);
            }

            /* 最後の余分な | を削除 */
            str.Remove(str.Length - 1, 1);

            /* [*.*] を追加 */
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

        private IEnumerable<FileFormatClass> GetFormatsFromPath(IEnumerable<FileFormatClass> formats, string path)
        {
            if ((formats == null) || (path == null)) {
                return (null);
            }

            try {
                var path_ext = Path.GetExtension(path);

                if (path_ext == null)return (null);

                /* 最初のドットを削除 */
                if (path_ext.Length > 0) {
                    path_ext = path_ext.Substring(1);
                }

                return (from format in formats
                        where format.FileExtension.Contains(path_ext)
                        select format);

            } catch {
                return (null);
            }
        }

        private IEnumerable<FileFormatClass> GetReaderFormats(IEnumerable<FileFormatClass> formats, params Type[] type_filters)
        {
            if (formats == null)return (null);

            /* 読込可能フォーマットのみ抽出 */
            formats = from format in Formats
                      where format.CanRead
                      select format;

            /* フィルターが設定されている場合はフィルタリング */
            if (type_filters.Length > 0) {
                formats = from format in formats
                          let format_type = format.GetType()
                          from type in type_filters
                          where type.IsAssignableFrom(format_type)
                          select format;
            }

            return (formats);
        }

        private IEnumerable<FileFormatClass> GetWriterFormats(IEnumerable<FileFormatClass> formats, params Type[] type_filters)
        {
            if (formats == null)return (null);

            /* 読込可能フォーマットのみ抽出 */
            formats = from format in Formats
                      where format.CanWrite
                      select format;

            /* フィルターが設定されている場合はフィルタリング */
            if (type_filters.Length > 0) {
                formats = from format in formats
                          let format_type = format.GetType()
                          from type in type_filters
                          where type.IsAssignableFrom(format_type)
                          select format;
            }

            return (formats);
        }

        public FileFormatClass SelectReaderFormatFromPath(string path, params Type[] type_filters)
        {
            /* 読込可能フォーマットのみ抽出 */
            var formats = GetReaderFormats(Formats, type_filters);

            /* 拡張子からフォーマットを選定 */
            var formats_ext = GetFormatsFromPath(formats, path);

            /* フォーマットが見つからない場合は手動で選択 */
            if (formats_ext == null) {
                var dialog = new FileFormatSelectDialog(formats);

                if (dialog.ShowDialog() != DialogResult.OK)return (null);

                formats = new [] { dialog.Format };
            }

            return ((formats_ext.Count() > 0) ? (formats_ext.First()) : (null));
        }

        public FileFormatClass SelectWriterFormatFromPath(string path, params Type[] type_filters)
        {
            /* 読込可能フォーマットのみ抽出 */
            var formats = GetWriterFormats(Formats, type_filters);

            /* 拡張子からフォーマットを選定 */
            var formats_ext = GetFormatsFromPath(formats, path);

            return ((formats_ext.Count() > 0) ? (formats_ext.First()) : (null));
        }

        public FileFormatClass SelectReaderFormatFromDialog(string init_dir, bool multi_select, bool any_file, ref string[] paths, params Type[] type_filters)
        {
            /* 読込可能フォーマットのみ抽出 */
            var formats = GetReaderFormats(Formats, type_filters);

            /* ファイルを選択する */
            var dialog = new OpenFileDialog();

            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = false;
            dialog.InitialDirectory = init_dir;
            dialog.Multiselect = multi_select;
            dialog.Filter = GetDialogFilter(formats, any_file);

            if (dialog.ShowDialog() != DialogResult.OK)return (null);

            /* 選択したファイルパスを格納 */
            paths = dialog.FileNames;

            if ((dialog.FilterIndex > 0) && (dialog.FilterIndex <= formats.Count())) {
                /* === 指定したフォーマットを返す === */
                return (formats.ElementAt(dialog.FilterIndex - 1));
            } else {
                /* === ファイル名からフォーマットを選出 === */
                return (SelectReaderFormatFromPath(paths.First(), type_filters));
            }
        }

        public (FileFormatReader reader, FileFormatOption option, string[] paths) SelectReaderFromDialog(string init_dir, bool multi_select, bool any_file, params Type[] type_filters)
        {
            var paths = (string[])null;

            var format = SelectReaderFormatFromDialog(init_dir, multi_select, any_file, ref paths, type_filters);

            if (format == null)return (null, null, null);

            var reader = format.GetReader();

            if (reader.reader == null)return (null, null, null);

            return (reader.reader, reader.option, paths);
        }

        public FileFormatClass SelectWriterFormatFromDialog(string init_dir, ref string path, params Type[] type_filters)
        {
            /* 書込み可能フォーマットのみ抽出 */
            var formats = GetWriterFormats(Formats, type_filters);

            /* ファイルを選択する */
            var dialog = new SaveFileDialog();

            dialog.RestoreDirectory = true;
            dialog.CheckFileExists = false;
            dialog.CheckPathExists = false;
            dialog.InitialDirectory = init_dir;
            dialog.Filter = GetDialogFilter(formats, false);

            if (dialog.ShowDialog() != DialogResult.OK)return (null);

            /* 選択したファイルパスを格納 */
            path = dialog.FileName;

            if ((dialog.FilterIndex > 0) && (dialog.FilterIndex <= formats.Count())) {
                /* === 指定したフォーマットを返す === */
                return (formats.ElementAt(dialog.FilterIndex - 1));
            } else {
                /* === ファイル名からフォーマットを選出 === */
                return (SelectWriterFormatFromPath(path, type_filters));
            }
        }

        public (FileFormatWriter writer, FileFormatOption option, string path) SelectWriterFromDialog(string init_dir, params Type[] type_filters)
        {
            var path = (string)null;

            var format = SelectWriterFormatFromDialog(init_dir, ref path, type_filters);

            if (format == null)return (null, null, null);

            var writer = format.GetWriter();

            if (writer.writer == null)return (null, null, null);

            return (writer.writer, writer.option, path);
        }
    }
}
