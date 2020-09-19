using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Framework.Device;
using RtsCore.Packet;
using RtsPlugin.Pcap.Utility;

#if __SHARPPCAP__
using SharpPcap;
using SharpPcap.LibPcap;
#elif __PCAPDOTNET__
using PcapDotNet.Core;
using PcapDotNet.Packets;
#endif

namespace RtsPlugin.Pcap.Devices.EthernetCapture
{
    internal sealed class DeviceInstanceImpl : DeviceInstance
    {
        private DevicePropertyImpl        prop_;

        private PcapPacketParserOption parser_option_;

#if __SHARPPCAP__
        private LibPcapLiveDevice pcap_dev_ = null;
#elif __PCAPDOTNET__
        private PacketDevice       pcap_dev_ = null;
        private PacketCommunicator pcap_comm_ = null;
#endif


        public DeviceInstanceImpl(DeviceManagementClass devm, DeviceConfig devconf, DeviceClass devd, DeviceProperty devp)
            : base(devm, devconf, devd, devp)
        {
        }

        private void NotifyPacket(PcapPacketInfo pinfo)
        {
            try {
                /* 通知 */
                if (pinfo.Direction == PacketDirection.Recv) {
                    NotifyRecvComplete(pinfo.DateTime, pinfo.Information, pinfo.Source, pinfo.Destination, pinfo.Data);
                } else {
                    NotifySendComplete(pinfo.DateTime, pinfo.Information, pinfo.Source, pinfo.Destination, pinfo.Data);
                }

            } catch (Exception exp) {
                NotifyMessage(PacketPriority.Standard, "Ethernet", string.Format("Parse error.[{0}]", exp.Message));
            }
        }

        protected override void OnConnectStart()
        {
            prop_ = Property as DevicePropertyImpl;

            /* パケット解析オプション取得 */
            parser_option_ = new PcapPacketParserOption(
                                    prop_.ViewSourceType.Value,
                                    prop_.ViewDestinationType.Value,
                                    prop_.ViewDataType.Value);
        }

        protected override EventResult OnConnectBusy()
        {
            try {
                /* 選択したインターフェースを取得 */
#if __SHARPPCAP__
                pcap_dev_ = LibPcapLiveDeviceList.Instance.First(value => value.Name == prop_.Interface.Value);
#elif __PCAPDOTNET__
                pcap_dev_ = LivePacketDevice.AllLocalMachine.First(value => value.Name == prop_.Interface.Value);
#endif

                if (pcap_dev_ == null)throw new Exception();

                /* イベント登録 */
#if __SHARPPCAP__
                pcap_dev_.OnPacketArrival += OnSharpPcapDev_OnPacketArrival;
#endif

                /* デバイスを開く */
#if __SHARPPCAP__
                pcap_dev_.Open();
                if (!pcap_dev_.Opened)throw new Exception();

#elif __PCAPDOTNET__
                if (pcap_comm_ == null) {
                    pcap_comm_ = pcap_dev_.Open();
                }
                if (pcap_comm_ == null)throw new Exception();
#endif

                /* フィルタ設定 */
#if __SHARPPCAP__
                pcap_dev_.Filter = prop_.Filter.Value;
#elif __PCAPDOTNET__
                pcap_comm_.SetFilter(prop_.Filter.Value);
#endif

                /* キャプチャー開始 */
#if __SHARPPCAP__
                pcap_dev_.StartCapture();
                if (!pcap_dev_.Started)throw new Exception();
#elif __PCAPDOTNET__
                if (pcap_comm_.ReceivePackets(0, OnPcapDotNet_OnPacketReceive) != PacketCommunicatorReceiveResult.Ok)return (EventResult.Busy);
#endif
            } catch {
#if __SHARPPCAP__
                pcap_dev_?.Close();
                pcap_dev_ = null;
#elif __PCAPDOTNET__
#endif
            }

#if __SHARPPCAP__
            if (pcap_dev_ == null)return (EventResult.Error);
#elif __PCAPDOTNET__
            if (pcap_comm_ == null)return (EventResult.Error);
#endif

            return (EventResult.Success);
        }

        protected override void OnConnected()
        {
            var prop = Property as DevicePropertyImpl;

        }

        protected override void OnDisconnectStart()
        {
            /* キャプチャー停止 */
#if __SHARPPCAP__
            if (pcap_dev_ != null) {
                if (pcap_dev_.Started) {
                    pcap_dev_.StopCapture();
                }

                /* デバイスクローズ */
                if (pcap_dev_.Opened) {
                    pcap_dev_.Close();
                }
            }

#elif __PCAPDOTNET__
            if (pcap_comm_ != null) {
                pcap_comm_.Dispose();
                pcap_comm_ = null;
            }
#endif
        }

        protected override PollState OnPoll()
        {
            var busy = false;

            SendPoll(ref busy);
            RecvPoll(ref busy);

            return ((busy) ? (PollState.Active) : (PollState.Idle));
        }

        private void SendPoll(ref bool busy)
        {
            if (!prop_.SendEnable.Value)return;

            var send_data = GetSendData();

            if (send_data == null)return;

#if __SHARPPCAP__
            pcap_dev_.SendPacket(send_data);
#elif __PCAPDOTNET__
            pcap_comm_.SendPacket(new Packet(send_data, DateTime.Now, DataLinkKind.Ethernet));
#endif

            busy = true;
        }

        private void RecvPoll(ref bool busy)
        {
#if __SHARPPCAP__
#elif __PCAPDOTNET__
            pcap_comm_.ReceiveSomePackets(out int countGot, 128, OnPcapDotNet_OnPacketReceive);

            if (countGot > 0) {
                busy = true;
            }
#endif
        }

#if __SHARPPCAP__
        private void OnSharpPcapDev_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            try {
                NotifyPacket(SharpPcapPacketParser.Parse(e.Device, e.Packet, parser_option_));
            } catch (Exception exp) {
                NotifyMessage(PacketPriority.Standard, "Ethernet", string.Format("Parse error.[{0}]", exp.Message));
            }
        }
#elif __PCAPDOTNET__
        private void OnPcapDotNet_OnPacketReceive(Packet packet)
        {
            try {
            } catch (Exception exp) {
                NotifyMessage(PacketPriority.Standard, "Ethernet", string.Format("Parse error.[{0}]", exp.Message));
            }
        }
#endif
    }
}
