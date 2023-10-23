using ClientManagerDTO.Validation;
using System.ComponentModel.DataAnnotations;
using static ClientManagerDTO.Validation.DateOfBirthValidation;
namespace ClientManagerDTO.ClientDTO;

public class UpdateClientDto : IValidatableObject
{
    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [StringLength(maximumLength: 30, ErrorMessage = "El campo {0} no debe tener más de {1} caracteres.")]
    [WordValidation]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [StringLength(maximumLength: 30, ErrorMessage = "El campo {0} no debe tener más de {1} caracteres.")]
    [WordValidation]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "El campo {0} es requerido.")]
    public bool Married { get; set; } = false;

    [DateValidation]
    public DateTime? DateOfBirth { get; set; } = DateTime.MinValue;

    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [Range(18, 120, ErrorMessage = "El campo {0} debe estar entre {1} y {2}")]
    public int Age { get; set; } = 0;

    [Required(ErrorMessage = "El campo {0} es requerido.")]
    public string Address { get; set; } = string.Empty;

    [Phone(ErrorMessage = "El campo {0} debe ser un número telefónico.")]
    public string? PhoneNumber { get; set; }

    [Required(ErrorMessage = "El campo {0} es requerido.")]
    [EmailAddress(ErrorMessage = "El campo {0} debe ser un correo electrónico.")]
    public string Email { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        return ValidateAgeDateOfBirth(DateOfBirth, Age);
    }
}