﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ratatoskr.Scripts.PacketFilterExp.Parser;
using Ratatoskr.Packet;

namespace Ratatoskr.Scripts.PacketFilterExp
{
    internal sealed class ExpressionFilter
    {
        public static ExpressionFilter Build(string exp_text)
        {
            /* 式オブジェクトに変換 */
            var exp = ExpressionCompiler.Compile(exp_text);

            if (exp == null)return (null);

            return (new ExpressionFilter(exp));
        }


        private ExpressionObject exp_ = null;


        private ExpressionFilter(ExpressionObject exp)
        {
            exp_ = exp;
        }

        public string ExpressionText
        {
            get { return (exp_.ExpressionText); }
        }

        public ExpressionCallStack CallStack { get; set; } = new ExpressionCallStack();

        public bool Input(PacketObject packet)
        {
            /* 式が無効の場合は有効パケットとして扱う */
            if (exp_ == null)return (true);

            /* カレントパケット更新 */
            CallStack.LastPacket = packet;

            /* ヒットテスト */
            var hitstate = false;

            if (!exp_.HitTest(CallStack, out hitstate)) {
                /* --- 解析失敗時は有効パケットとして扱う --- */
                hitstate = true;
            }

            return (hitstate);
        }
    }
}
