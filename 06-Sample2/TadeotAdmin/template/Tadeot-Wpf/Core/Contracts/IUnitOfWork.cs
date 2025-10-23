using AuthenticationBase.Contracts.Persistence;
using Core.Contracts.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        public ICityRepository Cities { get; }
        public IDistrictRepository Districts { get; }
        public IReasonForVisitRepository ReasonsForVisit { get; }
        public IVisitorRepository Visitors { get; }

        public ISchoolTypeRepository SchoolTypes { get; }
    }
}
