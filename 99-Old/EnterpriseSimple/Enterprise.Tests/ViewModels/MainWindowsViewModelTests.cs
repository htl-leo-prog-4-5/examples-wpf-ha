using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enterprise.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enterprise.Logic.Contracts;
using NSubstitute;
using Enterprise.Shared;
using FluentAssertions;

namespace Enterprise.WPF.ViewModels.Tests
{
    [TestClass()]
    public class MainWindowsViewModelTests
    {
        [TestMethod()]
        public void MainWindowsViewModelTest()
        {
            var myservice = Substitute.For<IMyService>();
            myservice.GetZero().Returns(10);

            var mainviewmodel = new MainWindowsViewModel(myservice);

            mainviewmodel.GetZero().Should().Be(10);
        }
    }
}