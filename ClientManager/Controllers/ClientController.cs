using ClientManagerDTO.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ClientManagerDTO.ClientDTO;
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
        private readonly IMapper _mapper;

        public ClientController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet(template: "ListClient", Name = GetAll)]
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
                return BadRequest(error: "La solicitud es inválida.");
            }
            catch (Exception)
            {
                return StatusCode(500, value: "Se produjo un error interno en el servidor.");
            }
        }

        [HttpGet(template: "{clientId:guid}", Name = GetOneById)]
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

        [HttpGet(template: "search", Name = GetSearch)]
        public async Task<ActionResult<Client[]>> GetSearchClient([FromQuery] string search, Client e)
        {
            try
            {

                var results = await _context.Client
                    .Where(e =>
                        e.Rut.Contains(search) ||
                        e.FirstName.Contains(search) ||
                        e.LastName.Contains(search) ||
                        e.Married.ToString().Contains(search) ||
                        e.Address.Contains(search) ||
                        (e.PhoneNumber != null && e.PhoneNumber.Contains(search)) ||
                        e.Email.Contains(search) || 
                        (e.DateOfBirth != null && e.DateOfBirth.ToString().Contains(search))||
                        e.Age.ToString().Contains(search)
                    )
                    .ToListAsync();

                if (results.Count < 1)
                {
                    return NotFound("No se encontro informacion con esta busqueda.");
                }

                return Ok(results);
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


        [HttpPost(Name = CreateNew)]
        public async Task<ActionResult> CreateNewClient([FromBody] CreateClientDTO clientCreate)
        {
            try
            {
                var client = _mapper.Map<Client>(clientCreate);
                client.ClientId = Guid.NewGuid();
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
                    return BadRequest(error: "El registro tiene valores que ya existen en la Base de datos. Puede ser Rut o Email");
                }

                return StatusCode(500, value: "Se produjo un error interno en el servidor.");
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
                _context.Remove(entity: new Client() { ClientId = clientId });
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
