using Microsoft.EntityFrameworkCore;

namespace KDSB_JMRH_NNNN_NDMM.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
        }

        public DbSet<roles> roles { get; set; }

        public DbSet<PhoneNumbers> phoneNumbers { get; set; }

        public DbSet<Suppliers> suppliers { get; set; }

        public DbSet<users> users { get; set; }

    }
}
