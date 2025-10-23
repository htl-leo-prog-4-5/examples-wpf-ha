namespace UnitTest.ViewModels;

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Threading.Tasks;

using Base.Tools.CsvImport;

using Core.Contracts;
using Core.Entities;
using Core.QueryResults;

using FluentAssertions;

using NSubstitute;

using WinUIWpf.ViewModels;

using Xunit;

public class MainViewModelTests
{
    [Fact]
    public void ResultButtonInactiveTest()
    {
        //Arrange

        var uow = Substitute.For<IUnitOfWork>();

        var mv = new MainViewModel(uow);

        //Act/Assert

        mv.SelectedCompetition = null;
        mv.ShowRacesCommand.CanExecute(null).Should().BeFalse();

        mv.SelectedCompetition = new CompetitionSummary();
        mv.ShowRacesCommand.CanExecute(null).Should().BeTrue();
    }

    [Fact]
    public async Task LoadDataTest()
    {
        //Arrange

        var uow                   = Substitute.For<IUnitOfWork>();
        var competitionRepository = Substitute.For<ICompetitionRepository>();

        uow.Competition.Returns(competitionRepository);

        var comp = new CsvImport<CompetitionSummary>().Read(
            new[]
            {
                "ID;Name;RaceCount",
                "1;Competition 1;10",
                "2;Competition 2;20",
                "3;Competition No Race;0",
            });

        uow.Competition
            .GetSummaryAsync()
            .Returns(comp);

        var mv = new MainViewModel(uow);
        
        //Act

        await mv.LoadDataAsync();

        //Assert

        mv.Competitions.Should()
            .NotBeEmpty()
            .And.HaveCount(comp.Count)
            .And.Contain(comp);

        // load again
        await mv.LoadDataAsync();
        mv.Competitions.Should()
            .NotBeEmpty()
            .And.HaveCount(comp.Count)
            .And.Contain(comp);
    }

    [Fact]
    public void ShowRacesTest()
    {
        //Arrange

        var uow = Substitute.For<IUnitOfWork>();
        var navigator = Substitute.For<IWindowNavigator>();

        var mv = new MainViewModel(uow);

        mv.Controller = navigator;
        mv.SelectedCompetition = new CompetitionSummary()
        {
            Id = 14
        };

        //Act

        mv.ShowRacesCommand.Execute(null);

        //Assert

        navigator.Received().ShowRacesWindow(Arg.Is<CompetitionSummary>(x => x.Id == 14));
    }
}