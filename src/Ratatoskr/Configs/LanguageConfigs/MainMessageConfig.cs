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
    internal sealed class MainMessageConfig : ConfigHolder
    {
        public StringConfig PacketNowPreprocessing { get; } = new StringConfig("During packet processing.");
        public StringConfig PacketNowDrawing       { get; } = new StringConfig("In the packet drawing.");

        public StringConfig EventFileLoading       { get; } = new StringConfig("Loading packet file.");
        public StringConfig EventFileLoadComplete  { get; } = new StringConfig("Read completion of packet file");

        public StringConfig EventFileSaving        { get; } = new StringConfig("During storage in the packet file.");
        public StringConfig EventFileSaveComplete  { get; } = new StringConfig("Output completion to the packet file.");

        public StringConfig TimeStampManual        { get; } = new StringConfig("TimeStamp - Manual");
        public StringConfig TimeStampAuto          { get; } = new StringConfig("TimeStamp - Auto");

        public StringConfig Confirm_CreateNewProfile      { get; } = new StringConfig("Would you like to create a new profile?");
        public StringConfig Confirm_ImportProfileOverride { get; } = new StringConfig("There is a profile with the same name.\nDo you want to overwrite?");

        public StringConfig Description_FilterExp { get; } = new StringConfig(
@"EX: IsData && HexText == /02.*03/
●Conditional operator
A && B : The conditions are satisfied when both A and B conditions are satisfied.
A || B : The condition is satisfied when either condition of A or B is satisfied.

●Variable name
PacketCount: Returns the number of packets that arrived at the converter.
LastDelta: Returns the difference time from the last arrived packet.
IsControl: Returns whether the packet is a control packet. (unused)
IsMessage: Returns whether the packet is a message packet.
IsData: Returns whether the packet is a data packet.
Alias: Returns the Alias ​​of the gate that created the packet.
DateTime: Returns the time that the packet occurred.
Information: Returns the additional information of the packet.
Mark: Returns the mark value of the packet. (unused)
IsSend: Returns whether or not it is an outgoing packet.
IsRecv: Returns whether or not it is a received packet.
Source: Returns the sender information of the packet.
Destination: Returns the destination information of the packet.
DataSize: Returns the data size of the packet.
BitText: Returns the data part in binary notation.
HexText: Returns the data part in hexadecimal notation.
AsciiText: Returns the data part in ASCII notation.
Utf8Text: Returns the data part in UTF-8 notation.
UnicodeLText: Returns the data part in UTF-16LE notation.
UnicodeBText: Returns the data part in UTF-16BE notation.

●Parameter element
Number: 0 or 12345 or 12345.0123 or 0x01234567
DateTime: 2018-01-01T01:00:00.000Z or 2018-01-01T01:00:00.000+09:00
DateTimeOffset: 00:00:15.123
Text: ""abcdef""
Regex: /02.*03/
"
        );
    }
}

#if false
メモ
●条件演算子
A && B   | AとBの両方の条件を満たしたときに条件を満たします。
A || B   | AとBのどちらか一方の条件を満たしたときに条件を満たします。

●変数名
PacketCount: その変換機に到達したパケット数を返します。
LastDelta: 直前に到達したパケットとの差分時間を返します。
IsControl: そのパケットがコントロールパケットかどうかを返します。(未使用)
IsMessage: そのパケットがメッセージパケットかどうかを返します。
IsData: そのパケットがデータパケットかどうかを返します。
Alias: そのパケットを作成したゲートのAliasを返します。
DateTime: そのパケットが発生した時間を返します。
Information: そのパケットの付加情報を返します。
Mark: そのパケットのマーク値を返します。(未使用)
IsSend: 送信パケットかどうかを返します。
IsRecv: 受信パケットかどうかを返します。
Source: パケットの送信元情報を返します。
Destination: パケットの送信先情報を返します。
DataSize: そのパケットのデータサイズを返します。
BitText: データ部分を2進数表記で返します。
HexText: データ部分を16進数表記で返します。
AsciiText: データ部分をASCII表記で返します。
Utf8Text: データ部分をUTF-8表記で返します。
UnicodeLText: データ部分をUTF-16LE表記で返します。
UnicodeBText: データ部分をUTF-16BE表記で返します。

●パラメータ要素
Number:  0 or 12345 or 12345.0123 or 0x01234567
DateTime: 2018-01-01T01:00:00.000Z or 2018-01-01T01:00:00.000+09:00
DateTimeOffset: 00:00:15.123
Text: "abcdef"
Regex: /02.*03/

#endif
