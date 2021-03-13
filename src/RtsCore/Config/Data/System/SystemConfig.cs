using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Config;
using RtsCore.Config.Types;

namespace RtsCore.Framework.Config.Data.System
{
    public sealed class SystemConfig : ConfigManagerBase<SystemConfig>
    {
        public SystemConfig() : base("system")
        {
        }

        public StringConfig          CurrentDirectory { get; } = new StringConfig("");

        public ApplicationCoreConfig ApplicationCore  { get; } = new ApplicationCoreConfig();

        public ProfileConfig         Profile          { get; } = new ProfileConfig();
        public StringConfig          LanguageFile     { get; } = new StringConfig("");
        public MainWindowConfig      MainWindow       { get; } = new MainWindowConfig();
        public ScriptWindowConfig    ScriptWindow     { get; } = new ScriptWindowConfig();

        public AutoPacketSaveConfig  AutoPacketSave { get; } = new AutoPacketSaveConfig();
        public AutoTimeStampConfig   AutoTimeStamp  { get; } = new AutoTimeStampConfig();

        public BoolConfig AutoScroll { get; } = new BoolConfig(true);

        public MailListConfig MailList { get; } = new MailListConfig();
    }
}
