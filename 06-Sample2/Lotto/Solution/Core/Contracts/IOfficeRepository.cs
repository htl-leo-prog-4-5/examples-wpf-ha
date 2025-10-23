using Base.Core.Contracts;

using Core.DataTransferObjects;
using Core.Entities;

namespace Core.Contracts;

public interface IOfficeRepository : IGenericRepository<Office>
{
    Task<Office?> GetByNoAsync(string no);
}