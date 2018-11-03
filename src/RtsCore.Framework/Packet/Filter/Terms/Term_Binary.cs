using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RtsCore.Framework.Packet.Filter;
using RtsCore.Utility;

namespace RtsCore.Framework.Packet.Filter.Terms
{
    internal sealed class Term_Binary : Term
    {
        private byte[] value_ = new byte[] { };


        public Term_Binary()
        {
        }

        public Term_Binary(string text)
        {
            value_ = HexTextEncoder.ToByteArray(text);
        }

        public Term_Binary(byte[] data)
        {
            value_ = data;
        }

        public byte[] Value
        {
            get { return (value_); }
        }

        public override bool ToBool(PacketFilterCallStack cs)
        {
            return ((value_ != null) && (value_.Length > 0));
        }

        protected override Term Exec_RELOP_EQUAL(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_Binary === */
            if (term_sub.GetType() == typeof(Term_Binary)) {
                return (new Term_Bool(value_.SequenceEqual((term_sub as Term_Binary).Value)));
            }

            return (null);
        }
    }
}
