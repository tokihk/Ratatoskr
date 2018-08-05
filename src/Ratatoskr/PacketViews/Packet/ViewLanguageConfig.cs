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
        public StringConfig Column_Class               { get; } = new StringConfig("Class");
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

        public StringConfig CMenu_Packet_Copy                           { get; } = new StringConfig("Copy");
        public StringConfig CMenu_Packet_Copy_AllInfo_Csv               { get; } = new StringConfig("All Information(CSV)");
        public StringConfig CMenu_Packet_Copy_Class                     { get; } = new StringConfig("Class");
        public StringConfig CMenu_Packet_Copy_Alias                     { get; } = new StringConfig("Alias");
        public StringConfig CMenu_Packet_Copy_Information               { get; } = new StringConfig("Information");
        public StringConfig CMenu_Packet_Copy_Source                    { get; } = new StringConfig("Source");
        public StringConfig CMenu_Packet_Copy_Destination               { get; } = new StringConfig("Destination");
        public StringConfig CMenu_Packet_Copy_Message                   { get; } = new StringConfig("Message");
        public StringConfig CMenu_Packet_Copy_DateTime                  { get; } = new StringConfig("DateTime");
        public StringConfig CMenu_Packet_Copy_DateTime_UTC_ISO8601      { get; } = new StringConfig("UTC: ISO8601 format");
        public StringConfig CMenu_Packet_Copy_DateTime_UTC_Display      { get; } = new StringConfig("UTC: Display format");
        public StringConfig CMenu_Packet_Copy_DateTime_Local_ISO8601    { get; } = new StringConfig("Local: ISO8601 format");
        public StringConfig CMenu_Packet_Copy_DateTime_Local_Display    { get; } = new StringConfig("Local: Display format");
        public StringConfig CMenu_Packet_Copy_Data                      { get; } = new StringConfig("Data (without Line Feed)");
        public StringConfig CMenu_Packet_Copy_DataLF                    { get; } = new StringConfig("Data (with Line Feed)");
        public StringConfig CMenu_Packet_Copy_Data_BitString            { get; } = new StringConfig("Bit string");
        public StringConfig CMenu_Packet_Copy_Data_HexString            { get; } = new StringConfig("Hex string");
        public StringConfig CMenu_Packet_Copy_Data_AsciiText            { get; } = new StringConfig("ASCII text");
        public StringConfig CMenu_Packet_Copy_Data_Utf8Text             { get; } = new StringConfig("UTF-8 text");
        public StringConfig CMenu_Packet_Copy_Data_Utf16BeText          { get; } = new StringConfig("UTF-16BE text");
        public StringConfig CMenu_Packet_Copy_Data_Utf16LeText          { get; } = new StringConfig("UTF-16LE text");
        public StringConfig CMenu_Packet_Copy_Data_ShiftJisText         { get; } = new StringConfig("Shift_JIS text");
        public StringConfig CMenu_Packet_Copy_Data_EucJpText            { get; } = new StringConfig("EUC-JP text");
        public StringConfig CMenu_Packet_Copy_Data_Custom               { get; } = new StringConfig("Custom preview format");
    }
}
