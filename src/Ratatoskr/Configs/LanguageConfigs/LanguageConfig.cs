using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;
using Ratatoskr.Generic.Generic;

namespace Ratatoskr.Configs.LanguageConfigs
{
    internal sealed class LanguageConfig : ConfigManagerBase<LanguageConfig>
    {
        public MainMessageConfig MainMessage { get; } = new MainMessageConfig();
        public MainUiConfig      MainUI      { get; } = new MainUiConfig();
        public PacketViewConfig  PacketView  { get; } = new PacketViewConfig();


        public LanguageConfig() : base("language") { }

        public bool Load()
        {
            var path_profile = ConfigManager.User.GetProfilePath();

            if (path_profile == null)return (false);

            var path_language = Path.Combine(path_profile, "default.gdlng");

            /* プロファイルを読み込み */
            return (LoadConfig(path_language));
        }

        public bool Save()
        {
            var path_profile = ConfigManager.User.GetProfilePath();

            if (path_profile == null)return (false);

            var path_language = Path.Combine(path_profile, "default.gdlng");

            /* プロファイルを読み込み */
            return (SaveConfig(path_language));
        }
    }
}
