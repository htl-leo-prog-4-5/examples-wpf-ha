using Base.Core.Entities;

namespace Core.Entities;

public class Hotel : EntityObject
{
    public required string  Name          { get; set; }
    public required string  Location      { get; set; }
    public          decimal PricePerNight { get; set; }
    public          decimal Rating        { get; set; }
}