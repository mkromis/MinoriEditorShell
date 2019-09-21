using MinoriEditorStudio.Platforms.Wpf.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Localization;
using System.Windows.Controls;

namespace MinoriEditorStudio.Platforms.Wpf.Views
{
    /// <summary>
    ///     Interaction logic for MainMenuSettingsView.xaml
    /// </summary>
    public partial class MainMenuSettingsView
    {
        public MainMenuSettingsView()
        {
            InitializeComponent();

            DataContextChanged += (s, e) =>
            {
                using (MvxFluentBindingDescriptionSet<MainMenuSettingsView, MainMenuSettingsViewModel> bindingSet =
                    this.CreateBindingSet<MainMenuSettingsView, MainMenuSettingsViewModel>())
                {
                    bindingSet
                        .Bind(ColorThemeLabel)
                        .For(l => l.Content)
                        .To(vm => vm.TextSource)
                        .WithConversion<MvxLanguageConverter>("MainMenuSettingsColorTheme")
                        .WithFallback("MainMenuSettingsColorTheme");
                    bindingSet.Apply();
                }
            };
        }
    }
}