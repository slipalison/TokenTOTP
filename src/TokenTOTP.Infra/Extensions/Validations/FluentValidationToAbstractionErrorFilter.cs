using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TokenTOTP.Infra.Extensions.Validations
{
    public class FluentValidationToAbstractionErrorFilter : IActionFilter
    {
        public static (string code, string message) InvalidPayloadError = ("404", "Requisição inválida");

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