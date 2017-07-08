using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ratatoskr.Scripts.Expression.Parser;

namespace Ratatoskr.Scripts.Expression.Terms
{
    internal abstract class Term
    {
        public virtual bool ErrorCheck(ExpressionCallStack cs)
        {
            return (false);
        }

        public virtual bool ToBool(ExpressionCallStack cs)
        {
            return (false);
        }

        public virtual Term Exec(ExpressionCallStack cs, Tokens token, Term right)
        {
            var left = this;

            /* 左辺がIDの場合は一部条件のときのみ参照先と差し替え */
            if (left is Term_Id) {
                if (   (token != Tokens.ARMOP_SET)
                    && (token != Tokens.ARRAY)
                    && (token != Tokens.CALL)
                    && (token != Tokens.REFERENCE)
                ) {
                    left = (left as Term_Id).GetReferences(cs);
                }
            }

            if (left == null)return (null);

            /* 右辺がIDの場合は参照先と差し替え */
            if ((right != null) && (right is Term_Id)) {
                right = (right as Term_Id).GetReferences(cs);
            }

            /* メソッド呼び出し以外で右辺が存在しない場合はエラー */
            if ((token != Tokens.CALL) && (right == null)) {
                return (null);
            }

            switch (token) {
                case Tokens.ARMOP_SET:          return (left.Exec_ARMOP_SET(cs, right));
                case Tokens.ARMOP_ADD:          return (left.Exec_ARMOP_ADD(cs, right));
                case Tokens.ARMOP_SUB:          return (left.Exec_ARMOP_SUB(cs, right));
                case Tokens.ARMOP_MUL:          return (left.Exec_ARMOP_MUL(cs, right));
                case Tokens.ARMOP_DIV:          return (left.Exec_ARMOP_DIV(cs, right));
                case Tokens.ARMOP_REM:          return (left.Exec_ARMOP_REM(cs, right));
                case Tokens.RELOP_GREATER:      return (left.Exec_RELOP_GREATER(cs, right));
                case Tokens.RELOP_LESS:         return (left.Exec_RELOP_LESS(cs, right));
                case Tokens.RELOP_GREATEREQUAL: return (left.Exec_RELOP_GREATEREQUAL(cs, right));
                case Tokens.RELOP_LESSEQUAL:    return (left.Exec_RELOP_LESSEQUAL(cs, right));
                case Tokens.RELOP_EQUAL:        return (left.Exec_RELOP_EQUAL(cs, right));
                case Tokens.RELOP_UNEQUAL:      return (left.Exec_RELOP_UNEQUAL(cs, right));
                case Tokens.LOGOP_AND:          return (left.Exec_LOGOP_AND(cs, right));
                case Tokens.LOGOP_OR:           return (left.Exec_LOGOP_OR(cs, right));
                case Tokens.ARRAY:              return (left.Exec_ARRAY(cs, right));
                case Tokens.CALL:               return (left.Exec_CALL(cs, right));
                case Tokens.REFERENCE:          return (left.Exec_REFERENCE(cs, right));
                default:                        return (null);
            }
        }

        protected virtual Term Exec_ARMOP_SET(ExpressionCallStack cs, Term right)
        {
            return (null);
        }

        protected virtual Term Exec_ARMOP_ADD(ExpressionCallStack cs, Term right)
        {
            return (null);
        }

        protected virtual Term Exec_ARMOP_SUB(ExpressionCallStack cs, Term right)
        {
            return (null);
        }

        protected virtual Term Exec_ARMOP_MUL(ExpressionCallStack cs, Term right)
        {
            return (null);
        }

        protected virtual Term Exec_ARMOP_DIV(ExpressionCallStack cs, Term right)
        {
            return (null);
        }

        protected virtual Term Exec_ARMOP_REM(ExpressionCallStack cs, Term right)
        {
            return (null);
        }

        protected virtual Term Exec_RELOP_GREATER(ExpressionCallStack cs, Term right)
        {
            return (null);
        }

        protected virtual Term Exec_RELOP_LESS(ExpressionCallStack cs, Term right)
        {
            return (null);
        }

        protected virtual Term Exec_RELOP_GREATEREQUAL(ExpressionCallStack cs, Term right)
        {
            return (null);
        }

        protected virtual Term Exec_RELOP_LESSEQUAL(ExpressionCallStack cs, Term right)
        {
            return (null);
        }

        protected virtual Term Exec_RELOP_EQUAL(ExpressionCallStack cs, Term right)
        {
            return (null);
        }

        protected Term Exec_RELOP_UNEQUAL(ExpressionCallStack cs, Term right)
        {
            var val = Exec_RELOP_EQUAL(cs, right);

            if (val == null)return (null);

            return (new Term_Bool(!val.ToBool(cs)));
        }

        protected Term Exec_LOGOP_AND(ExpressionCallStack cs, Term right)
        {
            return (new Term_Bool(ToBool(cs) && right.ToBool(cs)));
        }

        protected Term Exec_LOGOP_OR(ExpressionCallStack cs, Term right)
        {
            return (new Term_Bool(ToBool(cs) || right.ToBool(cs)));
        }

        protected virtual Term Exec_ARRAY(ExpressionCallStack cs, Term right)
        {
            return (new Term_Array(this, right));
        }

        protected virtual Term Exec_CALL(ExpressionCallStack cs, Term right)
        {
            return (new Term_Void());
        }

        protected virtual Term Exec_REFERENCE(ExpressionCallStack cs, Term right)
        {
            return (new Term_Void());
        }
    }
}
