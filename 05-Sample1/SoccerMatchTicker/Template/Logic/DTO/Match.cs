namespace Logic.DTO;

using System.Collections.Generic;

public class Match
{
    public Team Team1 { get; set; }
    public Team Team2 { get; set; }

    public IEnumerable<Event> Events { get; set; }
}