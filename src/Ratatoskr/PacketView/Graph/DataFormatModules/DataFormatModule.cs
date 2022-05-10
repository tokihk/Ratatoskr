using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketView.Graph.DataFormatModules
{
    internal class DataFormatModule
    {
        public delegate void ExtractedEventHandler(object sender, decimal value);


		private bool byte_little_endian_ = false;


        public event ExtractedEventHandler		Extracted;


		public DataFormatModule(PacketViewPropertyImpl prop)
		{
			ByteEndian = prop.InputDataByteEndian.Value;
			BitEndian = prop.InputDataBitEndian.Value;
		}

		public DataEndianType ByteEndian { get; }
		public DataEndianType BitEndian  { get; }

        public void InputData(IEnumerable<byte> data)
        {
            foreach (var data_one in data) {
                OnInputData(data_one);
            }
        }

        protected virtual void OnInputData(byte data)
        {
        }

        protected void ExtractValue(decimal value)
        {
			Extracted?.Invoke(this, value);
        }