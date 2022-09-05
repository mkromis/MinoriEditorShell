using MahApps.Metro.Controls;
using MinoriEditorShell.Services;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using System;
using System.Linq;
using System.Windows;
using MvxApplication = MvvmCross.Platforms.Wpf.Views.MvxApplication;

namespace MinoriEditorShell.Platforms.Wpf.Views
{
    /// <inheritdoc cref="IMesWindow"/>
    public class MesWindow : MetroWindow, IMesWindow, IMvxWindow, IMvxWpfView
    {
        private IMvxViewModel _viewModel;
        private IMvxBindingContext _bindingContext;

        /// <inheritdoc />
        public IMvxViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                DataContext = value;
                BindingContext.DataContext = value;
            }
        }
        /// <inheritdoc />
        public string Identifier { get; set; }
        /// <inheritdoc />
        public IMvxBindingContext BindingContext
        {
            get
            {
                if (_bindingContext != null)
                {
                    return _bindingContext;
                }

                if (Mvx.IoCProvider != null)
                {
                    this.CreateBindingContext();
                }

                return _bindingContext;
            }
            set => _bindingContext = value;
        }
        /// <inheritdoc />
        public string DisplayName
        {
            get => Title;
            set => Title = value;
        }

        /// <summary>
        /// Opens as main window
        /// </summary>
        public MesWindow()
        {
            Unloaded += MesWindow_Unloaded;
            Loaded += MesWindow_Loaded;
            Initialized += MesWindow_Initialized;
        }

        private void MesWindow_Initialized(object sender, EventArgs e)
        {
            if (this == Application.Current.MainWindow)
            {
                // Application startup
                (Application.Current as MvxApplication).ApplicationInitialized();

                // Init mes setup
                Mvx.IoCProvider.Resolve<IMesThemeManager>();

#warning fix style
                //WindowCloseButtonStyle = Application.Current.Resources.MergedDictionaries
                //WindowCloseButtonStyle = "{DynamicResource MetroWindowButtonStyle}"
                //WindowMinButtonStyle = "{DynamicResource MetroWindowButtonStyle}"
                //WindowMaxButtonStyle = "{DynamicResource MetroWindowButtonStyle}"
            }
        }

        private void MesWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            ViewModel?.ViewDisappearing();
            ViewModel?.ViewDisappeared();
            ViewModel?.ViewDestroy();
        }

        /// <summary>
        /// Load inital settings if possible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MesWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string blueTheme = "MesBlueTheme";

            // Get main theme property if possible
            string themeName = Properties.Settings.Default.ThemeName;
            if (string.IsNullOrEmpty(themeName)) themeName = blueTheme;

            // Set theme from name
            IMesThemeManager manager = Mvx.IoCProvider.Resolve<IMesThemeManager>();
            IMesTheme theme = manager.Themes.FirstOrDefault(x => x.GetType().Name == themeName);

            // Set to default if missing or error
            theme ??= manager.Themes.First(x => x.GetType().Name == themeName);

            manager.SetCurrentTheme(theme.Name);

            ViewModel?.ViewAppearing();
            ViewModel?.ViewAppeared();
        }
    }

    /// <summary>
    /// Generics implementation of main window
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public class MesWindow<TViewModel> : MesWindow, IMvxWpfView<TViewModel> where TViewModel : class, IMvxViewModel
    {
        /// <inheritdoc />
        public new TViewModel ViewModel
        {
            get => (TViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }
        /// <inheritdoc />
        MvxFluentBindingDescriptionSet<IMvxWpfView<TViewModel>, TViewModel> IMvxWpfView<TViewModel>.CreateBindingSet() => 
            this.CreateBindingSet<IMvxWpfView<TViewModel>, TViewModel>();
    }
}