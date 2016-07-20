using MicroStub.Contract.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Contract.Interface
{
    public interface IStubData
    {
        List<Subscriber> GetSubscribers();

        Project GetProject(string subscriberKey, string projectName);
    }
}
