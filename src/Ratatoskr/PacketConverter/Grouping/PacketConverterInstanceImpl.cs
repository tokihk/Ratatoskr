﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Forms;
using Ratatoskr.PacketConverter;
using Ratatoskr.General.Packet;

namespace Ratatoskr.PacketConverter.Grouping
{
    internal sealed class PacketConverterInstanceImpl : PacketConverterInstance
    {
        private sealed class RuleTypeObject
        {
            public RuleType Value { get; }


            public RuleTypeObject(RuleType type)
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
                    case RuleType.None:
                        return ("No convert");
                    case RuleType.DataContentsMatch:
                        return ("Data patterns match");
                    case RuleType.DataLengthMatch:
                        return ("Data size");
                    case RuleType.MSecPassingFromFirstPacket:
                        return ("Interval");
                    case RuleType.MSecPassingFromPrevPacket:
                        return ("Receive timeout");
                    default:
                        return (Value.ToString());
                }
            }
        }


        private PacketConverterPropertyImpl prop_;
        private ConvertMethodClass rule_obj_ = null;

        private PacketObject packet_last_ = null;

        private List<string> alias_list_ = new List<string>();
        private RoundComboBox CBox_TargetList;
        private Button Btn_Option;
        private Panel Panel_Sub;

        private ContextMenuStrip  CMenu_Option;
        private ToolStripMenuItem CMenu_DirectionChangeDivide;
        private ToolStripMenuItem CMenu_EventDetectDivide;
        private ToolStripMenuItem CMenu_EachAlias;
        private ToolStripMenuItem CMenu_EachDirection;
        private ToolStripSeparator toolStripSeparator1;
        private System.ComponentModel.IContainer components;


        public PacketConverterInstanceImpl() : base()
        {
            InitializeComponent();
        }

        public PacketConverterInstanceImpl(PacketConvertManager pcvtm, PacketConverterClass pcvtd, PacketConverterProperty pcvtp, Guid id)
            : base(pcvtm, pcvtd, pcvtp, id)
        {
            prop_ = Property as PacketConverterPropertyImpl;

            InitializeComponent();
            InitializeTargetList();

            CMenu_EventDetectDivide.Checked = prop_.Global_DivideByEventDetect.Value;
            CMenu_DirectionChangeDivide.Checked = prop_.Local_DivideByDirectionChange.Value;
            CMenu_EachAlias.Checked = prop_.Local_EachAlias.Value;
            CMenu_EachDirection.Checked = prop_.Local_EachDirection.Value;

            SelectRule(prop_.Rule.Value);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Btn_Option = new System.Windows.Forms.Button();
            this.CBox_TargetList = new Ratatoskr.Forms.RoundComboBox();
            this.Panel_Sub = new System.Windows.Forms.Panel();
            this.CMenu_Option = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CMenu_EventDetectDivide = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CMenu_DirectionChangeDivide = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_EachAlias = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_EachDirection = new System.Windows.Forms.ToolStripMenuItem();
            this.CMenu_Option.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Option
            // 
            this.Btn_Option.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Btn_Option.Location = new System.Drawing.Point(3, 3);
            this.Btn_Option.Name = "Btn_Option";
            this.Btn_Option.Size = new System.Drawing.Size(75, 20);
            this.Btn_Option.TabIndex = 3;
            this.Btn_Option.Text = "Option";
            this.Btn_Option.UseVisualStyleBackColor = true;
            this.Btn_Option.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Btn_Option_MouseClick);
            // 
            // CBox_TargetList
            // 
            this.CBox_TargetList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBox_TargetList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CBox_TargetList.FormattingEnabled = true;
            this.CBox_TargetList.Location = new System.Drawing.Point(84, 3);
            this.CBox_TargetList.Name = "CBox_TargetList";
            this.CBox_TargetList.Size = new System.Drawing.Size(220, 20);
            this.CBox_TargetList.TabIndex = 2;
            this.CBox_TargetList.SelectedIndexChanged += new System.EventHandler(this.CBox_TargetList_SelectedIndexChanged);
            // 
            // Panel_Sub
            // 
            this.Panel_Sub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Sub.Location = new System.Drawing.Point(307, 1);
            this.Panel_Sub.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Sub.Name = "Panel_Sub";
            this.Panel_Sub.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.Panel_Sub.Size = new System.Drawing.Size(323, 25);
            this.Panel_Sub.TabIndex = 1;
            // 
            // CMenu_Option
            // 
            this.CMenu_Option.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CMenu_EventDetectDivide,
            this.toolStripSeparator1,
            this.CMenu_DirectionChangeDivide,
            this.CMenu_EachAlias,
            this.CMenu_EachDirection});
            this.CMenu_Option.Name = "contextMenuStrip1";
            this.CMenu_Option.Size = new System.Drawing.Size(305, 98);
            // 
            // CMenu_EventDetectDivide
            // 
            this.CMenu_EventDetectDivide.Name = "CMenu_EventDetectDivide";
            this.CMenu_EventDetectDivide.Size = new System.Drawing.Size(304, 22);
            this.CMenu_EventDetectDivide.Text = "Forced split at event";
            this.CMenu_EventDetectDivide.Click += new System.EventHandler(this.OptionMenu_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(301, 6);
            // 
            // CMenu_DirectionChangeDivide
            // 
            this.CMenu_DirectionChangeDivide.Name = "CMenu_DirectionChangeDivide";
            this.CMenu_DirectionChangeDivide.Size = new System.Drawing.Size(304, 22);
            this.CMenu_DirectionChangeDivide.Text = "Forced output when data direction changes";
            this.CMenu_DirectionChangeDivide.Click += new System.EventHandler(this.OptionMenu_Click);
            // 
            // CMenu_EachAlias
            // 
            this.CMenu_EachAlias.Name = "CMenu_EachAlias";
            this.CMenu_EachAlias.Size = new System.Drawing.Size(304, 22);
            this.CMenu_EachAlias.Text = "Process for each Alias";
            this.CMenu_EachAlias.Click += new System.EventHandler(this.OptionMenu_Click);
            // 
            // CMenu_EachDirection
            // 
            this.CMenu_EachDirection.Name = "CMenu_EachDirection";
            this.CMenu_EachDirection.Size = new System.Drawing.Size(304, 22);
            this.CMenu_EachDirection.Text = "Process for each Direction";
            this.CMenu_EachDirection.Click += new System.EventHandler(this.OptionMenu_Click);
            // 
            // PacketConverterInstanceImpl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.Btn_Option);
            this.Controls.Add(this.CBox_TargetList);
            this.Controls.Add(this.Panel_Sub);
            this.Name = "PacketConverterInstanceImpl";
            this.Size = new System.Drawing.Size(630, 30);
            this.CMenu_Option.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void InitializeTargetList()
        {
            CBox_TargetList.BeginUpdate();
            {
                foreach (RuleType type in Enum.GetValues(typeof(RuleType))) {
                    CBox_TargetList.Items.Add(new RuleTypeObject(type));
                }
            }
            CBox_TargetList.EndUpdate();
        }

        private ConvertMethodClass CreateRuleObject(RuleType type)
        {
            var obj = (ConvertMethodClass)null;

            switch (type) {
                case RuleType.DataContentsMatch:
                    obj = new DataContentsMatch.ConvertMethodClassImpl(this, prop_);
                    break;

                case RuleType.DataLengthMatch:
                    obj = new DataLengthMatch.ConvertMethodClassImpl(this, prop_);
                    break;

                case RuleType.MSecPassingFromFirstPacket:
                    obj = new MSecPassingFromFirstPacket.ConvertMethodClassImpl(this, prop_);
                    break;

                case RuleType.MSecPassingFromPrevPacket:
                    obj = new MSecPassingFromPrevPacket.ConvertMethodClassImpl(this, prop_);
                    break;
            }

            return (obj);
        }

        private void SelectRule(RuleType type)
        {
            /* ターゲット別オブジェクト生成 */
            var obj = CreateRuleObject(type);

            /* 実行中ルールの設定をバックアップ */
            if (rule_obj_ != null) {
                rule_obj_.BackupProperty();
            }

            /* コントロールを破棄 */
            Panel_Sub.Controls.Clear();

            /* 新しいルールオブジェクトを設定 */
            prop_.Rule.Value = type;
            rule_obj_ = obj;

            /* 新しいルールインターフェースを設定 */
            if (rule_obj_ != null) {
                rule_obj_.Dock = DockStyle.Fill;
                Panel_Sub.Controls.Add(rule_obj_);
            }

            /* UIも更新 */
            CBox_TargetList.SelectedItem = type;

            UpdateConvertStatus();
        }

        protected override void OnBackupProperty()
        {
            prop_.Global_DivideByEventDetect.Value = CMenu_EventDetectDivide.Checked;
            prop_.Local_DivideByDirectionChange.Value = CMenu_DirectionChangeDivide.Checked;
            prop_.Local_EachAlias.Value = CMenu_EachAlias.Checked;
            prop_.Local_EachDirection.Value = CMenu_EachDirection.Checked;
        }

        protected override void OnInputStatusClear()
        {
            if (rule_obj_ == null)return;

            packet_last_ = null;

            rule_obj_.InputStatusClear();
        }

        protected override void OnInputPacket(PacketObject input, ref List<PacketObject> output)
        {
            /* ルールが設定されていないときはスルー */
            if (rule_obj_ == null) {
                output.Add(input);
                return;
            }

            /* イベント検出時に強制分割 */
            if (   (prop_.Global_DivideByEventDetect.Value)
                && (input.Attribute == PacketAttribute.Message)
            ) {
                rule_obj_.InputBreak(ref output);
            }

            /* データパケット以外はスルー */
            if (input.Attribute != PacketAttribute.Data) {
                output.Add(input);
                return;
            }

            /* ターゲットオブジェクトに変換を任せる */
            rule_obj_.InputPacket(input, ref output);

            /* 最終処理パケットを記憶 */
            packet_last_ = input;
        }

        protected override void OnInputBreak(ref List<PacketObject> output)
        {
            /* ルールが設定されていないときは無視 */
            if (rule_obj_ == null)return;

            /* ターゲットオブジェクトに変換を任せる */
            rule_obj_.InputBreak(ref output);
        }

        protected override void OnInputPoll(ref List<PacketObject> output)
        {
            /* ルールが設定されていないときは無視 */
            if (rule_obj_ == null)return;

            /* ターゲットオブジェクトに変換を任せる */
            rule_obj_.InputPoll(ref output);
        }

        private void CBox_TargetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var type = RuleType.None;

            if (CBox_TargetList.SelectedIndex >= 0) {
                type = (CBox_TargetList.SelectedItem as RuleTypeObject).Value;
            }

            SelectRule(type);
        }

        private void Btn_Option_MouseClick(object sender, MouseEventArgs e)
        {
            CMenu_Option.Show(
                PointToScreen(
                    new Point(
                        Location.X,
                        Location.Y + Size.Height)));
        }

        private void OptionMenu_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem menu) {
                menu.Checked = !menu.Checked;

                BackupProperty();

                InputStatusClear();

                UpdateConvertStatus();
            }
        }
    }
}
