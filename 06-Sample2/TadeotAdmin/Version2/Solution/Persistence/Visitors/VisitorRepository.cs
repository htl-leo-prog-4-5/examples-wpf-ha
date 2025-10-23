using AuthenticationBase.Persistence.Repositories;

using Bogus;

using Core.Contracts.Visitors;
using Core.Entities.Visitors;

using Microsoft.EntityFrameworkCore;

namespace Persistence.Visitors;

public class VisitorRepository : GenericRepository<Visitor>, IVisitorRepository
{
    public ApplicationDbContext? DbContext { get; }

    public VisitorRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        DbContext = dbContext;
    }

    public async Task DeleteAllAsync()
    {
        // ExecuteSqlCommand

        await DbContext!.Database.ExecuteSqlRawAsync("truncate table visitors");
        DbContext!.ChangeTracker.Clear();
        //var visitors = await DbContext!.Visitors.ToListAsync();
        //DbContext!.RemoveRange(visitors);
    }

    public async Task GenerateTestDataAsync(int nrVisitors)
    {
        var cities  = await DbContext!.Cities.ToListAsync();
        var reasons = await DbContext!.ReasonsForVisit.ToListAsync();
        var types   = await DbContext!.SchoolTypes.ToListAsync();

        var visitorFaker = new Faker<Visitor>()
                .RuleFor(u => u.DateTime,       f => f.Date.Between(DateTime.Today, DateTime.Today.AddDays(1)))
                .RuleFor(u => u.IsMale,         f => f.Random.Bool(0.7f))
                .RuleFor(u => u.Adults,         f => f.Random.Number(0, 4))
                .RuleFor(u => u.Comment,        f => string.Join(' ', f.Lorem.Words(5)))
                .RuleFor(u => u.CityId,         f => f.PickRandom(cities).Id)
                .RuleFor(u => u.ReasonForVisit, f => f.PickRandom(reasons).Reason)
                .RuleFor(u => u.SchoolLevel,    f => f.Random.Number(7, 9))
                .RuleFor(u => u.SchoolType,     f => f.PickRandom(types).Type)
                .RuleFor(u => u.InterestHIF,    f => f.Random.Bool(0.7f))
                .RuleFor(u => u.InterestHITM,   f => f.Random.Bool(0.6f))
                .RuleFor(u => u.InterestHBG,    f => f.Random.Bool(0.3f))
                .RuleFor(u => u.InterestHEL,    f => f.Random.Bool(0.2f))
                .RuleFor(u => u.InterestFEL,    f => f.Random.Bool(0.05f))
            ;
        var visitors = visitorFaker.Generate(nrVisitors);
        await DbContext!.Visitors.AddRangeAsync(visitors);
    }

    public async Task<IEnumerable<Visitor>> GetAllUntrackedAsync()
    {
        return await DbContext!.Visitors
            .AsNoTracking()
            .Include(v => v.City)
            .ThenInclude(c => c!.District)
            .OrderBy(v => v.Id)
            .ToListAsync();
    }
}