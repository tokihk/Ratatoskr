﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config.Types;
using Ratatoskr.PacketView;
using Ratatoskr.General;

namespace Ratatoskr.PacketView.Sequential
{
    internal enum DrawDataType
    {
        ASCII,
        ShiftJIS,
        UTF8,
        HEX,
        BIN,
    }

    internal class PacketViewPropertyImpl : PacketViewProperty
    {
        public IntegerConfig            ShiftBit       { get; } = new IntegerConfig(0);
        public StringConfig             EndLinePattern { get; } = new StringConfig("");
        public BoolConfig               EchoBack       { get; } = new BoolConfig(false);

        public EnumConfig<DrawDataType> DrawType       { get; } = new EnumConfig<DrawDataType>(DrawDataType.UTF8);
        public StringConfig             BoundaryText   { get; } = new StringConfig(" ");


        public override PacketViewProperty Clone()
        {
            return (ClassUtil.Clone<PacketViewPropertyImpl>(this));
        }
    }
}
