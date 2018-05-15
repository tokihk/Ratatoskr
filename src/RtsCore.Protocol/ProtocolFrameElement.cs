using System;
using System.Collections.Generic;
using System.Text;

namespace RtsCore.Protocol
{
    public enum ProtocolDataErrorLevel
    {
        NoError,
        LevelLow,
        LevelHigh,
    }

    public abstract class ProtocolFrameElement
    {
        private ProtocolFrameElement       parent_ = null;

        private ProtocolBitData            pack_data_ = null;

        private ProtocolBitData            unpack_data_ = null;
        private ProtocolFrameValue         unpack_value_ = null;
        private List<ProtocolFrameElement> unpack_elements_ = new List<ProtocolFrameElement>();


        public ProtocolFrameElement(ProtocolFrameElement parent, string name, uint pack_bitlen, uint unpack_bitlen)
        {
            Name = name;
            PackDataBitLength = pack_bitlen;
            UnpackDataBitLength = unpack_bitlen;

            parent_ = parent;
            if (parent_ != null) {
                parent_.unpack_elements_.Add(this);
            }
        }

        public ProtocolFrameElement Parent
        {
            get { return (parent_); }
        }

        public ProtocolFrameElement RootParent
        {
            get
            {
                var parent = parent_;

                while ((parent != null) && (parent.parent_ != null)) {
                    parent = parent.parent_;
                }

                return (parent);
            }
        }

        public string Name { get; }

        public uint   PackDataBitLength   { get; }
        public uint   UnpackDataBitLength { get; }

        public ProtocolDataErrorLevel ErrorStatus { get; set; } = ProtocolDataErrorLevel.NoError;


        public void Unpack()
        {
            unpack_data_ = new ProtocolBitData(UnpackDataBitLength);
            unpack_value_ = null;
            unpack_elements_ = new List<ProtocolFrameElement>();

            if (pack_data_ != null) {
                OnPackDataToUnpackData(pack_data_, unpack_data_);
            }

            if (unpack_value_ != null) {
                unpack_value_ = OnUnpackDataToUnpackValue(unpack_data_);
                OnUnpackDataToUnpackElements(unpack_data_, unpack_elements_);
            }
        }

        public void UpdateFromUnpackElement()
        {
            pack_data_ = new ProtocolBitData(PackDataBitLength);
            unpack_data_ = new ProtocolBitData(UnpackDataBitLength);
            unpack_value_ = null;

            if (unpack_elements_ != null) {
                OnUnpackElementsToUnpackData(unpack_elements_.ToArray(), unpack_data_);
            }

            unpack_value_ = OnUnpackDataToUnpackValue(unpack_data_);
            OnUnpackDataToPackData(unpack_data_, pack_data_);

            /* 親要素も更新 */
            if (parent_ != null) {
                parent_.UpdateFromUnpackElement();
            }
        }

        private void UpdateFromUnpackData()
        {
            pack_data_ = new ProtocolBitData(PackDataBitLength);
            unpack_value_ = null;
            unpack_elements_ = new List<ProtocolFrameElement>();

            if (unpack_data_ != null) {
                unpack_value_ = OnUnpackDataToUnpackValue(unpack_data_);
                OnUnpackDataToUnpackElements(unpack_data_, unpack_elements_);
                OnUnpackDataToPackData(unpack_data_, pack_data_);
            }

            /* 親要素も更新 */
            if (parent_ != null) {
                parent_.UpdateFromUnpackElement();
            }
        }

        private void UpdateFromUnpackValue()
        {
            pack_data_ = new ProtocolBitData(PackDataBitLength);
            unpack_data_ = new ProtocolBitData(UnpackDataBitLength);
            unpack_elements_ = new List<ProtocolFrameElement>();

            if (unpack_value_ != null) {
                OnUnpackValueToUnpackData(unpack_value_, unpack_data_);
            }

            if (unpack_data_ != null) {
                OnUnpackDataToUnpackElements(unpack_data_, unpack_elements_);
                OnUnpackDataToPackData(unpack_data_, pack_data_);
            }

            /* 親要素も更新 */
            if (parent_ != null) {
                parent_.UpdateFromUnpackElement();
            }
        }

        public ProtocolBitData GetPackData()
        {
            return (pack_data_);
        }

        public ProtocolBitData GetUnpackData()
        {
            return (unpack_data_);
        }

        public ProtocolFrameValue GetUnpackValue()
        {
            return (unpack_value_);
        }

        public ProtocolFrameElement[] GetUnpackElement()
        {
            return (unpack_elements_.ToArray());
        }

        public void SetPackData(byte[] pack_bitdata, uint pack_bitdata_bitlen)
        {
            pack_data_ = new ProtocolBitData(pack_bitdata, Math.Min(PackDataBitLength, pack_bitdata_bitlen));

            Unpack();
        }

        public void SetUnpackData(byte[] unpack_bitdata, uint unpack_bitdata_bitlen)
        {
            unpack_data_ = new ProtocolBitData(unpack_bitdata, Math.Min(UnpackDataBitLength, unpack_bitdata_bitlen));

            UpdateFromUnpackData();
        }

        public void SetUnpackValue(ProtocolFrameValue value)
        {
            if (value == null)return;

            unpack_value_ = value;

            UpdateFromUnpackValue();
        }

        protected virtual void OnPackDataToUnpackData(ProtocolBitData pack_data, ProtocolBitData unpack_data)
        {
            unpack_data.SetBitData(0, pack_data);
        }

        protected virtual ProtocolFrameValue OnUnpackDataToUnpackValue(ProtocolBitData unpack_data)
        {
            return (null);
        }

        protected virtual void OnUnpackDataToUnpackElements(ProtocolBitData unpack_data, List<ProtocolFrameElement> unpack_elements)
        {
        }

        protected virtual void OnUnpackElementsToUnpackData(ProtocolFrameElement[] unpack_elements, ProtocolBitData unpack_data)
        {
        }

        protected virtual void OnUnpackValueToUnpackData(ProtocolFrameValue unpack_value, ProtocolBitData unpack_data)
        {
            unpack_data.SetBitData(0, unpack_value.GetBitData());
        }

        protected virtual void OnUnpackDataToPackData(ProtocolBitData unpack_data, ProtocolBitData pack_data)
        {
            pack_data.SetBitData(0, unpack_data);
        }

        public override string ToString()
        {
            var value = GetUnpackValue();

            return ((value != null) ? (value.ToString()) : (""));
        }
    }
}
