namespace Core.Entities;

using System.ComponentModel.DataAnnotations.Schema;

using Base.Core.Entities;

public class RouteStep : EntityObject
{
    public int No { get; set; }

    public required string Description { get; set; }

    public int    RouteId { get; set; }
    public Route? Route   { get; set; }

    public int?   HotelId { get; set; }
    public Hotel? Hotel   { get; set; }

    public int?   PlaneId { get; set; }
    public Plane? Plane   { get; set; }

    public int?  ShipId { get; set; }
    public Ship? Ship   { get; set; }


    [NotMapped]
    public string TypeInfo
    {
        get
        {
            if (HotelId.HasValue)
            {
                return "Hotel";
            }
            else if (PlaneId.HasValue)
            {
                return "Plane";
            }
            else if (ShipId.HasValue)
            {
                return "Ship";
            }

            return "???";
        }
    }

    [NotMapped]
    public string TypeInfoName
    {
        get
        {
            if (HotelId.HasValue)
            {
                return Hotel!.Name;
            }
            else if (PlaneId.HasValue)
            {
                return Plane!.Model;
            }
            else if (ShipId.HasValue)
            {
                return Ship!.Name;
            }

            return "???";
        }
    }
}