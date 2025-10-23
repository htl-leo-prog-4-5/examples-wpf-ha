namespace Persistence;

using Core.Contracts;

using Base.Persistence;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    public ApplicationDbContext? ApplicationDbContext => BaseApplicationDbContext as ApplicationDbContext;

    public UnitOfWork(
        ApplicationDbContext            applicationDbContext,
        IExamRepository                 exam
    ) : base(applicationDbContext)
    {
        Exam                 = exam;

    }

    public IExamRepository                 Exam                 { get; }
}