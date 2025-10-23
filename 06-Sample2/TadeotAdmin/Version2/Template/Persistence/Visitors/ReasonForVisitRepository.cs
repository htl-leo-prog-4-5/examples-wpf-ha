using AuthenticationBase.Persistence.Repositories;

using Core.Contracts.Visitors;
using Core.Entities.Visitors;

using Microsoft.EntityFrameworkCore;

namespace Persistence.Visitors;

public class ReasonForVisitRepository : GenericRepository<ReasonForVisit>, IReasonForVisitRepository
{
    public ApplicationDbContext? DbContext { get; }

    public ReasonForVisitRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        DbContext = dbContext;
    }
}