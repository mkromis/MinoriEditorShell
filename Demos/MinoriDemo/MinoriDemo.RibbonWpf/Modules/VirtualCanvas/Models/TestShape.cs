using MinoriDemo.Core.Modules.VirtualCanvas.Models;
using MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Controls;
using MinoriEditorShell.VirtualCanvas.Services;
using System;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Brushes = System.Windows.Media.Brushes;
using Color = System.Drawing.Color;
using FontFamily = System.Windows.Media.FontFamily;
using FontStyle = System.Windows.FontStyle;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace MinoriDemo.RibbonWPF.Modules.VirtualCanvas.Models;

public class TestShape : ITestShape
{
    private IMesVirtualCanvasControl _parent;
    private Typeface _typeface;
    private double _fontSize;
    private Color _baseColor;
    private RectangleF _bounds;
    private TestShapeType _shape;
    private Point[] _points;
    private LinearGradientBrush _fill;
    private LinearGradientBrush _stroke;

    public event EventHandler BoundsChanged;

    public string Label { get; set; }

    public Color BaseColor
    {
        get => _baseColor;
        set
        {
            _baseColor = value;

            // use hls color for shading
            HlsColor hls = new(_baseColor);
            Color c1 = hls.Darker(0.25f);
            Color c2 = hls.Lighter(0.25f);

            // Create fill and stroke when color is created.
            // this is created here because of wpf dependency
            _fill = new LinearGradientBrush(
                System.Windows.Media.Color.FromArgb(0x80, c1.R, c1.G, c1.B),
                System.Windows.Media.Color.FromArgb(0x80, _baseColor.R, _baseColor.G, _baseColor.B), 45);
            _stroke = new LinearGradientBrush(
                System.Windows.Media.Color.FromArgb(0x80, _baseColor.R, _baseColor.G, _baseColor.B),
                System.Windows.Media.Color.FromArgb(0x80, c2.R, c2.G, c2.B), 45);
        }
    }

    public void Initialize(RectangleF bounds, TestShapeType shape, Random r)
    {
        _bounds = bounds;
        _shape = shape;
        if (_shape == TestShapeType.Curve)
        {
            _bounds.Width *= 2;
            _bounds.Height *= 2;
            BoundsChanged?.Invoke(this, EventArgs.Empty);

            _points = new Point[3];

            bounds = new RectangleF(0, 0, _bounds.Width, _bounds.Height);
            switch (r.Next(0, 8))
            {
                case 0:
                    _points[0] = new Point(bounds.Left, bounds.Top);
                    _points[1] = new Point(bounds.Right, bounds.Top);
                    _points[2] = new Point(bounds.Right, bounds.Bottom);
                    break;

                case 1:
                    _points[0] = new Point(bounds.Right, bounds.Top);
                    _points[1] = new Point(bounds.Right, bounds.Bottom);
                    _points[2] = new Point(bounds.Left, bounds.Right);
                    break;

                case 2:
                    _points[0] = new Point(bounds.Right, bounds.Bottom);
                    _points[1] = new Point(bounds.Left, bounds.Bottom);
                    _points[2] = new Point(bounds.Left, bounds.Top);
                    break;

                case 3:
                    _points[0] = new Point(bounds.Left, bounds.Bottom);
                    _points[1] = new Point(bounds.Left, bounds.Top);
                    _points[2] = new Point(bounds.Right, bounds.Top);
                    break;

                case 4:
                    _points[0] = new Point(bounds.Left, bounds.Top);
                    _points[1] = new Point(bounds.Right, bounds.Height / 2);
                    _points[2] = new Point(bounds.Left, bounds.Bottom);
                    break;

                case 5:
                    _points[0] = new Point(bounds.Right, bounds.Top);
                    _points[1] = new Point(bounds.Left, bounds.Height / 2);
                    _points[2] = new Point(bounds.Right, bounds.Bottom);
                    break;

                case 6:
                    _points[0] = new Point(bounds.Left, bounds.Top);
                    _points[1] = new Point(bounds.Width / 2, bounds.Bottom);
                    _points[2] = new Point(bounds.Right, bounds.Top);
                    break;

                case 7:
                    _points[0] = new Point(bounds.Left, bounds.Bottom);
                    _points[1] = new Point(bounds.Width / 2, bounds.Top);
                    _points[2] = new Point(bounds.Right, bounds.Bottom);
                    break;
            }
        }
    }

