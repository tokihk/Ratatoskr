using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RtsCore.Framework.Packet.Filter;

namespace RtsCore.Framework.Packet.Filter.Terms
{
    internal abstract class Term
    {
        public virtual bool ToBool(PacketFilterCallStack cs)
        {
            return (false);
        }

        public virtual Term Exec(PacketFilterCallStack cs, Tokens token, Term term_sub)
        {
            var term_main = this;

            switch (token) {
                case Tokens.ARMOP_SET:          return (term_main.Exec_ARMOP_SET(cs, term_sub));
                case Tokens.ARMOP_ADD:          return (term_main.Exec_ARMOP_ADD(cs, term_sub));
                case Tokens.ARMOP_SUB:          return (term_main.Exec_ARMOP_SUB(cs, term_sub));
                case Tokens.ARMOP_MUL:          return (term_main.Exec_ARMOP_MUL(cs, term_sub));
                case Tokens.ARMOP_DIV:          return (term_main.Exec_ARMOP_DIV(cs, term_sub));
                case Tokens.ARMOP_REM:          return (term_main.Exec_ARMOP_REM(cs, term_sub));
                case Tokens.ARMOP_NEG:          return (term_main.Exec_ARMOP_NEG(cs));
                case Tokens.RELOP_GREATER:      return (term_main.Exec_RELOP_GREATER(cs, term_sub));
                case Tokens.RELOP_LESS:         return (term_main.Exec_RELOP_LESS(cs, term_sub));
                case Tokens.RELOP_GREATEREQUAL: return (term_main.Exec_RELOP_GREATEREQUAL(cs, term_sub));
                case Tokens.RELOP_LESSEQUAL:    return (term_main.Exec_RELOP_LESSEQUAL(cs, term_sub));
                case Tokens.RELOP_EQUAL:        return (term_main.Exec_RELOP_EQUAL(cs, term_sub));
                case Tokens.RELOP_UNEQUAL:      return (term_main.Exec_RELOP_UNEQUAL(cs, term_sub));
                case Tokens.LOGOP_AND:          return (term_main.Exec_LOGOP_AND(cs, term_sub));
                case Tokens.LOGOP_OR:           return (term_main.Exec_LOGOP_OR(cs, term_sub));
                default:                        return (new Term_Bool(ToBool(cs)));
            }
        }

        protected virtual Term Exec_ARMOP_SET(PacketFilterCallStack cs, Term term_sub)
        {
            return (null);
        }

        protected virtual Term Exec_ARMOP_ADD(PacketFilterCallStack cs, Term term_sub)
        {
            return (null);
        }

        protected virtual Term Exec_ARMOP_SUB(PacketFilterCallStack cs, Term term_sub)
        {
            return (null);
        }

        protected virtual Term Exec_ARMOP_MUL(PacketFilterCallStack cs, Term term_sub)
        {
            return (null);
        }

        protected virtual Term Exec_ARMOP_DIV(PacketFilterCallStack cs, Term term_sub)
        {
            return (null);
        }

        protected virtual Term Exec_ARMOP_REM(PacketFilterCallStack cs, Term term_sub)
        {
            return (null);
        }

        protected virtual Term Exec_ARMOP_NEG(PacketFilterCallStack cs)
        {
            return (new Term_Bool(!ToBool(cs)));
        }

        protected virtual Term Exec_RELOP_GREATER(PacketFilterCallStack cs, Term term_sub)
        {
            return (null);
        }

        protected virtual Term Exec_RELOP_LESS(PacketFilterCallStack cs, Term term_sub)
        {
            return (null);
        }

        protected virtual Term Exec_RELOP_GREATEREQUAL(PacketFilterCallStack cs, Term term_sub)
        {
            return (null);
        }

        protected virtual Term Exec_RELOP_LESSEQUAL(PacketFilterCallStack cs, Term term_sub)
        {
            return (null);
        }

        protected virtual Term Exec_RELOP_EQUAL(PacketFilterCallStack cs, Term term_sub)
        {
            return (null);
        }

        protected virtual Term Exec_RELOP_UNEQUAL(PacketFilterCallStack cs, Term term_sub)
        {
            return (new Term_Bool(!Exec_RELOP_EQUAL(cs, term_sub).ToBool(cs)));
        }

        protected virtual Term Exec_LOGOP_AND(PacketFilterCallStack cs, Term term_sub)
        {
            return (new Term_Bool(ToBool(cs) && term_sub.ToBool(cs)));
        }

        protected virtual Term Exec_LOGOP_OR(PacketFilterCallStack cs, Term term_sub)
        {
            return (new Term_Bool(ToBool(cs) || term_sub.ToBool(cs)));
        }
    }
}
