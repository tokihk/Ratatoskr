using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ratatoskr.Configs;
using Ratatoskr.Forms;
using Ratatoskr.FileFormats;
using Ratatoskr.Gate;
using Ratatoskr.Scripts.PacketFilterExp.Terms;

namespace Ratatoskr.Actions.ActionModules
{
    internal sealed class Action_PlayRecord : ActionObject
    {
        private const int SEND_TIMMING_MARGIN = -3;

        public enum Argument
        {
            Path,
            Filter,
        }

        public enum ArgumentFilter
        {
            RecvDataOnly,
            SendDataOnly,
            Both,
        }

        public enum Result
        {
            State,
        }


        private IAsyncResult ar_task_ = null;

        private bool         exit_req_ = false;


        public Action_PlayRecord()
        {
            RegisterArgument(Argument.Path.ToString(), typeof(string), null);
            RegisterArgument(Argument.Filter.ToString(), typeof(string), ArgumentFilter.RecvDataOnly.ToString());
        }

        public Action_PlayRecord(string path, string target) : this()
        {
            SetArgumentValue(Argument.Path.ToString(), path);
            SetArgumentValue(Argument.Filter.ToString(), target);
        }

        protected override void OnExecStart()
        {
            ar_task_ = (new ExecTaskDelegate(delegate ()
            {
                while (!exit_req_) {

                }
            })).BeginInvoke(null, null);
        }

        protected override void OnExecPoll()
        {
            if (!ar_task_.IsCompleted)return;

            SetResult(ActionResultType.Success, null);
        }

        private delegate void ExecTaskDelegate();
        private void ExecTask()
        {
        }

#if false
        private delegate PacketLogReader LoadPacketLogReaderDelegate(string path);
        private PacketLogReader LoadPacketLogReader(string path)
        {
            if (FormUiManager.InvokeRequired()) {
                return (FormUiManager.Invoke(new LoadPacketLogReaderDelegate(LoadPacketLogReader), path) as PacketLogReader);
            }

//            var reader = FileManager.PacketOpen.SelectReaderFromPath(path, typeof(IPacketLogReader));

//            if (format == null)return (null);

//            var reader = format.GetReader();
//            var reader_p = reader.reader as PacketLogReader;

//            if (reader_p == null)return (null);

//            if (!reader_p.Open(reader.option, path))return (null);

            return (reader_p);
        }
#endif
    }
}
