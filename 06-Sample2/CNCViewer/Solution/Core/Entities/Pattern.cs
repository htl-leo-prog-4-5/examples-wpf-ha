namespace Core.Entities
{
    public partial class Pattern
    {
        public string Name { get; set; } = "";
        public List<CutLine> Lines { get; set; } = [];
        public double Width => Lines.Max(l => l.Width);
        public double Height => Lines.Max(l => l.Height);

        public double Left => Lines.Min(l => l.Points.Min(p => p.X));
        public double Top => Lines.Min(l => l.Points.Min(p => p.Y));
    }
}
