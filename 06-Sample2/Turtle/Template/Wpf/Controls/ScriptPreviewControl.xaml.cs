namespace Wpf.Controls;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Move = Core.Turtle.Move;

public partial class ScriptPreviewControl : UserControl
{
    public ScriptPreviewControl()
    {
        InitializeComponent();
    }

    public static DependencyProperty MovesProperty = DependencyProperty.Register(nameof(Moves), typeof(List<Move>), typeof(ScriptPreviewControl),
        new PropertyMetadata(new List<Move>(), OnMovesChanged));

    public IList<Move> Moves
    {
        get => (IList<Move>)GetValue(MovesProperty);
        set => SetValue(MovesProperty, value);
    }

    private static void OnMovesChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var ctrl = (ScriptPreviewControl)dependencyObject;
        ctrl.InvalidateVisual();
    }

    private static readonly Pen[] Pens =
    {
        new Pen(new SolidColorBrush(Colors.Red),        1.0d),
        new Pen(new SolidColorBrush(Colors.Orange),     1.0d),
        new Pen(new SolidColorBrush(Colors.Yellow),     1.0d),
        new Pen(new SolidColorBrush(Colors.Green),      1.0d),
        new Pen(new SolidColorBrush(Colors.Blue),       1.0d),
        new Pen(new SolidColorBrush(Colors.DarkBlue),   1.0d),
        new Pen(new SolidColorBrush(Colors.Violet),     1.0d),
        new Pen(new SolidColorBrush(Colors.DarkViolet), 1.0d),
        new Pen(new SolidColorBrush(Colors.Black),      1.0d),
        new Pen(new SolidColorBrush(Colors.White),      1.0d),
        new Pen(new SolidColorBrush(Colors.Gray),       1.0d),
        new Pen(new SolidColorBrush(Colors.Magenta),    1.0d),
        new Pen(new SolidColorBrush(Colors.Olive),      1.0d),
        new Pen(new SolidColorBrush(Colors.DarkGreen),  1.0d),
        new Pen(new SolidColorBrush(Colors.Peru),       1.0d),
        new Pen(new SolidColorBrush(Colors.Aqua),       1.0d)
    };


    protected override void OnRender(DrawingContext drawingContext)
    {
        //  scaleX = ActualWidth / sizeX;
        //  scaleY = ActualHeight / sizeY;

        //  scaleX = Math.Min(scaleX, scaleY);
        //  scaleY = scaleX;

        //  offsetX = -minX;
        //  offsetY = -minY;
    }

    /*
        double ToX(double x)
        {
            return (x + _offsetX) * _scaleX;
        }

        double ToY(double y)
        {
            return  ((y + _offsetY) * _scaleY);
            // return ActualHeight - ((y + _offsetY) * _scaleY);
        }

        Point ToPoint(double x, double y)
        {
            Point pt = new Point();
            pt.X = ToX(x);
            pt.Y = ToY(y);
            return pt;
        }
      */
}