using System.IO;
using System.Windows.Input;

namespace WPFApp.ViewModels;

using System.Linq;
using System.Reflection;

using Base.WpfMvvm;

using Logic.DTO;
using Logic.Helpers;

public class MatchViewModel : NotifyPropertyChanged
{
    #region Properties

    private static string BaseDirectory => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    private        string _filename = $"{BaseDirectory}\\Lask-vs-BLW.txt";

    public string FileName
    {
        get => _filename;
        set => SetProperty(ref _filename, value);
    }

    private Models.Match _match = new Models.Match() { Events = new System.Collections.ObjectModel.ObservableCollection<Models.Event>() };

    public Models.Match Match
    {
        get => _match;
        set => SetProperty(ref _match, value);
    }

    #endregion

    #region Operations

    public void Load()
    {
        var match = new Logic.MatchManager().Load(FileName);
        Match.TeamName1 = match.Team1.Name;
        Match.TeamName2 = match.Team2.Name;

        Match.Events.Clear();

        foreach (var w in match.Events)
        {
            Match.Events.Add(new Models.Event() { EventTime = (w.EventHalf, w.EventTime).ToEventTime(), EventText = w.Text, EventInfo = w.Information });
        }

        OnPropertyChanged(() => Match);
    }

    bool CanLoad()
    {
        return File.Exists(FileName);
    }

    public void Save()
    {
        Match.Events.Add(new Models.Event() { EventTime = Match.NewEventTime, EventText = Match.NewEventText, EventInfo = Match.NewEventInfo });

        var match = new Logic.DTO.Match()
        {
            Team1 = new Team { Name = Match.TeamName1 },
            Team2 = new Team { Name = Match.TeamName2 },
            Events = Match.Events.Select(w =>
            {
                var halfAndTime = w.EventTime.ToHalfAndTime();
                return new Logic.DTO.Event()
                {
                    EventTime   = halfAndTime.time,
                    EventHalf   = halfAndTime.half,
                    Text        = w.EventText,
                    Information = w.EventInfo
                };
            })
        };

        new Logic.MatchManager().Save(match, FileName);

        Match.NewEventTime = "";
        Match.NewEventText = "";
        Match.NewEventInfo = "";

        OnPropertyChanged(() => Match);
    }

    bool CanSave()
    {
        return !string.IsNullOrEmpty(Match.TeamName1) &&
               !string.IsNullOrEmpty(Match.TeamName2) &&
               !string.IsNullOrEmpty(FileName) &&
               !string.IsNullOrEmpty(Match.NewEventTime) &&
               !string.IsNullOrEmpty(Match.NewEventText) &&
               !string.IsNullOrEmpty(Match.NewEventInfo);
    }

    #endregion

    #region Commands

    public ICommand LoadCommand => new RelayCommand(Load, CanLoad);
    public ICommand SaveCommand => new RelayCommand(Save, CanSave);

    #endregion
}