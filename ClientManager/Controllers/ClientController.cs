using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ClientManagerDTO.ClientDTO;
using static ClientManager.Constant.NameEndpointMethods.NameEndpointClient;
using ExceptionHandler = ClientManagerDAO.Exceptions.ExceptionHandler;
using ClientManagerDao.ClientManager;

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
        public async Task<ActionResult<List<ClientDto>>> GetAllClient()
        {
            try
            {
                var clientDao = new ClientDao(_context, _mapper);
                var result = await clientDao.GetAllClients();

                if (result.Count < 1)
                {
                    return NotFound("No se encontraron clientes.");
                }

                return Ok(result);
            }
            catch (ValidationException)
            {
                return BadRequest("La solicitud es inválida.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Se produjo un error interno en el servidor.");
            }
        }

        [HttpGet(template: "{clientId:guid}", Name = GetOneById)]
        public async Task<ActionResult<ClientDto>> GetOneClientById([FromRoute] Guid clientId)
        {
            try
            {
                var clientDao = new ClientDao(_context, _mapper);
                var result = await clientDao.GetClientById(clientId);

                if (result == null)
                {
                    return NotFound("No se encontró el cliente.");
                }

                return Ok(result);
            }
        
            catch (ValidationException)
            {
                return BadRequest("La solicitud es inválida.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Se produjo un error interno en el servidor.");
            }
        }

        [HttpGet(template: "search", Name = GetSearch)]
        public async Task<ActionResult<ClientDto[]>> GetSearchClient([FromQuery] string search)
        {
            try
            {
                var clientDao = new ClientDao(_context, _mapper);
                var results = await clientDao.SearchClients(search);

                if (results.Count < 1)
                {
                    return NotFound("No se encontró información con esta búsqueda.");
                }

                return Ok(results);
            }
            catch (ValidationException)
            {
                return BadRequest("La solicitud es inválida.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Se produjo un error interno en el servidor.");
            }
        }

        [HttpPost(Name = CreateNew)]
        public async Task<ActionResult> CreateNewClient([FromBody] CreateClientDto clientCreate)
        {
            try
            {
                var clientDao = new ClientDao(_context, _mapper);
                await clientDao.CreateNewClient(clientCreate);
                return Ok();
            }
            catch (ValidationException)
            {
                return BadRequest("La solicitud es inválida.");
            }
            catch (DbUpdateException ex) when (ExceptionHandler.IsUniqueViolationException(ex))
            {
                return BadRequest("El registro tiene valores que ya existen en la Base de datos. Puede ser Rut o Email.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Se produjo un error interno en el servidor.");
            }
        }

        [HttpPut(template: "{clientId:guid}", Name = UpdateOne)]
        public async Task<ActionResult> UpdateOneClient([FromRoute] Guid clientId, [FromBody] UpdateClientDto clientUpdate)
        {
            try
            {
                var clientDao = new ClientDao(_context, _mapper);
                await clientDao.UpdateClient(clientId, clientUpdate);
                return Ok();
            }
            catch (ValidationException)
            {
                return BadRequest("La solicitud es inválida.");
            }
            catch (DbUpdateException ex) when (ExceptionHandler.IsUniqueViolationException(ex))
            {
                return BadRequest("El registro tiene valores que ya existen en la Base de datos. Puede ser Rut o Email.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Se produjo un error interno en el servidor.");
            }
        }

        [HttpDelete(template: "{clientId:guid}", Name = DeleteOneById)]
        public async Task<ActionResult> DeleteOneClientById([FromRoute] Guid clientId)
        {
            try
            {
                var clientDao = new ClientDao(_context, _mapper);
                await clientDao.DeleteClientById(clientId);
                return Ok();
            }
            catch (ValidationException)
            {
                return BadRequest("La solicitud es inválida.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Se produjo un error interno en el servidor.");
            }
        }
    }
}
