using Microsoft.AspNetCore.Mvc;
using CompanyName.GatewayManagement.Api.Presenter;
using CompanyName.GatewayManagement.Domain.DTO;
using CompanyName.GatewayManagement.Domain.Interfaces;
using System.Threading.Tasks;

namespace CompanyName.GatewayManagement.Api.Controllers
{
    [Route("gateways")]
    [ApiController]
    public class GatewayController : BaseController
    {
        private readonly IGatewayService _gatewayService;

        public GatewayController(IGatewayService gatewayService, IResponseWrappable wrapper) : base(wrapper)
        {
            _gatewayService = gatewayService;
        }

        /// <summary>
        /// Get All Gateways with it's associated devices
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<object>> GetAllGateways()
        {
            var response = await _gatewayService.GetAllGateways();
            return Ok(await _wrapper.Get(response));
        }

        /// <summary>
        /// Get a specific Gateway according to it's id
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<object>> GetAllGateways(long id)
        {
            var response = await _gatewayService.GetGateway(id);
            return Ok(await _wrapper.Get(response));
        }

        /// <summary>
        /// Add new Gateway
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<object>> AddGateway([FromBody] GatewayRequestDto gateway)
        {
            var response = await _gatewayService.AddGateway(gateway);
            return Ok(await _wrapper.Get(response));
        }
    }
}
