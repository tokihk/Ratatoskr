using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ratatoskr.FileFormat
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

                str.AppendFormat("{0}|{1}|", format.ToString(), ext_filter);
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
                str.AppendFormat("*{0};", ext);
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
                return (from format in formats
						from format_ext in format.FileExtension
						where path.IndexOf(format_ext, Math.Max(0, path.Length - format_ext.Length)) >= 0
                        select format);

            } catch {
                return (null);
            }
        }

        private IEnumerable<FileFormatClass> GetReadFormats(IEnumerable<FileFormatClass> formats, params Type[] type_filters)
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

        private IEnumerable<FileFormatClass> GetWriteFormats(IEnumerable<FileFormatClass> formats, params Type[] type_filters)
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

        public IEnumerable<FileFormatClass> SelectReadFormatFromPath(string path, params Type[] type_filters)
        {
            /* 拡張子からフォーマットを選定 */
            return (GetFormatsFromPath(GetReadFormats(Formats, type_filters), path));
        }

        public IEnumerable<FileFormatClass> SelectWriteFormatFromPath(string path, params Type[] type_filters)
        {
            /* 拡張子からフォーマットを選定 */
            return (GetFormatsFromPath(GetWriteFormats(Formats, type_filters), path));
        }

        private FileControlParam GetReadControllerFromPath(IEnumerable<FileFormatClass> formats, string path)
        {
            if (formats.Count() == 0)return (null);

            /* 拡張子を基に該当の可能性があるフォーマットのみを取得 */
            var formats_ext = GetFormatsFromPath(formats, path);

            /* 一つも見つからない場合は全てのフォーマットを対象とする */
            if ((formats_ext == null) || (formats_ext.Count() == 0)) {
                formats_ext = formats;
            }

            var format = formats_ext.First();

            return (new FileControlParam()
            {
                FilePath = path,
				Format   = format,
				Option   = format.CreateReaderOption(),
            });
        }

        public FileControlParam GetReadControllerFromPath(string path, params Type[] type_filters)
        {
			return (GetReadControllerFromPath(GetReadFormats(Formats, type_filters), path));
        }

        private IEnumerable<FileControlParam> GetReadControllerFromPaths(IEnumerable<FileFormatClass> formats, IEnumerable<string> paths)
        {
            var controllers = new List<FileControlParam>();
			var controller = (FileControlParam)null;

            foreach (var path in paths) {
				controller = GetReadControllerFromPath(formats, path);
				if (controller != null) {
					controllers.Add(controller);
				}
            }

            return (controllers);
        }

        public IEnumerable<FileControlParam> GetReadControllerFromPaths(IEnumerable<string> paths, params Type[] type_filters)
        {
			return (GetReadControllerFromPaths(GetReadFormats(Formats, type_filters), paths));
        }

        public IEnumerable<FileControlParam> SelectReadControllerFromDialog(string init_dir, bool multi_select, bool any_file, params Type[] type_filters)
        {
            /* 読込可能フォーマットのみ抽出 */
            var formats = GetReadFormats(Formats, type_filters);

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
            var controllers = new List<FileControlParam>();

            if ((dialog.FilterIndex > 0) && (dialog.FilterIndex <= formats.Count())) {
                /* === 指定したフォーマットを初期値とする === */
                var format = formats.ElementAt(dialog.FilterIndex - 1);

                foreach (var name in dialog.FileNames) {
                    controllers.Add(new FileControlParam()
                    {
                        FilePath = name,
						Format = format,
						Option = format.CreateReaderOption(),
                    });
                }

            } else {
                /* === ファイル名から個々にフォーマットを判定して初期値とする === */
				controllers.AddRange(GetReadControllerFromPaths(formats, dialog.FileNames));
            }

            return (controllers);
        }

        public FileControlParam SelectWriteControllerFromDialog(string init_dir, params Type[] type_filters)
        {
            /* 書込み可能フォーマットのみ抽出 */
            var formats = GetWriteFormats(Formats, type_filters);

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
                var formats_ext = SelectWriteFormatFromPath(dialog.FileName, type_filters);

                if ((formats_ext != null) && (formats_ext.Count() > 0)) {
                    format = formats_ext.First();
                }
            }

            if (format == null)return (null);

            return (new FileControlParam()
            {
                FilePath = dialog.FileName,
                Format = format,
				Option = format.CreateWriterOption(),
            });
        }
    }
}
