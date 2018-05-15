using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Gate;
using Ratatoskr.Packet;
using Ratatoskr.Utility;

namespace Ratatoskr.Api
{
    public class ApiSandbox
    {
        [Flags]
        public enum WatchPacketType
        {
            RawPacket  = 1 << 0,
            ViewPacket = 1 << 1,
        }


        public delegate void PacketDetectedHandler(object sender, PacketObject packet);


        public virtual void API_Sleep(uint time_ms)
        {
            System.Threading.Thread.Sleep((int)time_ms);
        }

        public static void API_CommentOutToPacketView(string comment)
        {
            GatePacketManager.SetComment(comment);
        }

        public void API_GateConnect(string gate_alias)
        {
            var gate_objs = GateManager.FindGateObjectFromWildcardAlias(gate_alias);

            if (gate_objs == null)return;

            /* 対象の全ゲートを接続 */
            foreach (var gate_obj in gate_objs) {
                gate_obj.ConnectRequest = true;
            }

            /* 対象の全ゲートが接続されるのを待つ */
            while (gate_objs.FirstOrDefault(obj => (obj.DeviceClassID != Guid.Empty) && (obj.ConnectStatus != Devices.ConnectState.Connected)) != null) {
                API_Sleep(10);
            }
        }

        public void API_GateDisconnect(string gate_alias)
        {
            var gate_objs = GateManager.FindGateObjectFromWildcardAlias(gate_alias);

            if (gate_objs == null)return;

            /* 対象の全ゲートを切断 */
            foreach (var gate_obj in gate_objs) {
                gate_obj.ConnectRequest = false;
            }

            /* 対象の全ゲートが切断されるのを待つ */
            while (gate_objs.FirstOrDefault(obj => (obj.DeviceClassID != Guid.Empty) && (obj.ConnectStatus != Devices.ConnectState.Disconnected)) != null) {
                API_Sleep(10);
            }
        }

        public void API_SendData(string gate_alias, byte[] data)
        {
            if (data == null)return;

            /* 送信先ゲート取得 */
            var gates = GateManager.FindGateObjectFromWildcardAlias(gate_alias);

            if (gates == null)return;

            /* 送信実行 */
            foreach (var obj in gates) {
                obj.SendRequest(data);
            }
        }

        public void API_SendData(string gate_alias, string bin_text)
        {
            API_SendData(gate_alias, HexTextEncoder.ToByteArray(bin_text));
        }

        public ApiTask_SendFile API_SendFileAsync(string gate_alias, string file_path, uint send_block_size = 1024)
        {
            var obj = new ApiTask_SendFile(this, gate_alias, file_path, send_block_size);

            obj.StartAsync();

            return (obj);
        }

        public bool API_SendFile(string gate_alias, string file_path, uint send_block_size = 1024)
        {
            var obj = API_SendFileAsync(gate_alias, file_path, send_block_size);

            return (obj.Join());
        }

        public ApiTask_CapturePacket API_CapturePacketAsync(WatchPacketType target, PacketDetectedHandler callback, string filter = "")
        {
            var obj = new ApiTask_CapturePacket(this, target, filter);

            if (callback != null) {
                obj.PacketDetected += callback;
            }

            obj.StartAsync();

            return (obj);
        }

        public ApiTask_WaitPacket API_WaitPacketAsync(WatchPacketType target, string filter = "")
        {
            var obj = new ApiTask_WaitPacket(this, target, filter);

            obj.StartAsync();

            return (obj);
        }

        public PacketObject API_WaitPacket(WatchPacketType target, string filter = "", int timeout_ms = -1)
        {
            var obj = API_WaitPacketAsync(target, filter);

            return (obj.Join(timeout_ms));
        }

        public ApiTask_ReplayRecordFile API_ReplayRecordFileAsync(string gate_alias, string file_path, string filter = "")
        {
            var obj = new ApiTask_ReplayRecordFile(this, gate_alias, file_path, filter);

            obj.StartAsync();

            return (obj);
        }

        public bool API_ReplayRecordFile(string gate_alias, string file_path, string filter = "")
        {
            var obj = API_ReplayRecordFileAsync(gate_alias, file_path, filter);

            return (obj.Join());
        }
    }
}
