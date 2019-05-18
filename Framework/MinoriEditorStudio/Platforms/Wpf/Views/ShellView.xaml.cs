using MinoriEditorStudio.Framework;
using MinoriEditorStudio.Framework.Services;
using MinoriEditorStudio.Modules.Shell.ViewModels;
using MinoriEditorStudio.Modules.Shell.Views;
using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Xceed.Wpf.AvalonDock;

namespace MinoriEditorStudio.Platforms.Wpf.Views
{
    [MvxViewFor(typeof(ShellViewModel))]
    public partial class ShellView : IShellView
	{
        public ShellView()
        {
            InitializeComponent();
            DataContext = Mvx.IoCProvider.Resolve<IShell>();
        }

        public void LoadLayout(
            Stream stream,
            Action<ITool> addToolCallback,
            Action<IDocument> addDocumentCallback,
            Dictionary<string, ILayoutItem> itemsState) =>
            LayoutUtility.LoadLayout(Manager, stream, addDocumentCallback, addToolCallback, itemsState);

        public void SaveLayout(Stream stream) => LayoutUtility.SaveLayout(Manager, stream);

        private void OnManagerLayoutUpdated(object sender, EventArgs e)
        {
            UpdateFloatingWindows();
        }

        public void UpdateFloatingWindows()
        {
            var mainWindow = Window.GetWindow(this);
            var mainWindowIcon = (mainWindow != null) ? mainWindow.Icon : null;
            var showFloatingWindowsInTaskbar = ((ShellViewModel)DataContext).ShowFloatingWindowsInTaskbar;
            foreach (var window in Manager.FloatingWindows)
            {
                window.Icon = mainWindowIcon;
                window.ShowInTaskbar = showFloatingWindowsInTaskbar;
            }
        }
    }
}
