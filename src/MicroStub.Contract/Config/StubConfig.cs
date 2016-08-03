using MicroStub.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Contract.Config
{
    public class StubConfig
    {
        public List<Subscriber> Subscribers { get; set; }

        public StubConfig()
        {
            Subscribers = new List<Dto.Subscriber>();
        }
    }
}
