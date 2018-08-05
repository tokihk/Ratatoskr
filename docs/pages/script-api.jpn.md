<link href="../params.css" rel="stylesheet"></link>

# Script - API


## 定数

### PrintTargetType

    PacketLog     = 1 << 0,     // メッセージパケットとしてログに出力します
    EditorConsole = 1 << 1,     // スクリプトエディタのコンソールに出力します
    EditorComment = 1 << 2,     // スクリプトエディタのコメントウィンドウに出力します
    All = -1,                   // 全ての出力先に出力します

### WatchPacketType

    RawPacket  = 1 << 0,        // ゲートから発生した元パケットを対象とします
    ViewPacket = 1 << 1,        // 変換器を経由したパケットを対象とします

## メソッド

### API_Pause

`void API_Pause()`

スクリプトを実行状態のままブロックします。<br>

### API_Sleep

`void API_Sleep(uint msec)`

スクリプトを実行状態のまま指定時間ブロックします。

### API_Print

`void API_Print(object obj, PrintTargetType target = PrintTargetType.All)`

任意のオブジェクトテキストを出力します。

objには文字列を含めたオブジェクトを指定できます。

targetには出力先を指定します。<br>
複数の条件を指定することで複数の出力先へ同時に出力します。

### API_GateConnect

`void API_GateConnect(string gate_alias)`

指定したパターン名に一致するゲートを接続します。<br>
該当した全てのゲートが接続されるまでブロックします。

### API_GateDisconnect

`void API_GateDisconnect(string gate_alias)`

指定したパターン名に一致するゲートを接続します。<br>
該当した全てのゲートが切断されるまでブロックします。

### API_SendData

`void API_SendData(string gate_alias, byte[] data)`
`void API_SendData(string gate_alias, string bin_text)`

指定したパターン名に一致するゲートにデータを送信します。<br>
bin_textには送信パネルと同じコードパターンでデータを設定します。

### API_SendFileAsync

`ApiTask_SendFile API_SendFileAsync(string gate_alias, string file_path, uint send_block_size = 1024)`

指定したパターン名に一致するゲートにファイルを送信します。<br>
実行するとすぐに応答が返ります。<br>
送信状況は戻り値のオブジェクトを使用して確認します。<br>

### API_SendFile

`bool API_SendFile(string gate_alias, string file_path, uint send_block_size = 1024)`

指定したパターン名に一致するゲートにファイルを送信します。<br>
送信が完了もしくは失敗するまでブロックします。<br>

### API_CapturePacketAsync

`ApiTask_CapturePacket API_CapturePacketAsync(WatchPacketType target, PacketDetectedHandler callback, string filter = "")`

条件に一致するパケットをキャプチャします。

### API_WaitPacketAsync

`ApiTask_WaitPacket API_WaitPacketAsync(WatchPacketType target, string filter = "")`

条件に一致するパケットの受信を監視します。

### API_WaitPacket

`PacketObject API_WaitPacket(WatchPacketType target, string filter = "", int timeout_ms = -1)`

### API_ReplayRecordFileAsync

`ApiTask_ReplayRecordFile API_ReplayRecordFileAsync(string gate_alias, string file_path, string filter = "")`

### API_ReplayRecordFile

`bool API_ReplayRecordFile(string gate_alias, string file_path, string filter = "")`

## オブジェクト

### ApiTask_CapturePacket

<pre>
class ApiTask_CapturePacket
{
    public bool IsRunning { get; }

    public void Stop()
}
</pre>

### ApiTask_ReplayRecordFile

<pre>
class ApiTask_ReplayRecordFile
{
    public string GateAlias { get; }
    public string FilePath  { get; }
    public string Filter    { get; }

    public bool Success   { get; }
    public bool IsRunning { get; }

    public void Stop()
    public bool Join(int timeout_ms = -1)
}
</pre>

### ApiTask_SendFile

<pre>
class ApiTask_SendFile : IDisposable
{
    public string GateAlias     { get; }
    public string FilePath      { get; }
    public uint   SendBlockSize { get; }

    public bool Success { get; }
    public bool IsRunning { get; }

    public void Stop()
    public bool Join(int timeout_ms = -1)
}
</pre>

### ApiTask_WaitPacket

<pre>
class ApiTask_WaitPacket : IDisposable
{
    public PacketObject DetectPacket { get; }
    public bool IsRunning

    public void Stop()
    public PacketObject Join(int timeout_ms)
}
</pre>

## イベントハンドラ

### PacketDetectedHandler

<pre>
delegate void PacketDetectedHandler(object sender, PacketObject packet)
</pre>
