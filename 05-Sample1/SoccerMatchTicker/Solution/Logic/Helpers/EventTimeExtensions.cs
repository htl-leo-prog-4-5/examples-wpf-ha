namespace Logic.Helpers;

using Logic.DTO;

public static class EventTimeExtensions
{
    static readonly int[] _halfLengthAr = { 45, 45, 15, 15 };
    static readonly int[] _halfStartAr  = { 0, 45, 90, 105, 120, int.MaxValue };

    public static string ToEventTime(this (int half, int time) time)
    {
        var halfLength = _halfLengthAr[time.half];
        var halfStart  = _halfStartAr[time.half];

        if (time.time >= halfLength)
        {
            return $"{halfLength + halfStart}'+{time.time - halfLength}";
        }

        return $"{time.time + halfStart}'";
    }

    public static (int half, int time) ToHalfAndTime(this string time)
    {
        var parts = time.Replace("'", "").Split("+");
        int t     = int.Parse(parts[0]);
        int half  = 0;

        while (t >= _halfStartAr[half + 1])
        {
            half++;
        }

        if (parts.Length > 1)
        {
            half--;
            t += int.Parse(parts[1]);
        }

        return (half, t - _halfStartAr[half]);
    }
}