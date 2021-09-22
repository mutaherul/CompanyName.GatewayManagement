using System.Collections.Generic;

namespace CompanyName.GatewayManagement.Api.Presenter
{
    public class EnvelopeError
    {
        public Error error { get; set; }
    }

    public class Error
    {
        public Error()
        {
        }

        public Error(string message, string code)
        {
            this.message = message;
            this.code = code;
        }

        public string message { get; set; }
        public string code { get; set; }
        public List<ErrorDetails> errorDetails { get; set; }
    }

    public class ErrorDetails
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
        public object AttemptedValue { get; set; }
        public string ErrorCode { get; set; }
    }
}
