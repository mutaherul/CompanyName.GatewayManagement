using System;
using System.Runtime.Serialization;

namespace CompanyName.GatewayManagement.Domain.Exceptions
{
    [Serializable]
    public class GatewayManagementForbiddenRequestException : GatewayManagementException
    {
        public string ErrorCode { get; private set; }

        protected GatewayManagementForbiddenRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public GatewayManagementForbiddenRequestException(string message) : base(message)
        {
        }

        public GatewayManagementForbiddenRequestException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public GatewayManagementForbiddenRequestException(string message, Exception innerException, string errorCode = null)
             : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
