namespace WinUIWpf.ViewModels;

using Core.Contracts;

using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Base.WpfMvvm;

using Core.Entities;
using Core.QueryResults;

public class RacesViewModel : BaseViewModel
{
    #region ctr

    public RacesViewModel(IUnitOfWork uow)
    {
        _uow = uow;
    }

    private IUnitOfWork _uow;

    #endregion

    #region Properties

    public CompetitionSummary CompetitionSummary { get; set; } = default!;

    public IWindowNavigator? Controller { get; set; }

    public ObservableCollection<Race> Races               { get; set; } = new ObservableCollection<Race>();
    public Race?                      SelectedRace { get; set; }

    public RelayCommand EditRaceCommand => new RelayCommand(EditRace, () => SelectedRace != null);
    public RelayCommand DeleteRaceCommand => new RelayCommand(async () => await DeleteRace(), () => SelectedRace != null);

    #endregion

    #region Operations

    public async Task LoadDataAsync()
    {
        Races.Clear();
        var races = await _uow.Race.GetNoTrackingAsync(r => r.CompetitionId == CompetitionSummary.Id,null,"Driver", "Car");
        foreach (var v in races)
        {
            Races.Add(v);
        }
    }

    private void EditRace()
    {
        Controller?.ShowMovesWindow(SelectedRace!);
    }

    private async Task DeleteRace()
    {
        if (SelectedRace != null && (Controller?.AskYesNoMessageBox("Question", $"Delete race: Driver: {SelectedRace.Driver!.Name}, Car: {SelectedRace.Car!.Name}?") ?? false))
        {
            var race = await _uow.Race.GetByIdAsync(SelectedRace.Id);
            _uow.Race.Remove(race!);
            await _uow.SaveChangesAsync();
            Races.Remove(SelectedRace);
        }
    }

    #endregion
}