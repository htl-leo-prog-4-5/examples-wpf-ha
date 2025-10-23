namespace Persistence;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Base.Persistence;

using Core.Contracts;
using Core.Entities;
using Core.QueryResults;

using Microsoft.EntityFrameworkCore;

public class ExamRepository : GenericRepository<Exam>, IExamRepository
{
    public ExamRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}