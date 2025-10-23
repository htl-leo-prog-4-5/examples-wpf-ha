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

    [ObsoleteAttribute("This is only used to generate ASP.net CRUD Page. Must not be used. Please use UOW instead.", true)]
    public DbSet<Station>        Stations         { get; set; }
    [ObsoleteAttribute("This is only used to generate ASP.net CRUD Page. Must not be used. Please use UOW instead.", true)]
    public DbSet<Infrastructure> Infrastructures  { get; set; }
    [ObsoleteAttribute("This is only used to generate ASP.net CRUD Page. Must not be used. Please use UOW instead.", true)]
    public DbSet<RailwayCompany> RailwayCompanies { get; set; }
    [ObsoleteAttribute("This is only used to generate ASP.net CRUD Page. Must not be used. Please use UOW instead.", true)]
    public DbSet<City>           Cities           { get; set; }
    [ObsoleteAttribute("This is only used to generate ASP.net CRUD Page. Must not be used. Please use UOW instead.", true)]
    public DbSet<Line>           Lines            { get; set; }

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
        modelBuilder.Entity<Station>().Map();
        modelBuilder.Entity<Infrastructure>().Map();
        modelBuilder.Entity<RailwayCompany>().Map();
        modelBuilder.Entity<City>().Map();
        modelBuilder.Entity<Line>().Map();
    }
}