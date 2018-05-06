using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Packet;

namespace Ratatoskr.PacketViews.Graph
{
    internal abstract class GraphChartObject : IDisposable
    {
        public GraphChartObject(ViewPropertyImpl prop)
        {
            Property = prop;
        }

        public virtual void Dispose()
        {
        }

        public ViewPropertyImpl Property { get; }

        public virtual bool IsViewUpdate
        {
            get { return (false); }
        }

        public GraphViewData LoadViewData()
        {
            return (OnLoadViewData());
        }

        public void InputPacket(PacketObject packet)
        {
            /* 入力データを通知 */
            OnInputData(packet.Data);
        }

        protected virtual GraphViewData OnLoadViewData()
        {
            return (null);
        }

        protected virtual void OnInputData(byte[] value) { }

        protected virtual void OnSampling() { }
    }
}
