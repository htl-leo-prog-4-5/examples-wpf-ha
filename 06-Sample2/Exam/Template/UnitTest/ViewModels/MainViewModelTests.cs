namespace UnitTest.ViewModels;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Core.Contracts;
using Core.Entities;

using FluentAssertions;

using NSubstitute;

using WinUIWpf.ViewModels;

using Xunit;

public class MainViewModelTests
{
    [Fact]
    public async Task ResultButtonInactiveTest()
    {
        // TODO vm Test
        1.Should().Be(2);
    }

    [Fact]
    public async Task LoadDataTest()
    {
        // TODO vm Test

        //Arrange

        var uow     = Substitute.For<IUnitOfWork>();
        var examRep = Substitute.For<IExamRepository>();

        uow.Exam.Returns(examRep);

        uow.Exam.GetNoTrackingAsync(Arg.Any<Expression<Func<Exam, bool>>>(),
                Arg.Any<Func<IQueryable<Exam>, IOrderedQueryable<Exam>>>(),
                Arg.Any<string[]>())
            .Returns(new Exam[]
            {
                examMaxi,
                examSeppi
            });

        var mv = new MainViewModel(uow);
        //Act

        await mv.LoadDataAsync();

        //Assert

        mv.Exams.Should().Contain(examMaxi);
        mv.Exams.Should().Contain(examSeppi);
        mv.Exams.Should().HaveCount(2);
    }
}