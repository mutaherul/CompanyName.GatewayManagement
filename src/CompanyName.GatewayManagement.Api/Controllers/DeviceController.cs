using CompanyName.GatewayManagement.Api.Presenter;
using CompanyName.GatewayManagement.Domain.DTO;
using CompanyName.GatewayManagement.Domain.Exceptions;
using CompanyName.GatewayManagement.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CompanyName.GatewayManagement.Api.Controllers
{
    [Route("gateways/{id}")]
    [ApiController]
    public class DeviceController : BaseController
    {
        private readonly IGatewayService _gatewayService;
        private readonly IDeviceService _deviceService;
        public DeviceController(IGatewayService gatewayService, IDeviceService deviceService, IResponseWrappable wrapper) : base(wrapper)
        {
            _gatewayService = gatewayService;
            _deviceService = deviceService;
        }
        /// <summary>
        /// Get all devices of a gateway
        /// </summary>
        [HttpGet]
        [Route("devices")]
        public async Task<ActionResult<object>> GetDevices(long id)
        {
            var response = await _gatewayService.GetGateway(id);
            return Ok(await _wrapper.Get(response.PeripheralDevices));
        }
        /// <summary>
        /// Add new device under a gateway
        /// </summary>
        [HttpPost]
        [Route("devices")]
        public async Task<ActionResult<object>> AddDevice(long id, [FromBody] DeviceRequestDto device)
        {
            var response = await _deviceService.AddDevice(id, device);
            return Ok(await _wrapper.Get(response));
        }

        /// <summary>
        /// Get a specific device according to it's UId
        /// </summary>
        [HttpGet]
        [Route("devices/{deviceUid}")]
        public async Task<ActionResult<object>> GetDevice(long id, long deviceUid)
        {
            await GetGateway(id);
            var response = await _deviceService.GetDevice(deviceUid);
            return Ok(await _wrapper.Get(response));
        }


        /// <summary>
        /// Delete a specific device according to it's UId
        /// </summary>
        [HttpDelete]
        [Route("devices/{deviceUid}")]
        public async Task<ActionResult<object>> RemoveDevice(long id, long deviceUid)
        {
            await GetGateway(id);

            var response = await _deviceService.RemoveDevice(deviceUid);
            return Ok(await _wrapper.Get(response));
        }

        private async Task<GatewayDetailsDto> GetGateway(long id)
        {
            var gateway = await _gatewayService.GetGateway(id);
            if (gateway == null)
            {
                throw new GatewayManagementNotFoundResultException("Gateway not found.", ErrorCode.E0404.ToString());
            }
            return gateway;
        }
    }
}
