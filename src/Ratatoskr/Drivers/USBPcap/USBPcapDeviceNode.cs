using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Drivers.USBPcap
{
    internal class USBPcapDeviceNode
    {
        private List<USBPcapDeviceNode> nodes_ = null;


        public USBPcapDeviceNode(string devname, string hid)
        {
            DeviceName = devname;
            HardwareID = hid;
        }

        public string DeviceName { get; }
        public string HardwareID { get; }

        public IEnumerable<USBPcapDeviceNode> Nodes
        {
            get { return ((nodes_ != null) ? (nodes_.ToArray()) : (new USBPcapDeviceNode[] { })); }
        }

        public void ClearNode()
        {
            nodes_ = null;
        }

        public void AddNode(USBPcapDeviceNode node)
        {
            if (nodes_ == null) {
                nodes_ = new List<USBPcapDeviceNode>();
            }

            nodes_.Add(node);
        }

        public override int GetHashCode()
        {
            return (base.GetHashCode());
        }

        public override bool Equals(object obj)
        {
            var obj_c = obj as string;

            if (obj_c != null) {
                return (obj_c == DeviceName);
            }

            return (base.Equals(obj));
        }

        public override string ToString()
        {
            return (HardwareID);
        }
    }
}
