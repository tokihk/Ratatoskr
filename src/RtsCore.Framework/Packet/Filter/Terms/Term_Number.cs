using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RtsCore.Framework.Packet.Filter;

namespace RtsCore.Framework.Packet.Filter.Terms
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

        public override bool ToBool(PacketFilterCallStack cs)
        {
            return (value_ != 0);
        }

        public TimeSpan ToTimeSpan()
        {
            return (TimeSpan.FromMilliseconds(value_ * 1000));
        }

        protected override Term Exec_ARMOP_ADD(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Number(value_ + (term_sub as Term_Number).Value));
            }

            return (null);
        }

        protected override Term Exec_ARMOP_SUB(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Number(value_ - (term_sub as Term_Number).Value));
            }

            return (null);
        }

        protected override Term Exec_ARMOP_DIV(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Number(value_ / (term_sub as Term_Number).Value));
            }

            return (null);
        }

        protected override Term Exec_ARMOP_MUL(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Number(value_ * (term_sub as Term_Number).Value));
            }

            return (null);
        }

        protected override Term Exec_ARMOP_REM(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Number(value_ % (term_sub as Term_Number).Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_EQUAL(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Bool(value_ == (term_sub as Term_Number).Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_GREATER(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Bool(value_ > (term_sub as Term_Number).Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_GREATEREQUAL(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Bool(value_ >= (term_sub as Term_Number).Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_LESS(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Bool(value_ < (term_sub as Term_Number).Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_LESSEQUAL(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_Number === */
            if (term_sub.GetType() == typeof(Term_Number)) {
                return (new Term_Bool(value_ <= (term_sub as Term_Number).Value));
            }

            return (null);
        }
    }
}
