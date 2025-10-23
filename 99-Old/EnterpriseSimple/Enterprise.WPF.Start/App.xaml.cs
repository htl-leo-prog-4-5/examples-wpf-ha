using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Enterprise.Logic;
using Enterprise.Logic.Contracts;
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
            Dependency.RegisterType<IMyService,MyService>();
            Dependency.RegisterType<MainWindowsViewModel, MainWindowsViewModel>();
        }
    }
}