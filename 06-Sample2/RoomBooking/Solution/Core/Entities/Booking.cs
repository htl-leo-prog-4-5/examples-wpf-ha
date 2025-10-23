using Core.Validations;

using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

using Base.Core.Entities;

public class Booking : EntityObject
{
    [ForeignKey(nameof(CustomerId))]
    public Customer? Customer { get; set; }

    public int CustomerId { get; set; }

    [ForeignKey(nameof(RoomId))]
    public Room? Room { get; set; }

    public int RoomId { get; set; }

    public DateTime From { get; set; }

    [BookingRange]
    public DateTime? To { get; set; }

    public int? Days
    {
        get
        {
            if (To == null)
            {
                return null;
            }

            var fromDate = DateTime.Parse(From.ToShortDateString());
            var toDate   = DateTime.Parse(To.Value.ToShortDateString());
            return (toDate - fromDate).Days;
        }
    }

    public override string ToString()
    {
        return $"{Room} From {From.ToShortDateString()} To: {To?.ToShortDateString()}";
    }
}