using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

using Microsoft.EntityFrameworkCore;

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using WpfMvvmBase;

namespace WinUIWpf.ViewModels;

public class ShowBookingWindowViewModel : BaseViewModel
{
    #region crt

    public ShowBookingWindowViewModel(IUnitOfWork uow)
    {
        _uow         = uow;
        CloseCommand = new RelayCommand(_ => Controller?.CloseWindow());
    }

    private IUnitOfWork _uow;

    #endregion

    #region Properties

    private int _lockerNo;

    public int LockerNo
    {
        get => _lockerNo;
        set => SetProperty(ref _lockerNo, value);
    }

    public ObservableCollection<Booking> Bookings { get; } = new ObservableCollection<Booking>();

    #endregion

    #region Commands

    public ICommand CloseCommand { get; set; }

    #endregion

    #region Operations

    public async Task LoadBookingAsync()
    {
        var locker = await _uow.Lockers.GetByNumberAsync(LockerNo);

        Bookings.Clear();

        if (locker is not null)
        {
            foreach (var booking in locker.Bookings)
            {
                Bookings.Add(booking);
            }
        }
    }

    #endregion
}