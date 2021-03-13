using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Config;
using Ratatoskr.Config.Types;

namespace Ratatoskr.Config.Data.User
{
    [Serializable]
    public sealed class OptionConfig : ConfigHolder
    {

        private StringListConfig CustomConvertAlgorithm { get; } = new StringListConfig();

//        public StringConfig ExtPath_USBPcapCMD { get; } = new StringConfig("C:\\Program Files\\USBPcap\\USBPcapCMD.exe");
    }
}
