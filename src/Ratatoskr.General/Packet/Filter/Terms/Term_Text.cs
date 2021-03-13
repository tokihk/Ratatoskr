using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ratatoskr.General.Packet.Filter;

namespace Ratatoskr.General.Packet.Filter.Terms
{
    internal sealed class Term_Text : Term
    {
        private string value_ = "";


        public Term_Text()
        {
        }

        public Term_Text(string text)
        {
            value_ = text;
        }

        public string Value
        {
            get { return (value_); }
        }

        public override bool ToBool(PacketFilterCallStack cs)
        {
            return ((value_ != null) && (value_.Length > 0));
        }

        protected override Term Exec_RELOP_EQUAL(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_Text === */
            if (term_sub is Term_Text term_sub_text) {
                return (new Term_Bool(value_ == term_sub_text.Value));
            }

            /* === Term_Regex === */
            if (term_sub is Term_Regex term_sub_reg) {
                return (term_sub_reg.Exec(cs, Tokens.RELOP_EQUAL, this));
            }

            return (null);
        }

        protected override Term Exec_ARMOP_ADD(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_Text === */
            if (term_sub is Term_Text term_sub_text) {
                return (new Term_Text(value_ + term_sub_text.Value));
            }

            return (null);
        }
    }
}
