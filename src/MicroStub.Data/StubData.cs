using MicroStub.Contract.Dto;
using MicroStub.Contract.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace MicroStub.Data
{
    public class StubData : IStubData
    {
        private List<Subscriber> _subscribers;
        private List<Project> _projects;

        public IConfigurationRoot _configuration { get; }

        private AppSettings _appSettings;

        public StubData(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            _appSettings = new AppSettings();

            ConfigurationBinder.Bind(_configuration, _appSettings);

        }

        public Project GetProject(string subscriberKey, string projectName)
        {
            return _appSettings.MicroStub.Subscriber.Projects.FirstOrDefault(o => o.Name == projectName);
        }

        public List<Subscriber> GetSubscribers()
        {
            return new List<Subscriber> { _appSettings.MicroStub.Subscriber };
        }
    }
}
