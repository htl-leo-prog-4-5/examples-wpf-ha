namespace Core.Turtle
{
    public record Move(
        Direction Direction,
        int       Count,
        int?      Color
    );
}