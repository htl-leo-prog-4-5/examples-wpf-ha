namespace WpfApp;

using System;
using System.Threading.Tasks;
using System.Windows;

using Base.WpfMvvm;

using Core.DataTransferObjects;

using WinUIWpf.ViewModels;

using WpfApp.Views;

public class WindowNavigator : BaseWindowNavigator, IWindowNavigator
{
    public WindowNavigator(Window window) : base(window)
    {
    }

    public async Task ShowCheckinRoomAsync(RoomDto room)
    {
        throw new NotImplementedException();
    }

    public async Task ShowCheckoutRoomAsync(RoomDto room)
    {
        var window     = new CheckoutBookingWindow();
        var controller = new WindowNavigator(window);
        var viewModel  = new CheckoutBookingViewModel(controller, room);
        window.DataContext = viewModel;
        await viewModel.LoadDataAsync();
        window.ShowDialog();
    }
}