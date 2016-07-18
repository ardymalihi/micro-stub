using Microsoft.Extensions.Caching.Memory;
using MicroStub.Common;
using MicroStub.Contract;
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
        private IMemoryCache _memoryCache;
        private IHttpHelper _httpHelper;

        private const int SUBSCRIBERS_TIMEOUT_DURATION_MINUTES = 10;
        private const int PROJECTS_TIMEOUT_DURATION_MINUTES = 5;

        public StubService(
            IMemoryCache memoryCache, 
            IHttpHelper httpHelper,
            IStubData stubData)
        {
            _stubData = stubData;
            _memoryCache = memoryCache;
            _httpHelper = httpHelper;
        }
        public bool SubscriberExists(string key, string secret)
        {            
            var subscribers = _memoryCache.Get<List<Subscriber>>("subscribers");
            if (subscribers == null)
            {
                subscribers = _stubData.GetSubscribers();
                _memoryCache.Set("subscribers", subscribers, new DateTimeOffset(DateTime.Now.AddMinutes(SUBSCRIBERS_TIMEOUT_DURATION_MINUTES)));
            }
            return subscribers.Any(o => o.Key == key && o.Secret == secret);
        }

        
        public Method GetMethod(RequestInfo requestInfo)
        {
            var project = _memoryCache.Get<Project>("project_"+ requestInfo.Project);
            if (project == null)
            {
                project = _stubData.GetProject(requestInfo.Key, requestInfo.Project);
                _memoryCache.Set("project_" + requestInfo.Project, project, new DateTimeOffset(DateTime.Now.AddMinutes(PROJECTS_TIMEOUT_DURATION_MINUTES)));
            }

            if (project != null)
            {
                var endpoint = project.EndPoints.FirstOrDefault(o => o.Address.ToLower() == requestInfo.Endpoint.ToLower());
                if (endpoint != null)
                {
                    return endpoint.Methods.FirstOrDefault(o =>
                    o.HttpMethodName.ToLower() == requestInfo.Method.ToLower() &&
                    _httpHelper.QueryStringsEqual(o.QueryString, requestInfo.QueryString) &&
                    _httpHelper.JsonsEqual(o.RequestBody, requestInfo.RequestBody));
                }
            }

            return null;
        }
    }
}
