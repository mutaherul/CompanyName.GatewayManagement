using System;
using System.Collections.Generic;

#nullable disable

namespace CompanyName.GatewayManagement.Data.Entities

{
    public partial class Gateway
    {
        public Gateway()
        {
            PeripheralDevices = new HashSet<PeripheralDevice>();
        }

        public long Id { get; set; }
        public string GatewayName { get; set; }
        public Guid SerialNumber { get; set; }
        public string AddressIpv4 { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<PeripheralDevice> PeripheralDevices { get; set; }
    }
}
