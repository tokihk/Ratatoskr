using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ratatoskr.Scripts.Expression.Parser;

namespace Ratatoskr.Scripts.Expression.Terms
{
    internal sealed class Term_DateTime : Term
    {
        private static readonly string TIME_FORMAT = 
                                        "(?<year>[0-9]{4})" +
                                        "(/(?<month>[0][0-9]|1[0-2])){0,1}" +
                                        "(/(?<day>[0-2][0-9]|3[0-1])){0,1}" +
                                        "([ ](?<hour>[0-1][0-9]|2[0-3])){0,1}" +
                                        "(:(?<min>[0-5][0-9])){0,1}" +
                                        "(:(?<sec>[0-5][0-9])){0,1}" +
                                        "([\\.](?<msec>[0-9]{3})){0,1}";


        private DateTime value_ = DateTime.MinValue;


        public Term_DateTime()
        {
        }

        public Term_DateTime(string text)
        {
            var match = System.Text.RegularExpressions.Regex.Match(text, TIME_FORMAT);

            if (match.Success) {
                var time_now = DateTime.Now;

                value_ = new DateTime(
                                match.Groups["year"].Success  ? (int.Parse(match.Groups["year"].Value))  : (time_now.Year),
                                match.Groups["month"].Success ? (int.Parse(match.Groups["month"].Value)) : (time_now.Month),
                                match.Groups["day"].Success   ? (int.Parse(match.Groups["day"].Value))   : (time_now.Day),
                                match.Groups["hour"].Success  ? (int.Parse(match.Groups["hour"].Value))  : (0),
                                match.Groups["min"].Success   ? (int.Parse(match.Groups["min"].Value))   : (0),
                                match.Groups["sec"].Success   ? (int.Parse(match.Groups["sec"].Value))   : (0),
                                match.Groups["msec"].Success  ? (int.Parse(match.Groups["msec"].Value))  : (0));
            }
        }

        public Term_DateTime(DateTime time)
        {
            value_ = time;
        }

        public DateTime Value
        {
            get { return (value_); }
        }

        public override bool ToBool(ExpressionCallStack cs)
        {
            return (value_ != DateTime.MinValue);
        }

        protected override Term Exec_ARMOP_ADD(ExpressionCallStack cs, Term right)
        {
            /* === Term_TimeSpan === */
            {
                var right_r = right as Term_TimeSpan;

                if (right_r != null) {
                    return (new Term_DateTime(value_.Add(right_r.Value)));
                }
            }

            return (null);
        }

        protected override Term Exec_ARMOP_SUB(ExpressionCallStack cs, Term right)
        {
            /* === Term_DateTime === */
            {
                var right_r = right as Term_DateTime;

                if (right_r != null) {
                    return (new Term_TimeSpan(Value - right_r.Value));
                }
            }

            /* === Term_TimeSpan === */
            {
                var right_r = right as Term_TimeSpan;

                if (right_r != null) {
                    return (new Term_DateTime(Value.Subtract(right_r.Value)));
                }
            }

            return (null);
        }

        protected override Term Exec_RELOP_EQUAL(ExpressionCallStack cs, Term right)
        {
            /* === Term_DateTime === */
            {
                var right_r = right as Term_DateTime;

                if (right_r != null) {
                    return (new Term_Bool(value_ == right_r.Value));
                }
            }

            return (null);
        }

        protected override Term Exec_RELOP_GREATER(ExpressionCallStack cs, Term right)
        {
            /* === Term_DateTime === */
            {
                var right_r = right as Term_DateTime;

                if (right_r != null) {
                    return (new Term_Bool(value_ > right_r.Value));
                }
            }

            return (null);
        }

        protected override Term Exec_RELOP_GREATEREQUAL(ExpressionCallStack cs, Term right)
        {
            /* === Term_DateTime === */
            {
                var right_r = right as Term_DateTime;

                if (right_r != null) {
                    return (new Term_Bool(value_ >= right_r.Value));
                }
            }

            return (null);
        }

        protected override Term Exec_RELOP_LESS(ExpressionCallStack cs, Term right)
        {
            /* === Term_DateTime === */
            {
                var right_r = right as Term_DateTime;

                if (right_r != null) {
                    return (new Term_Bool(value_ < right_r.Value));
                }
            }

            return (null);
        }

        protected override Term Exec_RELOP_LESSEQUAL(ExpressionCallStack cs, Term right)
        {
            /* === Term_DateTime === */
            {
                var right_r = right as Term_DateTime;

                if (right_r != null) {
                    return (new Term_Bool(value_ <= right_r.Value));
                }
            }

            return (null);
        }
    }
}
