using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Api
{
    public interface IApiTask : IDisposable
    {
        bool IsRunning { get; }

        void Stop();
    }
}
