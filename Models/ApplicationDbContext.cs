using Microsoft.EntityFrameworkCore;

namespace KDSB_JMRH_NNNN_NDMM.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    }
}
