﻿using MinoriEditorStudio.VirtualCanvas.Platforms.Wpf.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using RectangleF = System.Drawing.RectangleF;

namespace MinoriDemo.RibbonWPF.Modules.VirtualCanvas.Models
{

    enum TestShapeType { Ellipse, Curve, Rectangle, Last };

    class TestShape : IVirtualChild
    {
        RectangleF _bounds;
        public System.Windows.Media.Brush Fill { get; set; }
        public System.Windows.Media.Brush Stroke { get; set; }
        public String Label { get; set; }

        private readonly TestShapeType _shape;
        private readonly System.Windows.Point[] _points;

        public event EventHandler BoundsChanged;

        public TestShape(RectangleF bounds, TestShapeType s, Random r)
        {
            _bounds = bounds;
            _shape = s;
            if (s == TestShapeType.Curve)
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

        public UIElement Visual { get; private set; }

        public UIElement CreateVisual(MinoriEditorStudio.VirtualCanvas.Platforms.Wpf.Controls.VirtualCanvas parent)
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

        MinoriEditorStudio.VirtualCanvas.Platforms.Wpf.Controls.VirtualCanvas _parent;
        Typeface _typeface;
        Double _fontSize;

        public Size MeasureText(MinoriEditorStudio.VirtualCanvas.Platforms.Wpf.Controls.VirtualCanvas parent, String label)
        {
            if (_parent != parent)
            {
                FontFamily fontFamily = (FontFamily)parent.GetValue(TextBlock.FontFamilyProperty);
                FontStyle fontStyle = (FontStyle)parent.GetValue(TextBlock.FontStyleProperty);
                FontWeight fontWeight = (FontWeight)parent.GetValue(TextBlock.FontWeightProperty);
                FontStretch fontStretch = (FontStretch)parent.GetValue(TextBlock.FontStretchProperty);
                _fontSize = (Double)parent.GetValue(TextBlock.FontSizeProperty);
                _typeface = new Typeface(fontFamily, fontStyle, fontWeight, fontStretch);
                _parent = parent;
            }
            FormattedText ft = new FormattedText(label, CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight, _typeface, _fontSize, Brushes.Black);
            return new Size(ft.Width, ft.Height);
        }
    }
}
