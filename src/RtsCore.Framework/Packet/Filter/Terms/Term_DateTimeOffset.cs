using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RtsCore.Framework.Packet.Filter;

namespace RtsCore.Framework.Packet.Filter.Terms
{
    internal sealed class Term_DateTimeOffset : Term
    {
        private static readonly string TIME_FORMAT = 
                                        "(?<hour>[0-1][0-9]|2[0-3]){0,1}" +
                                        "(:(?<min>[0-5][0-9])){0,1}" +
                                        "(:(?<sec>[0-5][0-9])){0,1}" +
                                        "([\\.](?<msec>[0-9]{3}))";


        private TimeSpan value_ = TimeSpan.MinValue;


        public Term_DateTimeOffset()
        {
        }

        public Term_DateTimeOffset(string text)
        {
            var match = System.Text.RegularExpressions.Regex.Match(text, TIME_FORMAT);

            if (match.Success) {
                value_ = new TimeSpan(
                                match.Groups["hour"].Success  ? (int.Parse(match.Groups["hour"].Value))  : (0),
                                match.Groups["min"].Success   ? (int.Parse(match.Groups["min"].Value))   : (0),
                                match.Groups["sec"].Success   ? (int.Parse(match.Groups["sec"].Value))   : (0),
                                match.Groups["msec"].Success  ? (int.Parse(match.Groups["msec"].Value))  : (0));
            }
        }

        public Term_DateTimeOffset(TimeSpan time)
        {
            value_ = time;
        }

        public TimeSpan Value
        {
            get { return (value_); }
        }

        protected override Term Exec_ARMOP_ADD(PacketFilterCallStack cs, Term right)
        {
            /* === Term_TimeSpan === */
            {
                var right_r = right as Term_DateTimeOffset;

                if (right_r != null) {
                    return (new Term_DateTimeOffset(value_ + right_r.Value));
                }
            }

            /* === Term_DateTime === */
            {
                var right_r = right as Term_DateTime;

                if (right_r != null) {
                    return (new Term_DateTime(right_r.Value.Add(Value)));
                }
            }

            return (null);
        }

        protected override Term Exec_ARMOP_SUB(PacketFilterCallStack cs, Term right)
        {
            /* === Term_TimeSpan === */
            {
                var right_r = right as Term_DateTimeOffset;

                if (right_r != null) {
                    return (new Term_DateTimeOffset(value_ - right_r.Value));
                }
            }

            /* === Term_DateTime === */
            {
                var right_r = right as Term_DateTime;

                if (right_r != null) {
                    return (new Term_DateTime(right_r.Value.Subtract(Value)));
                }
            }

            return (null);
        }

        protected override Term Exec_RELOP_EQUAL(PacketFilterCallStack cs, Term right)
        {
            /* === Term_TimeSpan === */
            {
                var right_r = right as Term_DateTimeOffset;

                if (right_r != null) {
                    return (new Term_Bool(value_ == right_r.Value));
                }
            }

            /* === Term_Double === */
            {
                var right_r = right as Term_Number;

                if (right_r != null) {
                    return (new Term_Bool(value_ == right_r.ToTimeSpan()));
                }
            }

            return (null);
        }

        protected override Term Exec_RELOP_GREATER(PacketFilterCallStack cs, Term right)
        {
            /* === Term_TimeSpan === */
            {
                var right_r = right as Term_DateTimeOffset;

                if (right_r != null) {
                    return (new Term_Bool(value_ > right_r.Value));
                }
            }

            /* === Term_Double === */
            {
                var right_r = right as Term_Number;

                if (right_r != null) {
                    return (new Term_Bool(value_ > right_r.ToTimeSpan()));
                }
            }

            return (null);
        }

        protected override Term Exec_RELOP_GREATEREQUAL(PacketFilterCallStack cs, Term right)
        {
            /* === Term_TimeSpan === */
            {
                var right_r = right as Term_DateTimeOffset;

                if (right_r != null) {
                    return (new Term_Bool(value_ >= right_r.Value));
                }
            }

            /* === Term_Double === */
            {
                var right_r = right as Term_Number;

                if (right_r != null) {
                    return (new Term_Bool(value_ >= right_r.ToTimeSpan()));
                }
            }

            return (null);
        }

        protected override Term Exec_RELOP_LESS(PacketFilterCallStack cs, Term right)
        {
            /* === Term_TimeSpan === */
            {
                var right_r = right as Term_DateTimeOffset;

                if (right_r != null) {
                    return (new Term_Bool(value_ < right_r.Value));
                }
            }

            /* === Term_Double === */
            {
                var right_r = right as Term_Number;

                if (right_r != null) {
                    return (new Term_Bool(value_ < right_r.ToTimeSpan()));
                }
            }

            return (null);
        }

        protected override Term Exec_RELOP_LESSEQUAL(PacketFilterCallStack cs, Term right)
        {
            /* === Term_TimeSpan === */
            {
                var right_r = right as Term_DateTimeOffset;

                if (right_r != null) {
                    return (new Term_Bool(value_ <= right_r.Value));
                }
            }

            /* === Term_Double === */
            {
                var right_r = right as Term_Number;

                if (right_r != null) {
                    return (new Term_Bool(value_ <= right_r.ToTimeSpan()));
                }
            }

            return (null);
        }
    }
}
