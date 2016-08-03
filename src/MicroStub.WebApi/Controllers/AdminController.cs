using Microsoft.AspNetCore.Mvc;
using MicroStub.Data;
using Newtonsoft.Json.Schema;
using MicroStub.WebApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace MicroStub.WebApi.Controllers
{
    public class AdminController : Controller
    {
        private IHostingEnvironment _env;

        public AdminController(IHostingEnvironment env)
        {
            _env = env;
        }
        public IActionResult Index()
        {
            var jsonSchemaGenerator = new JsonSchemaGenerator();
            var myType = typeof(StubConfig);
            var schemaGenerator = jsonSchemaGenerator.Generate(myType);
            schemaGenerator.Title = myType.Name;
            var schema = schemaGenerator.ToString();

            var microStubInfo = new StubConfig();

            var builder = new ConfigurationBuilder()
                    .SetBasePath(_env.ContentRootPath)
                    .AddJsonFile("microstub.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"microstub.{_env.EnvironmentName}.json", optional: true)
                    .AddEnvironmentVariables();

            var config = builder.Build();

            config.Bind(microStubInfo);

            var result = new AdminModel {
                Data = microStubInfo,
                Schema = schema
            };
            return View(result);
        }
    }
}
