using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ratatoskr.Config
{
    public interface IConfigRoot
    {
        bool LoadFromStream(Stream stream);

        bool LoadConfig(string path);

        bool SaveToStream(Stream stream);

        bool SaveConfig(string path);
    }
}
