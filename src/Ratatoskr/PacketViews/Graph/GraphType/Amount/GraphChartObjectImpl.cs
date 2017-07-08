using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;

namespace Ratatoskr.PacketViews.Graph.GraphType.Amount
{
    internal class GraphChartObjectImpl : GraphChartObject
    {
        private double[] view_data_;
        private int      view_data_in_ = 0;
        private bool     view_update_ = false;

        private double busy_data_ = 0;

        private System.Timers.Timer sampling_timer_ = new System.Timers.Timer();
        private object sampling_sync_ = new object();


        public GraphChartObjectImpl(ViewPropertyImpl prop) : base(prop)
        {
            view_data_ = new double[(int)Property.DrawPointNum.Value];

            sampling_timer_.AutoReset = true;
            sampling_timer_.Interval = (double)Property.SamplingInterval.Value;
            sampling_timer_.Elapsed += OnSampling;
            sampling_timer_.Start();
        }

        public override void Dispose()
        {
            sampling_timer_.Dispose();
        }

        public override bool IsViewUpdate
        {
            get {
                return (view_update_);
            }
        }

        protected override GraphViewData OnLoadViewData()
        {
            var draw_data = new double[view_data_.Length];

            /* 最も古いデータから表示 */
            var src_index = 0;
            var dst_index = 0;
            var data_now = double.NaN;
            var data_min = double.MaxValue;
            var data_max = double.MinValue;

            lock (sampling_sync_) {
                src_index = view_data_in_;
                if (src_index >= view_data_.Length) {
                    src_index = 0;
                }

                do {
                    data_now = view_data_[src_index++];
                    if (data_now < data_min)data_min = data_now;
                    if (data_now > data_max)data_max = data_now;

                    draw_data[dst_index++] = data_now;
                    if (src_index >= view_data_.Length) {
                        src_index = 0;
                    }
                } while (src_index != view_data_in_);

                view_update_ = false;
            }

            return (new GraphViewData(data_min, data_max, draw_data));
        }

        protected override void OnInputData(byte[] value)
        {
            lock (sampling_sync_) {
                busy_data_ += value.Length;
            }
        }

        private void OnSampling(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (sampling_sync_) {
                /* ビューバッファに格納 */
                view_data_[view_data_in_++] = busy_data_;
                if (view_data_in_ >= view_data_.Length) {
                    view_data_in_ = 0;
                }
                busy_data_ = 0;
                view_update_ = true;
            }
        }
    }
}
