using Base.Persistence;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

namespace Persistence;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

public class ExaminationRepository : GenericRepository<Examination>, IExaminationRepository
{
    public ExaminationRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}