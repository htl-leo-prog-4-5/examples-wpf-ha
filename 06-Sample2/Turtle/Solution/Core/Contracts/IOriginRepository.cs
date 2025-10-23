using Base.Core.Contracts;

using Core.Entities;

namespace Core.Contracts;

public interface IOriginRepository : IGenericRepository<Origin>
{
    Task<Origin?> GetByNameAsync(string? origin);
}