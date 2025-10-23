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
    private IUnitOfWork _uow;

    private IWindowNavigator Controller;

    private Booking _booking;

    public Booking Booking
    {
        set
        {
            _booking = value;
            OnPropertyChanged();
        }
        get { return _booking; }
    }

    private DateTime _bookedUntil;

    public DateTime BookedUntil
    {
        set
        {
            if (value <= _booking.From)
            {
                _bookedUntil = _booking.From.AddDays(1);
            }
            else
            {
                _bookedUntil = value;
            }

            var fromDate = DateTime.Parse(_booking.From.ToShortDateString());
            var toDate   = DateTime.Parse(_bookedUntil.ToShortDateString());
            _days = (toDate - fromDate).Days;
            OnPropertyChanged();
            OnPropertyChanged(nameof(DisplayOvernights));
        }
        get { return _bookedUntil; }
    }

    private int    _days;
    public  string DisplayOvernights => $"{_days} {(_days > 1 ? "Nächte" : "Nacht")}";

    public RelayCommand CommandUndo { get; set; }
    public RelayCommand CommandSave { get; set; }

    public CheckoutBookingViewModel(IWindowNavigator controller, RoomDto roomDto)
    {
        Controller  = controller;
        _booking    = roomDto.CurrentBooking!;
        CommandUndo = new RelayCommand(() => Controller.CloseWindow(), () => true);
        CommandSave = new RelayCommand(async () => await SaveAsync(),  () => _days > 0);
        _uow        = new UnitOfWork();
    }

    private async Task SaveAsync()
    {
        Booking.To = BookedUntil;
        await _uow.SaveChangesAsync();
        Controller.CloseWindow();
    }

    public async Task LoadDataAsync()
    {
        Booking     = (await _uow.Bookings.GetByIdAsync(Booking.Id, nameof(Core.Entities.Booking.Customer), nameof(Core.Entities.Booking.Room)))!;
        BookedUntil = Booking.To ?? DateTime.Now;
    }
}