using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ratatoskr.Gate;

namespace Ratatoskr.Scripts.API
{
    public static class API_CommentOut
    {
        public static void Exec(string text)
        {
            GatePacketManager.SetComment(text);
        }
    }
}
