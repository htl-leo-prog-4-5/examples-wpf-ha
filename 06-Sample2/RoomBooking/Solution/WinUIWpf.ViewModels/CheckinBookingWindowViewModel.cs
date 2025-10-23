namespace WinUIWpf.ViewModels;

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Base.WpfMvvm;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

using Persistence;

public class CheckinBookingWindowViewModel : BaseViewModel
{
    private IWindowNavigator Controller;

    private readonly IUnitOfWork _uow;
    public           RoomDto     Room { get; set; }
    public           DateTime    From { get; set; } = DateTime.Today;

    public ICommand CancelCommand { get; set; }
    public ICommand SaveCommand   { get; set; }

    public ObservableCollection<Customer> Customers { get; set; }

    private Customer? _selectedCustomer;

    public Customer? SelectedCustomer
    {
        get => _selectedCustomer;
        set
        {
            _selectedCustomer = value;
            OnPropertyChanged();
        }
    }

    public CheckinBookingWindowViewModel(IWindowNavigator controller, RoomDto room)
    {
        Controller = controller;
        _uow       = new UnitOfWork();
        Room       = room;
        OnPropertyChanged(nameof(Room));
        Customers = new ObservableCollection<Customer>();
        CancelCommand = new RelayCommand(
            () => Controller.CloseWindow(),
            () => true
        );

        SaveCommand = new RelayCommand(
            async () => await SaveAsync(),
            () => SelectedCustomer != null
        );
    }

    public async Task SaveAsync()
    {
        await _uow.Bookings.AddAsync(new Booking
        {
            CustomerId = SelectedCustomer!.Id,
            RoomId     = Room.RoomId,
            From       = From
        });
        await _uow.SaveChangesAsync();
        Controller.CloseWindow();
    }

    public async Task LoadDataAsync()
    {
        var customers = await _uow.Customers.GetAsync(null, orderBy: cust => cust.OrderBy(c => c.LastName).ThenBy(c => c.FirstName));
        Customers.Clear();
        foreach (var customer in customers)
        {
            Customers.Add(customer);
        }
        //SelectedCustomer = Customers.First();
    }
}