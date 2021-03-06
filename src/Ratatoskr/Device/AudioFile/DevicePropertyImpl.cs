﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config.Types;
using Ratatoskr.General;

namespace Ratatoskr.Device.AudioFile
{
    internal enum ConnectActionType
    {
        Restart,
        Continue,
    }

    [Serializable]
    internal sealed class DevicePropertyImpl : DeviceProperty
    {
        public StringConfig  InputFilePath { get; } = new StringConfig("");
        public IntegerConfig InputSamplingRate  { get; } = new IntegerConfig(8000);
        public IntegerConfig InputBitsPerSample { get; } = new IntegerConfig(8);
        public IntegerConfig InputChannelNum    { get; } = new IntegerConfig(1);

        public IntegerConfig                 RepeatCount   { get; } = new IntegerConfig(1);
        public EnumConfig<ConnectActionType> ConnectAction { get; } = new EnumConfig<ConnectActionType>(ConnectActionType.Restart);


        public override DeviceProperty Clone()
        {
            return (ClassUtil.Clone<DevicePropertyImpl>(this));
        }

        public override string GetStatusString()
        {
            return (String.Format(
                "{0:G}(Connect:{1:G},Repeat:{2:G})",
                InputFilePath.Value,
                ConnectAction.Value.ToString(),
                RepeatCount.Value));
        }

        public override DevicePropertyEditor GetPropertyEditor()
        {
            return (new DevicePropertyEditorImpl(this));
        }
    }
}
