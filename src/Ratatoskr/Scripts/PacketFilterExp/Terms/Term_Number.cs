using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ratatoskr.Scripts.PacketFilterExp.Parser;

namespace Ratatoskr.Scripts.PacketFilterExp.Terms
{
    internal sealed class Term_Number : Term
    {
        private double value_ = 0;


        public Term_Number()
        {
        }

        public Term_Number(double value)
        {
            value_ = value;
        }

        public double Value
        {
            get { return (value_); }
        }

        public override bool ToBool(ExpressionCallStack cs)
        {
            return (value_ != 0);
        }

        public TimeSpan ToTimeSpan()
        {
            return (TimeSpan.FromMilliseconds(value_ * 1000));
        }

        protected override Term Exec_ARMOP_ADD(ExpressionCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Number(value_ + (term_sub as Term_Number).Value));
            }

            return (null);
        }

        protected override Term Exec_ARMOP_SUB(ExpressionCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Number(value_ - (term_sub as Term_Number).Value));
            }

            return (null);
        }

        protected override Term Exec_ARMOP_DIV(ExpressionCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Number(value_ / (term_sub as Term_Number).Value));
            }

            return (null);
        }

        protected override Term Exec_ARMOP_MUL(ExpressionCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Number(value_ * (term_sub as Term_Number).Value));
            }

            return (null);
        }

        protected override Term Exec_ARMOP_REM(ExpressionCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Number(value_ % (term_sub as Term_Number).Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_EQUAL(ExpressionCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Bool(value_ == (term_sub as Term_Number).Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_GREATER(ExpressionCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Bool(value_ > (term_sub as Term_Number).Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_GREATEREQUAL(ExpressionCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Bool(value_ >= (term_sub as Term_Number).Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_LESS(ExpressionCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Bool(value_ < (term_sub as Term_Number).Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_LESSEQUAL(ExpressionCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Bool(value_ <= (term_sub as Term_Number).Value));
            }

            return (null);
        }
    }
}
