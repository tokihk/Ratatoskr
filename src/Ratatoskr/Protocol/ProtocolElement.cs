using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Protocol
{
    internal class ProtocolElement
    {
        private class ChildElement
        {
            public ChildElement(string alias, uint offset)
            {
                Alias = alias;
                Offset = offset;
            }

            public string Alias  { get; }
            public uint   Offset { get; }
        }



        public ProtocolElement(string name, string alias, uint bitlen)
        {
            Name = name;
            Alias = alias;
        }

        public string Name  { get; }
        public string Alias { get; }

        public string PreProcess  { get; }
        public string PostProcess { get; }



    }
}
