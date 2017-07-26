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

        public override bool ErrorCheck(ExpressionCallStack cs)
        {
            return (value_ == null);
        }

        public string Value
        {
            get { return (value_); }
        }

        public override bool ToBool(ExpressionCallStack cs)
        {
            return ((value_ != null) && (value_.Length > 0));
        }

        protected override Term Exec_RELOP_EQUAL(ExpressionCallStack cs, Term right)
        {
            /* === Term_Text === */
            {
                var right_r = right as Term_Text;

                if (right_r != null) {
                    return (new Term_Bool(value_ == right_r.Value));
                }
            }

            /* === Term_Regex === */
            {
                var right_r = right as Term_Regex;

                if (right_r != null) {
                    return (right_r.Exec(cs, Tokens.RELOP_EQUAL, this));
                }
            }

            return (null);
        }

        protected override Term Exec_ARMOP_ADD(ExpressionCallStack cs, Term right)
        {
            /* === Term_Text === */
            {
                var right_r = right as Term_Text;

                if (right_r != null) {
                    value_ += right_r.Value;
                    return (this);
                }
            }

            return (null);
        }
    }
}
