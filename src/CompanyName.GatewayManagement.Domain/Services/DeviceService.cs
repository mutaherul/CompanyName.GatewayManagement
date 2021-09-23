using AutoMapper;
using CompanyName.GatewayManagement.Data;
using CompanyName.GatewayManagement.Data.Entities;
using CompanyName.GatewayManagement.Domain.DTO;
using CompanyName.GatewayManagement.Domain.Exceptions;
using CompanyName.GatewayManagement.Domain.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyName.GatewayManagement.Domain.Services
{
    public class DeviceService : BaseService, IDeviceService
    {
        private const int MAXIMUM_NUMBER_OF_DEVICES_ALLOWED = 10;
        private const string MESSAGE_WHEN_MAXIMUM_NUMBER_OF_DEVICE_EXCEEDED = "Maximum 10 peripheral devices are allowed for a gateway.";


        public DeviceService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public async Task<BaseResponseDto> AddDevice(long gatewayId, DeviceRequestDto device)
        {
            const string MESSAGE_DEVICE_ADDED = "Device added successfully.";

            var gateway = await _unitOfWork.GatewayRepository.FindAsync(g => g.Id.Equals(gatewayId) && !g.IsDeleted, g => g.PeripheralDevices.Where(y => !y.IsDeleted));
            if (gateway == null)
            {
                throw new GatewayManagementNotFoundResultException(DomainConstants.MESSAGE_GATEWAY_NOT_FOUND, ErrorCode.E0404.ToString());
            }

            if (gateway.PeripheralDevices != null && gateway.PeripheralDevices.Count.Equals(MAXIMUM_NUMBER_OF_DEVICES_ALLOWED))
            {
                throw new GatewayManagementForbiddenRequestException(MESSAGE_WHEN_MAXIMUM_NUMBER_OF_DEVICE_EXCEEDED, ErrorCode.S001.ToString());
            }

            var deviceEntity = _mapper.Map<PeripheralDevice>(device);
            deviceEntity.GatewayId = gateway.Id;

            await _unitOfWork.PeripheralDeviceRepository.AddAsync(deviceEntity);
            await _unitOfWork.CommitAsync();


            return new BaseResponseDto(MESSAGE_DEVICE_ADDED);
        }
        public async Task<DeviceResponseDto> GetDevice(long deviceUid)
        {
            var device = await _unitOfWork.PeripheralDeviceRepository.FindAsync(d => d.Uid.Equals(deviceUid) && !d.IsDeleted);
            if (device == null)
            {
                throw new GatewayManagementNotFoundResultException(DomainConstants.MESSAGE_DEVICE_NOT_FOUND, ErrorCode.E0404.ToString());
            }

            var response = _mapper.Map<DeviceResponseDto>(device);
            return response;
        }
        public async Task<BaseResponseDto> RemoveDevice(long uid)
        {
            const string MESSAGE_DELETED = "Device deleted successfully.";

            var device = await _unitOfWork.PeripheralDeviceRepository.FindAsync(d => d.Uid.Equals(uid) && !d.IsDeleted);
            if (device == null)
            {
                throw new GatewayManagementNotFoundResultException(DomainConstants.MESSAGE_DEVICE_NOT_FOUND, ErrorCode.E0404.ToString());
            }

            device.IsDeleted = true;
            device.ModifiedOn = DateTime.Now;
            await _unitOfWork.CommitAsync();

            return new BaseResponseDto(MESSAGE_DELETED);
        }

    }
}
