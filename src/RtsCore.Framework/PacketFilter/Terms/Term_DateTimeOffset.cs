using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RtsCore.Framework.PacketFilter;

namespace RtsCore.Framework.PacketFilter.Terms
{
    internal sealed class Term_DateTimeOffset : Term
    {
        public enum ParseMode
        {
            Details,
            Simple,
        }


        private static readonly string TIME_FORMAT_DETAILS = 
                                        "(?<hour>[0-1][0-9]|2[0-3]){0,1}" +
                                        "(:(?<min>[0-5][0-9])){0,1}" +
                                        "(:(?<sec>[0-5][0-9])){0,1}" +
                                        "([\\.](?<msec>[0-9]{3}))";

        private static readonly string TIME_FORMAT_SIMPLE = "(?<value>[0]|([1-9][0-9]{0,9}))(?<unit>[a-zA-Z]+)";


        private TimeSpan value_ = TimeSpan.MinValue;


        public Term_DateTimeOffset()
        {
        }

        public Term_DateTimeOffset(ParseMode mode, string text)
        {
            switch (mode) {
                case ParseMode.Details:
                    Setup_DetailsMode(text);
                    break;

                default:
                    Setup_SimpleMode(text);
                    break;
            }
        }

        public void Setup_DetailsMode(string text)
        {
            var match = System.Text.RegularExpressions.Regex.Match(text, TIME_FORMAT_DETAILS);

            if (!match.Success)return;

            value_ = new TimeSpan(
                            0,
                            match.Groups["hour"].Success  ? (int.Parse(match.Groups["hour"].Value))  : (0),
                            match.Groups["min"].Success   ? (int.Parse(match.Groups["min"].Value))   : (0),
                            match.Groups["sec"].Success   ? (int.Parse(match.Groups["sec"].Value))   : (0),
                            match.Groups["msec"].Success  ? (int.Parse(match.Groups["msec"].Value))  : (0));
        }

        public void Setup_SimpleMode(string text)
        {
            var match = System.Text.RegularExpressions.Regex.Match(text, TIME_FORMAT_SIMPLE);

            if (!match.Success)return;

            var value = ulong.Parse(match.Groups["value"].Value);
            var day = 0;
            var hour = 0;
            var min = 0;
            var sec = 0;
            var msec = 0;

            switch (match.Groups["unit"].Value) {
                case "h":
                case "hour":
                    day  = (int)(value / 24);    value -= ((ulong)day * 24);
                    hour = (int)(value);
                    break;
                case "m":
                case "min":
                    day  = (int)(value / 1440);  value -= ((ulong)day * 1440);
                    hour = (int)(value / 60);    value -= ((ulong)day * 60);
                    min  = (int)(value);
                    break;
                case "s":
                case "sec":
                    day  = (int)(value / 86400); value -= ((ulong)day * 86400);
                    hour = (int)(value / 1440);  value -= ((ulong)day * 1440);
                    min  = (int)(value / 60);    value -= ((ulong)day * 60);
                    sec  = (int)(value);
                    break;
                case "ms":
                case "msec":
                    day  = (int)(value / 86400000); value -= ((ulong)day * 86400000);
                    hour = (int)(value / 1440000);  value -= ((ulong)day * 1440000);
                    min  = (int)(value / 60000);    value -= ((ulong)day * 60000);
                    sec  = (int)(value / 1000);     value -= ((ulong)day * 1000);
                    msec = (int)(value);
                    break;
            }

            value_ = new TimeSpan(day, hour, min, sec, msec);

            System.Diagnostics.Debug.WriteLine(value_.TotalMilliseconds);
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
            /* === Term_DateTimeOffset === */
            if (right is Term_DateTimeOffset right_dto) {
                return (new Term_DateTimeOffset(value_ + right_dto.Value));
            }

            /* === Term_DateTime === */
            if (right is Term_DateTime right_dt) {
                return (new Term_DateTime(right_dt.Value.Add(Value)));
            }

            return (null);
        }

        protected override Term Exec_ARMOP_SUB(PacketFilterCallStack cs, Term right)
        {
            /* === Term_DateTimeOffset === */
            if (right is Term_DateTimeOffset right_dto) {
                return (new Term_DateTimeOffset(value_ - right_dto.Value));
            }

            /* === Term_DateTime === */
            if (right is Term_DateTime right_dt) {
                return (new Term_DateTime(right_dt.Value.Subtract(Value)));
            }

            return (null);
        }

        protected override Term Exec_RELOP_EQUAL(PacketFilterCallStack cs, Term right)
        {
            /* === Term_DateTimeOffset === */
            if (right is Term_DateTimeOffset right_dto) {
                return (new Term_Bool(value_ == right_dto.Value));
            }

            /* === Term_Number === */
            if (right is Term_Number right_num) {
                return (new Term_Bool(value_ == right_num.ToTimeSpan()));
            }

            return (null);
        }

        protected override Term Exec_RELOP_GREATER(PacketFilterCallStack cs, Term right)
        {
            /* === Term_DateTimeOffset === */
            if (right is Term_DateTimeOffset right_dto) {
                return (new Term_Bool(value_ > right_dto.Value));
            }

            /* === Term_Number === */
            if (right is Term_Number right_num) {
                return (new Term_Bool(value_ > right_num.ToTimeSpan()));
            }

            return (null);
        }

        protected override Term Exec_RELOP_GREATEREQUAL(PacketFilterCallStack cs, Term right)
        {
            /* === Term_DateTimeOffset === */
            if (right is Term_DateTimeOffset right_dto) {
                return (new Term_Bool(value_ >= right_dto.Value));
            }

            /* === Term_Number === */
            if (right is Term_Number right_num) {
                return (new Term_Bool(value_ >= right_num.ToTimeSpan()));
            }

            return (null);
        }

        protected override Term Exec_RELOP_LESS(PacketFilterCallStack cs, Term right)
        {
            /* === Term_DateTimeOffset === */
            if (right is Term_DateTimeOffset right_dto) {
                return (new Term_Bool(value_ < right_dto.Value));
            }

            /* === Term_Number === */
            if (right is Term_Number right_num) {
                return (new Term_Bool(value_ < right_num.ToTimeSpan()));
            }

            return (null);
        }

        protected override Term Exec_RELOP_LESSEQUAL(PacketFilterCallStack cs, Term right)
        {
            /* === Term_DateTimeOffset === */
            if (right is Term_DateTimeOffset right_dto) {
                return (new Term_Bool(value_ <= right_dto.Value));
            }

            /* === Term_Number === */
            if (right is Term_Number right_num) {
                return (new Term_Bool(value_ <= right_num.ToTimeSpan()));
            }

            return (null);
        }
    }
}
