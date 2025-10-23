namespace Wpf.Controls;

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Core.Entities;

public partial class ExaminationPreviewControl : UserControl
{
    public ExaminationPreviewControl()
    {
        InitializeComponent();
    }

    public static DependencyProperty DataStreamsProperty = DependencyProperty.Register(nameof(DataStreams), typeof(List<ExaminationDataStream>), typeof(ExaminationPreviewControl),
        new PropertyMetadata(new List<ExaminationDataStream>(), OnMovesChanged));

    public IList<ExaminationDataStream> DataStreams
    {
        get => (IList<ExaminationDataStream>)GetValue(DataStreamsProperty);
        set => SetValue(DataStreamsProperty, value);
    }

    private static void OnMovesChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        var ctrl = (ExaminationPreviewControl)dependencyObject;
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
        DrawDataStreams(drawingContext);
    }

    private double _scaleX  = 1.0;
    private double _scaleY  = 1.0;
    private double _offsetX = 1.0;
    private double _offsetY = 1.0;

    private int _streamCount = 0;

    double ToX(double x)
    {
        return (x + _offsetX) * _scaleX;
    }

    double ToY(double y, int idx)
    {
        return ActualHeight - (ActualHeight * idx / _streamCount) - ((y + _offsetY) * _scaleY);
    }

    Point ToPoint(double x, double y, int idx)
    {
        Point pt = new Point();
        pt.X = ToX(x);
        pt.Y = ToY(y, idx);
        return pt;
    }

    private void DrawDataStreams(DrawingContext context)
    {
        if (DataStreams == null || DataStreams.Count == 0)
            return;

        int streamIndex = 0;

        _streamCount = DataStreams.Count;

        foreach (var dataStream in DataStreams)
        {
            DrawDataStream(context, dataStream, streamIndex++);
        }
    }

    private void DrawDataStream(DrawingContext context, ExaminationDataStream dataStream, int streamIndex)
    {
        var data = dataStream.MyValues;

        double periodTime = dataStream.Period;

        double minX = 0;
        double maxX = periodTime * data.Count;

        double minY = data.Min();
        double maxY = data.Max();

        var sizeX = maxX - minX;
        var sizeY = maxY - minY;

        _scaleX = ActualWidth / sizeX;
        _scaleY = ActualHeight / _streamCount / sizeY;

        _offsetX = -minX;
        _offsetY = -minY;

        if (_scaleX == 0.0 || _scaleY == 0.0) return;

        var lastPt = ToPoint(0, data.FirstOrDefault(), streamIndex);
        int idx    = 0;

        foreach (var val in data.Skip(1))
        {
            var ptNew = ToPoint((idx++) * periodTime, val, streamIndex);
            context.DrawLine(Pens[streamIndex % Pens.Length], lastPt, ptNew);

            lastPt = ptNew;
        }
    }
}