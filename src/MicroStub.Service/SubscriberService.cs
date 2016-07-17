using Microsoft.Extensions.Caching.Memory;
using MicroStub.Common;
using MicroStub.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Service
{
    public class SubscriberService : ISubscriberService
    {
        private ISubscriberData _subscriberData;
        private List<Subscriber> _subscribers;
        private IMemoryCache _memoryCache;
        private IHttpHelper _httpHelper;

        private const int TIMEOUT_DURATION_MINUTES = 5;

        public SubscriberService(
            IMemoryCache memoryCache, 
            IHttpHelper httpHelper,
            ISubscriberData subscriberData)
        {
            _subscriberData = subscriberData;
            _memoryCache = memoryCache;
            _httpHelper = httpHelper;
        }
        public bool Exists(string key, string secret)
        {            
            var subscribers = _memoryCache.Get<List<Subscriber>>("subscribers");
            if (subscribers == null)
            {
                subscribers = _subscriberData.GetSubscribers();
                _memoryCache.Set("subscribers", subscribers, new DateTimeOffset(DateTime.Now.AddMinutes(TIMEOUT_DURATION_MINUTES)));
            }
            return subscribers.Any(o => o.Key == key && o.Secret == secret);
        }

        

        public ScenarioItem GetScenarioItem(RequestInfo requestInfo)
        {
            var scenario = _subscriberData.GetScenario(requestInfo.Key, requestInfo.Project, requestInfo.Endpoint);

            if (scenario != null)
            {
                return scenario.Items.FirstOrDefault(o => o.Method.ToLower() == requestInfo.Method.ToLower() && _httpHelper.QueryStringsEqual(o.QueryString, requestInfo.QueryString) && _httpHelper.JsonsEqual(o.RequestBody, requestInfo.RequestBody));
            }
            else
            {
                return null;
            }
        }
    }
}
