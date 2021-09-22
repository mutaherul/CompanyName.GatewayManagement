using System.Threading.Tasks;

namespace CompanyName.GatewayManagement.Api.Presenter
{
    public interface IResponseWrappable
    {
        Task<object> Get(object value);
    }
}
