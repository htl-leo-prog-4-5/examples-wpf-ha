using Core.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System.Diagnostics;

namespace Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Pupil>   Pupils   => Set<Pupil>();
    public DbSet<Locker>  Lockers  => Set<Locker>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            Debug.Write(configuration.ToString());
            var connectionString = configuration["ConnectionStrings:DefaultConnection"];
            optionsBuilder.UseSqlServer(connectionString);
            //optionsBuilder.UseLoggerFactory(GetLoggerFactory());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}