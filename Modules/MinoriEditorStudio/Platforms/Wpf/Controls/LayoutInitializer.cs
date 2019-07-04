using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MinoriEditorStudio.Services;
using Xceed.Wpf.AvalonDock.Layout;

namespace MinoriEditorStudio.Platforms.Wpf.Controls
{
	public class LayoutInitializer : ILayoutUpdateStrategy
	{
		public Boolean BeforeInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableToShow, ILayoutContainer destinationContainer)
		{
            if (anchorableToShow.Content is ITool tool)
            {
                PaneLocation preferredLocation = tool.PreferredLocation;
                String paneName = GetPaneName(preferredLocation);
                LayoutAnchorablePane toolsPane = layout.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault(d => d.Name == paneName);
                if (toolsPane == null)
                {
                    switch (preferredLocation)
                    {
                        case PaneLocation.Left:
                            toolsPane = CreateAnchorablePane(layout, Orientation.Horizontal, paneName, InsertPosition.Start);
                            break;
                        case PaneLocation.Right:
                            toolsPane = CreateAnchorablePane(layout, Orientation.Horizontal, paneName, InsertPosition.End);
                            break;
                        case PaneLocation.Bottom:
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

		private static String GetPaneName(PaneLocation location)
		{
			switch (location)
			{
				case PaneLocation.Left:
					return "LeftPane";
				case PaneLocation.Right:
					return "RightPane";
				case PaneLocation.Bottom:
					return "BottomPane";
				default:
					throw new ArgumentOutOfRangeException("location");
			}
		}

        private static LayoutAnchorablePane CreateAnchorablePane(LayoutRoot layout, Orientation orientation,
            String paneName, InsertPosition position)
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
            if (anchorableShown.Content is ITool tool)
            {
                if (anchorableShown.Parent is LayoutAnchorablePane anchorablePane && anchorablePane.ChildrenCount == 1)
                {
                    switch (tool.PreferredLocation)
                    {
                        case PaneLocation.Left:
                        case PaneLocation.Right:
                            anchorablePane.DockWidth = new GridLength(tool.PreferredWidth, GridUnitType.Pixel);
                            break;
                        case PaneLocation.Bottom:
                            anchorablePane.DockHeight = new GridLength(tool.PreferredHeight, GridUnitType.Pixel);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }

        public Boolean BeforeInsertDocument(LayoutRoot layout, LayoutDocument anchorableToShow, ILayoutContainer destinationContainer) => false;

        public void AfterInsertDocument(LayoutRoot layout, LayoutDocument anchorableShown)
	    {
	        
	    }
	}
}
