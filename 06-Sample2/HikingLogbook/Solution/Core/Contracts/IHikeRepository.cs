using Core.DataTransferObjects;
using Core.Entities;

namespace Core.Contracts;

using Base.Core.Contracts;

public interface IHikeRepository : IGenericRepository<Hike>
{
    Task<IList<HikeOverview>> GetHikesAsync(string? companion);
}