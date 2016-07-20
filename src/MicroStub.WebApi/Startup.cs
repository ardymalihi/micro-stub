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
using MicroStub.Data;
using MicroStub.Service;
using System.IO;
using MicroStub.Common;
using MicroStub.Contract.Interface;

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
            services.AddSingleton<IHttpHelper, HttpHelper>();
            services.AddSingleton<IStubData, StubData>();
            services.AddSingleton<IStubService, StubService>();

            services.AddMemoryCache();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            IHttpHelper httpHelper,
            IStubService stubService)
        {
            //Authentication
            app.Use(next => async context =>
            {
                var requestInfo = httpHelper.GetRequestInfo(context);
                var authorized = false;
                if (requestInfo != null)
                {
                    authorized = stubService.SubscriberExists(requestInfo.Key, requestInfo.Secret);
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
                var requestInfo = httpHelper.GetRequestInfo(context);
                var method = stubService.GetMethod(requestInfo);
                if (method != null)
                {
                    context.Response.StatusCode = method.HttpStatusCode;
                    context.Response.ContentType = method.ContentType;
                    await context.Response.WriteAsync(method.Response);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
            });

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

        }

        
    }
}
