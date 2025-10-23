namespace Wpf.ViewModels;

using System.Collections.ObjectModel;

using Base.WpfMvvm;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

public class MainWindowViewModel : ValidatableBaseViewModel
{
    #region crt

    public           IWindowNavigator? Controller { get; set; }
    private readonly IUnitOfWork       _uow;

    public MainWindowViewModel(IUnitOfWork uow)
    {
        _uow    = uow;
    }

    #endregion

    #region Properties/Commands

    public ObservableCollection<ScriptOverview> FilteredScripts { get; } = new();

    private ScriptOverview? _selectedScript;

    public ScriptOverview? SelectedScript
    {
        get => _selectedScript;
        set
        {
            if (SetProperty(ref _selectedScript, value))
            {
                if (_selectedScript is not null && !string.IsNullOrEmpty(_selectedScript.Origin))
                {
                    SelectedOrigin = Origins.SingleOrDefault(o => o.Name == _selectedScript.Origin);
                }
                else
                {
                    SelectedOrigin = Origins.SingleOrDefault(o => o.Id == 0);
                }

                IsEditMode = false;
            }
        }
    }

    public IList<Origin> FilterOrigins { get; set; } = new List<Origin>();

    private Origin? _selectedFilterOrigin;

    public Origin? SelectedFilterOrigin
    {
        get => _selectedFilterOrigin;
        set => SetProperty(ref _selectedFilterOrigin, value);
    }

    public IList<Origin> Origins { get; set; } = new List<Origin>();

    private Origin? _selectedOrigin;

    public Origin? SelectedOrigin
    {
        get => _selectedOrigin;
        set => SetProperty(ref _selectedOrigin, value);
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


    public RelayCommand FilterCommand => new RelayCommand(async () => await FilterAsync(), () => true);
    public RelayCommand DetailCommand => new RelayCommand(async () => await DetailAsync(), () => SelectedScript != null);
    public RelayCommand EditCommand   => new RelayCommand(Edit,                            () => SelectedScript != null && IsNotEditMode);
    public RelayCommand UpdateCommand => new RelayCommand(async () => await UpdateAsync(), () => IsEditMode);
    public RelayCommand ImportCommand => new RelayCommand(async () => await ImportAsync());

    #endregion

    #region Operations

    private void Edit()
    {
        IsEditMode = true;
    }

    private async Task UpdateAsync()
    {
        var inDb = (await _uow.ScriptRepository.GetByIdAsync(SelectedScript!.Id)) ?? throw new ArgumentNullException();
        inDb.OriginId = SelectedOrigin is null || SelectedOrigin.Id == 0 ? null : SelectedOrigin.Id;
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
        if (Controller != null && SelectedScript != null)
        {
            await Controller.ShowBacklogCommentWindowAsync(SelectedScript.Id);
        }

        await Load(_uow);
    }

    private async Task ImportAsync()
    {
        if (Controller is not null)
        {
            await Controller.ShowImportTurtleWindowAsync();
        }

        await Load(_uow);
    }

    public override async Task InitializeDataAsync()
    {
        FilterOrigins = await _uow.OriginRepository.GetNoTrackingAsync(orderBy: x => x.OrderBy(c => c.Name));

        var all = new Origin()
        {
            Name = "<All>",
            Code = "<All>",
        };

        FilterOrigins.Add(all);
        OnPropertyChanged(nameof(FilterOrigins));
        await Task.Delay(1);
        SelectedFilterOrigin = all;

        Origins = await _uow.OriginRepository.GetNoTrackingAsync(orderBy: x => x.OrderBy(c => c.Name));

        var empty = new Origin()
        {
            Name = "<Empty>",
            Code = "<Empty>",
        };

        Origins.Add(empty);
        OnPropertyChanged(nameof(Origins));

        await Load(_uow);
    }

    public async Task Load(IUnitOfWork uow)
    {
        var filtered = await uow.ScriptRepository
            .GetScriptOverviewAsync(
                SelectedFilterOrigin != null && SelectedFilterOrigin.Id > 0 ? SelectedFilterOrigin.Name : null);

        FilteredScripts.Clear();
        foreach (var filteredStation in filtered)
        {
            FilteredScripts.Add(filteredStation);
        }
    }

    #endregion
}