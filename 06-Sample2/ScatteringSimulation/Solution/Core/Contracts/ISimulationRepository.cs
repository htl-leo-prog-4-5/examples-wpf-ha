using Core.DataTransferObjects;
using Core.Entities;

namespace Core.Contracts;

using Base.Core.Contracts;

public interface ISimulationRepository : IGenericRepository<Simulation>
{
    Task<IList<SimulationOverview>> GetSimulationsAsync(string? category);
}