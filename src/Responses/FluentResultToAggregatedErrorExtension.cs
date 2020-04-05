using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Responses
{
    [ExcludeFromCodeCoverage]
    public static class FluentResultToAggregatedErrorExtension
    {
        public static IMvcBuilder FluentResultToAggregatedError(this IMvcBuilder mvcBuilder, (string code, string message) errorCodeMessage)
        {
            FluentValidationToAbstractionErrorFilter.InvalidPayloadError = errorCodeMessage;
            return FluentResultToAggregatedError(mvcBuilder);
        }

        public static IMvcBuilder FluentResultToAggregatedError(this IMvcBuilder mvcBuilder) => mvcBuilder
                .AddMvcOptions(opt => AddFilter(opt))
                .ConfigureApiBehaviorOptions(SetupApiBehavior());

        public static IMvcCoreBuilder FluentResultToAggregatedError(this IMvcCoreBuilder mvcBuilder, (string code, string message) errorCodeMessage)
        {
            FluentValidationToAbstractionErrorFilter.InvalidPayloadError = errorCodeMessage;
            return FluentResultToAggregatedError(mvcBuilder);
        }

        public static IMvcCoreBuilder FluentResultToAggregatedError(this IMvcCoreBuilder mvcBuilder) => mvcBuilder
                .AddMvcOptions(opt => AddFilter(opt))
                .ConfigureApiBehaviorOptions(SetupApiBehavior());

        private static Action<ApiBehaviorOptions> SetupApiBehavior() => x => x.SuppressModelStateInvalidFilter = true;

        private static IFilterMetadata AddFilter(MvcOptions opt) => opt.Filters.Add<FluentValidationToAbstractionErrorFilter>();
    }
}