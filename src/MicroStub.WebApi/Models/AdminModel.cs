using MicroStub.Contract.Config;

namespace MicroStub.WebApi.Models
{
    public class AdminModel
    {
        public StubConfig Data { get; set; }

        public string Schema { get; set; }
    }
}
