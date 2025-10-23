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

        throw new NotImplementedException();
    }

    public async Task GenerateTestDataAsync(int nrVisitors)
    {
        throw new NotImplementedException();

        var visitorFaker = new Faker<Visitor>()
                .RuleFor(u => u.DateTime,       f => f.Date.Between(DateTime.Today, DateTime.Today.AddDays(1)))
            ;
        
        var visitors = visitorFaker.Generate(nrVisitors);
        await DbContext!.Visitors.AddRangeAsync(visitors);
    }
}