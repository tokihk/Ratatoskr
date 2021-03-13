using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config;
using Ratatoskr.Config.Data.Language;
using Ratatoskr.Config.Data.User;
using Ratatoskr.Config.Data.System;

namespace Ratatoskr.Forms.OptionEditForm
{
	internal class OptionEditData
	{
		public static OptionEditData LoadFromCurrent()
		{
			return (new OptionEditData()
			{
				System   = ConfigManager.System.DeepClone(),
				User     = ConfigManager.User.DeepClone(),
				Language = ConfigManager.Language.DeepClone(),
			});
		}

		public static void SaveToCurrent(OptionEditData edit_data)
		{
			ConfigManager.System.DeepCopy(edit_data.System);
			ConfigManager.User.DeepCopy(edit_data.User);
			ConfigManager.Language.DeepCopy(edit_data.Language);
		}


		public SystemConfig		System   { get; private set; } = new SystemConfig();
		public UserConfig		User     { get; private set; } = new UserConfig();
		public LanguageConfig	Language { get; private set; } = new LanguageConfig();


		public OptionEditData()
		{
		}
	}
}
