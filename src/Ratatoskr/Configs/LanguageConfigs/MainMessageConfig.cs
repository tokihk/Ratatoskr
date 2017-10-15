using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ratatoskr.Configs;
using Ratatoskr.Configs.Types;

namespace Ratatoskr.Configs.LanguageConfigs
{
    internal sealed class MainMessageConfig : ConfigHolder
    {
        public StringConfig PacketNowPreprocessing { get; } = new StringConfig("During packet processing.");
        public StringConfig PacketNowDrawing       { get; } = new StringConfig("In the packet drawing.");

        public StringConfig EventFileLoading       { get; } = new StringConfig("Loading packet file.");
        public StringConfig EventFileLoadComplete  { get; } = new StringConfig("Read completion of packet file");

        public StringConfig EventFileSaving        { get; } = new StringConfig("During storage in the packet file.");
        public StringConfig EventFileSaveComplete  { get; } = new StringConfig("Output completion to the packet file.");

        public StringConfig TimeStampManual        { get; } = new StringConfig("TimeStamp - Manual");
        public StringConfig TimeStampAuto          { get; } = new StringConfig("TimeStamp - Auto");
    }
}
