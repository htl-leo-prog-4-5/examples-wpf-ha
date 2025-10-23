using Core.DataTransferObjects;
using Core.Entities;

namespace Core.Contracts;

using Base.Core.Contracts;

public interface IStationRepository : IGenericRepository<Station>
{
    Task<IList<StationOverview>> GetStationOverviewAsync();
}