using ClientManagerDTO.Entity;
using Microsoft.EntityFrameworkCore;

namespace ClientManager
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Client> Client { get; set; }   
    }
}
