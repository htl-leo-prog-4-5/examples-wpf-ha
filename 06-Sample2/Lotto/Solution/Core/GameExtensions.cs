using Base.Core.Entities;

using Core.Entities;

using System.Linq;

namespace Core;

public static class GameExtensions
{
    public static ICollection<byte> GetResult(this Game game)
    {
        if (game.No1.HasValue)
        {
            return new[] { game.No1.Value, game.No2.Value, game.No3.Value, game.No4.Value, game.No5.Value, game.No6.Value }.Order().ToList();
        }

        return Array.Empty<byte>();
    }
    public static ICollection<byte> GetResultZZ(this Game game)
    {
        if (game.No1.HasValue)
        {
            return new[] { game.No1.Value, game.No2.Value, game.No3.Value, game.No4.Value, game.No5.Value, game.No6.Value, game.NoX.Value }.Order().ToList();
        }

        return Array.Empty<byte>();
    }
}