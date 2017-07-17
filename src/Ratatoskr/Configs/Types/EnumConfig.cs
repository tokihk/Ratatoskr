﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ratatoskr.Configs.Types
{
    [Serializable]
    public sealed class EnumConfig<T> : IConfigData<T>, IConfigReader, IConfigWriter
        where T : struct
    {
        public T Value { get; set; }


        public EnumConfig(T value)
        {
            Value = value;
        }

        public bool LoadConfigData(XmlElement xml_own)
        {
            T value;

            if (!Enum.TryParse<T>(xml_own.InnerText, out value))return (false);

            Value = value;

            return (true);
        }

        public bool SaveConfigData(XmlElement xml_own)
        {
            xml_own.InnerText = Enum.GetName(typeof(T), Value);

            return (true);
        }
    }
}