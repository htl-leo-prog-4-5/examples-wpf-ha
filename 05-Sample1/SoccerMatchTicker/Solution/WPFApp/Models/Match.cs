using System.Collections.ObjectModel;

namespace WPFApp.Models;

public class Match
{
    public string TeamName1 { get; set; }
    public string TeamName2 { get; set; }

    public string NewEventTime { get; set; }
    public string NewEventText { get; set; }
    public string NewEventInfo { get; set; }

    public ObservableCollection<Event> Events { get; set; }
}