using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Ratatoskr.General.Packet.Filter;

namespace Ratatoskr.General.Packet.Filter.Terms
{
    internal sealed class Term_DateTime : Term
    {
        private readonly Regex REGEX_LOCALTIME_DATEBASE = new Regex("(?<year>[0-9]{4})(-(?<month>[0][0-9]|1[0-2])(-(?<day>[0-2][0-9]|3[0-1])([ ](?<hour>[0-1][0-9]|2[0-3])(:(?<min>[0-5][0-9])(:(?<sec>[0-5][0-9])(.(?<msec>[0-9]{3}))?)?)?)?)?)?", RegexOptions.Compiled);
        private readonly Regex REGEX_LOCALTIME_TIMEBASE = new Regex("((((?<year>[0-9]{4})-)?(?<month>[0][0-9]|1[0-2])-)?(?<day>[0-2][0-9]|3[0-1])[ ])?(?<hour>[0-1][0-9]|2[0-3]):(?<min>[0-5][0-9]):(?<sec>[0-5][0-9])(.(?<msec>[0-9]{3}))?", RegexOptions.Compiled);


        public enum FormatType
        {
            ISO8601,
            LocalTime_DateBase,
            LocalTime_TimeBase,
        }


        private DateTime value_ = DateTime.MinValue;


        public Term_DateTime()
        {
        }

        public Term_DateTime(DateTime time)
        {
            value_ = time;
        }

        public Term_DateTime(FormatType type, string text)
        {
            /* 先頭と終端の$を削除 */
            text = text.Substring(1, text.Length - 2);

            switch (type) {
                case FormatType.ISO8601:            value_ = GetDateTime_ISO8601(text);            break;
                case FormatType.LocalTime_DateBase: value_ = GetDateTime_LocalTime_DateBase(text); break;
                case FormatType.LocalTime_TimeBase: value_ = GetDateTime_LocalTime_TimeBase(text); break;
            }
        }

        private DateTime GetDateTime_ISO8601(string text)
        {
            return (DateTime.Parse(text, null, System.Globalization.DateTimeStyles.RoundtripKind).ToUniversalTime());
        }

        private DateTime GetDateTime_LocalTime_DateBase(string text)
        {
            var year  = 1;
            var month = 1;
            var day   = 1;
            var hour  = 0;
            var min   = 0;
            var sec   = 0;
            var msec  = 0;

            foreach (Group group in REGEX_LOCALTIME_DATEBASE.Match(text).Groups) {
                if (group.Captures.Count == 0)continue;

                switch (group.Name) {
                    case "year":  year  = int.Parse(group.Captures[0].Value); break;
                    case "month": month = int.Parse(group.Captures[0].Value); break;
                    case "day":   day   = int.Parse(group.Captures[0].Value); break;
                    case "hour":  hour  = int.Parse(group.Captures[0].Value); break;
                    case "min":   min   = int.Parse(group.Captures[0].Value); break;
                    case "sec":   sec   = int.Parse(group.Captures[0].Value); break;
                    case "msec":  msec  = int.Parse(group.Captures[0].Value); break;
                }
            }

            return (new DateTime(year, month, day, hour, min, sec, msec, DateTimeKind.Local));
        }

        private DateTime GetDateTime_LocalTime_TimeBase(string text)
        {
            var dt_now = DateTime.Now;

            var year  = dt_now.Year;
            var month = dt_now.Month;
            var day   = dt_now.Day;
            var hour  = 0;
            var min   = 0;
            var sec   = 0;
            var msec  = 0;

            foreach (Group group in REGEX_LOCALTIME_TIMEBASE.Match(text).Groups) {
                if (group.Captures.Count == 0)continue;

                switch (group.Name) {
                    case "year":  year  = int.Parse(group.Captures[0].Value); break;
                    case "month": month = int.Parse(group.Captures[0].Value); break;
                    case "day":   day   = int.Parse(group.Captures[0].Value); break;
                    case "hour":  hour  = int.Parse(group.Captures[0].Value); break;
                    case "min":   min   = int.Parse(group.Captures[0].Value); break;
                    case "sec":   sec   = int.Parse(group.Captures[0].Value); break;
                    case "msec":  msec  = int.Parse(group.Captures[0].Value); break;
                }
            }

            return (new DateTime(year, month, day, hour, min, sec, msec, DateTimeKind.Local));
        }

        public DateTime Value
        {
            get { return (value_); }
        }

        public override bool ToBool(PacketFilterCallStack cs)
        {
            return (value_ != DateTime.MinValue);
        }

        protected override Term Exec_ARMOP_ADD(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_DateTimeOffset === */
            if (term_sub is Term_DateTimeOffset term_sub_dto) {
                return (new Term_DateTime(value_ + term_sub_dto.Value));
            }

            return (null);
        }

        protected override Term Exec_ARMOP_SUB(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_DateTimeOffset === */
            if (term_sub is Term_DateTimeOffset term_sub_dto) {
                return (new Term_DateTime(value_ - term_sub_dto.Value));
            }

            /* === Term_DateTime === */
            if (term_sub is Term_DateTime term_sub_dt) {
                return (new Term_DateTimeOffset(value_ - term_sub_dt.Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_EQUAL(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_DateTime === */
            if (term_sub is Term_DateTime term_sub_dt) {
                return (new Term_Bool(value_ == term_sub_dt.Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_GREATER(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_DateTime === */
            if (term_sub is Term_DateTime term_sub_dt) {
                return (new Term_Bool(value_ > term_sub_dt.Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_GREATEREQUAL(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_DateTime === */
            if (term_sub is Term_DateTime term_sub_dt) {
                return (new Term_Bool(value_ >= term_sub_dt.Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_LESS(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_DateTime === */
            if (term_sub is Term_DateTime term_sub_dt) {
                return (new Term_Bool(value_ < term_sub_dt.Value));
            }

            return (null);
        }

        protected override Term Exec_RELOP_LESSEQUAL(PacketFilterCallStack cs, Term term_sub)
        {
            /* === Term_DateTime === */
            if (term_sub is Term_DateTime term_sub_dt) {
                return (new Term_Bool(value_ <= term_sub_dt.Value));
            }

            return (null);
        }
    }
}
