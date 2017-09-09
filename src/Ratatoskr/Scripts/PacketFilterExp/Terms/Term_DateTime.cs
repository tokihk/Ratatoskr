using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ratatoskr.Scripts.PacketFilterExp.Parser;

namespace Ratatoskr.Scripts.PacketFilterExp.Terms
{
    internal sealed class Term_DateTime : Term
    {
        private DateTime value_ = DateTime.MinValue;


        public Term_DateTime()
        {
        }

        public Term_DateTime(DateTime time)
        {
            value_ = time;
        }

        public Term_DateTime(string text)
        {
            value_ = DateTime.Parse(text, null, System.Globalization.DateTimeStyles.RoundtripKind);
        }

        public DateTime Value
        {
            get { return (value_); }
        }

        public override bool ToBool(ExpressionCallStack cs)
        {
            return (value_ != DateTime.MinValue);
        }

        protected override Term Exec_ARMOP_ADD(ExpressionCallStack cs, Term term_sub)
        {
            /* === Term_DateTimeOffset === */
            if (term_sub.GetType() == typeof(Term_DateTimeOffset)) {
                return (new Term_DateTime(value_ + (term_sub as Term_DateTimeOffset).Value));
            }

            return (null);
        }

        protected override Term Exec_ARMOP_SUB(ExpressionCallStack cs, Term term_sub)
        {
            /* === Term_DateTimeOffset === */
            if (term_sub.GetType() == typeof(Term_DateTimeOffset)) {
                return (new Term_DateTime(value_ - (term_sub as Term_DateTimeOffset).Value));
            }

            /* === Term_DateTime === */
            if (term_sub.GetType() == typeof(Term_DateTime)) {
                return (new Term_DateTimeOffset(value_ - (term_sub as Term_DateTime).Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_EQUAL(ExpressionCallStack cs, Term term_sub)
        {
            /* === Term_DateTime === */
            if (term_sub.GetType() == typeof(Term_DateTime)) {
                return (new Term_Bool(value_ == (term_sub as Term_DateTime).Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_GREATER(ExpressionCallStack cs, Term term_sub)
        {
            /* === Term_DateTime === */
            if (term_sub.GetType() == typeof(Term_DateTime)) {
                return (new Term_Bool(value_ > (term_sub as Term_DateTime).Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_GREATEREQUAL(ExpressionCallStack cs, Term term_sub)
        {
            /* === Term_DateTime === */
            if (term_sub.GetType() == typeof(Term_DateTime)) {
                return (new Term_Bool(value_ >= (term_sub as Term_DateTime).Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_LESS(ExpressionCallStack cs, Term term_sub)
        {
            /* === Term_DateTime === */
            if (term_sub.GetType() == typeof(Term_DateTime)) {
                return (new Term_Bool(value_ < (term_sub as Term_DateTime).Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_LESSEQUAL(ExpressionCallStack cs, Term term_sub)
        {
            /* === Term_DateTime === */
            if (term_sub.GetType() == typeof(Term_DateTime)) {
                return (new Term_Bool(value_ <= (term_sub as Term_DateTime).Value));
            }

            return (null);
        }
    }
}
