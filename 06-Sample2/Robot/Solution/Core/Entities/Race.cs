using System;

namespace Core.Entities;

using Base.Core.Entities;

using System.Collections.Generic;

public class Race : EntityObject
{
    public DateTime RaceStartTime { get; set; }

    public TimeOnly RaceTime { get; set; }

    public Competition? Competition   { get; set; }
    public int          CompetitionId { get; set; }

    public Driver? Driver   { get; set; }
    public int     DriverId { get; set; }

    public Car? Car   { get; set; }
    public int  CarId { get; set; }

    public IList<Move>? Moves { get; set; }
}