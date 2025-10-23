namespace Wpf.Controls;

using System;
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
        new Pen(new SolidColorBrush(Colors.Gray),      1.0d),
        new Pen(new SolidColorBrush(Colors.Magenta),      1.0d),
        new Pen(new SolidColorBrush(Colors.Olive),      1.0d),
        new Pen(new SolidColorBrush(Colors.DarkGreen),      1.0d),
        new Pen(new SolidColorBrush(Colors.Peru),      1.0d),
        new Pen(new SolidColorBrush(Colors.Aqua),      1.0d)
        };


    protected override void OnRender(DrawingContext drawingContext)
    {
        DrawScript(drawingContext);
    }

    private double _scaleX  = 1.0;
    private double _scaleY  = 1.0;
    private double _offsetX = 1.0;
    private double _offsetY = 1.0;

    double ToX(double x)
    {
        return (x + _offsetX) * _scaleX;
    }

    double ToY(double y)
    {
        return /* ActualHeight - */ ((y + _offsetY) * _scaleY);
    }

    Point ToPoint(double x, double y)
    {
        Point pt = new Point();
        pt.X = ToX(x);
        pt.Y = ToY(y);
        return pt;
    }

    private void DrawScript(DrawingContext context)
    {
        if (Moves == null || Moves.Count == 0)
            return;

        int minX = 0;
        int maxX = 0;

        int minY = 0;
        int maxY = 0;

        int posX = 0;
        int posY = 0;

        int[] diffX = { -1, 0, 1, 1, 1, 0, -1, -1 };
        int[] diffY = { -1, -1, -1, 0, 1, 1, 1, 0 };

        foreach (var move in Moves)
        {
            posX += diffX[(int)move.Direction] * move.Count;
            posY += diffY[(int)move.Direction] * move.Count;

            if (posX > maxX) maxX = posX;
            if (posY > maxY) maxY = posY;
            if (posX < minX) minX = posX;
            if (posY < minY) minY = posY;
        }

        var sizeX = maxX - minX + 1;
        var sizeY = maxY - minY + 1;

        _scaleX = ActualWidth / sizeX;
        _scaleY = ActualHeight / sizeY;

        _scaleX = Math.Min(_scaleX, _scaleY);
        _scaleY = _scaleX;

        _offsetX = -minX;
        _offsetY = -minY;

        if (_scaleX == 0.0 || _scaleY == 0.0) return;

        posX = 0;
        posY = 0;
        var lastPt = ToPoint(0.5, 0.5);

        foreach (var move in Moves)
        {
            posX += diffX[(int)move.Direction] * move.Count;
            posY += diffY[(int)move.Direction] * move.Count;

            var ptNew = ToPoint(posX + 0.5, posY + 0.5);
            if (move.Color is not null)
            {
                int colorIdx = int.Min(move.Color.Value, Pens.Length - 1);
                context.DrawLine(Pens[colorIdx], lastPt, ptNew);
            }

            lastPt = ptNew;
        }
    }
}