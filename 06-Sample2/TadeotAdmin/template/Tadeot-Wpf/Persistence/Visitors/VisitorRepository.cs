using AuthenticationBase.Persistence.Repositories;
using Bogus;
using Core.Contracts.Visitors;
using Core.Entities.Visitors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Visitors
{
    internal class VisitorRepository : GenericRepository<Visitor>, IVisitorRepository
    {
        public ApplicationDbContext? DbContext { get; }

        public VisitorRepository(DbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext as ApplicationDbContext;
        }

        public async Task GenerateTestDataAsync(int nrVisitors)
        {
            var visitorFaker = new Faker<Visitor>()
           .RuleFor(u => u.DateTime, f => f.Date.Between(DateTime.Today, DateTime.Today.AddDays(1)))
           // TODO: Fake all other visitor properties as well
           ;
            var visitors = visitorFaker.Generate(nrVisitors);
            await DbContext!.Visitors.AddRangeAsync(visitors);
        }
    }
}

