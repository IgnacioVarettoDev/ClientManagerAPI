using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ClientManagerDTO.ClientDTO;
using static ClientManager.Constant.NameEndpointMethods.NameEndpointClient;
using ExceptionHandler = ClientManagerDAO.Exceptions.ExceptionHandler;
using ClientManagerDTO.Entity;

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
        public async Task<ActionResult<List<ClientDTO>>> GetAllClient()
        {
            try
            {
                var clientList = await _context.Client.ToListAsync();

                if (clientList.Count < 1)
                {
                    return NotFound("No se encontraron clientes.");
                }

                var clientListMapping = _mapper.Map<List<ClientDTO>>(clientList);

                return Ok(clientListMapping);
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
        public async Task<ActionResult<ClientDTO>> GetOneClientById([FromRoute] Guid clientId)
        {
            try
            {
                var existClient = await _context.Client.FirstOrDefaultAsync(client => client.clientId == clientId);
                if (existClient == null)
                {
                    return NotFound("No se encontro el cliente.");
                }
                var clientMapping = _mapper.Map<ClientDTO>(existClient);

                return Ok(clientMapping);
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
        public async Task<ActionResult<ClientDTO[]>> GetSearchClient([FromQuery] string search)
        {
            try
            {
                var results = await _context.Client
                    .Where(e =>
                        e.rut.Contains(search) ||
                        e.firstName.Contains(search) ||
                        e.lastName.Contains(search) ||
                        e.married.ToString().Contains(search) ||
                        e.address.Contains(search) ||
                        (e.phoneNumber != null && e.phoneNumber.Contains(search)) ||
                        e.email.Contains(search) || 
                        (e.dateOfBirth != null && e.dateOfBirth.ToString().Contains(search))||
                        e.age.ToString().Contains(search)
                    )
                    .ToListAsync();

                if (results.Count < 1)
                {
                    return NotFound("No se encontro informacion con esta busqueda.");
                }
                var clientListMapping = _mapper.Map<ClientDTO>(results);

                return Ok(clientListMapping);
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
                client.clientId = Guid.NewGuid();
                client.registerClient = DateTime.Now;
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
        public async Task<ActionResult> UpdateOneClient([FromRoute] Guid clientId, [FromBody] UpdateClientDTO clientUpdate)
        {
            try
            {
                //var existClient = await _context.Client.AnyAsync(clientAny => clientAny.clientId == clientId);

                var existClient = await _context.Client.FirstOrDefaultAsync(client => client.clientId == clientId);
                if (existClient == null)
                {
                    return NotFound("No se encontro el cliente.");
                }

                _mapper.Map(clientUpdate, existClient);

                existClient.updateClient = DateTime.Now;

                _context.Entry(existClient).State = EntityState.Modified;

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
                var existClient = await _context.Client.AnyAsync(clientAny => clientAny.clientId == clientId);
                if (!existClient)
                {
                    return NotFound("No se encontro el cliente.");
                }

                _context.Remove(entity: new Client() { clientId = clientId });

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
