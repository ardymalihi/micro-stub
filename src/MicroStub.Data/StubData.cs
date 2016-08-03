using MicroStub.Contract.Dto;
using MicroStub.Contract.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MicroStub.Contract.Info;
using MicroStub.Contract.Config;
using Newtonsoft.Json;

namespace MicroStub.Data
{
    public class StubData : IStubData
    {
        private IHostingEnvironment _env;
        private StubConfig _microStubConfig;

        public StubConfig MicroStubConfig
        {
            get
            {
                return _microStubConfig;
            }
        }

        public StubData(IHostingEnvironment env)
        {
            _env = env;

            Bind();
        }

        private void Bind()
        {
            _microStubConfig = new StubConfig();

            var builder = new ConfigurationBuilder()
                    .SetBasePath(_env.ContentRootPath)
                    .AddJsonFile("microstub.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"microstub.{_env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

            var config = builder.Build();

            config.Bind(_microStubConfig);

            var token = config.GetReloadToken();
            token.RegisterChangeCallback(c =>
            {
                Bind();

            }, this);
        }

        public Subscriber GetSubscriber(string subscriberKey, string subscriberSecret)
        {
            return _microStubConfig.Subscribers.FirstOrDefault(o => o.Key == subscriberKey && o.Secret == subscriberSecret);
        }

        public void Save(StubConfig stubConfig, string fileName)
        {
            var logFile = System.IO.File.Create(fileName);
            var logWriter = new System.IO.StreamWriter(logFile);
            logWriter.WriteLine(JsonConvert.SerializeObject(stubConfig, Formatting.Indented));
            logWriter.Dispose();
            Bind();
        }
    }
}
