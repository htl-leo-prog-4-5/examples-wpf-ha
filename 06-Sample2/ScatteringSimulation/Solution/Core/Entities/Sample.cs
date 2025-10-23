using Base.Core.Entities;

namespace Core.Entities;

public class Sample : EntityObject
{
    public int SeqNo { get; set; }

    public double X { get; set; }

    public double Y { get; set; }

    public double Value { get; set; }

    public int         SimulationId { get; set; }
    public Simulation? Simulation   { get; set; }
}