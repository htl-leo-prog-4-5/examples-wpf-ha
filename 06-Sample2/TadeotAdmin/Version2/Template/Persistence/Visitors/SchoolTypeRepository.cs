using AuthenticationBase.Persistence.Repositories;

using Core.Contracts.Visitors;
using Core.Entities.Visitors;

using Microsoft.EntityFrameworkCore;

namespace Persistence.Visitors;

public class SchoolTypeRepository : GenericRepository<SchoolType>, ISchoolTypeRepository
{
    public ApplicationDbContext? DbContext { get; }

    public SchoolTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        DbContext = dbContext;
    }
}