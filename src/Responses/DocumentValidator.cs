using FluentValidation.Validators;

namespace Responses
{
    public class DocumentValidator : PropertyValidator
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

        private static bool IsCnpjValid(string cnpj)
        {
            var multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int sum;
            int rest;
            string digit;
            string cnpjTemp;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            cnpjTemp = cnpj.Substring(0, 12);
            sum = 0;

            for (var i = 0; i < 12; i++)
                sum += int.Parse(cnpjTemp[i].ToString()) * multiplier1[i];

            rest = sum % 11;
            rest = rest < 2 ? 0 : 11 - rest;
            digit = rest.ToString();
            cnpjTemp = cnpjTemp + digit;
            sum = 0;

            for (var i = 0; i < 13; i++)
                sum += int.Parse(cnpjTemp[i].ToString()) * multiplier2[i];

            rest = sum % 11;
            rest = rest < 2 ? 0 : 11 - rest;
            digit = digit + rest.ToString();

            return cnpj.EndsWith(digit);
        }

        private static bool IsCpfValid(string cpf)
        {
            var multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string cpfTemp;
            string digit;
            int sum;
            int rest;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            cpfTemp = cpf.Substring(0, 9);
            sum = 0;

            for (var i = 0; i < 9; i++)
                sum += int.Parse(cpfTemp[i].ToString()) * multiplier1[i];

            rest = sum % 11;
            rest = rest < 2 ? 0 : 11 - rest;
            digit = rest.ToString();
            cpfTemp = cpfTemp + digit;
            sum = 0;

            for (var i = 0; i < 10; i++)
                sum += int.Parse(cpfTemp[i].ToString()) * multiplier2[i];

            rest = sum % 11;
            rest = rest < 2 ? 0 : 11 - rest;
            digit = digit + rest.ToString();

            return cpf.EndsWith(digit);
        }
    }
}