﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solucao.Application.Utils
{
    public class ApplicationError : Exception
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
