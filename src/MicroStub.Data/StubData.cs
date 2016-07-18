using MicroStub.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroStub.Data
{
    public class StubData : IStubData
    {
        private List<Subscriber> _subscribers;
        private List<Project> _projects;
        public StubData()
        {
            _subscribers = new List<Subscriber> {
                new Subscriber { Key = "0A2C3880-4969-4AC4-873B-A3E1CB88B0F6", Secret = "650A65AB-2730-4869-B8FD-93F987A6C91E" },
                new Subscriber { Key = "233C259C-675E-4504-9BA6-8FA828ECE8CE", Secret = "21E841E5-4685-4D9D-8D5C-0B34D6EA1B82" }
            };

            _projects = new List<Project>
            {
                new Project
                {
                    Name = "CartService",
                    SubscriberKey = "0A2C3880-4969-4AC4-873B-A3E1CB88B0F6",
                    EndPoints = new List<Endpoint>
                    {
                        new Endpoint
                        {
                            Address = "/api/v1/cart/1",
                            Methods = new List<Method>
                            {
                                new Method
                                {
                                    ContentType = "application/json",
                                    HttpMethodName = "GET",
                                    Response = "{ \"cartId\": 123, \"total\": 12.49, \"items\": [ { \"price\": 10.29 }, { \"price\": 2.20} ] }",
                                    HttpStatusCode = 200
                                }
                            }
                        }
                    }
                }
            };
        }

        public Project GetProject(string subscriberKey, string projectName)
        {
            return _projects.FirstOrDefault(o => o.SubscriberKey == subscriberKey && o.Name == projectName);
        }

        public List<Subscriber> GetSubscribers()
        {
            return _subscribers;
        }
    }
}
