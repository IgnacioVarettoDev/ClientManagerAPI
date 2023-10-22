using System.ComponentModel.DataAnnotations;

namespace ClientManagerDTO.Validation
{
    public static class DateOfBirthValidation
    {
        public static IEnumerable<ValidationResult> ValidateAgeDateOfBirth(DateTime? dateOfBirth, int age)
        {
            if (dateOfBirth.HasValue)
            {
                var yearBirth = dateOfBirth.Value.Year;
                var yearNow = DateTime.Now.Year;

                if (yearNow - age != yearBirth)
                {
                    yield return new ValidationResult(errorMessage: "El campo Age no coincide con el campo DateOfBirth.", memberNames: new List<string>() { nameof(age) });
                }
            }
        }
    }
}
