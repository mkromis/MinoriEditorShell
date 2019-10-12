using Fluent;
using MahApps.Metro.Controls;
using MinoriEditorShell.Platforms.Wpf.Views;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using MvxApplication = MvvmCross.Platforms.Wpf.Views.MvxApplication;

namespace MinoriEditorShell.Ribbon.Platform.Wpf.Views
{
    public class MesRibbonWindow : MesWindow, IMvxWindow, IMvxWpfView, IRibbonWindow, IDisposable
    {
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
            DependencyProperty.RegisterReadOnly(nameof(TitleBar), typeof(RibbonTitleBar), typeof(MesRibbonWindow), new PropertyMetadata());

        /// <summary>
        /// <see cref="DependencyProperty"/> for <see cref="TitleBar"/>.
        /// </summary>
        public static readonly DependencyProperty TitleBarProperty = TitleBarPropertyKey.DependencyProperty;
        #endregion

        public MesRibbonWindow() : base()
        {
            Loaded += MesRibbonWindow_Loaded;
            Initialized += MesRibbonWindow_Initialized;
        }

        private void MesRibbonWindow_Initialized(Object sender, EventArgs e)
        {
            if (this == Application.Current.MainWindow)
            {
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


        /// <summary>
        /// Initial Metro setup
        /// </summary>
        /// <seealso cref="https://fluentribbon.github.io/documentation/interop_with_MahApps.Metro"/>
        /// <seealso cref="https://stackoverflow.com/questions/5755455/how-to-set-control-template-in-code"/>
        private void MesRibbonWindow_Loaded(Object sender, RoutedEventArgs e)
        {
            // Finding title must be done after init, (Time to render?)
            TitleBar = this.FindChild<RibbonTitleBar>("ribbonTitleBar");
            TitleBar?.InvalidateArrange();
            TitleBar?.UpdateLayout();
        }
    }

    public class MesRibbonWindow<TViewModel> : MesRibbonWindow, IMvxWpfView<TViewModel> where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get => (TViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }
    }
}
