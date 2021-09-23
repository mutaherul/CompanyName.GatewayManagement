using CompanyName.GatewayManagement.Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyName.GatewayManagement.Domain.Interfaces
{
    public interface IGatewayService
    {
        Task<List<GatewayDetailsDto>> GetAllGateways();
        Task<GatewayDetailsDto> GetGateway(long id);
        Task<BaseResponseDto> AddGateway(GatewayRequestDto gateway);
    }
}
