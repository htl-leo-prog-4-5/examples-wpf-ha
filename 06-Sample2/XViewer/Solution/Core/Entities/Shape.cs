using System.Windows.Media;
using Core.Draw;

namespace Core.Entities
{
    public abstract class Shape
    {
        public (double x, double y) StartPoint { get; set; } = (0.0, 0.0);

        public int ColorIdx { get; set; } = 0;

        public abstract void Draw(DrawingContext context, DrawingState state);


        public virtual double MinY => StartPoint.y;
        public virtual double MaxY => StartPoint.y;
        public virtual double MinX => StartPoint.x;
        public virtual double MaxX => StartPoint.x;
    }
}