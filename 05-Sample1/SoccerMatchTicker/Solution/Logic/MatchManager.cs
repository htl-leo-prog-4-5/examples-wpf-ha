namespace Logic;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Logic.DTO;

public class MatchManager
{
    public Match Load(string filename)
    {
        var lines = File.ReadAllLines(filename);

        var matchDescLine = lines.First();
        var matchColumns  = matchDescLine.Split(';');
        var eventLines    = lines.Skip(1);

        return new Match()
        {
            Team1 = new Team { Name = matchColumns[0] },
            Team2 = new Team { Name = matchColumns[1] },
            Events = eventLines.Select(line =>
                {
                    var content = line.Split(';');
                    return new Event()
                    {
                        EventHalf   = int.Parse(content[0]),
                        EventTime   = int.Parse(content[1]),
                        Text        = content[2],
                        Information = content[3],
                    };
                }
            ).ToArray()
        };
    }

    public void Save(Match match, string filename)
    {
        var lines = new List<string>();
        lines.Add($"{match.Team1.Name};{match.Team2.Name}");
        lines.AddRange(match.Events.Select(w => $"{w.EventHalf};{w.EventTime};{w.Text};{w.Information}"));

        File.WriteAllLines(filename, lines);
    }
}