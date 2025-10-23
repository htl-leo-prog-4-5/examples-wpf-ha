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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}