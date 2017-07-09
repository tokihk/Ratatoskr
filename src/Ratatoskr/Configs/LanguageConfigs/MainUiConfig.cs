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
        public StringConfig Title_AdminMode { get; } = new StringConfig("Administrator Mode");

        public StringConfig AboutForm_Title   { get; } = new StringConfig("About Ratatoskr");

        public StringConfig MenuBar_File                 { get; } = new StringConfig("File");
        public StringConfig MenuBar_File_Open            { get; } = new StringConfig("Open");
        public StringConfig MenuBar_File_Save            { get; } = new StringConfig("Save");
        public StringConfig MenuBar_File_Save_Original   { get; } = new StringConfig("Original");
        public StringConfig MenuBar_File_Save_Shaping    { get; } = new StringConfig("Shaping");
        public StringConfig MenuBar_File_SaveAs          { get; } = new StringConfig("Save as");
        public StringConfig MenuBar_File_SaveAs_Original { get; } = new StringConfig("Original...");
        public StringConfig MenuBar_File_SaveAs_Shaping  { get; } = new StringConfig("Shaping...");
        public StringConfig MenuBar_File_Exit            { get; } = new StringConfig("Exit");

        public StringConfig MenuBar_Edit                 { get; } = new StringConfig("Edit");

        public StringConfig SCmdPanel_Target  { get; } = new StringConfig("Target");
        public StringConfig SCmdPanel_Command { get; } = new StringConfig("Command");

        public StringConfig MCmdPanel_Column_Command        { get; } = new StringConfig("Command");
        public StringConfig MCmdPanel_Column_DelayFixed     { get; } = new StringConfig("Fixed Delay[ms]");
        public StringConfig MCmdPanel_Column_DelayRandomMax { get; } = new StringConfig("Random Delay(MAX)[ms]");
    }
}
