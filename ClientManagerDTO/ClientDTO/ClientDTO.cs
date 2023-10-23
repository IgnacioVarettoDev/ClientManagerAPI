using System.ComponentModel.DataAnnotations;

namespace ClientManagerDTO.ClientDTO
{
    public class ClientDto: CreateClientDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public Guid ClientId { get; set; }
    }
}
