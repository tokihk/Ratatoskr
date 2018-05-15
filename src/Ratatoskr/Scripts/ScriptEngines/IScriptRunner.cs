using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Scripts.ScriptEngines
{
    internal delegate void ScriptMessageAppendedHandler(object sender, ScriptMessageData msg);
    internal delegate void ScriptCommentUpdatedHandler(object sender, int line_no, ScriptMessageData msg);


    internal interface IScriptRunner
    {
        bool IsRunning { get; }
        bool IsPause   { get; }

        void RunAsync();

        void PauseAsync(bool pause);
        void Pause(bool pause);

        void StopAsync();
        void Stop();

        void ClearMessage();
        ScriptMessageData[] GetMessageList();

        void ClearComment();
        ScriptMessageData[] GetCommentList();

        event EventHandler                 StatusChanged;
        event ScriptMessageAppendedHandler MessageAppended;
        event ScriptCommentUpdatedHandler  CommentUpdated;
    }
}
