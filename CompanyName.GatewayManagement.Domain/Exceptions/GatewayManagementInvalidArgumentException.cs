using System;
using System.Runtime.Serialization;

namespace CompanyName.GatewayManagement.Domain.Exceptions
{
    [Serializable]
    public class GatewayManagementInvalidArgumentException : GatewayManagementException
    {
        public string ErrorCode { get; private set; }

        protected GatewayManagementInvalidArgumentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public GatewayManagementInvalidArgumentException(string message) : base(message)
        {
        }

        public GatewayManagementInvalidArgumentException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public GatewayManagementInvalidArgumentException(string message, Exception innerException, string errorCode = null)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
