using ClientManagerDTO.Validation;
using System.ComponentModel.DataAnnotations;

namespace ClientManagerDTO.ClientDTO
{
    public class CreateClientDto : UpdateClientDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MinLength(9, ErrorMessage = "El campo {0} debe tener como mínimo {1} caracteres.")]
        [MaxLength(10, ErrorMessage = "El campo {0} no debe tener más de {1} caracteres.")]
        [RutValidation]
        public string Rut { get; set; } = string.Empty;
    }
}
