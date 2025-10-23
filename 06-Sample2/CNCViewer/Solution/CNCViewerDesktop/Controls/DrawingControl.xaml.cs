using Core.Entities;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CNCViewerDesktop.Controls
{
    /// <summary>
    /// Interaction logic for DrawingControl.xaml
    /// </summary>
    public partial class DrawingControl : UserControl
    {

        public DrawingControl()
        {
            InitializeComponent();
        }
        public static DependencyProperty PatternProperty = DependencyProperty.Register(
            nameof(Pattern),
            typeof(Pattern),
            typeof(DrawingControl),
            new PropertyMetadata(Pattern.Demo,
                OnScaleXChanged));

        public Pattern Pattern
        {
            get => (Pattern)GetValue(PatternProperty);
            set => SetValue(PatternProperty, value);
        }

        private static void OnScaleXChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (DrawingControl)dependencyObject;
            ctrl.InvalidateVisual();
        }

        private static readonly Pen RedPen = new Pen(new SolidColorBrush(Colors.DarkRed), 2.0d);

        protected override void OnRender(DrawingContext drawingContext)
        {
            DrawLines(drawingContext);
        }

        private double _scale = 1.0;
        private double _offsetX = 1.0;
        private double _offsetY = 1.0;

        Point ScalePoint(LinePoint point)
        {
            return new Point
            {
                X = (point.X + _offsetX) * _scale,
                Y = ActualHeight - ((point.Y + _offsetY) * _scale)
            };
        }

        private void DrawLine(DrawingContext context, CutLine line)
        {
            var points = line.Points;
            var from = ScalePoint(points.First());
            foreach (var pt in points.Skip(1))
            {
                var to = ScalePoint(pt);
                context.DrawLine(RedPen, from, to);
                from = to;
            }
        }

        private void DrawLines(DrawingContext context)
        {
            _scale = Math.Min(ActualWidth / Pattern!.Width, ActualHeight / Pattern.Height);
            _offsetX = (ActualWidth - Pattern!.Width * _scale) / 2 / _scale;
            _offsetY = -Pattern!.Top;

            if (_scale == 0.0) return;

            foreach (var line in Pattern.Lines)
            {
                DrawLine(context, line);
            }
        }
    }
}
