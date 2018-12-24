using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using RtsCore.Framework.PacketFilter;

namespace RtsCore.Framework.PacketFilter.Terms
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
            if (term_sub is Term_Text term_sub_c) {
                return (new Term_Bool(value_.IsMatch(term_sub_c.Value)));
            }

            return (null);
        }
    }
}
