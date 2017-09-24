using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Scripts.PacketFilterExp.Parser;
using Ratatoskr.Scripts.PacketFilterExp;
using Ratatoskr.Generic.Packet;

namespace Ratatoskr.PacketConverters.Filter
{
    internal sealed class PacketConverterInstanceImpl : PacketConverterInstance
    {
        private const int EXP_LOG_LIMIT = 10;

        private PacketConverterPropertyImpl prop_;

        private string           filter_exp_busy_ = "";
        private ExpressionFilter filter_obj_busy_ = null;

        private string           filter_exp_new_ = "";
        private ExpressionFilter filter_obj_new_ = null;

        private ToolTip TTip_Filter;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ComboBox CBox_Exp;


        public PacketConverterInstanceImpl() : base()
        {
            InitializeComponent();
        }

        public PacketConverterInstanceImpl(PacketConverterManager pcvtm, PacketConverterClass pcvtd, PacketConverterProperty pcvtp, Guid id)
            : base(pcvtm, pcvtd, pcvtp, id)
        {
            prop_ = Property as PacketConverterPropertyImpl;

            InitializeComponent();

            TTip_Filter.SetToolTip(
                CBox_Exp,
@"通過させるパケットのルールを記述します。
  Ex: Data && HexText == /02.*03/
  Ex: DateTime >= 2017-09-11T16:44:50.000+09:00

[使用可能な算術演算子]
  + - * / ! ()

[使用可能な論理演算子]
  && ||

[使用可能な比較演算子]
  == != >= <= < >

以下のデータを定義できます。
  0～99999999     :<Number>10進数
  0x0～0xFFFFFFFF :<Number>16進数
  ISO8601形式     :<DateTime>時刻情報(ISO8601形式)
  ""...""         :<Text>文字列
  /.../           :<Text>正規表現

以下の状態変数を使用できます。
  PacketCount     :<Number>入力されたパケットの総数
  LastDelta       :<Number>現在のパケットと直前のパケットの差分時刻(msec)
  IsMessage       :<Bool>メッセージパケットのときはTrueになります。
  IsData          :<Bool>データパケットのときはTrueになります。
  Alias           :<Text>パケットのエイリアス
  DateTime        :<DateTime>パケットのUTC時刻
  Information     :<Text>パケットの付加情報
  IsSend          :<Bool>送信データのときはTrueになります。
  IsRecv          :<Bool>受信データのときはTrueになります。
  Source          :<Text>パケットの送信元情報。
  Destination     :<Text>パケットの送信先情報。
  DataSize        :<Number>パケットのペイロードデータサイズ。
  BitText         :<Text>パケットのペイロードデータ(2進数文字列) 01010101...
  HexText         :<Text>パケットのペイロードデータ(16進数文字列) F0F1F2F3...
  AsciiText       :<Text>パケットのペイロードデータ(アスキー文字列)
  Utf8Text        :<Text>パケットのペイロードデータ(UTF-8文字列)
  UnicodeLText    :<Text>パケットのペイロードデータ(UTF-32 Little Endian)
  UnicodeBText    :<Text>パケットのペイロードデータ(UTF-32 Bit Endian)
");

            SetExpList(prop_.ExpList.Value);

            Apply();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.CBox_Exp = new System.Windows.Forms.ComboBox();
            this.TTip_Filter = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // CBox_Exp
            // 
            this.CBox_Exp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CBox_Exp.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBox_Exp.FormattingEnabled = true;
            this.CBox_Exp.Location = new System.Drawing.Point(0, 3);
            this.CBox_Exp.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.CBox_Exp.Name = "CBox_Exp";
            this.CBox_Exp.Size = new System.Drawing.Size(604, 20);
            this.CBox_Exp.TabIndex = 1;
            this.CBox_Exp.TextChanged += new System.EventHandler(this.CBox_Exp_TextChanged);
            this.CBox_Exp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CBox_Exp_KeyDown);
            // 
            // TTip_Filter
            // 
            this.TTip_Filter.AutoPopDelay = 10000;
            this.TTip_Filter.InitialDelay = 500;
            this.TTip_Filter.ReshowDelay = 100;
            // 
            // PacketConverterInstanceImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.CBox_Exp);
            this.Name = "PacketConverterInstanceImpl";
            this.Size = new System.Drawing.Size(607, 24);
            this.ResumeLayout(false);

        }

        private void SetExpList(IEnumerable<string> exp_list)
        {
            CBox_Exp.BeginUpdate();
            {
                CBox_Exp.Items.Clear();
                CBox_Exp.Items.AddRange(exp_list.ToArray());

                /* 先頭のアイテムを選択 */
                if (CBox_Exp.Items.Count > 0) {
                    CBox_Exp.SelectedIndex = 0;
                }
            }
            CBox_Exp.EndUpdate();
        }

        private IEnumerable<string> GetExpList()
        {
            return (from string exp in CBox_Exp.Items
                    select exp);
        }

        private void Apply()
        {
            filter_exp_busy_ = filter_exp_new_;
            filter_obj_busy_ = filter_obj_new_;

            UpdateView();

            /* 変換器反映 */
            UpdateConvertStatus();
        }

        private void UpdateView()
        {
            /* 表示中のフィルター式をコンパイル */
            filter_exp_new_ = CBox_Exp.Text;
            filter_obj_new_ = ExpressionFilter.Build(filter_exp_new_);

            /* 表示更新 */
            if (filter_exp_new_.Length > 0) {
                CBox_Exp.BackColor = (filter_obj_new_ != null) ? (Color.LightSkyBlue) : (Color.LightPink);
            } else {
                CBox_Exp.BackColor = Color.White;
            }

            /* 変更状態確認 */
            CBox_Exp.ForeColor = (filter_exp_busy_ != filter_exp_new_) ? (Color.Gray) : (Color.Black);
        }

        private void AddExpLog(string exp)
        {
            CBox_Exp.BeginUpdate();
            {
                var text_now = CBox_Exp.Text;

                /* 重複するコマンドを削除 */
                CBox_Exp.Items.Remove(exp);

                /* ログの最大値に合わせて古いログを削除 */
                if (CBox_Exp.Items.Count >= EXP_LOG_LIMIT) {
                    CBox_Exp.Items.RemoveAt(CBox_Exp.Items.Count - 1);
                }

                /* 先頭に追加 */
                CBox_Exp.Items.Insert(0, exp);

                /* コマンドを復元 */
                CBox_Exp.Text = text_now;
            }
            CBox_Exp.EndUpdate();
        }

        protected override void OnBackupProperty()
        {
            prop_.ExpList.Value.Clear();
            prop_.ExpList.Value.AddRange(from string exp in CBox_Exp.Items select exp);
        }

        protected override void OnInputStatusClear()
        {
            if (filter_obj_busy_ != null) {
                filter_obj_busy_.CallStack = new ExpressionCallStack();
            }
        }

        protected override void OnInputPacket(PacketObject input, ref List<PacketObject> output)
        {
            if ((filter_obj_busy_ == null) || (filter_obj_busy_.Input(input))) {
                output.Add(input);
            }
        }

        private void CBox_Exp_TextChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void CBox_Exp_KeyDown(object sender, KeyEventArgs e)
        {
            if (   (e.KeyCode == Keys.Enter)
                && ((prop_.ExpList.Value.Count == 0) || (CBox_Exp.Text != prop_.ExpList.Value[0]))
            ) {
                AddExpLog(CBox_Exp.Text);
                OnBackupProperty();
                Apply();
            }
        }
    }
}
