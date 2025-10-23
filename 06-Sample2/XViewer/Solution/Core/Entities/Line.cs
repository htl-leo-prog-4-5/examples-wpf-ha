using System.Windows;
using System.Windows.Media;
using Core.Draw;

namespace Core.Entities
{
    public class Line : Shape
    {
        public (double x, double y) EndPoint { get; set; } = (0.0, 0.0);

        public override double MinY => Math.Min(StartPoint.y, EndPoint.y);
        public override double MaxY => Math.Max(StartPoint.y, EndPoint.y);
        public override double MinX => Math.Min(StartPoint.x, EndPoint.x);
        public override double MaxX => Math.Max(StartPoint.x, EndPoint.x);

        public override void Draw(DrawingContext context, DrawingState state)
        {
            context.DrawLine(
                state.Pens[ColorIdx],
                new Point(state.ToX(StartPoint.x), state.ToY(StartPoint.y)),
                new Point(state.ToX(EndPoint.x), state.ToY(EndPoint.y)));
        }
    }
}