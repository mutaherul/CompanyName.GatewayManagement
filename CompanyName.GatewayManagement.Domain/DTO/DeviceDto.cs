using System;

namespace CompanyName.GatewayManagement.Domain.DTO
{
    public class DeviceResponseDto
    {
        public long Uid { get; set; }
        public string VendorName { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
