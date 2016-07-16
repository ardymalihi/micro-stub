using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MicroStub.Controllers
{
    [Route("api/[controller]")]
    public class StubsController : Controller
    {
        private readonly string _key;
        private readonly string _secret;
        private readonly string _project;

        public StubsController() 
        {
            //_key = this.ControllerContext.RouteData.Values["key"]?.ToString();
            //_secret = this.ControllerContext.RouteData.Values["secret"]?.ToString();
            //_project = this.ControllerContext.RouteData.Values["project"]?.ToString();
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
