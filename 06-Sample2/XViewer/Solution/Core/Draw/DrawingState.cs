namespace Core.Draw
{
    using System.Windows.Media;

    public class DrawingState
    {
        public required double ScaleX { get; set; }
        public required double ScaleY { get; set; }
        public required double OffsetX { get; set; }
        public required double OffsetY { get; set; }

        public double ToX(double x) => (x + OffsetX) * ScaleX;
        public double ToY(double y) => TargetSizeY - (y + OffsetY) * ScaleY;

        public required double TargetSizeX { get; set; }
        public required double TargetSizeY { get; set; }

        public required Pen[] Pens { get; set; }
    }
}