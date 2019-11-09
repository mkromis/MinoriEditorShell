using MinoriDemo.Core.Modules.VirtualCanvas.Models;
using MinoriEditorShell.Services;
using MinoriEditorShell.VirtualCanvas.Extensions;
using MinoriEditorShell.VirtualCanvas.Services;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Windows.Input;

namespace MinoriDemo.Core.ViewModels
{
    /// <summary>
    /// This demo shows the VirtualCanvas managing up to 50,000 random WPF shapes providing smooth scrolling and
    /// zooming while creating those shapes on the fly.  This helps make a WPF canvas that is a lot more
    /// scalable.
    /// </summary>
    public class VirtualCanvasViewModel : MesDocument, IMesVirtualCanvas
    {
        private readonly Boolean _animateStatus = true;
        private readonly Int32 _totalVisuals = 0;
        private readonly String[] _colorNames = new String[10];
        private readonly Color[] _baseColor = new Color[10];
        private readonly Double _tileWidth = 50;
        private readonly Double _tileHeight = 30;
        private readonly Double _tileMargin = 10;
        private readonly IMesStatusBar _statusbar;
        private Int32 _rows;
        private Int32 _cols;
        private Boolean _showGridLines;
        //private readonly Polyline _gridLines = new Polyline();

        public EventHandler IsClosing;

        public Boolean ShowContextRibbon => true;

        public ICommand OnHelpCommand => new MvxCommand(() =>
        {
            Mvx.IoCProvider.Resolve<IMesMessageBox>().Alert(
                "Click left mouse button and drag to pan the view " +
                "Hold Control-Key and run mouse wheel to zoom in and out " +
                "Click middle mouse button to turn on auto-scrolling " +
                "Hold Control-Key and drag the mouse with left button down to draw a rectangle to zoom into that region.",
                "User Interface");
        });

        public ICommand DumpQuadTreeCommand => new MvxCommand(() =>
        {
            //SaveFileDialog s = new SaveFileDialog
            //{
            //    FileName = "quadtree.xml"
            //};
            //if (s.ShowDialog() == true)
            //{
            //    using (StreamWriter w = new StreamWriter(s.FileName))
            //    {
            //        using (LogWriter log = new LogWriter(w))
            //        {
            //            log.Open("QuadTree");
            //            ((MinoriEditorStudio.VirtualCanvas.Controls.VirtualCanvas)Canvas.Graph).Index.Dump(log);
            //            log.Open("Other");
            //            log.WriteAttribute("MaxDepth", log.MaxDepth.ToString(CultureInfo.CurrentUICulture));
            //            log.Close();
            //            log.Close();
            //        }
            //    }
            //}
        });

        public ICommand RowColChange => new MvxCommand<Object>((x) =>
        {
            Int32 value = Int32.Parse(x.ToString());
            _rows = _cols = value;
            AllocateNodes();
        });

        public ICommand ZoomCommand => new MvxCommand<String>((x) =>
        {
            if (x == "Fit")
            {
                this.ZoomToContent(ZoomToContent.WidthAndHeight);
            }
            else
            {
                Double value = Double.Parse(x);
                Zoom.Value = value / 100;
                _statusbar.Text = $"Zoom is {Zoom.Value}";
            }
        });

        public Double ZoomValue
        {
            get => Zoom?.Value ?? 0;
            set => Zoom.Value = value;
        }

        public VirtualCanvasViewModel()
        {
            // Update Statusbar
            _statusbar = Mvx.IoCProvider.Resolve<IMesStatusBar>();
            _statusbar.Text = "Loading";
        }

