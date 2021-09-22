using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using CompanyName.GatewayManagement.Api.Presenter;
using CompanyName.GatewayManagement.Domain.Exceptions;
using System.Collections.Generic;

namespace CompanyName.GatewayManagement.Api.Filters
{
    public class GatewayManagementExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<GatewayManagementExceptionFilterAttribute> _logger;

        public GatewayManagementExceptionFilterAttribute(ILogger<GatewayManagementExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        #region Overrides of ExceptionFilterAttribute

        public override void OnException(ExceptionContext context)
        {

            var gatewayManagementForbiddenRequestException = context.Exception is GatewayManagementForbiddenRequestException;
            var gatewayManagementNotFoundResultException = context.Exception is GatewayManagementNotFoundResultException;
            var gatewayManagementInvalidArgumentException = context.Exception is GatewayManagementInvalidArgumentException;
            var modelValidationException = context.Exception is ModelValidationException;


            if (gatewayManagementForbiddenRequestException)
            {
                HandleException(StatusCodes.Status403Forbidden, context, ((GatewayManagementForbiddenRequestException)context.Exception).ErrorCode);
            }
            else if (gatewayManagementNotFoundResultException)
            {
                HandleException(StatusCodes.Status404NotFound, context, ((GatewayManagementNotFoundResultException)context.Exception).ErrorCode);
            }
            else if (gatewayManagementInvalidArgumentException)
            {
                HandleException(StatusCodes.Status400BadRequest, context, ((GatewayManagementInvalidArgumentException)context.Exception).ErrorCode);
            }
            else if (modelValidationException)
            {
                HandleModelValidationException(StatusCodes.Status400BadRequest, context);
            }
            else
            {
                HandleException(StatusCodes.Status500InternalServerError, context, ErrorCode.E9999.ToString());
            }

            base.OnException(context);
        }

        #endregion

        #region Custom Exception Handle

        private void HandleException(int statusCode, ExceptionContext context, string errorCode)
        {
            _logger.LogError(context.Exception.Message, context.Exception);
            SetExceptionToResponse(statusCode, context, errorCode);
        }

        private void HandleModelValidationException(int statusCode, ExceptionContext context)
        {
            _logger.LogError(context.Exception.Message, context.Exception);
            SetModelValidationExceptionToResponse(statusCode, context);
        }

        private void SetExceptionToResponse(int statusCode, ExceptionContext context, string errorCode)
        {
            SetResponseAndStatusCode(statusCode, context);

            JsonResult jsonResult = new JsonResult(new EnvelopeError() { error = new Error() { message = context.Exception.Message, code = errorCode } })
            {
                ContentType = "application/json"
            };

            context.Result = jsonResult;
        }
        private void SetModelValidationExceptionToResponse(int statusCode, ExceptionContext context)
        {
            SetResponseAndStatusCode(statusCode, context);

            JsonResult jsonResult = new JsonResult(new EnvelopeError() { error = new Error() { message = context.Exception.Message, errorDetails = GetErrorDetails(context) } })
            {
                ContentType = "application/json"
            };

            context.Result = jsonResult;
        }
        private List<ErrorDetails> GetErrorDetails(ExceptionContext context)
        {
            var errorDetails = new List<ErrorDetails>();
            var validationResult = ((ModelValidationException)context.Exception).ValidationResult;

            if (validationResult == null)
                return errorDetails;

            foreach (var error in validationResult.Errors)
            {
                errorDetails.Add(new ErrorDetails() { AttemptedValue = error.AttemptedValue, ErrorMessage = error.ErrorMessage, ErrorCode = error.ErrorCode, PropertyName = error.PropertyName });
            }

            return errorDetails;
        }
        private void SetResponseAndStatusCode(int statusCode, ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = statusCode;
            context.HttpContext.Response.ContentType = "application/json";
        }

        #endregion
    }
}
