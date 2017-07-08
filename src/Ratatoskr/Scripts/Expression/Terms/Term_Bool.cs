using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ratatoskr.Scripts.Expression.Parser;

namespace Ratatoskr.Scripts.Expression.Terms
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

        public override bool ToBool(ExpressionCallStack cs)
        {
            return (value_);
        }

        protected override Term Exec_RELOP_EQUAL(ExpressionCallStack cs, Term right)
        {
            /* === Term_Bool === */
            {
                var right_r = right as Term_Bool;

                if (right_r != null) {
                    return (new Term_Bool(value_ == right_r.value_));
                }
            }

            return (null);
        }
    }
}
