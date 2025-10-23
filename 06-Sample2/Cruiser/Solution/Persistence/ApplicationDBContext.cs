using Base.Tools;

using Core.Entities;

using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

namespace Persistence;

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

using Persistence.Mapping;

public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Parameterless constructor reads the connection string from appsettings.json (at design time)
    /// For migration generation! Note: The constructor must be the first one in order.
    /// </summary>
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ShippingCompany> Companies { get; set; }
    public DbSet<CruiseShip>      Ships { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //We need this for migration
            var connectionString = ConfigurationHelper.GetConfiguration().Get("DefaultConnection", "ConnectionStrings");
            optionsBuilder.UseSqlServer(connectionString);
        }

        optionsBuilder.LogTo(message => Debug.WriteLine(message));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShippingCompany>().Map();
        modelBuilder.Entity<ShipName>().Map();
        modelBuilder.Entity<CruiseShip>().Map();
    }


    public sealed class ApplicationDbContextFactory(string connectionString) : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] _)
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionString)
                .ConfigureWarnings(warnings =>
                    warnings.Throw(RelationalEventId
                        .MultipleCollectionIncludeWarning))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .LogTo(Console.WriteLine, LogLevel.Information);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}