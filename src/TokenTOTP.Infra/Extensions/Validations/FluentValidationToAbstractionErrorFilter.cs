using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TokenTOTP.Infra.Extensions.Validations
{
    public class FluentValidationToAbstractionErrorFilter : IActionFilter
    {
#pragma warning disable S2223 // Non-constant static fields should not be visible
#pragma warning disable S1104 // Fields should not have public accessibility
        public static (string code, string message) InvalidPayloadError = ("404", "Requisição inválida");
#pragma warning restore S1104 // Fields should not have public accessibility
#pragma warning restore S2223 // Non-constant static fields should not be visible

        public FluentValidationToAbstractionErrorFilter()
        {
        }

        public void OnActionExecuted(ActionExecutedContext context) => InvalidModelState(context);

        public void OnActionExecuting(ActionExecutingContext context) => InvalidModelState(context);

        private static void InvalidModelState(ActionExecutedContext context)
        {
            var statusCode = 404;
            if (context.Result is ForbidResult)
                statusCode = 403;
            if (context.Result is ObjectResult objectResult)
                statusCode = objectResult.StatusCode ?? statusCode;
            if (context.Result is StatusCodeResult statusCodeResult)
                statusCode = statusCodeResult.StatusCode;

            if (!context.ModelState.IsValid)
                context.Result = new ObjectResult(context.ToAggregatedError(InvalidPayloadError))
                {
                    StatusCode = statusCode
                };
        }

        private static void InvalidModelState(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new BadRequestObjectResult(context.ToAggregatedError(InvalidPayloadError));
        }
    }
}
