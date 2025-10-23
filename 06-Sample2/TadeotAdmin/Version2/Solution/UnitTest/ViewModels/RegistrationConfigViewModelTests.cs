using System;

namespace UnitTest.ViewModels;

using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Core.Contracts;
using Core.Contracts.Visitors;
using Core.Entities.Visitors;

using FluentAssertions;

using NSubstitute;

using WpfTadeotAdmin.ViewModels;

using Xunit;

public class RegistrationConfigViewModelTests
{
    [Fact]
    public async Task LoadDataTest()
    {
        //Arrange

        var uow       = Substitute.For<IUnitOfWork>();
        var reasonRep = Substitute.For<IReasonForVisitRepository>();
        var typesRep  = Substitute.For<ISchoolTypeRepository>();

        uow.ReasonsForVisit.Returns(reasonRep);
        uow.SchoolTypes.Returns(typesRep);

        uow.ReasonsForVisit.GetAsync(Arg.Any<Expression<Func<ReasonForVisit, bool>>>(),
                Arg.Any<Func<IQueryable<ReasonForVisit>, IOrderedQueryable<ReasonForVisit>>>(),
                Arg.Any<string[]>())
            .Returns(new ReasonForVisit[]
            {
                new ReasonForVisit() { Id = 1, Reason = "1", Rank = 1 },
                new ReasonForVisit() { Id = 2, Reason = "2", Rank = 2 }
            });

        uow.SchoolTypes.GetAsync(Arg.Any<Expression<Func<SchoolType, bool>>>(),
                Arg.Any<Func<IQueryable<SchoolType>, IOrderedQueryable<SchoolType>>>(),
                Arg.Any<string[]>())
            .Returns(new SchoolType[]
            {
                new SchoolType() { Id = 1, Type = "Type 1" },
                new SchoolType() { Id = 2, Type = "Type 2" }
            });

        var mv = new RegistrationConfigViewModel(uow);

        //Act

        await mv.LoadDataAsync();

        //Assert

        mv.ReasonsText.Should().Be("1\n2");
        mv.TypesText.Should().Be("Type 1\nType 2");
    }

    [Fact]
    public async Task SaveSaveTypesAsyncTest()
    {
        //Arrange

        var uow      = Substitute.For<IUnitOfWork>();
        var typesRep = Substitute.For<ISchoolTypeRepository>();
        uow.SchoolTypes.Returns(typesRep);

        var mv = new RegistrationConfigViewModel(uow);

        mv.TypesText = "1\n2\n3";

        //Act

        mv.UpdateTypes.Execute(null);

        //Assert
        await typesRep.Received().UpdateAllAsync(Arg.Any<string[]>());
        await typesRep.Received(1).UpdateAllAsync(Arg.Is<string[]>(x =>
            x.Contains("1") &&
            x.Contains("2") &&
            x.Contains("3") &&
            x.Length == 3));
 
        await uow.Received(1).SaveChangesAsync();
    }
}