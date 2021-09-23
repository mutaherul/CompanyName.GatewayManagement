namespace CompanyName.GatewayManagement.Domain.DTO
{
    public class BaseResponseDto
    {
        public BaseResponseDto()
        {
        }

        public BaseResponseDto(string message)
        {
            Message = message;
        }

        public BaseResponseDto(bool status, string message)
        {
            Status = status;
            Message = message;
        }

        public bool Status { get; set; } = true;
        public string Message { get; set; }
    }
}
