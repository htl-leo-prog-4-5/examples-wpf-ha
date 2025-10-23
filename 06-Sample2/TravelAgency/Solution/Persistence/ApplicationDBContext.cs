using Base.Tools;

using Core.Entities;

using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

namespace Persistence;

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

    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Plane> Planes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ship>().Map();
        modelBuilder.Entity<Plane>().Map();
        modelBuilder.Entity<Trip>().Map();
        modelBuilder.Entity<Hotel>().Map();
        modelBuilder.Entity<Route>().Map();
        modelBuilder.Entity<RouteStep>().Map();
    }
}