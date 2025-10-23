using Base.Persistence;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

namespace Persistence;

using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

public class SimulationRepository : GenericRepository<Simulation>, ISimulationRepository
{
    public SimulationRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}