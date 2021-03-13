using System;
using System.Collections.Generic;
using System.Text;
using Ratatoskr.General;

namespace RtsCore.Protocol
{
    public enum ProtocolFrameStatus
    {
        Normal,
        Error,
    }

    public abstract class ProtocolFrameElement
    {
        private BitData                    pack_data_ = null;
        private BitData                    unpack_data_ = null;
        private List<ProtocolFrameElement> unpack_elements_ = null;


        public ProtocolFrameElement(ProtocolFrameElement parent, string name, uint pack_bitlen)
        {
            Parent = parent;
            Name = name;
            PackDataBitLength = pack_bitlen;

            pack_data_ = new BitData(PackDataBitLength);
            unpack_data_ = pack_data_;
            unpack_elements_ = null;

            if (Parent != null) {
                Parent.AddUnpackElement(this);
            }
        }

        public ProtocolFrameElement Parent { get; }

        public string Name { get; }

        public uint   PackDataBitLength { get; }

        public ProtocolFrameStatus Status { get; set; } = ProtocolFrameStatus.Normal;

        public virtual double GetFrameTickMsec()
        {
            return (0.0f);
        }

        public bool UpdateFromPackData()
        {
            if (pack_data_ == null)return (false);

            unpack_data_ = null;
            unpack_elements_ = null;

            /* Pack BitData -> Unpack BitData */
            if (pack_data_ != null) {
                if (!OnPackDataToUnpackData(pack_data_, ref unpack_data_))return (false);
            }

            /* Unpack BitData -> Unpack Elements */
            if (unpack_data_ != null) {
                if (!OnUnpackDataToUnpackElements(unpack_data_))return (false);
            }

            return (true);
        }

        public bool UpdateFromUnpackData()
        {
            if (unpack_data_ == null)return (false);

            pack_data_ = null;
            unpack_elements_ = null;

            if (unpack_data_ != null) {
                if (!OnUnpackDataToUnpackElements(unpack_data_))return (false);

                if (!OnUnpackDataToPackData(unpack_data_, ref pack_data_))return (false);
            }

            return (true);
        }

        public bool UpdateFromUnpackElements()
        {
            if (unpack_elements_ == null)return (false);

            pack_data_ = null;
            unpack_data_ = null;

            if (unpack_elements_ != null) {
                /* 下層全ての要素を更新 */
                unpack_elements_.ForEach(elem => elem.UpdateFromUnpackElements());

                if (!OnUnpackElementsToUnpackData(unpack_elements_, ref unpack_data_))return (false);
            }

            if (unpack_data_ != null) {
                if (!OnUnpackDataToPackData(unpack_data_, ref pack_data_))return (false);
            }

            return (true);
        }

        public BitData GetPackData()
        {
            return (pack_data_);
        }

        public BitData GetUnpackData()
        {
            return (unpack_data_);
        }

        public object GetUnpackValue()
        {
            var value = (object)null;

            if (!OnUnpackDataToUnpackValue(unpack_data_, ref value)) {
                value = null;
            }

            return (value);
        }

        public IEnumerable<ProtocolFrameElement> GetUnpackElements()
        {
            return ((unpack_elements_ != null) ? (unpack_elements_.ToArray()) : (null));
        }

        public bool SetPackData(BitData pack_data)
        {
            pack_data_ = new BitData(pack_data.Data, Math.Min(PackDataBitLength, pack_data.Length));

            return (UpdateFromPackData());
        }

        public bool SetUnpackData(BitData unpack_data)
        {
            unpack_data_ = unpack_data;

            return (UpdateFromUnpackData());
        }

        public bool SetUnpackValue(object unpack_value)
        {
            if (unpack_value == null)return (false);

            unpack_data_ = null;

            if (!OnUnpackValueToUnpackData(unpack_value, ref unpack_data_))return (false);

            return (UpdateFromUnpackData());
        }

        private void AddUnpackElement(ProtocolFrameElement prfe)
        {
            if (unpack_elements_ == null) {
                unpack_elements_ = new List<ProtocolFrameElement>();
            }
            unpack_elements_.Add(prfe);
        }

        protected virtual bool OnPackDataToUnpackData(BitData pack_data, ref BitData unpack_data)
        {
            unpack_data = pack_data;

            return (true);
        }

        protected virtual bool OnUnpackValueToUnpackData(object unpack_value, ref BitData unpack_data)
        {
            return (true);
        }

        protected virtual bool OnUnpackDataToUnpackElements(BitData unpack_data)
        {
            return (true);
        }

        protected virtual bool OnUnpackElementsToUnpackData(IEnumerable<ProtocolFrameElement> unpack_elements, ref BitData unpack_data)
        {
            var unpack_bitdata_list = new List<BitData>();
            var unpack_bitdata_length = (uint)0;
            var unpack_bitdata_offset = (uint)0;
            var unpack_bitdata = (BitData)null;

            foreach (var elem in unpack_elements) {
                unpack_bitdata = elem.GetUnpackData();

                if (unpack_bitdata != null) {
                    unpack_bitdata_list.Add(unpack_bitdata);
                    unpack_bitdata_length += unpack_bitdata.Length;
                }
            }

            unpack_data = new BitData(unpack_bitdata_length);

            foreach (var bitdata in unpack_bitdata_list) {
                unpack_data.SetBitData(unpack_bitdata_offset, bitdata);
                unpack_bitdata_offset += bitdata.Length;
            }

            return (true);
        }

        protected virtual bool OnUnpackDataToUnpackValue(BitData unpack_data, ref object unpack_value)
        {
            unpack_value = unpack_data;

            return (true);
        }

        protected virtual bool OnUnpackDataToPackData(BitData unpack_data, ref BitData pack_data)
        {
            pack_data = unpack_data_;

            return (true);
        }

        public override string ToString()
        {
            var unpack_value = GetUnpackValue();

            return ((unpack_value != null) ? (unpack_value.ToString()) : ("Unknown"));
        }
    }
}
