﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.PacketConverter;
using Ratatoskr.Forms;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketConverter.Convert
{
    internal sealed class PacketConverterInstanceImpl : PacketConverterInstance
    {
        private sealed class AlgorithmTypeObject
        {
            public AlgorithmType Value { get; }


            public AlgorithmTypeObject(AlgorithmType type)
            {
                Value = type;
            }

            public override bool Equals(object obj)
            {
                return (Value.Equals(obj));
            }

            public override int GetHashCode()
            {
                return (base.GetHashCode());
            }

            public override string ToString()
            {
                switch (Value) {
                    case AlgorithmType.None:
                        return ("No convert");
                    case AlgorithmType.ChangeAlias:
                        return ("Change alias");
                    case AlgorithmType.CodeExtentionEncode:
                        return ("Code encode");
                    case AlgorithmType.CodeExtentionDecode:
                        return ("Code decode");
                    case AlgorithmType.DataRemove:
                        return ("Data remove");
                    case AlgorithmType.DataChange:
                        return ("Data change");
//                    case AlgorithmType.Custom:
//                        return ("(カスタム)");
                    default:
                        return (Value.ToString());
                }
            }
        }


        private PacketConverterPropertyImpl prop_;
        private AlgorithmObject algorithm_obj_ = null;
        private Panel Panel_Sub;
        private RoundComboBox CBox_AlgorithmList;

        public PacketConverterInstanceImpl() : base()
        {
            InitializeComponent();
        }

        public PacketConverterInstanceImpl(PacketConvertManager pcvtm, PacketConverterClass pcvtd, PacketConverterProperty pcvtp, Guid id)
            : base(pcvtm, pcvtd, pcvtp, id)
        {
            prop_ = Property as PacketConverterPropertyImpl;

            InitializeComponent();
            InitializeAlgorithmList();

            SelectAlgorithm(prop_.Algorithm.Value);
        }

        private void InitializeComponent()
        {
            this.CBox_AlgorithmList = new RoundComboBox();
            this.Panel_Sub = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // CBox_AlgorithmList
            // 
            this.CBox_AlgorithmList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_AlgorithmList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CBox_AlgorithmList.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CBox_AlgorithmList.FormattingEnabled = true;
            this.CBox_AlgorithmList.Location = new System.Drawing.Point(3, 3);
            this.CBox_AlgorithmList.Name = "CBox_AlgorithmList";
            this.CBox_AlgorithmList.Size = new System.Drawing.Size(160, 20);
            this.CBox_AlgorithmList.TabIndex = 1;
            this.CBox_AlgorithmList.SelectedIndexChanged += new System.EventHandler(this.CBox_Exp_SelectedIndexChanged);
            // 
            // Panel_Sub
            // 
            this.Panel_Sub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Sub.Location = new System.Drawing.Point(166, 0);
            this.Panel_Sub.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Sub.Name = "Panel_Sub";
            this.Panel_Sub.Size = new System.Drawing.Size(432, 25);
            this.Panel_Sub.TabIndex = 2;
            // 
            // PacketConverterInstanceImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.Panel_Sub);
            this.Controls.Add(this.CBox_AlgorithmList);
            this.Name = "PacketConverterInstanceImpl";
            this.Size = new System.Drawing.Size(598, 25);
            this.ResumeLayout(false);

        }

        private void InitializeAlgorithmList()
        {
            CBox_AlgorithmList.BeginUpdate();
            {
                CBox_AlgorithmList.Items.Clear();
                foreach (AlgorithmType type in Enum.GetValues(typeof(AlgorithmType))) {
                    CBox_AlgorithmList.Items.Add(new AlgorithmTypeObject(type));
                }
            }
            CBox_AlgorithmList.EndUpdate();
        }

        private AlgorithmObject CreateAlgorithmObject(AlgorithmType type)
        {
            var obj = (AlgorithmObject)null;

            switch (type) {
                case AlgorithmType.ChangeAlias:
                    obj = new ChangeAlias.AlgorithmObjectImpl(this, prop_);
                    break;

                case AlgorithmType.CodeExtentionEncode:
                    obj = new CodeExtentionEncode.AlgorithmObjectImpl(this, prop_);
                    break;

                case AlgorithmType.CodeExtentionDecode:
                    obj = new CodeExtentionDecode.AlgorithmObjectImpl(this, prop_);
                    break;

                case AlgorithmType.DataRemove:
                    obj = new DataRemove.AlgorithmObjectImpl(this, prop_);
                    break;

                case AlgorithmType.DataChange:
                    obj = new DataChange.AlgorithmObjectImpl(this, prop_);
                    break;

//                case AlgorithmType.Custom:
//                    obj = new MSecPassingFromFirstPacket.RuleObjectImpl();
//                    break;
            }

            return (obj);
        }

        private void SelectAlgorithm(AlgorithmType type)
        {
            /* ターゲット別オブジェクト生成 */
            var obj = CreateAlgorithmObject(type);

            /* 実行中ルールの設定をバックアップ */
            if (algorithm_obj_ != null) {
                algorithm_obj_.OnBackupProperty();
            }

            /* コントロールを破棄 */
            Panel_Sub.Controls.Clear();

            /* 新しいルールオブジェクトを設定 */
            prop_.Algorithm.Value = type;
            algorithm_obj_ = obj;

            /* 新しいルールインターフェースを設定 */
            if (algorithm_obj_ != null) {
                algorithm_obj_.Dock = DockStyle.Fill;
                Panel_Sub.Controls.Add(algorithm_obj_);
            }

            /* UIも更新 */
            CBox_AlgorithmList.SelectedItem = type;
        }

        protected override void OnBackupProperty()
        {
            if (CBox_AlgorithmList.SelectedItem is AlgorithmTypeObject obj) {
                prop_.Algorithm.Value = obj.Value;
            }
        }

        protected override void OnInputStatusClear()
        {
        }

        protected override void OnInputPacket(PacketObject input, ref List<PacketObject> output)
        {
            /* アルゴリズムが設定されていないときは無視 */
            if (algorithm_obj_ == null) {
                output.Add(input);
                return;
            }

            /* データパケット以外は無視 */
            if (input.Attribute != PacketAttribute.Data) {
                output.Add(input);
                return;
            }

#if false
            /* 受信パケット以外は無視 */
            if (packet_d.Direction != PacketDirection.Recv) {
                output.Add(input);
                return;
            }
#endif

            /* アルゴリズムオブジェクトに変換を任せる */
            algorithm_obj_.OnInputPacket(input, ref output);
        }

        private void CBox_Exp_SelectedIndexChanged(object sender, EventArgs e)
        {
            var type = AlgorithmType.None;

            if (CBox_AlgorithmList.SelectedIndex >= 0) {
                type = (CBox_AlgorithmList.SelectedItem as AlgorithmTypeObject).Value;
            }

            SelectAlgorithm(type);
        }
    }
}
