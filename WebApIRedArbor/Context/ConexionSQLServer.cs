using Microsoft.EntityFrameworkCore;
using WebApIRedArbor.Models;

namespace WebApIRedArbor.Context
{
    public class ConexionSQLServer : DbContext
    {
        public ConexionSQLServer(DbContextOptions<ConexionSQLServer> options) : base(options) { }
        public DbSet<Status> Status { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Portal> Portal { get; set; }
        public DbSet<Company> Company { get; set; }
    }
}
