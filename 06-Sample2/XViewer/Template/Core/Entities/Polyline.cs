using System.Net;
using System.Windows;
using System.Windows.Media;
using Core.Draw;

namespace Core.Entities
{
    public class Polyline : Shape
    {
        public override double MinY => Math.Min(StartPoint.y, Points.Min(pt => pt.y));
        public override double MaxY => Math.Max(StartPoint.y, Points.Max(pt => pt.y));
        public override double MinX => Math.Min(StartPoint.x, Points.Min(pt => pt.x));
        public override double MaxX => Math.Max(StartPoint.x, Points.Max(pt => pt.x));

        public IList<(double x, double y)> Points = []; // must be EXCLUDING StartPoint

        public override void Draw(DrawingContext context, DrawingState state)
        {
            throw new NotImplementedException();
        }
    }
}