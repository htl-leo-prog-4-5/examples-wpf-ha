namespace Core.Entities
{
    public class MyDrawing
    {
        public required string Name { get; set; }

        public IList<Shape> Shapes { get; set; } = [];

        public double Width => MaxX - MinX;
        public double Height => MaxY - MinY;

        public double MinX => Shapes.Any() ? Shapes.Min(s => s.MinX) : 0.0;
        public double MaxX => Shapes.Any() ? Shapes.Max(s => s.MaxX) : 0.0;

        public double MinY => Shapes.Any() ? Shapes.Min(s => s.MinY) : 0.0;
        public double MaxY => Shapes.Any() ? Shapes.Max(s => s.MaxY) : 0.0;
    }
}