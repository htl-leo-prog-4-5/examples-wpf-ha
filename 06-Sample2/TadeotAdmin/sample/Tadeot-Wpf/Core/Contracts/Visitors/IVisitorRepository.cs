using AuthenticationBase.Contracts.Persistence;
using Core.Entities.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Visitors
{
    public interface IVisitorRepository : IGenericRepository<Visitor>
    {
        Task DeleteAllAsync();
        Task GenerateTestDataAsync(int nrVisitors);
        Task<IEnumerable<Visitor>> GetAllUntrackedAsync();
    }
}
