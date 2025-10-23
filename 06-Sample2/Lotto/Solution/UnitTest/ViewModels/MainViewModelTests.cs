namespace UnitTest.ViewModels;

using System.Threading.Tasks;

using Base.Tools.CsvImport;

using Core.Contracts;
using Core.Entities;
using Core.DataTransferObjects;

using FluentAssertions;

using NSubstitute;

using Wpf.ViewModels;

using Xunit;
using System.Linq.Expressions;
using System.Linq;
using System;

public class MainViewModelTests
{
    [Fact]
    public void DetailButtonInactiveTest()
    {
        //Arrange

        var uow = Substitute.For<IUnitOfWork>();

        var mv = new MainWindowViewModel(uow);

        //Act/Assert

        mv.SelectedGame = null;
        mv.DetailCommand.CanExecute(null).Should().BeFalse();
    }

    [Fact]
    public async Task LoadDataTest()
    {
        //Arrange

        var uow             = Substitute.For<IUnitOfWork>();
        var gameRepository = Substitute.For<IGameRepository>();

        uow.GameRepository.Returns(gameRepository);

        var games = new CsvImport<Game>().Read(
            """
                Id;DateFrom;DateTo;ExpectedDrawDate;DrawDate;No1;No2;No3;No4;No5;No6;NoX
                314;2023/12/30;2024/01/05;2024/01/06;2024/01/06;5;8;13;20;26;45;32
                315;2024/01/06;2024/01/12;2024/01/13;2024/01/13;3;4;13;33;37;38;7
                316;2024/01/13;2024/01/19;2024/01/20;2024/01/20;4;5;10;15;22;28;27
                317;2024/01/20;2024/01/26;2024/01/27;2024/01/27;13;15;28;34;36;42;2
                """
                .Replace("\r", "").Split('\n')
        );

        uow.GameRepository.GetNoTrackingAsync(Arg.Any<Expression<Func<Game, bool>>>(),
                Arg.Any<Func<IQueryable<Game>, IOrderedQueryable<Game>>>(),
                Arg.Any<string[]>())
            .Returns(games);

        var mv = new MainWindowViewModel(uow);

        //Act

        await mv.InitializeDataAsync();

        //Assert


        mv.FilteredGames.Should()
            .NotBeEmpty()
            .And.HaveCount(games.Count)
            .And.Contain(games);

        // load again
        await mv.InitializeDataAsync();
        mv.FilteredGames.Should()
            .NotBeEmpty()
            .And.HaveCount(games.Count)
            .And.Contain(games);
    }
}