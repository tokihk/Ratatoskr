using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ratatoskr.Scripts.Expression.Parser;
using Ratatoskr.Generic;

namespace Ratatoskr.Scripts.Expression.Terms
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

        public override bool ErrorCheck(ExpressionCallStack cs)
        {
            return (value_ == null);
        }

        public byte[] Value
        {
            get { return (value_); }
        }

        public override bool ToBool(ExpressionCallStack cs)
        {
            return ((value_ != null) && (value_.Length > 0));
        }

        protected override Term Exec_RELOP_EQUAL(ExpressionCallStack cs, Term right)
        {
            /* === Term_Binary === */
            {
                var right_r = right as Term_Binary;

                if (right_r != null) {
                    return (new Term_Bool(value_.SequenceEqual(right_r.value_)));
                }
            }

            return (null);
        }
    }
}
