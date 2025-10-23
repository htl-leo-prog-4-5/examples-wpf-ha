namespace Logic.Helpers;

using System;

using Logic.DTO;

public static  class EventTimeExtensions
{
    public static string ToEventTime(this (int half, int time) time)
    {
        throw new NotImplementedException();
        //TODO Convert e.g. (1,46) to 90'+1
    }

    public static (int half, int time) ToHalfAndTime(this string time)
    {
        throw new NotImplementedException();
        //TODO Convert e.g. 90'+1 to (1,46)
    }
}