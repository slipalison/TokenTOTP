using Acesso.Sdk.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static TokenTOTP.API.ErrorMessages;

namespace TokenTOTP.API.Infra.Filters
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class CorrelationIdValidateActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Method intentionally left empty.
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (string.IsNullOrWhiteSpace(context.HttpContext.Request.Headers["X-Correlation-ID"]))
                context.Result = new BadRequestObjectResult(new Error(InvalidCorrelationID));
        }
    }
}