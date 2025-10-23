using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

namespace Persistence;

using Base.Persistence;

public class OfficeRepository : GenericRepository<Office>, IOfficeRepository
{
    public OfficeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Office?> GetByNoAsync(string no)
    {
        return await DbSet.SingleOrDefaultAsync(office => office.No == no);
    }
}