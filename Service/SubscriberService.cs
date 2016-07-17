﻿using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using MicroStub.Contract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        private const int TIMEOUT_DURATION_MINUTES = 5;

        public SubscriberService(IMemoryCache memoryCache, ISubscriberData subscriberData)
        {
            _subscriberData = subscriberData;
            _memoryCache = memoryCache;
        }
        public bool Exists(string key, string secret)
        {
            var e = QueryStringsEqual("ID=951357852456&FNAME=Jaime&LNAME=Lopez", "?LNAME=Lopez&ID=951357852456&fname=Jaime&");
            var subscribers = _memoryCache.Get<List<Subscriber>>("subscribers");
            if (subscribers == null)
            {
                subscribers = _subscriberData.GetSubscribers();
                _memoryCache.Set("subscribers", subscribers, new DateTimeOffset(DateTime.Now.AddMinutes(TIMEOUT_DURATION_MINUTES)));
            }
            return subscribers.Any(o => o.Key == key && o.Secret == secret);
        }

        public bool JsonsEqual(string first, string second)
        {
            var firstJson = JObject.Parse(first);
            var secondJson = JObject.Parse(second);
            return JToken.DeepEquals(firstJson, secondJson);
        }

        public bool QueryStringsEqual(string first, string second)
        {
            //new Uri(first).Query
            var firstQueryDic = QueryHelpers.ParseQuery(first);
            var secondQueryDic = QueryHelpers.ParseQuery(second);

            var firstJson = JsonConvert.SerializeObject(firstQueryDic.Keys.ToDictionary(k => k.ToLower(), k => firstQueryDic[k]));
            var secondJson = JsonConvert.SerializeObject(secondQueryDic.Keys.ToDictionary(k => k.ToLower(), k => secondQueryDic[k]));

            return JsonsEqual(firstJson, secondJson);
        }
    }
}
