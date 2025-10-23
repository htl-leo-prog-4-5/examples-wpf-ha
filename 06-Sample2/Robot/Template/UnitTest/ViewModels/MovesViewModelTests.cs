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

using Persistence;

using WinUIWpf.ViewModels;

using Xunit;

public class MovesViewModelTests
{
    [Fact]
    public void EditButtonInactiveTest()
    {
        //Arrange

        var uow = Substitute.For<IUnitOfWork>();

        var mv = new MovesViewModel(uow);

        //Act

        mv.SelectedMove = null;
        mv.EditMoveCommand.CanExecute(null).Should().BeFalse();
        mv.UpdateMoveCommand.CanExecute(null).Should().BeFalse();

        mv.SelectedMove = new Move();
        mv.EditMoveCommand.CanExecute(null).Should().BeTrue();
        mv.UpdateMoveCommand.CanExecute(null).Should().BeFalse();

        mv.EditMoveCommand.Execute(null);

        mv.EditMoveCommand.CanExecute(null).Should().BeFalse();
        mv.UpdateMoveCommand.CanExecute(null).Should().BeTrue();

        mv.SelectedMove = new Move();

        mv.EditMoveCommand.CanExecute(null).Should().BeTrue();
        mv.UpdateMoveCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public async Task LoadDataTest()
    {
        //Arrange

        var uow            = Substitute.For<IUnitOfWork>();
        var moveRepository = Substitute.For<IMoveRepository>();

        uow.Move.Returns(moveRepository);

        var moves = new CsvImport<Move>().Read(
            new[]
            {
                "Id;No;Direction;Speed;Duration;RaceId",
                "10;1;1;200;250;1",
                "10;2;1;200;250;1",
                "10;3;1;200;250;1",
                "10;4;1;200;250;1",
                "10;5;1;200;250;1",
            });

        uow.Move
            .GetNoTrackingAsync(Arg.Any<Expression<Func<Move, bool>>>(),
                Arg.Any<Func<IQueryable<Move>, IOrderedQueryable<Move>>>(),
                Arg.Any<string[]>())
            .Returns(moves);

        var mv = new MovesViewModel(uow);
        //Act

        await mv.LoadDataAsync();

        //Assert

        mv.Moves.Should()
            .NotBeEmpty()
            .And.HaveCount(moves.Count)
            .And.Contain(moves);

        // load again
        await mv.LoadDataAsync();
        mv.Moves.Should()
            .NotBeEmpty()
            .And.HaveCount(moves.Count)
            .And.Contain(moves);
    }

    [Fact]
    public void SelectMoveTest()
    {
        //Arrange

        var uow       = Substitute.For<IUnitOfWork>();
        var navigator = Substitute.For<IWindowNavigator>();

        var mv = new MovesViewModel(uow);

        mv.Controller = navigator;
        mv.SelectedMove = new Move()
        {
            Id = 14,
            Direction = 1,
            Duration = 250,
            Speed = 200
        };

        //Act
        //Assert

        mv.Direction.Should().Be(1);
        mv.Duration.Should().Be(250);
        mv.Speed.Should().Be(200);

        // revert edit
        mv.SelectedMove = new Move()
        {
            Id        = 15,
            Direction = 2,
            Duration  = 150,
            Speed     = 100
        };

        mv.Direction.Should().Be(2);
        mv.Duration.Should().Be(150);
        mv.Speed.Should().Be(100);

        mv.EditMoveCommand.Execute(null);
        mv.IsEditMode.Should().BeTrue();

        mv.SelectedMove = null;

        mv.IsEditMode.Should().BeFalse();

        mv.Direction.Should().BeNull();
        mv.Duration.Should().BeNull();
        mv.Speed.Should().BeNull();
    }

    [Fact]
    public async Task UpdateTest()
    {
        //Arrange

        var uow       = Substitute.For<IUnitOfWork>();
        var navigator = Substitute.For<IWindowNavigator>();

        var mv = new MovesViewModel(uow);
        var testMove = new Move()
        {
            Id        = 14,
            Direction = 1,
            Duration  = 250,
            Speed     = 200
        };

        mv.Controller   = navigator;
        mv.SelectedMove = testMove;

        uow.Move
            .GetByIdAsync(Arg.Any<int>())
            .Returns(mv.SelectedMove);

        //Act/Assert

        // no modification
        mv.EditMoveCommand.Execute(null);
        mv.UpdateMoveCommand.Execute(null);

        navigator.DidNotReceive().AskYesNoMessageBox(Arg.Any<string>(), Arg.Any<string>());

        mv.IsEditMode.Should().BeFalse();

        // with modification, msgbox => no

        mv.SelectedMove = testMove;
        mv.EditMoveCommand.Execute(null);
        mv.Duration = mv.Duration + 1;
        mv.UpdateMoveCommand.Execute(null);

        navigator.Received().AskYesNoMessageBox(Arg.Any<string>(), Arg.Any<string>());

        mv.SelectedMove.Should().BeNull();
        mv.Direction.Should().BeNull();
        mv.IsEditMode.Should().BeFalse();
        await uow.DidNotReceive().SaveChangesAsync();

        // with modification, msgbox => yes

        navigator.AskYesNoMessageBox(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

        mv.SelectedMove = testMove;
        mv.EditMoveCommand.Execute(null);
        mv.Duration = mv.Duration + 1;
        mv.UpdateMoveCommand.Execute(null);

        mv.IsEditMode.Should().BeFalse();

        await uow.Received().SaveChangesAsync();
    }
}