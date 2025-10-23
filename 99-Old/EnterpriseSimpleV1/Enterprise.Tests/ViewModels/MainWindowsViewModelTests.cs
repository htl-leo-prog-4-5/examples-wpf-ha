using System.Threading.Tasks;

using Enterprise.Logic.Contracts;
using Enterprise.Shared;
using Enterprise.WPF.ViewModels;

using FluentAssertions;

using NSubstitute;

using Xunit;

namespace Enterprise.Tests.ViewModels
{
    public class MainWindowsViewModelTests
    {
        [Fact]
        public async Task CallGetZeroTest()
        {
            // arrange
            var myService        = Substitute.For<IMyService>();
            var myServiceFactory = new FactoryInstance<IMyService>(myService);

            myService.GetZero().Returns(Task.FromResult(10));

            var mainViewModel = new MainWindowsViewModel(myServiceFactory);

            // act
            var result = await mainViewModel.GetZero();

            // assert
            result.Should().Be(10);
        }

        [Fact]
        public void CallMyServiceICommandTest()
        {
            // arrange
            var myService        = Substitute.For<IMyService>();
            var myServiceFactory = new FactoryInstance<IMyService>(myService);

            myService.GetZero().Returns(Task.FromResult(10));

            var mainViewModel = new MainWindowsViewModel(myServiceFactory);

            // act
            mainViewModel.CallMyService.CanExecute(null).Should().BeTrue();
            mainViewModel.CallMyService.Execute(null);

            // assert
            mainViewModel.ServiceValue.Should().Be(10);
            myService.Received(1).GetZero();
        }
    }
}