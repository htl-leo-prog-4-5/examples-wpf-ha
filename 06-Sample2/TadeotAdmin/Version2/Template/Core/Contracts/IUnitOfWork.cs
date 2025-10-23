using AuthenticationBase.Contracts.Persistence;

using Core.Contracts.Visitors;

namespace Core.Contracts;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public ICityRepository           Cities          { get; }
    public IDistrictRepository       Districts       { get; }
    public IReasonForVisitRepository ReasonsForVisit { get; }
    public IVisitorRepository        Visitors        { get; }

    public ISchoolTypeRepository SchoolTypes { get; }
}