using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Contract
{
    public class RequestInfo
    {
        public string Key { get; set; }

        public string Method { get; set; }

        public string RequestBody { get; set; }

        public string Secret { get; set; }

        public string Project { get; set; }

        public string Endpoint { get; set; }

        public string QueryString { get; set; }
    }
}
