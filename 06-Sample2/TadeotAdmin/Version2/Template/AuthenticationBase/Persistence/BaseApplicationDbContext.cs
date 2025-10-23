using AuthenticationBase.Entities;
using AuthenticationBase.Helper;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationBase.Persistence;

public class BaseApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public BaseApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public BaseApplicationDbContext() : base()
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    public DbSet<Session>         Sessions         => Set<Session>();


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //string dbFileName = configuration["ConnectionStrings:DbFileName"];
            //optionsBuilder.UseSqlite($"Data Source={dbFileName}");
            ////optionsBuilder.UseLoggerFactory(GetLoggerFactory());

            string connectionString = ConfigurationHelper.GetConfiguration("DefaultConnection", "ConnectionStrings");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}