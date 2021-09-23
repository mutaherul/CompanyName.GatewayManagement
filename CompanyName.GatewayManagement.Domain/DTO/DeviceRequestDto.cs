using System.ComponentModel.DataAnnotations;

namespace CompanyName.GatewayManagement.Domain.DTO
{
    public class DeviceRequestDto
    {
        [Required]
        [StringLength(80, ErrorMessage = "Vendor Name length can''t be more than 80.")]
        public string VendorName { get; set; }
        [Required]
        [Range(0, 1)]
        public int Status { get; set; }
    }
}
