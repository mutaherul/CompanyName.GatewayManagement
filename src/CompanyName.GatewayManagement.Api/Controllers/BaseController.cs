using Microsoft.AspNetCore.Mvc;
using CompanyName.GatewayManagement.Api.Presenter;

namespace CompanyName.GatewayManagement.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        public readonly IResponseWrappable _wrapper;

        public BaseController(IResponseWrappable wrapper)
        {
            _wrapper = wrapper;
        }
    }
}
