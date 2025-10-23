namespace Wpf.ViewModels;

using System.IO;

using Base.WpfMvvm;

using Core.Contracts;

public class ImportTurtleViewModel : ValidatableBaseViewModel
{
    #region crt

    public  IWindowNavigator? Controller { get; set; }
    private IUnitOfWork       _uow;
    private IImportService    _importService;

    public ImportTurtleViewModel(IUnitOfWork uow, IImportService importService)
    {
        _uow           = uow;
        _importService = importService;
    }

    #endregion

    #region Properties/Commands

    private string _name = "Demo1";

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private string? _description = "Demo drawing";

    public string? Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    private string _origin = "Demo";

    public string Origin
    {
        get => _origin;
        set => SetProperty(ref _origin, value);
    }

    private string _fileName = @"ImportData/DemoDrawing1.txt";

    public string FileName
    {
        get => _fileName;
        set => SetProperty(ref _fileName, value);
    }


    public RelayCommand ImportCommand => new RelayCommand(async () => await ImportAsync(), () => CanImport());
    public RelayCommand CloseCommand  => new RelayCommand(() => Controller?.CloseWindow(), () => true);

    #endregion

    #region Operations

    public override async Task InitializeDataAsync()
    {
        await Task.CompletedTask;
    }

    private async Task ImportAsync()
    {
        await _importService.ImportScriptAsync(Name, Description, Origin, DateOnly.FromDateTime(DateTime.Today), FileName);
        Controller?.CloseWindow();
    }

    private bool CanImport()
    {
        return File.Exists(FileName);
    }

    #endregion
}