using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Core.Draw;
using Core.Entities;

namespace Wpf.Controls
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

        public static DependencyProperty DrawingProperty = DependencyProperty.Register(
            nameof(Drawing),
            typeof(MyDrawing),
            typeof(DrawingControl),
            new PropertyMetadata(null, OnMyDrawingChanged));

        public MyDrawing? Drawing
        {
            get => (MyDrawing?) GetValue(DrawingProperty);
            set => SetValue(DrawingProperty, value);
        }

        private static void OnMyDrawingChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (DrawingControl) dependencyObject;
            ctrl.InvalidateVisual();
        }

        private static readonly Pen[] Pens =
        {
            new Pen(new SolidColorBrush(Colors.Red), 1.0d),
            new Pen(new SolidColorBrush(Colors.Orange), 1.0d),
            new Pen(new SolidColorBrush(Colors.Yellow), 1.0d),
            new Pen(new SolidColorBrush(Colors.Green), 1.0d),
            new Pen(new SolidColorBrush(Colors.Blue), 1.0d),
            new Pen(new SolidColorBrush(Colors.DarkBlue), 1.0d),
            new Pen(new SolidColorBrush(Colors.Violet), 1.0d),
            new Pen(new SolidColorBrush(Colors.DarkViolet), 1.0d),
            new Pen(new SolidColorBrush(Colors.Black), 1.0d),
            new Pen(new SolidColorBrush(Colors.White), 1.0d),
            new Pen(new SolidColorBrush(Colors.Gray), 1.0d),
            new Pen(new SolidColorBrush(Colors.Magenta), 1.0d),
            new Pen(new SolidColorBrush(Colors.Olive), 1.0d),
            new Pen(new SolidColorBrush(Colors.DarkGreen), 1.0d),
            new Pen(new SolidColorBrush(Colors.Peru), 1.0d),
            new Pen(new SolidColorBrush(Colors.Aqua), 1.0d)
        };

        protected override void OnRender(DrawingContext drawingContext)
        {
            DrawLines(drawingContext);
        }

        private void DrawLines(DrawingContext context)
        {
            if (Drawing == null || !Drawing.Shapes.Any())
            {
                return;
            }

            var scaleX = ActualWidth / Drawing.Width;
            var scaleY = ActualHeight / Drawing.Height;

            if (scaleX == 0.0 || scaleY == 0.0) return;


            //TODO init a drawing state object and call the draw abstract methode

            /*
            var drawingState = new DrawingState()
            {
            };
            */

        }
    }
}