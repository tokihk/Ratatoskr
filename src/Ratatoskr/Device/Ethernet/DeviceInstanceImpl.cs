#define __SHARPPCAP__

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General.Packet;
using Ratatoskr.General.Pcap;
using Ratatoskr.General.Pcap.SharpPcap;

#if __SHARPPCAP__
using SharpPcap;
using SharpPcap.LibPcap;
#elif __PCAPDOTNET__
using PcapDotNet.Core;
using PcapDotNet.Packets;
#endif

namespace Ratatoskr.Device.Ethernet
{
    internal sealed class DeviceInstanceImpl : DeviceInstance
    {
        private DevicePropertyImpl			prop_;

        private PcapPacketParserOption		parser_option_;

#if __SHARPPCAP__
        private LibPcapLiveDevice pcap_dev_ = null;
#elif __PCAPDOTNET__
        private PacketDevice       pcap_dev_ = null;
        private PacketCommunicator pcap_comm_ = null;
#endif


        public DeviceInstanceImpl(DeviceConfig devconf, DeviceClass devd, DeviceProperty devp)
            : base(devconf, devd, devp)
        {
        }

		private void NotifyPcapPacket(PacketObject packet)
		{
			if (packet.Direction == PacketDirection.Send) {
				NotifySendComplete(packet.MakeTime, packet.Information, packet.Source, packet.Destination, packet.Data);
			} else {
				NotifyRecvComplete(packet.MakeTime, packet.Information, packet.Source, packet.Destination, packet.Data);
			}
		}

        protected override void OnConnectStart()
        {
            prop_ = Property as DevicePropertyImpl;
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

				/* パケット解析オプション取得 */
				parser_option_ = new PcapPacketParserOption()
				{
					LinkType = (PcapLinkType)pcap_dev_.LinkType,
					InfoType = prop_.PacketInfo.Value,
					SourceType = prop_.PacketSource.Value,
					DestinationType = prop_.PacketDestination.Value,
					DataContentsType = prop_.PacketData.Value
				};

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

#if __SHARPPCAP__
#elif __PCAPDOTNET__
            RecvPoll(ref busy);
#endif

            return ((busy) ? (PollState.Active) : (PollState.Idle));
        }

		private void SendPoll(ref bool busy)
		{
			var send_data = GetSendData();

			if (send_data == null)return;

//			var packet_pdt = new PacketDotNet.EthernetPacket(new PacketDotNet.Utils.ByteArraySegment(send_data));

			pcap_dev_.SendPacket(send_data);
		}

#if __PCAPDOTNET__
        private void RecvPoll(ref bool busy)
        {
            pcap_comm_.ReceiveSomePackets(out int countGot, 128, OnPcapDotNet_OnPacketReceive);

            if (countGot > 0) {
                busy = true;
            }
        }
#endif

#if __SHARPPCAP__
        private void OnSharpPcapDev_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            try {
                NotifyPcapPacket(SharpPcapParser.ParseAndBuild(e.Packet, parser_option_));
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
