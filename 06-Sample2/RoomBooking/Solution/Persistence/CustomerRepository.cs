using Core.Contracts;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

using Core.DataTransferObjects;

using Base.Persistence;

namespace Persistence;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<CustomerDto>> GetAllAsync(string? filterName, bool? onlyWithBookings)
    {
        IQueryable<Customer> customers = _dbContext.Customers;
        if (filterName != null)
        {
            customers = customers.Where(c => (c.LastName + " " + c.FirstName).ToLower().Contains(filterName.ToLower()));
        }

        if (onlyWithBookings != null)
        {
            customers = customers.Where(c => c.Bookings.Count(b => b.To == null || (b.From <= DateTime.Now && b.To >= DateTime.Now)) > 0);
        }

        return await customers
            .OrderBy(c => c.LastName)
            .ThenBy(c => c.FirstName)
            .Select(c => new CustomerDto(c.Id, c.FirstName, c.LastName, c.Bookings.Count()))
            .ToListAsync();
    }
}