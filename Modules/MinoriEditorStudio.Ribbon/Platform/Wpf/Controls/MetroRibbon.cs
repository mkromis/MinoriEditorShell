using Fluent;
using MahApps.Metro.Controls;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using MvxApplication = MvvmCross.Platforms.Wpf.Views.MvxApplication;

namespace MinoriEditorStudio.Ribbon.Platform.Wpf.Controls
{
    public class MetroRibbon : MetroWindow, IMvxWindow, IMvxWpfView, IRibbonWindow, IDisposable
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

        public string Identifier { get; set; }

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

        #region TitelBar
        /// <summary>
        /// Gets ribbon titlebar
        /// </summary>
        public RibbonTitleBar TitleBar
        {
            get => (RibbonTitleBar)GetValue(TitleBarProperty);
            private set => SetValue(TitleBarPropertyKey, value);
        }

        // ReSharper disable once InconsistentNaming
        private static readonly DependencyPropertyKey TitleBarPropertyKey =
            DependencyProperty.RegisterReadOnly(nameof(TitleBar), typeof(RibbonTitleBar), typeof(MetroRibbon), new PropertyMetadata());

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="TitleBar"/>.
        /// </summary>
        public static readonly DependencyProperty TitleBarProperty = TitleBarPropertyKey.DependencyProperty;

        #endregion

        public MetroRibbon()
        {
            Unloaded += MvxMetroRibbon_Unloaded;
            Loaded += MvxMetroRibbon_Loaded;
            Initialized += MvxMetroRibbon_Initialized;
        }


        private void MvxMetroRibbon_Initialized(object sender, EventArgs e)
        {
            if (this == Application.Current.MainWindow)
            {
                (Application.Current as MvxApplication).ApplicationInitialized();

                // Apply tempate if we are window. We invalidate later for rendering.
                String template =
                    "<DataTemplate " +
                    " xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"" +
                    " xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\"" +
                    " xmlns:fluent=\"urn:fluent-ribbon\">" +
                    "<fluent:RibbonTitleBar x:Name = \"ribbonTitleBar\" Header =\"{Binding RelativeSource={RelativeSource Mode=FindAncestor, " +
                    " AncestorType={x:Type Window}}, Path=Title}\" /> " +
                    "</DataTemplate> ";

                TitleTemplate = (DataTemplate)XamlReader.Parse(template);
            }
        }

        private void MvxMetroRibbon_Unloaded(object sender, RoutedEventArgs e)
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
        private void MvxMetroRibbon_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel?.ViewAppearing();
            ViewModel?.ViewAppeared();

            // Finding title must be done after init, (Time to render?)
            TitleBar = this.FindChild<RibbonTitleBar>("ribbonTitleBar");
            TitleBar?.InvalidateArrange();
            TitleBar?.UpdateLayout();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~MetroRibbon()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
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
