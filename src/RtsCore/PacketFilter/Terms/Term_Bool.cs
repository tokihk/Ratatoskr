using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RtsCore.Framework.PacketFilter;

namespace RtsCore.Framework.PacketFilter.Terms
{
    internal sealed class Term_Bool : Term
    {
        private bool value_ = false;


        public Term_Bool()
        {
        }

        public Term_Bool(bool value)
        {
            value_ = value;
        }

        public bool Value
        {
            get { return (value_); }
        }

        public override bool ToBool(PacketFilterCallStack cs)
        {
            return (value_);
        }

        protected override Term Exec_RELOP_EQUAL(PacketFilterCallStack cs, Term term_sub)
        {
            return (new Term_Bool(value_ == term_sub.ToBool(cs)));
        }
    }
}
