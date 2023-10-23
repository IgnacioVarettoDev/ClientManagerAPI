using AutoMapper;
using ClientManager;
using ClientManagerDTO.ClientDTO;
using ClientManagerDTO.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ClientManagerDao.ClientManager
{
    public class ClientDao
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClientDao(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ClientDto>> GetAllClients()
        {
            var clientList = await _context.Client.ToListAsync();

            var clientListMapping = _mapper.Map<List<ClientDto>>(clientList);

            return clientListMapping;
        }

        public async Task<ClientDto?> GetClientById(Guid clientId)
        {
            var existClient = await _context.Client.FirstOrDefaultAsync(client => client.clientId == clientId);
            if (existClient == null)
            {
                throw new Exception("12321");
            }
            var clientMapping = _mapper.Map<ClientDto>(existClient);

            return clientMapping;
        }

        public async Task<List<ClientDto[]>> SearchClients(string search)
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
                    (e.dateOfBirth != null && e.dateOfBirth.ToString().Contains(search)) ||
                    e.age.ToString().Contains(search)
                )
                .ToListAsync();

            var clientListMapping = _mapper.Map<List<ClientDto[]>>(results);

            return clientListMapping;
        }

        public async Task CreateNewClient(CreateClientDto clientCreate)
        {
            var client = _mapper.Map<Client>(clientCreate);
            client.clientId = Guid.NewGuid();
            client.registerClient = DateTime.Now;
            _context.Add(client);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateClient(Guid clientId, UpdateClientDto clientUpdate)
        {
            var existClient = await _context.Client.FirstOrDefaultAsync(client => client.clientId == clientId);
            if (existClient == null)
            {
                throw new Exception("No se encontro el cliente.");
            }

            _mapper.Map(clientUpdate, existClient);

            existClient.updateClient = DateTime.Now;

            _context.Entry(existClient).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteClientById(Guid clientId)
        {
            var existClient = await _context.Client.AnyAsync(clientAny => clientAny.clientId == clientId);
            if (!existClient)
            {
                throw new Exception("No se encontro el cliente.");
            }

            _context.Remove(entity: new Client() { clientId = clientId });

            await _context.SaveChangesAsync();
        }
    }
}
