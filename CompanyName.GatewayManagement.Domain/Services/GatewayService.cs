using AutoMapper;
using CompanyName.GatewayManagement.Data;
using CompanyName.GatewayManagement.Data.Entities;
using CompanyName.GatewayManagement.Domain.DTO;
using CompanyName.GatewayManagement.Domain.Exceptions;
using CompanyName.GatewayManagement.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyName.GatewayManagement.Domain.Services
{
    public class GatewayService : BaseService, IGatewayService
    {
        //public GatewayService()
        //{

        //}
        public GatewayService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        public async Task<List<GatewayDetailsDto>> GetAllGateways()
        {
            var gatewaysList = await _unitOfWork.GatewayRepository.GetAllAsync(g => !g.IsDeleted, o => o.OrderByDescending(g => g.CreatedOn), null, null, g => g.PeripheralDevices.Where(y => !y.IsDeleted));

            if (!gatewaysList.Any())
            {
                return new List<GatewayDetailsDto>();
            }
            var response = _mapper.Map<List<GatewayDetailsDto>>(gatewaysList);
            return response;
        }

        public async Task<GatewayDetailsDto> GetGateway(long id)
        {
            var gateway = await _unitOfWork.GatewayRepository.FindAsync(g => g.Id.Equals(id) && !g.IsDeleted, g => g.PeripheralDevices.Where(y => !y.IsDeleted));
            if (gateway == null)
            {
                throw new GatewayManagementNotFoundResultException(DomainConstants.MESSAGE_GATEWAY_NOT_FOUND, ErrorCode.E0404.ToString());
            }

            var response = _mapper.Map<GatewayDetailsDto>(gateway);
            return response;
        }

        public async Task<BaseResponseDto> AddGateway(GatewayRequestDto gateway)
        {
            const string MESSAGE_GATEWAY_ADDED = "Gateway added successfully.";
            var gatewayEntity = _mapper.Map<Gateway>(gateway);

            await _unitOfWork.GatewayRepository.AddAsync(gatewayEntity);
            await _unitOfWork.CommitAsync();

            return new BaseResponseDto(MESSAGE_GATEWAY_ADDED);
        }


    }
}
