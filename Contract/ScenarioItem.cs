﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Contract
{
    public class ScenarioItem
    {
        public string Method { get; set; }

        public string QueryString { get; set; }

        public string RequestBody { get; set; }

        public string ContentType { get; set; }

        public string Response { get; set; }
    }
}
