using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Contract.Dto
{
    public class Endpoint
    {
        public string Address { get; set; }

        public List<Method> Methods { get; set; }

        public Endpoint()
        {
            Methods = new List<Method>();
        }
    }
}
