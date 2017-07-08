using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ratatoskr.Actions;
using Ratatoskr.Scripts.Expression.Parser;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;

namespace Ratatoskr.Scripts.Expression.Terms
{
    internal sealed class Term_Id : Term
    {
        private enum PacketPosId
        {
            Unknown,
            Variable,
            Current,
            Preview,
        }

        private enum PacketInfoId
        {
            Unknown,
            IsControl,
            IsMessage,
            IsData,
            Alias,
            MakeTime,
            DeltaTime,
            Information,
            Mark,
            Data_IsSend,
            Data_IsRecv,
            Data_Source,
            Data_Destination,
            Data_Length,
            Data_HexText,
            Data_Ascii,
            Data_Utf8,
            Data_UnicodeL,
            Data_UnicodeB,
        }


        private static readonly char[] TOKENS = new char[] { '.' };
        private static readonly char TOKEN = '.';

        private string id_ = "";

        private PacketPosId  packet_pos_ = PacketPosId.Unknown;
        private PacketInfoId packet_info_ = PacketInfoId.Unknown;


        public Term_Id()
        {
        }

        public Term_Id(string id)
        {
            id_ = id;
        }

        public override bool ErrorCheck(ExpressionCallStack cs)
        {
            return (false);
        }

        public override bool ToBool(ExpressionCallStack cs)
        {
            var term = GetReferences(cs);

            return ((term != null) ? (term.ToBool(cs)) : (false));
        }

        public Term GetReferences(ExpressionCallStack cs)
        {
            /* 要素判定 */
            if (packet_pos_ == PacketPosId.Unknown) {
                var sep_pos = id_.IndexOf(TOKEN);

                if (sep_pos >= 0) {
                    var name_pos = id_.Substring(0, sep_pos).ToUpper();
                    var name_info = id_.Substring(sep_pos + 1).ToUpper();

                    /* ターゲットパケット */
                    switch (name_pos) {
                        case "CURRENT":  packet_pos_ = PacketPosId.Current;      break;
                        case "PREVIEW":  packet_pos_ = PacketPosId.Preview;      break;
                    }

                    switch (name_info) {
                        case "ISCONTROL":   packet_info_ = PacketInfoId.IsControl;      break;
                        case "ISMESSAGE":   packet_info_ = PacketInfoId.IsMessage;      break;
                        case "ISDATA":      packet_info_ = PacketInfoId.IsData;         break;
                        case "ALIAS":       packet_info_ = PacketInfoId.Alias;          break;
                        case "MAKETIME":    packet_info_ = PacketInfoId.MakeTime;      break;
                        case "DELTATIME":   packet_info_ = PacketInfoId.DeltaTime;      break;
                        case "INFORMATION": packet_info_ = PacketInfoId.Information;      break;
                        case "MARK":        packet_info_ = PacketInfoId.Mark;           break;
                        case "ISSEND":      packet_info_ = PacketInfoId.Data_IsSend;      break;
                        case "ISRECV":      packet_info_ = PacketInfoId.Data_IsRecv;      break;
                        case "SOURCE":      packet_info_ = PacketInfoId.Data_Source;      break;
                        case "DESTINATION": packet_info_ = PacketInfoId.Data_Destination;      break;
                        case "LENGTH":      packet_info_ = PacketInfoId.Data_Length;      break;
                        case "HEXTEXT":     packet_info_ = PacketInfoId.Data_HexText;      break;
                        case "ASCII":       packet_info_ = PacketInfoId.Data_Ascii;      break;
                        case "UTF8":        packet_info_ = PacketInfoId.Data_Utf8;      break;
                        case "UNICODEL":    packet_info_ = PacketInfoId.Data_UnicodeL;      break;
                        case "UNICODEB":    packet_info_ = PacketInfoId.Data_UnicodeB;      break;
                    }
                } else {
                    packet_pos_ = PacketPosId.Variable;
                }
            }

            /* 要素種別が判定できなかった場合は無効 */
            if (   (packet_pos_ == PacketPosId.Unknown)
                || (packet_info_ == PacketInfoId.Unknown)
            ) {
                return (null);
            }

            Term result = null;

            switch (packet_pos_) {
                case PacketPosId.Current:  result = GetReferences_Packet(cs, cs.CurrentPacket); break;
                case PacketPosId.Preview:  result = GetReferences_Packet(cs, cs.PrevPacket);    break;
                case PacketPosId.Variable: result = GetReferences_Variable(cs);                        break;
            }

            return (result);
        }

        private Term GetReferences_Packet(ExpressionCallStack cs, PacketObject packet)
        {
            if (packet == null)return (null);

            var result = (Term)null;

            switch (packet_info_) {
                /* --- 制御パケットかどうか --- */
                case PacketInfoId.IsControl:
                {
                    result = new Term_Bool(packet.Attribute == PacketAttribute.Control);
                }
                    break;

                /* --- メッセージパケットかどうか --- */
                case PacketInfoId.IsMessage:
                {
                    result = new Term_Bool(packet.Attribute == PacketAttribute.Message);
                }
                    break;

                /* --- データパケットかどうか --- */
                case PacketInfoId.IsData:
                {
                    result = new Term_Bool(packet.Attribute == PacketAttribute.Data);
                }
                    break;

                /* --- パケット生成元のエイリアス --- */
                case PacketInfoId.Alias:
                {
                    result = new Term_Text(packet.Alias);
                }
                    break;

                /* --- パケットの生成時間 --- */
                case PacketInfoId.MakeTime:
                {
                    result = new Term_DateTime(packet.MakeTime);
                }
                    break;

                /* --- 直前のパケットからの経過時間 --- */
                case PacketInfoId.DeltaTime:
                {
                    var ts = TimeSpan.MinValue;

                    if (cs.PrevPacket != null) {
                        ts = packet.MakeTime - cs.PrevPacket.MakeTime;
                    }

                    result = new Term_TimeSpan(ts);
                }
                    break;

                /* --- インフォメーション --- */
                case PacketInfoId.Information:
                {
                    result = new Term_Text(packet.Information);
                }
                    break;

                /* --- マーク --- */
                case PacketInfoId.Mark:
                {
                    result = new Term_Double(packet.UserMark);
                }
                    break;

                /* --- パケットのデータ要素 --- */
                default:
                {
                    result = GetArgument_Packet_Data(cs, packet as DataPacketObject);
                }
                    break;
            }

            return (result);
        }

