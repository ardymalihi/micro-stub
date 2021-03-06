﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.WebUtilities;
using MicroStub.Contract.Info;

namespace MicroStub.Common
{
    public class HttpHelper: IHttpHelper
    {
        public RequestInfo GetRequestInfo(HttpContext context)
        {
            RequestInfo result = null;

            var routes = context.Request.Path.Value.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            if (routes.Count() > 0)
            {
                var authInfo = routes[0].Split(new string[] { ":", "@" }, StringSplitOptions.RemoveEmptyEntries);
                if (authInfo.Count() == 3)
                {
                    var endpoint = "/";
                    string requestBody = new StreamReader(context.Request.Body).ReadToEnd();

                    if (routes.Count() > 1)
                    {
                        endpoint = endpoint + string.Join("/", routes.Skip(1).ToArray());
                    }
                    result = new RequestInfo
                    {
                        Key = authInfo[0],
                        Secret = authInfo[1],
                        Project = authInfo[2],
                        Endpoint = endpoint,
                        QueryString = context.Request.QueryString.Value,
                        Method = context.Request.Method,
                        RequestBody = requestBody
                    };
                }
            }

            return result;
        }

        public bool JsonsEqual(string first, string second)
        {
            try
            {
                var firstJson = JObject.Parse(!string.IsNullOrWhiteSpace(first) ? first : "{}");
                var secondJson = JObject.Parse(!string.IsNullOrWhiteSpace(second) ? second : "{}");
                return JToken.DeepEquals(firstJson, secondJson);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        public bool QueryStringsEqual(string first, string second)
        {
            try
            {
                var firstQueryDic = QueryHelpers.ParseQuery(first);
                var secondQueryDic = QueryHelpers.ParseQuery(second);

                var firstJson = JsonConvert.SerializeObject(firstQueryDic.Keys.ToDictionary(k => k.ToLower(), k => firstQueryDic[k]));
                var secondJson = JsonConvert.SerializeObject(secondQueryDic.Keys.ToDictionary(k => k.ToLower(), k => secondQueryDic[k]));

                return JsonsEqual(firstJson, secondJson);
            }
            catch(Exception ex)
            {
                throw;
            }
            
        }
    }
}
