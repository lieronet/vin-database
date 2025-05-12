using Microsoft.EntityFrameworkCore;
using vin_db.Domain;

namespace vin_db.Repos
{
    public class VinDbContext : DbContext
    {
        public VinDbContext(DbContextOptions<VinDbContext> options)
            : base(options)
        {
        }

        public DbSet<VinDetail> VinDetails { get; set; }
        public DbSet<VinQueue> VinQueue { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VinQueue>()
                .ToTable("VinQueue")
                .HasKey(v => v.Id);
            modelBuilder.Entity<VinDetail>()
                .ToTable("VinDetail")
                .HasKey(v => v.Vin);
        }
    }
}
