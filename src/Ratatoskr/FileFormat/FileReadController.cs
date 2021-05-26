using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.FileFormat
{
    internal class FileReadController
    {
		public static FileReadController Create(FileControlParam file)
		{
			if (   (file == null)
				|| (file.FilePath == null)
				|| (file.Format == null)
				|| (!File.Exists(file.FilePath))
			) {
				return (null);
			}

			var reader = file.Format.CreateReader();

			if (reader == null)return (null);

			var option = file.Format.CreateReaderOption();

			if (   ((file.Option != null) && (option == null))
				|| ((file.Option == null) && (option != null))
				|| ((file.Option != null) && (file.Option.GetType() != option.GetType()))
			) {
				return (null);
			}

			return (new FileReadController()
			{
				Param  = file,
				Reader = reader,
			});
		}

		public FileControlParam  Param  { get; private set; }
		public FileFormatReader Reader { get; private set; }
    }
}