        private Term GetArgument_Packet_Data(ExpressionCallStack cs, DataPacketObject packet)
        {
            if (packet == null)return (null);

            var result = (Term)null;

            switch (packet_info_) {
                /* --- 送信データ --- */
                case PacketInfoId.Data_IsSend:
                {
                    result = new Term_Bool(packet.Direction == PacketDirection.Send);
                }
                    break;

                /* --- 受信データ --- */
                case PacketInfoId.Data_IsRecv:
                {
                    result = new Term_Bool(packet.Direction == PacketDirection.Recv);
                }
                    break;

                /* --- 送信元情報 --- */
                case PacketInfoId.Data_Source:
                {
                    result = new Term_Text(packet.Source);
                }
                    break;

                /* --- 送信先情報 --- */
                case PacketInfoId.Data_Destination:
                {
                    result = new Term_Text(packet.Destination);
                }
                    break;

                /* --- データ長 --- */
                case PacketInfoId.Data_Length:
                {
                    result = new Term_Double(packet.GetDataSize());
                }
                    break;

                /* --- データ(16進テキスト) --- */
                case PacketInfoId.Data_HexText:
                {
                    result = new Term_Text(packet.GetHexText());
                }
                    break;

                /* --- データ(ASCII) --- */
                case PacketInfoId.Data_Ascii:
                {
                    result = new Term_Text(packet.GetAsciiText());
                }
                    break;

                /* --- データ(UTF8) --- */
                case PacketInfoId.Data_Utf8:
                {
                    result = new Term_Text(packet.GetUtf8Text());
                }
                    break;

                /* --- データ(Little Endian Unicode) --- */
                case PacketInfoId.Data_UnicodeL:
                {
                    result = new Term_Text(packet.GetUnicodeText(true));
                }
                    break;

                /* --- データ(Big Endian Unicode) --- */
                case PacketInfoId.Data_UnicodeB:
                {
                    result = new Term_Text(packet.GetUnicodeText(false));
                }
                    break;
            }

            return (result);
        }

        private Term GetReferences_Variable(ExpressionCallStack cs)
        {
            var term = (Term)null;

            if (!cs.Variables.TryGetValue(id_, out term)) {
                term = new Term_Void();
            }

            return (term);
        }

        protected override Term Exec_ARMOP_SET(ExpressionCallStack cs, Term right)
        {
            cs.Variables[id_] = right;

            return (right);
        }

        protected override Term Exec_CALL(ExpressionCallStack cs, Term right)
        {
            /* メソッド呼び出しの引数を取得 */
            var args = GetCallArguments(right);

            if (args == null)return (null);

            /* アクション名からオブジェクトに変換 */
            var act = GetCallAction(id_);

            if (act == null)return (null);

            /* アクションオブジェクトに引数を設定 */
            foreach (var arg in args.Select((v, i) => new { v, i })) {
                act.SetParameter((uint)arg.i, arg.v);
            }

            /* コールスタックを登録 */
            act.CallStack = cs;

            /* パラメータチェック */
            if (!act.ParameterCheck())return (null);

            /* アクション実行が許可されているときのみ実行 */
            if (cs.ExecuteFlag.HasFlag(ExecuteFlags.ActionExecute)) {
                /* 通常＋同期モードで実行 */
                cs.BusyAction = act;
                act.Exec();
                cs.BusyAction = null;

                /* 実行アクションを記録 */
                cs.LastAction = act;
            }

            return (act.GetResult(0));
        }

        protected override Term Exec_REFERENCE(ExpressionCallStack cs, Term right)
        {
            /* 参照時は参照先に変換してから行う */
            var term = GetReferences(cs);

            if (term == null) {
                return (new Term_Void());
            }

            return (term.Exec(cs, Tokens.REFERENCE, right));
        }

        private ActionObject GetCallAction(string name)
        {
            /* アクション名からアクションクラス名を取得 */
            var class_type = Type.GetType("Ratatoskr.Actions.ActionModules.Action_" + name);

            if (class_type == null)return (null);

            return (class_type.InvokeMember(
                        null,
                        System.Reflection.BindingFlags.CreateInstance,
                        null,
                        null,
                        new object[] { }
                    ) as ActionObject);
        }

        private Term[] GetCallArguments(Term term)
        {
            /* 引数無し呼び出しのときは空配列を返す */
            if (term == null)return (new Term[]{});

            var term_m = term as Term_Array;

            if (term_m != null) {
                /* 配列のときは中身を返す */
                return (term_m.Value);
            } else {
                /* 単一要素の場合はそのまま返す */
                return (new Term[] { term });
            }
        }
    }
}
