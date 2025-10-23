namespace Wpf.ViewModels;

using System.Collections.ObjectModel;
using System.IO;

using Base.WpfMvvm;

using Core.Contracts;
using Core.DataTransferObjects;
using Core.Entities;

public class MainWindowViewModel : ValidatableBaseViewModel
{
    #region crt

    public           IWindowNavigator? Controller { get; set; }
    private readonly IUnitOfWork       _uow;
    private readonly IImportService    _import;

    public MainWindowViewModel(IUnitOfWork uow, IImportService import)
    {
        _uow    = uow;
        _import = import;
    }

    #endregion

    #region Properties/Commands

    public ObservableCollection<SimulationOverview> FilteredSimulations { get; } = new();

    private SimulationOverview? _selectedSimulation;

    public SimulationOverview? SelectedSimulation
    {
        get => _selectedSimulation;
        set
        {
            if (SetProperty(ref _selectedSimulation, value))
            {
                if (_selectedSimulation is not null && !string.IsNullOrEmpty(_selectedSimulation.Origin))
                {
                    SelectedOrigin = Origins.SingleOrDefault(o => o.Name == _selectedSimulation.Origin);
                }
                else
                {
                    SelectedOrigin = Origins.SingleOrDefault(o => o.Id == 0);
                }

                if (_selectedSimulation is not null && !string.IsNullOrEmpty(_selectedSimulation.Category))
                {
                    SelectedCategory = Categories.SingleOrDefault(o => o.Description == _selectedSimulation.Category);
                }
                else
                {
                    SelectedCategory = Categories.SingleOrDefault(c => c.Id == 0);
                }

                IsEditMode = false;
            }
        }
    }

    public IList<Category> FilterCategories { get; set; } = new List<Category>();

    private Category? _selectedFilterCategory;

    public Category? SelectedFilterCategory
    {
        get => _selectedFilterCategory;
        set => SetProperty(ref _selectedFilterCategory, value);
    }

    public IList<Category> Categories { get; set; } = new List<Category>();

    private Category? _selectedCategory;

    public Category? SelectedCategory
    {
        get => _selectedCategory;
        set => SetProperty(ref _selectedCategory, value);
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

    private string _fileName  = "GaussianDistributionX_Info";
    private string _extension = "txt";

    public string FileName
    {
        get => _fileName;
        set
        {
            if (SetProperty(ref _fileName, value))
            {
                OnPropertyChanged(nameof(CalcFileName));
            }
        }
    }

    public string Extension
    {
        get => _extension;
        set
        {
            if (SetProperty(ref _extension, value))
            {
                OnPropertyChanged(nameof(CalcFileName));
            }
        }
    }

    public string CalcFileName
    {
        get => $"ImportData\\AddDistributions\\{FileName}.{Extension}";
    }


    public RelayCommand FilterCommand => new RelayCommand(async () => await FilterAsync(), () => true);
    public RelayCommand DetailCommand => new RelayCommand(async () => await DetailAsync(), () => SelectedSimulation != null);
    public RelayCommand EditCommand   => new RelayCommand(Edit,                            () => SelectedSimulation != null && IsNotEditMode);
    public RelayCommand UpdateCommand => new RelayCommand(async () => await Update(),      () => IsEditMode);
    public RelayCommand ImportCommand => new RelayCommand(async () => await Import(),      CanImport);

    #endregion

    #region Operations

    private void Edit()
    {
        IsEditMode = true;
    }

    private async Task Update()
    {
        var inDb = (await _uow.SimulationRepository.GetByIdAsync(SelectedSimulation!.Id)) ?? throw new ArgumentNullException();
        inDb.OriginId   = SelectedOrigin is null || SelectedOrigin.Id == 0 ? null : SelectedOrigin.Id;
        inDb.CategoryId = SelectedCategory is null || SelectedCategory.Id == 0 ? null : SelectedCategory.Id;
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
        if (Controller != null && SelectedSimulation != null)
        {
            await Controller.ShowBacklogCommentWindowAsync(SelectedSimulation.Id);
        }

        await Load(_uow);
    }

    public async Task Import()
    {
        await _import.ImportSampleAsync(CalcFileName);
        await InitializeDataAsync();
    }

    public bool CanImport()
    {
        return File.Exists(CalcFileName);
    }

    public override async Task InitializeDataAsync()
    {
        FilterCategories = await _uow.CategoryRepository.GetNoTrackingAsync(orderBy: x => x.OrderBy(c => c.Description));
        Categories       = await _uow.CategoryRepository.GetNoTrackingAsync(orderBy: x => x.OrderBy(c => c.Description));

        var all = new Category()
        {
            Description = "<All>",
        };

        FilterCategories.Add(all);
        OnPropertyChanged(nameof(FilterCategories));
        await Task.Delay(1);
        SelectedFilterCategory = all;

        var emptyCategory = new Category()
        {
            Description = "<Empty>",
        };

        Categories.Add(emptyCategory);
        OnPropertyChanged(nameof(Categories));

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
        var filtered = await uow.SimulationRepository
            .GetSimulationsAsync(
                SelectedFilterCategory != null && SelectedFilterCategory.Id > 0 ? SelectedFilterCategory.Description : null);

        FilteredSimulations.Clear();
        foreach (var filteredStation in filtered)
        {
            FilteredSimulations.Add(filteredStation);
        }
    }

    #endregion
}