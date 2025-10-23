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
    internal class DistrictRepository : GenericRepository<District>, IDistrictRepository
    {
        public ApplicationDbContext? DbContext { get; }

        public DistrictRepository(DbContext dbContext) : base(dbContext)
        {
            DbContext = dbContext as ApplicationDbContext;
        }
    }
}
