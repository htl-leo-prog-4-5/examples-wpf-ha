using System.IO;
using FluentAssertions;
using Xunit;

namespace F1FahrerMvvmWPF.ViewModels.Tests;

public class F1DriverViewModelTests
{
    [Fact]
    public void LoadTest()
    {
        // arrange

        var vm = new F1FahrerMvvmWPF.ViewModels.F1DriverViewModel();
        vm.FileName = Path.GetTempPath() + @"\test.csv";
        File.WriteAllText(vm.FileName, @"Lewis Hamilton;110");

        // act

        vm.Load();

        // assert

        vm.Points.Should().Be(110);
        vm.DriverName.Should().Be("Lewis Hamilton");
    }

    [Fact]
    public void CanLoadTestOK()
    {
        // arrange

        var vm = new F1FahrerMvvmWPF.ViewModels.F1DriverViewModel();
        vm.FileName = Path.GetTempPath() + @"\test.csv";
        File.WriteAllText(vm.FileName, @"Lewis Hamilton;110");

        // act

        bool canload = vm.CanLoad();

        // assert

        canload.Should().BeTrue();
    }

    [Fact]
    public void CanLoadTestFail()
    {
        // arrange

        var vm = new F1FahrerMvvmWPF.ViewModels.F1DriverViewModel();
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

        var vm = new F1FahrerMvvmWPF.ViewModels.F1DriverViewModel();
        vm.Points = 5;
        vm.DriverName = "Maxi";

        // act

        vm.Win();

        // assert

        vm.Points.Should().Be(20);
    }
}