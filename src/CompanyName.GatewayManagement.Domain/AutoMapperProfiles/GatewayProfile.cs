using AutoMapper;
using CompanyName.GatewayManagement.Data.Entities;
using CompanyName.GatewayManagement.Domain.DTO;

namespace CompanyName.GatewayManagement.Domain.AutoMapperProfiles
{
    public class GatewayProfile : Profile
    {
        public GatewayProfile()
        {
            CreateMap<Gateway, GatewayDetailsDto>();
            CreateMap<GatewayRequestDto, Gateway>();
        }

    }
}
