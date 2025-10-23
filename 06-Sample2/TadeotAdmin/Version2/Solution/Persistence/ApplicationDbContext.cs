using AuthenticationBase.Persistence;

using Core.Entities.Visitors;

using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class ApplicationDbContext : BaseApplicationDbContext
{
    public ApplicationDbContext() : base()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Visitor>        Visitors        => Set<Visitor>();
    public DbSet<ReasonForVisit> ReasonsForVisit => Set<ReasonForVisit>();
    public DbSet<City>           Cities          => Set<City>();
    public DbSet<District>       Districts       => Set<District>();
    public DbSet<SchoolType>     SchoolTypes     => Set<SchoolType>();


    ///// <summary>
    ///// Für InMemory-DB in UnitTests
    ///// </summary>
    ///// <param name="options"></param>
    //public ApplicationDbContext(DbContextOptions<BaseApplicationDbContext> options) : base(options)
    //{
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //Season season = new Season { Id = 1, Name = "Herbst 2020", Start = DateTime.Parse("20.8.2020"), IsSpringSeason = false };
        //Season season2021 = new Season { Id = 2, Name = "Frühjahr 2021", Start = DateTime.Parse("12.3.2021"), IsSpringSeason = true };
        //modelBuilder.Entity<Season>().HasData(season);
        //modelBuilder.Entity<Season>().HasData(season2021);
    }
}