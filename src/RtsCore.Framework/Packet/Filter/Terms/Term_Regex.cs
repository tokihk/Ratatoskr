using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using RtsCore.Framework.Packet.Filter;

namespace RtsCore.Framework.Packet.Filter.Terms
{
    internal sealed class Term_Regex : Term
    {
        private Regex value_ = null;


        public Term_Regex()
        {
        }

        public Term_Regex(string text)
        {
            value_ = new Regex(text, RegexOptions.Compiled);
        }

        public string Value
        {
            get { return (value_.ToString()); }
        }

        public override bool ToBool(PacketFilterCallStack cs)
        {
            return (value_ != null);
        }

        protected override Term Exec_RELOP_EQUAL(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_Text === */
            if (term_sub.GetType() == typeof(Term_Text)) {
                return (new Term_Bool(value_.IsMatch((term_sub as Term_Text).Value)));
            }

            return (null);
        }
    }
}
