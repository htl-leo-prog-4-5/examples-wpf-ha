using System.IO;
using FluentAssertions;
using Xunit;

namespace F1FahrerMvvmWPFTests.ViewModels
{
	public class PlayerViewModelTests
	{
		[Fact]
		public void LoadTest()
		{
			// arrange

			var vm = new TennisMvvmWPF.ViewModels.PlayerViewModel();
			vm.FileName = Path.GetTempPath() + @"\test.csv";
			File.WriteAllText(vm.FileName, @"TestPlayer;123");

			// act

			vm.Load();

			// assert

			vm.Points.Should().Be(12345);
			vm.PlayerName.Should().Be("TestPlayer");
		}

		[Fact]
		public void CanLoadTestOK()
		{
			// arrange

			var vm = new TennisMvvmWPF.ViewModels.PlayerViewModel();
			vm.FileName = Path.GetTempPath() + @"\test.csv";
			File.WriteAllText(vm.FileName, @"TestPlayer;123");

			// act

			bool canload = vm.CanLoad();

            // assert

            canload.Should().BeTrue();
        }

		[Fact]
		public void CanLoadTestFail()
		{
			// arrange

			var vm = new TennisMvvmWPF.ViewModels.PlayerViewModel();
			vm.FileName = Path.GetTempPath() + @"\testDoNotExist.csv";

			// act

			bool canload = vm.CanLoad();

			// assert

			canload.Should().BeFalse();
		}

		[Fact]
		public void WinTest()
		{
			// arrange

			var vm = new TennisMvvmWPF.ViewModels.PlayerViewModel();
			vm.Points = 5;
			vm.PlayerName = "Maxi";

			// act

			vm.Win();

			// assert

			vm.Points.Should().Be(20);
		}
	}
}