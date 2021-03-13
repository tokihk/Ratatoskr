using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.PacketView.Graph.DisplayModules
{
    internal class Display_Spectrum : DisplayModule
    {
        private decimal[][] points_;
        private int         point_in_ = 0;

        private uint       layer_count_ = 0;


        public Display_Spectrum(uint point_count)
        {
            point_count = Math.Max(1, point_count);

            var point_count_fft = (uint)1;

            while (point_count_fft < point_count) {
                point_count_fft <<= 1;
            }

            points_ = new decimal[point_count_fft][];
        }

        public override uint PointCount
        {
            get { return ((uint)points_.Length); }
        }

        protected override void OnClearData()
        {
            points_ = new decimal[points_.Length][];
            point_in_ = 0;
            layer_count_ = 0;
        }

        protected override void OnAssignData(decimal[] value)
        {
            points_[point_in_++] = value;
            if (point_in_ >= points_.Length) {
                point_in_ = 0;
            }

            layer_count_ = Math.Max(layer_count_, (uint)value.Length);
        }

        protected override bool OnLayerExistCheck(uint layer_index)
        {
            return (layer_index < layer_count_);
        }

        protected override void OnLoadGraphData(uint layer_index, uint data_index, ref decimal value)
        {
            var draw_point_offset = point_in_ + data_index;

            if (draw_point_offset >= points_.Length) {
                draw_point_offset -= points_.Length;
            }

            if (   (points_[draw_point_offset] != null)
                && (layer_index < points_[draw_point_offset].Length)
            ) {
                value = points_[draw_point_offset][layer_index];
            }
        }
    }
}
