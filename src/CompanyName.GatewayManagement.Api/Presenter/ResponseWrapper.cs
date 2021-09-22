using System.Threading.Tasks;

namespace CompanyName.GatewayManagement.Api.Presenter
{
    public class WrapperedResponse : IResponseWrappable
    {
        public Task<object> Get(object value)
        {
            var wrappedObject = new
            {
                data = value
            };

            return Task.FromResult<object>(wrappedObject);
        }
    }
}
