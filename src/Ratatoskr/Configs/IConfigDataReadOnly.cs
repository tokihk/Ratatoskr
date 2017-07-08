using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ratatoskr.Configs
{
    public interface IConfigDataReadOnly<Type>
    {
        Type Value { get; }
    }
}
