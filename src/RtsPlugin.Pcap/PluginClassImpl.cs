﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RtsCore.Framework.Device;
using RtsCore.Framework.FileFormat;
using RtsCore.Framework.Plugin;
using RtsCore.Protocol;
using RtsCore.Utility;

namespace RtsPlugin.Pcap
{
    public class PluginClassImpl : PluginClass
    {
        public PluginClassImpl() : base(Guid.Parse("82194695-A04C-4548-9CB3-C9A5F1638358"))
        {
        }

        public override string Name
        {
            get { return ("Pcap"); }
        }

        public override string Details
        {
            get { return ("Pcap Extention"); }
        }

        public override string Copyright
        {
            get { return ("2019-2020 Toki.H.K"); }
        }

        public override ModuleVersion Version
        {
            get { return (new ModuleVersion(1, 0, 0, "")); }
        }

        public override LicenseInfo[] ThirdPartyLicenses
        {
            get
            {
                return (new [] {
                    new LicenseInfo("SharpPcap", "https://github.com/chmorgan/sharppcap", "GNU Lesser General Public License v3.0", ""),
                    new LicenseInfo("PacketDotNet", "https://github.com/chmorgan/packetnet", "GNU Lesser General Public License v3.0", "")
                });
            }
        }

        protected override PluginInstance OnCreateInstance()
        {
            return (new PluginInstanceImpl(this));
        }

        protected override PluginProperty OnCreateProperty()
        {
            return (new PluginPropertyImpl());
        }
    }
}
