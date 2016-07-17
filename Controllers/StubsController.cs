using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicroStub.Contract;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MicroStub.Controllers
{
    [Route("{key}:{secret}@{project}")]
    public class StubsController : Controller
    {
        public string Key { get { return this.ControllerContext.RouteData.Values["key"]?.ToString(); } }

        public string Secret { get { return this.ControllerContext.RouteData.Values["secret"]?.ToString(); } }

        public string Project { get { return this.ControllerContext.RouteData.Values["project"]?.ToString(); } }

        private ISubscriberService _subscriberService;

        public StubsController(ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (_subscriberService.Exists(Key, Secret))
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new UnauthorizedResult();
            }

        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { this.Key,  this.Secret, this.Project };
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
