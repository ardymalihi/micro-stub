using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Contract.Dto
{
    public class Subscriber
    {
        public string Key { get; set; }

        public string Secret { get; set; }

        public List<Project> Projects { get; set; }

        public Subscriber()
        {
            Projects = new List<Project>();
            Key = Guid.NewGuid().ToString().ToString().Replace("-","");
            Secret = Guid.NewGuid().ToString().ToString().Replace("-", "");
        }

    }
}
