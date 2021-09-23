using System;
using System.Runtime.Serialization;

namespace CompanyName.GatewayManagement.Domain.Exceptions
{
    [Serializable]
    public class GatewayManagementNotFoundResultException : GatewayManagementException
    {
        public string ErrorCode { get; private set; }

        protected GatewayManagementNotFoundResultException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public GatewayManagementNotFoundResultException(string message) : base(message)
        {

        }

        public GatewayManagementNotFoundResultException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        public GatewayManagementNotFoundResultException(string message, Exception innerException, string errorCode = null)
         : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
