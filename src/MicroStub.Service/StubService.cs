using MicroStub.Common;
using MicroStub.Contract.Dto;
using MicroStub.Contract.Info;
using MicroStub.Contract.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Service
{
    public class StubService : IStubService
    {
        private IStubData _stubData;
        private List<Subscriber> _subscribers;
        private IHttpHelper _httpHelper;

        public StubService(
            IHttpHelper httpHelper,
            IStubData stubData)
        {
            _stubData = stubData;
            _httpHelper = httpHelper;
        }

        public bool SubscriberExists(string key, string secret)
        {
            return _stubData.GetSubscriber(key, secret) != null;
        }

        
        public Method GetMethod(RequestInfo requestInfo)
        {
            var subscriber = _stubData.GetSubscriber(requestInfo.Key, requestInfo.Secret);

            if (subscriber != null)
            {
                var project = subscriber.Projects.FirstOrDefault(o => o.Name.ToLower() == requestInfo.Project.ToLower());

                if (project != null)
                {
                    var endpoint = project.EndPoints.FirstOrDefault(o => o.Address.ToLower() == requestInfo.Endpoint.ToLower());
                    if (endpoint != null)
                    {
                        var method = endpoint.Methods.FirstOrDefault(o =>
                        o.HttpMethodName.ToLower() == requestInfo.Method.ToLower() &&
                        _httpHelper.QueryStringsEqual(o.QueryString, requestInfo.QueryString) &&
                        _httpHelper.JsonsEqual(o.RequestBody, requestInfo.RequestBody));
                        return method;
                    }
                }
            }
            
            return null;
        }
    }
}
