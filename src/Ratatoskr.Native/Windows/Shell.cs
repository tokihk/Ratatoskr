using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace Ratatoskr.Native.Windows
{
    public static class Shell
    {
        private enum FileObjectType
        {
            NotFound,
            File,
            Directory,
        }


        private static FileObjectType GetFileObjectType(string path)
        {
            if (File.Exists(path)) {
                return (FileObjectType.File);
            } else if (Directory.Exists(path)) {
                return (FileObjectType.Directory);
            } else {
                return (FileObjectType.NotFound);
            }
        }

        private static void AttributeToReadonly(string path)
        {
            switch (GetFileObjectType(path)) {
                case FileObjectType.File:
                    AttributeToReadonly(new FileInfo(path));
                    break;

                case FileObjectType.Directory:
                    AttributeToReadonly(new DirectoryInfo(path));
                    break;

                default:
                    break;
            }
        }

        private static void AttributeToReadonly(FileInfo fi)
        {
            /* 自身(ディレクトリ)の読み取り専用を外す */
            if (fi.Attributes.HasFlag(FileAttributes.ReadOnly)) {
                fi.Attributes = FileAttributes.Normal;
            }
        }

        private static void AttributeToReadonly(DirectoryInfo di)
        {
            /* 自身(ディレクトリ)の読み取り専用を外す */
            if (di.Attributes.HasFlag(FileAttributes.ReadOnly)) {
                di.Attributes = FileAttributes.Normal;
            }

            /* ディレクトリ内のファイルの読み取り専用を外す */
            foreach (var fi in di.GetFiles()) {
                AttributeToReadonly(fi);
            }

            /* ディレクトリ内のディレクトリの読み取り専用を外す */
            foreach (var di_sub in di.GetDirectories()) {
                AttributeToReadonly(di_sub);
            }
        }

        public static void mkdir(string path)
        {
            if (GetFileObjectType(path) != FileObjectType.NotFound)return;

            Directory.CreateDirectory(path);
        }

        public static void rm(string path)
        {
            switch (GetFileObjectType(path)) {
                case FileObjectType.File:
                {
                    FileSystem.DeleteFile(path);
                }
                    break;

                case FileObjectType.Directory:
                {
                    FileSystem.DeleteDirectory(path, DeleteDirectoryOption.DeleteAllContents);
                }
                    break;

                default:
                    break;
            }
        }

        public static void cp(string path_src, string path_dst)
        {
            var src_type = GetFileObjectType(path_src);
            var dst_type = GetFileObjectType(path_dst);
            
            /* コピー元が存在しない場合は失敗 */
            if (src_type == FileObjectType.NotFound)return;

            switch (src_type) {
                case FileObjectType.Directory:
                {
                    /* コピー先がファイル以外の場合のみ成功 */
                    if (dst_type != FileObjectType.File) {
                        FileSystem.CopyDirectory(path_src, path_dst, true);
                    }
                }
                    break;

                case FileObjectType.File:
                {
                    /* コピー先がディレクトリ以外の場合のみ成功 */
                    if (dst_type != FileObjectType.Directory) {
                        FileSystem.CopyFile(path_src, path_dst, true);
                    }
                }
                    break;
            }
        }

        public static void mv(string path_src, string path_dst)
        {
            var src_type = GetFileObjectType(path_src);
            var dst_type = GetFileObjectType(path_dst);

            /* コピー元が存在しない場合は失敗 */
            if (src_type == FileObjectType.NotFound)return;

            switch (src_type) {
                case FileObjectType.Directory:
                {
                    /* コピー先がファイル以外の場合のみ成功 */
                    if (dst_type != FileObjectType.File) {
                        FileSystem.MoveDirectory(path_src, path_dst, true);
                    }
                }
                    break;

                case FileObjectType.File:
                {
                    /* コピー先がディレクトリ以外の場合のみ成功 */
                    if (dst_type != FileObjectType.Directory) {
                        FileSystem.MoveFile(path_src, path_dst, true);
                    }
                }
                    break;
            }
        }

        public static string[] find(string path_search, string name, bool ignore_case = false)
        {
            if (GetFileObjectType(path_search) != FileObjectType.Directory)return (null);

            return (
                FileSystem.FindInFiles(
                    path_search,
                    name,
                    ignore_case,
                    Microsoft.VisualBasic.FileIO.SearchOption.SearchAllSubDirectories)
                .ToArray());
        }
    }
}
