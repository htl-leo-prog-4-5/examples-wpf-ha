namespace WpfTadeotAdmin;

using System.Windows;

using Core;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;

using WpfMvvmBase;

using WpfTadeotAdmin.Views;

public class WindowNavigator : IWindowNavigator
{
    private readonly Window _window;

    public WindowNavigator(Window window)
    {
        _window = window;
    }

    public void OpenRegistrationConfigDialog()
    {
        using (var scope = AppService.ServiceProvider!.CreateScope())
        {
            var registrationConfigWindow = scope.ServiceProvider.GetRequiredService<RegistrationConfigWindow>();
            registrationConfigWindow.ShowDialog();
        }
    }

    public bool AskDeleteVisitors()
    {
        var answer = MessageBox.Show("Wollen Sie wirklich alle Registrierungen löschen?",
            "Registrierungen löschen",
            MessageBoxButton.YesNo,
            MessageBoxImage.Exclamation);
        return answer == MessageBoxResult.Yes;
    }

    public string? AskSaveToCsvFile()
    {
        var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Csv file (*.csv)|*.csv";

        if (saveFileDialog.ShowDialog() == true)
        {
            return saveFileDialog.FileName;
        }

        return null;
    }

    public void CloseWindow()
    {
        _window.Close();
    }
}