﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Generic;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.Devices.SerialPort
{
    internal enum fDtrControlType
    {
        DTR_CONTROL_DISABLE,
        DTR_CONTROL_ENABLE,
        DTR_CONTROL_HANDSHAKE,
    }

    internal enum fRtsControlType
    {
        RTS_CONTROL_DISABLE,
        RTS_CONTROL_ENABLE,
        RTS_CONTROL_HANDSHAKE,
        RTS_CONTROL_TOGGLE,
    }

    [Serializable]
    internal sealed class DevicePropertyImpl : DeviceProperty
    {
        public StringConfig         PortName { get; } = new StringConfig("COM1");

        public IntegerConfig        BaudRate { get; } = new IntegerConfig(9600);
        public EnumConfig<Parity>   Parity   { get; } = new EnumConfig<System.IO.Ports.Parity>(System.IO.Ports.Parity.None);
        public IntegerConfig        DataBits { get; } = new IntegerConfig(8);
        public EnumConfig<StopBits> StopBits { get; } = new EnumConfig<System.IO.Ports.StopBits>(System.IO.Ports.StopBits.None);

        public BoolConfig           fOutxCtsFlow      { get; } = new BoolConfig(false);
        public BoolConfig           fOutxDsrFlow      { get; } = new BoolConfig(false);
        public BoolConfig           fDsrSensitivity   { get; } = new BoolConfig(false);
        public BoolConfig           fTXContinueOnXoff { get; } = new BoolConfig(false);
        public BoolConfig           fOutX             { get; } = new BoolConfig(false);
        public BoolConfig           fInX              { get; } = new BoolConfig(false);

        public EnumConfig<fDtrControlType>  fDtrControl { get; } = new EnumConfig<fDtrControlType>(fDtrControlType.DTR_CONTROL_DISABLE);
        public EnumConfig<fRtsControlType>  fRtsControl { get; } = new EnumConfig<fRtsControlType>(fRtsControlType.RTS_CONTROL_DISABLE);

        public IntegerConfig        XonLim   { get; } = new IntegerConfig(0);
        public IntegerConfig        XoffLim  { get; } = new IntegerConfig(0);
        public IntegerConfig        XonChar  { get; } = new IntegerConfig(0x11);
        public IntegerConfig        XoffChar { get; } = new IntegerConfig(0x13);


        public override DeviceProperty Clone()
        {
            return (ClassUtil.Clone<DevicePropertyImpl>(this));
        }

        public override string GetStatusString()
        {
            return (String.Format(
                "{0:G}:({1:G},{2:G},{3:G},{4:G})",
                PortName.Value,
                BaudRate.Value,
                System.Enum.GetName(typeof(Parity), Parity.Value).ToString(),
                DataBits.Value,
                System.Enum.GetName(typeof(StopBits), StopBits.Value)));
        }

        public override DevicePropertyEditor GetPropertyEditor()
        {
            return (new DevicePropertyEditorImpl(this));
        }
    }
}