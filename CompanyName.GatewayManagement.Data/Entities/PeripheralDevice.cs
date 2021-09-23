using System;

#nullable disable

namespace CompanyName.GatewayManagement.Data.Entities
{
    public partial class PeripheralDevice
    {
        public long Uid { get; set; }
        public string VendorName { get; set; }
        public long GatewayId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public EnumStatus Status { get; set; }

        public virtual Gateway Gateway { get; set; }
    }
}
