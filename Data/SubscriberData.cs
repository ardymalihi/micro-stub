using MicroStub.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Data
{
    public class SubscriberData : ISubscriberData
    {
        public Scenario GetScenario(string key, string project, string endpoint)
        {
            if (key== "0A2C3880-4969-4AC4-873B-A3E1CB88B0F6")
            {
                return new Scenario
                {
                    Key = key,
                    Project = project,
                    Endpoint = endpoint,
                    Items = new List<ScenarioItem> {
                        new ScenarioItem {
                            Method = "Get",
                            ContentType = "text/html",
                            Response = "Hello from Stub!"
                        }
                    }
                };
            }
            else
            {
                return null;
            }
        }

        public List<Subscriber> GetSubscribers()
        {
            return new List<Subscriber> {
                new Subscriber { Key = "0A2C3880-4969-4AC4-873B-A3E1CB88B0F6", Secret = "650A65AB-2730-4869-B8FD-93F987A6C91E" },
                new Subscriber { Key = "233C259C-675E-4504-9BA6-8FA828ECE8CE", Secret = "21E841E5-4685-4D9D-8D5C-0B34D6EA1B82" }
            };
        }
    }
}
