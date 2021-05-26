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

namespace Ratatoskr.Forms.ConfigEditor
{
	internal class ConfigEditorData
	{
		public static ConfigEditorData LoadFromCurrentConfig()
		{
			return (new ConfigEditorData()
			{
				System   = ConfigManager.System.DeepClone(),
				User     = ConfigManager.User.DeepClone(),
				Language = ConfigManager.Language.DeepClone(),
			});
		}

		public SystemConfig		System   { get; private set; } = new SystemConfig();
		public UserConfig		User     { get; private set; } = new UserConfig();
		public LanguageConfig	Language { get; private set; } = new LanguageConfig();


		public ConfigEditorData()
		{
		}
	}
}
