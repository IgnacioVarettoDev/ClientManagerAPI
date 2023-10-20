using ClientManagerDTO.ClientDTO;
using ClientManagerDTO.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClientManager.Controllers
{
    [Route("api/Client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ClientController(ApplicationDbContext context) {
            _context = context;
        }

        // GET: api/<ClientController>
        [HttpGet(Name = "getAllClient")]
        public async Task<ActionResult<List<Client>>> GetAllClient()
        {
            try
            {
                return await _context.Client.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET api/<ClientController>/5
        [HttpGet("{clientID:string}", Name = "getOneClientById")]
        public async Task<ActionResult<Client>> GetOneClientById([FromRoute] string clientId)
        {
            return await _context.Client.FirstOrDefaultAsync(client => client.ClientId == clientId);
        }

        // GET api/<ClientController>/5
        [HttpGet("search", Name = "getSearchClient")]
        public string GetSearchClient([FromQuery] string name)
        {
            return name;
        }

        // POST api/<ClientController>
        [HttpPost(Name = "createNewClient")]
        public async Task<ActionResult>  CreateNewClient([FromBody] Client client)
        {
            _context.Add(client);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<ClientController>/5
        [HttpPut("{clientID:string}", Name = "updateOneClient")]
        public async Task<ActionResult> UpdateOneClient([FromRoute] string clientId, [FromBody] Client client)
        {
            if (client.ClientId != clientId)
            {
                return BadRequest("El ID del cliente no coincide con el ID de la URL.");
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

        // DELETE api/<ClientController>/5
        [HttpDelete("{clientID:string}", Name = "deleteOneClientById")]
        public async Task<ActionResult> DeleteOneClientById([FromRoute] string clientId)
        {
            var existClient  = await _context.Client.AnyAsync(clientID => clientID.ClientId == clientId);
            if (!existClient)
            {
               return NotFound(); 
            }
            _context.Remove(new Client() { ClientId = clientId });
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
