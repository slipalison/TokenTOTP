using FluentValidation;
using Responses;

namespace TokenTOTP.Infra.Extensions.Validations
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> IsDocument<T>(this IRuleBuilder<T, string> ruleBuilder) =>
            ruleBuilder.SetValidator(new DocumentValidator());

        public static IRuleBuilderOptions<T, string> IsPhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder) =>
            ruleBuilder.SetValidator(new PatternValidator(4, "^([0-9])([^A-Z][^a-z]*)$"));

        public static IRuleBuilderOptions<T, string> IsCountryCallingCode<T>(this IRuleBuilder<T, string> ruleBuilder) =>
            ruleBuilder.SetValidator(new PatternValidator(1, "^(\\+?\\d{1,6})$"));

        public static IRuleBuilderOptions<T, string> IsStrongPassword<T>(this IRuleBuilder<T, string> ruleBuilder) =>
            ruleBuilder.SetValidator(new PatternValidator(8, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-])?.{8,}$"));
    }
}