using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FluentAssertions;
using Xunit;

namespace F1FahrerMvvmWPF.ViewModels.Tests
{
	public class SchuelerViewModelTests
    {
		[Fact]
		public void LoadTest()
		{
			// arrange

			var vm = new NotenschnittMvvmWPF.ViewModels.SchuelerViewModel();
			vm.FileName = Path.GetTempPath() + @"\test.csv";
			File.WriteAllText(vm.FileName, @"Lewis Hamilton;110");

			// act

			vm.Load();

			// assert

			vm.Mathematik.Should().Be(110);
			vm.SchuelerName.Should().Be("Lewis Hamilton");
		}

		[Fact]
		public void CanLoadTestOK()
		{
			// arrange

			var vm = new NotenschnittMvvmWPF.ViewModels.SchuelerViewModel();
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

			var vm = new NotenschnittMvvmWPF.ViewModels.SchuelerViewModel();
			vm.FileName = Path.GetTempPath() + @"\testDoNotExist.csv";

			// act

			bool canload = vm.CanLoad();

			// assert

			canload.Should().BeFalse();
		}
	}
}