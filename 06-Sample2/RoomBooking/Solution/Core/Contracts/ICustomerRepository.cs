using Core.DataTransferObjects;
using Core.Entities;

namespace Core.Contracts;

using Base.Core.Contracts;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<IList<CustomerDto>> GetAllAsync(string? filterName, bool? onlyWithBookings);
}