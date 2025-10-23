using Base.Core.Contracts;

using Core.Entities;

namespace Core.Contracts;

using Core.DataTransferObjects;

using System.Threading.Tasks;

public interface ICruiseShipRepository : IGenericRepository<CruiseShip>
{
    Task<IList<CruiseShip>> GetByCompanyIdNoTrackingAsync(int? companyId);
}