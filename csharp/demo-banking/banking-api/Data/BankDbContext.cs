using banking_api.Models;
using Microsoft.EntityFrameworkCore;

namespace banking_api.Data
{
    public class BankDbContext(DbContextOptions<BankDbContext> options) : DbContext(options)
    {
        public DbSet<Organisation> Organisations => Set<Organisation>();

        //public DbSet<Models.Account> Accounts { get; set; }
        //public DbSet<Models.Programme> Programmes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("banking");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseAsyncSeeding(async (context, _, cancellationToken) =>
                    {
                        var sampleOrganisation = await context.Set<Organisation>().FirstOrDefaultAsync(o => o.Name == "Sample Organisation");

                        if (sampleOrganisation == null)
                        {
                            var newOrganisation = Organisation.Create("Sample Organisation", "123 Sample Street", "sample@organisation.com");
                            await context.Set<Organisation>().AddAsync(newOrganisation, cancellationToken);
                            await context.SaveChangesAsync(cancellationToken);
                        }
                    })
                    .UseSeeding((context, _) =>
                    {
                        var sampleOrganisation = context.Set<Organisation>().FirstOrDefaultAsync(o => o.Name == "Sample Organisation");

                        if (sampleOrganisation == null)
                        {
                            var newOrganisation = Organisation.Create("Sample Organisation", "123 Sample Street", "sample@organisation.com");
                            context.Set<Organisation>().AddAsync(newOrganisation);
                            context.SaveChangesAsync();
                        }
                    });
            }
        }
    }
}
