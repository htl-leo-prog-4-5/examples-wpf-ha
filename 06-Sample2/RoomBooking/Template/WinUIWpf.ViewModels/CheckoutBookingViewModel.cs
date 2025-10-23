namespace WinUIWpf.ViewModels;

using System;
using System.Threading.Tasks;

using Base.WpfMvvm;

using Core.Contracts;
using Core.Entities;

using Persistence;

public class CheckoutBookingViewModel : BaseViewModel
{
    public IWindowNavigator Controller { get; set; }


    public CheckoutBookingViewModel(IWindowNavigator controller /*, RoomDto roomDto */)
    {
        Controller  = controller;
    }
}