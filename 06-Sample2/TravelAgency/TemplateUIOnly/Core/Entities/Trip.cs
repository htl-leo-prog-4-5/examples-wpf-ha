using Base.Core.Entities;

namespace Core.Entities;

public class Trip : EntityObject
{
    public int      RouteId           { get; set; }
    public Route?   Route             { get; set; }
    public DateTime DepartureDateTime { get; set; }
    public DateTime ArrivalDateTime   { get; set; }
}