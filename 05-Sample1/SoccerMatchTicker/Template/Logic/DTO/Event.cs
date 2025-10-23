namespace Logic.DTO;

using System;

public class Event
{
    public int EventHalf { get; set; } // 0 first half, 1 second half, 2 first extension 3 ...

    public int EventTime { get; set; }

    public string Text        { get; set; }
    public string Information { get; set; }
}