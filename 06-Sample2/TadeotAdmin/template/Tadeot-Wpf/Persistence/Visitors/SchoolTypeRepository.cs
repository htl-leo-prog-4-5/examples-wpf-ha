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

    }
}
