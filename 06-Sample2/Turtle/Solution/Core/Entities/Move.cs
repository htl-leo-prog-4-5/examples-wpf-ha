using Base.Core.Entities;

namespace Core.Entities;

public class Move : EntityObject
{
    public int SeqNo { get; set; }

    public int Direction { get; set; }

    public int Repeat { get; set; }

    public int? Color { get; set; }

    public int     ScriptId { get; set; }
    public Script? Script   { get; set; }
}