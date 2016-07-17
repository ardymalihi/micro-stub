using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Contract
{
    public interface ISubscriberService
    {
        bool Exists(string key, string secret);

        bool JsonsEqual(string first, string second);

        bool QueryStringsEqual(string first, string second);
    }
}
