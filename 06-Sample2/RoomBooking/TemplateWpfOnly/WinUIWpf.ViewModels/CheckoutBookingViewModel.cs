namespace WinUIWpf.ViewModels;

using System;
using System.Threading.Tasks;

using Base.WpfMvvm;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

using Persistence;

public class CheckoutBookingViewModel : BaseViewModel
{
    public IWindowNavigator Controller { get; set; }

	//TODO

    public CheckoutBookingViewModel(IWindowNavigator controller, RoomDto roomDto)
    {
        Controller  = controller;
        throw new NotImplementedException();
    }

    public async Task LoadDataAsync()
    {
    	throw new NotImplementedException();
    }
    
}