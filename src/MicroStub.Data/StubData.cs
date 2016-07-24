using MicroStub.Contract.Dto;
using MicroStub.Contract.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MicroStub.Contract.Info;

namespace MicroStub.Data
{
    public class StubData : IStubData
    {
        private IHostingEnvironment _env;
        private StubConfig _microStubInfo;

        public StubData(IHostingEnvironment env)
        {
            _env = env;

            Bind();
        }

        private void Bind()
        {
            _microStubInfo = new StubConfig();

            var builder = new ConfigurationBuilder()
                    .SetBasePath(_env.ContentRootPath)
                    .AddJsonFile("microstub.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"microstub.{_env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

            var config = builder.Build();

            config.Bind(_microStubInfo);

            var token = config.GetReloadToken();
            token.RegisterChangeCallback(c =>
            {
                Bind();

            }, this);
        }

        public Subscriber GetSubscriber(string subscriberKey, string subscriberSecret)
        {
            return _microStubInfo.Subscribers.FirstOrDefault(o => o.Key == subscriberKey && o.Secret == subscriberSecret);
        }
    }
}
