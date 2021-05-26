using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Config;
using Ratatoskr.Config.Types;

namespace Ratatoskr.Config.Data.Language
{
    public sealed class MainUiConfig : ConfigHolder
    {
        public StringConfig Title_AdminMode { get; } = new StringConfig("Administrator Mode");

        public StringConfig AboutForm_Title   { get; } = new StringConfig("About " + ConfigManager.Fixed.ApplicationName.Value);

        public StringConfig MenuBar_File                 { get; } = new StringConfig("File");
        public StringConfig MenuBar_File_Open            { get; } = new StringConfig("Open");
        public StringConfig MenuBar_File_Save            { get; } = new StringConfig("Save");
        public StringConfig MenuBar_File_Save_Original   { get; } = new StringConfig("Raw packets");
        public StringConfig MenuBar_File_Save_Shaping    { get; } = new StringConfig("Converted packets");
        public StringConfig MenuBar_File_SaveAs          { get; } = new StringConfig("Save as");
        public StringConfig MenuBar_File_SaveAs_Original { get; } = new StringConfig("Raw packets...");
        public StringConfig MenuBar_File_SaveAs_Shaping  { get; } = new StringConfig("Converted packets...");
        public StringConfig MenuBar_File_Exit            { get; } = new StringConfig("Exit");

        public StringConfig MenuBar_Edit                 { get; } = new StringConfig("Edit");

        public StringConfig MenuBar_View                 { get; } = new StringConfig("View");

        public StringConfig MenuBar_Tool                 { get; } = new StringConfig("Tool");

        public StringConfig MenuBar_Help                 { get; } = new StringConfig("Help");
        public StringConfig MenuBar_Help_Document        { get; } = new StringConfig(ConfigManager.Fixed.ApplicationName.Value + " Document");
        public StringConfig MenuBar_Help_About           { get; } = new StringConfig("About " + ConfigManager.Fixed.ApplicationName.Value);

        public StringConfig SCmdPanel_Target  { get; } = new StringConfig("Target");
        public StringConfig SCmdPanel_Command { get; } = new StringConfig("Command");

        public StringConfig MCmdPanel_Title                 { get; } = new StringConfig("Send data list");
        public StringConfig MCmdPanel_Column_Command        { get; } = new StringConfig("Command");
        public StringConfig MCmdPanel_Column_DelayFixed     { get; } = new StringConfig("Fixed Delay[ms]");
        public StringConfig MCmdPanel_Column_DelayRandomMax { get; } = new StringConfig("Random Delay(MAX)[ms]");

        public StringConfig WLPanel_Title                 { get; } = new StringConfig("Watch list");
        public StringConfig WLPanel_Column_Target         { get; } = new StringConfig("Target");
        public StringConfig WLPanel_Column_Expression     { get; } = new StringConfig("Expression");
        public StringConfig WLPanel_Column_DetectCount    { get; } = new StringConfig("Detect Count");
        public StringConfig WLPanel_Column_NtfPlan        { get; } = new StringConfig("Notify plan");
        public StringConfig WLPanel_Column_NtfPlan_Event  { get; } = new StringConfig("Event");
        public StringConfig WLPanel_Column_NtfPlan_Dialog { get; } = new StringConfig("Dialog");
        public StringConfig WLPanel_Column_NtfPlan_Mail   { get; } = new StringConfig("Mail");

		public StringConfig STPanel_Title { get; } = new StringConfig("Send traffic");
	}
}
