using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RtsCore.Framework.FileFormat;

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

        public IEnumerable<FileFormatClass> SelectReaderFormatFromPath(string path, params Type[] type_filters)
        {
            /* 読込可能フォーマットのみ抽出 */
            var formats = GetReaderFormats(Formats, type_filters);

            /* 拡張子からフォーマットを選定 */
            return (GetFormatsFromPath(formats, path));
        }

        public IEnumerable<FileFormatClass> SelectWriterFormatFromPath(string path, params Type[] type_filters)
        {
            /* 書込可能フォーマットのみ抽出 */
            var formats = GetWriterFormats(Formats, type_filters);

            /* 拡張子からフォーマットを選定 */
            return (GetFormatsFromPath(formats, path));
        }

        public FileReadTargetInfo GetReadTargetFromPath(string path, params Type[] type_filters)
        {
            var infos = GetReadTargetFromPaths(new [] { path }, type_filters);

            if (infos == null)return (null);
            if (infos.Count() == 0)return (null);

            return (infos.First());
        }

        public IEnumerable<FileReadTargetInfo> GetReadTargetFromPaths(IEnumerable<string> paths, params Type[] type_filters)
        {
            var infos = new List<FileReadTargetInfo>();
            var formats = GetReaderFormats(Formats, type_filters);
            var formats_ext = (IEnumerable<FileFormatClass>)null;
            var format = (FileFormatClass)null;

            foreach (var path in paths) {
                /* 該当の可能性があるフォーマット一覧を取得 */
                formats_ext = GetFormatsFromPath(formats, path);

                /* 一つも見つからない場合は全てに一致する扱いとする */
                if ((formats_ext == null) || (formats_ext.Count() == 0)) {
                    formats_ext = formats;
                }

                if (formats_ext.Count() == 0)continue;

                format = formats_ext.First();

                infos.Add(new FileReadTargetInfo()
                {
                    FilePath = path,
                    Reader = format.CreateReader(),
                    Option = format.CreateReaderOption()
                });
            }

            return (infos);
        }

        public IEnumerable<FileReadTargetInfo> SelectReadTargetFromDialog(string init_dir, bool multi_select, bool any_file, params Type[] type_filters)
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

            /* フォーマット判定 */
            var infos = new List<FileReadTargetInfo>();

            if ((dialog.FilterIndex > 0) && (dialog.FilterIndex <= formats.Count())) {
                /* === 指定したフォーマットを初期値とする === */
                var format = formats.ElementAt(dialog.FilterIndex - 1);
                var reader = format.CreateReader();
                var option = format.CreateReaderOption();

                foreach (var name in dialog.FileNames) {
                    infos.Add(new FileReadTargetInfo()
                    {
                        FilePath = name,
                        Reader = reader,
                        Option = option
                    });
                }

            } else {
                /* === ファイル名から個々にフォーマットを判定して初期値とする === */
                var formats_ext = (IEnumerable<FileFormatClass>)null;
                var format = (FileFormatClass)null;

                foreach (var name in dialog.FileNames) {
                    /* 該当の可能性があるフォーマット一覧を取得 */
                    formats_ext = GetFormatsFromPath(formats, name);

                    /* 一つも見つからない場合は全てに一致する扱いとする */
                    if ((formats_ext == null) || (formats_ext.Count() == 0)) {
                        formats_ext = formats;
                    }

                    if (formats_ext.Count() == 0)continue;

                    format = formats_ext.First();

                    infos.Add(new FileReadTargetInfo()
                    {
                        FilePath = name,
                        Reader = format.CreateReader(),
                        Option = format.CreateReaderOption()
                    });
                }
            }

            return (infos);
        }

        public FileWriteTargetInfo SelectWriterTargetFromDialog(string init_dir, params Type[] type_filters)
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

            /* フォーマット判定 */
            var format = (FileFormatClass)null;

            if ((dialog.FilterIndex > 0) && (dialog.FilterIndex <= formats.Count())) {
                /* === 指定したフォーマットを返す === */
                format = formats.ElementAt(dialog.FilterIndex - 1);
            } else {
                /* === ファイル名からフォーマットを選出 === */
                var formats_ext = SelectWriterFormatFromPath(dialog.FileName, type_filters);

                if ((formats_ext != null) && (formats_ext.Count() > 0)) {
                    format = formats_ext.First();
                }
            }

            if (format == null)return (null);

            return (new FileWriteTargetInfo()
            {
                FilePath = dialog.FileName,
                Writer = format.CreateWriter(),
                Option = format.CreateWriterOption()
            });
        }
    }
}
