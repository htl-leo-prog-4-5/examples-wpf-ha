using Core.DataTransferObjects;
using Core.Entities;

namespace Core.Contracts;

public interface ILockerRepository : IGenericRepository<Locker>
{
    Task<Locker?> GetByNumberAsync(int number);

    Task<IEnumerable<LockerBooking>> GetLockersOverviewAsync();
    Task<SchoolLockerDto[]>          GetLockersWithStateAsync();
    Task<bool>                       HasDuplicateAsync(Locker locker);
}