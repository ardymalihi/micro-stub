using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Contract
{
    public class Scenario
    {
        public string Key { get; set; }

        public string Project { get; set; }

        public string Endpoint { get; set; }

        public List<ScenarioItem> Items { get; set; }

    }
}
