using AuthenticationBase.Persistence;

using Core.Contracts;
using Core.Contracts.Visitors;

namespace Persistence;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    public ApplicationDbContext? ApplicationDbContext => BaseApplicationDbContext as ApplicationDbContext;

    public UnitOfWork(
        ApplicationDbContext      applicationDbContext,
        ICityRepository           citYRep,
        IDistrictRepository       districtRep,
        IReasonForVisitRepository reasonRep,
        IVisitorRepository        visitorRep,
        ISchoolTypeRepository     schoolTypeRep
    ) : base(applicationDbContext)
    {
        Cities          = citYRep;
        Districts       = districtRep;
        ReasonsForVisit = reasonRep;
        Visitors        = visitorRep;
        SchoolTypes     = schoolTypeRep;
    }

    public ICityRepository           Cities          { get; }
    public IDistrictRepository       Districts       { get; }
    public IReasonForVisitRepository ReasonsForVisit { get; }
    public IVisitorRepository        Visitors        { get; }

    public ISchoolTypeRepository SchoolTypes { get; }
}