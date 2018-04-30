using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Ratatoskr.Plugin;
using RtsCore.Protocol;

namespace Ratatoskr.Protocol
{
    internal class ProtocolDecoderInfo
    {
        private PluginInfo plugin_info_;


        public ProtocolDecoderInfo(PluginInfo plugin_info, ProtocolDecoder decoder)
        {
            plugin_info_ = plugin_info;

            ID = decoder.ID;
            Name = decoder.Name;
            Details = decoder.Details;
        }

        public Guid   ID      { get; }
        public string Name    { get; }
        public string Details { get; }

        public ProtocolDecoder LoadModule()
        {
            return (Activator.CreateInstance(plugin_info_.AssemblyType) as ProtocolDecoder);
        }

        public override string ToString()
        {
            return (Name);
        }

        public override bool Equals(object obj)
        {
            if (obj is ProtocolDecoderInfo) {
                return (((ProtocolDecoderInfo)obj).ID == ID);
            }

            return (base.Equals(obj));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
