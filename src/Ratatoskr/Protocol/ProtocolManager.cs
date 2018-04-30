using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Plugin;
using RtsCore.Protocol;

namespace Ratatoskr.Protocol
{
    internal static class ProtocolManager
    {
        private static List<ProtocolDecoderInfo> decoder_list_ = new List<ProtocolDecoderInfo>();


        public static ProtocolDecoderInfo[] DecoderList
        {
            get
            {
                return (decoder_list_.ToArray());
            }
        }

        public static void LoadFromPlugin(PluginInfo info)
        {
            if (info == null)return;

            if (!info.AssemblyType.IsSubclassOf(typeof(RtsCore.Protocol.ProtocolDecoder)))return;

            var decoder = Activator.CreateInstance(info.AssemblyType) as ProtocolDecoder;

            if (decoder == null)return;

            /* 重複しているデコーダーは削除 */
            decoder_list_.RemoveAll(item => item.ID == decoder.ID);

            decoder_list_.Add(new ProtocolDecoderInfo(info, decoder));
        }
    }
}
