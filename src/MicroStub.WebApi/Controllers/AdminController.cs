using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;
using MicroStub.WebApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using MicroStub.Contract.Interface;
using MicroStub.Contract.Config;

namespace MicroStub.WebApi.Controllers
{
    public class AdminController : Controller
    {
        private IHostingEnvironment _env;
        private IStubData _stubData;

        public AdminController(IHostingEnvironment env, IStubData stubData)
        {
            _env = env;
            _stubData = stubData;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var jsonSchemaGenerator = new JsonSchemaGenerator();
            var myType = typeof(StubConfig);
            var schemaGenerator = jsonSchemaGenerator.Generate(myType);
            schemaGenerator.Title = myType.Name;
            var schema = schemaGenerator.ToString();

            var result = new AdminModel {
                Data = _stubData.MicroStubConfig,
                Schema = schema
            };
            return View(result);
        }

        [HttpPost]
        public IActionResult Save(StubConfig request)
        {
            _stubData.Save(request, _env.ContentRootPath + "\\microstub.json");
            return Ok();
        }

        [HttpGet]
        public IActionResult Reset()
        {
            _stubData.MicroStubConfig = null;

            return RedirectToAction("Index");
        }
    }
}
