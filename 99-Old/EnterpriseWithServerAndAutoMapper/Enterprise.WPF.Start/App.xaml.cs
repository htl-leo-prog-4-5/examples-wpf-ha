using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using Enterprise.Service.Contracts;
using Enterprise.Service.ProxyWeb;
using Enterprise.Shared;
using Enterprise.WPF.ViewModels;

namespace Enterprise.WPF.Start
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void AppStartup(object sender, StartupEventArgs e)
        {
            Dependency.RegisterType<IMyService, MyServiceProxy>();
            Dependency.RegisterType<MainWindowsViewModel, MainWindowsViewModel>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<WpfAutoMapperProfile>();
            });
            config.AssertConfigurationIsValid();

            IMapper mapper = config.CreateMapper();
            Dependency.RegisterInstance(mapper);
        }
    }
}