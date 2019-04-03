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
        private static List<ProtocolEncoderClass> encoder_list_ = new List<ProtocolEncoderClass>();
        private static List<ProtocolDecoderClass> decoder_list_ = new List<ProtocolDecoderClass>();


        public static ProtocolEncoderClass[] GetEncoderList()
        {
            return (encoder_list_.ToArray());
        }

        public static ProtocolDecoderClass[] GetDecoderList()
        {
            return (decoder_list_.ToArray());
        }

        public static void AddEncoder(ProtocolEncoderClass prec)
        {
            lock (encoder_list_) {
                encoder_list_.Add(prec);
            }
        }

        public static void AddDecoder(ProtocolDecoderClass prdc)
        {
            lock (decoder_list_) {
                decoder_list_.Add(prdc);
            }
        }
    }
}
