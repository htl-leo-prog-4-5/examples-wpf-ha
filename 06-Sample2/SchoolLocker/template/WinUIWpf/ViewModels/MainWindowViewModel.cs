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
        //TODO LoadData Async, use _uow
    }

    #endregion
}