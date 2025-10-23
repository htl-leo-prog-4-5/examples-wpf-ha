using Core.Entities;

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
}