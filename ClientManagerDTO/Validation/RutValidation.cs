using System.ComponentModel.DataAnnotations;

namespace ClientManagerDTO.Validation;

public class RutValidation: ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        var rut = value.ToString();

        if (rut==null)
        {
            return ValidationResult.Success;
        }

        rut = rut.ToUpper().Replace(".", "").Replace("-", "");

        if (rut.Contains("-") || rut.Length < 3)
        {
            return new ValidationResult(errorMessage: "RUT con formato incorrecto.");
        }

        char checkDigit = rut[rut.Length - 1];
        rut = rut.Substring(0, rut.Length - 1);

        if (checkDigit == 'K')
        {
            checkDigit = 'k';
        }

        int numericRut;

        if (!int.TryParse(rut, out numericRut))
        {
            return new ValidationResult(errorMessage: "RUT con formato incorrecto.");
        }

        int m = 0;
        int s = 1;

        while (numericRut != 0)
        {
            s = (s + numericRut % 10 * (9 - m++ % 6)) % 11;
            numericRut /= 10;
        }

        if (checkDigit == (s != 0 ? (char)(s + 47) : 'k'))
        {
            return ValidationResult.Success;

        }
        return new ValidationResult(errorMessage: "RUT con formato incorrecto.");
    }
}