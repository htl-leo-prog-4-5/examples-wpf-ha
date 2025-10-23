using System.Windows;
using System.Windows.Media;
using Core.Draw;

namespace Core.Entities
{
    public class Rectangle : Shape
    {
        public (double x, double y) EndPoint { get; set; } = (0.0, 0.0);

        public override double MinY => Math.Min(StartPoint.y, EndPoint.y);
        public override double MaxY => Math.Max(StartPoint.y, EndPoint.y);
        public override double MinX => Math.Min(StartPoint.x, EndPoint.x);
        public override double MaxX => Math.Max(StartPoint.x, EndPoint.x);

        public override void Draw(DrawingContext context, DrawingState state)
        {
            throw new NotImplementedException();
        }
    }
}