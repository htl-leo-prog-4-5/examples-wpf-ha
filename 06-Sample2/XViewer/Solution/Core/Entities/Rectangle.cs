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
            var pt1 = new Point(state.ToX(StartPoint.x), state.ToY(StartPoint.y));
            var pt2 = new Point(state.ToX(EndPoint.x), state.ToY(StartPoint.y));
            var pt3 = new Point(state.ToX(EndPoint.x), state.ToY(EndPoint.y));
            var pt4 = new Point(state.ToX(StartPoint.x), state.ToY(EndPoint.y));

            context.DrawLine(state.Pens[ColorIdx], pt1, pt2);
            context.DrawLine(state.Pens[ColorIdx], pt2, pt3);
            context.DrawLine(state.Pens[ColorIdx], pt3, pt4);
            context.DrawLine(state.Pens[ColorIdx], pt4, pt1);
        }
    }
}