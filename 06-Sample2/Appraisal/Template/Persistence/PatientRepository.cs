using Base.Persistence;

using Core.Contracts;
using Core.Entities;

namespace Persistence;

public class PatientRepository : GenericRepository<Patient>, IPatientRepository
{
    public PatientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}