using System.ComponentModel.DataAnnotations;

namespace CompanyName.GatewayManagement.Domain.DTO
{
    public class GatewayRequestDto
    {
        [Required]
        public string GatewayName { get; set; }


        [RegularExpression(@"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$", ErrorMessage = "Not a valid IP adress.")]
        public string AddressIpv4 { get; set; }
    }
}
