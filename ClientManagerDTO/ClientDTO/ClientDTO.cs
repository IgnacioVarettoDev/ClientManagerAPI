using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagerDTO.ClientDTO
{
    public class ClientDTO: CreateClientDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public Guid ClientId { get; set; }
    }
}
