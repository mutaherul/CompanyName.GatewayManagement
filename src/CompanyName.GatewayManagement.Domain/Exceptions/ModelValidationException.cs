using FluentValidation.Results;
using System;
using System.Runtime.Serialization;

namespace CompanyName.GatewayManagement.Domain.Exceptions
{
    [Serializable]
    public class ModelValidationException : GatewayManagementException
    {
        public ValidationResult ValidationResult { get; private set; }

        protected ModelValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ModelValidationException(string message, ValidationResult result)
            : base(message)
        {
            ValidationResult = result;
        }
    }
}
