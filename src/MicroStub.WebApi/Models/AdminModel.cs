using MicroStub.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.WebApi.Models
{
    public class AdminModel
    {
        public StubConfig Data { get; set; }

        public string Schema { get; set; }
    }
}
