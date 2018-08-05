<link href="../params.css" rel="stylesheet"></link>

# Script - Code Style

スクリプトは Ｃ＃言語にて記述することができます。
クラス/関数定義も対応しています。

.NET Frameworkの基本モジュールも使用することができます。

## 記述例

C#言語で記述しますが、mainメソッドは存在しません。<br>
スクリプト起動時にビルド動作は行いますが、Basic,Ruby,Python等の
インタプリタ言語のように先頭から順番に実行します。

<pre>
void CountOut(int count_max)
{
  for (var count = 0; count < count_max; count++) {
    API_Print(count);
  }
}

API_Print("Count Start");
CountOut();
API_Print("Count End");
</pre>

## スクリプト例

<pre>
// 全てのゲートを接続する
API_GateConnect("*");
</pre>

<pre>
// 全てのゲートを切断する
API_GateDisconnect("*");
</pre>

<pre>
// 0x00～0xFFのデータを全てのゲートに出力する
for (var count = 0; count <= 0xFF; count++) {
  API_SendData("*", count)
}
</pre>

<pre>
// 0x02で始まって0x03で終わる受信データを10秒間待つ
if (API_WaitPacket(WatchPacketType.RawPacket, "IsRecv && HexText == /02.*03/"), 10000) {
  API_Print("Detect");
} else {
  API_Print("Not Detect");
}
</pre>
