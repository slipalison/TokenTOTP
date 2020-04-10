using FluentValidation.Validators;

namespace TokenTOTP.Infra.Extensions.Validations
{
    public partial class DocumentValidator : PropertyValidator
    {
        public DocumentValidator() : base("{PropertyName} is invalid.")
        { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            if (!(context.PropertyValue is string document))
                return false;

            if (string.IsNullOrEmpty(document))
                return false;

            if (document.Length == 11)
                return IsCpfValid(document);

            if (document.Length == 14)
                return IsCnpjValid(document);

            return false;
        }

        public static bool IsCnpjValid(Cnpj cnpj)
            => cnpj.EhValido;

        public static bool IsCpfValid(Cpf sourceCPF) =>
            sourceCPF.EhValido;
    }
}
