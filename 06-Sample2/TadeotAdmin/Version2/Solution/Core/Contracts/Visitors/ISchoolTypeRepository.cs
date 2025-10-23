using AuthenticationBase.Contracts.Persistence;

using Core.Entities.Visitors;

namespace Core.Contracts.Visitors;

public interface ISchoolTypeRepository : IGenericRepository<SchoolType>
{
    Task UpdateAllAsync(string[] types);
}