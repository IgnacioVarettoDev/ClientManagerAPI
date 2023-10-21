using ClientManagerDTO.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static ClientManager.Constant.NameEndpointMethods.NameEndpointClient;
using ExceptionHandler = ClientManagerDAO.Exceptions.ExceptionHandler;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClientManager.Controllers
{
    [Route("api/Client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ClientController> _logger;
        public ClientController(ApplicationDbContext context, ILogger<ClientController> logger) {
            _context = context;
            _logger = logger;
        }

        [HttpGet(template:"ListClient", Name = GetAll)]
        public async Task<ActionResult<List<Client>>> GetAllClient()
        {
            try
            {   
                var clientList = await _context.Client.ToListAsync();
                if (clientList.Count < 1)
                {
                    return NotFound("No se encontraron clientes.");
                }

                return Ok(clientList);
            }
            catch (ValidationException)
            {
                return BadRequest(error:"La solicitud es inválida.");
            }
            catch (Exception)
            {
                return StatusCode(500, value:"Se produjo un error interno en el servidor.");
            }
        }

        [HttpGet(template:"{clientId:guid}", Name = GetOneById)]
        public async Task<ActionResult<Client>> GetOneClientById([FromRoute] Guid clientId)
        {
            try
            {
                var existClient = await _context.Client.FirstOrDefaultAsync(client => client.ClientId == clientId);
                if (existClient == null)
                {
                    return NotFound("No se encontro el cliente.");
                }

                return Ok(existClient);
            }
            catch (ValidationException)
            {
                return BadRequest(error: "La solicitud es inválida.");
            }
            catch (Exception)
            {
                return StatusCode(500, value: "Se produjo un error interno en el servidor.");
            }
        }

        [HttpGet(template:"search", Name = GetSearch)]
        public string GetSearchClient([FromQuery] string name)
        {
            throw new NotImplementedException();
            return name;
        }

        [HttpPost(Name = CreateNew)]
        public async Task<ActionResult>  CreateNewClient([FromBody] Client client)
        {
            try
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (ValidationException)
            {
                return BadRequest(error: "La solicitud es inválida.");
            }
            catch (DbUpdateException ex)
            {
                if (ExceptionHandler.IsUniqueViolationException(ex))
                {
                    return BadRequest(error:"El registro tiene valores que ya existen en la Base de datos. Puede ser Rut o Email");
                }

                return StatusCode(500, value:"Se produjo un error interno en el servidor.");
            }
            catch (Exception)
            {
                return StatusCode(500, value: "Se produjo un error interno en el servidor.");
            }
        }

        [HttpPut(template: "{clientId:guid}", Name = UpdateOne)]
        public async Task<ActionResult> UpdateOneClient([FromRoute] Guid clientId, [FromBody] Client client)
        {
            try
            {
                if (client.ClientId != clientId)
                {
                    return BadRequest(error: "El ID del cliente no coincide con el ID de la URL.");
                }

                var existClient = await _context.Client.AnyAsync(clientAny => clientAny.ClientId == clientId);

                if (!existClient)
                {
                    return NotFound("No se encontro el cliente.");
                }

                _context.Update(client);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (ValidationException)
            {
                return BadRequest(error: "La solicitud es inválida.");
            }
            catch (Exception)
            {
                return StatusCode(500, value: "Se produjo un error interno en el servidor.");
            }
        }

        [HttpDelete(template: "{clientId:guid}", Name = DeleteOneById)]
        public async Task<ActionResult> DeleteOneClientById([FromRoute] Guid clientId)
        {

            try
            {
                var existClient = await _context.Client.AnyAsync(clientAny => clientAny.ClientId == clientId);
                if (!existClient)
                {
                    return NotFound("No se encontro el cliente.");
                }
                _context.Remove(entity:new Client() { ClientId = clientId });
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (ValidationException)
            {
                return BadRequest(error: "La solicitud es inválida.");
            }
            catch (Exception)
            {
                return StatusCode(500, value: "Se produjo un error interno en el servidor.");
            }
        }
    }
}
