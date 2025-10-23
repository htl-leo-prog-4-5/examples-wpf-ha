namespace Core.Entities
{

    public class CutLine
    {
        public List<LinePoint> Points { get; set; } = [];
        public double Width => Points.Max(p => p.X) - Points.Min(p => p.X);
        public double Height => Points.Max(p => p.Y) - Points.Min(p => p.Y);
    }
}
