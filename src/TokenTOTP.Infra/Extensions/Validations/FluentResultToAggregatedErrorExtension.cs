﻿using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace TokenTOTP.Infra.Extensions.Validations
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

        private static void AddFilter(MvcOptions opt) => opt.Filters.Add<FluentValidationToAbstractionErrorFilter>();
    }
}
