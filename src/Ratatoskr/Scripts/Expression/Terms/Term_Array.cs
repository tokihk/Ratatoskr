using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ratatoskr.Scripts.Expression.Parser;

namespace Ratatoskr.Scripts.Expression.Terms
{
    internal sealed class Term_Array : Term
    {
        private List<KeyValuePair<string, Term>> values_ = new List<KeyValuePair<string, Term>>();


        public Term_Array()
        {
        }

        public Term_Array(Term term1, Term term2)
        {
            values_.Add(new KeyValuePair<string, Term>(null, term1));
            values_.Add(new KeyValuePair<string, Term>(null, term2));
        }

        public override bool ErrorCheck(ExpressionCallStack cs)
        {
            return (values_ == null);
        }

        public Term[] Value
        {
            get
            {
                return (from value in values_
                        select value.Value
                       ).ToArray();
            }
        }

        public void AddValue(string name, Term term)
        {
            values_.RemoveAll(value => value.Key == name);

            values_.Add(new KeyValuePair<string, Term>(name, term));
        }

        private Term GetValue(uint index)
        {
            if (index >= values_.Count) {
                return (new Term_Void());
            }

            return (values_[(int)index].Value);
        }

        private Term GetValue(string name)
        {
            return (GetValue((uint)values_.FindIndex(value => value.Key == name)));
        }

        public override bool ToBool(ExpressionCallStack cs)
        {
            return ((values_ != null) && (values_.Count > 0));
        }

        protected override Term Exec_REFERENCE(ExpressionCallStack cs, Term right)
        {
            /* === Term_Double === */
            {
                var right_r = right as Term_Double;

                if (right_r != null) {
                    return (GetValue((uint)right_r.Value));
                }
            }

            /* === Term_Text === */
            {
                var right_r = right as Term_Text;

                if (right_r != null) {
                    return (GetValue(right_r.Value));
                }
            }

            return (null);
        }

        protected override Term Exec_ARRAY(ExpressionCallStack cs, Term right)
        {
            values_.Add(new KeyValuePair<string, Term>(null, right));

            return (this);
        }
    }
}
