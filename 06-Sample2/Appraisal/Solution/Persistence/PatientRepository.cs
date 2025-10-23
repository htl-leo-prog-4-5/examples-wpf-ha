using Base.Persistence;

using Core.Contracts;
using Core.Entities;

namespace Persistence;

using Core.DataTransferObjects;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;

public class PatientRepository : GenericRepository<Patient>, IPatientRepository
{
    public PatientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Patient?> GetBySVNumberAsync(string? svNumber)
    {
        if (string.IsNullOrEmpty(svNumber))
        {
            return null;
        }

        return await DbSet.SingleOrDefaultAsync(p => p.SVNumber == svNumber);
    }

    public async Task<IList<PatientOverview>> GetPatientOverviewAsync(bool missingNamesOnly)
    {
        var query = DbSet.Include(nameof(Patient.Examinations));

        if (missingNamesOnly)
        {
            query = query.Where(p => p.FirstName == null || p.FirstName == null);
        }

        return await query
            .OrderBy(p => p.SVNumber)
            .Select(p => new PatientOverview(
                p.Id,
                p.SVNumber,
                p.FirstName,
                p.LastName,
                p.Examinations!.Count,
                p.Examinations.Select(e => e.ExaminationDate).Max()))
            .ToListAsync();
    }
}