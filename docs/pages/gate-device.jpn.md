<link href="../params.css" rel="stylesheet"></link>

# Gate Device

## Null

  ゲートに対して他のデバイスと同じ操作が行えますが、何も行わないデバイスです。

## Serial Port

  シリアルポート(COMポート)と通信します。

## TCP Server

  TCP/IPサーバーとして動作します。<br>
  IPv4/v6の両方に対応します。

## TCP Client

  TCP/IPクライアントとして動作します。<br>
  IPv4/v6の両方に対応します。

  サーバーにはホスト名およびIPアドレスを指定できます。<br>
  自身のポート番号を0にした場合は自動で割り当てを行います。

## UDP Client

  UDPクライアントとして動作します。<br>
  IPv4/v6の両方に対応します。
  アドレスにはホスト名およびIPアドレスを指定できます。<br>

## Ethernet

イーサネットをキャプチャします。<br>
本デバイスは受信動作のみ可能です。
ペイロードのみを受信することもできます。<br>

機能を使用するには[WinPcap](https://www.winpcap.org/)もしくは[Win10Pcap](http://www.win10pcap.org/ja/)が必要です。<br>

## USB Monitor

USBをキャプチャします。<br>
本デバイスは受信動作のみ可能です。

機能を使用するには[USBPcap](http://desowin.org/usbpcap/)が必要です。<br>
<span class="app-name" />を管理者権限モードで起動したときのみ使用可能です。

## Audio Device

録音デバイスからの受信、および、再生デバイスへの出力を行うことができます。

## Audio File

音声ファイルからの受信を行うことができます。
