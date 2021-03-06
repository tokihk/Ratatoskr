﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.PacketConverter;

namespace Ratatoskr.PacketConverter.Filter
{
    internal sealed class PacketConverterClassImpl : PacketConverterClass
    {
        public static readonly Guid ClassID = new Guid("EFAEEC7A-DFBD-4BF2-B3D8-DF2B58A234AC");


        public PacketConverterClassImpl() : base(ClassID)
        {
        }

        public override string Name
        {
            get { return ("Filter"); }
        }

        public override string Details
        {
            get { return ("Passes arbitrary packets only."); }
        }

        public override Type GetPropertyType()
        {
            return (typeof(PacketConverterPropertyImpl));
        }

        public override PacketConverterProperty CreateProperty()
        {
            return (new PacketConverterPropertyImpl());
        }

        protected override PacketConverterInstance OnCreateInstance(PacketConvertManager pcvtm, Guid obj_id, PacketConverterProperty pcvtp)
        {
            return (new PacketConverterInstanceImpl(pcvtm, this, pcvtp, obj_id));
        }
    }
}
