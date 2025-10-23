using AuthenticationBase.Persistence.Repositories;
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
    public class SchoolTypeRepository : GenericRepository<SchoolType>, ISchoolTypeRepository
    {
        public ApplicationDbContext? DbContext { get; }

        public SchoolTypeRepository(DbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext as ApplicationDbContext;
        }

        public async Task UpdateAllAsync(string[] types)
        {
            var old = await DbContext.SchoolTypes.ToListAsync();
            DbContext.SchoolTypes.RemoveRange(old);
            var rank = 1;
            var newTypes = types
                .Select(type => new SchoolType
                {
                    Rank = rank++,
                    Type = type
                })
                .ToList();
            await DbContext.SchoolTypes.AddRangeAsync(newTypes);

        }
    }
}
