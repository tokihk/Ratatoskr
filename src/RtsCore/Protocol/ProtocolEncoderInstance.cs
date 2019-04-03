using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public abstract class ProtocolEncoderInstance
    {
        private ProtocolEncoderClass prec_;


        public ProtocolEncoderInstance(ProtocolEncoderClass prec)
        {
            prec_ = prec;
        }

        public ProtocolEncoderClass Class
        {
            get { return (prec_); }
        }
    }
}
