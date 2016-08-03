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
using MicroStub.Contract.Info;
using MicroStub.Contract.Dto;

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

            services.AddMvc();

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
            RequestInfo requestInfo = null;
            Subscriber subscriber = null;

            app.UseStaticFiles();

            app.UseMvc(rb =>
            {
                rb.MapRoute(
                    name: "admin",
                    template: "admin/index",
                    defaults: new { controller = "Admin", action = "Index" });
            });

            //Authentication
            app.Use(next => async context =>
            {
                requestInfo = httpHelper.GetRequestInfo(context);
                
                if (requestInfo != null)
                {
                    subscriber = stubService.GetSubscriber(requestInfo.Key, requestInfo.Secret);

                    if (subscriber != null)
                    {
                        await next.Invoke(context);
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    }
                }
                else
                {
                    await next.Invoke(context);
                }
            });

            //Service
            app.Use(next => async context =>
            {
                if (requestInfo != null)
                {
                    var method = stubService.GetMethod(requestInfo);
                    if (method != null)
                    {
                        context.Response.StatusCode = (int)method.HttpStatusCode;
                        context.Response.ContentType = method.ContentType.ToString().ToLower().Replace("_","/");
                        await context.Response.WriteAsync(method.Response);
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                    }
                }
                else
                {
                    await next.Invoke(context);
                }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

        }

        
    }
}
