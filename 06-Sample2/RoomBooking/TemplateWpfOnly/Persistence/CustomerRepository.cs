using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Base.Persistence;

public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    //TODO
}