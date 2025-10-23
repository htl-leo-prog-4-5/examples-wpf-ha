namespace WinUIWpf.ViewModels;

using Core.Contracts;

using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Base.WpfMvvm;

using Core.Entities;
using Core.QueryResults;

public class MainViewModel : BaseViewModel
{
    #region ctr

    public MainViewModel(IUnitOfWork uow)
    {
        _uow = uow;
    }

    private IUnitOfWork _uow;

    #endregion

    #region Properties

    public IWindowNavigator? Controller { get; set; }

    public ObservableCollection<CompetitionSummary> Competitions        { get; set; } = new ObservableCollection<CompetitionSummary>();
    public CompetitionSummary?                      SelectedCompetition { get; set; }

    public RelayCommand ShowRacesCommand => new RelayCommand(ShowRaces, () => SelectedCompetition != null);

    #endregion

    #region Operations

    public async Task LoadDataAsync()
    {
        Competitions.Clear();
        var competitionSummaries = await _uow.Competition.GetSummaryAsync();
        foreach (var v in competitionSummaries)
        {
            Competitions.Add(v);
        }
    }

    private void ShowRaces()
    {
        Controller?.ShowRacesWindow(SelectedCompetition!);
    }

    #endregion
}