namespace Wpf.ViewModels;

using System.Collections.ObjectModel;

using Base.WpfMvvm;

using Core.Contracts;
using Core.Entities;

public class NewGameViewModel : ValidatableBaseViewModel
{
    #region crt

    public  IWindowNavigator? Controller { get; set; }
    private IUnitOfWork       _uow;

    public NewGameViewModel(IUnitOfWork uow)
    {
        _uow = uow;

        NewCommand   = new RelayCommand(async () => await NewAsync(),    () => true);
        CloseCommand = new RelayCommand(() => Controller?.CloseWindow(), () => true);
    }

    #endregion

    #region Properties/Commands

    private DateTime _dateFrom = DateTime.Today;

    public DateTime DateFrom
    {
        get => _dateFrom;
        set => SetProperty(ref _dateFrom, value);
    }

    private DateTime _dateTo = DateTime.Today + TimeSpan.FromDays(7);

    public DateTime DateTo
    {
        get => _dateTo;
        set => SetProperty(ref _dateTo, value);
    }

    private DateTime _drawDate = DateTime.Today + TimeSpan.FromDays(8);

    public DateTime DrawDate
    {
        get => _drawDate;
        set => SetProperty(ref _drawDate, value);
    }


    public RelayCommand NewCommand   { get; set; }
    public RelayCommand CloseCommand { get; set; }

    #endregion

    #region Operations

    private async Task NewAsync()
    {
        if (DateTo <= DateFrom)
        {
            Controller!.ShowMessageBox($"Error: Date-From(${DateFrom.ToShortDateString()}) is after Date-To({DateTo.ToShortDateString()})");
        }
        else if (DateTo < DateTime.Today)
        {
            Controller!.ShowMessageBox($"Error: Date-From({DateFrom.ToShortDateString()}) must be in the future ({DateTime.Today.ToShortDateString()})");
        }
        else if (DrawDate <= DateTo)
        {
            Controller!.ShowMessageBox($"Draw-Date(${DateFrom.ToShortDateString()}) must be after Date-To({DateTo.ToShortDateString()})");
        }
        else
        {
            var newGame = new Game()
            {
                DateFrom         = DateOnly.FromDateTime(DateFrom),
                DateTo           = DateOnly.FromDateTime(DateTo),
                ExpectedDrawDate = DateOnly.FromDateTime(DrawDate),
            };

            await _uow.GameRepository.AddAsync(newGame);
            await _uow.SaveChangesAsync();
            Controller!.CloseWindow();
        }
    }

    public override async Task InitializeDataAsync()
    {
        await Task.CompletedTask;
    }

    #endregion
}