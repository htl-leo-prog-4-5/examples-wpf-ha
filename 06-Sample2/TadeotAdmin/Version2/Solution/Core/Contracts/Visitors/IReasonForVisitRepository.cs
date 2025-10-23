using AuthenticationBase.Contracts.Persistence;

using Core.Entities.Visitors;

namespace Core.Contracts.Visitors;

public interface IReasonForVisitRepository : IGenericRepository<ReasonForVisit>
{
    Task UpdateAllAsync(string[] reasons);
}