using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Contract
{
    public class Project
    {
        public string SubscriberId { get; set; }

        public string Name { get; set; }

        public List<Endpoint> EndPoints { get; set; }

        public Project()
        {
            EndPoints = new List<Endpoint>();
        }
    }
}
