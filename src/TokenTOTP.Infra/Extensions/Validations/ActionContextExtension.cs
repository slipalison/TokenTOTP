using Microsoft.AspNetCore.Mvc;
using Responses;
using System.Collections.Generic;
using System.Linq;

namespace TokenTOTP.Infra.Extensions.Validations
{
    public static class ActionContextExtension
    {
        public static AggregatedError ToAggregatedError(this ActionContext context)
            => new AggregatedError(("400", "Has invalid values into request")) { Errors = context.BuildAggregatedErrors() };

        public static AggregatedError ToAggregatedError(this ActionContext context, (string, string) invalidPayloadError)
            => new AggregatedError(invalidPayloadError) { Errors = context.BuildAggregatedErrors() };

        private static IEnumerable<KeyValuePair<string, string>> BuildAggregatedErrors(this ActionContext context)
        {
            foreach (var error in context.BuildErrors())
                foreach (var item in error.Value)
                    yield return new KeyValuePair<string, string>(error.Key, item);
        }

        private static IEnumerable<KeyValuePair<string, IEnumerable<string>>> BuildErrors(this ActionContext context)
        {
            using (var en = context.ModelState.Select(ms => ms.Key).GetEnumerator())
                while (en.MoveNext())
                    yield return new KeyValuePair<string, IEnumerable<string>>(string.IsNullOrWhiteSpace(en.Current) ? "InvalidCast" : en.Current,
                        context.ModelState[en.Current].Errors.Select(err => string.IsNullOrWhiteSpace(err.ErrorMessage) ? err.Exception.Message : err.ErrorMessage));
        }
    }
}