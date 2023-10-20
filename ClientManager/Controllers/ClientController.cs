using ClientManager.Constant;
using ClientManagerDTO.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static ClientManager.Constant.NameEndpointMethods;
using static ClientManager.Constant.NameEndpointMethods.NameEndpointClient;

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
                    return NotFound();
                }
                return Ok(clientList);
            }
            catch (Exception)
            {
                return BadRequest(error: "Error en la ejecucion de peticion HTTP.");
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
                    return NotFound();
                }

                return Ok(existClient);
            }
            catch (Exception e)
            {
                return BadRequest(error: "Error en el cliente");
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
            catch (Exception e)
            {
                return BadRequest(error: "Error en el cliente " + e);
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
                    return NotFound();
                }

                _context.Update(client);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(error: "Error en el cliente");
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
                    return NotFound();
                }
                _context.Remove(entity:new Client() { ClientId = clientId });
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(error: "Error en el cliente");
            }
        }
    }
}
