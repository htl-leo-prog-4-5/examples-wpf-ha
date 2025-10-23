using Core.DataTransferObjects;
using Core.Entities;

namespace Core.Contracts;

using Base.Core.Contracts;

public interface IBacklogItemRepository : IGenericRepository<BacklogItem>
{
    Task<IList<BacklogItemOverview>> GetBacklogAsync(string? teamMemberName);
}