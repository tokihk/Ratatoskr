<link href="../params.css" rel="stylesheet" />

# Packet View - Sequential

![image](../_images/ss-packet-view-sequential.png)

データパケットのデータ部分のみをテキスト表示します。<br>
バイナリデータで表示するか、文字列として表示するかも切り替えることができます。<br>

デバイスとの簡易データ通信テストに適したビューです。

## Parameter

### Input setting - Bit shift

入力データをビット単位でシフト(遅延)させることができます。<br>
追い出されたビットは次のデータの先頭に結合されます。<br>
表示処理はシフト後のデータを元に行われます。

### Input setting - Echo back

自身が送信したデータも表示します。

### View setting - Draw type

表示方式です。

### View setting - HEX/BIN Boundary text

「Draw Type」が「HEX」もしくは「BIN」のときに、バイト境界に表示する文字列です。<br>
表示する文字列をそのまま記述します。

### View setting - LF(Line Feeed) Code

データビューの改行パターンです。<br>
指定したパターンのコードを検出したときに表示ビューで改行します。<br>
「Send Mode - Edit Mode」と同じ形式でパターンを指定します。

| 記述例 | 動作 |
| ----   | ---- |
| `03`            |「03」を受信したときに改行する |
| `'END'`         |「45 4E 44」を受信したときに改行する |
| `<UTF-16>'END'` |「45 00 4E 00 44 00」を受信したときに改行する |

<br><br>
