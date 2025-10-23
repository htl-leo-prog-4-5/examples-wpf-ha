using Core.Entities;

using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

namespace Persistence;

using Base.Tools;

public class ApplicationDbContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Room>     Rooms     => Set<Room>();
    public DbSet<Booking>  Bookings  => Set<Booking>();

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
    }
}