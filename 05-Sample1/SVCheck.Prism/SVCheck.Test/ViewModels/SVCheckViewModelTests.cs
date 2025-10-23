using FluentAssertions;
using SVCheck.ViewModels;
using Xunit;

namespace SVCheck.Test.ViewModels
{
    public class SVCheckViewModelTests 
    {
        [Fact]
        public void CheckValidSV()
        {
            var vm = new SVCheckViewModel();
            vm.SVYear = 1910;
            vm.SVMonth = 12;
            vm.SVDay = 24;
            vm.SVNumber = 1237;

            vm.Check();

            vm.CheckResult.Should().Be(vm.SVOK);
        }

        [Fact]
        public void CheckValidSVFail()
        {
            var vm = new SVCheckViewModel();
            vm.SVYear = 1910;
            vm.SVMonth = 12;
            vm.SVDay = 24;
            vm.SVNumber = 1230;

            vm.Check();

            vm.CheckResult.Should().Be(vm.InvalidSV);
        }
    }
}