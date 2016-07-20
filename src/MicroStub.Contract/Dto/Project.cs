using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Contract.Dto
{
    public class Project
    {
        public string SubscriberKey { get; set; }

        public string Name { get; set; }

        public List<Endpoint> EndPoints { get; set; }

        public Project()
        {
            EndPoints = new List<Endpoint>();
        }
    }
}