        public override void ViewAppeared()
        {
            CanClose = false;
            DisplayName = "Virtual Canvas Sample";

            // Override ctrl with alt. (Test code)
            RectZoom.ConsoleModifiers = ConsoleModifiers.Alt;

            Zoom.ValueChanged += (s, e) =>
            {
                RaisePropertyChanged("ZoomValue");
                _statusbar.Text = $"Zoom:{e}";
            };

            RectZoom.ZoomReset += (s, e) => this.ZoomToContent(ZoomToContent.WidthAndHeight);

            // Set the background to see the boarder vs demo.
            Graph.ContentCanvas.SetCanvasBackgroundColor(Color.AliceBlue);

            // Do I even need this?
            //IVirtualCanvasControl graph = Canvas.Graph;
            //graph.SmallScrollIncrement = new Size(_tileWidth + _tileMargin, _tileHeight + _tileMargin);
            //graph.Scale.Changed += new EventHandler(OnScaleChanged);
            //graph.Translate.Changed += new EventHandler(OnScaleChanged);

            // Origianlly 100 x 100 nodes
            //AllocateNodes();

            // Update info 
            _statusbar.Text = "Ready";
        }
        
        private void AllocateNodes()
        {
            Zoom.ResetTranslate();

            // Fill a sparse grid of rectangular color palette nodes with each tile being 50x30.
            // with hue across x-axis and saturation on y-axis, brightness is fixed at 100;
            Random r = new Random(Environment.TickCount);
            Graph.VirtualChildren.Clear();
            Double w = _tileWidth + _tileMargin;
            Double h = _tileHeight + _tileMargin;
            Int32 count = _rows * _cols / 20;
            Double width = w * (_cols - 1);
            Double height = h * (_rows - 1);
            while (count > 0)
            {
                Double x = r.NextDouble() * width;
                Double y = r.NextDouble() * height;

                PointF pos = new PointF((Single)(_tileMargin + x), (Single)(_tileMargin + y));
                SizeF size = new SizeF(r.Next((Int32)_tileWidth, (Int32)_tileWidth * 5),
                                    r.Next((Int32)_tileHeight, (Int32)_tileHeight * 5));
                TestShapeType type = (TestShapeType)r.Next(0, (Int32)TestShapeType.Last);

                //Color color = HlsColor.ColorFromHLS((int)((x * 240) / _cols), 100, (int)(240 - ((y * 240) / _rows)));
                ITestShape shape = Mvx.IoCProvider.Resolve<ITestShape>();
                shape.Initialize(new RectangleF(pos, size), type, r);
                SetRandomBrushes(shape, r);
                Graph.AddVirtualChild(shape);
                count--;
            }
        }

        private void SetRandomBrushes(ITestShape s, Random r)
        {
            Int32 i = r.Next(0, 10);
            if (_baseColor[i].IsEmpty)
            {
                Color color = Color.FromArgb((Byte)r.Next(0, 255), (Byte)r.Next(0, 255), (Byte)r.Next(0, 255));


                _colorNames[i] = "#" + color.R.ToString("X2", CultureInfo.InvariantCulture) +
                    color.G.ToString("X2", CultureInfo.InvariantCulture) +
                    color.B.ToString("X2", CultureInfo.InvariantCulture);
                _baseColor[i] = color;
            }

            s.Label = _colorNames[i];
            s.BaseColor = _baseColor[i];
        }

        //void OnScaleChanged(Object sender, EventArgs e)
        //{
        //    // Make the grid lines get thinner as you zoom in
        //    MinoriEditorStudio.VirtualCanvas.Controls.VirtualCanvas graph = (MinoriEditorStudio.VirtualCanvas.Controls.VirtualCanvas)Canvas.Graph;
        //    Double t = _gridLines.StrokeThickness = 0.1 / graph.Scale.ScaleX;
        //    graph.Backdrop.BorderThickness = new Thickness(t);
        //}

        //private readonly Int32 lastTick = Environment.TickCount;
        //private readonly Int32 addedPerSecond = 0;
        //private readonly Int32 removedPerSecond = 0;

        //void OnVisualsChanged(Object sender, VisualChangeEventArgs e)
        //{
            //if (_animateStatus)
            //{
            //    StatusText.Text = string.Format(CultureInfo.InvariantCulture, "{0} live visuals of {1} total", grid.LiveVisualCount, _totalVisuals);

