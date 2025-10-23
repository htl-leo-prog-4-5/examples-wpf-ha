using Base.Core.Entities;

using Core.Entities;

using System.Linq;

namespace Core;

public static class TipExtensions
{
    public static ICollection<byte> Normalize(this ICollection<byte> tip)
    {
        return tip.Order().ToList();
    }

    public static ICollection<byte> Normalize(this Tip tip)
    {
        return new[] { tip.No1, tip.No2, tip.No3, tip.No4, tip.No5, tip.No6 }.Order().ToList();
    }

    public static ICollection<byte> SameNos(this Tip tip, IEnumerable<byte> gameResult)
    {
        var tipAr = new[] { tip.No1, tip.No2, tip.No3, tip.No4, tip.No5, tip.No6 };
        return tipAr.Union(gameResult).ToList();
    }

    public static Tip ToTip(this ICollection<byte> tip)
    {
        return new Tip
        {
            No1 = tip.ElementAt(0),
            No2 = tip.ElementAt(1),
            No3 = tip.ElementAt(2),
            No4 = tip.ElementAt(3),
            No5 = tip.ElementAt(4),
            No6 = tip.ElementAt(5)
        };
    }

    public static ICollection<byte> ToTip(uint no1, uint no2, uint no3, uint no4, uint no5, uint no6)
    {
        return new List<byte>
        {
            (byte)no1,
            (byte)no2,
            (byte)no3,
            (byte)no4,
            (byte)no5,
            (byte)no6
        };
    }
}