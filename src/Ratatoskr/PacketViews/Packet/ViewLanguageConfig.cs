using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.PacketViews.Packet
{
    internal sealed class ViewLanguageConfig : ConfigHolder
    {
        public StringConfig Column_Alias               { get; } = new StringConfig("Alias");
        public StringConfig Column_Datetime_UTC        { get; } = new StringConfig("DateTime(UTC)");
        public StringConfig Column_Datetime_Local      { get; } = new StringConfig("DateTime(Local)");
        public StringConfig Column_Information         { get; } = new StringConfig("Information");
        public StringConfig Column_Mark                { get; } = new StringConfig("Mark");
        public StringConfig Column_Source              { get; } = new StringConfig("Source");
        public StringConfig Column_Destination         { get; } = new StringConfig("Destination");
        public StringConfig Column_DataLength          { get; } = new StringConfig("Data Length");
        public StringConfig Column_DataPreviewBinary   { get; } = new StringConfig("Data Preview(Binary)");
        public StringConfig Column_DataPreviewText     { get; } = new StringConfig("Data Preview(Text)");
        public StringConfig Column_DataPreviewCustom   { get; } = new StringConfig("Data Preview(Custom)");

        public StringConfig CopyMenu_Alias          { get; } = new StringConfig("Alias");
        public StringConfig CopyMenu_Datetime_UTC   { get; } = new StringConfig("DateTime(UTC)");
        public StringConfig CopyMenu_Datetime_Local { get; } = new StringConfig("DateTime(Local)");
        public StringConfig CopyMenu_Information    { get; } = new StringConfig("Information");
        public StringConfig CopyMenu_Mark           { get; } = new StringConfig("Mark");
        public StringConfig CopyMenu_Source         { get; } = new StringConfig("Source");
        public StringConfig CopyMenu_Destination    { get; } = new StringConfig("Destination");
        public StringConfig CopyMenu_DataLength     { get; } = new StringConfig("Data Length");
        public StringConfig CopyMenu_DataRaw        { get; } = new StringConfig("Data (Raw)");
        public StringConfig CopyMenu_DataHexText    { get; } = new StringConfig("Data (Hex Text)");

        public StringConfig CMenu_Packet_CopyToClipboard               { get; } = new StringConfig("Copy to clipboard");
        public StringConfig CMenu_Packet_CopyToClipboard_AllInfo_Csv   { get; } = new StringConfig("All Information(CSV)");
        public StringConfig CMenu_Packet_CopyToClipboard_DataString    { get; } = new StringConfig("Data: String");
        public StringConfig CMenu_Packet_CopyToClipboard_DataHex       { get; } = new StringConfig("Data: Hex");
        public StringConfig CMenu_Packet_CopyToClipboard_DataCustom    { get; } = new StringConfig("Data: Custom");
        public StringConfig CMenu_Packet_CopyToClipboard_NewLineOn     { get; } = new StringConfig("Line feed on");
        public StringConfig CMenu_Packet_CopyToClipboard_NewLineOff    { get; } = new StringConfig("Line feed off");
        public StringConfig CMenu_Packet_CopyToPacketList              { get; } = new StringConfig("Copy to Packet List");
    }
}
