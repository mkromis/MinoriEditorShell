using AvalonDock.Layout;
using MinoriEditorShell.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MinoriEditorShell.Platforms.Wpf.Controls
{
#warning Not Implemented
#if false
    public class MesLayoutInitializer : ILayoutUpdateStrategy
    {
        public bool BeforeInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableToShow, ILayoutContainer destinationContainer)
        {
            if (anchorableToShow.Content is IMesTool tool)
            {
                MesPaneLocation preferredLocation = tool.PreferredLocation;
                string paneName = GetPaneName(preferredLocation);
                LayoutAnchorablePane toolsPane = layout.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault(d => d.Name == paneName);
                if (toolsPane == null)
                {
                    switch (preferredLocation)
                    {
                        case MesPaneLocation.Left:
                            toolsPane = CreateAnchorablePane(layout, Orientation.Horizontal, paneName, InsertPosition.Start);
                            break;

                        case MesPaneLocation.Right:
                            toolsPane = CreateAnchorablePane(layout, Orientation.Horizontal, paneName, InsertPosition.End);
                            break;

                        case MesPaneLocation.Bottom:
                            toolsPane = CreateAnchorablePane(layout, Orientation.Vertical, paneName, InsertPosition.End);
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                toolsPane.Children.Add(anchorableToShow);
                return true;
            }

            return false;
        }

        private static string GetPaneName(MesPaneLocation location)
        {
            switch (location)
            {
                case MesPaneLocation.Left:
                    return "LeftPane";

                case MesPaneLocation.Right:
                    return "RightPane";

                case MesPaneLocation.Bottom:
                    return "BottomPane";

                default:
                    throw new ArgumentOutOfRangeException("location");
            }
        }

        private static LayoutAnchorablePane CreateAnchorablePane(LayoutRoot layout, Orientation orientation,
            string paneName, InsertPosition position)
        {
            LayoutPanel parent = layout.Descendents().OfType<LayoutPanel>().First(d => d.Orientation == orientation);
            LayoutAnchorablePane toolsPane = new LayoutAnchorablePane { Name = paneName };
            if (position == InsertPosition.Start)
            {
                parent.InsertChildAt(0, toolsPane);
            }
            else
            {
                parent.Children.Add(toolsPane);
            }

            return toolsPane;
        }

        private enum InsertPosition
        {
            Start,
            End
        }

        public void AfterInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableShown)
        {
            // If this is the first anchorable added to this pane, then use the preferred size.
            if (anchorableShown.Content is IMesTool tool)
            {
                if (anchorableShown.Parent is LayoutAnchorablePane anchorablePane && anchorablePane.ChildrenCount == 1)
                {
                    switch (tool.PreferredLocation)
                    {
                        case MesPaneLocation.Left:
                        case MesPaneLocation.Right:
                            anchorablePane.DockWidth = new GridLength(tool.PreferredWidth, GridUnitType.Pixel);
                            break;

                        case MesPaneLocation.Bottom:
                            anchorablePane.DockHeight = new GridLength(tool.PreferredHeight, GridUnitType.Pixel);
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        public bool BeforeInsertDocument(LayoutRoot layout, LayoutDocument anchorableToShow, ILayoutContainer destinationContainer) => false;

        public void AfterInsertDocument(LayoutRoot layout, LayoutDocument anchorableShown)
        {
        }
    }
#endif
}