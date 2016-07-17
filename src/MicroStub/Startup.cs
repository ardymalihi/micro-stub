using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using MicroStub.Contract;
using MicroStub.Data;
using MicroStub.Service;
using System.IO;

namespace MicroStub
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISubscriberData, SubscriberData>();
            services.AddSingleton<ISubscriberService, SubscriberService>();

            services.AddMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,ISubscriberService subscriberService)
        {
            //Authentication
            app.Use(next => async context =>
            {
                var requestInfo = GetRequestInfo(context);
                var authorized = false;
                if (requestInfo != null)
                {
                    authorized = subscriberService.Exists(requestInfo.Key, requestInfo.Secret);
                }

                if (authorized)
                {
                    await next.Invoke(context);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                }
            });

            //Service
            app.Run(async context =>
            {
                var requestInfo = GetRequestInfo(context);
                var scenarioItem = subscriberService.GetScenarioItem(requestInfo);
                if (scenarioItem != null)
                {
                    context.Response.ContentType = scenarioItem.ContentType;
                    await context.Response.WriteAsync(scenarioItem.Response);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

        }

        private RequestInfo GetRequestInfo(HttpContext context)
        {
            RequestInfo result = null;

            var routes = context.Request.Path.Value.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            var authInfo = routes[0].Split(new string[] { ":", "@" }, StringSplitOptions.RemoveEmptyEntries);
            if (authInfo.Count() == 3)
            {
                var endpoint = "/";
                string requestBody = null;

                if (context.Request.Body.CanSeek)
                {
                    context.Request.Body.Position = 0;
                    requestBody = new StreamReader(context.Request.Body).ReadToEnd();
                }
                if (routes.Count() > 1)
                {
                    endpoint = endpoint + string.Join("/",routes.Skip(1).ToArray());
                }
                result = new RequestInfo
                {
                    Key = authInfo[0],
                    Secret = authInfo[1],
                    Project = authInfo[1],
                    Endpoint = endpoint,
                    QueryString = context.Request.QueryString.Value,
                    Method = context.Request.Method,
                    RequestBody = requestBody
                };
            }

            return result;
        }
    }
}
