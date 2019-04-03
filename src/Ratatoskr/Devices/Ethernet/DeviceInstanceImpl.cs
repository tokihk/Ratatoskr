using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SharpPcap;
using SharpPcap.LibPcap;
using RtsCore.Framework.Device;
using RtsCore.Packet;
using Ratatoskr.Drivers.WinPcap;

namespace Ratatoskr.Devices.Ethernet
{
    internal sealed class DeviceInstanceImpl : DeviceInstance
    {
        private DevicePropertyImpl        prop_;
        private WinPcapPacketParserOption parser_option_;

        private LibPcapLiveDevice pcap_dev_ = null;

        private object send_sync_ = new object();


        public DeviceInstanceImpl(DeviceManagementClass devm, DeviceConfig devconf, DeviceClass devd, DeviceProperty devp)
            : base(devm, devconf, devd, devp)
        {
        }

        private void RecvPacket(RawCapture packet)
        {
            try {
                var packet_info = WinPcapPacketParser.Parse(packet, parser_option_);

                /* 通知 */
                NotifyRecvComplete(packet_info.DateTime, "", packet_info.Source, packet_info.Destination, packet_info.Data);

            } catch (Exception exp) {
                NotifyMessage(PacketPriority.Standard, "Ethernet", string.Format("Parse error.[{0}]", exp.Message));
            }
        }

        protected override EventResult OnConnectStart()
        {
            prop_ = Property as DevicePropertyImpl;

            /* パケット解析オプション取得 */
            parser_option_ = new WinPcapPacketParserOption(
                                    prop_.ViewSourceType.Value,
                                    prop_.ViewDestinationType.Value,
                                    prop_.ViewDataType.Value);

            /* 選択したインターフェースを取得 */
            try {
                pcap_dev_ = LibPcapLiveDeviceList.Instance.First(value => value.Name == prop_.Interface.Value);
            } catch {
                return (EventResult.Error);
            }

            if (pcap_dev_ == null)return (EventResult.Error);

            /* イベント登録 */
            pcap_dev_.OnPacketArrival += OnSharpPcapDev_OnPacketArrival;

            return (EventResult.Success);
        }

        protected override EventResult OnConnectBusy()
        {
            /* デバイスを開く */
            if (!pcap_dev_.Opened) {
                pcap_dev_.Open();
            }
            if (!pcap_dev_.Opened)return (EventResult.Busy);

            /* フィルタ設定 */
            try {
                pcap_dev_.Filter = prop_.Filter.Value;
            } catch {
                pcap_dev_.Filter = "";
            }

            /* キャプチャー開始 */
            if (!pcap_dev_.Started) {
                pcap_dev_.StartCapture();
            }
            if (!pcap_dev_.Started)return (EventResult.Busy);

            return (EventResult.Success);
        }

        protected override void OnConnected()
        {
            var prop = Property as DevicePropertyImpl;

        }

        protected override void OnDisconnectStart()
        {
            /* キャプチャー停止 */
            if (pcap_dev_.Started) {
                pcap_dev_.StopCapture();
            }

            /* デバイスクローズ */
            if (pcap_dev_.Opened) {
                pcap_dev_.Close();
            }
        }

        protected override PollState OnPoll()
        {
            var busy = false;

            SendPoll(ref busy);

            return ((busy) ? (PollState.Active) : (PollState.Idle));
        }

        protected override void OnSendRequest()
        {
            var busy = false;

            SendPoll(ref busy);
        }

        private void SendPoll(ref bool busy)
        {
            if (!prop_.SendEnable.Value)return;

            lock (send_sync_) {
                var send_data = GetSendData();

                if (send_data == null)return;

                pcap_dev_.SendPacket(send_data);

                busy = true;
            }
        }

        private void OnSharpPcapDev_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            RecvPacket(e.Packet);
        }
    }
}
