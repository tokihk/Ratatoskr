using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Ratatoskr.Scripts.PacketFilterExp.Parser;

namespace Ratatoskr.Scripts.PacketFilterExp.Terms
{
    internal sealed class Term_Regex : Term
    {
        private Regex value_ = new Regex("", RegexOptions.Compiled);


        public Term_Regex()
        {
        }

        public Term_Regex(string text)
        {
            try {
                if ((text != null) && (text.Length > 0)) {
                    value_ = new Regex(text, RegexOptions.Compiled);
                }
            } catch (Exception) {
                value_ = null;
            }
        }

        public override bool ErrorCheck(ExpressionCallStack cs)
        {
            return (value_ == null);
        }

        public string Value
        {
            get { return (value_.ToString()); }
        }

        public override bool ToBool(ExpressionCallStack cs)
        {
            return (value_ != null);
        }

        protected override Term Exec_RELOP_EQUAL(ExpressionCallStack cs, Term right)
        {
            try {
                /* === Term_Binary === */
                {
                    var right_r = right as Term_Binary;

                    if (right_r != null) {
                        var str = Encoding.ASCII.GetString(right_r.Value);

                        return (new Term_Bool(value_.IsMatch(str)));
                    }
                }

                /* === Term_Text === */
                {
                    var right_r = right as Term_Text;

                    if (right_r != null) {
                        return (new Term_Bool(value_.IsMatch(right_r.Value)));
                    }
                }

                return (null);

            } catch (Exception) {
                return (null);
            }
        }
    }
}
