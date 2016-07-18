using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Contract
{
    public interface IStubService
    {
        bool SubscriberExists(string key, string secret);

        Method GetMethod(RequestInfo requestInfo);
    }
}
