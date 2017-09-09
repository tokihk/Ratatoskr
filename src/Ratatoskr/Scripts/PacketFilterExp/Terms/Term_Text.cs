using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ratatoskr.Scripts.PacketFilterExp.Parser;

namespace Ratatoskr.Scripts.PacketFilterExp.Terms
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

        public override bool ToBool(ExpressionCallStack cs)
        {
            return ((value_ != null) && (value_.Length > 0));
        }

        protected override Term Exec_RELOP_EQUAL(ExpressionCallStack cs, Term term_sub)
        {
            /* === Term_Text === */
            if (term_sub.GetType() == typeof(Term_Text)) {
                return (new Term_Bool(value_ == (term_sub as Term_Text).Value));
            }

            /* === Term_Regex === */
            if (term_sub.GetType() == typeof(Term_Regex)) {
                return ((term_sub as Term_Regex).Exec(cs, Tokens.RELOP_EQUAL, this));
            }

            return (null);
        }

        protected override Term Exec_ARMOP_ADD(ExpressionCallStack cs, Term term_sub)
        {
            /* === Term_Text === */
            if (term_sub.GetType() == typeof(Term_Text)) {
                return (new Term_Text(value_ + (term_sub as Term_Text).Value));
            }

            return (null);
        }
    }
}
