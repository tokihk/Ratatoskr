using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Generic
{
    internal sealed class Wildcard
    {
        public static bool Match(string pattern, string text)
        {
            return ((new Wildcard(pattern)).Input(text));
        }


        private string pattern_ = "";
        private int    pattern_chkpos_ = 0;

        private bool   any_match_mode_ = false;

        private StringBuilder match_text_ = new StringBuilder();


        public Wildcard(string pattern)
        {
            pattern_ = pattern;
        }

        public bool IsMatch
        {
            get { return (pattern_chkpos_ >= pattern_.Length); }
        }

        public string MatchText
        {
            get { return (match_text_.ToString()); }
        }

        public bool Input(char code)
        {
            if (IsMatch)return (IsMatch);

            var pattern_code = pattern_[pattern_chkpos_];

            switch (pattern_code) {
                case '?':
                {
                    /* あらゆる文字を一致と判定 */
                    pattern_chkpos_++;
                    match_text_.Append(code);
                }
                    break;

                case '*':
                {
                    /* あらゆる文字を一致と判定 */
                    pattern_chkpos_++;
                    match_text_.Append(code);

                    /* 合致パターンを検出するまで無条件に一致させる */
                    any_match_mode_ = true;
                }
                    break;

                default:
                {
                    if ((any_match_mode_) || (pattern_code == code)) {
                        match_text_.Append(code);
                    }

                    if ((any_match_mode_) && (pattern_code == code)) {
                        any_match_mode_ = false;
                    }

                    if ((!any_match_mode_) && (pattern_code == code)) {
                        pattern_chkpos_++;
                    }
                }
                    break;
            }

            return (IsMatch);
        }

        public bool Input(string text)
        {
            foreach (var code in text) {
                if (IsMatch)break;

                Input(code);
            }

            return (IsMatch);
        }
    }
}
