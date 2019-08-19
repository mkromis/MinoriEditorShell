﻿using MinoriDemo.Core.Modules.VirtualCanvas.Models;
using MinoriEditorStudio.VirtualCanvas.Service;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using RectangleF = System.Drawing.RectangleF;

namespace MinoriDemo.RibbonWPF.Modules.VirtualCanvas.Models
{
    public class TestShape : ITestShape
    {
        RectangleF _bounds;
        public Brush Fill { get; set; }
        public Brush Stroke { get; set; }
        public String Label { get; set; }

        private TestShapeType _shape;
        private Point[] _points;

        public event EventHandler BoundsChanged;

        public void Initialize(RectangleF bounds, TestShapeType shape, Random r)
        {
            _bounds = bounds;
            _shape = shape;
            if (_shape == TestShapeType.Curve)
            {
                _bounds.Width *= 2;
                _bounds.Height *= 2;
                BoundsChanged?.Invoke(this, null);

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

        public Object Visual { get; private set; }

        public Object CreateVisual(IVirtualCanvasControl parent)
        {
            if (Visual == null)
            {
                switch (_shape)
                {
                    case TestShapeType.Curve:
                        {
                            PathGeometry g = new PathGeometry();
                            PathFigure f = new PathFigure
                            {
                                StartPoint = _points[0]
                            };
                            g.Figures.Add(f);
                            for (Int32 i = 0, n = _points.Length; i < n; i += 3)
                            {
                                BezierSegment s = new BezierSegment(_points[i], _points[i + 1], _points[i + 2], true);
                                f.Segments.Add(s);
                            }
                            Path p = new Path
                            {
                                Data = g,

                                Stroke = Stroke,
                                StrokeThickness = 2
                            };

                            //DropShadowBitmapEffect effect = new DropShadowBitmapEffect();
                            //effect.Opacity = 0.8;
                            //effect.ShadowDepth = 3;
                            //effect.Direction = 270;
                            //c.BitmapEffect = effect;
                            Visual = p;
                            break;
                        }
                    case TestShapeType.Ellipse:
                        {
                            Canvas c = new Canvas();

                            Ellipse e = new Ellipse();
                            c.Width = e.Width = _bounds.Width;
                            c.Height = e.Height = _bounds.Height;
                            c.Children.Add(e);

                            Size s = MeasureText(parent, Label);
                            Double x = (_bounds.Width - s.Width) / 2;
                            Double y = (_bounds.Height - s.Height) / 2;

                            TextBlock text = new TextBlock
                            {
                                Text = Label
                            };
                            Canvas.SetLeft(text, x);
                            Canvas.SetTop(text, y);
                            c.Children.Add(text);

                            e.StrokeThickness = 2;
                            e.Stroke = Stroke;
                            e.Fill = Fill;

                            //DropShadowBitmapEffect effect = new DropShadowBitmapEffect();
                            //effect.Opacity = 0.8;
                            //effect.ShadowDepth = 3;
                            //effect.Direction = 270;
                            //c.BitmapEffect = effect;
                            Visual = c;
                            break;
                        }
                    case TestShapeType.Rectangle:
                        {
                            Border b = new Border
                            {
                                CornerRadius = new CornerRadius(3),
                                Width = _bounds.Width,
                                Height = _bounds.Height
                            };
                            TextBlock text = new TextBlock
                            {
                                Text = Label,
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Center
                            };

                            // Testing for BoundsChanged event.
                            b.MouseRightButtonDown += (s, e) =>
                            {
                                _bounds = new RectangleF(
                                    Bounds.X + 10,
                                    Bounds.Y + 10,
                                    (Single)b.Width,
                                    (Single)b.Height);
                                BoundsChanged?.Invoke(this, null);
                            };
                            b.Child = text;
                            b.Background = Fill;
                            //DropShadowBitmapEffect effect = new DropShadowBitmapEffect();
                            //effect.Opacity = 0.8;
                            //effect.ShadowDepth = 3;
                            //effect.Direction = 270;
                            //b.BitmapEffect = effect;
                            Visual = b;
                            break;
                        }
                }
            }
            return Visual;
        }

        public void DisposeVisual() => Visual = null;

        public RectangleF Bounds => _bounds;

        IVirtualCanvasControl _parent;
        Typeface _typeface;
        Double _fontSize;

        public Size MeasureText(IVirtualCanvasControl parent, String label)
        {
            if (_parent != parent && parent is MinoriEditorStudio.VirtualCanvas.Platforms.Wpf.Controls.VirtualCanvas control)
            {
                FontFamily fontFamily = (FontFamily)control.GetValue(TextBlock.FontFamilyProperty);
                FontStyle fontStyle = (FontStyle)control.GetValue(TextBlock.FontStyleProperty);
                FontWeight fontWeight = (FontWeight)control.GetValue(TextBlock.FontWeightProperty);
                FontStretch fontStretch = (FontStretch)control.GetValue(TextBlock.FontStretchProperty);
                _fontSize = (Double)control.GetValue(TextBlock.FontSizeProperty);
                _typeface = new Typeface(fontFamily, fontStyle, fontWeight, fontStretch);
                _parent = parent;
            }
            FormattedText ft = new FormattedText(label, CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight, _typeface, _fontSize, Brushes.Black);
            return new Size(ft.Width, ft.Height);
        }

        public void SetRandomBrushes(Random r)
        {
            //Int32 i = r.Next(0, 10);
            //if (_strokeBrushes[i] == null)
            //{
            //    Color color = Color.FromRgb((Byte)r.Next(0, 255), (Byte)r.Next(0, 255), (Byte)r.Next(0, 255));
            //    HlsColor hls = new HlsColor(color);
            //    Color c1 = hls.Darker(0.25f);
            //    Color c2 = hls.Lighter(0.25f);
            //    Brush fill = new LinearGradientBrush(Color.FromArgb(0x80, c1.R, c1.G, c1.B),
            //        Color.FromArgb(0x80, color.R, color.G, color.B), 45);
            //    Brush stroke = new LinearGradientBrush(Color.FromArgb(0x80, color.R, color.G, color.B),
            //        Color.FromArgb(0x80, c2.R, c2.G, c2.B), 45);

            //    _colorNames[i] = "#" + color.R.ToString("X2", CultureInfo.InvariantCulture) +
            //        color.G.ToString("X2", CultureInfo.InvariantCulture) +
            //        color.B.ToString("X2", CultureInfo.InvariantCulture);
            //    _strokeBrushes[i] = stroke;
            //    _fillBrushes[i] = fill;
            //}

            //s.Label = _colorNames[i];
            //s.Stroke = _strokeBrushes[i];
            //s.Fill = _fillBrushes[i];
        }
    }
}
