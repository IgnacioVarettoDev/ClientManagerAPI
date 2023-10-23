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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasKey(e => e.clientId);
            modelBuilder.Entity<Client>()
                .HasIndex(e => e.email)
                .IsUnique();
            modelBuilder.Entity<Client>()
                .HasIndex(e => e.rut)
                .IsUnique();
        }
    }
}
