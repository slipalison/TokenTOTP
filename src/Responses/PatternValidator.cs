using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace Responses
{
    public class PatternValidator : PropertyValidator
    {
        private readonly string _pattern;
        private readonly int _length;

        public PatternValidator(int length, string pattern) : base("{PropertyName} is invalid.")
        {
            _length = length;
            _pattern = pattern;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (!(context.PropertyValue is string document)) { return false; }

            if (document.Length < _length) return false;

            var regex = new Regex(_pattern, RegexOptions.Compiled);

            var match = regex.Match(document);

            return match.Success;
        }
    }
}