using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.Configs.LanguageConfigs
{
    internal sealed class MainUiConfig : ConfigHolder
    {
        public StringConfig AboutForm_Title   { get; } = new StringConfig("About Ratatoskr");

        public StringConfig MenuBar_File      { get; } = new StringConfig("File(&F)");
        public StringConfig MenuBar_File_Exit { get; } = new StringConfig("Exit(&X)");


        public StringConfig SCmdPanel_Target  { get; } = new StringConfig("Target");
        public StringConfig SCmdPanel_Command { get; } = new StringConfig("Command");

        public StringConfig MCmdPanel_Column_Command        { get; } = new StringConfig("Command");
        public StringConfig MCmdPanel_Column_DelayFixed     { get; } = new StringConfig("Fixed Delay[ms]");
        public StringConfig MCmdPanel_Column_DelayRandomMax { get; } = new StringConfig("Random Delay(MAX)[ms]");
    }
}
