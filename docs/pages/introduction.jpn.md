# Ratatoskr ユーザーマニュアル

![image](../_images/app_icon_128x128.png) ![image](../_images/app_logo_600x120.png)

https://github.com/tokihk/Ratatoskr

Ratatoskrは、パソコンに接続できる様々なデバイスと通信を行うことができる開発支援ツールです。
シリアルポートやTCP/UDP等の様々な通信ポートに対して、汎用的な通信デバッグ環境を提供します。

見やすいパケットビューや、操作性を重視したインターフェースにより、ポートへの入出力を直感的に操作することができます。

Ratatoskrには以下の特徴があります。

* シリアルポート、TCP/IP、UDP等の様々な通信に対応
  * Serial Port
  * TCP/IP Server or Client
  * UDP
  * USB Capture
* パケット単位やデータ単位など、用途に合わせたパケットビューに対応
* 複数ポートに対しての同時接続
* 複数種類のパケットビューを同時表示
* パケットログの保存/読み込み/自動保存
* フィルタリング、パケット結合等のリアルタイムパケット制御
* ポートからポートへのリダイレクト転送

![](../_images/basic.png)

## 著作権

* Toki.H.K - [Mail](mailto:toki.h.k@outlook.com) , [GitHub](https://github.com/tokihk)

## ライセンス

　　GPL3 (GNU GENERAL PUBLIC LICENSE v3)

## 動作環境

* .NET Framework 4.7

## インストール

.NET Framworkをインストールした後に、任意のフォルダに解凍して実行してください。

一部の機能を使用するときは他ソフトウェアが必要になります。

| 機能 | 必要ソフトウェア |
| :--- | :--- |
| USB Monitor | USBPcap ([http://desowin.org/usbpcap/](http://desowin.org/usbpcap/ "USBPcap")) |
| Ehernet | WinPcap ([https://www.winpcap.org/](https://www.winpcap.org/)) |

### アンインストール

Ratatoskrはレジストリを使用しません。

インストールで解凍したファイルを削除した後に、設定ファイルを削除してください。
設定ファイルは以下の場所に存在します。
`%APPDATA%\Ratatoskr`

