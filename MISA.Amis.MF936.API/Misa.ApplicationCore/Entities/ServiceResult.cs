﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Entities
{
    public class ServiceResult
    {
        public bool IsValid { get; set; } = true;

        public object Data { get; set; }
    }
}
