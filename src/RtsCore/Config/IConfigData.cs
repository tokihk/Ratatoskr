﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RtsCore.Config
{
    public interface IConfigData<Type>
    {
        Type Value { get; set; }
    }
}