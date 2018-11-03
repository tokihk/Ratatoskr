using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RtsCore.Framework.PacketConverter;
using RtsCore.Packet;
using RtsCore.Utility;

namespace Ratatoskr.PacketConverters.Separator.DataContentsMatch
{
    internal sealed class RuleObjectImpl : RuleObject
    {
        private class PatternMatchObject
        {
            private byte[] match_data_ = null;
            private int    match_size_ = 0;


            public PatternMatchObject(byte[] match_data)
            {
                match_data_ = match_data;
            }

            public void Reset()
            {
                match_size_ = 0;
            }

            public bool Input(byte data)
            {
                if (match_size_ >= match_data_.Length) {
                    match_size_ = 0;
                }

                if (data == match_data_[match_size_]) {
                    match_size_++;
                } else if (data == match_data_[0]) {
                    match_size_ = 1;
                } else {
                    match_size_ = 0;
                }

                return (match_size_ >= match_data_.Length);
            }
        }


        private DynamicPacketObject packet_busy_ = null;
        private PacketObject        packet_last_ = null;

        private byte[][]             match_data_list_ = null;
        private PatternMatchObject[] match_objs_ = null;

        private Forms.Controls.RoundTextBox TBox_Value;

        public RuleObjectImpl()
        {
            InitializeComponent();
        }

        public RuleObjectImpl(PacketConverterInstance instance, PacketConverterPropertyImpl prop) : base(instance, prop)
        {
            InitializeComponent();

            SetPattern(prop.DataContentsMatchProperty.Pattern.Value);

            Apply();
        }

        private void InitializeComponent()
        {
            this.TBox_Value = new Ratatoskr.Forms.Controls.RoundTextBox();
            this.SuspendLayout();
            // 
            // TBox_Value
            // 
            this.TBox_Value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TBox_Value.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TBox_Value.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TBox_Value.Location = new System.Drawing.Point(0, 0);
            this.TBox_Value.Margin = new System.Windows.Forms.Padding(0);
            this.TBox_Value.Name = "TBox_Value";
            this.TBox_Value.Size = new System.Drawing.Size(433, 19);
            this.TBox_Value.TabIndex = 1;
            this.TBox_Value.TextChanged += new System.EventHandler(this.TBox_Value_TextChanged);
            this.TBox_Value.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBox_Value_KeyDown);
            // 
            // RuleObjectImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.TBox_Value);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "RuleObjectImpl";
            this.Size = new System.Drawing.Size(433, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void SetPattern(string value)
        {
            TBox_Value.Text = value;
        }

        private void Apply()
        {
            /* バッファクリア */
            packet_busy_ = null;

            /* パターンコードを生成 */
            match_objs_ = CreateMatchObjects(Property.DataContentsMatchProperty.Pattern.Value);

            UpdateView();
            UpdateConvertStatus();
        }

        private void UpdateView()
        {
            var text = TBox_Value.Text;

            TBox_Value.ForeColor = (text == Property.DataContentsMatchProperty.Pattern.Value)
                                 ? (Color.Black)
                                 : (Color.Gray);

            if (text.Length > 0) {
                TBox_Value.BackColor = (HexTextEncoder.ToByteArray(TBox_Value.Text) != null)
                                     ? (RtsCore.Parameter.COLOR_OK)
                                     : (RtsCore.Parameter.COLOR_NG);
            } else {
                TBox_Value.BackColor = Color.White;
            }
        }

        private PatternMatchObject[] CreateMatchObjects(string pattern)
        {
            /* パターンコードからパターン一覧を作成 */
            var patterns = HexTextEncoder.ToByteArrayMap(pattern);

            if (patterns == null)return (null);

            /* マッチオブジェクトに変換 */
            var match_objs = new List<PatternMatchObject>();

            foreach (var pattern_data in patterns) {
                match_objs.Add(new PatternMatchObject(pattern_data));
            }

            return (match_objs.ToArray());
        }

        public override void OnBackupProperty()
        {
            Property.DataContentsMatchProperty.Pattern.Value = TBox_Value.Text;
        }

        protected override void OnInputStatusClear()
        {
            /* マッチデータをリセット */
            if (match_objs_ != null) {
                foreach (var obj in match_objs_) {
                    obj.Reset();
                }
            }

            /* 受信状態クリア */
            packet_busy_ = null;
            packet_last_ = null;
        }

        protected override void OnInputPacket(PacketObject input, ref List<PacketObject> output)
        {
            /* パターンが設定されていない場合はスルー */
            if ((match_objs_ == null) || (match_objs_.Length == 0)) {
                output.Add(input);
                return;
            }

            /* 収集開始 */
            if (packet_busy_ == null) {
                packet_busy_ = new DynamicPacketObject(input);

                foreach (var obj in match_objs_) {
                    obj.Reset();
                }
            }

            /* 最終受信パケットを記憶 */
            packet_last_ = input;

            var packet_new = (PacketObject)null;
            var match_ok = false;

            /* データ収集 */
            foreach (var data in input.Data) {
                /* 仮想パケットにデータを追加 */
                packet_busy_.AddData(data);

                /* 全マッチオブジェクトにデータセット */
                foreach (var obj in match_objs_) {
                    if (obj.Input(data)) {
                        match_ok = true;
                        break;
                    }
                }

                /* どれか１つでもパターンが一致すればOK */
                if (!match_ok)continue;

                /* 全マッチオブジェクトを初期化 */
                match_ok = false;
                foreach (var obj in match_objs_) {
                    obj.Reset();
                }

                /* パケット生成 */
                packet_new = packet_busy_.Compile(packet_last_);
                if (packet_new != null) {
                    output.Add(packet_new);
                }

                /* 新しいパケットの収集を開始 */
                packet_busy_ = new DynamicPacketObject(input);
            }
        }

        protected override void OnInputBreak(ref List<PacketObject> output)
        {
            if (packet_busy_ == null) {
                return;
            }

            if (packet_busy_.DataLength > 0) {
                var packet_new = packet_busy_.Compile(packet_last_);

                if (packet_new != null) {
                    output.Add(packet_new);
                }
            }

            packet_busy_ = null;
        }

        private void TBox_Value_TextChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void TBox_Value_KeyDown(object sender, KeyEventArgs e)
        {
            if (   (e.KeyCode == Keys.Enter)
                && (TBox_Value.Text != Property.DataContentsMatchProperty.Pattern.Value)
            ) {
                OnBackupProperty();
                Apply();
            }
        }
    }
}
