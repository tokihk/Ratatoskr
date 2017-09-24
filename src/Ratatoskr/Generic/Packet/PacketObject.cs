using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ratatoskr.Generic.Packet
{
    internal enum PacketElementID
    {
        Facility,
        Alias,
        Priority,
        Attribute,
        DateTime,
        Direction,
        Information,
        Source,
        Destination,
        Mark,
        Data,
    }

    internal enum PacketFacility : byte
    {
        System,
        View,
        Device,
    }

    internal enum PacketPriority : byte
    {
        Debug,
        Standard,
        Notice,
        Warning,
        Error,
        Critical,
        Alert,
        Emergency,
    }

    internal enum PacketAttribute : byte
    {
        Control,
        Message,
        Data,
    }

    internal enum PacketDirection
    {
        Recv,
        Send,
    }

    [Serializable]
    internal abstract class PacketObject
    {
        public PacketObject(
                    PacketFacility facility,
                    string alias,
                    PacketPriority prio,
                    PacketAttribute attr,
                    DateTime dt,
                    string info,
                    PacketDirection dir,
                    string src,
                    string dst,
                    byte mark)
        {
            Facility = facility;
            Alias = alias;
            Priority = prio;
            Attribute = attr;
            MakeTime = dt;
            Information = info;
            Direction = dir;
            Source = src;
            Destination = dst;
            UserMark = mark;
        }

        public PacketObject(PacketObject packet)
            : this(
                packet.Facility,
                packet.Alias,
                packet.Priority,
                packet.Attribute,
                packet.MakeTime,
                packet.Information,
                packet.Direction,
                packet.Source,
                packet.Destination,
                packet.UserMark)
        {
            
        }

        public PacketObject() { }


        public PacketFacility   Facility    { get; }

        public string           Alias       { get; set; }

        public PacketPriority   Priority    { get; }
        public PacketAttribute  Attribute   { get; }
        public DateTime         MakeTime    { get; }

        public string           Information { get; set; }
        public PacketDirection  Direction   { get; }
        public string           Source      { get; }
        public string           Destination { get; }

        public byte             UserMark    { get; set; }

        public abstract byte[] GetData();
        public abstract int    GetDataSize();


        public override bool Equals(object obj)
        {
            if (obj is PacketObject) {
                return (this == (PacketObject)obj);
            } else {
                return (base.Equals(obj));
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool AttributeCompare(PacketObject obj)
        {
            return (   (Facility == obj.Facility)
                    && (Alias == obj.Alias)
                    && (Priority == obj.Priority)
                    && (Attribute == obj.Attribute)
                    && (Direction == obj.Direction)
                    && (Source == obj.Source)
                    && (Destination == obj.Destination)
                    );
        }

        public virtual string GetElementText(PacketElementID id)
        {
            switch (id) {
                case PacketElementID.Facility:
                {
                    return (Facility.ToString());
                }

                case PacketElementID.Alias:
                {
                    return (Alias);
                }

                case PacketElementID.Priority:
                {
                    return (Priority.ToString());
                }

                case PacketElementID.Attribute:
                {
                    return (Attribute.ToString());
                }

                case PacketElementID.DateTime:
                {
                    return (MakeTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                }

                case PacketElementID.Information:
                {
                    return (Information);
                }

                case PacketElementID.Direction:
                {
                    return (Direction.ToString());
                }

                case PacketElementID.Source:
                {
                    return (Source);
                }

                case PacketElementID.Destination:
                {
                    return (Destination);
                }

                case PacketElementID.Mark:
                {
                    return (UserMark.ToString());
                }

                default:
                    return ("");
            }
        }

        public static string GetCsvHeaderString()
        {
            var str = new StringBuilder();

            foreach (var id in Enum.GetNames(typeof(PacketElementID))) {
                str.Append(id.ToString());
                str.Append(',');
            }

            if (str.Length > 0) {
                str.Remove(str.Length - 1, 1);
            }

            return (str.ToString());
        }

        public string GetCsvDataString()
        {
            var str = new StringBuilder();

            foreach (PacketElementID id in Enum.GetValues(typeof(PacketElementID))) {
                str.Append(GetElementText(id));
                str.Append(',');
            }

            if (str.Length > 0) {
                str.Remove(str.Length - 1, 1);
            }

            return (str.ToString());
        }

    }
}
