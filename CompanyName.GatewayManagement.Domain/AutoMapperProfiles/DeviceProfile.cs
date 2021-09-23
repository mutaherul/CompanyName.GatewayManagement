using AutoMapper;
using CompanyName.GatewayManagement.Data.Entities;
using CompanyName.GatewayManagement.Domain.DTO;

namespace CompanyName.GatewayManagement.Domain.AutoMapperProfiles
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<PeripheralDevice, DeviceResponseDto>();
            CreateMap<DeviceRequestDto, PeripheralDevice>();
        }
    }
}
