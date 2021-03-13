using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config;

namespace Ratatoskr.Config.Data.Language
{
    internal sealed class LanguageConfig : ConfigRoot<LanguageConfig>
    {
        public LanguageConfig() : base("language") { }

        public delegate void LoadedEventHandler(object sender, EventArgs e);
        public event LoadedEventHandler Loaded = delegate (object sender, EventArgs e) {};


        public MainMessageConfig MainMessage { get; } = new MainMessageConfig();
        public MainUiConfig      MainUI      { get; } = new MainUiConfig();
        public PacketViewConfig  PacketView  { get; } = new PacketViewConfig();


        public bool Load()
        {
            /* 言語ファイルを読み込み */
            if (!LoadConfig(ConfigManager.GetCurrentProfileFilePath(ConfigManager.System.LanguageFile.Value)))return (false);

            Loaded(this, EventArgs.Empty);

            return (true);
        }
    }
}
