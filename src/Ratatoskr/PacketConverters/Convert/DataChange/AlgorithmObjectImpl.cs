using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Forms;
using Ratatoskr.Generic.Packet;
using Ratatoskr.Generic.Packet.Types;
using Ratatoskr.Utility;

namespace Ratatoskr.PacketConverters.Convert.DataChange
{
    internal sealed class AlgorithmObjectImpl : AlgorithmObject
    {
        private string   target_codes_exp_busy_ = "";
        private byte[][] target_codes_obj_busy_ = null;

        private string   target_codes_exp_new_ = "";
        private byte[][] target_codes_obj_new_ = null;

        private string   replace_code_exp_busy_ = "";
        private byte[]   replace_code_obj_busy_ = null;

        private string   replace_code_exp_new_ = "";
        private byte[]   replace_code_obj_new_ = null;

        
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox TBox_Target;
        private TextBox TBox_Replace;

        public AlgorithmObjectImpl()
        {
            InitializeComponent();
        }

        public AlgorithmObjectImpl(PacketConverterInstance instance, PacketConverterPropertyImpl prop) : base(instance, prop)
        {
            InitializeComponent();

            TBox_Target.Text = prop.DataChangeProperty.TargetPattern.Value;
            TBox_Replace.Text = prop.DataChangeProperty.ReplacePattern.Value;

            Apply();
        }

        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.TBox_Target = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TBox_Replace = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.TBox_Target);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.TBox_Replace);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(587, 25);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label3.Size = new System.Drawing.Size(66, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "Target code";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TBox_Target
            // 
            this.TBox_Target.Location = new System.Drawing.Point(75, 3);
            this.TBox_Target.Name = "TBox_Target";
            this.TBox_Target.Size = new System.Drawing.Size(200, 19);
            this.TBox_Target.TabIndex = 17;
            this.TBox_Target.TextChanged += new System.EventHandler(this.TBox_TextChanged);
            this.TBox_Target.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBox_KeyDown);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(281, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.label2.Size = new System.Drawing.Size(2, 24);
            this.label2.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(289, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label1.Size = new System.Drawing.Size(74, 18);
            this.label1.TabIndex = 12;
            this.label1.Text = "Replace code";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TBox_Replace
            // 
            this.TBox_Replace.Location = new System.Drawing.Point(369, 3);
            this.TBox_Replace.Name = "TBox_Replace";
            this.TBox_Replace.Size = new System.Drawing.Size(200, 19);
            this.TBox_Replace.TabIndex = 18;
            this.TBox_Replace.TextChanged += new System.EventHandler(this.TBox_TextChanged);
            this.TBox_Replace.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBox_KeyDown);
            // 
            // AlgorithmObjectImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "AlgorithmObjectImpl";
            this.Size = new System.Drawing.Size(587, 25);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void Apply()
        {
            target_codes_exp_busy_ = target_codes_exp_new_;
            target_codes_obj_busy_ = target_codes_obj_new_;

            replace_code_exp_busy_ = replace_code_exp_new_;
            replace_code_obj_busy_ = replace_code_obj_new_;

            UpdateView();
            UpdateConvertStatus();
        }

        private void UpdateView()
        {
            /* 表示中の変換式をコンパイル */
            target_codes_exp_new_ = TBox_Target.Text;
            target_codes_obj_new_ = HexTextEncoder.ToByteArrayMap(target_codes_exp_new_);

            replace_code_exp_new_ = TBox_Replace.Text;
            replace_code_obj_new_ = HexTextEncoder.ToByteArray(replace_code_exp_new_);

            /* 表示更新 */
            if (target_codes_exp_new_.Length > 0) {
                TBox_Target.BackColor = (target_codes_obj_new_ != null) ? (Color.LightSkyBlue) : (Color.LightPink);
            } else {
                TBox_Target.BackColor = Color.White;
            }

            if (replace_code_exp_new_.Length > 0) {
                TBox_Replace.BackColor = (replace_code_obj_new_ != null) ? (Color.LightSkyBlue) : (Color.LightPink);
            } else {
                TBox_Replace.BackColor = Color.White;
            }

            /* 変更状態確認 */
            TBox_Target.ForeColor = (target_codes_exp_busy_ != target_codes_exp_new_) ? (Color.Gray) : (Color.Black);
            TBox_Replace.ForeColor = (replace_code_exp_busy_ != replace_code_exp_new_) ? (Color.Gray) : (Color.Black);
        }

        private byte[] ConvertCode(byte[] data, byte[] pattern, byte[] changed_data)
        {
            /* 変換対象となるデータ長が見つからない場合はそのまま出力 */
            if (   (data == null)
                || (data.Length == 0)
                || (pattern == null)
                || (pattern.Length == 0)
                || (data.Length < pattern.Length)
            ) {
                return (data);
            }

            /* 変換後データが空のときはデータ長0の配列を定義 */
            if (changed_data == null) {
                changed_data = new byte[0];
            }

            var detect_list = new Stack<int>();
            var pattern_index = pattern.Length - 1;

            /* 高速化のため後方から検索 */
            for (var data_index = data.Length - 1; data_index >= pattern.Length; data_index--) {
                if (data[data_index] == pattern[pattern_index]) {
                    /* --- 連続一致(次のデータの比較に進む) --- */
                    pattern_index--;
                } else if (data[data_index] == pattern[pattern.Length - 1]) {
                    /* --- 検出開始(2バイト目の比較に進む) --- */
                    pattern_index = pattern.Length - 2;
                } else {
                    /* --- 検出エラー(1バイト目の比較に進む) --- */
                    pattern_index = pattern.Length - 1;
                }

                if (pattern_index >= 0)continue;

                /* 検出リストに追加 */
                detect_list.Push(data_index);

                /* 1バイト目の検出から再開する */
                pattern_index = pattern.Length - 1;
            }

            var dst_data = new byte[data.Length - detect_list.Count * (pattern.Length - changed_data.Length)];
            var dst_index = 0;
            var src_index = 0;
            var copy_size = 0;

            foreach (var detect_index in detect_list) {
                /* 前回の終端から検出位置までの元データをコピー */
                copy_size = detect_index - src_index;
                Buffer.BlockCopy(data, src_index, dst_data, dst_index, copy_size);
                dst_index += copy_size;

                /* 検出位置から変換先データをコピー */
                Buffer.BlockCopy(changed_data, 0, dst_data, dst_index, changed_data.Length);
                dst_index += changed_data.Length;

                src_index = detect_index + pattern.Length;
            }

            /* 残り元データをコピー */
            if (src_index < data.Length) {
                copy_size = Math.Min(data.Length - src_index, dst_data.Length - dst_index);
                Buffer.BlockCopy(data, src_index, dst_data, dst_index, copy_size);
            }

            return (dst_data);
        }

        public override void OnBackupProperty()
        {
            Property.DataChangeProperty.TargetPattern.Value = TBox_Target.Text;
            Property.DataChangeProperty.ReplacePattern.Value = TBox_Replace.Text;
        }

        public override void OnInputPacket(DataPacketObject input, ref List<PacketObject> output)
        {
            /* ターゲットコードが指定されていない場合はそのまま出力 */
            if (target_codes_obj_busy_ == null) {
                output.Add(input);
                return;
            }

            var data = input.GetData();

            /* データ変換 */
            foreach (var code in target_codes_obj_busy_) {
                data = ConvertCode(data, code, replace_code_obj_busy_);
            }

            output.Add(new StaticDataPacketObject(input, data));
        }

        private void TBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                if (   (target_codes_exp_busy_ != target_codes_exp_new_)
                    || (replace_code_exp_busy_ != replace_code_exp_new_)
                ) {
                    OnBackupProperty();
                    Apply();
                }
            }
        }

        private void TBox_TextChanged(object sender, EventArgs e)
        {
            UpdateView();
        }
    }
}
