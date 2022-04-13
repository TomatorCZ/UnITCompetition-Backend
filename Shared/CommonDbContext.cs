using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shared.Models;

namespace Shared.Models
{
    public class CommonDbContext : IdentityDbContext
    {
        //public DbSet<TelemetryData> TelemetryData { get; set; } = null!;

        public DbSet<Head> Heads { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Test> Tests { get; set; }


        public CommonDbContext(DbContextOptions<CommonDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"DataSource = UnIT.db;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder
                .Entity<Head>(e =>
                {
                    e.Property(p => p.ComponentVersions).HasConversion(
                        x => JsonConvert.SerializeObject(x),  //convert TO a json string
                        x => JsonConvert.DeserializeObject<List<Tuple<string, string>>>(x) //convert FROM a json string
                    );
                });
            modelBuilder
                .Entity<Head>()
                .HasNoKey()
                .ToTable("Heads");

            modelBuilder
                .Entity<Group>();

            modelBuilder
                .Entity<Test>(e =>
                {
                    e.Property(p => p.Operations).HasConversion(
                            x => JsonConvert.SerializeObject(x),  //convert TO a json string
                            x => JsonConvert.DeserializeObject<Operation>(x) //convert FROM a json string
                        );
                    e.Property(p => p.Config).HasConversion(
                            x => JsonConvert.SerializeObject(x),  //convert TO a json string
                            x => JsonConvert.DeserializeObject<Dictionary<string, string>>(x) //convert FROM a json string
                        );
                    e.Property(p => p.ErrorInfo).HasConversion(
                            x => JsonConvert.SerializeObject(x),  //convert TO a json string
                            x => JsonConvert.DeserializeObject<List<string>>(x) //convert FROM a json string
                        );
                });

            base.OnModelCreating(modelBuilder);
        }

    }
}
