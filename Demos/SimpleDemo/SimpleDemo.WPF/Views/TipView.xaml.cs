using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using SimpleDemo.Core.ViewModels;

namespace SimpleDemo.Wpf.Views
{
    [MvxContentPresentation]
    public partial class TipView
    {
        public TipView()
        {
            InitializeComponent();
        }
    }
}