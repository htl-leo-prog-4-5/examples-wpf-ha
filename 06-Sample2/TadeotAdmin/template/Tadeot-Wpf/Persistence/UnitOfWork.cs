using AuthenticationBase.Persistence;
using Core.Contracts;
using Core.Contracts.Visitors;
using Persistence.Visitors;

namespace Persistence
{
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        public ApplicationDbContext? ApplicationDbContext => BaseApplicationDbContext as ApplicationDbContext;


        public UnitOfWork() : this(new ApplicationDbContext())
        {

        }
        public UnitOfWork(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            Cities = new CityRepository(applicationDbContext);
            Districts = new DistrictRepository(applicationDbContext);
            ReasonsForVisit = new ReasonForVisitRepository(applicationDbContext);
            Visitors = new VisitorRepository(applicationDbContext);
            SchoolTypes = new SchoolTypeRepository(applicationDbContext);
        }

        public ICityRepository Cities { get; }
        public IDistrictRepository Districts { get; }
        public IReasonForVisitRepository ReasonsForVisit { get; }
        public IVisitorRepository Visitors { get; }

        public ISchoolTypeRepository SchoolTypes { get; }


    }
}
