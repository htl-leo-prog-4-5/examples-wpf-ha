using System.Windows;
using GradeCalc.Core;

namespace GradeCalc
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Dependency.InitDependencies();
        }
    }
}