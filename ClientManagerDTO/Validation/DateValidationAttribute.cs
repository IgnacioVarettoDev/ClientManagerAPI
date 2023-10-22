using System.ComponentModel.DataAnnotations;

namespace ClientManagerDTO.Validation
{
    public class DateValidationAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            if (value is DateTime date)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(errorMessage:"El valor no es una fecha válida.");
        }
    }
}
