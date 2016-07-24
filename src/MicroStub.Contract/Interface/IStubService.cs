using MicroStub.Contract.Dto;
using MicroStub.Contract.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Contract.Interface
{
    public interface IStubService
    {
        Subscriber GetSubscriber(string key, string secret);

        Method GetMethod(RequestInfo requestInfo);
    }
}
