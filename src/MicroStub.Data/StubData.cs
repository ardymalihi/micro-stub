using MicroStub.Contract.Dto;
using MicroStub.Contract.Interface;
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
                new Subscriber { UserId = "ardalan.malihi@gmail.com",  Key = "0A2C388049694AC4873BA3E1CB88B0F6", Secret = "650A65AB27304869B8FD93F987A6C91E" }
            };

            _projects = new List<Project>
            {
                new Project
                {
                    SubscriberKey = "0A2C388049694AC4873BA3E1CB88B0F6",
                    Name = "CartService",
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
