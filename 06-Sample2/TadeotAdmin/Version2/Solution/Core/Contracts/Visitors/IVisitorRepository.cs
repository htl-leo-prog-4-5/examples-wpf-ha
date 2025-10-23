using AuthenticationBase.Contracts.Persistence;

using Core.Entities.Visitors;

namespace Core.Contracts.Visitors;

public interface IVisitorRepository : IGenericRepository<Visitor>
{
    Task                       DeleteAllAsync();
    Task                       GenerateTestDataAsync(int nrVisitors);
    Task<IEnumerable<Visitor>> GetAllUntrackedAsync();
}