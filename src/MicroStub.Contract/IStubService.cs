using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Contract
{
    public interface IStubService
    {
        bool Exists(string key, string secret);

        ScenarioItem GetScenarioItem(RequestInfo requestInfo);
    }
}
