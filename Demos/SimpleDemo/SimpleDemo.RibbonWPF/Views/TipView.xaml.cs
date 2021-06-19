using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;

namespace SimpleDemo.RibbonWpfCore.Views
{
    /// <summary>
    /// Interaction logic for TipView.xaml
    /// </summary>
    [MvxContentPresentation]
    public partial class TipView : MvxWpfView
    {
        public TipView() => InitializeComponent();
    }
}