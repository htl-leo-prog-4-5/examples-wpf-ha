namespace Core.QueryResults;

using System;
using System.Collections.Generic;

using Core.Entities;

public class CompetitionSummary
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int      RaceCount { get; set; }
    public DateTime FirstRace { get; set; }

    public DateTime LastRace { get; set; }
}