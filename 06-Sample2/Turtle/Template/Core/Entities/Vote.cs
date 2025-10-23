using Base.Core.Entities;

namespace Core.Entities;

using System.ComponentModel.DataAnnotations;

public class Vote : EntityObject
{
    public DateTime Created { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    public int          CompetitionId { get; set; }
    public Competition? Competition   { get; set; }

    public int     ScriptId { get; set; }
    public Script? Script   { get; set; }
}