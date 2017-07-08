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
using Ratatoskr.Scripts.Expression.Terms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_RecvWait : ActionObject
    {
        private Regex     gate_regex_ = null;

        private string    input_format_ = "";

        private string                       wildcard_pattern_ = "";
        private Dictionary<string, Wildcard> wildcard_data_ = new Dictionary<string, Wildcard>();

        private Stopwatch timeout_timer_ = new Stopwatch();
        private int       timeout_value_ = 0;


        public Action_RecvWait()
        {
            InitParameter<Term_Text>("gate");
            InitParameter<Term_Text>("input_format");
            InitParameter<Term_Text>("check_pattern");
            InitParameter<Term_Double>("timeout");

            InitResult<Term_Bool>("recv");
            InitResult<Term_Text>("gate");
            InitResult<Term_Text>("data");
        }

        public override bool OnParameterCheck()
        {
            /* gate */
            if (GetParameter<Term_Text>("gate") == null) {
                return (false);
            }

            /* input_format */
            if (GetParameter<Term_Text>("input_format") == null) {
                return (false);
            }

            /* check_pattern */
            if (GetParameter<Term_Text>("check_pattern") == null) {
                return (false);
            }

            /* timeout */
            if (GetParameter<Term_Double>("timeout") == null) {
                return (false);
            }

            return (true);
        }

        protected override ExecState OnExecStart()
        {
            if (!ParameterCheck()) {
                return (ExecState.Complete);
            }

            var param_gate = GetParameter<Term_Text>("gate");
            var param_format = GetParameter<Term_Text>("input_format");
            var param_pattern = GetParameter<Term_Text>("check_pattern");
            var param_timeout = GetParameter<Term_Double>("timeout");

            /* ゲート検出用正規表現モジュール生成 */
            gate_regex_ = GateManager.GetWildcardAliasModule(param_gate.Value);

            /* 入力データフォーマットバックアップ */
            input_format_ = param_format.Value;

            /* パターンバックアップ */
            wildcard_pattern_ = param_pattern.Value;

            /* タイムアウト設定 */
            timeout_value_ = (int)param_timeout.Value;
            if (timeout_value_ > 0) {
                timeout_timer_.Restart();
            }

            /* イベント登録 */
            GatePacketManager.EventPacketCleared += OnEventPacketCleared;
            GatePacketManager.EventPacketEntried += OnEventPacketEntried;

            return (ExecState.Busy);
        }

        protected override ExecState OnExecPoll()
        {
            /* パターン検出 */
            lock (wildcard_data_) {
                foreach (var wildcard in wildcard_data_) {
                    if (wildcard.Value.IsMatch) {
                        SetResult("recv", new Term_Bool(true));
                        SetResult("gate", new Term_Text(wildcard.Key));
                        SetResult("data", new Term_Text(wildcard.Value.MatchText));
                        return (ExecState.Complete);
                    }
                }
            }

            /* タイムアウト */
            if (   (timeout_timer_.IsRunning)
                && (timeout_timer_.ElapsedMilliseconds > timeout_value_)
            ) {
                SetResult("recv", new Term_Bool(false));
                return (ExecState.Complete);
            }

            System.Threading.Thread.Sleep(1);

            return (ExecState.Busy);
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
