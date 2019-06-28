using MahApps.Metro.Controls;
using MinoriEditorStudio.Services;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using MvxApplication = MvvmCross.Platforms.Wpf.Views.MvxApplication;

namespace MinoriEditorStudio.Platforms.Wpf.Views
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
            Unloaded += MvxMetroRibbon_Unloaded;
            Loaded += MvxMetroRibbon_Loaded;
        }

        private void MvxMetroRibbon_Unloaded(Object sender, RoutedEventArgs e)
        {
            ViewModel?.ViewDisappearing();
            ViewModel?.ViewDisappeared();
            ViewModel?.ViewDestroy();
        }

        /// <summary>
        /// Initial Metro setup
        /// </summary>
        /// <seealso cref="https://fluentribbon.github.io/documentation/interop_with_MahApps.Metro"/>
        /// <seealso cref="https://stackoverflow.com/questions/5755455/how-to-set-control-template-in-code"/>
        private void MvxMetroRibbon_Loaded(Object sender, RoutedEventArgs e)
        {
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
                Unloaded -= MvxMetroRibbon_Unloaded;
                Loaded -= MvxMetroRibbon_Loaded;
            }
        }

    }

    public class MvxWindow<TViewModel> : MvxWindow, IMvxWpfView<TViewModel> where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get => (TViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }
    }
}
