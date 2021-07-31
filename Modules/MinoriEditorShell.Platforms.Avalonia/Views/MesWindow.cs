// using MahApps.Metro.Controls;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using MinoriEditorShell.Services;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
// using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using System;
using System.Linq;
using System.Windows;
// using MvxApplication = MvvmCross.Platforms.Wpf.Views.MvxApplication;

namespace MinoriEditorShell.Platforms.Avalonia.Views
{
    public class MesWindow : Window, IMesWindow, IMesAvnView, IDisposable
    {
        private IMvxBindingContext _bindingContext;
        private bool _unloaded = false;
        private IMvxViewModel _viewModel;
        public MesWindow()
        {
            Closed += MvxWindow_Closed;
            Opened += MvxWindow_Opened;
            Initialized += MvxWindow_Initialized;
        }

        ~MesWindow()
        {
            Dispose(false);
        }

        public IMvxBindingContext BindingContext
        {
            get
            {
                if (_bindingContext != null)
                    return _bindingContext;

                if (Mvx.IoCProvider != null)
                    this.CreateBindingContext();

                return _bindingContext;
            }
            set => _bindingContext = value;
        }

        public String DisplayName { get; set; }

        public string Identifier { get; set; }

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
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Opened -= MvxWindow_Opened;
                Closed -= MvxWindow_Closed;
            }
        }

        private void MvxWindow_Closed(object sender, EventArgs e) => Unload();

        private void MvxWindow_Initialized(object sender, EventArgs e)
        {
            //if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            //{
                //if (this == desktop.MainWindow)
                //{
                (Application.Current as MesApplication).ApplicationInitialized();
                //}
            //}
        }


        private void MvxWindow_Opened(Object sender, EventArgs e)
        {
            ViewModel?.ViewAppearing();
            ViewModel?.ViewAppeared();
        }
        private void MvxWindow_Unloaded(object sender, RoutedEventArgs e) => Unload();
        private void Unload()
        {
            if (!_unloaded)
            {
                ViewModel?.ViewDisappearing();
                ViewModel?.ViewDisappeared();
                ViewModel?.ViewDestroy();
                _unloaded = true;
            }
        }
    }

    public class MvxWindow<TViewModel> : MesWindow, IMesAvnView<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public MvxFluentBindingDescriptionSet<IMesAvnView<TViewModel>, TViewModel> CreateBindingSet()
        {
            //return this.CreateBindingSet<IMesAvnfView<TViewModel>, TViewModel>();
            throw new NotImplementedException();
        }
    }
    //     public class MesWindow : MetroWindow, IMesWindow, IMvxWindow, IMvxWpfView
    //     {
    //         private IMvxViewModel _viewModel;
    //         private IMvxBindingContext _bindingContext;

    //         public IMvxViewModel ViewModel
    //         {
    //             get => _viewModel;
    //             set
    //             {
    //                 _viewModel = value;
    //                 DataContext = value;
    //                 BindingContext.DataContext = value;
    //             }
    //         }

    //         public String Identifier { get; set; }

    //         public IMvxBindingContext BindingContext
    //         {
    //             get
    //             {
    //                 if (_bindingContext != null)
    //                 {
    //                     return _bindingContext;
    //                 }

    //                 if (Mvx.IoCProvider != null)
    //                 {
    //                     this.CreateBindingContext();
    //                 }

    //                 return _bindingContext;
    //             }
    //             set => _bindingContext = value;
    //         }

    //         public String DisplayName
    //         {
    //             get => Title;
    //             set => Title = value;
    //         }

    //         public MesWindow()
    //         {
    //             Unloaded += MesWindow_Unloaded;
    //             Loaded += MesWindow_Loaded;
    //             Initialized += MesWindow_Initialized;
    //         }

    //         private void MesWindow_Initialized(Object sender, EventArgs e)
    //         {
    //             if (this == Application.Current.MainWindow)
    //             {
    //                 // Application startup
    //                 (Application.Current as MvxApplication).ApplicationInitialized();

    //                 // Init mes setup
    //                 Mvx.IoCProvider.Resolve<IMesThemeManager>();

    // #warning fix style
    //                 //WindowCloseButtonStyle = Application.Current.Resources.MergedDictionaries
    //                 //WindowCloseButtonStyle = "{DynamicResource MetroWindowButtonStyle}"
    //                 //WindowMinButtonStyle = "{DynamicResource MetroWindowButtonStyle}"
    //                 //WindowMaxButtonStyle = "{DynamicResource MetroWindowButtonStyle}"
    //             }
    //         }

    //         private void MesWindow_Unloaded(Object sender, RoutedEventArgs e)
    //         {
    //             ViewModel?.ViewDisappearing();
    //             ViewModel?.ViewDisappeared();
    //             ViewModel?.ViewDestroy();
    //         }

    //         /// <summary>
    //         /// Load inital settings if possible
    //         /// </summary>
    //         /// <param name="sender"></param>
    //         /// <param name="e"></param>
    //         private void MesWindow_Loaded(Object sender, RoutedEventArgs e)
    //         {
    //             String blueTheme = "MesBlueTheme";

    //             // Get main theme property if possible
    //             String themeName = Properties.Settings.Default.ThemeName;
    //             if (String.IsNullOrEmpty(themeName)) themeName = blueTheme;

    //             // Set theme from name
    //             IMesThemeManager manager = Mvx.IoCProvider.Resolve<IMesThemeManager>();
    //             IMesTheme theme = manager.Themes.FirstOrDefault(x => x.GetType().Name == themeName);

    //             // Set to defualt if missing or error
    //             if (theme == null) theme = manager.Themes.First(x => x.GetType().Name == themeName);

    //             manager.SetCurrentTheme(theme.Name);

    //             ViewModel?.ViewAppearing();
    //             ViewModel?.ViewAppeared();
    //         }
    //     }

    //     public class MesWindow<TViewModel> : MesWindow, IMvxWpfView<TViewModel> where TViewModel : class, IMvxViewModel
    //     {
    //         public new TViewModel ViewModel
    //         {
    //             get => (TViewModel)base.ViewModel;
    //             set => base.ViewModel = value;
    //         }

    //         MvxFluentBindingDescriptionSet<IMvxWpfView<TViewModel>, TViewModel> IMvxWpfView<TViewModel>.CreateBindingSet()
    //         {
    //             return this.CreateBindingSet<IMvxWpfView<TViewModel>, TViewModel>();
    //         }
    //     }
}