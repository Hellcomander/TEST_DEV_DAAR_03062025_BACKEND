using Microsoft.EntityFrameworkCore;
using TEST_DEV_DAAR_03062025.Models;

namespace TEST_DEV_DAAR_03062025.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<PersonaFisica> PersonasFisicas { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
