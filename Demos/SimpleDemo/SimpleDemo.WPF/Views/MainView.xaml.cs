using Microsoft.Extensions.Logging;
using MvvmCross.Logging;
using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using SimpleDemo.Core.ViewModels;

namespace SimpleDemo.Wpf.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    [MvxContentPresentation]
    public partial class MainView : MvxWpfView
    {
        public MainView()
        {
            ILogger<MainView> log = MvxLogHost.GetLog<MainView>();
            log.LogDebug("View Init");
            InitializeComponent();
        }
    }
}