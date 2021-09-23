using System;
using System.Runtime.Serialization;

namespace CompanyName.GatewayManagement.Domain.Exceptions
{
    [Serializable]
    public class GatewayManagementException : Exception
    {
        protected GatewayManagementException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public GatewayManagementException(string message) : base(message)
        {
        }

        public GatewayManagementException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
