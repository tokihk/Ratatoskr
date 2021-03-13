using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Packet;

namespace Ratatoskr.PacketViews.Graph.DataFormatModules
{
    internal class DataFormatModule
    {
        public delegate void ExtractedEventHandler(object sender, PacketObject base_packet, decimal data);


        private PacketObject base_packet_;


        public event ExtractedEventHandler Extracted;


        public void InputData(PacketObject base_packet, IEnumerable<byte> data)
        {
            base_packet_ = base_packet;

            foreach (var data_one in data) {
                OnAssignData(data_one);
            }
        }

        protected virtual void OnAssignData(byte assign_data)
        {
        }

        protected void ExtractData(decimal data)
        {
            Extracted?.Invoke(this, base_packet_, data);
        }
    }
}
