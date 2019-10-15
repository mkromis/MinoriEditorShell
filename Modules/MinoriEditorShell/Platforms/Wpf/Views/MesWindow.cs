using MahApps.Metro.Controls;
using MinoriEditorShell.Platforms.Wpf.Themes;
using MinoriEditorShell.Services;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using MvxApplication = MvvmCross.Platforms.Wpf.Views.MvxApplication;

namespace MinoriEditorShell.Platforms.Wpf.Views
{
    public class MesWindow : MetroWindow, IMvxWindow, IMvxWpfView, IDisposable
    {
        private IMvxViewModel _viewModel;
        private IMvxBindingContext _bindingContext;

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

        public String Identifier { get; set; }

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

        public MesWindow()
        {
            Unloaded += MesWindow_Unloaded;
            Loaded += MesWindow_Loaded;
            Initialized += MesWindow_Initialized;
        }

        private void MesWindow_Initialized(Object sender, EventArgs e)
        {
            if (this == Application.Current.MainWindow)
            {
                (Application.Current as MvxApplication).ApplicationInitialized();
                Mvx.IoCProvider.Resolve<IMesThemeManager>();
            }
        }

        private void MesWindow_Unloaded(Object sender, RoutedEventArgs e)
        {
            ViewModel?.ViewDisappearing();
            ViewModel?.ViewDisappeared();
            ViewModel?.ViewDestroy();
        }

        private void MesWindow_Loaded(Object sender, RoutedEventArgs e)
        {

            IMesThemeManager manager = Mvx.IoCProvider.Resolve<IMesThemeManager>();
            IMesTheme theme = manager.Themes.Single(x => x is MesBlueTheme);
            manager.SetCurrentTheme(theme.Name);

            ViewModel?.ViewAppearing();
            ViewModel?.ViewAppeared();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~MesWindow()
        {
            Dispose(false);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                Unloaded -= MesWindow_Unloaded;
                Loaded -= MesWindow_Loaded;
            }
        }
    }

    public class MesWindow<TViewModel>
        : MesWindow
          , IMvxWpfView<TViewModel> where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get => (TViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }
    }
}
