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

public class RacesViewModelTests
{
    [Fact]
    public void EditButtonInactiveTest()
    {
        //Arrange

        var uow = Substitute.For<IUnitOfWork>();

        var mv = new RacesViewModel(uow);

        //Act

        mv.SelectedRace = null;
        mv.EditRaceCommand.CanExecute(null).Should().BeFalse();
        mv.DeleteRaceCommand.CanExecute(null).Should().BeFalse();

        mv.SelectedRace = new Race();
        mv.EditRaceCommand.CanExecute(null).Should().BeTrue();
        mv.DeleteRaceCommand.CanExecute(null).Should().BeTrue();
    }

    [Fact]
    public async Task LoadDataTest()
    {
        //Arrange

        var uow            = Substitute.For<IUnitOfWork>();
        var raceRepository = Substitute.For<IRaceRepository>();

        uow.Race.Returns(raceRepository);

        var races = new CsvImport<Race>().Read(
            new[]
            {
                "Id;RaceTime",
                "1;00:00:30",
                "2;00:00:31",
                "3;00:00:32",
            });

        uow.Race
            .GetNoTrackingAsync(Arg.Any<Expression<Func<Race, bool>>>(),
                Arg.Any<Func<IQueryable<Race>, IOrderedQueryable<Race>>>(),
                Arg.Any<string[]>())
            .Returns(races);

        var mv = new RacesViewModel(uow);
        //Act

        await mv.LoadDataAsync();

        //Assert

        mv.Races.Should()
            .NotBeEmpty()
            .And.HaveCount(races.Count)
            .And.Contain(races);

        // load again
        await mv.LoadDataAsync();
        mv.Races.Should()
            .NotBeEmpty()
            .And.HaveCount(races.Count)
            .And.Contain(races);
    }

    [Fact]
    public void ShowMoveTest()
    {
        //Arrange

        var uow       = Substitute.For<IUnitOfWork>();
        var navigator = Substitute.For<IWindowNavigator>();

        var mv = new RacesViewModel(uow);

        mv.Controller = navigator;
        mv.SelectedRace = new Race()
        {
            Id = 14
        };

        //Act

        mv.EditRaceCommand.Execute(null);

        //Assert

        navigator.Received().ShowMovesWindow(Arg.Is<Race>(x => x.Id == 14));
    }

    [Fact]
    public void DeleteRaceTest()
    {
        //Arrange

        var uow            = Substitute.For<IUnitOfWork>();
        var raceRepository = Substitute.For<IRaceRepository>();

        uow.Race.Returns(raceRepository);
        var navigator = Substitute.For<IWindowNavigator>();

        var mv = new RacesViewModel(uow);

        mv.Controller = navigator;
        mv.SelectedRace = new Race()
        {
            Id          = 14,
            Driver      = new Driver() { Name      = "Maxi" },
            Competition = new Competition() { Name = "Competition" },
            Car         = new Car() { Name         = "Car" },
        };

        uow.Race
            .GetByIdAsync(Arg.Any<int>())
            .Returns(mv.SelectedRace);


        //Act/Assert

        mv.DeleteRaceCommand.Execute(null);

        navigator.Received().AskYesNoMessageBox(Arg.Any<string>(), Arg.Is<string>(x => x.Contains("Delete race:")));
        // messagebox returns false!
        raceRepository.DidNotReceive().Remove(Arg.Is<Race>(x => x.Id == 14));

        // now messagebox returns true!
        navigator.AskYesNoMessageBox(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

        mv.DeleteRaceCommand.Execute(null);

        navigator.Received().AskYesNoMessageBox(Arg.Any<string>(), Arg.Is<string>(x => x.Contains("Delete race:")));
        raceRepository.Received().Remove(Arg.Is<Race>(x => x.Id == 14));
        Received.InOrder(async () => { await uow.SaveChangesAsync(); });
    }
}