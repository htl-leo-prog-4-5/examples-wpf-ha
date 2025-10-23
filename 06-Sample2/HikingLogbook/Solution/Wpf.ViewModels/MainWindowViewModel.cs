namespace Wpf.ViewModels;

using System.Collections.ObjectModel;
using System.Linq.Expressions;

using Base.WpfMvvm;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

public class MainWindowViewModel : ValidatableBaseViewModel
{
    #region crt

    public  IWindowNavigator? Controller { get; set; }
    private IUnitOfWork       _uow;

    public MainWindowViewModel(IUnitOfWork uow)
    {
        _uow = uow;
    }

    #endregion

    #region Properties/Commands

    public ObservableCollection<HikeOverview> FilteredHikes { get; } = new();

    private HikeOverview? _selectedHike;

    public HikeOverview? SelectedHike
    {
        get => _selectedHike;
        set
        {
            if (SetProperty(ref _selectedHike, value))
            {
                Location = _selectedHike?.Location;
                Duration = _selectedHike?.Duration;
                Distance = _selectedHike?.Distance;

                IsEditMode = false;
            }
        }
    }

    public IList<Companion> Companions { get; set; } = new List<Companion>();

    private Companion? _selectedCompanion;

    public Companion? SelectedCompanion
    {
        get => _selectedCompanion;
        set => SetProperty(ref _selectedCompanion, value);
    }

    private bool _isEditMode;

    public bool IsEditMode
    {
        get => _isEditMode;
        set
        {
            if (SetProperty(ref _isEditMode, value))
            {
                OnPropertyChanged(nameof(IsNotEditMode));
            }
        }
    }

    public bool IsNotEditMode
    {
        get => !IsEditMode;
    }

    private string? _location;

    public string? Location
    {
        get => _location;
        set => SetProperty(ref _location, value);
    }

    private decimal? _duration;

    public decimal? Duration
    {
        get => _duration;
        set => SetProperty(ref _duration, value);
    }

    private decimal? _distance;

    public decimal? Distance
    {
        get => _distance;
        set => SetProperty(ref _distance, value);
    }

    public RelayCommand FilterCommand => new RelayCommand(async () => await FilterAsync(), () => true);
    public RelayCommand DetailCommand => new RelayCommand(async () => await DetailAsync(), () => SelectedHike != null);
    public RelayCommand EditCommand   => new RelayCommand(Edit,                            () => SelectedHike != null && IsNotEditMode);
    public RelayCommand UpdateCommand => new RelayCommand(async () => await Update(),      () => IsEditMode);

    #endregion

    #region Operations

    private void Edit()
    {
        IsEditMode = true;
    }

    private async Task Update()
    {
        var inDb = (await _uow.HikeRepository.GetByIdAsync(SelectedHike!.Id)) ?? throw new ArgumentNullException();
        inDb.Location = Location!;
        inDb.Duration = Duration ?? 0.0m;
        inDb.Distance = Distance ?? 0.0m;
        await _uow.SaveChangesAsync();
        await InitializeDataAsync();

        IsEditMode = false;
    }

    private async Task FilterAsync()
    {
        await Load(_uow);
    }

    private async Task DetailAsync()
    {
        if (Controller != null && SelectedHike != null)
        {
            await Controller.ShowHikeHighlightsWindowAsync(SelectedHike.Id);
        }

        await Load(_uow);
    }

    public override async Task InitializeDataAsync()
    {
        Companions = await _uow.CompanionRepository.GetNoTrackingAsync(orderBy: x => x.OrderBy(c => c.Name));

        var all = new Companion()
        {
            Name = "<Alle>"
        };

        Companions.Add(all);
        OnPropertyChanged(nameof(Companions));
        await Task.Delay(1);
        SelectedCompanion = all;
        await Load(_uow);
    }

    public async Task Load(IUnitOfWork uow)
    {
        var filtered = await uow.HikeRepository
            .GetHikesAsync(
                SelectedCompanion != null && SelectedCompanion.Id > 0 ? SelectedCompanion.Name : null);

        FilteredHikes.Clear();
        foreach (var filteredStation in filtered)
        {
            FilteredHikes.Add(filteredStation);
        }
    }

    #endregion
}