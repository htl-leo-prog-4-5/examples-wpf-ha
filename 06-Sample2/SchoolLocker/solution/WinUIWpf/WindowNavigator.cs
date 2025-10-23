namespace WinUIWpf
{
    using System.Windows;

    using Microsoft.Extensions.DependencyInjection;

    using WinUIWpf.ViewModels;
    using WinUIWpf.Views;

    using WpfMvvmBase;

    public class WindowNavigator : IWindowNavigator
    {
        private readonly Window _window;

        public WindowNavigator(Window window)
        {
            _window = window;
        }

        public void ShowAddLockerWindow()
        {
            using (var scope = AppService.ServiceProvider!.CreateScope())
            {
                var addLockerWindow = scope.ServiceProvider.GetRequiredService<AddLockerWindow>();
                addLockerWindow.ShowDialog();
            }
        }

        public void ShowBookingWindow(int lockerNo)
        {
            using (var scope = AppService.ServiceProvider!.CreateScope())
            {
                var showBookingWindow = scope.ServiceProvider.GetRequiredService<ShowBookingWindow>();
                var viewModel         = (ShowBookingWindowViewModel)showBookingWindow.DataContext;
                viewModel.LockerNo = lockerNo;
                showBookingWindow.ShowDialog();
            }
        }

        public void CloseWindow()
        {
            _window.Close();
        }
    }
}