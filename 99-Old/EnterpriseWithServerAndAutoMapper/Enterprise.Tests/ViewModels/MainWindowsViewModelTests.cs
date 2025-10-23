using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enterprise.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Enterprise.Service.Contracts;
using NSubstitute;
using Enterprise.Shared;
using FluentAssertions;

namespace Enterprise.WPF.ViewModels.Tests
{
    [TestClass()]
    public class MainWindowsViewModelTests
    {
        private IMapper _mapper; 

        [TestInitialize]
        public void CreateAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<WpfAutoMapperProfile>();
            });
            config.AssertConfigurationIsValid();

            _mapper = config.CreateMapper();
            Dependency.RegisterInstance(_mapper);
        }

        [TestMethod()]
        public void MainWindowsViewModelTest()
        {
            var myservice = Substitute.For<IMyService>();
            myservice.GetZero().Returns(Task.FromResult(10));

            var mainviewmodel = new MainWindowsViewModel(myservice, _mapper);

            mainviewmodel.GetZero().Result.Should().Be(10);
        }

        [TestMethod()]
        public void MainWindowsViewModelTestAutoMapper()
        {
            var myservice = Substitute.For<IMyService>();
            myservice.GetMyInfo().Returns(Task.FromResult(new DTO.MyInfo() { Name = "Seppi" }));

            var mainviewmodel = new MainWindowsViewModel(myservice, _mapper);

            mainviewmodel.GetInfo().Result.Should().BeEquivalentTo(new Models.MyInfo() { Name = "Seppi"});
        }
    }
}