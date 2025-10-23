namespace WpfApp;

using System.Threading.Tasks;
using System.Windows;

using Base.WpfMvvm;

// using Core.DataTransferObjects;

using WinUIWpf.ViewModels;

using WpfApp.Views;

public class WindowNavigator : BaseWindowNavigator, IWindowNavigator
{
    public WindowNavigator(Window window) : base(window)
    {
    }

    public async Task ShowCheckinRoomAsync(/* RoomDto room */)
    {
        /*
        var window     = new CheckinBookingWindow();
        var controller = new WindowNavigator(window);
        var viewModel  = new CheckinBookingWindowViewModel(controller, room);
        window.DataContext = viewModel;
        await viewModel.LoadDataAsync();
        window.ShowDialog();
        */
    }

    public async Task ShowCheckoutRoomAsync(/* RoomDto room */)
    {
    }
}