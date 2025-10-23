namespace Wpf;

using System.Windows;

using Wpf.Views;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void AppStartup(object sender, StartupEventArgs e)
    {

        MainWindow = new MainWindow();
        MainWindow.Show();
    }
}