            //    int tick = Environment.TickCount;
            //    if (e.Added != 0 || e.Removed != 0)
            //    {
            //        addedPerSecond += e.Added;
            //        removedPerSecond += e.Removed;
            //        if (tick > lastTick + 100)
            //        {
            //            Created.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(
            //                Math.Min(addedPerSecond, 450),
            //                new Duration(TimeSpan.FromMilliseconds(100))));
            //            CreatedLabel.Text = addedPerSecond.ToString(CultureInfo.InvariantCulture) + " created";
            //            addedPerSecond = 0;

            //            Destroyed.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(
            //                Math.Min(removedPerSecond, 450),
            //                new Duration(TimeSpan.FromMilliseconds(100))));
            //            DestroyedLabel.Text = removedPerSecond.ToString(CultureInfo.InvariantCulture) + " disposed";
            //            removedPerSecond = 0;
            //        }
            //    }
            //    if (tick > lastTick + 1000)
            //    {
            //        lastTick = tick;
            //    }
            //}
        //}

        //void OnAnimateStatus(Object sender, RoutedEventArgs e)
        //{
        //    //MenuItem item = (MenuItem)sender;
        //    //_animateStatus = item.IsChecked = !item.IsChecked;

        //    //StatusText.Text = "";
        //    //Created.BeginAnimation(Rectangle.WidthProperty, null);
        //    //Created.Width = 0;
        //    //CreatedLabel.Text = "";
        //    //Destroyed.BeginAnimation(Rectangle.WidthProperty, null);
        //    //Destroyed.Width = 0;
        //    //DestroyedLabel.Text = "";
        //}

        public Boolean ShowGridLines
        {
            get => _showGridLines;
            set
            {
                if (SetProperty(ref _showGridLines, value))
                {
                    if (value)
                    {
                    Double width = _tileWidth + _tileMargin;
                    Double height = _tileHeight + _tileMargin;

                    Double numTileToAccumulate = 16;

                    //            Polyline gridCell = _gridLines;
                    //            gridCell.Margin = new Thickness(_tileMargin);
                    //            gridCell.Stroke = Brushes.Blue;
                    //            gridCell.StrokeThickness = 0.1;
                    //            gridCell.Points = new PointCollection(new Point[] { new Point(0, height-0.1),
                    //                new Point(width-0.1, height-0.1), new Point(width-0.1, 0) });
                    //            VisualBrush gridLines = new VisualBrush(gridCell)
                    //            {
                    //                TileMode = TileMode.Tile,
                    //                Viewport = new Rect(0, 0, 1.0 / numTileToAccumulate, 1.0 / numTileToAccumulate),
                    //                AlignmentX = AlignmentX.Center,
                    //                AlignmentY = AlignmentY.Center
                    //            };

                    //            VisualBrush outerVB = new VisualBrush();
                    //            Rectangle outerRect = new Rectangle
                    //            {
                    //                Width = 10.0,  //can be any size
                    //                Height = 10.0,
                    //                Fill = gridLines
                    //            };
                    //            outerVB.Visual = outerRect;
                    //            outerVB.Viewport = new Rect(0, 0,
                    //                width * numTileToAccumulate, height * numTileToAccumulate);
                    //            outerVB.ViewportUnits = BrushMappingMode.Absolute;
                    //            outerVB.TileMode = TileMode.Tile;

                    //            graph.Backdrop.Background = outerVB;

                    //            Border border = graph.Backdrop;
                    //            border.BorderBrush = Brushes.Blue;
                    //            border.BorderThickness = new Thickness(0.1);
                    //            graph.InvalidateVisual();
                    }
                    else
                    {
                    //            graph.Backdrop.Background = null;
                    }
                }
            }
        }

        public IMesAutoScroll AutoScroll { get; set; }
        public IMesVirtualCanvasControl Graph { get; set; }
        public IMesPan Pan { get; set; }
        public IMesRectangleSelectionGesture RectZoom { get; set; }
        public IMesMapZoom Zoom { get; set; }
    }
}
