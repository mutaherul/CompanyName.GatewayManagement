using CompanyName.GatewayManagement.Domain.DTO;
using System.Threading.Tasks;

namespace CompanyName.GatewayManagement.Domain.Interfaces
{
    public interface IDeviceService
    {
        Task<BaseResponseDto> AddDevice(long gatewayId, DeviceRequestDto device);
        Task<DeviceResponseDto> GetDevice(long deviceUid);
        Task<BaseResponseDto> RemoveDevice(long uid);
    }
}
