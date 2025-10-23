using Core.Contracts;
using Core.DataTransferObjects;

using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using WpfMvvmBase;

namespace WinUIWpf.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    #region crt

    public MainWindowViewModel(IUnitOfWork uow)
    {
        _uow                   = uow;
        AddSchoolLockerCommand = new RelayCommand(async c => await AddSchoolLockerAsync(), c => true);
        ShowBookingCommand     = new RelayCommand(async c => await ShowBookingAsync(),     c => SelectedLocker is not null);
    }

    private IUnitOfWork _uow;

    #endregion

    #region Properties

    public ObservableCollection<SchoolLockerDto> SchoolLockers { get; } = new ObservableCollection<SchoolLockerDto>();

    public SchoolLockerDto? SelectedLocker { get; set; }

    #endregion

    #region Commands

    public ICommand AddSchoolLockerCommand { get; set; }
    public ICommand ShowBookingCommand     { get; set; }

    #endregion

    #region Operations

    private async Task AddSchoolLockerAsync()
    {
        Controller?.ShowAddLockerWindow();
        await LoadDataAsync();
    }

    private async Task ShowBookingAsync()
    {
        Controller?.ShowBookingWindow(SelectedLocker!.Number);
        await LoadDataAsync();
    }

    public async Task LoadDataAsync()
    {
        var lockerDtos = await _uow.Lockers.GetLockersWithStateAsync();

        SchoolLockers.Clear();
        foreach (var locker in lockerDtos)
        {
            SchoolLockers.Add(locker);
        }
    }

    #endregion
}