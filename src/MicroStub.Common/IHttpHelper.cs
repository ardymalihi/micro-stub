using Microsoft.AspNetCore.Http;
using MicroStub.Contract.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Common
{
    public interface IHttpHelper
    {
        RequestInfo GetRequestInfo(HttpContext context);

        bool JsonsEqual(string first, string second);

        bool QueryStringsEqual(string first, string second);
    }
}
