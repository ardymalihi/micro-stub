using MicroStub.Contract.Config;
using MicroStub.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Contract.Interface
{
    public interface IStubData
    {
        StubConfig MicroStubConfig { get; set; }
        Subscriber GetSubscriber(string subscriberKey, string subscriberSecret);
        void Save(StubConfig stubConfig, string fileName);
    }
}
