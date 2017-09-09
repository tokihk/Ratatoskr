using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ratatoskr.Gate;
using Ratatoskr.Generic;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;
using Ratatoskr.Scripts.PacketFilterExp.Terms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_RecvWait : ActionObject
    {
        public enum Argument
        {
            Gate,
            InputFormat,
            CheckPattern,
            TimeOut,
        }

        public enum Result
        {
            RecvState,
            MatchGate,
            MatchData,
        }

        private Regex     gate_regex_ = null;

        private string    input_format_ = "";

        private string                       wildcard_pattern_ = "";
        private Dictionary<string, Wildcard> wildcard_data_ = new Dictionary<string, Wildcard>();

        private Stopwatch timeout_timer_ = new Stopwatch();
        private int       timeout_value_ = 0;


        public Action_RecvWait()
        {
            RegisterArgument(Argument.Gate.ToString(), typeof(string), null);
            RegisterArgument(Argument.InputFormat.ToString(), typeof(string), null);
            RegisterArgument(Argument.CheckPattern.ToString(), typeof(string), null);
            RegisterArgument(Argument.TimeOut.ToString(), typeof(int), null);
        }

        public Action_RecvWait(string gate, string input_format, string check_pattern, int timeout) : this()
        {
            SetArgumentValue(Argument.Gate.ToString(), gate);
            SetArgumentValue(Argument.InputFormat.ToString(), input_format);
            SetArgumentValue(Argument.CheckPattern.ToString(), check_pattern);
            SetArgumentValue(Argument.TimeOut.ToString(), timeout);
        }

        protected override void OnExecStart()
        {
            var param_gate = GetArgumentValue(Argument.Gate.ToString()) as string;
            var param_format = GetArgumentValue(Argument.InputFormat.ToString()) as string;
            var param_pattern = GetArgumentValue(Argument.CheckPattern.ToString()) as string;
            var param_timeout = (int)GetArgumentValue(Argument.TimeOut.ToString());

            /* ゲート検出用正規表現モジュール生成 */
            gate_regex_ = GateManager.GetWildcardAliasModule(param_gate);

            /* 入力データフォーマットバックアップ */
            input_format_ = param_format;

            /* パターンバックアップ */
            wildcard_pattern_ = param_pattern;

            /* タイムアウト設定 */
            timeout_value_ = param_timeout;
            if (timeout_value_ > 0) {
                timeout_timer_.Restart();
            }

            /* イベント登録 */
            GatePacketManager.EventPacketCleared += OnEventPacketCleared;
            GatePacketManager.EventPacketEntried += OnEventPacketEntried;
        }

        protected override void OnExecPoll()
        {
            /* パターン検出 */
            lock (wildcard_data_) {
                foreach (var wildcard in wildcard_data_) {
                    if (wildcard.Value.IsMatch) {
                        SetResult(ActionResultType.Success, new [] {
                            new ActionParam(Result.RecvState.ToString(), typeof(bool), true),
                            new ActionParam(Result.MatchGate.ToString(), typeof(string), wildcard.Key),
                            new ActionParam(Result.MatchData.ToString(), typeof(string), wildcard.Value.MatchText)
                        });
                        return;
                    }
                }
            }

            /* タイムアウト */
            if (   (timeout_timer_.IsRunning)
                && (timeout_timer_.ElapsedMilliseconds > timeout_value_)
            ) {
                SetResult(ActionResultType.Success, new [] {
                    new ActionParam(Result.RecvState.ToString(), typeof(bool), false)
                });

                return;
            }

            System.Threading.Thread.Sleep(1);
        }

        protected override void OnExecComplete()
        {
            GatePacketManager.EventPacketCleared -= OnEventPacketCleared;
            GatePacketManager.EventPacketEntried -= OnEventPacketEntried;
        }

        private void OnEventPacketCleared()
        {
        }

        private void OnEventPacketEntried(IEnumerable<PacketObject> packets)
        {
            var match_alias = (string)null;
            var match_wildcard = (Wildcard)null;

            lock (wildcard_data_) {
                foreach (var packet in packets) {
                    /* データパケット以外は無視 */
                    if (packet.Attribute != PacketAttribute.Data)continue;

                    var packet_d = packet as DataPacketObject;

                    /* 受信パケット以外は無視 */
                    if (packet_d.Direction != PacketDirection.Recv)continue;

                    if (   (match_alias == null)
                        || (match_wildcard == null)
                        || (match_alias != packet.Alias)
                    ) {
                        /* 判定用ワイルドカードモジュール取得 */
                        if (!wildcard_data_.TryGetValue(packet.Alias, out match_wildcard)) {
                            /* 処理可能なゲートか判定 */
                            if (!gate_regex_.IsMatch(packet.Alias))continue;

                            /* 新しい判定用ワイルドカードモジュールを生成 */
                            match_wildcard = new Wildcard(wildcard_pattern_);
                            wildcard_data_.Add(packet.Alias, match_wildcard);
                        }

                        /* 受信データを取得 */
                        var recv_data = packet_d.GetFormatString(input_format_);

                        if (match_wildcard != null) {
                            match_alias = packet.Alias;
                            match_wildcard.Input(recv_data);
                        }
                    }
                }
            }
        }
    }
}
