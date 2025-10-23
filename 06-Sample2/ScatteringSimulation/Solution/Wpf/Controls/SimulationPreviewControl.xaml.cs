namespace Wpf.Controls;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public partial class SimulationPreviewControl : UserControl
{
    public SimulationPreviewControl()
    {
        InitializeComponent();
    }

    public static DependencyProperty SamplesProperty = DependencyProperty.Register(nameof(Samples), typeof(List<(double X, double Y, double Value)>), typeof(SimulationPreviewControl),
        new PropertyMetadata(new List<(double X, double Y, double Value)>(), OnSamplesChanged));

    public IList<(double X, double Y, double Value)> Samples
    {
        get => (IList<(double X, double Y, double Value)>)GetValue(SamplesProperty);
        set => SetValue(SamplesProperty, value);
    }

    private static void OnSamplesChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var ctrl = (SimulationPreviewControl)dependencyObject;
        ctrl.InvalidateVisual();
    }

    private static readonly Pen RedPen = new Pen(new SolidColorBrush(Colors.Red), 1.0d);


    protected override void OnRender(DrawingContext drawingContext)
    {
        DrawSamples(drawingContext);
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
        return ActualHeight - ((y + _offsetY) * _scaleY);
    }

    Point ToPoint(double x, double y)
    {
        Point pt = new Point();
        pt.X = ToX(x);
        pt.Y = ToY(y);
        return pt;
    }

    private void DrawSamples(DrawingContext context)
    {
        if (Samples == null || Samples.Count == 0)
            return;

        var minX = Samples.Min(pt => pt.X);
        var minY = Samples.Min(pt => pt.Y);

        var maxX = Samples.Max(pt => pt.X);
        var maxY = Samples.Max(pt => pt.Y);

        var sizeX = maxX - minX;
        var sizeY = maxY - minY;

        _scaleX = ActualWidth / sizeX;
        _scaleY = ActualHeight / sizeY;

        _scaleX = Math.Min(_scaleX, _scaleY);
        _scaleY = _scaleX;

        _offsetX = -minX;
        _offsetY = -minY;

        if (_scaleX == 0.0 || _scaleY == 0.0) return;

        foreach (var sample in Samples)
        {
            context.DrawEllipse(null, RedPen, ToPoint(sample.X, sample.Y), 1, 1);
        }
    }
}