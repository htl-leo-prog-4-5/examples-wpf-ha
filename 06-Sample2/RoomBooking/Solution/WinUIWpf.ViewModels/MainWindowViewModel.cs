namespace WinUIWpf.ViewModels;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Base.WpfMvvm;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

using Persistence;

public class RoomTypeDescription
{
    public RoomType? RoomType    { get; set; }
    public string    Description { get; set; } = string.Empty;
}

public class MainWindowViewModel : BaseViewModel
{
    private readonly IUnitOfWork _uow;

    private IWindowNavigator Controller;


    public List<RoomTypeDescription> RoomTypes { get; } = new()
    {
        new() { RoomType = null, Description              = "Alle" },
        new() { RoomType = RoomType.Standard, Description = RoomType.Standard.ToString() },
        new() { RoomType = RoomType.Premium, Description  = RoomType.Premium.ToString() },
        new() { RoomType = RoomType.Deluxe, Description   = RoomType.Deluxe.ToString() },
        new() { RoomType = RoomType.Suite, Description    = RoomType.Suite.ToString() },
    };

    private RoomTypeDescription _selectedRoomType;

    public RoomTypeDescription SelectedRoomType
    {
        get { return _selectedRoomType; }
        set
        {
            _selectedRoomType = value;
            OnPropertyChanged();
        }
    }

    private string _searchRoom = string.Empty;

    public string SearchRoom
    {
        get { return _searchRoom; }
        set
        {
            _searchRoom = value;
            OnPropertyChanged();
        }
    }

    private List<RoomDto>                 _allRooms = new();
    public  ObservableCollection<RoomDto> Rooms { get; } = new();

    private RoomDto? _selectedRoom;

    public RoomDto? SelectedRoom
    {
        get => _selectedRoom;
        set
        {
            _selectedRoom = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand CommandCheckin  { get; set; }
    public RelayCommand CommandCheckout { get; set; }

    public MainWindowViewModel(IWindowNavigator windowController)
    {
        Controller        = windowController;
        _selectedRoomType = RoomTypes[0];
        CommandCheckin = new RelayCommand(
            async () => await CheckinRoomAsync(),
            () => SelectedRoom != null && SelectedRoom!.CurrentlyAvailable);
        CommandCheckout = new RelayCommand(
            async () => await CheckoutRoomAsync(),
            () => SelectedRoom != null && !SelectedRoom!.CurrentlyAvailable);
        _uow            =  new UnitOfWork();
        PropertyChanged += MainWindowViewModel_PropertyChanged;
    }

    private async void MainWindowViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(SearchRoom):
            case nameof(SelectedRoomType):
                await LoadDataAsync();
                break;
            default: break;
        }
    }

    private async Task CheckinRoomAsync()
    {
        //MessageBox.Show($"Todo: Buche die Anreise eines Gastes für das Zimmer {SelectedRoom!.RoomNumber}", "TODO");

        await Controller.ShowCheckinRoomAsync(SelectedRoom!);
        await LoadDataAsync();
    }

    private async Task CheckoutRoomAsync()
    {
        await Controller.ShowCheckoutRoomAsync(SelectedRoom!);
        await LoadDataAsync();
    }

    public async Task LoadDataAsync()
    {
        _allRooms = await _uow.Rooms.GetRoomWithBookingsAsync(SelectedRoomType.RoomType, SearchRoom);

        Rooms.Clear();
        foreach (var r in _allRooms)
        {
            Rooms.Add(r);
        }
    }
}