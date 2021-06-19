using MvvmCross.Platforms.Wpf.Presenters.Attributes;
using MvvmCross.Platforms.Wpf.Views;

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
            InitializeComponent();
        }
    }
}