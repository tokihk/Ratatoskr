﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtsCore.Framework.FileFormat
{
    public abstract class FileFormatOption
    {
        public abstract FileFormatOptionEditor GetEditor();
    }
}