using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackendWebAPI.Services
{
    public class CommonDbContext : IdentityDbContext
    {
        //public DbSet<TelemetryData> TelemetryData { get; set; } = null!;

        public CommonDbContext(DbContextOptions<CommonDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder
            //    .Entity<TelemetryData>()
            //    .ToTable("TelemetryData");

            base.OnModelCreating(modelBuilder);
        }

    }
}
