namespace Core.Contracts;

using Base.Core.Contracts;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public IExamRepository                 Exam                 { get; }

    //TODO: Add other repositories here
}