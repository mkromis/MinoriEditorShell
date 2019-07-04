using MinoriEditorStudio.Services;
using MinoriEditorStudio.ViewModels;
using MvvmCross;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Xceed.Wpf.AvalonDock.Controls;

namespace MinoriEditorStudio.Platforms.Wpf.Views
{
    public partial class ManagerView : IManagerView
	{
        public ManagerView()
        {
            InitializeComponent();

            try
            {
                IManager manager = Mvx.IoCProvider.Resolve<IManager>();
                manager.ManagerView = this;
                DataContext = manager;
            } catch { }
        }

        public void LoadLayout(
            Stream stream,
            Action<ITool> addToolCallback,
            Action<IDocument> addDocumentCallback,
            Dictionary<String, ILayoutItem> itemsState) =>
            LayoutUtility.LoadLayout(Manager, stream, addDocumentCallback, addToolCallback, itemsState);

        public void SaveLayout(Stream stream) => LayoutUtility.SaveLayout(Manager, stream);

        private void OnManagerLayoutUpdated(Object sender, EventArgs e) => UpdateFloatingWindows();

        public void UpdateFloatingWindows()
        {
            Window mainWindow = Window.GetWindow(this);
            if (mainWindow != null)
            {
                ImageSource mainWindowIcon = mainWindow.Icon;
                Boolean showFloatingWindowsInTaskbar = ((ManagerViewModel)DataContext).ShowFloatingWindowsInTaskbar;
                foreach (LayoutFloatingWindowControl window in Manager?.FloatingWindows)
                {
                    window.Icon = mainWindowIcon;
                    window.ShowInTaskbar = showFloatingWindowsInTaskbar;
                }
            }
        }
    }
}
