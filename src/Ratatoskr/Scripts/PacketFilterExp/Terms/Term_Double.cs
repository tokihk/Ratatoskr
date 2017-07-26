using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ratatoskr.Scripts.PacketFilterExp.Parser;

namespace Ratatoskr.Scripts.PacketFilterExp.Terms
{
    internal sealed class Term_Double : Term
    {
        private double value_ = 0;


        public Term_Double()
        {
        }

        public Term_Double(double value)
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

        protected override Term Exec_ARMOP_ADD(ExpressionCallStack cs, Term right)
        {
            /* === Term_Double === */
            {
                var right_r = right as Term_Double;

                if (right_r != null) {
                    return (new Term_Double(value_ + right_r.value_));
                }
            }

            return (null);
        }

        protected override Term Exec_ARMOP_SUB(ExpressionCallStack cs, Term right)
        {
            /* === Term_Double === */
            {
                var right_r = right as Term_Double;

                if (right_r != null) {
                    return (new Term_Double(value_ - right_r.value_));
                }
            }

            return (null);
        }

        protected override Term Exec_ARMOP_DIV(ExpressionCallStack cs, Term right)
        {
            /* === Term_Double === */
            {
                var right_r = right as Term_Double;

                if (right_r != null) {
                    return (new Term_Double(value_ / right_r.value_));
                }
            }

            return (null);
        }

        protected override Term Exec_ARMOP_MUL(ExpressionCallStack cs, Term right)
        {
            /* === Term_Double === */
            {
                var right_r = right as Term_Double;

                if (right_r != null) {
                    return (new Term_Double(value_ * right_r.value_));
                }
            }

            return (null);
        }

        protected override Term Exec_ARMOP_REM(ExpressionCallStack cs, Term right)
        {
            /* === Term_Double === */
            {
                var right_r = right as Term_Double;

                if (right_r != null) {
                    return (new Term_Double(value_ % right_r.value_));
                }
            }

            return (null);
        }

        protected override Term Exec_RELOP_EQUAL(ExpressionCallStack cs, Term right)
        {
            /* === Term_Double === */
            {
                var right_r = right as Term_Double;

                if (right_r != null) {
                    return (new Term_Bool(value_ == right_r.value_));
                }
            }

            return (null);
        }

        protected override Term Exec_RELOP_GREATER(ExpressionCallStack cs, Term right)
        {
            /* === Term_Double === */
            {
                var right_r = right as Term_Double;

                if (right_r != null) {
                    return (new Term_Bool(value_ > right_r.value_));
                }
            }

            return (null);
        }

        protected override Term Exec_RELOP_GREATEREQUAL(ExpressionCallStack cs, Term right)
        {
            /* === Term_Double === */
            {
                var right_r = right as Term_Double;

                if (right_r != null) {
                    return (new Term_Bool(value_ >= right_r.value_));
                }
            }

            return (null);
        }

        protected override Term Exec_RELOP_LESS(ExpressionCallStack cs, Term right)
        {
            /* === Term_Double === */
            {
                var right_r = right as Term_Double;

                if (right_r != null) {
                    return (new Term_Bool(value_ < right_r.value_));
                }
            }

            return (null);
        }

        protected override Term Exec_RELOP_LESSEQUAL(ExpressionCallStack cs, Term right)
        {
            /* === Term_Double === */
            {
                var right_r = right as Term_Double;

                if (right_r != null) {
                    return (new Term_Bool(value_ <= right_r.value_));
                }
            }

            return (null);
        }
    }
}
