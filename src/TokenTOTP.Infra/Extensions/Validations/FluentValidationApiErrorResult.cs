using System.Collections.Generic;

namespace TokenTOTP.Infra.Extensions.Validations
{
    internal class FluentValidationApiErrorResult
    {
        public FluentValidationApiErrorResult(IEnumerable<KeyValuePair<string, IEnumerable<string>>> errors) => Errors = errors;

        public IEnumerable<KeyValuePair<string, IEnumerable<string>>> Errors { get; }
    }
}