    public object Visual { get; private set; }

    public object CreateVisual(IMesVirtualCanvasControl parent)
    {
        if (Visual != null)
        {
            return Visual;
        }

        switch (_shape)
        {
            case TestShapeType.Curve:
                PathGeometry g = new();
                PathFigure f = new() { StartPoint = _points[0] };
                g.Figures.Add(f);
                for (int i = 0, n = _points.Length; i < n; i += 3)
                {
                    BezierSegment bs = new(_points[i], _points[i + 1], _points[i + 2], true);
                    f.Segments.Add(bs);
                }

                Path p = new() { Data = g, Stroke = _stroke, StrokeThickness = 2 };

                //DropShadowBitmapEffect effect = new DropShadowBitmapEffect();
                //effect.Opacity = 0.8;
                //effect.ShadowDepth = 3;
                //effect.Direction = 270;
                //c.BitmapEffect = effect;
                Visual = p;
                break;
            case TestShapeType.Ellipse:
                Canvas c = new();

                Ellipse e = new();
                c.Width = e.Width = _bounds.Width;
                c.Height = e.Height = _bounds.Height;
                c.Children.Add(e);

                Size s = MeasureText(parent, Label);
                double x = (_bounds.Width - s.Width) / 2;
                double y = (_bounds.Height - s.Height) / 2;

                TextBlock text = new() { Text = Label };
                Canvas.SetLeft(text, x);
                Canvas.SetTop(text, y);
                c.Children.Add(text);

                e.StrokeThickness = 2;
                e.Stroke = _stroke;
                e.Fill = _fill;

                //DropShadowBitmapEffect effect = new DropShadowBitmapEffect();
                //effect.Opacity = 0.8;
                //effect.ShadowDepth = 3;
                //effect.Direction = 270;
                //c.BitmapEffect = effect;
                Visual = c;
                break;
            case TestShapeType.Rectangle:
                Border b = new()
                {
                    CornerRadius = new CornerRadius(3), Width = _bounds.Width, Height = _bounds.Height
                };
                TextBlock tb = new()
                {
                    Text = Label,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                // Testing for BoundsChanged event.
                b.MouseRightButtonDown += (_, _) =>
                {
                    _bounds = new RectangleF(
                        Bounds.X + 10,
                        Bounds.Y + 10,
                        (float)b.Width,
                        (float)b.Height);
                    BoundsChanged?.Invoke(this, EventArgs.Empty);
                };
                b.Child = tb;
                b.Background = _fill;
                //DropShadowBitmapEffect effect = new DropShadowBitmapEffect();
                //effect.Opacity = 0.8;
                //effect.ShadowDepth = 3;
                //effect.Direction = 270;
                //b.BitmapEffect = effect;
                Visual = b;
                break;
        }

        return Visual;
    }

    public void DisposeVisual() => Visual = null;

    public RectangleF Bounds => _bounds;

    public Size MeasureText(IMesVirtualCanvasControl parent, string label)
    {
        if (_parent != parent && parent is MesVirtualCanvas control)
        {
            FontFamily fontFamily = (FontFamily)control.GetValue(TextBlock.FontFamilyProperty);
            FontStyle fontStyle = (FontStyle)control.GetValue(TextBlock.FontStyleProperty);
            FontWeight fontWeight = (FontWeight)control.GetValue(TextBlock.FontWeightProperty);
            FontStretch fontStretch = (FontStretch)control.GetValue(TextBlock.FontStretchProperty);
            _fontSize = (double)control.GetValue(TextBlock.FontSizeProperty);
            _typeface = new Typeface(fontFamily, fontStyle, fontWeight, fontStretch);
            _parent = parent;
        }

        if (Visual is not Visual v)
        {
            return Size.Empty;
        }

        FormattedText ft = new(label, CultureInfo.CurrentUICulture,
            FlowDirection.LeftToRight, _typeface, _fontSize, Brushes.Black,
            VisualTreeHelper.GetDpi(v).PixelsPerDip);
        return new Size(ft.Width, ft.Height);
    }
}