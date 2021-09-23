using System.ComponentModel;

namespace CompanyName.GatewayManagement.Domain.Exceptions
{
    public enum ErrorCode
    {
        #region API
        [Description("Maximum 10 peripheral devices are allowed for a gateway.")]
        S001,
        [Description("No gateway added yet.")]
        S002,
        #endregion

        #region Exception

        [Description("General Exception.")]
        E9999,
        [Description("Result not found.")]
        E0404,


        #endregion

        #region Validation

        [Description("Value can not be empty.")]
        V1000,
        [Description("Invalid IPv4 address.")]
        V1100,

        #endregion
    }
}
