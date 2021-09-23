using System;
using System.Collections.Generic;

namespace CompanyName.GatewayManagement.Domain.DTO
{
    public class GatewayDetailsDto
    {
        public string GatewayName { get; set; }
        public Guid SerialNumber { get; set; }
        public string AddressIpv4 { get; set; }

        public List<DeviceResponseDto> PeripheralDevices { get; set; }
    }
}
