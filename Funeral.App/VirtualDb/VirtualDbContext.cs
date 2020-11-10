using Microsoft.EntityFrameworkCore;

namespace Funeral.App.VirtualDb
{
    public class VirtualDbContext : DbContext
    {       
        public VirtualDbContext()
        {            
        }

        public VirtualDbContext(DbContextOptions<VirtualDbContext> options) : base(options)
        {

        }

        public DbSet<TempData> TempDatas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
